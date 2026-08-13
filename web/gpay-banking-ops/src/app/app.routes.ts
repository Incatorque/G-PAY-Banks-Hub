import { Routes } from '@angular/router';
import { HealthComponent } from './pages/health.component';
import { QueuesComponent } from './pages/queues.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'health' },
  { path: 'health', component: HealthComponent },
  { path: 'queues', component: QueuesComponent },
  { path: '**', redirectTo: 'health' }
];
