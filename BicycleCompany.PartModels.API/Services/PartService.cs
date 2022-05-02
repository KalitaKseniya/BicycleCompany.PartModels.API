using AutoMapper;
using BicycleCompany.Models.Request;
using BicycleCompany.Models.Response;
using BicycleCompany.PartModels.API.Boundary.Features;
using BicycleCompany.PartModels.API.Models;
using BicycleCompany.PartModels.API.Repositories.Interfaces;
using BicycleCompany.PartModels.API.Services.Interfaces;
using BicycleCompany.PartModels.API.Utils;
using Newtonsoft.Json;

namespace BicycleCompany.PartModels.API.Services
{
    /// <summary>
    /// Service to manage parts.
    /// </summary>
    public class PartService : IPartService
    {
        private readonly IMapper _mapper;
        private readonly IPartRepository _partRepository;
        private readonly ILoggerManager _logger;

        public PartService(IMapper mapper, IPartRepository partRepository, ILoggerManager logger)
        {
            _mapper = mapper;
            _partRepository = partRepository;
            _logger = logger;
        }

        public async Task<List<PartForReadModel>> GetPartListAsync(PartParameters partParameters, HttpResponse response)
        {
            var parts = await _partRepository.GetPartsAsync(partParameters);
            if (response != null)
            {
                response.Headers.Add("Pagination", JsonConvert.SerializeObject(parts.MetaData));
            }

            return _mapper.Map<List<PartForReadModel>>(parts);
        }

        public async Task<PartForReadModel> GetPartAsync(Guid id)
        {
            var partEntity = await _partRepository.GetPartAsync(id);
            CheckIfFound(id, partEntity);
            return _mapper.Map<PartForReadModel>(partEntity);
        }

        public async Task<Guid> CreatePartAsync(PartForCreateOrUpdateModel model)
        {
            await CheckIfAlreadyExists(model);

            var partEntity = _mapper.Map<Part>(model);

            await _partRepository.CreatePartAsync(partEntity);

            return partEntity.Id;
        }

        public async Task DeletePartAsync(Guid id)
        {
            var partEntity = await _partRepository.GetPartAsync(id);

            CheckIfFound(id, partEntity);

            await _partRepository.DeletePartAsync(partEntity);
        }

        public async Task UpdatePartAsync(Guid id, PartForCreateOrUpdateModel model)
        {
            await CheckIfAlreadyExists(model);

            var partEntity = await _partRepository.GetPartAsync(id);
            CheckIfFound(id, partEntity);

            _mapper.Map(model, partEntity);
            await _partRepository.UpdatePartAsync(partEntity);
        }

        public async Task<PartForCreateOrUpdateModel> GetPartForUpdateModelAsync(Guid id)
        {
            var partEntity = await GetPartAsync(id);

            return _mapper.Map<PartForCreateOrUpdateModel>(partEntity);
        }

        private void CheckIfFound(Guid id, Part partEntity)
        {
            if (partEntity is null)
            {
                _logger.LogInfo($"Part with id: {id} doesn't exist in the database.");
                throw new EntityNotFoundException("Part", id);
            }
        }

        private async Task CheckIfAlreadyExists(PartForCreateOrUpdateModel model)
        {
            var part = await _partRepository.GetPartByNameAsync(model.Name);
            if (part != null)
            {
                _logger.LogInfo("Part with the same name already exists.");
                throw new ArgumentException("Part with the same name already exists.");
            }
        }
    }
}
