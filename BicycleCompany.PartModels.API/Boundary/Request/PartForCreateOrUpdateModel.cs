using System.ComponentModel.DataAnnotations;

namespace BicycleCompany.PartModels.API.Boundary.Request
{
    public class PartForCreateOrUpdateModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
