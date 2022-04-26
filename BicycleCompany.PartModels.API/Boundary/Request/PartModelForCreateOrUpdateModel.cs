using System.ComponentModel.DataAnnotations;

namespace BicycleCompany.PartModels.API.Boundary.Request
{
    public class PartModelForCreateOrUpdateModel
    {
        [Required]
        public Guid PartId { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableQuantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public Guid ManufacturerId { get; set; }
    }
}
