namespace CookMartin.API.Models.Budget;

public class UpdateBudgetItemRequest
{
    public string    Label          { get; set; } = string.Empty;
    public decimal   BudgetedAmount { get; set; }
    public DateTime? DueDate        { get; set; }
    public decimal?  ActualAmount   { get; set; }
    public DateTime? ActualDate     { get; set; }
    public string    Type           { get; set; } = string.Empty;
}
