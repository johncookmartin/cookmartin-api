namespace CookMartin.Data.Models.Budget.Collection;

public class BudgetCollectionDto
{
    public int      CollectionId  { get; set; }
    public string   Name          { get; set; } = string.Empty;
    public decimal  EmergencyFund { get; set; }
    public string   OwnerId       { get; set; } = string.Empty;
    public DateTime CreatedDate   { get; set; }
    public DateTime? UpdatedDate  { get; set; }
    public bool     IsDeleted     { get; set; }
    public List<BudgetCollectionUserDto> Users { get; set; } = [];
}
