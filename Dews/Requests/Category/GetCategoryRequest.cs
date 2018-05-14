using Dews.Api.Results.Base;
using Dews.DataAccess.Types;
using System;

namespace Dews.Api.Requests.News
{
    public class GetCategoryRequest : OperationRequest
    {
        public GetCategoryRequest()
        {
            RequestParams = new ListRequestParam();
        }
        public ListRequestParam RequestParams { get; set; }
         
    }
}
