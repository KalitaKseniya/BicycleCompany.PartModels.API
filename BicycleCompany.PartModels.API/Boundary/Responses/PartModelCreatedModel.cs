namespace BicycleCompany.PartModels.API.Boundary.Responses
{
    public class PartModelCreatedModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
        public Guid ManufacturerId { get; set; }
        public Guid PartId { get; set; }
    }
}
