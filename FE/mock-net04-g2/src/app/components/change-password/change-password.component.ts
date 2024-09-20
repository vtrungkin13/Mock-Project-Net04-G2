import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ChangepasswordService } from '../../services/changepassword-service/changepassword.service';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss',
})
export class ChangePasswordComponent {
  isPasswordMatch: boolean = false; //check password and re-password is match?
  changePasswordFailMessage: string = ''; // Store error message if change password fails

  constructor(private location: Location, private router: Router,private changePasswordService: ChangepasswordService) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      //changePasswordData = { password, newPassword, reNewPassword }
      const changePasswordData = form.value;

      if (changePasswordData.confirmPassword !== changePasswordData.newPassword) {
        this.isPasswordMatch = false;
      } else {
        this.isPasswordMatch = true;
      }
      if (this.isPasswordMatch) {
        this.changePasswordService.changePassword(changePasswordData).subscribe({
          next: (response) => {
            this.router.navigateByUrl('/');
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
