using Dews.Api.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.News
{
    public class UpdateNewsResult : OperationResult
    {
        public UpdateNewsResult() : base() { }
        public UpdateNewsResult(Exception ex) : base(ex) { }
    }
}
