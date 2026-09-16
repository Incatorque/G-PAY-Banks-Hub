-- GPay Banking Orchestrator — payment operation tables
-- Run against the Banking connection string database.
-- Persistence wiring optional; ABP may own its own tables.

IF OBJECT_ID(N'dbo.GpayPaymentOperations', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GpayPaymentOperations
    (
        Id              UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_GpayPaymentOperations PRIMARY KEY,
        CorrelationId   NVARCHAR(64)     NOT NULL,
        BankCode        NVARCHAR(32)     NOT NULL,
        TransactionRef  NVARCHAR(128)    NULL,
        ApiRef          NVARCHAR(128)    NULL,
        Status          NVARCHAR(32)     NOT NULL,
        Amount          DECIMAL(18, 2)   NULL,
        Currency        NVARCHAR(8)      NULL,
        Rail            NVARCHAR(16)     NULL,
        RequestJson     NVARCHAR(MAX)    NULL,
        ResponseJson    NVARCHAR(MAX)    NULL,
        CreatedAtUtc    DATETIMEOFFSET   NOT NULL,
        UpdatedAtUtc    DATETIMEOFFSET   NULL
    );

    CREATE INDEX IX_GpayPaymentOperations_CorrelationId ON dbo.GpayPaymentOperations (CorrelationId);
    CREATE INDEX IX_GpayPaymentOperations_TransactionRef ON dbo.GpayPaymentOperations (TransactionRef);
    CREATE INDEX IX_GpayPaymentOperations_ApiRef ON dbo.GpayPaymentOperations (ApiRef);
    CREATE INDEX IX_GpayPaymentOperations_CreatedAtUtc ON dbo.GpayPaymentOperations (CreatedAtUtc);
END
GO
