namespace CookMartin.API.Models.Oscar;

public class SubmitPicksRequest
{
    public string UserName { get; set; } = string.Empty;
    public List<int> Picks { get; set; } = [];
}
