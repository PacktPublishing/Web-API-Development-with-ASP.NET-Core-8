namespace SchoolManagement.Models;

public class Teacher
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Bio { get; set; }

    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = default!;
    public List<Course> Courses { get; set; } = new();
    public List<TeacherCourse> TeacherCourses { get; set; } = new();
}
