using CookMartin.Data.Models.Budget;

namespace CookMartin.API.Models.Budget;

public class CreateBudgetRequest
{
    public string     Name           { get; set; } = string.Empty;
    public BudgetType Type           { get; set; }
    public decimal    StartingAmount { get; set; }
}
