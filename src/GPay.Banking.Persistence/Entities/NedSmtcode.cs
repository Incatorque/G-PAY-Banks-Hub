using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class NedSmtcode
{
    public int PkNedSmscodeId { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;
}
