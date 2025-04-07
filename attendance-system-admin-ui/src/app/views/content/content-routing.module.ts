import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClassComponent } from './classes/class.component';
import { AuthGuard } from '../../shared/auth.guard';
import { DeviceComponent } from './devices/device.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'classes',
    pathMatch: 'full',
  },
  {
    path: 'classes',
    component: ClassComponent,
    data: {
      title: 'Lớp học',
      requiredPolicy: 'Permissions.Classes.View'
    },
    canActivate: [AuthGuard],
  },
  {
    path: 'devices',
    component: DeviceComponent,
    data: {
      title: 'Thiết bị',
      requiredPolicy: 'Permissions.Devices.View'
    },
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ContentRoutingModule {}
