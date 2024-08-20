using Core;
using Core.Extensions;
using Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Filters
{
    public class AuthFilter : ActionFilterAttribute, IAsyncActionFilter
    {
        private readonly IAccountService _accSvc;

        public AuthFilter(IAccountService accSvc)
        {
            _accSvc = accSvc;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller as Controller;
            var controllerDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerName = controllerDescriptor.ControllerName;

            var currentUser = context.HttpContext.User.GetUserClaims();
            var userMenuResult = await _accSvc.GetUserMenu(currentUser.RoleId);
            var userMenus = userMenuResult.Value;
            var userMenuGroup = userMenus.Where(m => m.Menus.Any(o => o.Controller == controllerName)).FirstOrDefault();
            var userMenuAll = userMenus.SelectMany(m => m.Menus);
            var userMenu = userMenuAll.Where(m => m.Controller == controllerName).FirstOrDefault();

            if (!userMenus.Any() || !userMenuAll.Where(m => m.Controller == controllerName).Any() || !userMenu.AllowView)
            {
                context.Result = new RedirectToActionResult("Logout", "Account", null);
            }

            controller.ViewData["UserMenus"] = userMenus;
            controller.ViewData["CurrentMenu"] = userMenu;
            controller.ViewData["GroupMenuName"] = userMenuGroup?.Name;
            controller.ViewData["MenuName"] = userMenu?.Name;
            controller.ViewData[ViewDataType.Controller] = controller.RouteData.Values["controller"]?.ToString();
            controller.ViewData[ViewDataType.Action] = controller.RouteData.Values["action"]?.ToString();

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
