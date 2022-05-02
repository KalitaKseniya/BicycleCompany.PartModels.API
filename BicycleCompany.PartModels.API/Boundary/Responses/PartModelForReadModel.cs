using BicycleCompany.Models.Response;
using BicycleCompany.PartModels.API.Boundary.Responses.Manufacturer;

namespace BicycleCompany.PartModels.API.Boundary.Responses
{
    public class PartModelForReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public ManufacturerForReadModel Manufacturer { get; set; }
        public PartForReadModel Part { get; set; }
    }
}
