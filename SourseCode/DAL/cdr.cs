//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRM.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class cdr
    {
        public System.DateTime calldate { get; set; }
        public string clid { get; set; }
        public string src { get; set; }
        public string dst { get; set; }
        public string dcontext { get; set; }
        public string channel { get; set; }
        public string dstchannel { get; set; }
        public string lastapp { get; set; }
        public string lastdata { get; set; }
        public int duration { get; set; }
        public int billsec { get; set; }
        public string disposition { get; set; }
        public int amaflags { get; set; }
        public string accountcode { get; set; }
        public string uniqueid { get; set; }
        public string userfield { get; set; }
        public Nullable<int> tag { get; set; }
        public Nullable<int> outup { get; set; }
        public string calltype { get; set; }
        public Nullable<System.DateTime> addtime { get; set; }
        public string play { get; set; }
        public string outbound { get; set; }
    }
}