using System;
using System.Collections.Generic;

namespace invoices.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Phone { get; set; } = null!;

    public string? City { get; set; }

    public string? Governorate { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
