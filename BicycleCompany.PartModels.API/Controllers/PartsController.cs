using BicycleCompany.PartModels.API.Boundary.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BicycleCompany.PartModels.API.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class PartsController : Controller
    {
        [HttpGet]
        public IActionResult GetParts()
        {
            var partModels = new List<PartDto>
            {
                Getpart(),
                Getpart(),
                Getpart(),
                Getpart(),
                Getpart(),
                Getpart()
            };
            return Ok(partModels);
        }

        private PartDto Getpart()
        {
            return new PartDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.First(),
            };
        }
    }
}
