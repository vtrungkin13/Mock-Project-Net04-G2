import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  isExistedEmail: boolean = false; //check registered email have existed in db?
  isPasswordMatch: boolean = false; //check password and re-password is match?

  onSubmit(form: NgForm) {
    if (form.valid) {
      //registerData { email, name, phone, dob, password, rePassword }
      const registerData = form.value;
      this.isExistedEmail = true;
      if (registerData.rePassword !== registerData.password) {
        this.isPasswordMatch = false;
      } else {
        this.isPasswordMatch = true;
      }
    }
  }

  onEmailInput() {
    this.isExistedEmail = false;
  }
}
