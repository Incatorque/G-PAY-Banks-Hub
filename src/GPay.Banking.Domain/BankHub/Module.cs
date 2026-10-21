using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Module")]
public partial class Module : Entity<Guid>
{
    [Key]
    [Column("PkModuleID")]
    public override Guid Id { get; protected set; }

    [StringLength(65)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("FkMenuItemID")]
    public Guid? FkMenuItemId { get; set; }

    [InverseProperty("FkModule")]
    public virtual ICollection<EntityModule> EntityModules { get; set; } = new List<EntityModule>();

    [ForeignKey("FkMenuItemId")]
    [InverseProperty("Modules")]
    public virtual Menu? FkMenuItem { get; set; }

    [InverseProperty("FkModule")]
    public virtual ICollection<GenericLookUpTable> GenericLookUpTables { get; set; } = new List<GenericLookUpTable>();

    [InverseProperty("FkModule")]
    public virtual ICollection<UserTypeModule> UserTypeModules { get; set; } = new List<UserTypeModule>();
}


