using Dews.Api.Results.Base;
using Dews.News.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.News
{
    public class GetNewsResult : OperationResult
    {
        public IEnumerable<NewsDTO> Items { get; set; }
        public GetNewsResult() : base() { }
        public GetNewsResult(Exception ex) : base(ex) { }
    }
}
