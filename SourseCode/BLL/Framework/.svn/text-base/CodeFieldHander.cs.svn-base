using Offer.DAL;
using Offer.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.BLL
{
    public class CodeFieldHander : IDisposable, ICodeFieldHander
    {
        protected OfferEntities db = new OfferEntities();

        /// <summary>
        /// 获取下拉框的数据
        /// </summary>
        /// <param name="codeCategory">类别名称</codeCategory>
        /// <returns></returns>
        public List<CodeTable> GetCodeField(string codeCategory)
        {

            return (from m in db.CodeTable
                    where m.CodeCategory == codeCategory
                    orderby m.CodeSeq
                    select m).ToList();

        }
        /// <summary>
        /// 根据主键id，获取数据字典的展示字段
        /// </summary>
        /// <param name="codeId">编码ID</param>
        /// <returns></returns>
        public string GetMyTextsById(string codeId)
        {
            return db.CodeTable.Where(s => s.CodeID == codeId).Select(s => s.CodeValue).FirstOrDefault();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
