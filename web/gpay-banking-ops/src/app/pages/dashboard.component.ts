import { DatePipe, DecimalPipe, PercentPipe } from '@angular/common';
import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { gsap } from 'gsap';
import { OpsApiService } from '../services/ops-api.service';
import { BankApiCall, BankApiCallListItem, BankApiCallPage, PlatformHealthResponse } from '../models/ops.models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [DatePipe, DecimalPipe, PercentPipe, FormsModule, RouterLink],
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit, AfterViewInit, OnDestroy {
  private readonly api = inject(OpsApiService);
  @ViewChild('dashboardRoot') private dashboardRoot?: ElementRef<HTMLElement>;
  private motionContext?: gsap.Context;
  private readonly reduceMotion = typeof window !== 'undefined' && window.matchMedia('(prefers-reduced-motion: reduce)').matches;

  readonly page = signal<BankApiCallPage | null>(null);
  readonly health = signal<PlatformHealthResponse | null>(null);
  readonly selectedCall = signal<BankApiCall | null>(null);
  readonly loading = signal(false);
  readonly detailLoading = signal(false);
  readonly error = signal('');
  readonly detailError = signal('');
  readonly token = signal(this.api.getAccessToken());
  readonly bank = signal('');
  readonly direction = signal('');
  readonly operation = signal('');
  readonly outcome = signal('');
  readonly search = signal('');
  readonly from = signal('');
  readonly to = signal('');
  readonly take = 50;
  readonly filtersExpanded = signal(false);
  readonly timeRange = signal(7);

  readonly featuredCall = computed(() => {
    const calls = this.page()?.items ?? [];
    return calls.find(call => !call.success) ?? calls[0] ?? null;
  });

  readonly dailyChart = computed(() => {
    const data = new Map((this.page()?.dailyActivity ?? []).map(day => [day.day.slice(0, 10), day]));
    const today = new Date();
    const first = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 13);
    const points = Array.from({ length: 14 }, (_, index) => {
      const date = new Date(first.getFullYear(), first.getMonth(), first.getDate() + index);
      const key = `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}-${String(date.getDate()).padStart(2, '0')}`;
      const point = data.get(key);
      return {
        key,
        label: date.toLocaleDateString([], { month: 'short', day: 'numeric' }),
        total: point?.total ?? 0,
        failed: point?.failed ?? 0
      };
    });
    const max = Math.max(1, ...points.map(point => point.total));
    return points.slice(-this.timeRange()).map(point => ({
      ...point,
      successHeight: point.total ? Math.max(2, 100 * (point.total - point.failed) / max) : 0,
      failureHeight: point.failed ? Math.max(3, 100 * point.failed / max) : 0,
      totalHeight: point.total ? Math.max(4, 100 * point.total / max) : 3
    }));
  });

  readonly dailyPeak = computed(() => Math.max(1, ...(this.page()?.dailyActivity ?? []).map(point => point.total)));
  readonly operationPeak = computed(() => Math.max(1, ...(this.page()?.operationBreakdown ?? []).map(item => item.total)));
  readonly failurePeak = computed(() => Math.max(1, ...(this.page()?.failureBreakdown ?? []).map(item => item.total)));

  readonly outcomesStyle = computed(() => {
    const rate = this.page()?.successRate ?? 0;
    return `conic-gradient(#10a447 0 ${rate}%, #18181b ${rate}% 100%)`;
  });

  readonly hasFilters = computed(() => Boolean(
    this.bank() || this.direction() || this.operation() || this.outcome() || this.search() || this.from() || this.to()
  ));

  ngOnInit(): void {
    this.refresh();
  }

  ngAfterViewInit(): void {
    if (this.reduceMotion || !this.dashboardRoot) return;

    this.motionContext = gsap.context(() => {
      gsap.timeline({ defaults: { ease: 'power3.out' } })
        .from('.page-heading', { y: 14, autoAlpha: 0, duration: 0.48 })
        .from('.control-bar', { y: 12, autoAlpha: 0, duration: 0.4 }, '-=0.24')
        .from('.kpi-card', { y: 15, autoAlpha: 0, scale: 0.985, duration: 0.42, stagger: 0.07 }, '-=0.19')
        .from('.chart-row .card, .secondary-column .card', { y: 12, autoAlpha: 0, duration: 0.38, stagger: 0.045 }, '-=0.2')
        .from('.operations-card', { y: 10, autoAlpha: 0, duration: 0.38 }, '-=0.16');

      gsap.fromTo('.traffic-total, .traffic-failed',
        { scaleY: 0, transformOrigin: 'bottom center' },
        { scaleY: 1, duration: 0.65, stagger: 0.018, ease: 'back.out(1.25)', delay: 0.28 });
    }, this.dashboardRoot.nativeElement);
  }

  ngOnDestroy(): void {
    this.motionContext?.revert();
  }

  refresh(): void {
    this.api.getHealth().subscribe({
      next: value => this.health.set(value),
      error: () => this.health.set({ status: 'Unhealthy', checkedAtUtc: new Date().toISOString() })
    });
    this.loading.set(true);
    this.error.set('');
    const outcome = this.outcome();
    this.api.getCalls({
      bank: this.bank() || undefined,
      direction: this.direction() || undefined,
      operation: this.operation() || undefined,
      success: outcome ? outcome === 'success' : undefined,
      search: this.search().trim() || undefined,
      from: this.from() ? new Date(this.from()).toISOString() : undefined,
      to: this.to() ? new Date(new Date(this.to()).getTime() + 86_400_000).toISOString() : undefined,
      skip: 0,
      take: this.take
    }).pipe(finalize(() => this.loading.set(false))).subscribe({
      next: result => {
        this.page.set(result);
        this.animateLiveUpdate();
      },
      error: err => {
        this.page.set(null);
        this.error.set(this.errorMessage(err, 'Unable to load Bank Hub activity.'));
      }
    });
  }

  saveToken(): void {
    this.api.setAccessToken(this.token());
    this.refresh();
  }

  applyFilters(): void {
    this.refresh();
  }

  clearFilters(): void {
    this.bank.set('');
    this.direction.set('');
    this.operation.set('');
    this.outcome.set('');
    this.search.set('');
    this.from.set('');
    this.to.set('');
    this.refresh();
  }

  openCall(call: BankApiCallListItem): void {
    this.selectedCall.set(null);
    this.detailError.set('');
    this.detailLoading.set(true);
    this.api.getCall(call.id).pipe(finalize(() => this.detailLoading.set(false))).subscribe({
      next: detail => {
        this.selectedCall.set(detail);
        this.animateDrawer();
      },
      error: err => this.detailError.set(this.errorMessage(err, 'Unable to load the call details.'))
    });
  }

  closeCall(): void {
    this.selectedCall.set(null);
    this.detailError.set('');
  }

  filterByOperation(operation: string): void {
    this.operation.set(operation);
    this.applyFilters();
  }

  filterByFailure(code: string): void {
    this.outcome.set('failure');
    this.search.set(code === 'Unspecified' ? '' : code);
    this.applyFilters();
  }

  filterByBank(bank: string): void {
    this.bank.set(bank);
    this.applyFilters();
  }

  toggleFilters(): void {
    this.filtersExpanded.update(value => !value);
  }

  payload(value: string | null): string {
    if (!value) return 'No payload captured';
    try {
      return JSON.stringify(JSON.parse(value), null, 2);
    } catch {
      return value;
    }
  }

  private errorMessage(err: any, fallback: string): string {
    if (err?.status === 401 || err?.status === 403) {
      return 'Access denied. Connect with a valid Bank Hub access token to continue.';
    }
    return err?.error?.error?.message ?? err?.error?.message ?? err?.message ?? fallback;
  }

  private animateLiveUpdate(): void {
    if (this.reduceMotion || !this.motionContext || !this.dashboardRoot) return;
    requestAnimationFrame(() => this.motionContext?.add(() => {
      gsap.fromTo('.kpi-card',
        { y: 5, autoAlpha: 0.82 },
        { y: 0, autoAlpha: 1, duration: 0.36, stagger: 0.035, ease: 'power2.out', overwrite: 'auto' });
      gsap.fromTo('.traffic-total, .traffic-failed',
        { scaleY: 0.72, transformOrigin: 'bottom center' },
        { scaleY: 1, duration: 0.5, stagger: 0.012, ease: 'back.out(1.2)', overwrite: 'auto' });
    }));
  }

  private animateDrawer(): void {
    if (this.reduceMotion || !this.motionContext) return;
    requestAnimationFrame(() => this.motionContext?.add(() => {
      gsap.fromTo('.scrim', { autoAlpha: 0 }, { autoAlpha: 1, duration: 0.2, ease: 'power1.out' });
      gsap.fromTo('.detail-drawer',
        { x: 28, autoAlpha: 0 },
        { x: 0, autoAlpha: 1, duration: 0.38, ease: 'power3.out' });
    }));
  }
}
