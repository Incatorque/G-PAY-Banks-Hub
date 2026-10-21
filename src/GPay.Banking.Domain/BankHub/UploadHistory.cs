using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UploadHistory")]
public partial class UploadHistory : Entity<int>
{
    [Key]
    public override int Id { get; protected set; }

    [Column(TypeName = "datetime")]
    public DateTime UploadDate { get; set; }

    public Guid UserId { get; set; }

    public byte[] UploadedData { get; set; } = null!;

    [Unicode(false)]
    public string? OriginalPath { get; set; }

    [Unicode(false)]
    public string? FileName { get; set; }
}


