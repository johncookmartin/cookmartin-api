namespace CookMartin.Data.Models.Budget.RecurringItem;

public class UpdateRecurringItemDto
{
    public int     RecurringItemId { get; set; }
    public string  Label           { get; set; } = string.Empty;
    public decimal Amount          { get; set; }
    public string  Type            { get; set; } = string.Empty;
    public bool    IsShared        { get; set; }
}
