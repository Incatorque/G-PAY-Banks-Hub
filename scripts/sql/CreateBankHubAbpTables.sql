-- Bank Hub ABP schema (App* tables). Idempotent.
IF OBJECT_ID(N'[AppBankHubAvsBatches]', N'U') IS NULL
BEGIN
  CREATE TABLE [AppBankHubAvsBatches] (
    [Id] uniqueidentifier NOT NULL PRIMARY KEY,
    [FileName] nvarchar(256) NOT NULL,
    [TotalRows] int NOT NULL,
    [ProcessedCount] int NOT NULL,
    [VerifiedCount] int NOT NULL,
    [FailedCount] int NOT NULL,
    [PendingCount] int NOT NULL,
    [SegmentCount] int NOT NULL,
    [Status] nvarchar(32) NOT NULL,
    [BankCode] nvarchar(32) NULL,
    [ExternalBatchId] uniqueidentifier NULL,
    [ApiClientId] uniqueidentifier NULL,
    [ExtraProperties] nvarchar(max) NOT NULL,
    [ConcurrencyStamp] nvarchar(40) NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierId] uniqueidentifier NULL
  );
END
GO
IF OBJECT_ID(N'[AppBankHubAvsRecords]', N'U') IS NULL
BEGIN
  CREATE TABLE [AppBankHubAvsRecords] (
    [Id] uniqueidentifier NOT NULL PRIMARY KEY,
    [BatchId] uniqueidentifier NULL,
    [ApiClientId] uniqueidentifier NULL,
    [RowNumber] int NULL,
    [BankCode] nvarchar(32) NOT NULL,
    [Reference] nvarchar(max) NULL,
    [CorrelationId] nvarchar(max) NULL,
    [AccountNumber] nvarchar(32) NOT NULL,
    [BranchCode] nvarchar(16) NOT NULL,
    [IdentityNumber] nvarchar(max) NULL,
    [AccountHolderName] nvarchar(max) NULL,
    [IsVerified] bit NOT NULL,
    [ResultCode] nvarchar(max) NULL,
    [ResultDescription] nvarchar(max) NULL,
    [BankResultCode] nvarchar(max) NULL,
    [BankReference] nvarchar(max) NULL,
    [AccountFound] bit NULL,
    [AccountOpen] bit NULL,
    [AccountActive] bit NULL,
    [IdentityMatch] nvarchar(max) NULL,
    [NameMatch] nvarchar(max) NULL,
    [InitialsMatch] nvarchar(max) NULL,
    [EmailMatch] nvarchar(max) NULL,
    [PhoneMatch] nvarchar(max) NULL,
    [AccountTypeMatch] nvarchar(max) NULL,
    [AccountOpenLongerThan3Months] bit NULL,
    [AllowsCredit] bit NULL,
    [AcceptsCredit] bit NULL,
    [AllowsDebit] bit NULL,
    [AcceptsDebit] bit NULL,
    [SuccessRate] decimal(5,2) NOT NULL,
    [MatchingCriteriaJson] nvarchar(max) NULL,
    [RequestHeadersJson] nvarchar(max) NULL,
    [RequestBodyJson] nvarchar(max) NULL,
    [ResponseHeadersJson] nvarchar(max) NULL,
    [ResponseBodyJson] nvarchar(max) NULL,
    [ErrorMessage] nvarchar(max) NULL,
    [Status] nvarchar(32) NULL,
    [ExtraProperties] nvarchar(max) NOT NULL,
    [ConcurrencyStamp] nvarchar(40) NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierId] uniqueidentifier NULL
  );
END
GO
IF OBJECT_ID(N'[AppBankHubPaymentRecords]', N'U') IS NULL
BEGIN
  CREATE TABLE [AppBankHubPaymentRecords] (
    [Id] uniqueidentifier NOT NULL PRIMARY KEY,
    [BankCode] nvarchar(32) NOT NULL,
    [CorrelationId] nvarchar(max) NULL,
    [Reference] nvarchar(max) NULL,
    [TransactionReference] nvarchar(max) NULL,
    [ApiReference] nvarchar(max) NULL,
    [Amount] decimal(18,2) NOT NULL,
    [Currency] nvarchar(8) NOT NULL,
    [PaymentRail] nvarchar(16) NOT NULL,
    [Status] nvarchar(32) NOT NULL,
    [BankStatusCode] nvarchar(max) NULL,
    [FromAccountNumber] nvarchar(32) NOT NULL,
    [ToAccountNumber] nvarchar(32) NOT NULL,
    [ToBranchCode] nvarchar(max) NULL,
    [BeneficiaryName] nvarchar(max) NULL,
    [ResultDescription] nvarchar(max) NULL,
    [RequestJson] nvarchar(max) NULL,
    [ResponseJson] nvarchar(max) NULL,
    [ErrorMessage] nvarchar(max) NULL,
    [LastStatusCheckTime] datetime2 NULL,
    [ExtraProperties] nvarchar(max) NOT NULL,
    [ConcurrencyStamp] nvarchar(40) NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierId] uniqueidentifier NULL
  );
END
GO
IF COL_LENGTH(N'AppBankHubPaymentRecords', N'LastStatusCheckTime') IS NULL
  ALTER TABLE [AppBankHubPaymentRecords] ADD [LastStatusCheckTime] datetime2 NULL;
GO
IF OBJECT_ID(N'[AppBankHubApiCalls]', N'U') IS NULL
BEGIN
  CREATE TABLE [AppBankHubApiCalls] (
    [Id] uniqueidentifier NOT NULL PRIMARY KEY,
    [BankCode] nvarchar(32) NOT NULL,
    [Direction] nvarchar(16) NOT NULL,
    [Operation] nvarchar(64) NOT NULL,
    [HttpMethod] nvarchar(16) NULL,
    [Path] nvarchar(max) NULL,
    [HttpStatus] int NULL,
    [CorrelationId] nvarchar(max) NULL,
    [RequestJson] nvarchar(max) NULL,
    [ResponseJson] nvarchar(max) NULL,
    [DurationMs] bigint NULL,
    [Success] bit NOT NULL,
    [ErrorCode] nvarchar(max) NULL,
    [ExtraProperties] nvarchar(max) NOT NULL,
    [ConcurrencyStamp] nvarchar(40) NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL
  );
END
GO
