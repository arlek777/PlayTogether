using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace PlayTogether.WebClient.Infrastructure
{
    public static class ModelStateDictionaryExtensions
    {
        public static string GetFirstError(this ModelStateDictionary modelState)
        {
            var firstError = modelState.Select(ms => ms.Value.Errors.FirstOrDefault()?.ErrorMessage).FirstOrDefault();
            return firstError;
        }
    }
}