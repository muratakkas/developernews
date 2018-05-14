using Dews.Api.Results.Base;
using Dews.News.DTOs; 

namespace Dews.Api.Requests.News
{
    public class SaveCategoryRequest : OperationRequest
    {
        public CategoryDTO Category { get; set; }
    }
}
