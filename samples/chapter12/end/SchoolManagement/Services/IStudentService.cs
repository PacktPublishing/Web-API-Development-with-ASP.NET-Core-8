using SchoolManagement.Models;

namespace SchoolManagement.Services;

public interface IStudentService
{
    Task<IQueryable<Student>> GetStudentsAsync();
    Task<List<Student>> GetStudentsByGroupIdAsync(Guid groupId);
    Task<List<Student>> GetStudentsByGroupIdsAsync(List<Guid> groupIds);
}
