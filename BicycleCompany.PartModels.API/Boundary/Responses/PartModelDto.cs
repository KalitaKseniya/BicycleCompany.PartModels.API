namespace BicycleCompany.PartModels.API.Boundary.Responses
{
    public class PartModelDto
    {
        public Guid Id { get; set; }
        public PartDto Part { get; set; }
        public string Name { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
        public ManufacturerDto Manufacturer { get; set; }
    }
}
