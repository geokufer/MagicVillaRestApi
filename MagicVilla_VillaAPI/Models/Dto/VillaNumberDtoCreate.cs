using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaNumberDtoCreate
    {
        [Required]
        public int Number { get; set; }
        public string SpecialDetails { get; set; }
    }
}
