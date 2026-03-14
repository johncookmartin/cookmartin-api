namespace CookMartin.Data.Models.Oscar;

public class NomineeDto
{
    public int NomineeId { get; set; }
    public int CategoryId { get; set; }
    public string NomineeName { get; set; } = string.Empty;
    public bool IsWinner { get; set; }
}
