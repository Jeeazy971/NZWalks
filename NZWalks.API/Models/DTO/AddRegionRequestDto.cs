using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Le Code doit être minimum de 3 caractères")]
        [MaxLength(3, ErrorMessage = "Le Code doit être maximum de 3 caractères")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Le nom doit être maximum de 3 caractères")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
