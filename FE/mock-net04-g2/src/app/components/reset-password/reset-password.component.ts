import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
})
export class ResetPasswordComponent {
  isExistedEmail: boolean = true; //check registered email have existed in db?

  onSubmit(form: NgForm) {
    if (form.valid) {
      const email = form.value.email;
      this.isExistedEmail = false;
    }
  }

  onEmailInput() {
    this.isExistedEmail = true;
  }
}
