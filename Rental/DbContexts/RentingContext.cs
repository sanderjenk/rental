using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rental.Entities;
using Rental.Enums;

namespace Rental.DbContexts
{
    public class RentingContext: DbContext
    {
        public RentingContext(DbContextOptions<RentingContext> options) : base(options)
        {

        }
        public DbSet<Equipment> Equipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>().HasData(new Equipment { Id = 1, Name = "Caterpillar bulldozer", Type = EquipmentType.Heavy});
            modelBuilder.Entity<Equipment>().HasData(new Equipment { Id = 2, Name = "KamAZ truck", Type = EquipmentType.Regular});
            modelBuilder.Entity<Equipment>().HasData(new Equipment { Id = 3, Name = "Komatsu crane", Type = EquipmentType.Heavy});
            modelBuilder.Entity<Equipment>().HasData(new Equipment { Id = 4, Name = "Volvo steamroller", Type = EquipmentType.Regular});
            modelBuilder.Entity<Equipment>().HasData(new Equipment { Id = 5, Name = "Bosch jackhammer", Type = EquipmentType.Specialized});
        }

    }
}
