using Dews.Api.Results.Base; 

namespace Dews.Api.Requests.News
{
    public class DeleteCategoryRequest : OperationRequest
    {
        public int Id { get; set; }
    }
}
