namespace CookMartin.Data.Models.Budget.Budget;

public class UpdateBudgetDto
{
    public int     BudgetId       { get; set; }
    public string  Name           { get; set; } = string.Empty;
    public string  Type           { get; set; } = string.Empty;
    public decimal StartingAmount { get; set; }
}
