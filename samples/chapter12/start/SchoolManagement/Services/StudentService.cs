using Microsoft.EntityFrameworkCore;

using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Services;

public class StudentService(IDbContextFactory<AppDbContext> contextFactory) : IStudentService
{
    public async Task<IQueryable<Student>> GetStudentsAsync()
    {
        var dbContext = await contextFactory.CreateDbContextAsync();
        var students = dbContext.Students.AsQueryable();
        return students;
    }

    public async Task<List<Student>> GetStudentsByGroupIdAsync(Guid groupId)
    {
        var dbContext = await contextFactory.CreateDbContextAsync();
        var students = await dbContext.Students.Where(x => x.GroupId == groupId).ToListAsync();
        return students;
    }

    public async Task<List<Student>> GetStudentsByGroupIdsAsync(List<Guid> groupIds)
    {
        var dbContext = await contextFactory.CreateDbContextAsync();
        var students = await dbContext.Students.Where(x => groupIds.Contains(x.GroupId)).ToListAsync();
        return students;
    }
}
