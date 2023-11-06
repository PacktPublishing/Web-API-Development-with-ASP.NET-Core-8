using SchoolManagement.Models;

namespace SchoolManagement.Services;

public interface IEquipmentService
{
    Task<List<Equipment>> GetEquipmentListAsync();
    Task<Equipment> GetEquipmentAsync(Guid equipmentId);
    //Task<Equipment> CreateEquipmentAsync(Equipment equipment);
    //Task<Equipment> UpdateEquipmentAsync(Equipment equipment);
    //Task DeleteEquipmentAsync(Guid equipmentId);
}
