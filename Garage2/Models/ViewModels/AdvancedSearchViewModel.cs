using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models.ViewModels
{
    public class AdvancedSearchViewModel
    {
        public int Id { get; set; }
        public VehicleType VehicleType { get; set; }
        public string LicensePlate { get; set; }
        [Display(Name = "Parked From")]
        public DateTime ArrivalTime { get; set; }
        [Display(Name = "Parked To")]
        public DateTime DepartureTime { get; set; }
        [Display(Name = "Total Parked Time")]
        public TimeSpan ParkedTime { get; set; }
        public string State { get; set; }
    }
}
