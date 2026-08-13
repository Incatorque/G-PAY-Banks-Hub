using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MobileMartUtilityToken
{
    public Guid PkMobileMartUtilityTokenId { get; set; }

    public string Type { get; set; } = null!;

    public string Token { get; set; } = null!;

    public int? Amount { get; set; }

    public int? Tax { get; set; }

    public Guid FkMobileMartResponseId { get; set; }

    public virtual MobileMartResponse FkMobileMartResponse { get; set; } = null!;

    public virtual ICollection<MobileMartUtilityUnit> MobileMartUtilityUnits { get; set; } = new List<MobileMartUtilityUnit>();
}
