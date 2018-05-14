using Dews.Api.Results.Base; 

namespace Dews.Api.Requests.News
{
    public class DeleteNewsRequest : OperationRequest
    {
        public int Id { get; set; }
    }
}
