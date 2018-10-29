using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Entity.BaseManage;
using Offertech.Application.Entity.WeChatManage;
using Offertech.Application.IService.WeChatManage;
using Offertech.Application.Service.WeChatManage;
using Offertech.Util.Extension;
using Offertech.Util.WeChat.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Offertech.Application.Busines.WeChatManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号成员
    /// </summary>
    public class WeChatUserBLL
    {
        private IWeChatUserService service = new WeChatUserService();
        private WeChatOrganizeBLL weChatOrganizeBLL = new WeChatOrganizeBLL();
        private UserBLL userBLL = new UserBLL();

        #region 获取数据
        /// <summary>
        /// 成员列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WeChatUserRelationEntity> GetList()
        {
            return service.GetList();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存成员（并自动添加企业号成员）
        /// </summary>
        /// <param name="userIds">成员Id</param>
        /// <returns></returns>
        public void SaveMember(string[] userIds, out string msg)
        {
            List<UserEntity> usreList = userBLL.GetList().ToList();
            List<WeChatDeptRelationEntity> departmentList = weChatOrganizeBLL.GetList().ToList();
            int succeed = 0;
            int error = 0;
            foreach (var userId in userIds)
            {
                try
                {
                    UserEntity userEntity = usreList.Find(t => t.UserId == userId);
                    WeChatDeptRelationEntity weChatDeptRelationEntity = departmentList.Find(t => t.DeptId == userEntity.DepartmentId);


                    UserCreate userCreate = new UserCreate();
                    userCreate.userid = userEntity.Account;
                    userCreate.name = userEntity.RealName;
                    userCreate.position = userEntity.PostName;
                    userCreate.mobile = userEntity.Mobile;
                    userCreate.gender = userEntity.Gender == 1 ? "1" : "2";
                    userCreate.email = userEntity.Email;
                    userCreate.weixinid = userEntity.WeChat;
                    string departmentId = weChatDeptRelationEntity.WeChatDeptId.ToString();
                    userCreate.department = new List<string>() { departmentId };
                    var result = userCreate.Send();
                    if (result.errcode == 0)
                    {
                        UserInvite userInvite = new UserInvite();
                        userInvite.userid = userCreate.userid;
                        result = userInvite.Send();
                    }

                    WeChatUserRelationEntity weChatUserRelationEntity = new WeChatUserRelationEntity();
                    weChatUserRelationEntity.UserRelationId = userCreate.userid;
                    weChatUserRelationEntity.UserId = userCreate.userid;
                    weChatUserRelationEntity.DeptId = weChatDeptRelationEntity.DeptId;
                    weChatUserRelationEntity.DeptName = weChatDeptRelationEntity.DeptName;
                    weChatUserRelationEntity.WeChatDeptId = departmentId.ToInt();
                    weChatUserRelationEntity.SyncState = result.errcode.ToString();
                    weChatUserRelationEntity.SyncLog = result.errmsg;
                    service.SaveForm("", weChatUserRelationEntity);

                    succeed++;
                }
                catch (System.Exception)
                {
                    error++;
                }
            }
            msg = "成功：" + succeed + " ;错误：" + error;
        }
        /// <summary>
        /// 删除成员（并自动删除企业号成员）
        /// </summary>
        /// <param name="userId">成员Id</param>
        /// <returns></returns>
        public void DeleteMember(string userId)
        {
            try
            {
                UserDelete userDelete = new UserDelete();
                userDelete.userid = userId;
                var result = userDelete.Send();
                if (result.errcode == 0)
                {
                    service.RemoveForm(userId);
                }
                else
                {
                    WeChatUserRelationEntity weChatUserRelationEntity = new WeChatUserRelationEntity();
                    weChatUserRelationEntity.SyncState = result.errcode.ToString();
                    weChatUserRelationEntity.SyncLog = result.errmsg;
                    service.SaveForm(userId, weChatUserRelationEntity);
                    throw new Exception(result.errmsg);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 同步成员
        /// </summary>
        /// <param name="userIds">成员Id</param>
        /// <returns></returns>
        public void Synchronization(string[] userIds)
        {
            List<UserEntity> usreList = userBLL.GetList().ToList();
            List<WeChatDeptRelationEntity> departmentList = weChatOrganizeBLL.GetList().ToList();
            foreach (var userId in userIds)
            {
                UserEntity userEntity = usreList.Find(t => t.UserId == userId);
                WeChatDeptRelationEntity weChatDeptRelationEntity = departmentList.Find(t => t.DeptId == userEntity.DepartmentId);

                #region 同步更新信息
                UserUpdate userUpdate = new UserUpdate();
                userUpdate.userid = userEntity.Account;
                userUpdate.name = userEntity.RealName;
                userUpdate.position = userEntity.PostName;
                userUpdate.mobile = userEntity.Mobile;
                userUpdate.gender = userEntity.Gender == 1 ? "1" : "2";
                userUpdate.email = userEntity.Email;
                userUpdate.weixinid = userEntity.WeChat;
                string departmentId = weChatDeptRelationEntity.WeChatDeptId.ToString();
                userUpdate.department = new List<string>() { departmentId };
                userUpdate.enable = userEntity.EnabledMark.ToInt();
                var result = userUpdate.Send();

                #endregion

                #region 同步邀请关注
                UserGet userGet = new UserGet();
                userGet.userid = userEntity.Account;
                var status = userGet.Send();
                #endregion

                WeChatUserRelationEntity weChatUserRelationEntity = new WeChatUserRelationEntity();
                weChatUserRelationEntity.UserId = userUpdate.userid;
                weChatUserRelationEntity.DeptId = weChatDeptRelationEntity.DeptId;
                weChatUserRelationEntity.DeptName = weChatDeptRelationEntity.DeptName;
                weChatUserRelationEntity.WeChatDeptId = departmentId.ToInt();
                weChatUserRelationEntity.SyncState = status.status.ToString();
                weChatUserRelationEntity.SyncLog = status.errmsg;
                service.SaveForm(userEntity.Account, weChatUserRelationEntity);
            }
        }
        #endregion
    }
}
