using Dews.Api.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.News
{
    public class AddNewsResult : OperationResult
    {
        public AddNewsResult() : base() { }
        public AddNewsResult(Exception ex) : base(ex) { }
    }
}
