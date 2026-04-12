using CookMartin.Data.Models.Budget;

namespace CookMartin.Data.Models.Budget.RecurringItem;

public class CreateRecurringItemDto
{
    public int               CollectionId { get; set; }
    public string            UserId       { get; set; } = string.Empty;
    public string            Label        { get; set; } = string.Empty;
    public decimal           Amount       { get; set; }
    public RecurringItemType Type         { get; set; }
    public bool              IsShared     { get; set; }
}
