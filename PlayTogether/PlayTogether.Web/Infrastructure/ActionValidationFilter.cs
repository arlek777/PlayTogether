using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Models;

namespace PlayTogether.Web.Infrastructure
{
    public class ActionValidationFilter: ActionFilterAttribute
    {
        private readonly ISimpleCRUDService _crudService;

        public ActionValidationFilter(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(ValidationResultMessages.InvalidModelState);
            }

            if (context.ActionArguments.ContainsKey("model"))
            {
                var model = context.ActionArguments["model"] as BaseModel;
                if (model != null)
                {
                    await CheckUser(model.UserId, context);
                }
            }
            else if (context.ActionArguments.ContainsKey("userId"))
            {
                await CheckUser(Guid.Parse(context.ActionArguments["userId"].ToString()), context);
            }

            await base.OnActionExecutionAsync(context, next);
        }

        private async Task CheckUser(Guid userId, ActionExecutingContext context)
        {
            var user = await _crudService.GetById<User>(userId);
            if (user == null)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
