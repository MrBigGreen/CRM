using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Data.Repository;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Offertech.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-03-09 14:45
    /// �� ������Ŀ��
    /// </summary>
    public class ProjectService : RepositoryFactory, IProjectIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<ProjectEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList<ProjectEntity>(pagination);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ProjectEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<ProjectEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<Client_ProjectEvalDetailsEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<Client_ProjectEvalDetailsEntity>("select * from Client_ProjectEvalDetails where ProjectEvalDetailsId='" + keyValue + "'");
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<ProjectEntity>(keyValue);
                db.Delete<Client_ProjectEvalDetailsEntity>(t => t.ProjectId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ProjectEntity entity, List<Client_ProjectEvalDetailsEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //����
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //��ϸ
                    db.Delete<Client_ProjectEvalDetailsEntity>(t => t.ProjectEvalDetailsId.Equals(keyValue));
                    foreach (Client_ProjectEvalDetailsEntity item in entryList)
                    {
                        item.Create();
                        item.ProjectEvalDetailsId = entity.ProjectId;
                        db.Insert(item);
                    }
                }
                else
                {
                    //����
                    entity.Create();
                    db.Insert(entity);
                    //��ϸ
                    foreach (Client_ProjectEvalDetailsEntity item in entryList)
                    {
                        item.Create();
                        item.ProjectEvalDetailsId = entity.ProjectId;
                        db.Insert(item);
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ProjectEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //����
                    entity.Modify(keyValue);
                    db.Update(entity);
                }
                else
                {
                    //����
                    entity.Create();
                    db.Insert(entity);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
