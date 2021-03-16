using Rental.Entities;
using System.Collections.Generic;
using Rental.Helpers;
using Rental.ResourceParameters;

namespace Rental.Services
{
    public interface IRentingRepository
    {
        IEnumerable<Equipment> GetEquipments();
        PagedList<Equipment> GetEquipments(EquipmentsResourceParameters parameters);
        Equipment GetEquipment(int id);
    }
}