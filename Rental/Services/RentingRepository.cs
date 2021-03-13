using Rental.DbContexts;
using Rental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.Services
{
    public class RentingRepository : IRentingRepository
    {
        private readonly RentingContext _context;
        public RentingRepository(RentingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IEnumerable<Equipment> GetEquipments()
        {
            return _context.Equipments;
        }

        public Equipment GetEquipment(int id)
        {
            return _context.Equipments.Find(id);
        }
    }
}
