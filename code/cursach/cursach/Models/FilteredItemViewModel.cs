namespace cursach.Models;

public class FilteredItemViewModel
{
    public int ItemId { get; set; }
    public string? ItemName { get; set; }
    public decimal? Price { get; set; }
    public string? ClientName { get; set; }
    public DateTime? SoldDate { get; set; }
}