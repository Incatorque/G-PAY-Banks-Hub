import { DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';
import { BankApiCall, BankApiCallListItem, BankApiCallPage } from '../models/ops.models';
import { OpsApiService } from '../services/ops-api.service';

@Component({
  selector: 'app-api-activity',
  standalone: true,
  imports: [DatePipe, FormsModule],
  templateUrl: './api-activity.component.html'
})
export class ApiActivityComponent {
  private readonly api = inject(OpsApiService);
  readonly page = signal<BankApiCallPage | null>(null);
  readonly selectedCall = signal<BankApiCall | null>(null);
  readonly loading = signal(false);
  readonly detailLoading = signal(false);
  readonly error = signal('');
  readonly detailError = signal('');
  readonly token = signal(this.api.getAccessToken());
  readonly accessExpanded = signal(false);
  readonly bank = signal('');
  readonly direction = signal('');
  readonly outcome = signal('');
  readonly operation = signal('');
  readonly search = signal('');
  readonly from = signal('');
  readonly to = signal('');
  readonly skip = signal(0);
  readonly take = 50;

  constructor() {
    this.refresh();
  }

  refresh(): void {
    this.loading.set(true);
    this.error.set('');
    this.api.getCalls({
      bank: this.bank() || undefined,
      direction: this.direction() || undefined,
      operation: this.operation() || undefined,
      success: this.outcome() ? this.outcome() === 'success' : undefined,
      search: this.search().trim() || undefined,
      from: this.from() ? new Date(this.from()).toISOString() : undefined,
      to: this.to() ? new Date(new Date(this.to()).getTime() + 86_400_000).toISOString() : undefined,
      skip: this.skip(),
      take: this.take
    }).pipe(finalize(() => this.loading.set(false))).subscribe({
      next: result => this.page.set(result),
      error: err => {
        this.page.set(null);
        this.error.set(this.errorMessage(err, 'Unable to load API activity.'));
      }
    });
  }

  applyFilters(): void {
    this.skip.set(0);
    this.refresh();
  }

  clearFilters(): void {
    this.bank.set('');
    this.direction.set('');
    this.outcome.set('');
    this.operation.set('');
    this.search.set('');
    this.from.set('');
    this.to.set('');
    this.skip.set(0);
    this.refresh();
  }

  saveToken(): void {
    this.api.setAccessToken(this.token());
    this.refresh();
  }

  toggleAccess(): void {
    this.accessExpanded.update(open => !open);
  }

  openCall(call: BankApiCallListItem): void {
    this.selectedCall.set(null);
    this.detailError.set('');
    this.detailLoading.set(true);
    this.api.getCall(call.id).pipe(finalize(() => this.detailLoading.set(false))).subscribe({
      next: detail => this.selectedCall.set(detail),
      error: err => this.detailError.set(this.errorMessage(err, 'Unable to load the call details.'))
    });
  }

  closeCall(): void {
    this.selectedCall.set(null);
    this.detailError.set('');
  }

  setBank(bank: string): void {
    this.bank.set(bank);
    this.applyFilters();
  }

  previousPage(): void {
    this.skip.set(Math.max(0, this.skip() - this.take));
    this.refresh();
  }

  nextPage(): void {
    if (this.page() && this.skip() + this.take < this.page()!.totalCount) {
      this.skip.update(value => value + this.take);
      this.refresh();
    }
  }

  payload(value: string | null): string {
    if (!value) return 'No payload captured';
    try { return JSON.stringify(JSON.parse(value), null, 2); }
    catch { return value; }
  }

  private errorMessage(err: any, fallback: string): string {
    if (err?.status === 401 || err?.status === 403) return 'Access denied. Connect with a valid Bank Hub access token to continue.';
    return err?.error?.error?.message ?? err?.error?.message ?? err?.message ?? fallback;
  }
}
