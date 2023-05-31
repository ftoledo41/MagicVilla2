using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext :DbContext
    {
     
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Villa> villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Villa Real",
                    Details = "Detalle de la villa...",
                    ImagenUrl="",
                    Ocupantes=5,
                    MetrosCuadrados=5,
                    Tarifa=200,
                    Amenidad="",
                    FechaActualizacion=DateTime.Now,
                    FechaCreacion=DateTime.Now
                },

                new Villa()
                {
                    Id = 2,
                    Name = "Villa Real Premiun",
                    Details = "Detalle de la villa...",
                    ImagenUrl = "",
                    Ocupantes = 10,
                    MetrosCuadrados = 10,
                    Tarifa = 500,
                    Amenidad = "",
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now
                });
        }
    }
}
