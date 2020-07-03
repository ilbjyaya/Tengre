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
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-08-22 11:54
    /// 描 述：行政区域表
    /// </summary>
    public class Base_FlowService : RepositoryFactory<Base_FlowEntity>, Base_FlowIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Base_FlowEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList().OrderBy(t=>t.CreateDate).ThenBy(t=>t.FlowName);
        }

        public IEnumerable<Base_FlowEntity> GetListApi(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();
            //SellerName 销售人
            //SellerName,销售门店
            //PaymentMode  销售类型：门店？人员
            strSql.Append(@"SELECT  o.*  
                            FROM    Base_Flow o  
                            WHERE   1 = 1");

            //  开始日期，订单结束日期 

           

            //订单开始日期
            if (!queryParam["StartTime"].IsEmpty())
            {
                strSql.Append(" AND o.CreateDate >= @StartTime ");
                parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
            }

            //订单结束日期
            if (!queryParam["EndTime"].IsEmpty())
            {
                strSql.Append(" AND o.CreateDate <= @EndTime ");
                parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
            }

           
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        } 
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Base_FlowEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
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
