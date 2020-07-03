using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
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
    /// �� �ڣ�2018-08-22 11:54
    /// �� �������������
    /// </summary>
    public class Base_FlowService : RepositoryFactory<Base_FlowEntity>, Base_FlowIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Base_FlowEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList().OrderBy(t=>t.CreateDate).ThenBy(t=>t.FlowName);
        }

        public IEnumerable<Base_FlowEntity> GetListApi(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();
            //SellerName ������
            //SellerName,�����ŵ�
            //PaymentMode  �������ͣ��ŵꣿ��Ա
            strSql.Append(@"SELECT  o.*  
                            FROM    Base_Flow o  
                            WHERE   1 = 1");

            //  ��ʼ���ڣ������������� 

           

            //������ʼ����
            if (!queryParam["StartTime"].IsEmpty())
            {
                strSql.Append(" AND o.CreateDate >= @StartTime ");
                parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
            }

            //������������
            if (!queryParam["EndTime"].IsEmpty())
            {
                strSql.Append(" AND o.CreateDate <= @EndTime ");
                parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
            }

           
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        } 
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Base_FlowEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
        public void SaveForm(string keyValue, Base_FlowEntity entity)
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

        public void SaveFormApi(string keyValue, Base_FlowEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.FlowId = keyValue;
                entity.ModifyDate = DateTime.Now;
               
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.FlowId = Guid.NewGuid().ToString();
                entity.CreateDate = DateTime.Now;
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
