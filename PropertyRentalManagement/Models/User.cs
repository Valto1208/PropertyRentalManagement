using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagement.Models;

public partial class User
{
   
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Email is required!")]
    [RegularExpression("^[a-z09_\\+-]+(\\.[a-z0-9_\\+-]+)(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage ="Not valid Email!")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage="Password Required")]
    public string Password { get; set; } = null!;

    public int UserType { get; set; } = 3;

    public virtual ICollection<Appartment> AppartmentManagers { get; set; } = new List<Appartment>();

    public virtual ICollection<Appartment> AppartmentOwners { get; set; } = new List<Appartment>();

    public virtual ICollection<Appointment> AppointmentManagers { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentTenants { get; set; } = new List<Appointment>();

    public virtual UserType UserTypeNavigation { get; set; } = null!;
}
