using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaDtoUpdate
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public int SquareMeters { get; set; }
        public string Amenity { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [Required]
        public double Rate { get; set; }
    }
}
