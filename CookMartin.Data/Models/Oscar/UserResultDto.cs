namespace CookMartin.Data.Models.Oscar;

public class UserResultDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public int? PickedNomineeId { get; set; }
    public string? PickedNomineeName { get; set; }
    public int? WinnerNomineeId { get; set; }
    public string? WinnerNomineeName { get; set; }
    public bool IsCorrect { get; set; }
}
