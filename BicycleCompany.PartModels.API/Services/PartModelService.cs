using AutoMapper;
using BicycleCompany.PartModels.API.Boundary.Features;
using BicycleCompany.PartModels.API.Boundary.Request;
using BicycleCompany.PartModels.API.Boundary.Responses;
using BicycleCompany.PartModels.API.Helpers;
using BicycleCompany.PartModels.API.Models;
using BicycleCompany.PartModels.API.Repositories.Interfaces;
using BicycleCompany.PartModels.API.Services.Interfaces;
using BicycleCompany.PartModels.API.Utils;

namespace BicycleCompany.PartModels.API.Services
{
    /// <summary>
    /// Service to manage part models.
    /// </summary>
    public class PartModelService : IPartModelService
    {

        private readonly IMapper _mapper;
        private readonly IPartModelRepository _partModelRepository;
        private readonly IPartRepository _partRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ILoggerManager _logger;
        private readonly IFileStorageService _fileStorageService;

        public PartModelService(IMapper mapper, IPartModelRepository partModelRepository, ILoggerManager logger,
            IPartRepository partRepository, IManufacturerRepository  manufacturerRepository, IFileStorageService fileStorageService)
        {
            _mapper = mapper;
            _partModelRepository = partModelRepository;
            _logger = logger;
            _partRepository = partRepository;
            _manufacturerRepository = manufacturerRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<PartModelCreatedModel> CreateAsync(PartModelForCreateOrUpdateModel model)
        {
            await CheckIfAlreadyExists(model);

            CheckIfManufacturerExists(model.ManufacturerId);
            CheckIfPartExists(model.PartId);

            var entity = _mapper.Map<PartModel>(model);
            if (!string.IsNullOrWhiteSpace(model.ImageUrl))
            {
                var partModelPicture = Convert.FromBase64String(model.ImageUrl);
                entity.ImageUrl = await _fileStorageService.SaveFileAsync(partModelPicture, ".jpg", "part-models");
            }

            await _partModelRepository.CreateAsync(entity);

            var partModelDto = _mapper.Map<PartModelCreatedModel>(entity);

            return partModelDto;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _partModelRepository.GetByIdAsync(id);

            CheckIfFound(id, entity);

            await _partModelRepository.DeleteAsync(entity);
        }

        public async Task<PartModelForReadModel> GetByIdAsync(Guid id)
        {
            var partEntity = await _partModelRepository.GetByIdIncludedAsync(id);
            
            CheckIfFound(id, partEntity);

            return _mapper.Map<PartModelForReadModel>(partEntity);
        }

        public async Task<PagedList<PartModelForReadModel>> GetListAsync(PartModelParameters parameters)
        {
            var partModels = await _partModelRepository.GetAllAsync(parameters);
            //ToDo K: map to pagedlist -> custom converter?
            var partModelsDto = _mapper.Map<IEnumerable<PartModelForReadModel>>(partModels);

            return PagedList<PartModelForReadModel>.ToPagedList(partModelsDto, parameters.PageNumber, parameters.PageSize);
        }

        public async Task UpdateAsync(Guid id, PartModelForCreateOrUpdateModel model)
        {
            var entity = await _partModelRepository.GetByIdAsync(id);
            CheckIfFound(id, entity);

            CheckIfManufacturerExists(model.ManufacturerId);
            CheckIfPartExists(model.PartId);

            _mapper.Map(model, entity);
            if (!string.IsNullOrWhiteSpace(model.ImageUrl))
            {
                var partModelPicture = Convert.FromBase64String(model.ImageUrl);
                entity.ImageUrl = await _fileStorageService.EditFileAsync(partModelPicture, ".jpg", "part-models", entity.ImageUrl);
            }
            await _partModelRepository.UpdateAsync(entity);
        }

        private void CheckIfFound(Guid id, PartModel entity)
        {
            if (entity is null)
            {
                _logger.LogInfo($"Part model with id: {id} doesn't exist in the database.");
                throw new EntityNotFoundException(nameof(PartModel), id);
            }
        }

        private async Task CheckIfAlreadyExists(PartModelForCreateOrUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var partModel = await _partModelRepository.GetByNameAsync(model.Name);
            if (partModel != null)
            {
                _logger.LogInfo("Part model with the same name already exists.");
                throw new ArgumentException("Part model with the same name already exists.");
            }
        }
        
        private void CheckIfManufacturerExists(Guid manufacturerId)
        {
            var manufacturerExists = _manufacturerRepository.Exist(m => m.Id == manufacturerId);
            if (!manufacturerExists)
            {
                _logger.LogInfo($"Manufacturer with id {manufacturerId} name does not exist.");
                throw new EntityNotFoundException(nameof(Manufacturer),  manufacturerId);
            }
        }

        private void CheckIfPartExists(Guid partId)
        {
            var partExists = _partRepository.Exist(m => m.Id == partId);
            if (!partExists)
            {
                _logger.LogInfo($"Part with id {partId} name does not exist.");
                throw new EntityNotFoundException(nameof(Part),  partId);
            }
        }
    }
}
