import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { SystemRoutingModule } from './system-routing.module';
import { UserComponent } from './users/user.component';
import { RoleComponent } from './roles/role.component';
import { TableModule } from 'primeng/table';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { BlockUIModule } from 'primeng/blockui';
import { PaginatorModule } from 'primeng/paginator';
import { PanelModule } from 'primeng/panel';
import { CheckboxModule } from 'primeng/checkbox';
import { SharedModule } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { RolesDetailComponent } from './roles/roles-detail.component';
import { TeduSharedModule } from '../../shared/modules/tedu-shared.module';
import { KeyFilterModule } from 'primeng/keyfilter';
import {PermissionGrantComponent} from './roles/permission-grant.component';
import { ChangeEmailComponent } from './users/change-email.component';
import { RoleAssignComponent } from './users/role-assign.component';
import { SetPasswordComponent } from './users/set-password.component';
import { UserDetailComponent } from './users/user-detail.component';
import { BadgeModule } from 'primeng/badge';
import { PickListModule } from 'primeng/picklist';
import { ImageModule } from 'primeng/image';

@NgModule({
  imports: [
    SystemRoutingModule,
    CommonModule,
    ReactiveFormsModule, 
    TableModule,
    ProgressSpinnerModule,
    BlockUIModule,
    PaginatorModule,
    PanelModule,
    CheckboxModule,
    SharedModule,
    ButtonModule,
    InputTextModule,
    KeyFilterModule,
    TeduSharedModule,
    BadgeModule,
    PickListModule,
    ImageModule
  ],
  declarations: [
    UserComponent, 
    RoleComponent, 
    RolesDetailComponent, 
    PermissionGrantComponent,
    ChangeEmailComponent,
    RoleAssignComponent,
    SetPasswordComponent,
    UserDetailComponent
   ]
})
export class SystemModule {
}
