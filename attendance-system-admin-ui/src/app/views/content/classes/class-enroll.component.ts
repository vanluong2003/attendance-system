import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { AdminApiClassApiClient, AdminApiEnrollmentApiClient, ClassDto, StudentInClassDto, StudentInClassDtoPageResult } from '../../../api/admin-api.service.generated';
import { UtilityService } from '../../../shared/services/utility.service';

@Component({
    templateUrl: 'class-enroll.component.html'
})
export class ClassEnrollComponent implements OnInit, OnDestroy {
    private ngUnsubscribe = new Subject<void>();

    // Default
    public blockedPanelDetail: boolean = false;
    public form: FormGroup;
    public title: string;
    public btnDisabled = false;
    public saveBtnName: string;
    public closeBtnName: string;
    selectedEntity = {} as ClassDto;

    public selectedItems: StudentInClassDto[] = [];
    public items: StudentInClassDto[];

    public pageIndex: number = 1;
    public pageSize: number = 10;
    public totalCount: number;

    formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

    constructor(
        public ref: DynamicDialogRef,
        public config: DynamicDialogConfig,
        private classService: AdminApiClassApiClient,
        private enrollmentService: AdminApiEnrollmentApiClient,

        private utilService: UtilityService,
        private fb: FormBuilder) {
    }

    ngOnDestroy(): void {
        if (this.ref) {
            this.ref.close();
        }
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
    
    ngOnInit() {
        this.buildForm();
        if (this.utilService.isEmpty(this.config.data?.id) == false) {
            this.loadDetail(this.config.data.id);
            this.saveBtnName = 'Thêm sinh viên';
            this.closeBtnName = 'Hủy';
        } else {
            this.saveBtnName = 'Thêm';
            this.closeBtnName = 'Đóng';
        }
    }

    loadData() {
    this.toggleBlockUI(true);

    this.enrollmentService.getEnrollmentsPaging(this.selectedEntity.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: StudentInClassDtoPageResult) => {
          this.items = response.results;
          this.totalCount = 50;

          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);

        }
      });
  }

    // Validate
    noSpecial: RegExp = /^[^<>*!_~]+$/
    validationMessages = {
        'location': [
            { type: 'required', message: 'Bạn phải nhập tên' },
            { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
            { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' }
        ]
    }

    loadDetail(id: any) {
        this.toggleBlockUI(true);
        this.classService.getClassById(id)
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
                next: (response: ClassDto) => {
                    this.selectedEntity = response;
                    this.buildForm();
                    this.toggleBlockUI(false);
                    this.loadData();

                }
                , error: () => {
                    this.toggleBlockUI(false);
                }
            });
    }
    saveChange() {
        this.toggleBlockUI(true);

        this.saveData();
    }

    private saveData() {
        this.classService.createClassEnroll(this.form.value)
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe(() => {
                this.ref.close(this.form.value);
                this.toggleBlockUI(false);
            });
    }

    buildForm() {
        this.form = this.fb.group({
            classId: new FormControl(this.selectedEntity.id || null, Validators.required),
            studentId: new FormControl(null, Validators.compose([
                Validators.required,
                Validators.maxLength(255),
                Validators.minLength(3)
            ]))
        });
    }


    private toggleBlockUI(enabled: boolean) {
        if (enabled == true) {
            this.btnDisabled = true;
            this.blockedPanelDetail = true;
        }
        else {
            setTimeout(() => {
                this.btnDisabled = false;
                this.blockedPanelDetail = false;
            }, 1000);
        }

    }
}
