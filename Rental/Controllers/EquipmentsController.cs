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
        public IActionResult GetEquipments()
        {
            var equipments = _rentingRepository.GetEquipments();
            return Ok(equipments);
        }
    }
}
