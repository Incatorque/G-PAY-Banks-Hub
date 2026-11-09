import { DatePipe, DecimalPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';
import { BankPaymentQueuePage } from '../models/ops.models';
import { OpsApiService } from '../services/ops-api.service';

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [DatePipe, DecimalPipe, FormsModule],
  templateUrl: './transactions.component.html'
})
export class TransactionsComponent {
  private readonly api = inject(OpsApiService);
  readonly transactions = signal<BankPaymentQueuePage | null>(null);
  readonly bank = signal('');
  readonly status = signal('');
  readonly search = signal('');
  readonly loading = signal(false);
  readonly error = signal('');
  readonly token = signal(this.api.getAccessToken());
  readonly accessExpanded = signal(false);
  private readonly pageSize = 50;

  constructor() {
    this.refresh();
  }

  refresh(): void {
    this.loading.set(true);
    this.error.set('');
    this.api.getTransactions({
      bank: this.bank() || undefined,
      status: this.status() || undefined,
      search: this.search().trim() || undefined,
      skip: 0,
      take: this.pageSize
    }).pipe(finalize(() => this.loading.set(false))).subscribe({
      next: page => this.transactions.set(page),
      error: err => {
        this.transactions.set(null);
        this.error.set(err?.error?.error?.message ?? err?.error?.message ?? err?.message ?? 'Unable to load queued and submitted transactions.');
      }
    });
  }

  loadMore(): void {
    const current = this.transactions();
    if (!current || this.loading() || current.items.length >= current.totalCount) return;
    this.loading.set(true);
    this.error.set('');
    this.api.getTransactions({
      bank: this.bank() || undefined,
      status: this.status() || undefined,
      search: this.search().trim() || undefined,
      skip: current.items.length,
      take: this.pageSize
    }).pipe(finalize(() => this.loading.set(false))).subscribe({
      next: nextPage => this.transactions.set({
        totalCount: nextPage.totalCount,
        banks: nextPage.banks,
        items: [...current.items, ...nextPage.items]
      }),
      error: err => this.error.set(err?.error?.error?.message ?? err?.error?.message ?? err?.message ?? 'Unable to load more transactions.')
    });
  }

  setBank(bank: string): void {
    this.bank.set(bank);
    this.refresh();
  }

  setStatus(status: string): void {
    this.status.set(status);
    this.refresh();
  }

  saveToken(): void {
    this.api.setAccessToken(this.token());
    this.refresh();
  }

  toggleAccess(): void {
    this.accessExpanded.update(open => !open);
  }
}
