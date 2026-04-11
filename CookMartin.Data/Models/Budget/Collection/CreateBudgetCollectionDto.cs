namespace CookMartin.Data.Models.Budget.Collection;

public class CreateBudgetCollectionDto
{
    public string  OwnerId       { get; set; } = string.Empty;
    public string  Name          { get; set; } = string.Empty;
    public decimal EmergencyFund { get; set; }
}
