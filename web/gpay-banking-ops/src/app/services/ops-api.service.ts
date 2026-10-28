import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { PlatformHealthResponse } from '../models/ops.models';

@Injectable({ providedIn: 'root' })
export class OpsApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getHealth(): Observable<PlatformHealthResponse> {
    return this.http.get<PlatformHealthResponse>(`${this.baseUrl}/health`);
  }

}
