import { RoutesService, eLayoutType } from '@abp/ng.core';
import { inject, provideAppInitializer } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
    {
      path: '/',
      name: '::Menu:Home',
      iconClass: 'fas fa-home',
      order: 1,
      layout: eLayoutType.application,
    },
    {
      path: '/directors',
      name: '::Menu:Directors',
      iconClass: 'fas fa-film',
      order: 2,
      layout: eLayoutType.application,
    },
    {
      path: '/movies',
      name: '::Menu:Movies',
      iconClass: 'fas fa-film',
      order: 3,
      layout: eLayoutType.application,
    },
    {
      path: '/customers',
      name: '::Menu:Customers',
      iconClass: 'fas fa-users',
      order: 4,
      layout: eLayoutType.application,
    },
    {
      path: '/rentals',
      name: '::Menu:Rentals',
      iconClass: 'fas fa-ticket-alt',
      order: 5,
      layout: eLayoutType.application,
    },
  ]);
}
