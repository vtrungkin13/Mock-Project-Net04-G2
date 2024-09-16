import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/shared/header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'mock-net04-g2';

  showHeader = true;

  constructor(private router: Router) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        const url = event.url;
        if (
          url !== '/login' &&
          url !== '/register' &&
          url !== '/reset-password'
        ) {
          this.showHeader = true;
        } else {
          this.showHeader = false;
        }
      }
    });
  }
}
