import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  isValidLoginData: boolean = true; //check email and password login is correct?

  onSubmit(form: NgForm) {
    if (form.valid) {
      // loginData { email, password, remember }
      const loginData = form.value;
      this.isValidLoginData = false;      
    }
  }
}
