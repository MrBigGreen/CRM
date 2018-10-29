
using Common;
using System.Web.Mvc;
namespace Models
{
    
    public class SupportFilterAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// 当Action中标注了[SupportFilter]的时候会执行
        /// </summary>
        /// <param name="filterContext">请求上下文</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Account account = AccountModel.GetCurrentAccount();
            if (account == null)
            {
                filterContext.HttpContext.Response.Write(" <script type='text/javascript'> window.top.location='/Login/index'; </script>");
                filterContext.Result = new EmptyResult();
                return;
            }

        }

    }
}
