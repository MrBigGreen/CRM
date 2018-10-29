using Senparc.Weixin;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using System.Collections.Generic;
using System.Linq;

namespace Offertech.Util.WeChat.SenparcTools
{
    public class DepartmentHelper : SenparcSdkBase
    {
        public static List<DepartmentList> GetAllDepartment()
        {
            var result = MailListApi.GetDepartmentList(GetToken());
            return result.department;
        }

        public static int CreateWxDepartment(string departmentName, int parentDepartmentId, int order)
        {
            var result = MailListApi.CreateDepartment(GetToken(), departmentName, parentDepartmentId, order);
            return result.id;
        }

        public static bool UpdateWxDepartment(int departmentId, string departmentName, int parentDepartmentId, int order)
        {
            var result = MailListApi.UpdateDepartment(GetToken(), departmentId.ToString(), departmentName, parentDepartmentId, order);
            return result.errcode == ReturnCode_QY.请求成功;
        }


        public static bool DeleteWxDepartment(int departmentId)
        {
            if (MailListApi.GetDepartmentList(GetToken(), 0).department.Any(m => m.id == departmentId))
            {
                var result = MailListApi.DeleteDepartment(GetToken(), departmentId.ToString());
                return result.errcode == ReturnCode_QY.请求成功;
            }
            else
            {
                return true; ;
            }
        }
    }
}
