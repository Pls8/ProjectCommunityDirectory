import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home';
import { LoginComponent } from './pages/login/login';
import { RegisterComponent } from './pages/register/register';
import { ResourcesComponent } from './pages/resources/resources';
import { EventsComponent } from './pages/events/events';
import { AdminComponent } from './pages/admin/admin';

import { authGuard } from './guards/auth-guard'; 
import { adminGuard } from './guards/admin-guard';
import { ResourceCreateComponent } from './pages/resource-create/resource-create';
import { EventCreateComponent } from './pages/event-create/event-create';

export const routes: Routes = [
{ path: '', component: HomeComponent },

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: 'resources', component: ResourcesComponent },
  { path: 'events', component: EventsComponent },

  { path: 'resources/new', component: ResourceCreateComponent, canActivate: [authGuard] },
  { path: 'events/new', component: EventCreateComponent, canActivate: [adminGuard] },

  { path: 'admin', component: AdminComponent, canActivate: [adminGuard] },

  { path: '**', redirectTo: '' }
];
