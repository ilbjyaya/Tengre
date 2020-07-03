using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-07-27 08:05
    /// �� �����̻�������¼
    /// </summary>
    public class ChanceDescriptionService : RepositoryFactory<Client_ChanceDescriptionEntity>, IChanceDescriptionService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Client_ChanceDescriptionEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Client_ChanceDescriptionEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// ��ȡ��ע�б�
        /// </summary>
        /// <param name="keyValue"></param> 
        /// <returns>�����б�</returns>
        public IEnumerable<Client_ChanceDescriptionEntity> GetList_(string keyValue)
        { 
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  r.OrderId ,
                                    r.DescriptionId ,
                                    r.DescriptionContent,                                                                     
                                    r.CreateDate ,
                                    r.CreateUserId,
                                    r.CreateUserName,
                                    d.FullName DepartmentName  
                            FROM    Client_ChanceDescription r
                                    LEFT JOIN Base_User o ON o.UserId = r.CreateUserId
                                    LEFT JOIN Base_Department d ON o.DepartmentId = d.DepartmentId
                            WHERE   1 = 1");
         
            //��������
            if (!keyValue.IsEmpty())
            {
                strSql.Append(" AND r.OrderId = @OrderId");
                parameter.Add(DbParameters.CreateDbParameter("@OrderId", keyValue.ToString() ));
            }          
            strSql.Append(" ORDER BY r.CreateDate DESC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Client_ChanceDescriptionEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
