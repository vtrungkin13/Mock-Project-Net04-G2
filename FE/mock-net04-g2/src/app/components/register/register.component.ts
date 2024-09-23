import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { RegisterService } from '../../services/register-service/register.service';
import { ToastComponent } from '../shared/toast/toast.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink, ToastComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  isExistedEmail: boolean = false; //check registered email have existed in db?
  isPasswordMatch: boolean = false; //check password and re-password is match?
  registerFailMessage: string = '';
  isPhoneInvalid: boolean = false; // check if phone number is valid

  constructor(
    private router: Router,
    private registerService: RegisterService
  ) {}

  isSubmitting = false;

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.isSubmitting = true;

      //registerData { email, name, phone, dob, password, confirmPassword }
      const registerData = form.value;
      // this.isExistedEmail = true;
      if (registerData.confirmPassword !== registerData.password) {
        this.isPasswordMatch = false;
      } else {
        this.isPasswordMatch = true;
      }
      const phonePattern = /^[0-9]{10,11}$/;
      if (!phonePattern.test(registerData.phone)) {
        this.isPhoneInvalid = true;
        return;
      } else {
        this.isPhoneInvalid = false;
      }
      this.registerService.register(registerData).subscribe({
        next: (response) => {
          this.storeUserLoginData(response);
          this.isSubmitting = false;
          this.router.navigateByUrl('/');
        },
        error: (error) => {
          this.isSubmitting = false;
          this.isExistedEmail = error.error.error === 'Email already exists';
          this.registerFailMessage = error.error.error;
        },
      });
    }
  }

  onEmailInput() {
    this.isExistedEmail = false;
  }

  storeUserLoginData(userData: any) {
    sessionStorage.setItem('token', userData.body.token);
    sessionStorage.setItem('user', JSON.stringify(userData.body.user));
  }
}
