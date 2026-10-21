using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("TransactionCodeMap")]
public partial class TransactionCodeMap : Entity<int>
{
    [Key]
    [Column("ID")]
    public override int Id { get; protected set; }

    [StringLength(200)]
    public string Term { get; set; } = null!;

    public int TransactionCodeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    public bool? Active { get; set; }

    [Column("ProductID")]
    public Guid? ProductId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("TransactionCodeMaps")]
    public virtual Product? Product { get; set; }

    [ForeignKey("TransactionCodeId")]
    [InverseProperty("TransactionCodeMaps")]
    public virtual TransactionCode TransactionCode { get; set; } = null!;
}


