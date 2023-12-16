using SchoolManagement.Models;

namespace SchoolManagement.GraphQL.Mutations;

public class AddTeacherPayload(Teacher teacher)
{
    public Teacher Teacher { get; } = teacher;
}
