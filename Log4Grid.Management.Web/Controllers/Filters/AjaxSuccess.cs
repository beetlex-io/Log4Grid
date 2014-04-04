using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Log4Grid.Management.Web.Controllers.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AjaxSuccess : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);

        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ContentResult result = new ContentResult();
            result.Content = "success";
            if (filterContext.Exception != null)
            {
                result.Content = filterContext.Exception.Message;
                filterContext.ExceptionHandled = true;
            }

            filterContext.Result = result;
            base.OnActionExecuted(filterContext);

        }
    }

}