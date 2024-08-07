using System.ComponentModel.DataAnnotations.Schema;

namespace MeteredRateofFare.Models
{
    public class MeteredFareModel
    {
        public int Id { get; set; }

        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public int MilesTraveled { get; set; }
        public int MinutesTraveled { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
