using Dews.Api.Results.Base;
using Dews.News.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.News
{
    public class GetCategoryResult : OperationResult
    {
        public IEnumerable<CategoryDTO> Items { get; set; }
        public GetCategoryResult() : base() { }
        public GetCategoryResult(Exception ex) : base(ex) { }
    }
}
