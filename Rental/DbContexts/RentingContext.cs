using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rental.Entities;
using Rental.Enums;

namespace Rental.DbContexts
{
    public class RentingContext : DbContext
    {
        public RentingContext(DbContextOptions<RentingContext> options) : base(options)
        {

        }
        public DbSet<Equipment> Equipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment { Id = 1, Name = "Caterpillar bulldozer", Type = EquipmentType.Heavy, ImageUrl = "https://s7d2.scene7.com/is/image/Caterpillar/C830336?$cc-g$" },
                new Equipment { Id = 2, Name = "KamAZ truck", Type = EquipmentType.Regular, ImageUrl = "https://kamazexport.com/wp-content/uploads/2016/03/kamaz_43118-640x480.jpg" },
                new Equipment { Id = 3, Name = "Komatsu crane", Type = EquipmentType.Heavy, ImageUrl = "https://autoline.info/img/s/construction-equipment-crawler-craneKOMATSU-PC-360LC-10-s-predlzenym-ramenom-Long-Reach---1580382162735903530_big--20013013023838362900.jpg" },
                new Equipment { Id = 4, Name = "Volvo steamroller", Type = EquipmentType.Regular, ImageUrl = "https://www.volvoce.com/-/media/volvoce/global/products/compactors/soil-compactors/walkaround/volvo-find-soil-compactor-sd115b-t4f-walkaround-1000x1000.jpg?mw=420&v=89VDPw&jq=80&hash=65D602D4BBD93DC2DC6074E1369FD3D9DA3DA4C8" },
                new Equipment { Id = 5, Name = "Bosch jackhammer", Type = EquipmentType.Specialized, ImageUrl = "https://www.boschtools.com/us/en/ocsmedia/optimized/full/Bosch_Breaker_Hammer_BH2760VC_(EN).png" });
        }

    }
}
