import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { User } from '../../../models/User';
import { UserService } from '../../../services/user-service/user.service';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss',
})
export class UserProfileComponent implements OnInit {
  @Input() user!: User;
  @Output() onUserUpdate = new EventEmitter();

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
          this.onUserUpdate.emit();
        },
        error: (error) => {
          console.log(error);
        },
      });
    }
  }
}
