using CookMartin.Data.Models.Budget;

namespace CookMartin.Data.Models.Budget.Budget;

public class BudgetDto
{
    public int        BudgetId       { get; set; }
    public string     Name           { get; set; } = string.Empty;
    public BudgetType Type           { get; set; }
    public decimal  StartingAmount { get; set; }
    public int      CollectionId   { get; set; }
    public DateTime CreatedDate    { get; set; }
    public DateTime? UpdatedDate   { get; set; }
    public bool     IsDeleted      { get; set; }
}
