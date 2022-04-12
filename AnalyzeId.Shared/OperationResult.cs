using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Shared
{
    public class OperationResult<TData>
    {
        public static string SuccessMessage = "Successfully done.";
        public static string ErrorMessage = "Error occurred";
        public bool Succeed { get; set; }
        public TData Data { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }

        public static OperationResult<TData> Complete => new OperationResult<TData> { Succeed = true, Message = SuccessMessage };
        public static OperationResult<TData> Error(Exception ex) => new OperationResult<TData> { Succeed = false, Message = ex.InnerException != null ? ex.InnerException.Message : ErrorMessage };
        public static OperationResult<object> ModelStateError(ModelStateDictionary ModelState)
        {
            return new OperationResult<object> { Succeed = false, Message = ModelState.ToErrorString() };
        }
    }
}
