using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModelsLib.Dto
{
    public class SportDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int PlayersPerTeam { get; set; }

        public bool IsTeamSport { get; set; }
    }
}
