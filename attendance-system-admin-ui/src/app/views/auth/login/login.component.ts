import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminApiAuthApiClient, AuthenticatedResult, LoginRequest } from '../../../api/admin-api.service.generated';
import { AlertService } from '../../../shared/services/alert.service';
import { UrlConstants } from '../../../shared/constants/url.constants'
import { TokenStorageService } from '../../../shared/services/token-storage.service';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnDestroy{
  loginForm: FormGroup;
  private ngUnsubcribe = new Subject<void>();
  loading =false;
  constructor(private fb: FormBuilder, 
    private authApiClient: AdminApiAuthApiClient, 
    private alertService: AlertService,
    private router: Router,
    private tokenStorageService: TokenStorageService) {
    this.loginForm = this.fb.group({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
   }
  ngOnDestroy(): void {
    this.ngUnsubcribe.next();
    this.ngUnsubcribe.complete();
  }
   login() {
    this.loading = true;
    var request: LoginRequest = new LoginRequest({
      userName: this.loginForm.controls['userName'].value,
      password: this.loginForm.controls['password'].value
    });
    this.authApiClient.login(request)
    .pipe(takeUntil(this.ngUnsubcribe))
    .subscribe({
      next: (res: AuthenticatedResult) => {
        //Save token and refresh token to localstorage
        this.tokenStorageService.saveToken(res.token);
        this.tokenStorageService.saveRefreshToken(res.refreshToken);
        this.tokenStorageService.saveUser(res);
        //Redirect to dashboard
        this.router.navigate([UrlConstants.HOME]);
      },
      error: (error: any) => {
        console.log(error);
        this.alertService.showError('Login invalid');
        this.loading = false;
      },
    });
   }
}
