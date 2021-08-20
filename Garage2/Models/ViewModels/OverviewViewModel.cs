using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models.ViewModels
{
    public class OverviewViewModel
    {
        public int Id { get; set; }
        public VehicleType VehicleType { get; set; }
        public string LicensePlate { get; set; }
        public DateTime ArrivalTime { get; set; }
        [Display(Name = "Parked Time")]
        public TimeSpan ParkedTime { get; set; }
        public string State { get; set; }

    }
}
