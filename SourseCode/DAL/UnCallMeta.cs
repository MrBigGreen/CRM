using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    [MetadataType(typeof(UnCallMeta))]//使用UnCallMeta对UnCall进行数据验证
    public partial class UnCall : BaseEntity
    {
       
    }

    public class UnCallMeta
    {
        public UnCallMeta()
        { }
        private DateTime _calldate;
        private string _clid;
        private string _src;
        private string _dst;
        private string _dcontext;
        private string _channel;
        private string _dstchannel;
        private string _lastapp;
        private string _lastdata;
        private int _duration = 0;
        private int _billsec = 0;
        private string _disposition;
        private int _amaflags = 0;
        private string _accountcode;
        private string _uniqueid;
        private string _userfield;
        private int? _tag = 0;
        private int? _outup = 0;
        private string _calltype ;
        private DateTime? _addtime;
        private string _play;
        private string _outbound;
        //分机号
        public string Fcode { get; set; }
        //企业名称
        public string CompanyName { get; set; }
        //归属人
        public string FcodeUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime calldate
        {
            set { _calldate = value; }
            get { return _calldate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string clid
        {
            set { _clid = value; }
            get { return _clid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string src
        {
            set { _src = value; }
            get { return _src; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dst
        {
            set { _dst = value; }
            get { return _dst; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dcontext
        {
            set { _dcontext = value; }
            get { return _dcontext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string channel
        {
            set { _channel = value; }
            get { return _channel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dstchannel
        {
            set { _dstchannel = value; }
            get { return _dstchannel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastapp
        {
            set { _lastapp = value; }
            get { return _lastapp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastdata
        {
            set { _lastdata = value; }
            get { return _lastdata; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int duration
        {
            set { _duration = value; }
            get { return _duration; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int billsec
        {
            set { _billsec = value; }
            get { return _billsec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string disposition
        {
            set { _disposition = value; }
            get { return _disposition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int amaflags
        {
            set { _amaflags = value; }
            get { return _amaflags; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string accountcode
        {
            set { _accountcode = value; }
            get { return _accountcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string uniqueid
        {
            set { _uniqueid = value; }
            get { return _uniqueid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userfield
        {
            set { _userfield = value; }
            get { return _userfield; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? tag
        {
            set { _tag = value; }
            get { return _tag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? outup
        {
            set { _outup = value; }
            get { return _outup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string calltype
        {
            set { _calltype = value; }
            get { return _calltype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string play
        {
            set { _play = value; }
            get { return _play; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string outbound
        {
            set { _outbound = value; }
            get { return _outbound; }
        }
    }
}
