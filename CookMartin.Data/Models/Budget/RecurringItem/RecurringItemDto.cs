using CookMartin.Data.Models.Budget;

namespace CookMartin.Data.Models.Budget.RecurringItem;

public class RecurringItemDto
{
    public int               RecurringItemId { get; set; }
    public string            Label           { get; set; } = string.Empty;
    public decimal           Amount          { get; set; }
    public RecurringItemType Type            { get; set; }
    public bool     IsShared       { get; set; }
    public string   UserId         { get; set; } = string.Empty;
    public int      CollectionId   { get; set; }
    public DateTime CreatedDate    { get; set; }
    public DateTime? UpdatedDate   { get; set; }
    public bool     IsDeleted      { get; set; }
}
