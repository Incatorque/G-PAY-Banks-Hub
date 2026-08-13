using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Module
{
    public Guid PkModuleId { get; set; }

    public string Name { get; set; } = null!;

    public Guid? FkMenuItemId { get; set; }

    public virtual ICollection<EntityModule> EntityModules { get; set; } = new List<EntityModule>();

    public virtual Menu? FkMenuItem { get; set; }

    public virtual ICollection<GenericLookUpTable> GenericLookUpTables { get; set; } = new List<GenericLookUpTable>();

    public virtual ICollection<UserTypeModule> UserTypeModules { get; set; } = new List<UserTypeModule>();
}
