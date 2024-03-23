using NZWalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {

        [Required]
        [MaxLength(50, ErrorMessage = "Le nom doit être maximum de 50 caractères")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "La description doit être maximum de 1000 caractères")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
