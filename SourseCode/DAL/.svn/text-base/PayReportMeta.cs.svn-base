using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    public partial class PayReportMeta
    {
    }

    public partial class ComboxEntity
    {
        public string id { get; set; }
        public string text { get; set; }
        public List<ComboxEntity> children { get; set; }
    }

    public partial class PayReportEntity
    {
        public string ContractID { get; set; }

        //公司Name
        public string ContractName { get; set; }
        //客户数量
        public int? CustomerCounts { get; set; }
        //应付工资人数
        public int? PayPersonsCounts { get; set; }
        //应付工资金额
        public Decimal? PayPersonsMoneys { get; set; }
        //实付工资人数
        public int? RealPayPersonsCounts { get; set; }
        //实付工资金额
        public Decimal? RealPayPersonsMoneys { get; set; }
        //差异金额
        public Decimal? DifferenceAmount { get; set; }
        public string Remark { get; set; }
        //客户人数
        public int? CustomerNums { get; set; }
        //客户金额
        public Decimal? CustomerAmount { get; set; }
        //个人人数
        public int? PersonsNums { get; set; }
        //个人金额
        public Decimal? PersonsAmount { get; set; }
        //实际人数
        public int? RealPersonsNums { get; set; }
        //实际个人金额
        public Decimal? RealPersonsAmount { get; set; }
        //实际公司金额
        public Decimal? RealCompanyAmount { get; set; }
        //实际总和
        public Decimal? RealTotal { get; set; }
        //结算扣缴差异额
        public Decimal? SetDifferenceAmount { get; set; }


        public string BankName { get; set; }
        public int? BankPayPersons { get; set; }
        public Decimal? BankPayAmount { get; set; }
        public int? CustomerPayPersonsNums { get; set; }
        public Decimal? CustomerPayPersonsAmount { get; set; }
        public int? PayPersonsSocialSecurityNums { get; set; }
        public Decimal? PayPersonsSocialSecurityAmount { get; set; }
        public int? PayPersonsFundsNums { get; set; }
        public Decimal? PayPersonsFundsAmount { get; set; }
        public int? PayPersonsTaxNums { get; set; }
        public Decimal? PayPersonsTaxAmount { get; set; }

        public Decimal? CompanyAcceptAmount { get; set; }
        public Decimal? PersonAcceptAmount { get; set; }
        public Decimal? AcceptAmountTotal { get; set; }

    }
}
