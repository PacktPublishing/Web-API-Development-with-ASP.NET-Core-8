using Microsoft.EntityFrameworkCore;

using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Services;

public class TeacherService(IDbContextFactory<AppDbContext> contextFactory) : ITeacherService
{
    public async Task<List<Course>> GetCoursesAsync(Guid teacherId)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var teacher = await dbContext.Teachers.Include(x => x.Courses).FirstOrDefaultAsync(x => x.Id == teacherId);
        return teacher is null ? throw new ArgumentException("Teacher not found", nameof(teacherId)) : teacher.Courses;
    }

    public async Task<Department> GetDepartmentAsync(Guid departmentId)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var department = await dbContext.Departments.FindAsync(departmentId);
        return department ?? throw new ArgumentException("Department not found", nameof(departmentId));
    }

    public async Task<List<Teacher>> GetTeachersAsync()
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var teachers = await dbContext.Teachers.ToListAsync();
        return teachers;
    }

    public async Task<Teacher> GetTeacherAsync(Guid teacherId)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var teacher = await dbContext.Teachers.FindAsync(teacherId);
        return teacher ?? throw new ArgumentException("Teacher not found", nameof(teacherId));
    }

    public async Task<Teacher> CreateTeacherAsync(Teacher teacher)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        await dbContext.Teachers.AddAsync(teacher);
        await dbContext.SaveChangesAsync();
        return teacher;
    }

    public async Task<Teacher> UpdateTeacherAsync(Teacher teacher)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        dbContext.Teachers.Update(teacher);
        await dbContext.SaveChangesAsync();
        return teacher;
    }

    public async Task DeleteTeacherAsync(Guid teacherId)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var teacher = await dbContext.Teachers.FindAsync(teacherId);
        if (teacher is null)
        {
            throw new ArgumentException("Teacher not found", nameof(teacherId));
        }

        dbContext.Teachers.Remove(teacher);
        await dbContext.SaveChangesAsync();
    }
}
