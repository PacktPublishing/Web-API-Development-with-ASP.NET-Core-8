namespace EfCoreRelationshipsDemo.Models;

public class Student
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<Course> Courses { get; set; } = new();
    public List<StudentCourse> StudentCourses { get; set; } = new();
}
