using System.Net;

namespace BicycleCompany.PartModels.API.Boundary.Responses
{
    public class AddedResponse : BaseResponseModel
    {
        public Guid Id { get; set; }
        public AddedResponse(Guid id)
        {
            Id = id;
            StatusCode = HttpStatusCode.Created;
        }
    }
}
