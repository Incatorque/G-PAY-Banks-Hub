using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Table("Frequency")]
[Keyless]
public partial class Frequency
{
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [InverseProperty("FkFrequency")]
    public virtual ICollection<Sweep> Sweeps { get; set; } = new List<Sweep>();
}


