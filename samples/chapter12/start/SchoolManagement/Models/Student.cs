namespace SchoolManagement.Models;

public class Student
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Grade { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; }

    public Guid GroupId { get; set; }
    public Group Group { get; set; } = default!;
    public List<Course> Courses { get; set; } = new();
    public List<StudentCourse> StudentCourses { get; set; } = new();
}
