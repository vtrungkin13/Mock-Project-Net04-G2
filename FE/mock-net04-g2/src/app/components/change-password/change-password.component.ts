import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ChangepasswordService } from '../../services/changepassword-service/changepassword.service';
import { ToastComponent } from '../shared/toast/toast.component';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [FormsModule, ToastComponent],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss',
})
export class ChangePasswordComponent {
  isPasswordMatch: boolean = false; //check password and re-password is match?
  changePasswordFailMessage: string = ''; // Store error message if change password fails

  // 1: success, 2: fail, 3: loading
  changePasswordStatus: number = 0;
  changePasswordMessage: string = '';
  changePasswordShowToast: boolean = false;

  constructor(
    private location: Location,
    private router: Router,
    private changePasswordService: ChangepasswordService
  ) {}

  handleShowToast(status: number, message: string) {
    this.changePasswordShowToast = true;
    this.changePasswordStatus = status;
    this.changePasswordMessage = message;
    setTimeout(() => {
      this.changePasswordShowToast = false;
    }, 3000);
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      //changePasswordData = { password, newPassword, reNewPassword }
      const changePasswordData = form.value;

      if (
        changePasswordData.confirmPassword !== changePasswordData.newPassword
      ) {
        this.isPasswordMatch = false;
      } else {
        this.isPasswordMatch = true;
      }
      if (this.isPasswordMatch) {
        this.changePasswordService
          .changePassword(changePasswordData)
          .subscribe({
            next: (response) => {
              // form.reset();
              this.handleShowToast(1, 'Đổi mật khẩu thành công!');
            },
            error: (error) => {
              this.changePasswordFailMessage = error.error.error;
            },
          });
      }
    }
  }

  goBack() {
    this.location.back();
  }
}
