using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Role
{
    public int IdRole { get; set; }

    public int IdProfil { get; set; }

    public string TypeRole { get; set; } = null!;

    public virtual Profil IdProfilNavigation { get; set; } = null!;

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();

    public virtual ICollection<Menu> IdMenus { get; set; } = new List<Menu>();
}
