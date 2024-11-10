using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaNumberDtoUpdate
    {
        [Required]
        public int Number { get; set; }
        public string SpecialDetails { get; set; }
    }
}
