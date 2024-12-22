using System;
using System.Collections.Generic;

namespace cursach.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public string? PassportData { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
