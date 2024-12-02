using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HouseFinderAPI.Models
{
    public class House
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HouseId { get; set; }

        public string? HouseName { get; set; }
        public int? Sqft { get; set; }
        public string? HouseNoOrRoad { get; set; }


        public string? Division { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }

        public string? HouseAddress
        {
            get
            {
                return HouseName + ", " + HouseNoOrRoad + ", " + City + ", " + District + ", " + Division;
            }
            set {; }
        }
        public int? BedNumber { get; set; }
        public int? BalconyNumber { get; set; }
        public int? Rent { get; set; }
        public int? FloorNumber { get; set; }
        public bool? HasLift { get; set; } = false;
        public bool? IsAvailable { get; set; }
        public string? ContactNumber { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now.Date;
        public DateTime? LastUpdatedAt { get; set; } = DateTime.Now.Date;
    }
}
