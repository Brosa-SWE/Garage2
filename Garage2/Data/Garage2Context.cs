using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage2.Models;
using Garage2.Models.ViewModels;

namespace Garage2.Data
{
    public class Garage2Context : DbContext
    {
        public Garage2Context (DbContextOptions<Garage2Context> options)
            : base(options)
        {
        }

        public DbSet<Garage2.Models.ParkedVehicle> ParkedVehicle { get; set; }

        public DbSet<Garage2.Models.ViewModels.DetailViewModel> DetailViewModel { get; set; }

        public DbSet<Garage2.Models.ViewModels.OverviewViewModel> OverviewViewModel { get; set; }
    }
}
