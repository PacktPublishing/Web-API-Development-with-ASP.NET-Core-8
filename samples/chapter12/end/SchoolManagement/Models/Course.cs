namespace SchoolManagement.Models;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CourseCode { get; set; } = string.Empty;
    public int Credits { get; set; }
    public string? Description { get; set; }
    public CourseType CourseType { get; set; }

    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = default!;

    public List<Teacher> Teachers { get; set; } = new();
    public List<TeacherCourse> TeacherCourses { get; set; } = new();
    public List<Student> Students { get; set; } = new();
    public List<StudentCourse> StudentCourses { get; set; } = new();
}
