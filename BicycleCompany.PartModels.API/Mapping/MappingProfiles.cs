using AutoMapper;
using BicycleCompany.Models.Request;
using BicycleCompany.Models.Response;
using BicycleCompany.PartModels.API.Boundary.Request;
using BicycleCompany.PartModels.API.Boundary.Responses.Manufacturer;
using BicycleCompany.PartModels.API.Models;

namespace BicycleCompany.PartModels.API.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Part, PartForReadModel>();
            CreateMap<PartForCreateOrUpdateModel, Part>().ReverseMap();
            CreateMap<PartForReadModel, PartForCreateOrUpdateModel>();
            
            CreateMap<Manufacturer, ManufacturerForReadModel>();
            CreateMap<ManufacturerForCreateOrUpdateModel, Manufacturer>().ReverseMap();
        }
    }
}
