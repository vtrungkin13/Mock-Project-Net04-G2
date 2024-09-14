import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./components/login/login.component').then(
        (module) => module.LoginComponent
      ),
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./components/register/register.component').then(
        (module) => module.RegisterComponent
      ),
  },
  {
    path: 'reset-password',
    loadComponent: () =>
      import('./components/reset-password/reset-password.component').then(
        (module) => module.ResetPasswordComponent
      ),
  },
  {
    path: 'change-password',
    loadComponent: () =>
      import('./components/change-password/change-password.component').then(
        (module) => module.ChangePasswordComponent
      ),
  },
];
