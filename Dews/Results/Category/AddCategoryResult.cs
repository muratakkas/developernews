using Dews.Api.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.News
{
    public class AddCategoryResult : OperationResult
    {
        public AddCategoryResult() : base() { }
        public AddCategoryResult(Exception ex) : base(ex) { }
    }
}
