using System.ComponentModel.DataAnnotations;

namespace BicycleCompany.PartModels.API.Boundary.Request
{
    public class ManufacturerForCreateOrUpdateModel
    {
        [Required]
        public string Name { get; set; }
    }
}
