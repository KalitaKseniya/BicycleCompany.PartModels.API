namespace BicycleCompany.PartModels.API.Boundary.Features
{
    public abstract class RequestParameters
    {
        private const int MaxPageSize = 50;

        /// <summary>
        /// Page Number
        /// </summary>
        /// <example>1</example>
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        /// <summary>
        /// Page size
        /// </summary>
        /// <example>10</example>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        /// <summary>
        /// Rule how to order records
        /// </summary>
        /// <example>name</example>
        public string OrderBy { get; set; }
    }
}
