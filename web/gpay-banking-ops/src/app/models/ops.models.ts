export interface PlatformHealthResponse {
  status: 'Healthy' | 'Unhealthy';
  checkedAtUtc: string;
}
