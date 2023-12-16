using Microsoft.EntityFrameworkCore;

using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Services;

public class EquipmentService(IDbContextFactory<AppDbContext> contextFactory) : IEquipmentService
{
    public async Task<List<Equipment>> GetEquipmentListAsync()
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var equipment = await dbContext.Equipment.ToListAsync();
        return equipment;
    }

    public async Task<Equipment> GetEquipmentAsync(Guid equipmentId)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var equipment = await dbContext.Equipment.FindAsync(equipmentId);
        return equipment ?? throw new ArgumentException("Equipment not found", nameof(equipmentId));
    }
}
