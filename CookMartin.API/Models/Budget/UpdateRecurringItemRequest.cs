using CookMartin.Data.Models.Budget;

namespace CookMartin.API.Models.Budget;

public class UpdateRecurringItemRequest
{
    public string            Label    { get; set; } = string.Empty;
    public decimal           Amount   { get; set; }
    public RecurringItemType Type     { get; set; }
    public bool              IsShared { get; set; }
}
