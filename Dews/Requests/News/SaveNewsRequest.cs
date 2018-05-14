using Dews.Api.Results.Base;
using Dews.News.DTOs; 

namespace Dews.Api.Requests.News
{
    public class SaveNewsRequest : OperationRequest
    {
        public NewsDTO News { get; set; }
    }
}
