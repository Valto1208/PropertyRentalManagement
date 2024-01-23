using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagement.Models;

public partial class Appartment
{
    public int ApartmentId { get; set; }

    public int StatusId { get; set; }

    public int OwnerId { get; set; }

    public int AptNumber { get; set; }

    public int AptSize { get; set; }

    public decimal AptRent { get; set; }

    public int AptNumberOfRooms { get; set; }

    public string AptDescription { get; set; } = null!;

    public int BuildingId { get; set; }

    public int ManagerId { get; set; }

    public virtual Building Building { get; set; } = null!;

    public virtual User Manager { get; set; } = null!;

    public virtual User Owner { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

}
