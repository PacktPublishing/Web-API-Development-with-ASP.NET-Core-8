namespace EfCoreRelationshipsDemo.Models;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Student> Students { get; set; } = new();
    public List<StudentCourse> StudentCourses { get; set; } = new();
}
