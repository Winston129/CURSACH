using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cursach.Models;

public class GroupedItemViewModel
{
    public decimal? Price { get; set; }
    public List<Item> Items { get; set; }
}
