using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rental.Entities;
using Rental.Models;
using Rental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.Controllers
{
    [Route("api/equipments")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRentingRepository _rentingRepository;
        public EquipmentsController(IRentingRepository rentingRepository, IMapper mapper)
        {
            _rentingRepository = rentingRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetEquipments()
        {
            var equipments = _rentingRepository.GetEquipments();
            var dtos = _mapper.Map<IEnumerable<EquipmentDto>>(equipments);
            return Ok(dtos);
        }
    }
}
