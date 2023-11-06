namespace SchoolManagement.Models;

public class LabRoom : ISchoolRoom
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Capacity { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Equipment { get; set; } = string.Empty;
    public bool HasChemicals { get; set; }
}
