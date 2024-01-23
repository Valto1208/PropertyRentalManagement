using System;
using System.Collections.Generic;

namespace PropertyRentalManagement.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateTime { get; set; }
}
