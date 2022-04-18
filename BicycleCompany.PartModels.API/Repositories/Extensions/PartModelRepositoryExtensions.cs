using BicycleCompany.PartModels.API.Models;
using BicycleCompany.PartModels.API.Repositories.Extensions.Utils;
using System.Linq.Dynamic.Core;

namespace BicycleCompany.PartModels.API.Repositories.Extensions
{
    public static class PartModelRepositoryExtensions
    {
        public static IQueryable<PartModel> Search(this IQueryable<PartModel> partModels, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return partModels;
            }

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return partModels.Where(c => c.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<PartModel> Sort(this IQueryable<PartModel> partModels, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return partModels.OrderBy(c => c.Name);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<PartModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return partModels.OrderBy(c => c.Name);
            }

            return partModels.OrderBy(orderQuery);
        }

        public static IQueryable<PartModel> FilterByPrice(this IQueryable<PartModel> partModels,
                                                          decimal minPrice,
                                                          decimal maxPrice)
           => partModels.Where(p => minPrice <= p.Price && p.Price <= maxPrice);
    }
}
