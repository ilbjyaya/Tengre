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
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-27 08:05
    /// 描 述：商机跟进记录
    /// </summary>
    public class ChanceDescriptionService : RepositoryFactory<Client_ChanceDescriptionEntity>, IChanceDescriptionService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Client_ChanceDescriptionEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Client_ChanceDescriptionEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取备注列表
        /// </summary>
        /// <param name="keyValue"></param> 
        /// <returns>返回列表</returns>
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
         
            //订单单号
            if (!keyValue.IsEmpty())
            {
                strSql.Append(" AND r.OrderId = @OrderId");
                parameter.Add(DbParameters.CreateDbParameter("@OrderId", keyValue.ToString() ));
            }          
            strSql.Append(" ORDER BY r.CreateDate DESC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
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
