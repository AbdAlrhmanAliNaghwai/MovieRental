import { authGuard, permissionGuard } from '@abp/ng.core';
import { Routes } from '@angular/router';

export const APP_ROUTES: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadComponent: () => import('./home/home.component').then(c => c.HomeComponent),
  },
  {
    path: 'directors',
    loadComponent: () => import('./director/director.component').then(c => c.DirectorComponent),
  },
  {
    path: 'movies',
    loadComponent: () => import('./movie/movie.component').then(c => c.MovieComponent),
  },
  {
    path: 'customers',
    loadComponent: () => import('./customer/customer.component').then(c => c.CustomerComponent),
  },
  {
    path: 'rentals',
    loadComponent: () => import('./rental/rental.component').then(c => c.RentalComponent),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(c => c.createRoutes()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(c => c.createRoutes()),
  },
  {
    path: 'tenant-management',
    loadChildren: () => import('@abp/ng.tenant-management').then(c => c.createRoutes()),
  },
  {
    path: 'setting-management',
    loadChildren: () => import('@abp/ng.setting-management').then(c => c.createRoutes()),
  },
];
