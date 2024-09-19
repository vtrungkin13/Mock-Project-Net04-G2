import { Component, Input } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services/auth-service/auth.service';
import { User } from '../../../models/User';
import { UserProfileComponent } from "../../users/user-profile/user-profile.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, UserProfileComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  @Input() user?: User;

  constructor(private router: Router, private authService: AuthService) {}

  logout() {
    this.router.navigateByUrl('/login');
    this.authService.clearSession();
  }
}
