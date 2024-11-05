using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaDtoCreate
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        public int Occupancy { get; set; }
        public int SquareMeters { get; set; }
        public string Amenity { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [Required]
        public double Rate { get; set; }
    }
}
