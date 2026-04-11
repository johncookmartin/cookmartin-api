namespace CookMartin.API.Models.Budget;

public class UpdateBudgetCollectionRequest
{
    public string  Name          { get; set; } = string.Empty;
    public decimal EmergencyFund { get; set; }
}
