namespace BicycleCompany.PartModels.API.Models
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<PartModel> PartModels { get; set; }
    }
}
