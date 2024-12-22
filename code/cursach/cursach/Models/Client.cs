using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cursach.Models;

public partial class Client
{
    public int ClientId { get; set; }

    [Required(ErrorMessage = "First Name required.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last Name required.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Middle Name required.")]
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Passport Data required.")]
    public string? PassportData { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
