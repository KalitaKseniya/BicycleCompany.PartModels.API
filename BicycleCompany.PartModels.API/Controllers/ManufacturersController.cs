using BicycleCompany.PartModels.API.Boundary.Request;
using BicycleCompany.PartModels.API.Boundary.Responses;
using BicycleCompany.PartModels.API.Boundary.Responses.Manufacturer;
using BicycleCompany.PartModels.API.Extensions;
using BicycleCompany.PartModels.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BicycleCompany.PartModels.API.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IManufacturerService _manufacturerService;

        public ManufacturersController(ILoggerManager logger, IManufacturerService manufacturerService)
        {
            _logger = logger;
            _manufacturerService = manufacturerService;
        }

        /// <summary>
        /// Return a list of all manufacturers.
        /// </summary>
        /// <response code="200">List of Manufacturers returned successfully</response>
        /// <response code="401">You need to authorize first</response>
        /// <response code="403">Your role doesn't have enough rights</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ManufacturerForReadModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetManufacturerList()
        {
            var manufacturer = await _manufacturerService.GetListAsync();

            return Ok(manufacturer);
        }

        /// <summary>
        /// Return Manufacturer.
        /// </summary>
        /// <param name="id">The value that is used to find Manufacturer</param>
        /// <response code="200">Manufacturer returned successfully</response> 
        /// <response code="401">You need to authorize first</response>
        /// <response code="403">Your role doesn't have enough rights</response>
        /// <response code="404">Manufacturer with provided id cannot be found!</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ManufacturerForReadModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpGet("{id:guid}", Name = "GetManufacturer")]
        public async Task<IActionResult> GetManufacturer(Guid id)
        {
            _logger.LogDebug("Logger exists?");
            var manufacturer = await _manufacturerService.GetByIdAsync(id);

            return Ok(manufacturer);
        }

        /// <summary>
        /// Create new Manufacturer.
        /// </summary>
        /// <param name="manufacturer">The Manufacturer object for creation</param>
        /// <response code="201">Manufacturer created successfully</response> 
        /// <response code="400">Manufacturer model is invalid</response>
        /// <response code="401">You need to authorize first</response>
        /// <response code="403">Your role doesn't have enough rights</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpPost]
        public async Task<IActionResult> CreateManufacturer([FromBody] ManufacturerForCreateOrUpdateModel manufacturer)
        {
            this.ValidateObject();

            var manufacturerId = await _manufacturerService.CreateAsync(manufacturer);

            return CreatedAtRoute("GetManufacturer", new { id = manufacturerId }, new AddedResponse(manufacturerId));
        }

        /// <summary>
        /// Delete Manufacturer.
        /// </summary>
        /// <param name="id">The value that is used to find Manufacturer</param>
        /// <response code="204">Manufacturer deleted successfully</response>
        /// <response code="401">You need to authorize first</response>
        /// <response code="403">Your role doesn't have enough rights</response>
        /// <response code="404">Manufacturer with provided id cannot be found!</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteManufacturer(Guid id)
        {
            await _manufacturerService.DeleteAsync(id);

            return NoContent();
        }
    }
}
