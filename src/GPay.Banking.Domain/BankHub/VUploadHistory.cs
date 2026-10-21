using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VUploadHistory
{
    public int Id { get; set; }

    [Unicode(false)]
    public string? FileName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UploadDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UploadBy { get; set; }

    public bool Active { get; set; }
}


