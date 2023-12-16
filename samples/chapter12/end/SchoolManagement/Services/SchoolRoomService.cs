using Microsoft.EntityFrameworkCore;

using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Services;

public class SchoolRoomService(IDbContextFactory<AppDbContext> contextFactory) : ISchoolRoomService
{
    public async Task<List<ISchoolRoom>> GetSchoolRoomsAsync()
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var labRooms = await dbContext.LabRooms.ToListAsync();
        var classrooms = await dbContext.Classrooms.ToListAsync();
        var schoolRooms = new List<ISchoolRoom>();
        schoolRooms.AddRange(labRooms);
        schoolRooms.AddRange(classrooms);
        return schoolRooms;
    }

    public async Task<List<LabRoom>> GetLabRoomsAsync()
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var labRooms = await dbContext.LabRooms.ToListAsync();
        return labRooms;
    }

    public async Task<List<Classroom>> GetClassroomsAsync()
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var classrooms = await dbContext.Classrooms.ToListAsync();
        return classrooms;
    }

    public async Task<LabRoom> GetLabRoomAsync(Guid labRoomId)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var labRoom = await dbContext.LabRooms.FindAsync(labRoomId);
        return labRoom ?? throw new ArgumentException("LabRoom not found", nameof(labRoomId));
    }

    public async Task<Classroom> GetClassroomAsync(Guid classroomId)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var classroom = await dbContext.Classrooms.FindAsync(classroomId);
        return classroom ?? throw new ArgumentException("Classroom not found", nameof(classroomId));
    }
}
