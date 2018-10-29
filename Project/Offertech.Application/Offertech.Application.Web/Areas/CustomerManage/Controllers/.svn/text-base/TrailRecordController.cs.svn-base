using Offertech.Application.Busines.CustomerManage;
using Offertech.Application.Busines.PublicInfoManage;
using Offertech.Application.Code;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Util;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offertech.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-32 16:51
    /// 描 述：跟进记录
    /// </summary>
    public class TrailRecordController : MvcControllerBase
    {
        private TrailRecordBLL chancetrailbll = new TrailRecordBLL();
        private FileInfoBLL fileInfoBLL = new FileInfoBLL();
        private CustomerBLL customerBLL = new CustomerBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 填写跟进记录表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 跟进计划页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult PlanList()
        {
            return View();
        }
        /// <summary>
        /// 填写跟进记录表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult Done()
        {
            return View();
        }
        /// <summary>
        /// 跟进历史
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult History()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取客户实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = chancetrailbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string objectId)
        {
            var data = chancetrailbll.GetLastList(objectId, 50);
            Dictionary<string, string> dictionaryDate = new Dictionary<string, string>();
            foreach (TrailRecordModel item in data)
            {
                string key = item.StartTime.ToDate().ToString("yyyy-MM-dd");
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
                if (item.StartTime.ToDate().ToString("yyyy-MM-dd") == currentTime)
                {
                    key = "今天";
                }
                if (!dictionaryDate.ContainsKey(key))
                {
                    dictionaryDate.Add(key, item.StartTime.ToDate().ToString("yyyy-MM-dd"));
                }
            }
            var jsonData = new
            {
                timeline = dictionaryDate,
                rows = data,
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取跟进计划列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPlanDataJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = chancetrailbll.GetPlanData(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取跟进历史
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetHistoryDataJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = chancetrailbll.GetHistoryData(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFile()
        {
            string folderId = "";
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //没有文件上传，直接返回
            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return HttpNotFound();
            }
            string FileEextension = Path.GetExtension(files[0].FileName);
            string UserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            string virtualPath = string.Empty;

            #region  保存文件
            //获取文件完整文件名(包含绝对路径)
            //文件存放路径格式：/Resource/ResourceFile/{userId}{data}/{guid}.{后缀名}
            string FilePath = Config.GetValue("FilePath");
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            string fileGuid = Guid.NewGuid().ToString();
            string uploadDate = DateTime.Now.ToString("yyyyMMdd");
            string userPath = string.Format("{0}/{1}/{2}{3}", userId, uploadDate, fileGuid, FileEextension);
            virtualPath = FilePath + userPath;
            string fullFileName = this.Server.MapPath(virtualPath);

            //创建文件夹
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            if (!System.IO.File.Exists(fullFileName))
            {
                //保存文件
                files[0].SaveAs(fullFileName);
            }
            #endregion
            #region 上传到文件服务器
            try
            {
                FtpClient ftpClient = new FtpClient(Config.GetValue("ImgFtpUrl"), FilePath,
                    Config.GetValue("ImgFtpUserName"), Config.GetValue("ImgFtpPassword"));

                //查看文件是否存在
                if (!ftpClient.DirectoryExist(userId))
                    ftpClient.MakeDir(userId);
                ftpClient.GotoDirectory(userId, false);
                //查看文件是否存在
                if (!ftpClient.DirectoryExist(uploadDate))
                    ftpClient.MakeDir(uploadDate);
                ftpClient.GotoDirectory(uploadDate, false);

                ftpClient.Upload(fullFileName);

                System.IO.File.Delete(fullFileName);
            }
            catch (Exception e)
            {

            }
            #endregion

            return Json(
               new
               {
                   type = 1,
                   message = "上传成功",
                   FilePath = virtualPath,
               });

        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, TrailRecordModel model)
        {

            TrailRecordEntity entity = new Entity.CustomerManage.TrailRecordEntity();
            entity.Contact = model.Contact;
            entity.StartTime = model.StartTime;
            entity.EndTime = model.EndTime;
            entity.FollowUpMode = model.FollowUpMode;
            entity.ObjectId = model.ObjectId;
            entity.ObjectName = model.ObjectName;
            entity.ObjectSort = model.ObjectSort;
            entity.SaleStageName = model.SaleStageName;
            entity.SaleStageId = model.SaleStageId;
            entity.TrackContent = model.TrackContent;
            entity.Description = model.Description;
            entity.TrailType = model.TrailType;//跟进类型
            entity.FilesPath = model.FilesPath;
            if (model.ObjectSort == 2)
            {
                entity.ObjectName = customerBLL.GetEntity(model.ObjectId).FullName;
            }
            chancetrailbll.SaveForm(keyValue, entity);
            //if (!string.IsNullOrWhiteSpace(model.FilesPath))
            //{
            //    //相关文件
            //    string[] files = model.FilesPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (var item in files)
            //    {
            //        var fileInfoEntity = fileInfoBLL.GetEntity(item);
            //        if (fileInfoEntity != null)
            //        {
            //            fileInfoEntity.ObjectId = entity.TrailId;
            //            fileInfoBLL.SaveForm(fileInfoEntity.FileId, fileInfoEntity);
            //        }
            //    }
            //}



            return Success("操作成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateForm(string keyValue, TrailRecordModel model)
        {

            TrailRecordEntity entity = new Entity.CustomerManage.TrailRecordEntity();
            entity.Contact = model.Contact;
            entity.StartTime = model.StartTime;
            entity.EndTime = model.EndTime;
            entity.FollowUpMode = model.FollowUpMode;
            entity.ObjectId = model.ObjectId;
            entity.ObjectName = model.ObjectName;
            entity.ObjectSort = model.ObjectSort;
            entity.SaleStageName = model.SaleStageName;
            entity.SaleStageId = model.SaleStageId;
            entity.TrackContent = model.TrackContent;
            entity.Description = model.Description;
            entity.TrailType = model.TrailType;//跟进类型
            entity.FilesPath = model.FilesPath;
            chancetrailbll.SaveForm(keyValue, entity);
            //if (!string.IsNullOrWhiteSpace(model.FilesPath))
            //{
            //    //相关文件
            //    string[] files = model.FilesPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (var item in files)
            //    {
            //        //文件信息写入数据库
            //        FileInfoEntity fileInfoEntity = new FileInfoEntity();
            //        fileInfoEntity.ObjectId = entity.TrailId;
            //        fileInfoEntity.FolderId = "-1";
            //        fileInfoEntity.FileName = Path.GetFileName(item); ;
            //        fileInfoEntity.FilePath = item;
            //        fileInfoEntity.FileSize = "99";
            //        fileInfoEntity.FileExtensions = Path.GetExtension(item);
            //        fileInfoEntity.DeleteMark = 1;
            //        fileInfoEntity.FileType = fileInfoEntity.FileExtensions.Replace(".", "");
            //        fileInfoBLL.SaveForm("", fileInfoEntity);

            //    }
            //}



            return Success("操作成功。");
        }
        #endregion
    }
}
