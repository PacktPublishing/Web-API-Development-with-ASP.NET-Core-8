namespace SchoolManagement.Models;

public class Department
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<Teacher> Teachers { get; set; } = new();
    public List<Group> Groups { get; set; } = new();
    public List<Course> Courses { get; set; } = new();
}
