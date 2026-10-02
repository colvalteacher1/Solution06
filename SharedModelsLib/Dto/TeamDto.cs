using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModelsLib.Dto
{
    public class TeamDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int SportId { get; set; }
    }
}
