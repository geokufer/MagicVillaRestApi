﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaNumberDto
    {
        [Required]
        public int Number { get; set; }
        public string SpecialDetails { get; set; }
    }
}
