-- GPay Banking Orchestrator — AVS batch tables
-- Run against the Banking connection string database.

IF OBJECT_ID(N'dbo.GpayAvsBatches', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GpayAvsBatches
    (
        Id                UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_GpayAvsBatches PRIMARY KEY,
        ExternalBatchId   NVARCHAR(64)     NULL,
        Reference         NVARCHAR(128)    NULL,
        SourceFileName    NVARCHAR(260)    NULL,
        CorrelationId     NVARCHAR(64)     NOT NULL,
        Status            NVARCHAR(32)     NOT NULL,
        TotalItems        INT              NOT NULL,
        ProcessedCount    INT              NOT NULL CONSTRAINT DF_GpayAvsBatches_Processed DEFAULT (0),
        SucceededCount    INT              NOT NULL CONSTRAINT DF_GpayAvsBatches_Succeeded DEFAULT (0),
        FailedCount       INT              NOT NULL CONSTRAINT DF_GpayAvsBatches_Failed DEFAULT (0),
        PendingCount      INT              NOT NULL CONSTRAINT DF_GpayAvsBatches_Pending DEFAULT (0),
        SegmentCount      INT              NOT NULL CONSTRAINT DF_GpayAvsBatches_Segments DEFAULT (0),
        StartedAtUtc      DATETIMEOFFSET   NULL,
        CompletedAtUtc    DATETIMEOFFSET   NULL,
        CreatedAtUtc      DATETIMEOFFSET   NOT NULL,
        UpdatedAtUtc      DATETIMEOFFSET   NULL
    );

    CREATE INDEX IX_GpayAvsBatches_ExternalBatchId ON dbo.GpayAvsBatches (ExternalBatchId);
    CREATE INDEX IX_GpayAvsBatches_CreatedAtUtc ON dbo.GpayAvsBatches (CreatedAtUtc);
END
GO

IF OBJECT_ID(N'dbo.GpayAvsBatchSegments', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GpayAvsBatchSegments
    (
        Id             UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_GpayAvsBatchSegments PRIMARY KEY,
        BatchId        UNIQUEIDENTIFIER NOT NULL,
        BankCode       NVARCHAR(32)     NOT NULL,
        SegmentIndex   INT              NOT NULL,
        ItemCount      INT              NOT NULL,
        ProcessedCount INT              NOT NULL CONSTRAINT DF_GpayAvsBatchSegments_Processed DEFAULT (0),
        Status         NVARCHAR(32)     NOT NULL,
        QueuedAtUtc    DATETIMEOFFSET   NULL,
        StartedAtUtc   DATETIMEOFFSET   NULL,
        CompletedAtUtc DATETIMEOFFSET   NULL,
        CreatedAtUtc   DATETIMEOFFSET   NOT NULL,
        UpdatedAtUtc   DATETIMEOFFSET   NULL,
        CONSTRAINT FK_GpayAvsBatchSegments_Batch FOREIGN KEY (BatchId)
            REFERENCES dbo.GpayAvsBatches (Id) ON DELETE CASCADE
    );

    CREATE UNIQUE INDEX IX_GpayAvsBatchSegments_Batch_Index ON dbo.GpayAvsBatchSegments (BatchId, SegmentIndex);
END
GO

IF OBJECT_ID(N'dbo.GpayAvsBatchItems', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GpayAvsBatchItems
    (
        Id               UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_GpayAvsBatchItems PRIMARY KEY,
        BatchId          UNIQUEIDENTIFIER NOT NULL,
        SegmentId        UNIQUEIDENTIFIER NOT NULL,
        ExternalRecordId NVARCHAR(64)     NULL,
        RowNumber        INT              NOT NULL,
        BankCode         NVARCHAR(32)     NOT NULL,
        Status           NVARCHAR(32)     NOT NULL,
        RequestJson      NVARCHAR(MAX)    NOT NULL,
        ResponseJson     NVARCHAR(MAX)    NULL,
        ErrorCode        NVARCHAR(64)     NULL,
        ErrorMessage     NVARCHAR(1024)   NULL,
        BankReference    NVARCHAR(128)    NULL,
        ProcessedAtUtc   DATETIMEOFFSET   NULL,
        CreatedAtUtc     DATETIMEOFFSET   NOT NULL,
        UpdatedAtUtc     DATETIMEOFFSET   NULL,
        CONSTRAINT FK_GpayAvsBatchItems_Batch FOREIGN KEY (BatchId)
            REFERENCES dbo.GpayAvsBatches (Id) ON DELETE CASCADE,
        CONSTRAINT FK_GpayAvsBatchItems_Segment FOREIGN KEY (SegmentId)
            REFERENCES dbo.GpayAvsBatchSegments (Id)
    );

    CREATE INDEX IX_GpayAvsBatchItems_BatchId ON dbo.GpayAvsBatchItems (BatchId);
    CREATE INDEX IX_GpayAvsBatchItems_SegmentId ON dbo.GpayAvsBatchItems (SegmentId);
    CREATE INDEX IX_GpayAvsBatchItems_ExternalRecordId ON dbo.GpayAvsBatchItems (ExternalRecordId);
    CREATE INDEX IX_GpayAvsBatchItems_Batch_Row ON dbo.GpayAvsBatchItems (BatchId, RowNumber);
END
GO
