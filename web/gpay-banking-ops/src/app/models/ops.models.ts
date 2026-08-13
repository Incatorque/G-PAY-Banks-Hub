export interface QueueDepthInfo {
  queueName: string;
  messageCount: number;
  consumerCount: number;
  bank?: string | null;
}

export interface QueueHealthInfo {
  queueName: string;
  messageCount: number;
  consumerCount: number;
  isClogged: boolean;
}

export interface ServiceHealthInfo {
  serviceName: string;
  bankCode?: string | null;
  status: 'Healthy' | 'Unhealthy' | 'Critical' | number;
  isResponding: boolean;
  detail?: string | null;
  queues: QueueHealthInfo[];
}

export interface PlatformHealthResponse {
  overallStatus: 'Healthy' | 'Unhealthy' | 'Critical' | number;
  checkedAtUtc: string;
  services: ServiceHealthInfo[];
}
