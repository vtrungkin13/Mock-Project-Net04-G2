import { Component, Input } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { RoleEnum } from '../../../models/enum/RoleEnum';
import { AuthService } from '../../../services/auth-service/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  @Input() userRole?: RoleEnum;

  constructor(private router: Router, private authService: AuthService) {}

  logout() {
    this.router.navigateByUrl('/login');
    this.authService.clearSession();
  }
}
