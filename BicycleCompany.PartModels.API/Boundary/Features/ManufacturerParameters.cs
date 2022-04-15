namespace BicycleCompany.PartModels.API.Boundary.Features
{
    public class ManufacturerParameters : RequestParameters
    {
        public ManufacturerParameters()
        {
            OrderBy = "name";
        }
        public string SearchTerm { get; set; }
    }
}
