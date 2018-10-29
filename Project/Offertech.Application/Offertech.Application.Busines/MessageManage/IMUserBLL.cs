using Offertech.Application.Entity.MessageManage;
using Offertech.Application.IService.MessageManage;
using Offertech.Application.Service.MessageManage;
using System.Collections.Generic;

namespace Offertech.Application.Busines.MessageManage
{
    /// <summary>
    /// 版 本 V6.1
    /// 苏州欧孚科技
    /// 创建人：陈彬彬
    /// 日 期：2015.11.25 16:12
    /// 描 述：用户管理
    /// </summary>
    public class IMUserBLL
    {
        private IMsgUserService service = new IMUserService();
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMUserModel> GetList(string OrganizeId)
        {
            return service.GetList(OrganizeId);
        }
    }
}
