import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services/auth-service/auth.service';
import { User } from '../../../models/User';
import { UserProfileComponent } from '../../users/user-profile/user-profile.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, UserProfileComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit, OnChanges {
  @Input() user?: User;
  userName?: string;

  @Output() onUserUpdate = new EventEmitter();

  constructor(private router: Router, private authService: AuthService) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['user']) {
      this.setUserName();
    }
  }

  ngOnInit(): void {
    this.setUserName();
  }

  setUserName() {
    if (this.user) {
      this.userName = this.user.name;
    }
  }

  userUpdate() {
    this.onUserUpdate.emit();
  }

  logout() {
    this.router.navigateByUrl('/login');
    this.authService.clearSession();
  }
}
