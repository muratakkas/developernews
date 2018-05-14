using Dews.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dews.Api.Results.Base
{
    public class OperationResult
    {
        public OperationResult()
        {
            OperationStatus = OperationStatus.SUCCESS;
        }

        public OperationResult(Exception ex)
        {
            OperationStatus = OperationStatus.FAILED;

            StatusMessage = ex != null ? ex.Message + (ex.InnerException != null ? ex.InnerException.Message : null) : null; 
        }
        public OperationStatus OperationStatus { get; set; }

        public string StatusMessage { get; set; }

        public string StackTrace { get; set; }
    }
}
