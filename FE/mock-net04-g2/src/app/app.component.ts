import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/shared/header/header.component';
import { AuthService } from './services/auth-service/auth.service';
import { RoleEnum } from './models/enum/RoleEnum';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  userRole?: RoleEnum;

  constructor(private router: Router, private authService: AuthService) {
    // this.router.events.subscribe((event) => {
    //   if (event instanceof NavigationEnd) {
    //     const url = event.url;
    //     if (
    //       url !== '/login' &&
    //       url !== '/register' &&
    //       url !== '/reset-password'
    //     ) {
    //       this.showHeader = true;
    //     } else {
    //       this.showHeader = false;
    //     }
    //   }
    // });
  }
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
    // if (newUrl === '/') {
    const user = this.authService.getUser();
    if (user) {
      this.userRole = user.role;
    } else {
      this.userRole = undefined;
    }
    // }
  }
}
