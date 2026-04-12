using CookMartin.Data.Models.Budget;

namespace CookMartin.Data.Models.Budget.BudgetItem;

public class CreateBudgetItemDto
{
    public int            BudgetId       { get; set; }
    public string         UserId         { get; set; } = string.Empty;
    public string         Label          { get; set; } = string.Empty;
    public decimal        BudgetedAmount { get; set; }
    public DateTime?      DueDate        { get; set; }
    public BudgetItemType Type           { get; set; }
}
