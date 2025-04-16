import { Component, OnDestroy, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { DialogService, DynamicDialogComponent } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from '../../../shared/constants/messages.constants';
import { ClassDetailComponent } from './class-detail.component';
import { ClassEnrollComponent } from './class-enroll.component';
import { AdminApiClassApiClient, ClassDto, ClassDtoPageResult, ClassScheduleDto, EnrollmentDto } from '../../../api/admin-api.service.generated';
import { AlertService } from '../../../shared/services/alert.service';
import { ClassScheduleComponent } from './class-schedule.component';

@Component({
  selector: 'app-class',
  templateUrl: './class.component.html',
  styleUrls: ['./class.component.scss'],
})
export class ClassComponent implements OnInit, OnDestroy {

  //System variables
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;


  //Paging variables
  public pageIndex: number = 1;
  public pageSize: number = 10;
  public totalCount: number;

  //Business variables
  public items: ClassDto[];
  public selectedItems: ClassDto[] = [];
  public keyword: string = '';

  constructor(
    private deviceService: AdminApiClassApiClient,
    public dialogService: DialogService,
    private alertService: AlertService,
    private confirmationService: ConfirmationService) { }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.toggleBlockUI(true);

    this.deviceService.getClassesPaging(this.keyword, this.pageIndex, this.pageSize)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ClassDtoPageResult) => {
          this.items = response.results;
          this.totalCount = response.rowCount;

          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);

        }
      });
  }

  showAddModal() {
    const ref = this.dialogService.open(ClassDetailComponent, {
      header: 'Thêm thiết bị',
      width: '70%'
    });
    const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
    const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
    const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
    dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
    ref.onClose.subscribe((data: ClassDto) => {
      if (data) {
        this.alertService.showSuccess(MessageConstants.CREATED_OK_MSG);
        this.selectedItems = [];
        this.loadData();
      }
    });
  }

  pageChanged(event: any): void {
    this.pageIndex = event.page;
    this.pageSize = event.rows;
    this.loadData();
  }

  showEditModal() {
    if (this.selectedItems.length == 0) {
      this.alertService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    
    var id = this.selectedItems[0].id;
    const ref = this.dialogService.open(ClassDetailComponent, {
      data: {
        id: id
      },
      header: 'Cập nhật lớp học',
      width: '70%'
    });
    const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
    const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
    const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
    dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
    ref.onClose.subscribe((data: ClassDto) => {
      if (data) {
        this.alertService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.loadData();
      }
    });
  }

/* Check */
showEnrollModal() {
  if (this.selectedItems.length == 0) {
    this.alertService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
    return;
  }
  
  var id = this.selectedItems[0].id;
  const ref = this.dialogService.open(ClassEnrollComponent, {
    data: {
      id: id
    },
    header: 'Đăng kí sinh viên vào lớp',
    width: '70%'
  });
  const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
  const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
  const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
  dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
  ref.onClose.subscribe((data: EnrollmentDto) => {
    if (data) {
      this.alertService.showSuccess(MessageConstants.UPDATED_OK_MSG);
      this.selectedItems = [];
      this.loadData();
    }
  });
}

showClassScheduleModal() {
  if (this.selectedItems.length == 0) {
    this.alertService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
    return;
  }
  
  var id = this.selectedItems[0].id;
  const ref = this.dialogService.open(ClassScheduleComponent, {
    data: {
      id: id
    },
    header: 'Đăng kí sinh viên vào lớp',
    width: '70%'
  });
  const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
  const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
  const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
  dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
  ref.onClose.subscribe((data: ClassScheduleDto) => {
    if (data) {
      this.alertService.showSuccess(MessageConstants.UPDATED_OK_MSG);
      this.selectedItems = [];
      this.loadData();
    }
  });
}
/* End check */


  deleteItems() {
    if (this.selectedItems.length == 0) {
      this.alertService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    var ids = [];
    this.selectedItems.forEach(element => {
      ids.push(element.id);
    });
    this.confirmationService.confirm({
      message: MessageConstants.CONFIRM_DELETE_MSG,
      accept: () => {
        this.deleteItemsConfirm(ids)
      }
    });
  }

  deleteItemsConfirm(ids: any[]) {
    this.toggleBlockUI(true);

    this.deviceService.deleteClass(ids)
      .subscribe({
        next: () => {
          this.alertService.showSuccess(MessageConstants.DELETED_OK_MSG);
          this.loadData();
          this.selectedItems = [];
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        }
      });
  }
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    }
    else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }

  }

}