using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Dto;
using MagicVilla_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db, IMapper mapper) 
        { 
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas");
            //Lista de las villas
            IEnumerable<Villa> villaList = await _db.villas.ToListAsync();
            //
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer villa con Id " + id);
                return BadRequest();
            }             
            //var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
            var villa = await _db.villas.FirstOrDefaultAsync(x => x.Id == id);

            if (villa == null) return NotFound();
            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreateDto createvillaDto)
        {

            if (!ModelState.IsValid) return BadRequest();

            if ( await _db.villas.FirstOrDefaultAsync(v => v.Name.ToLower() == createvillaDto.Name.ToLower()) !=null)
            {
                ModelState.AddModelError("Nombre Existe", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (createvillaDto == null) return BadRequest(createvillaDto);

            //Villa modelo = new()
            //{
            //    Name = createvillaDto.Name,
            //    Details = createvillaDto.Details,
            //    ImagenUrl = createvillaDto.ImagenUrl,
            //    Ocupantes = createvillaDto.Occupants,
            //    Tarifa = createvillaDto.Tarifa,
            //    MetrosCuadrados = createvillaDto.SquareMeter,
            //    Amenidad = createvillaDto.Amenidad
            //};

            //Con esto evitamos ir agregando campos nuevos si se van agregando de la tabla
            Villa modelo = _mapper.Map<Villa>(createvillaDto);

            await _db.villas.AddAsync(modelo);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new {id=modelo.Id}, modelo);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0) return BadRequest();
            var villa = await _db.villas.FirstOrDefaultAsync(v => v.Id==id);
            if (villa == null) return NotFound();
            _db.villas.Remove(villa); //Remove no es asincrono
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto UpdatevillaDto)
        {
            if(UpdatevillaDto == null || id != UpdatevillaDto.Id) return BadRequest();
            Villa modelo = _mapper.Map<Villa>(UpdatevillaDto);
            _db.villas.Update(modelo); // Update no es asincrono
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0) return BadRequest();
            var villa = await _db.villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);
            if(villa == null) return BadRequest();
            patchDto.ApplyTo(villaDto, ModelState);
            if (!ModelState.IsValid) return BadRequest();
            Villa modelo = _mapper.Map<Villa>(villaDto);
            _db.villas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();

        }
    }
}
