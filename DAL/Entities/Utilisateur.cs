using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Utilisateur
{
    public int IdUser { get; set; }

    public string Email { get; set; } = null!;

    public string Motdepasse { get; set; } = null!;

    public int IdRole { get; set; }

    public string? Matricule { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;
}
