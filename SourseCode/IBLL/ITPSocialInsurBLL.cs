using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{
    public interface ITPSocialInsurBLL : IBaseDAL<TP_SocialInsur>
    {
        List<SocialInsurModel> GetSocialInsurData(int page, int rows, string order, string sort, string search, ref int total);
    }
}
