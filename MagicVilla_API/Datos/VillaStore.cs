using MagicVilla_API.Dto;
using System.Diagnostics.Metrics;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> VillaList = new List<VillaDto>
        {
            new VillaDto{Id=1, Name="Vista a la Piscina", Occupants=3, SquareMeter=50},
            new VillaDto{Id=2, Name="Vista a la Playa", Occupants=4, SquareMeter=80}
        };
    }
}
