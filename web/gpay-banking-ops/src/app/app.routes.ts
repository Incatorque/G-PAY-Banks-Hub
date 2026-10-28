import { Routes } from '@angular/router';
import { HealthComponent } from './pages/health.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'health' },
  { path: 'health', component: HealthComponent },
  { path: '**', redirectTo: 'health' }
];
