import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss',
})
export class ChangePasswordComponent {
  isPasswordCorrect: boolean = true; //check password is correct?
  isPasswordDuplicate: boolean = true; //check new password duplicate with old password?
  isPasswordMatch: boolean = false; //check password and re-password is match?

  constructor(private location: Location) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      //changePasswordData = { password, newPassword, reNewPassword }
      const changePasswordData = form.value;
      this.isPasswordCorrect = false;

      if (changePasswordData.newPassword === changePasswordData.password) {
        this.isPasswordDuplicate = true;
      } else {
        this.isPasswordDuplicate = false;
      }

      if (changePasswordData.reNewPassword !== changePasswordData.newPassword) {
        this.isPasswordMatch = false;
      } else {
        this.isPasswordMatch = true;
      }
    }
  }

  onPasswordInput() {
    this.isPasswordCorrect = true;
  }

  goBack() {
    this.location.back();
  }
}
