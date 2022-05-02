namespace BicycleCompany.PartModels.API.Boundary.Features
{
    public class PartParameters : RequestParameters
    {
        public PartParameters()
        {
            OrderBy = "name";
        }

        public string SearchTerm { get; set; }
    }
}
