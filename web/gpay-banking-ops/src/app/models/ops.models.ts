export interface PlatformHealthResponse {
  status: 'Healthy' | 'Unhealthy';
  checkedAtUtc: string;
}

export interface BankApiCallQuery {
  bank?: string;
  direction?: string;
  operation?: string;
  success?: boolean;
  search?: string;
  from?: string;
  to?: string;
  skip: number;
  take: number;
}

export interface BankApiCallListItem {
  id: string;
  creationTime: string;
  bankCode: string;
  direction: string;
  operation: string;
  httpMethod: string | null;
  path: string | null;
  httpStatus: number | null;
  correlationId: string | null;
  gpayOrderId: string | null;
  durationMs: number | null;
  success: boolean;
  errorCode: string | null;
}

export interface BankApiCall extends BankApiCallListItem {
  requestJson: string | null;
  responseJson: string | null;
}

export interface BankApiCallPage {
  totalCount: number;
  pageCount: number;
  failedCount: number;
  successRate: number;
  averageDurationMs: number | null;
  minDurationMs: number | null;
  maxDurationMs: number | null;
  outboundCount: number;
  inboundCount: number;
  uniqueOrderCount: number;
  items: BankApiCallListItem[];
  banks: string[];
  operations: string[];
  dailyActivity: BankApiCallTrend[];
  bankBreakdown: BankApiCallBreakdown[];
  statusBreakdown: BankApiCallBreakdown[];
  operationBreakdown: BankApiCallBreakdown[];
  directionBreakdown: BankApiCallBreakdown[];
  failureBreakdown: BankApiCallBreakdown[];
}

export interface BankApiCallTrend {
  day: string;
  total: number;
  failed: number;
  averageDurationMs: number | null;
}

export interface BankApiCallBreakdown {
  label: string;
  total: number;
  failed: number;
  successRate: number;
  averageDurationMs: number | null;
}

export interface BankPaymentQueueItem {
  orderId: string;
  orderNumber: string;
  reference: string;
  bank: string;
  route: string;
  rail: string;
  status: string;
  orderDate: string;
  amount: number;
  currency: string;
}

export interface BankPaymentQueuePage {
  totalCount: number;
  banks: string[];
  items: BankPaymentQueueItem[];
}
