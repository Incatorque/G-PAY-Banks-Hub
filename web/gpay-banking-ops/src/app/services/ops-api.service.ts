import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpParams, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';
import { BankApiCall, BankApiCallPage, BankApiCallQuery, BankPaymentQueuePage, PlatformHealthResponse } from '../models/ops.models';

@Injectable({ providedIn: 'root' })
export class OpsApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;
  private readonly accessTokenKey = 'gpay-banking-ops.access-token';

  getAccessToken(): string {
    return sessionStorage.getItem(this.accessTokenKey) ?? '';
  }

  setAccessToken(token: string): void {
    const normalized = token.trim().replace(/^Bearer\s+/i, '');
    if (normalized) {
      sessionStorage.setItem(this.accessTokenKey, normalized);
    } else {
      sessionStorage.removeItem(this.accessTokenKey);
    }
  }

  getHealth(): Observable<PlatformHealthResponse> {
    return this.http.get<PlatformHealthResponse>(`${this.baseUrl}/health`, { headers: this.headers() });
  }

  getCalls(query: BankApiCallQuery): Observable<BankApiCallPage> {
    let params = new HttpParams();
    for (const [key, value] of Object.entries(query)) {
      if (value !== undefined && value !== null && value !== '') {
        params = params.set(key, String(value));
      }
    }
    return this.http.get<BankApiCallPage | AbpResult<BankApiCallPage>>(`${this.baseUrl}/api/app/operations-dashboard/calls`, {
      headers: this.headers(),
      params
    }).pipe(map(response => this.unwrap(response)));
  }

  getCall(id: string): Observable<BankApiCall> {
    return this.http.get<BankApiCall | AbpResult<BankApiCall>>(`${this.baseUrl}/api/app/operations-dashboard/${id}/call`, {
      headers: this.headers()
    }).pipe(map(response => this.unwrap(response)));
  }

  getTransactions(query: { bank?: string; status?: string; search?: string; skip: number; take: number }): Observable<BankPaymentQueuePage> {
    let params = new HttpParams();
    for (const [key, value] of Object.entries(query)) {
      if (value !== undefined && value !== null && value !== '') params = params.set(key, String(value));
    }
    return this.http.get<BankPaymentQueuePage | AbpResult<BankPaymentQueuePage>>(
      `${this.baseUrl}/api/app/operations-dashboard/transactions`,
      { headers: this.headers(), params }
    ).pipe(map(response => this.unwrap(response)));
  }

  private headers(): HttpHeaders {
    const token = this.getAccessToken();
    return token ? new HttpHeaders({ Authorization: `Bearer ${token}` }) : new HttpHeaders();
  }

  private unwrap<T>(response: T | AbpResult<T>): T {
    return response && typeof response === 'object' && 'result' in response
      ? (response as AbpResult<T>).result
      : response as T;
  }
}

interface AbpResult<T> {
  result: T;
}
