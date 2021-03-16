using Rental.DbContexts;
using Rental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rental.Helpers;
using Rental.ResourceParameters;

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

        public PagedList<Equipment> GetEquipments(EquipmentsResourceParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var collection = _context.Equipments as IQueryable<Equipment>;

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                var searchQuery = parameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery));
            }

            return PagedList<Equipment>.Create(collection,
                parameters.PageNumber,
                parameters.PageSize);
        }

        public Equipment GetEquipment(int id)
        {
            return _context.Equipments.Find(id);
        }
    }
}
