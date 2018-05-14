using Dews.Api.Results.Base;
using Dews.DataAccess.Types;
using System;

namespace Dews.Api.Requests.News
{
    public class GetNewsRequest : OperationRequest
    {
        public ListRequestParam RequestParams { get; set; }

        public override void ValidateRequest()
        {
            base.ValidateRequest(); 
            if (RequestParams == null) throw new ArgumentNullException(nameof(RequestParams)); 
        }
    }
}
