namespace CookMartin.API.Models.Budget;

public class CreateBudgetCollectionRequest
{
    public string  Name          { get; set; } = string.Empty;
    public decimal EmergencyFund { get; set; }
}
