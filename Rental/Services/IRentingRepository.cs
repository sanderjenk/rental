using Rental.Entities;
using System.Collections.Generic;

namespace Rental.Services
{
    public interface IRentingRepository
    {
        IEnumerable<Equipment> GetEquipments();
        Equipment GetEquipment(int id);
    }
}