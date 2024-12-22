using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cursach.Models;

public partial class Item
{
    public int ItemId { get; set; }

    [Required(ErrorMessage = "Name required.")]
    public string? ItemName { get; set; }

    [Required(ErrorMessage = "Type Name required.")]
    public int? ItemTypeId { get; set; }

    [Required(ErrorMessage = "Price required.")]
    public decimal? Price { get; set; }

    [Required(ErrorMessage = "Status required.")]
    public string? Status { get; set; }

    public int? AvailableId { get; set; }

    public int? ReservedId { get; set; }

    public int? SoldId { get; set; }

    public int? ClientId { get; set; }

    public virtual Available? Available { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ItemType? ItemType { get; set; }

    public virtual Reserved? Reserved { get; set; }

    public virtual Sold? Sold { get; set; }
}
