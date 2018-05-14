using Dews.Api.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.News
{
    public class UpdateCategoryResult : OperationResult
    {
        public UpdateCategoryResult() : base() { }
        public UpdateCategoryResult(Exception ex) : base(ex) { }
    }
}
