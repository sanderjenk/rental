using AutoMapper;
using Rental.Entities;
using Rental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.Profiles
{
    public class RentingProfile:Profile
    {
        public RentingProfile()
        {
            CreateMap<Equipment, EquipmentDto>();
        }
    }
}
