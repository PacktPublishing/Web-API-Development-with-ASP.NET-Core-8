using Microsoft.EntityFrameworkCore;

using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Services;

public class FurnitureService : IFurnitureService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public FurnitureService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Furniture>> GetFurnitureListAsync()
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        var furniture = await dbContext.Furniture.ToListAsync();
        return furniture;
    }

    public async Task<Furniture> GetFurnitureAsync(Guid furnitureId)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        var furniture = await dbContext.Furniture.FindAsync(furnitureId);
        return furniture ?? throw new ArgumentException("Furniture not found", nameof(furnitureId));
    }
}
