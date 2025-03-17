import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { ResultModel } from '../../models/result.model';
import { LoginResponseModel } from '../../models/login.response.model';
import { Router, RouterLink } from '@angular/router';
import { FlexiToastService } from 'flexi-toast';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  model = signal<LoginModel>(new LoginModel());

  constructor(
    private http: HttpClient,
    private router: Router,
    private toast: FlexiToastService
  ) {}

  login() {
    console.log(this.model());

    this.http
      .post<ResultModel<LoginResponseModel>>(
        'https://localhost:7076/api/Auth/Login',
        this.model()
      )
      .subscribe({
        next: (res) => {
          localStorage.setItem('accessToken', res.data!.access_token);
          this.router.navigateByUrl('/');
          this.toast.showToast('Success', 'Login is successful.');
        },
        error: (err: HttpErrorResponse) => {
          console.log(err);

          if (err.error.errorMessages) {
            const e = err.error.errorMessages;
            e.forEach((el: string) => {
              this.toast.showToast('Error', el, 'error');
            });
          } else {
            this.toast.showToast('Error', 'Something went wrong!', 'error');
          }
        },
      });
  }
}
