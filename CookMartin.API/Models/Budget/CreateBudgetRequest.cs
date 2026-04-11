namespace CookMartin.API.Models.Budget;

public class CreateBudgetRequest
{
    public string  Name           { get; set; } = string.Empty;
    public string  Type           { get; set; } = string.Empty;
    public decimal StartingAmount { get; set; }
}
