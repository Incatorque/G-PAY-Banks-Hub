using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MobileMartUtilityMunicipalityDetail
{
    public Guid PkMobileMartUtilityMunicipalityDetailId { get; set; }

    public string Municipality { get; set; } = null!;

    public string MunicipalityVatNumber { get; set; } = null!;

    public Guid FkMobileMartResponseId { get; set; }

    public virtual MobileMartResponse FkMobileMartResponse { get; set; } = null!;
}
