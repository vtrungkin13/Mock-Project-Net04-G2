import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth-service/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  isValidLoginData: boolean = true; //check email and password login is correct?
  loginFailMessage: string = '';

  constructor(private router: Router, private authService: AuthService) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      // loginData { email, password, remember }
      const loginData = form.value;
      this.authService.login(loginData).subscribe({
        next: (response) => {
          this.isValidLoginData = true;
          this.storeUserLoginData(response);
          this.router.navigateByUrl('/');
        },
        error: (error) => {
          this.isValidLoginData = false;
          this.loginFailMessage = error.error.error;
        },
      });
    }
  }

  storeUserLoginData(userData: any) {
    sessionStorage.setItem('token', userData.body.token);
    sessionStorage.setItem('user', JSON.stringify(userData.body.user));
  }
}
