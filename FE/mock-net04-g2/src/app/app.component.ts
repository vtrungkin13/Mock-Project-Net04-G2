import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/shared/header/header.component';
import { AuthService } from './services/auth-service/auth.service';
import { filter } from 'rxjs/operators';
import { User } from './models/User';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  user?: User;

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    // Subscribe to NavigationEnd event
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        // Execute your function here
        this.onRouteChange(event.urlAfterRedirects);
      });
  }

  onRouteChange(newUrl: string) {
    // Execute route-specific logic if needed
    this.loadUser();
  }

  loadUser() {
    const user = this.authService.getUser();
    if (user) {
      this.user = user;
    } else {
      this.user = undefined;
    }
  }
}
