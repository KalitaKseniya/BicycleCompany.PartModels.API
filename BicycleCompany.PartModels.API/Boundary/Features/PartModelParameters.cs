namespace BicycleCompany.PartModels.API.Boundary.Features
{
    public class PartModelParameters : RequestParameters
    {
        public PartModelParameters()
        {
            OrderBy = "name";
        }

        public string SearchTerm { get; set; }
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = int.MaxValue;
        public bool IsPriceRangeValid()
        {
            return MinPrice <= MaxPrice;
        }
    }
}
