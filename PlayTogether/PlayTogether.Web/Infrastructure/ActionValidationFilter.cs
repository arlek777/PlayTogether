using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlayTogether.Web.Models;

namespace PlayTogether.Web.Infrastructure
{
    public class ActionValidationFilter: ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(ValidationResultMessages.InvalidModelState);
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
