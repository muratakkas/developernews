using Dews.Api.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.News
{
    public class DeleteNewsResult : OperationResult
    {
        public DeleteNewsResult() : base() { }
        public DeleteNewsResult(Exception ex) : base(ex) { }
    }
}
