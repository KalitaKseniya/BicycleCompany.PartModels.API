using BicycleCompany.PartModels.API.Boundary.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BicycleCompany.PartModels.API.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class ManufacturersController : Controller
    {
        [HttpGet]
        public IActionResult GetManufacturers()
        {
            var partModels = new List<ManufacturerDto>
            {
                GetManufacturer(),
                GetManufacturer(),
                GetManufacturer(),
                GetManufacturer(),
                GetManufacturer(),
                GetManufacturer()
            };
            return Ok(partModels);
        }

        private ManufacturerDto GetManufacturer()
        {
            return new ManufacturerDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.First(),
            };
        }
    }
}
