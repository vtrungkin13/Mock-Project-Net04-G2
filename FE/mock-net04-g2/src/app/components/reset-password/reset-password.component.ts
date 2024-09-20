import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ChangepasswordService } from '../../services/changepassword-service/changepassword.service';
import { ResetpasswordService } from '../../services/resetpassword-service/resetpassword.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
})
export class ResetPasswordComponent {
  isExistedEmail: boolean = true; //check registered email have existed in db?
  resetPasswordFailMessage: string = ''; // Store error message
  resetPasswordSuccess: boolean = false; // Flag to track success

  constructor(private router: Router, private resetPasswordService : ResetpasswordService) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      const email = form.value.email;
      //this.isExistedEmail = false;
      this.resetPasswordService.resetPassword(email).subscribe({
        next: (response) => {
          this.isExistedEmail = true; 
          this.resetPasswordSuccess = true; // Show success message
        },
        error: (error) => {
          this.isExistedEmail = false;
          this.resetPasswordFailMessage = error.error.error;
          this.resetPasswordSuccess = false; // reset to false in case of failure
        },
      });
    } 
  }

  onEmailInput() {
    this.isExistedEmail = true;
    this.resetPasswordSuccess = false;
  }
}
