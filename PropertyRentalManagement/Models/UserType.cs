using System;
using System.Collections.Generic;

namespace PropertyRentalManagement.Models;

public partial class UserType
{
    public int UserTypeId { get; set; }

    public string UserDescription { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
