using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MobileMartUtilityConsumerDetail
{
    public Guid PkMobileMartConsumerDetailId { get; set; }

    public string Name { get; set; } = null!;

    public string Message { get; set; } = null!;

    public Guid FkMobileMartResponseId { get; set; }

    public virtual MobileMartResponse FkMobileMartResponse { get; set; } = null!;
}
