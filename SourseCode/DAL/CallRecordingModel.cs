using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    /// <summary>
    /// Name:通话记录实体
    /// Author：Jonny
    /// Date:2015-10-27
    /// </summary>
    public class CallRecordingModel
    {
        public DateTime CallDate { get; set; }
        public string Clid { get; set; }
        public string Src { get; set; }
        public string Dst { get; set; }
        public string Dcontext { get; set; }
        public string Channel { get; set; }
        public string DstChannel { get; set; }
        public string LastApp { get; set; }
        public string LastData { get; set; }
        public int Duration { get; set; }
        public int Billsec { get; set; }
        public string Disposition { get; set; }
        public int AmaFlags { get; set; }
        public string AccountCode { get; set; }
        public string Uniqueid { get; set; }
        public string UserField { get; set; }
        public int? Tag { get; set; }
        public int? OutUp { get; set; }
        public string CallType { get; set; }
        public DateTime? Addtime { get; set; }
        public string Play { get; set; }
        public string OutBound { get; set; }
        public string CompanyName { get; set; }
        public string FcodeUserName { get; set; }
        public string CallTime { get; set; }
        public string LeaderRemark { get; set; }

        public int Dial { get; set; }
        public int Connect { get; set; }
        public int Valid { get; set; }

        public string Efficiency { get; set; }

        public double CallTotal { get; set; }
        public string CallTotals { get; set; }
    }
}
