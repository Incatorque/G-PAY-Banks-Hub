import { Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard.component';
import { ApiActivityComponent } from './pages/api-activity.component';
import { HealthComponent } from './pages/health.component';
import { TransactionsComponent } from './pages/transactions.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', component: DashboardComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'api-activity', component: ApiActivityComponent },
  { path: 'transactions', component: TransactionsComponent },
  { path: 'health', component: HealthComponent },
  { path: '**', redirectTo: '' }
];
