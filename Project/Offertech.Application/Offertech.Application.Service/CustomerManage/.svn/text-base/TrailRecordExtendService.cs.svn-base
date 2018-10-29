using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Data;
using Offertech.Data.Repository;
using Offertech.Util;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offertech.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-21 16:10
    /// 描 述：跟进记录
    /// </summary>
    public class TrailRecordExtendService : RepositoryFactory, ITrailRecordExtendService
    {
        #region

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns>返回列表</returns>
        public IEnumerable<TrailRecordModel> GetLastList(string objectId, int rows)
        {
            string sql = "SELECT TOP " + rows + " * FROM dbo.Client_TrailRecord WHERE ObjectSort=2 AND ObjectId=@ObjectId ORDER BY StartTime DESC ";
            DbParameter[] dbParameter = { DbParameters.CreateDbParameter("@ObjectId", objectId) };
            var list = this.BaseRepository().FindList<TrailRecordModel>(sql, dbParameter);
            //if (list != null)
            //{
            //    //图片集合
            //    foreach (var item in list)
            //    {
            //        try
            //        {
            //            DataTable fileList = this.BaseRepository().FindTable("SELECT FilePath FROM  dbo.Base_FileInfo   WHERE ObjectId='" + item.TrailId + "' AND DeleteMark=0 ORDER BY CreateDate");
            //            if (fileList != null && fileList.Rows.Count > 0)
            //            {
            //                item.FilePathList = new List<string>();
            //                for (int i = 0; i < fileList.Rows.Count; i++)
            //                {
            //                    item.FilePathList.Add(fileList.Rows[i]["FilePath"].ToString());
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //}

            return list;
        }
        /// <summary>
        /// 获取跟进记录
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">条件</param>
        /// <returns></returns>
        public IEnumerable<TrailRecordModel> GetPageList(string objectId, Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<TrailRecordModel>();
            var queryParam = queryJson.ToJObject();
            expression = expression.And(t => t.ObjectId == objectId && t.DeleteMark == 0 && t.EnabledMark > 0);
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "TrackContent":              //跟进内容
                        expression = expression.And(t => t.TrackContent.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            var list = this.BaseRepository().FindList<TrailRecordModel>(expression, pagination);
            //if (list != null)
            //{
            //    //图片集合
            //    foreach (var item in list)
            //    {
            //        try
            //        {
            //            DataTable fileList = this.BaseRepository().FindTable("SELECT FilePath FROM  dbo.Base_FileInfo   WHERE ObjectId='" + item.TrailId + "' AND DeleteMark=0 ORDER BY CreateDate");
            //            if (fileList != null && fileList.Rows.Count > 0)
            //            {
            //                item.FilePathList = new List<string>();
            //                for (int i = 0; i < fileList.Rows.Count; i++)
            //                {
            //                    item.FilePathList.Add(fileList.Rows[i]["FilePath"].ToString());
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //}
            return list;
        }
        #endregion
    }
}
