using LeaRun.Application.Code;
using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using LeaRun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Text;
using LeaRun.Data;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-14 09:47
    /// 描 述：客户信息
    /// </summary>
    public class CustomerService : RepositoryFactory<CustomerEntity>, ICustomerService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //客户编号
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":            //客户名称
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    case "Contact":             //联系人
                        expression = expression.And(t => t.Contact.Contains(keyword));
                        break;
                    case "TraceUserName":       //跟进人员
                        expression = expression.And(t => t.TraceUserName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CustomerEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">查询条件</param>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetEntityByCondation(string CustomerName, string Mobile)
        {
            //var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT  o.*  
                            FROM    Client_Customer o   
                            WHERE   1 = 1");


            //客户名称
            if (!CustomerName.IsEmpty())
            {
                strSql.Append(" and o.FullName = @CustomerName");
                parameter.Add(DbParameters.CreateDbParameter("@CustomerName", CustomerName.ToString()));
            }

            //手机或电话
            if (!Mobile.IsEmpty())
            {
                strSql.Append(" and (o.Mobile = @Mobile or o.Tel=@Mobile)");
                parameter.Add(DbParameters.CreateDbParameter("@Mobile", Mobile.ToString()));
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }

        #endregion

        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.CustomerId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
                db.Delete<CustomerEntity>(keyValue);
                db.Delete<TrailRecordEntity>(t => t.ObjectId.Equals(keyValue));
                db.Delete<CustomerContactEntity>(t => t.CustomerId.Equals(keyValue));
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
        public void SaveForm(string keyValue, CustomerEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    entity.Create();
                    //获得指定模块或者编号的单据号
                    entity.EnCode = coderuleService.SetBillCode(entity.CreateUserId, SystemInfo.CurrentModuleId, "", db);
                    db.Insert(entity);
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }

   public void SaveFormApi(string keyValue, CustomerEntity entity )
        {
            if (!string.IsNullOrEmpty(keyValue))
            { 
                entity.CustomerId = keyValue;
                entity.ModifyDate = DateTime.Now;
                entity.ModifyUserId = entity.CreateUserId;
                entity.ModifyUserName = entity.CreateUserName;
                this.BaseRepository().Update(entity);
            }
            else
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    entity.CustomerId = Guid.NewGuid().ToString();
                    entity.CreateUserId = Guid.NewGuid().ToString();
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserId = entity.CreateUserId;
                    entity.CreateUserName = entity.CreateUserName;
                    ////获得指定模块或者编号的单据号
                    entity.EnCode = coderuleService.SetBillCodeApi(entity.CreateUserId, "1d3797f6-5cd2-41bc-b769-27f2513d61a9", "");
                    db.Insert(entity);
                   db.Commit();
                }
                catch (Exception)
                {
                   db.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="moduleId">模块</param>
        public void SaveForm(string keyValue, CustomerEntity entity, string moduleId)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    entity.Create();
                    //获得指定模块或者编号的单据号
                    entity.EnCode = coderuleService.SetBillCode(entity.CreateUserId, moduleId, "", db);
                    db.Insert(entity);
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        #endregion
    }
}
