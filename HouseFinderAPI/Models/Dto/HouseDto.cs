
namespace HouseFinderAPI.Models.Dto
{
    public class HouseDto
    {
        public int HouseId { get; set; }
        [Required]
        public string? HouseName { get; set; }
        [Required]
        public int? Sqft { get; set; }
        [Required]
        public string? HouseNoOrRoad { get; set; }
        [Required]
        public string? Division { get; set; }
        [Required]
        public string? District { get; set; }
        [Required]
        public string? City { get; set; }
        //public string? HouseAddress { get; set; }
        [Required]
        public int? BedNumber { get; set; }
        [Required]
        public int? BalconyNumber { get; set; }
        [Required]
        public int? Rent { get; set; }
        [Required]
        public int? FloorNumber { get; set; }
        [Required]
        public bool? HasLift { get; set; } = false;
        [Required]
        public bool? IsAvailable { get; set; }

        [MaxLength(11, ErrorMessage = "Cannot be longer than 11 characters")]
        public string? ContactNumber { get; set; }


    }
}
