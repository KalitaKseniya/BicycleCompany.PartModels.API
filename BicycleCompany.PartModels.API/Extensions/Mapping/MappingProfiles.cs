using AutoMapper;
using BicycleCompany.PartModels.API.Boundary.Features;
using BicycleCompany.PartModels.API.Boundary.Request;
using BicycleCompany.PartModels.API.Boundary.Responses;
using BicycleCompany.PartModels.API.Boundary.Responses.Manufacturer;
using BicycleCompany.PartModels.API.Models;

namespace BicycleCompany.PartModels.API.Extensions.Mapping
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

            CreateMap<PartModelForCreateOrUpdateModel, PartModel>()
                .ForMember(x => x.ImageUrl, opt => opt.Ignore());
            CreateMap<PartModel, PartModelCreatedModel>();
            CreateMap<PartModel, PartModelForReadModel>();

            //CreateMap<PagedList<PartModel>, PagedList<PartModelForReadModel>>().ReverseMap();
        }
    }
}
