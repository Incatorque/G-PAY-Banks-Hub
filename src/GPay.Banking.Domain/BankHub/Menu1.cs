using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Menus")]
public partial class Menu1 : Entity<int>
{
    [Key]
    [Column("MenuId")]
    public override int Id { get; protected set; }

    public int ParentMenuId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Title { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string Url { get; set; } = null!;
}


