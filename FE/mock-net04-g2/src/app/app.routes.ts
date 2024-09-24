import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./components/home/home.component').then(
        (module) => module.HomeComponent
      ),
  },
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
  {
    path: 'campaign-detail/:campaignId',
    loadComponent: () =>
      import(
        './components/campaigns/campaign-detail/campaign-detail.component'
      ).then((module) => module.CampaignDetailComponent),
  },
  {
    path: 'users-list',
    loadComponent: () =>
      import('./components/users/users-list/users-list.component').then(
        (module) => module.UsersListComponent
      ),
  },
  {
    path: 'campaign-chart/:campaignId',
    loadComponent: () =>
      import('./components/campaigns/campaign-chart/campaign-chart.component').then(
        (module) => module.CampaignChartComponent
      ),
  },
  {
    path: 'donation-history',
    loadComponent: () =>
      import('./components/donate-list/donate-list.component').then(
        (module) => module.DonateListComponent
      ),
  }
];
