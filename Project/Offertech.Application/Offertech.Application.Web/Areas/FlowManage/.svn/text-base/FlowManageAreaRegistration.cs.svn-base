using System.Web.Mvc;

namespace Offertech.Application.Web.Areas.FlowManage
{
    public class FlowManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FlowManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              this.AreaName + "_Default",
              this.AreaName + "/{controller}/{action}/{id}",
              new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
              new string[] { "Offertech.Application.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}
