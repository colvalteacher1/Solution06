using System;
using System.Collections.Generic;

namespace SharedModelsLib;

public partial class Sport
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int PlayersPerTeam { get; set; }

    public bool IsTeamSport { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
