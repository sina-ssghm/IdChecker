using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace AnalyzeId.Shared
{
    public static class ErrorHelper
    {
        //public static string ErrorMessage = "Operation Failed";
        public static string ToErrorString(this ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(s => s.Errors).ToList().Select(s => s.ErrorMessage).ToList().Aggregate((s1, s2) => s1 + " , " + s2);
        }
        //public static string ToErrorString(this IdentityResult result)
        //{
        //    if (result.Succeeded)
        //        return "";
        //    return result.Errors.Select(s => s.Description).Aggregate((s1, s2) => s1 + " , " + s2);
        //}

    }
}
