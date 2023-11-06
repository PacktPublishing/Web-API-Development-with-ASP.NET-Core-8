using SchoolManagement.Models;

namespace SchoolManagement.Services;

public interface IFurnitureService
{
    Task<List<Furniture>> GetFurnitureListAsync();
    Task<Furniture> GetFurnitureAsync(Guid furnitureId);
    //Task<Furniture> CreateFurnitureAsync(Furniture furniture);
    //Task<Furniture> UpdateFurnitureAsync(Furniture furniture);
    //Task DeleteFurnitureAsync(Guid furnitureId);
}
