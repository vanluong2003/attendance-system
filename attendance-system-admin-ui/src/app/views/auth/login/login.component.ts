import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminApiAuthApiClient, AuthenticatedResult, LoginRequest } from '../../../api/admin-api.service.generated';
import { AlertService } from '../../../shared/services/alert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  constructor(private fb: FormBuilder, 
    private authApiClient: AdminApiAuthApiClient, 
    private alertService: AlertService,
    private router: Router) {
    this.loginForm = this.fb.group({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
   }
   login() {
    var request: LoginRequest = new LoginRequest({
      userName: this.loginForm.controls['userName'].value,
      password: this.loginForm.controls['password'].value
    });
    this.authApiClient.login(request).subscribe({
      next: (res: AuthenticatedResult) => {
        //Save token and refresh token to localstorage

        //Redirect to dashboard
        this.router.navigate(['/dashboard']);
      },
      error: (error: any) => {
        console.log(error);
        this.alertService.showError('Login invalid');
      },
    });
   }
}
