using Microsoft.AspNetCore.Mvc;
using Rental.Entities;
using Rental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rental.Controllers
{
    [Route("api/equipments")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IRentingRepository _rentingRepository;
        public EquipmentsController(IRentingRepository rentingRepository)
        {
            _rentingRepository = rentingRepository;
        }
        // GET: api/<EquipmentsController>
        [HttpGet]
        public IEnumerable<Equipment> GetEquipments()
        {
            return new List<Equipment> {
               new Equipment { Name = "Caterpillar bulldozer", Type = Enums.EquipmentType.Heavy} 
            };
        }
    }
}
