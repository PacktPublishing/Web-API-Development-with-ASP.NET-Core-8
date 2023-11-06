using Microsoft.EntityFrameworkCore;

using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public EquipmentService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Equipment>> GetEquipmentListAsync()
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        var equipment = await dbContext.Equipment.ToListAsync();
        return equipment;
    }

    public async Task<Equipment> GetEquipmentAsync(Guid equipmentId)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        var equipment = await dbContext.Equipment.FindAsync(equipmentId);
        return equipment ?? throw new ArgumentException("Equipment not found", nameof(equipmentId));
    }
}
