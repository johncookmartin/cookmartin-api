namespace CookMartin.Data.Models.Budget.BudgetItem;

public class BudgetItemDto
{
    public int       BudgetItemId   { get; set; }
    public string    Label          { get; set; } = string.Empty;
    public decimal   BudgetedAmount { get; set; }
    public DateTime? DueDate        { get; set; }
    public decimal?  ActualAmount   { get; set; }
    public DateTime? ActualDate     { get; set; }
    public string    Type           { get; set; } = string.Empty;
    public int       BudgetId       { get; set; }
    public string    UserId         { get; set; } = string.Empty;
    public DateTime  CreatedDate    { get; set; }
    public DateTime? UpdatedDate    { get; set; }
    public bool      IsDeleted      { get; set; }
}
