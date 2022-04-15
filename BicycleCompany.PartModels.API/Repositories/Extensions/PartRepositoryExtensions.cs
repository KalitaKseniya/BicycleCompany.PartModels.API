using BicycleCompany.PartModels.API.Models;
using BicycleCompany.PartModels.API.Repositories.Extensions.Utils;
using System.Linq.Dynamic.Core;

namespace BicycleCompany.PartModels.API.Repositories.Extensions
{
    public static class PartRepositoryExtensions
    {
        public static IQueryable<Part> Search(this IQueryable<Part> parts, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return parts;
            }

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return parts.Where(c => c.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Part> Sort(this IQueryable<Part> parts, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return parts.OrderBy(c => c.Name);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Part>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return parts.OrderBy(c => c.Name);
            }

            return parts.OrderBy(orderQuery);
        }
    }
}
