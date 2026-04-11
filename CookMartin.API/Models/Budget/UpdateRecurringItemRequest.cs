namespace CookMartin.API.Models.Budget;

public class UpdateRecurringItemRequest
{
    public string  Label    { get; set; } = string.Empty;
    public decimal Amount   { get; set; }
    public string  Type     { get; set; } = string.Empty;
    public bool    IsShared { get; set; }
}
