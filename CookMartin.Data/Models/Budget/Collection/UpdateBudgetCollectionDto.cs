namespace CookMartin.Data.Models.Budget.Collection;

public class UpdateBudgetCollectionDto
{
    public int     CollectionId  { get; set; }
    public string  Name          { get; set; } = string.Empty;
    public decimal EmergencyFund { get; set; }
}
