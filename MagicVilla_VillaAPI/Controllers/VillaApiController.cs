using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogger<VillaApiController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper
            ;

        public VillaApiController(ILogger<VillaApiController> logger, ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");

            IEnumerable<Villa> villas = await _applicationDbContext.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDto>>(villas));
        }

        [HttpGet("{id:int}",Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Get Villa Error with Id {Id}", id);
                return BadRequest("Id must be greater than 0");
            }

            var villa = await _applicationDbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);

            if (villa == null) 
                return NotFound("Villa Id was not found");

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody]VillaDtoCreate? villaDtoCreate)
        {
            #region Commented section

            //no need to check this as we are using [Required] and [MaxLength] attributes
            //if (!ModelState.IsValid) 
            //    return BadRequest(ModelState);

            //if (villaDto.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            //if (_applicationDbContext.Villas.FirstOrDefault(u => u.Name?.ToLower() == villaDto.Name?.ToLower()) != null)
            //{
            //    ModelState.AddModelError("CustomError", "Villa name must be unique");
            //    return BadRequest(ModelState);
            //}

            #endregion

            if (villaDtoCreate == null) 
                return BadRequest(villaDtoCreate);
            
            Villa modelVilla = _mapper.Map<Villa>(villaDtoCreate);

            await _applicationDbContext.Villas.AddAsync(modelVilla);
            await _applicationDbContext.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new {id = modelVilla.Id}, modelVilla);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0) 
                return BadRequest("Id must be greater than 0");

            var villa = await _applicationDbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);

            if (villa == null) 
                return NotFound("Villa Id was not found");

            _applicationDbContext.Villas.Remove(villa);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaDtoUpdate? villaDtoUpdate)
        {
            if (id == 0)
                return BadRequest("Id must be greater than 0");

            if (villaDtoUpdate == null)
                return BadRequest(villaDtoUpdate);

            if (villaDtoUpdate.Id != id)
                return BadRequest("Id must be same in the request body and the URL");

            var villa = _mapper.Map<Villa>(villaDtoUpdate);

            _applicationDbContext.Villas.Update(villa);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdateVillaPartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVillaPartial(int id, JsonPatchDocument<VillaDtoUpdate>? patchDto)
        {
            if (id == 0)
                return BadRequest("Id must be greater than 0");

            if (patchDto == null)
                return BadRequest(patchDto);
            
            var villaModel = await _applicationDbContext.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (villaModel == null)
                return NotFound("Villa Id was not found");

            var villaDtoUpdate = _mapper.Map<VillaDtoUpdate>(villaModel);

            patchDto.ApplyTo(villaDtoUpdate, ModelState);

            var modelVilla = _mapper.Map<Villa>(villaDtoUpdate);

            _applicationDbContext.Villas.Update(modelVilla);
            await _applicationDbContext.SaveChangesAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }
    }
}
