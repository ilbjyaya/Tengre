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
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-31 09:56
    /// 描 述：系统日志表
    /// </summary>
    public class Employ_NoteService : RepositoryFactory, IEmploy_NoteService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Employ_NoteEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList<Employ_NoteEntity>(pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Employ_NoteEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  o.*                                                         
                            FROM    Employ_Note o
                            WHERE   1 = 1");

            //用户名
            if (!queryParam["UserName"].IsEmpty())
            {
                strSql.Append(" AND o.UserName like @UserName");
                parameter.Add(DbParameters.CreateDbParameter("@UserName", '%' + queryParam["UserName"].ToString() + '%'));
            }

            //开始日期
            if (!queryParam["StartTime"].IsEmpty())
            {
                strSql.Append(" AND o.OperateTime >= @StartTime ");
                parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
            }

            //结束日期
            if (!queryParam["EndTime"].IsEmpty())
            {
                strSql.Append(" AND o.OperateTime <= @EndTime ");
                parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
            }

            strSql.Append(" ORDER BY o.OperateTime DESC");
            return this.BaseRepository().FindList<Employ_NoteEntity>(strSql.ToString(), parameter.ToArray());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Employ_NoteEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Employ_NoteEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Employ_Note_DetailEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<Employ_Note_DetailEntity>("select * from Employ_Note_Detail where NoteId='" + keyValue + "'");
        }

        /// 获取子表详细信息
        /// </summary> 
        /// <returns></returns>
        public IEnumerable<Employ_Note_DetailEntity> GetDetails()
        {
            return this.BaseRepository().FindList<Employ_Note_DetailEntity>("select * from Employ_Note_Detail");
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Employ_NoteEntity entity, Employ_Note_DetailEntity entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    entity.Modify(keyValue);
                    entity.OperateTime = DateTime.Now;
                    db.Update(entity);
                    //明细
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
                    //主表
                    entity.Create();
                    entryList.OperateTime = DateTime.Now;
                    db.Insert(entity);
                    ////明细
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
                    //主表
                    entity.Modify(keyValue);
                    entity.UserId = null;
                    entity.UserName = null;
                    entity.OperateTime = null;
                    db.Update(entity);
                    //明细
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
                    //主表
                    entity.NoteId = Guid.NewGuid().ToString();
                    entity.OperateTime = DateTime.Now;
                    db.Insert(entity);
                    ////明细
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
