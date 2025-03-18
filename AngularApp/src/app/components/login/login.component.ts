import { Component, signal } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { LoginResponseModel } from '../../models/login.response.model';
import { Router, RouterLink } from '@angular/router';
import { FlexiToastService } from 'flexi-toast';
import { FormsModule } from '@angular/forms';
import { HttpService } from '../../services/http.service';

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
    private http: HttpService,
    private router: Router,
    private toast: FlexiToastService
  ) {}

  login() {
    this.http.post<LoginResponseModel>('Auth/Login', this.model(), (res) => {
      localStorage.setItem('accessToken', res.access_token);
      this.router.navigateByUrl('/');
      this.toast.showToast('Success', 'Login is successful.');
    });
  }
}
