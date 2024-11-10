using System.Net;
using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController(
        ILogger<VillaApiController> logger,
        IVillaRepository villaRepository,
        IMapper mapper)
        : ControllerBase
    {
        protected ApiResponse ApiResponse = new();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetVillas()
        {
            logger.LogInformation("Getting all villas");

            try
            {
                IEnumerable<Villa> villas = await villaRepository.GetAllAsync();
                ApiResponse.Result = mapper.Map<List<VillaDto>>(villas);
                ApiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(ApiResponse);
            }
            catch (Exception e)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.Errors = [e.ToString()];
            }

            return ApiResponse;
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    logger.LogError("Get Villa Error with Id {Id}", id);
                    
                    ApiResponse.IsSuccess = false;
                    ApiResponse.StatusCode = HttpStatusCode.BadRequest;
                    ApiResponse.Errors = ["Id must be greater than 0"];

                    return BadRequest(ApiResponse);
                }

                var villa = await villaRepository.GetAsync(u => u.Id == id);

                if (villa == null)
                {
                    logger.LogError("Get Villa Error with Id {Id}", id);

                    ApiResponse.IsSuccess = false;
                    ApiResponse.StatusCode = HttpStatusCode.NotFound;
                    ApiResponse.Errors = ["Villa Id was not found"];

                    return NotFound(ApiResponse);
                }
                

                ApiResponse.Result = mapper.Map<VillaDto>(villa);
                ApiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(ApiResponse);
            }
            catch (Exception e)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.Errors = [e.ToString()];
            }

            return ApiResponse;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateVilla([FromBody] VillaDtoCreate? villaDtoCreate)
        {
            try
            {
                if (villaDtoCreate == null)
                    return BadRequest(villaDtoCreate);

                if (await villaRepository.GetAsync(u => u.Name.ToLower() == villaDtoCreate.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa name must be unique");
                    return BadRequest(ModelState);
                }

                var villa = mapper.Map<Villa>(villaDtoCreate);
                await villaRepository.CreateAsync(villa);

                ApiResponse.Result = mapper.Map<VillaDto>(villa);
                ApiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, ApiResponse);
            }
            catch (Exception e)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.Errors = [e.ToString()];
            }

            return ApiResponse;
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Id must be greater than 0");

                var villa = await villaRepository.GetAsync(u => u.Id == id);

                if (villa == null)
                    return NotFound("Villa Id was not found");

                await villaRepository.RemoveAsync(villa);

                ApiResponse.StatusCode = HttpStatusCode.NoContent;
                ApiResponse.IsSuccess = true;
                return Ok(ApiResponse);
            }
            catch (Exception e)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.Errors = [e.ToString()];
            }

            return ApiResponse;
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> UpdateVilla(int id, [FromBody] VillaDtoUpdate? villaDtoUpdate)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Id must be greater than 0");

                if (villaDtoUpdate == null)
                    return BadRequest(villaDtoUpdate);

                if (villaDtoUpdate.Id != id)
                    return BadRequest("Id must be same in the request body and the URL");

                var villa = mapper.Map<Villa>(villaDtoUpdate);

                await villaRepository.UpdateAsync(villa);

                ApiResponse.StatusCode = HttpStatusCode.NoContent;
                ApiResponse.IsSuccess = true;

                return Ok(ApiResponse);
            }
            catch (Exception e)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.Errors = [e.ToString()];
            }

            return ApiResponse;
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

            var villaModel = await villaRepository.GetAsync(u => u.Id == id, tracked: false);

            if (villaModel == null)
                return NotFound("Villa Id was not found");

            var villaDtoUpdate = mapper.Map<VillaDtoUpdate>(villaModel);

            patchDto.ApplyTo(villaDtoUpdate, ModelState);

            var modelVilla = mapper.Map<Villa>(villaDtoUpdate);

            await villaRepository.UpdateAsync(modelVilla);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }
    }
}
