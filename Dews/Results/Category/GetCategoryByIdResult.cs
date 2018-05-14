using Dews.Api.Results.Base;
using Dews.News.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.News
{
    public class GetCategoryByIdResult : OperationResult
    {
        public CategoryDTO Item { get; set; }
        public GetCategoryByIdResult() : base() { }
        public GetCategoryByIdResult(Exception ex) : base(ex) { }
    }
}
