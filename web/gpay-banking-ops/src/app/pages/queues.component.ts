import { Component, OnInit, inject, signal } from '@angular/core';
import { OpsApiService } from '../services/ops-api.service';
import { QueueDepthInfo } from '../models/ops.models';

@Component({
  selector: 'app-queues',
  standalone: true,
  templateUrl: './queues.component.html',
  styleUrl: './queues.component.css'
})
export class QueuesComponent implements OnInit {
  private readonly api = inject(OpsApiService);

  readonly queues = signal<QueueDepthInfo[]>([]);
  readonly error = signal<string | null>(null);
  readonly loading = signal(false);

  ngOnInit(): void {
    this.refresh();
  }

  refresh(): void {
    this.loading.set(true);
    this.error.set(null);
    this.api.getQueues().subscribe({
      next: (data) => {
        this.queues.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set(err?.message ?? 'Failed to load queues.');
        this.loading.set(false);
      }
    });
  }
}
