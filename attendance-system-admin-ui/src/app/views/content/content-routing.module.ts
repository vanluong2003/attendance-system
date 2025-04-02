import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClassComponent } from './classes/class.component';
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
      title: 'Classes',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ContentRoutingModule {}
