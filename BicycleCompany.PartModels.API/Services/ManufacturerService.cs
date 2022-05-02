using AutoMapper;
using BicycleCompany.PartModels.API.Boundary.Request;
using BicycleCompany.PartModels.API.Boundary.Responses.Manufacturer;
using BicycleCompany.PartModels.API.Models;
using BicycleCompany.PartModels.API.Repositories.Interfaces;
using BicycleCompany.PartModels.API.Services.Interfaces;
using BicycleCompany.PartModels.API.Utils;

namespace BicycleCompany.PartModels.API.Services
{
    /// <summary>
    /// Service to manage manufacturers.
    /// </summary>
    public class ManufacturerService : IManufacturerService
    {
        private readonly IMapper _mapper;
        private readonly IManufacturerRepository _repository;
        private readonly ILoggerManager _logger;

        public ManufacturerService(IMapper mapper, IManufacturerRepository manufacturerRepository, ILoggerManager logger)
        {
            _mapper = mapper;
            _repository = manufacturerRepository;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(ManufacturerForCreateOrUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            await CheckIfAlreadyExists(model);

            var entity = _mapper.Map<Manufacturer>(model);

            await _repository.CreateAsync(entity);

            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            CheckIfFound(id, entity);

            await _repository.DeleteAsync(entity);
        }

        public async Task<List<ManufacturerForReadModel>> GetListAsync()
        {
            var entities = await _repository.GetAllAsync();

            return _mapper.Map<List<ManufacturerForReadModel>>(entities);
        }

        public async Task<ManufacturerForReadModel> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            CheckIfFound(id, entity);

            return _mapper.Map<ManufacturerForReadModel>(entity);
        }

        private async Task CheckIfAlreadyExists(ManufacturerForCreateOrUpdateModel model)
        {
            var manufacturer = await _repository.GetByNameAsync(model.Name);
            if (manufacturer != null)
            {
                _logger.LogInfo("Manufacturer with the same name already exists.");
                throw new ArgumentException("Manufacturer with the same name already exists.");
            }
        }

        private void CheckIfFound(Guid id, Manufacturer entity)
        {
            if (entity is null)
            {
                _logger.LogInfo($"Manufacturer with id: {id} doesn't exist in the database.");
                throw new EntityNotFoundException(nameof(Manufacturer), id);
            }
        }
    }
}
