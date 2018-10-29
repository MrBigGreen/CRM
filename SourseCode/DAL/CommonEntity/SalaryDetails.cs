using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.CommonEntity
{
    /// <summary>
    /// 薪资自定义实体
    /// </summary>
    public class SalaryDetailsEntity
    {
        #region  人员信息
        public string CustomerID { get; set; }

        public string CustomerName { get; set; }


        public string EmpID { get; set; }
        public string EmpName { get; set; }

        public short Gender { get; set; }
        public short JobStatus { get; set; }

        public string BankTypeID { get; set; }

        public string BankTypeName { get; set; }

        public string BankCity { get; set; }
        public string BankID { get; set; }

        public string IDType { get; set; }

        public string IDIDTypeText { get; set; }


        public string IDCard { get; set; }
        public string ContractSubject { get; set; }
        public string WagesSubject { get; set; }
        public string SocialSecuritySubject { get; set; }
        public string TaxSubject { get; set; }


        #endregion



        #region 薪资信息



        public string SalaryDetailsID { get; set; }
        public int SalaryMonth { get; set; }


         
        public Nullable<decimal> PayableSalary1 { get; set; }
        public Nullable<decimal> IndiPension1 { get; set; }
        public Nullable<decimal> IndiMedical1 { get; set; }
        public Nullable<decimal> IndiJobSecurity1 { get; set; }
        public Nullable<decimal> IndiSSTotal1 { get; set; }
        public Nullable<decimal> IndiFund1 { get; set; }
        public Nullable<decimal> Tax1 { get; set; }
        public Nullable<decimal> AnnualBonus1 { get; set; }
        public Nullable<decimal> AnnualBonusTax1 { get; set; }
        public Nullable<decimal> RealSalary1 { get; set; }
        public Nullable<decimal> CompanyPension1 { get; set; }
        public Nullable<decimal> CompanyMedical1 { get; set; }
        public Nullable<decimal> CompanyJobSecurity1 { get; set; }

        public Nullable<decimal> CompanyFund1 { get; set; }
        public Nullable<decimal> IndusInsurance1 { get; set; }
        public Nullable<decimal> BirthInsurance1 { get; set; }
        public Nullable<decimal> CompanySSTotal1 { get; set; }
        public string Remark1 { get; set; }
        public Nullable<decimal> PayableSalary2 { get; set; }
        public Nullable<decimal> TranSubsidies2 { get; set; }
        public Nullable<decimal> MealAllowance2 { get; set; }
        public Nullable<decimal> TravelAllowance2 { get; set; }
        public Nullable<decimal> BackPay2 { get; set; }
        public Nullable<decimal> PayableTotal2 { get; set; }
        public Nullable<decimal> IndiFund2 { get; set; }
        public Nullable<decimal> IndiSSTotal2 { get; set; }
        public Nullable<decimal> IndiPension2 { get; set; }
        public Nullable<decimal> IndiMedical2 { get; set; }
        public Nullable<decimal> IndiJobSecurity2 { get; set; }
        public Nullable<decimal> Other2 { get; set; }
        public Nullable<decimal> Tax2 { get; set; }
        public Nullable<decimal> TaxDeductions2 { get; set; }
        public Nullable<decimal> RealSalary2 { get; set; }
        public string PaymentMode2 { get; set; }
        public string Remark2 { get; set; }
        public Nullable<decimal> PayBaseBK3 { get; set; }
        public Nullable<decimal> PayBase3 { get; set; }
        public Nullable<decimal> CompanyPension3 { get; set; }
        public Nullable<decimal> CompanyMedical3 { get; set; }
        public Nullable<decimal> CompanyJobSecurity3 { get; set; }
        public Nullable<decimal> CompanyInjury3 { get; set; }
        public Nullable<decimal> CompanyBirth3 { get; set; }
        public Nullable<decimal> CompanySSTotal3 { get; set; }
        public Nullable<decimal> CompanyPensionBK3 { get; set; }
        public Nullable<decimal> CompanyMedicalBK3 { get; set; }
        public Nullable<decimal> CompanyJobSecurityBK3 { get; set; }
        public Nullable<decimal> IndusInsuranceBK3 { get; set; }
        public Nullable<decimal> BirthInsuranceBK3 { get; set; }
        public Nullable<decimal> CompanySSTotalBK3 { get; set; }
        public Nullable<decimal> IndiPension3 { get; set; }
        public Nullable<decimal> IndiMedical3 { get; set; }
        public Nullable<decimal> IndiJobSecurity3 { get; set; }
        public Nullable<decimal> IndiSubtotal3 { get; set; }
        public Nullable<decimal> IndiPensionBK3 { get; set; }
        public Nullable<decimal> IndiMedicalBK3 { get; set; }
        public Nullable<decimal> IndiJobSecurityBK3 { get; set; }
        public Nullable<decimal> IndiSubtotalBK3 { get; set; }
        public Nullable<decimal> Total3 { get; set; }
        public Nullable<int> SalaryMonthBK3 { get; set; }
        public Nullable<decimal> FundAccount3 { get; set; }
        public Nullable<decimal> IndiFund3 { get; set; }
        public Nullable<decimal> CompanyFund3 { get; set; }
        public Nullable<decimal> FundSubtotal3 { get; set; }
        public Nullable<decimal> PayAmount3 { get; set; }
        public Nullable<decimal> FundAccountBC3 { get; set; }
        public Nullable<decimal> IndiFundBC3 { get; set; }
        public Nullable<decimal> CompanyFundBC3 { get; set; }
        public Nullable<decimal> FundSubtotalBC3 { get; set; }
        public string Remark3 { get; set; }


        #endregion

    }
}
