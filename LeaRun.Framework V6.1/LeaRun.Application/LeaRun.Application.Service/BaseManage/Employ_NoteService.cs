using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.BaseManage;
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

namespace LeaRun.Application.Service.BaseManage
{
    /// <summary>
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-07-31 09:56
    /// �� ����ϵͳ��־��
    /// </summary>
    public class Employ_NoteService : RepositoryFactory, IEmploy_NoteService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Employ_NoteEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList<Employ_NoteEntity>(pagination);
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Employ_NoteEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  o.*                                                         
                            FROM    Employ_Note o
                            WHERE   1 = 1");

            //�û���
            if (!queryParam["UserName"].IsEmpty())
            {
                strSql.Append(" AND o.UserName like @UserName");
                parameter.Add(DbParameters.CreateDbParameter("@UserName", '%' + queryParam["UserName"].ToString() + '%'));
            }

            //��ʼ����
            if (!queryParam["StartTime"].IsEmpty())
            {
                strSql.Append(" AND o.OperateTime >= @StartTime ");
                parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
            }

            //��������
            if (!queryParam["EndTime"].IsEmpty())
            {
                strSql.Append(" AND o.OperateTime <= @EndTime ");
                parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
            }

            strSql.Append(" ORDER BY o.OperateTime DESC");
            return this.BaseRepository().FindList<Employ_NoteEntity>(strSql.ToString(), parameter.ToArray());
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Employ_NoteEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Employ_NoteEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<Employ_Note_DetailEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<Employ_Note_DetailEntity>("select * from Employ_Note_Detail where NoteId='" + keyValue + "'");
        }

        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary> 
        /// <returns></returns>
        public IEnumerable<Employ_Note_DetailEntity> GetDetails()
        {
            return this.BaseRepository().FindList<Employ_Note_DetailEntity>("select * from Employ_Note_Detail");
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
                db.Delete<Employ_NoteEntity>(keyValue);
                db.Delete<Employ_Note_DetailEntity>(t => t.NoteId.Equals(keyValue));
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
        public void SaveForm(string keyValue, Employ_NoteEntity entity, Employ_Note_DetailEntity entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //����
                    entity.Modify(keyValue);
                    entity.OperateTime = DateTime.Now;
                    db.Update(entity);
                    //��ϸ
                    //db.Delete<Employ_Note_DetailEntity>(t => t.NoteId.Equals(keyValue));
                    //foreach (Employ_Note_DetailEntity item in entryList)
                    //{
                    if (entryList != null)
                    {
                        entryList.Create(); 
                        entryList.NoteId = entity.NoteId;
                        db.Insert(entryList);
                    }
                    //}
                }
                else
                {
                    //����
                    entity.Create();
                    entryList.OperateTime = DateTime.Now;
                    db.Insert(entity);
                    ////��ϸ
                    //foreach (Employ_Note_DetailEntity item in entryList)
                    //{
                    if (entryList != null)
                    {
                        entryList.Create();
                        entryList.OperateTime = DateTime.Now;
                        entryList.NoteId = entity.NoteId;
                        db.Insert(entryList);
                    }
                    //}
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        public void SaveFormApi(string keyValue, Employ_NoteEntity entity, Employ_Note_DetailEntity entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //����
                    entity.Modify(keyValue);
                    entity.UserId = null;
                    entity.UserName = null;
                    entity.OperateTime = null;
                    db.Update(entity);
                    //��ϸ
                    //db.Delete<Employ_Note_DetailEntity>(t => t.NoteId.Equals(keyValue));
                    //foreach (Employ_Note_DetailEntity item in entryList)
                    //{
                    if (entryList != null)
                    {
                        entryList.Create();
                        entryList.OperateTime = DateTime.Now;
                        entryList.NoteId = entity.NoteId;
                        db.Insert(entryList);
                    }
                    //}
                }
                else
                {
                    //����
                    entity.NoteId = Guid.NewGuid().ToString();
                    entity.OperateTime = DateTime.Now;
                    db.Insert(entity);
                    ////��ϸ
                    //foreach (Employ_Note_DetailEntity item in entryList)
                    //{
                    if (entryList != null)
                    {
                        entryList.Create();
                        entryList.OperateTime = DateTime.Now;
                        entryList.NoteId = entity.NoteId;
                        db.Insert(entryList);
                    }
                    //}
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
