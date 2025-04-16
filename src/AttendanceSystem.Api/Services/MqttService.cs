using System.Text;
using System.Text.Json;
using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.SeedWorks;
using AttendanceSystem.Data.SeedWorks;
using AutoMapper;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using static AttendanceSystem.Core.Domain.Content.Attendance;

namespace AttendanceSystem.Api.Services
{
    public class MqttService
    {
        private IMqttClient _mqttClient;
        private IMqttClientOptions _mqttOptions;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        public MqttService(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();
            _scopeFactory = scopeFactory;
            _mapper = mapper;

            _mqttOptions = new MqttClientOptionsBuilder()
                .WithClientId("ASPNET-API-Client") 
                .WithTcpServer("broker.emqx.io", 1883)
                .Build();

            // Action after connection successfull
            _mqttClient.UseConnectedHandler(async e =>
            {
                Console.WriteLine("MQTT connected!");

                // Subscribe topic
                await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                    .WithTopic("attsys/test")
                    .Build());

                // Send message announce success
                await PublishAsync("attsys/test", "API đã kết nối MQTT thành công!");
            });

            // Handle message receive
            _mqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                var topic = e.ApplicationMessage.Topic;
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                Console.WriteLine($"Received message on topic {topic}: {payload}");

                using var doc = JsonDocument.Parse(payload);
                var root = doc.RootElement;
                string location = root.GetProperty("location").GetString();
                string uid = root.GetProperty("uid").GetString();

                Console.WriteLine($"Location: {location}");
                Console.WriteLine($"UID: {uid}");
                if(location != null && uid != null)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var student = await unitOfWork.Users.GetUserByUIDAsync(uid);
                    if (student == null)
                    {
                        await PublishAsync("attsys/test", $"Không tồn tại sinh viên");
                        return;
                    }
                    Console.WriteLine(student);
                    var classSchedule = await unitOfWork.ClassSchedules.GetScheduleByDateAndLocation(location, DateTime.Now);
                    if (classSchedule == null)
                    {
                        await PublishAsync("attsys/test", $"Không có lịch học tại thời điểm này");
                        return;
                    }
                    Console.WriteLine(classSchedule);
                    var enrollment = await unitOfWork.Enrollments.GetEnrollmentByStudentAndClass(student.Id, classSchedule.ClassId);
                    if (enrollment == null)
                    {
                        await PublishAsync("attsys/test", $"Sinh viên không có trong danh sách lớp");
                        return;
                    }
                    Console.WriteLine(enrollment);
                    var attendance = unitOfWork.Attendances.GetAttendanceByStudentAndClassSchedule(student.Id, classSchedule.Id);
                    if(attendance != null)
                    {
                        await PublishAsync("attsys/test", $"Sinh viên đã điểm danh");
                        return;
                    }
                    unitOfWork.Attendances.Add(new Attendance
                    {
                        ScheduleId = classSchedule.Id,
                        StudentId = student.Id,
                        Status = AttendanceStatus.Present,
                        CheckInTime = DateTime.Now
                    });
                    var result = await unitOfWork.CompeleAsync();
                    if( result > 0)
                    {
                        await PublishAsync("attsys/test", $"Điểm danh thành công");
                        Console.WriteLine("Điểm danh thành công!");
                        return;
                    }
                }
                await PublishAsync("attsys/test", $"[API phản hồi] Đã nhận được: {payload}");
            });
        }

        public async Task ConnectAsync()
        {
            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ConnectAsync(_mqttOptions);
            }
        }

        public async Task PublishAsync(string topic, string message)
        {
            if (!_mqttClient.IsConnected)
                await ConnectAsync();

            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithExactlyOnceQoS()
                .WithRetainFlag(false)
                .Build();

            await _mqttClient.PublishAsync(mqttMessage);
        }
    }
}
