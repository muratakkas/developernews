using Dews.Api.Results.Base;
using Dews.News.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.News
{
    public class GetNewsByIdResult : OperationResult
    {
        public NewsDTO Item { get; set; }
        public GetNewsByIdResult() : base() { }
        public GetNewsByIdResult(Exception ex) : base(ex) { }
    }
}
