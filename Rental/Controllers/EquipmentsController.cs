using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rental.Entities;
using Rental.Models;
using Rental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Rental.ResourceParameters;

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
        public IActionResult GetEquipments([FromQuery]EquipmentsResourceParameters parameters)
        {
            var equipmentsFromRepo = _rentingRepository.GetEquipments(parameters);

            var paginationMetadata = new
            {
                totalCount = equipmentsFromRepo.TotalCount,
                pageSize = equipmentsFromRepo.PageSize,
                currentPage = equipmentsFromRepo.CurrentPage,
                totalPages = equipmentsFromRepo.TotalPages
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            var equipmentsToReturn = _mapper.Map<IEnumerable<EquipmentDto>>(equipmentsFromRepo);

            return Ok(equipmentsToReturn);
        }
    }
}
