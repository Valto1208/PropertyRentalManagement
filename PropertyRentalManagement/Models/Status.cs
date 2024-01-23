using System;
using System.Collections.Generic;

namespace PropertyRentalManagement.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusDescription { get; set; } = null!;

    public virtual ICollection<Appartment> Appartments { get; set; } = new List<Appartment>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
