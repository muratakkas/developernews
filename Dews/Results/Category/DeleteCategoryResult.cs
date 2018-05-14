using Dews.Api.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.News
{
    public class DeleteCategoryResult : OperationResult
    {
        public DeleteCategoryResult() : base() { }
        public DeleteCategoryResult(Exception ex) : base(ex) { }
    }
}
