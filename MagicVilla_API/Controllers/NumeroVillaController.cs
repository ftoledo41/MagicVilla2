using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Dto;
using MagicVilla_API.Models;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly INumeroVillaRepositorio _numerovillaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroVillaController(ILogger<NumeroVillaController> 
            logger, IVillaRepositorio villaRepo, INumeroVillaRepositorio numerovillaRepo, IMapper mapper) 
        { 
            _logger = logger;
            _villaRepo = villaRepo;
            _numerovillaRepo = numerovillaRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener las villas");
                //Lista de las villas
                IEnumerable<NumeroVilla> villaList = await _numerovillaRepo.ObtenerTodos();
                //
                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(villaList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErroreMessages = new List<string>() { ex.ToString() };
            }

            return _response;
            
        }

        [HttpGet("id:int", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer villa con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var numerovilla = await _numerovillaRepo.Obtener(v => v.VillaNo == id);

                if (numerovilla == null)          
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<NumeroVillaDto>(numerovilla);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErroreMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                if (await _numerovillaRepo.Obtener(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("Nombre Existe", "La villa con ese nombre ya existe");
                    return BadRequest(ModelState);
                }

                if(await _villaRepo.Obtener(v=>v.Id == createDto.VillaId) == null)
                {
                    ModelState.AddModelError("LlaveForanea", "El Id de la villa no existe");
                    return BadRequest(ModelState);
                }


                if (createDto == null) return BadRequest(createDto);

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);

               
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _numerovillaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = modelo.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErroreMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso=false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                
                var villa = await _numerovillaRepo.Obtener(v => v.VillaNo == id);
                if (villa == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _numerovillaRepo.Remover(villa);

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErroreMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto UpdatevillaDto)
        {
            if (UpdatevillaDto == null || id != UpdatevillaDto.VillaNo)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _villaRepo.Obtener(v => v.Id == UpdatevillaDto.VillaId) == null)
            {
                ModelState.AddModelError("LlaveForanea", "El Id de la villa no existe");
                return BadRequest(ModelState);
            }

            NumeroVilla modelo = _mapper.Map<NumeroVilla>(UpdatevillaDto);
            modelo.FechaActualizacion = DateTime.Now;
            await _numerovillaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
