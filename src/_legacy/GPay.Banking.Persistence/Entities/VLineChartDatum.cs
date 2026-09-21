using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VLineChartDatum
{
    public string? Class { get; set; }

    public Guid? PkClassId { get; set; }

    public decimal Amount { get; set; }

    public Guid PkEntityBankStatementId { get; set; }

    public Guid? FkAccountId { get; set; }

    public Guid? FkEntityId { get; set; }

    public Guid? FkClassCategoryId { get; set; }

    public DateTime TransactionDate { get; set; }
}
