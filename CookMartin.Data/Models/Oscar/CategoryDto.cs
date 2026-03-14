namespace CookMartin.Data.Models.Oscar;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public List<NomineeDto> Nominees { get; set; } = [];
}
