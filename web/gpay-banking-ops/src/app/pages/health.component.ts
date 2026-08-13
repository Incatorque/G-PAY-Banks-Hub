import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe, NgClass } from '@angular/common';
import { OpsApiService } from '../services/ops-api.service';
import { PlatformHealthResponse } from '../models/ops.models';

@Component({
  selector: 'app-health',
  standalone: true,
  imports: [DatePipe, NgClass],
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
    this.api.getHealth().subscribe({
      next: (data) => {
        this.health.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set(err?.message ?? 'Failed to load health.');
        this.loading.set(false);
      }
    });
  }

  statusLabel(status: string | number): string {
    if (typeof status === 'number') {
      return ['Healthy', 'Unhealthy', 'Critical'][status] ?? String(status);
    }
    return status;
  }
}
