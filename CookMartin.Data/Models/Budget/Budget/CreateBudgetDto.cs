namespace CookMartin.Data.Models.Budget.Budget;

public class CreateBudgetDto
{
    public int     CollectionId   { get; set; }
    public string  Name           { get; set; } = string.Empty;
    public string  Type           { get; set; } = string.Empty;
    public decimal StartingAmount { get; set; }
}
