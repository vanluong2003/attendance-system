import { Component } from '@angular/core';
import { AdminApiTestApiClient } from '../../../api/admin-api.service.generated';

@Component({
  selector: 'app-post',
  templateUrl: './class.component.html'
})
export class ClassComponent {

  constructor(private testApiClient: AdminApiTestApiClient) {  }
  test() {
    this.testApiClient.testAuthen().subscribe({
      next: () => {
        console.log('ok');
      },
      error: (error: any) => {
        console.log(error);
      },
    });
  }
}
