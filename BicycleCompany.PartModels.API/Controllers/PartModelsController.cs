using AutoMapper;
using BicycleCompany.PartModels.API.Boundary.Features;
using BicycleCompany.PartModels.API.Boundary.Request;
using BicycleCompany.PartModels.API.Boundary.Responses;
using BicycleCompany.PartModels.API.Extensions;
using BicycleCompany.PartModels.API.Repositories.Interfaces;
using BicycleCompany.PartModels.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BicycleCompany.PartModels.API.Controllers
{
    [ApiController]
    [Route("api/admin/part-models")]
    public class PartModelsController : ControllerBase
    {
        //ToDo K: get forproblem??
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IPartModelService _partModelService;

        public PartModelsController(ILoggerManager logger, IPartModelService partModelService, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _partModelService = partModelService;
        }

        /// <summary>
        /// Create new Part Model.
        /// </summary>
        /// <param name="partModelDto">The Part Model object for creation</param>
        /// <response code="201">Part created successfully</response> 
        /// <response code="400">Part model is invalid</response>
        /// <response code="401">You need to authorize first</response>
        /// <response code="403">Your role dosn't have enough rights</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ObjectResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpPost]
        public async Task<IActionResult> CreatePartModel([FromBody] PartModelForCreateOrUpdateModel partModelDto)
        {
            if (partModelDto == null)
            {
                return BadRequest("Part model cannot be null");
            }
            this.ValidateObject();

            var partModel = await _partModelService.CreateAsync(partModelDto);

            return new ObjectResult(partModel) { StatusCode = StatusCodes.Status201Created };
        }

        /// <summary>
        /// Return a list of all Parts Models.
        /// </summary>
        /// <response code="200">List of part  models returned successfully</response>
        /// <response code="401">You need to authorize first</response>
        /// <response code="403">Your role dosn't have enough rights</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PartModelForReadModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpGet]
        public async Task<IActionResult> GetPartModelList([FromQuery] PartModelParameters parameters)
        {
            if (!parameters.IsPriceRangeValid())
            {
                _logger.LogError($"Invalid price range minPrice={parameters.MinPrice} > maxPrice={parameters.MaxPrice}");
                return BadRequest($"Invalid price range minPrice ={ parameters.MinPrice} > maxPrice ={ parameters.MaxPrice}");
            }

            var partModels = await _partModelService.GetListAsync(parameters);

            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(partModels.MetaData));
            var partModelsDto = _mapper.Map<IEnumerable<PartModelForReadModel>>(partModels);

            return Ok(partModelsDto);
        }

        /// <summary>
        /// Return Part model.
        /// </summary>
        /// <param name="id">The value that is used to find part model</param>
        /// <response code="200">Part model returned successfully</response> 
        /// <response code="401">You need to authorize first</response>
        /// <response code="403">Your role dosn't have enough rights</response>
        /// <response code="404">Part model with provided id cannot be found!</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PartModelForReadModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpGet("{id}", Name = "GetPartModel")]
        public async Task<IActionResult> GetPartModel(Guid id)
        {
            var partModelEntity = await _partModelService.GetByIdAsync(id);

            return Ok(partModelEntity);
        }

        /// <summary>
        /// Delete Part model.
        /// </summary>
        /// <param name="id">The value that is used to find Part model</param>
        /// <response code="204">Part model deleted successfully</response>
        /// <response code="401">You need to authorize first</response>
        /// <response code="403">Your role dosn't have enough rights</response>
        /// <response code="404">Part model with provided id cannot be found!</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartModel(Guid id)
        {
            await _partModelService.DeleteAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Update Part model information.
        /// </summary>
        /// <param name="id">The value that is used to find Part model</param>
        /// <param name="partModel">The Part object which is used for update Part model with provided id</param>
        /// <response code="204">Part model updated successfully</response>
        /// <response code="400">Part model is invalid</response>
        /// <response code="401">You need to authorize first</response>
        /// <response code="403">Your role dosn't have enough rights</response>
        /// <response code="404">Part model with provided id cannot be found!</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePartModel(Guid id, [FromBody] PartModelForCreateOrUpdateModel partModel)
        {
            if (partModel == null)
            {
                return BadRequest("Part model can't be null.");
            }
            this.ValidateObject();

            await _partModelService.UpdateAsync(id, partModel);

            return NoContent();
        }
    }
}
