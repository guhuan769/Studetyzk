using MediatR;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace UserMgr.WebAPI
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
            if (result.Exception != null)//只有Action执行成功，才自动调用SaveChanges
            {
                return;
            }

            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDesc == null)
            {
                return;
            }
            //拿到标注得Attribute
            var uowAttr = actionDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
            if (uowAttr == null)//如果没有标注就不进行处理 
            {
                return;
            }
            ///此处得DbContext都是通过DI来进行注入得
            foreach (var dbContext in uowAttr.DbContextTypes)
            {
                //如何得到DI注册得对象呢？ 向DI要DbContext对象
                var dbCtx = context.HttpContext.RequestServices.GetService(dbContext) as DbContext;
                if (dbCtx != null)
                {
                    await dbCtx.SaveChangesAsync();
                }
            }
        }
    }
}
