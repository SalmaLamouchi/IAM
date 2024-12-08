using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Profil
{
    public int IdProfil { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public int? Telephone { get; set; }

    public string? Adresse { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
