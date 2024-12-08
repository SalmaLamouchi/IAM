using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string Titre { get; set; } = null!;

    public string? Description { get; set; }

    public string? Link { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<Menu> InverseParent { get; set; } = new List<Menu>();

    public virtual Menu? Parent { get; set; }

    public virtual ICollection<Role> IdRoles { get; set; } = new List<Role>();
}
