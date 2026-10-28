import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { finalize } from 'rxjs';
import { OpsApiService } from '../services/ops-api.service';
import { PlatformHealthResponse } from '../models/ops.models';

@Component({
  selector: 'app-health',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './health.component.html',
  styleUrl: './health.component.css'
})
export class HealthComponent implements OnInit {
  private readonly api = inject(OpsApiService);

  readonly health = signal<PlatformHealthResponse | null>(null);
  readonly error = signal<string | null>(null);
  readonly loading = signal(false);

  ngOnInit(): void {
    this.refresh();
  }

  refresh(): void {
    this.loading.set(true);
    this.error.set(null);
    this.api.getHealth()
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (data) => this.health.set(data),
        error: (err) => this.error.set(err?.message ?? 'Failed to load health.')
      });
  }
}
