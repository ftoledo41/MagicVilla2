using MagicVilla_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Dto
{
    public class NumeroVillaUpdateDto
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }

        public string DetalleEspecial { get; set; }
    }
}
