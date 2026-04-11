namespace CookMartin.API.Models.Budget;

public class CreateBudgetItemRequest
{
    public string    Label          { get; set; } = string.Empty;
    public decimal   BudgetedAmount { get; set; }
    public DateTime? DueDate        { get; set; }
    public string    Type           { get; set; } = string.Empty;
}
