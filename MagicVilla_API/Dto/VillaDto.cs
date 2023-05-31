using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Occupants { get; set; }
        public int SquareMeter { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
