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

  // 1: success, 2: fail, 3: loading
  resetPasswordStatus: number = 0;
  resetPasswordMessage: string = '';
  resetPasswordShowToast: boolean = false;

  constructor(
    private router: Router,
    private resetPasswordService: ResetpasswordService
  ) {}

  handleShowToast(status: number, message: string) {
    this.resetPasswordShowToast = true;
    this.resetPasswordStatus = status;
    this.resetPasswordMessage = message;
    setTimeout(() => {
      this.resetPasswordShowToast = false;
    }, 3000);
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      const email = form.value.email;
      //this.isExistedEmail = false;
      this.resetPasswordService.resetPassword(email).subscribe({
        next: (response) => {
          this.isExistedEmail = true;
          this.handleShowToast(
            1,
            'Thành công. Kiểm tra email để lấy mật khẩu mới'
          );
          this.resetPasswordSuccess = true; // Show success message
        },
        error: (error) => {
          this.isExistedEmail = false;
          this.handleShowToast(2, error.error.error);
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
