using System;
using System.Collections.Generic;

namespace SharedModelsLib;

public partial class Player
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Country { get; set; } = null!;

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;
}
