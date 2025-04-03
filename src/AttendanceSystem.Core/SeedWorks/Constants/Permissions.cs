using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Core.SeedWorks.Constants
{
    public static class Permissions
    {
        public static class Dashboard
        {
            [Description("Xem dashboard")]
            public const string View = "Permissions.Dashboard.View";
        }
        public static class Roles
        {
            [Description("Xem quyền")]
            public const string View = "Permissions.Roles.View";
            [Description("Tạo mới quyền")]
            public const string Create = "Permissions.Roles.Create";
            [Description("Sửa quyền")]
            public const string Edit = "Permissions.Roles.Edit";
            [Description("Xóa quyền")]
            public const string Delete = "Permissions.Roles.Delete";
        }
        public static class Users
        {
            [Description("Xem người dùng")]
            public const string View = "Permissions.Users.View";
            [Description("Tạo người dùng")]
            public const string Create = "Permissions.Users.Create";
            [Description("Sửa người dùng")]
            public const string Edit = "Permissions.Users.Edit";
            [Description("Xóa người dùng")]
            public const string Delete = "Permissions.Users.Delete";
        }
        public static class Classes
        {
            [Description("Xem lớp học")]
            public const string View = "Permissions.Classes.View";
            [Description("Tạo tạo lớp học")]
            public const string Create = "Permissions.Classes.Create";
            [Description("Sửa lớp học")]
            public const string Edit = "Permissions.Classes.Edit";
            [Description("Xóa lớp học")]
            public const string Delete = "Permissions.Classes.Delete";
        }

        public static class Devices
        {
            [Description("Xem thiết bị")]
            public const string View = "Permissions.Devices.View";
            [Description("Tạo thiết bị")]
            public const string Create = "Permissions.Devices.Create";
            [Description("Sửa thiết bị")]
            public const string Edit = "Permissions.Devices.Edit";
            [Description("Xóa thiết bị")]
            public const string Delete = "Permissions.Devices.Delete";
        }
    }
}
