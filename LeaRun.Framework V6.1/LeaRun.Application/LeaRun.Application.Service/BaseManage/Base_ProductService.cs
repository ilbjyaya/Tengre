using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.BaseManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
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
    /// 日 期：2018-07-23 07:50
    /// 描 述：商品表
    /// </summary>
    public class Base_ProductService : RepositoryFactory<Base_ProductEntity>, Base_ProductIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_ProductEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Base_ProductEntity>();
            var queryParam = queryJson.ToJObject();

            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT  
o.ProductId ,
o.ProductCode ,
                                    o.ProductName ,
                                    b.ItemName Brand                                   
                            FROM    Base_Product o                                   
                                    LEFT JOIN (select b.* from Base_DataItemDetail b inner join Base_DataItem c on b.ItemId= c.ItemId and c.ItemCode ='Client_BrandOption' ) b ON b.ItemValue = o.Brand   
                            WHERE   1 = 1");

            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "ProductCode":
                        strSql.Append(" AND o.ProductCode like @ProductCode");
                        parameter.Add(DbParameters.CreateDbParameter("@ProductCode", '%' + keyword + '%'));
                        break;
                    case "ProductName":           //商品名
                        strSql.Append(" AND o.ProductName like @ProductName");
                        parameter.Add(DbParameters.CreateDbParameter("@ProductName", '%' + keyword + '%'));
                        break;
                    case "Brand":              //品牌
                        strSql.Append(" AND b.ItemValue like @ItemValue");
                        parameter.Add(DbParameters.CreateDbParameter("@ItemValue", '%' + keyword + '%'));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Base_ProductEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Base_ProductEntity>(); 
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT  
                                    o.ProductId ,
                                    o.ProductCode ,
                                    o.ProductName ,
                                    o.Brand BrandId ,    
                                    b.ItemName Brand                                   
                            FROM    Base_Product o                                   
                                    LEFT JOIN (select b.* from Base_DataItemDetail b inner join Base_DataItem c on b.ItemId= c.ItemId and c.ItemCode ='Client_BrandOption' ) b ON b.ItemValue = o.Brand   
                            WHERE   1 = 1");

            //查询条件
            if (queryJson != "")
            {
                var queryParam = queryJson.ToJObject();
                if (queryParam != null)
                {
                    if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                    {
                        string condition = queryParam["condition"].ToString();
                        string keyword = queryParam["keyword"].ToString();
                        switch (condition)
                        {
                            case "ProductCode":
                                strSql.Append(" AND o.ProductCode like @ProductCode");
                                parameter.Add(DbParameters.CreateDbParameter("@ProductCode", '%' + keyword + '%'));
                                break;
                            case "ProductName":           //商品名
                                strSql.Append(" AND o.ProductName like @ProductName");
                                parameter.Add(DbParameters.CreateDbParameter("@ProductName", '%' + keyword + '%'));
                                break;
                            case "Brand":              //品牌
                                strSql.Append(" AND b.ItemValue like @ItemValue");
                                parameter.Add(DbParameters.CreateDbParameter("@ItemValue", '%' + keyword + '%'));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());

           // return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Base_ProductEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Base_ProductEntity entity)
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
