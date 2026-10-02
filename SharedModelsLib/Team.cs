using System;
using System.Collections.Generic;

namespace SharedModelsLib;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SportId { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual Sport Sport { get; set; } = null!;
}
