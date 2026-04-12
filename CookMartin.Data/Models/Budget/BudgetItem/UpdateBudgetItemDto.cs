using CookMartin.Data.Models.Budget;

namespace CookMartin.Data.Models.Budget.BudgetItem;

public class UpdateBudgetItemDto
{
    public int            BudgetItemId   { get; set; }
    public string         Label          { get; set; } = string.Empty;
    public decimal        BudgetedAmount { get; set; }
    public DateTime?      DueDate        { get; set; }
    public decimal?       ActualAmount   { get; set; }
    public DateTime?      ActualDate     { get; set; }
    public BudgetItemType Type           { get; set; }
}
