using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModelsLib.Dto
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public string Country { get; set; } = null!;

        public int TeamId { get; set; }
    }
}
