using BicycleCompany.PartModels.API.Boundary.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BicycleCompany.PartModels.API.Controllers
{
    [ApiController]
    [Route("api/admin/partModels")]
    public class PartModelsController : Controller
    {
        //[HttpGet]
        //public IActionResult GetPartModels()
        //{
        //    var partModels = new List<PartModelDto>
        //    {
        //        GetPartModel(),
        //        GetPartModel(),
        //        GetPartModel(),
        //        GetPartModel(),
        //        GetPartModel(),
        //        GetPartModel()
        //    };
        //    return Ok(partModels);
        //}

        //private PartModelDto GetPartModel()
        //{
        //    return new PartModelDto
        //    {
        //        Id = Guid.NewGuid(),
        //        AvailableQuantity = Faker.RandomNumber.Next(),
        //        Manufacturer = new ManufacturerDto
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = Faker.Name.First(),
        //        },
        //        Name = Faker.Name.First(),
        //        Part = new PartDto
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = Faker.Name.First(),
        //        },
        //        Price = Faker.RandomNumber.Next(),
        //    };
        //}
    }
}
