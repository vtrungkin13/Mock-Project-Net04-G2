import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { User } from '../../../models/User';
import { UserService } from '../../../services/user-service/user.service';
import { ToastComponent } from '../../shared/toast/toast.component';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [FormsModule, ToastComponent],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss',
})
export class UserProfileComponent implements OnInit {
  @Input() user!: User;
  @Output() onUserUpdate = new EventEmitter();

  // 1: success, 2: fail, 3: loading
  updateUserStatus: number = 0;
  updateUserMessage: string = '';
  updateUserShowToast: boolean = false;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.user.dob = new Date(this.user.dob.toString());
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      const newUser = form.value;
      this.userService.updateUser(this.user.id, newUser).subscribe({
        next: (response) => {
          sessionStorage.setItem('user', JSON.stringify(response.body));
          this.handleShowToast(1, 'Cập nhật thông tin thành công!');
          this.onUserUpdate.emit();
        },
        error: (error) => {
          this.handleShowToast(2, error.error.error);
        },
      });
    }
  }

  handleShowToast(status: number, message: string) {
    this.updateUserShowToast = true;
    this.updateUserStatus = status;
    this.updateUserMessage = message;
    setTimeout(() => {
      this.updateUserShowToast = false;
    }, 3000);
  }
}
