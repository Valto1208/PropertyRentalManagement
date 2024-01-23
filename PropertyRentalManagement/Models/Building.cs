using System;
using System.Collections.Generic;

namespace PropertyRentalManagement.Models;

public partial class Building
{
    public int BuildingId { get; set; }

    public string Address { get; set; } = null!;

    public string BuildingName { get; set; } = null!;

    public virtual ICollection<Appartment> Appartments { get; set; } = new List<Appartment>();
}
