namespace MVCProject01.Models;

public class Plan
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int DurationInDays { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
