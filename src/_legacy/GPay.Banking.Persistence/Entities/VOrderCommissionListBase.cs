using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderCommissionListBase
{
    public Guid PkOrderId { get; set; }

    public Guid FkAgentId { get; set; }

    public decimal? DeliveredQuantity { get; set; }

    public decimal? VatIncl { get; set; }

    public decimal TotalPercComm { get; set; }

    public decimal TotalFixedComm { get; set; }

    public decimal TotalPerQcomm { get; set; }
}
