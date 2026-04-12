using CookMartin.Data.Models.Budget;

namespace CookMartin.Data.Models.Budget.Budget;

public class CreateBudgetDto
{
    public int        CollectionId   { get; set; }
    public string     Name           { get; set; } = string.Empty;
    public BudgetType Type           { get; set; }
    public decimal StartingAmount { get; set; }
}
