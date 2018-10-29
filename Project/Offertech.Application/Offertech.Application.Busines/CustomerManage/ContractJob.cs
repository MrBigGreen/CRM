
using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Busines.PublicInfoManage;
using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Util;
using Offertech.Util.Log;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Offertech.Application.Busines.CustomerManage
{

    public class ContractJob : IJob
    {
        private SmsLogBLL smsLogBLL = new SmsLogBLL();
        private CustomerBLL customerbll = new CustomerBLL();
        private UserBLL userBLL = new UserBLL();
        private Log _logger;
        /// <summary>
        /// 日志操作
        /// </summary>
        public Log Logger
        {
            get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        }
        public void Execute(IJobExecutionContext context)//必须实现IJob接口下的Execute方法
        {
            Logger.Info("执行合同定时任务。");
            ContractBLL contractbll = new ContractBLL();
            var list = contractbll.GetDeadlineList(null).ToList();
            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    Logger.Info("循环执行短信通知");
                    try
                    {
                        string phoneNum = string.Empty;
                        if (list[i].PhoneNumber != null && list[i].PhoneNumber > 0)
                        {
                            phoneNum = list[i].PhoneNumber.ToString();
                        }
                        else
                        {
                            var customer = customerbll.GetEntity(list[i].CustomerId);
                            if (customer != null)
                            {
                                var user = userBLL.GetEntity(customer.TraceUserId);
                                phoneNum = user.Mobile;
                            }
                        }
                        if (!ValidateUtil.IsValidMobile(list[i].PhoneNumber.ToString()))
                        {
                            string content = string.Format("您的客户：{0}，合同即将到期，合同编号：{1}，请尽快处理！", list[i].CustomerName, list[i].ContractCode);
                             smsLogBLL.SaveBySend(list[i].PhoneNumber.ToString(), content, "");
                          
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.ToString());
                    }
                }
            }
        }


    }
    /// <summary>
    /// 创建触发器trigge
    /// </summary>
    public class QuartzJobScheduler
    {

        public static void Start()
        {
            //0 15 10 * * ? *	每天上午10:15触发
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler(); //从工厂中获取一个调度器实例化
            scheduler.Start();   //开始调度器
            IJobDetail job = JobBuilder.Create<ContractJob>().Build();//创建一个作业
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithSimpleSchedule(t =>                
            //    t.WithIntervalInSeconds(5) //触发执行，5s一次
            //    .RepeatForever())          //重复执行
            //    .Build();
            //scheduler.ScheduleJob(job, trigger);       //把作业，触发器加入调度器。 
            
            
            //每天上午10:15触发
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule("0 15 10 ? * *")
                .WithIdentity("ContractJob", "group1")
                .StartNow()
                .Build();
            scheduler.ScheduleJob(job, trigger);       //把作业，触发器加入调度器。
        }
    }
}
