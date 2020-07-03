using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using LeaRun.Util;
using System.Collections.Generic;
using System.Linq;
using System;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Code;
using System.Text;
using LeaRun.Data;
using System.Data.Common;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单管理
    /// </summary>
    public class OrderModelService : RepositoryFactory, IOrderModelService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.OrderDate >= startTime && t.OrderDate <= endTime);
            }
            //单据编号
            if (!queryParam["OrderCode"].IsEmpty())
            {
                string OrderCode = queryParam["OrderCode"].ToString();
                expression = expression.And(t => t.OrderCode.Contains(OrderCode));
            }
            //客户名称
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                expression = expression.And(t => t.CustomerName.Contains(CustomerName));
            }
            //销售人员
            if (!queryParam["SellerName"].IsEmpty())
            {
                string SellerName = queryParam["SellerName"].ToString();
                expression = expression.And(t => t.SellerName.Contains(SellerName));
            }
            //收款状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                expression = expression.And(t => t.PaymentState == PaymentState);
            }

            return this.BaseRepository().FindList(expression, pagination);
        }


        public IEnumerable<OrderModel> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();
            //SellerName 销售人
            //SellerName,销售门店
            //PaymentMode  销售类型：门店？人员
            strSql.Append(@"SELECT  o.OrderId ,
                                    o.OrderCode ,
                                    o.CreateDate ,
                                    o.OrderDate ,
                                    o.ReceivedAmount, 
                                    o.RealSize,
                                    o.SellerName, 
                                    o.SellerId,
                                    o.OrderStatusName, 
                                    o.CustomerName, 
                                    bo2.FullName  CompanyName 
                            FROM    Client_Order o     
                                    LEFT JOIN Client_Customer c ON c.CustomerId = o.CustomerId
                                    LEFT JOIN Base_User u ON u.UserId = o.SellerId                                 
                                    LEFT JOIN Base_Organize bo2 ON u.OrganizeId = bo2.OrganizeId
                            WHERE   1 = 1");

            // UserAccount，通用条件（订单编号 / 客户姓名 / 手机 / 小区名称）,订单开始日期，订单结束日期，门店，销售代表，完工状态

            //订单单号
            if (!queryParam["ParamCommon"].IsEmpty())
            {
                strSql.Append(" AND (o.OrderCode like @ParamCommon");
                //parameter.Add(DbParameters.CreateDbParameter("@OrderCode", '%' + queryParam["OrderCode"].ToString() + '%'));
                // }
                ////客户编号
                //if (!queryParam["CustomerCode"].IsEmpty())
                //{
                //    strSql.Append(" AND c.CustomerCode like @CustomerCode");
                //    parameter.Add(DbParameters.CreateDbParameter("@CustomerCode", '%' + queryParam["CustomerCode"].ToString() + '%'));
                //}

                ////客户名称
                //if (!queryParam["CustomerName"].IsEmpty())
                //{
                strSql.Append(" OR o.CustomerName like @ParamCommon");
                // parameter.Add(DbParameters.CreateDbParameter("@CustomerName", '%' + queryParam["CustomerName"].ToString() + '%'));
                //}

                //手机或电话
                //if (!queryParam["Mobile"].IsEmpty())
                //{
                strSql.Append(" OR o.ContecterTel like @ParamCommon");
                // parameter.Add(DbParameters.CreateDbParameter("@Mobile",  queryParam["Mobile"].ToString() ));
                //}

                ////小区名称
                //if (!queryParam["CompanyAddress"].IsEmpty())
                //{
                strSql.Append(" OR o.DistrictAddress like @ParamCommon OR o.OpAddress like @ParamCommon  )");
                parameter.Add(DbParameters.CreateDbParameter("@ParamCommon", '%' + queryParam["ParamCommon"].ToString() + '%'));
            }

            ////地址
            //if (!queryParam["CompanyAddress"].IsEmpty())
            //{
            //    strSql.Append(" AND c.CompanyAddress like @CompanyAddress");
            //    parameter.Add(DbParameters.CreateDbParameter("@CompanyAddress", '%' + queryParam["CompanyAddress"].ToString() + '%'));
            //}

            //完工状态，订单状态
            if (!queryParam["OrderStatusName"].IsEmpty())
            {

                //订单开始日期
                if (!queryParam["StartTime"].IsEmpty())
                {
                    if(queryParam["OrderStatusName"].ToString().Contains("全"))
                    {//查询下单日期和完工日期
                        strSql.Append(" AND ( o.OrderDate >= @StartTime ) ");
                    }
                    else if(queryParam["OrderStatusName"].ToString().Contains("已"))
                    {//查询完工日期
                        strSql.Append(" AND o.FinishDate >= @StartTime  AND o.OrderStatusId = '3' ");
                    }
                    else if (queryParam["OrderStatusName"].ToString().Contains("未"))
                    {//查询未完工日期
                        strSql.Append(" AND o.OrderDate >= @StartTime  AND (o.OrderStatusId = '1' or o.OrderStatusId is null) ");
                    }
                    else
                    {//查询下单日期
                        strSql.Append(" AND o.OrderDate >= @StartTime  ");
                    }
                   
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
                }

                //订单结束日期
                if (!queryParam["EndTime"].IsEmpty())
                {
                    if (queryParam["OrderStatusName"].ToString().Contains("全"))
                    {//查询下单日期和完工日期
                        strSql.Append(" AND ( o.OrderDate <= @EndTime ) ");
                    }
                    else if (queryParam["OrderStatusName"].ToString().Contains("已"))
                    {//查询完工日期
                        strSql.Append(" AND o.FinishDate <= @EndTime  AND o.OrderStatusId = '3'");
                    }
                    else if (queryParam["OrderStatusName"].ToString().Contains("未"))
                    {//查询未完工日期
                        strSql.Append(" AND o.OrderDate <= @EndTime AND (o.OrderStatusId = '1' or o.OrderStatusId is null) ");
                    }
                    else
                    {//查询下单日期
                        strSql.Append(" AND o.OrderDate <= @EndTime ");
                    }

                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
                }
            }
            else
            {
                //订单开始日期
                if (!queryParam["StartTime"].IsEmpty())
                { 
                       // strSql.Append(" AND ( o.OrderDate >= @StartTime or o.FinishDate >= @StartTime) ");
                    strSql.Append(" AND ( o.OrderDate >= @StartTime )    ");
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
                }

                //订单结束日期
                if (!queryParam["EndTime"].IsEmpty())
                { 
                     //   strSql.Append(" AND ( o.OrderDate <= @EndTime or o.FinishDate <= @EndTime) ");
                    strSql.Append(" AND ( o.OrderDate <= @EndTime ) ");
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
                }
            }
            //门店(公司名)
            if (!queryParam["CompanyName"].IsEmpty())
            {
                strSql.Append(" AND  bo2.FullName like @CompanyName");
                parameter.Add(DbParameters.CreateDbParameter("@CompanyName", '%' + queryParam["CompanyName"].ToString() + '%'));
            }

            //销售人员
            if (!queryParam["SellerName"].IsEmpty())
            {
                strSql.Append(" AND o.SellerName like @SellerName");
                parameter.Add(DbParameters.CreateDbParameter("@SellerName", '%' + queryParam["SellerName"].ToString() + '%'));
            }

            ////完工状态，订单状态
            //if (!queryParam["OrderStatusName"].IsEmpty())
            //{
            //    strSql.Append(" AND o.OrderStatusName like @OrderStatusName");
            //    parameter.Add(DbParameters.CreateDbParameter("@OrderStatusName", '%' + queryParam["OrderStatusName"].ToString() + '%'));
            //}

            //strSql.Append(" GROUP BY o.OrderId ,o.OrderCode ,  o.CreateDate ,                                                                           ");
            //strSql.Append("                         o.ReceivedAmount ,                                                         ");
            //strSql.Append("                         o.SellerName,                       ");
            //strSql.Append("                         o.OrderStatusName,                       ");
            //strSql.Append("                       o.SellerType ,                       ");
            //strSql.Append("                         case when isnull(o.SellerType,'')=''  then   bo2.FullName else bo.FullName end        ");

            strSql.Append(" ORDER BY o.CreateDate DESC");
            return this.BaseRepository().FindList<OrderModel>(strSql.ToString(), parameter.ToArray());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OrderEntity GetEntity(string keyValue)
        {
            OrderService orser = new OrderService();

            return orser.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取前单、后单 数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="type">类型（1-前单；2-后单）</param>
        /// <returns>返回实体</returns>
        public OrderEntity GetPrevOrNextEntity(string keyValue, int type)
        {
            OrderService orser = new OrderService();

            return orser.GetPrevOrNextEntity(keyValue, type);
        }


        public IEnumerable<Product_StyleModelEntity> GetStyleList_(string queryJson)
        {
            StringBuilder strSql = new StringBuilder();
            //SellerName 销售人
            //SellerName,销售门店
            //PaymentMode  销售类型：门店？人员
            strSql.Append(@"SELECT  o.ProductName ,
 o.ProductCode ,
                                    o.ItemName,
                                    o.StyleId,  
                                    isnull(c.SalePrice,0) Price
                            FROM    Product_Style o                                   
                                    LEFT JOIN Base_Price c ON c.ProductId = o.ProductCode and o.ItemValue=c.ItemDetailId
                           ");
            return this.BaseRepository().FindList<Product_StyleModelEntity>(strSql.ToString());
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
                db.Delete<OrderEntity>(keyValue);
                db.Delete<OrderEntryEntity>(t => t.OrderId.Equals(keyValue));
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
        /// <param name="orderEntity">实体对象</param>
        /// <param name="orderEntryList">明细实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, OrderEntity orderEntity, List<OrderEntryEntity> orderEntryList, List<Client_OrderDescriptionEntity> orderDescriptionList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    orderEntity.Modify(keyValue);
                    db.Update(orderEntity);
                    //明细
                    db.Delete<OrderEntryEntity>(t => t.OrderId.Equals(keyValue));
                    foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                    {
                        orderEntryEntity.OrderId = orderEntity.OrderId;
                        db.Insert(orderEntryEntity);
                    }

                    if (orderDescriptionList != null)
                    {
                        //备注明细
                        db.Delete<Client_OrderDescriptionEntity>(t => t.OrderId.Equals(keyValue));
                        foreach (Client_OrderDescriptionEntity orderDescriptionEntity in orderDescriptionList)
                        {
                            orderDescriptionEntity.OrderId = orderEntity.OrderId;
                            if (orderDescriptionEntity.DescriptionId == string.Empty) orderDescriptionEntity.DescriptionId = Guid.NewGuid().ToString();
                            //orderDescriptionEntity.CreateDate = DateTime.Now;
                            //orderDescriptionEntity.CreateUserId = OperatorProvider.Provider.Current().UserId;
                            //orderDescriptionEntity.CreateUserName = OperatorProvider.Provider.Current().UserName;

                            db.Insert(orderDescriptionEntity);
                        }
                    }
                }
                else
                {
                    //主表
                    orderEntity.Create();
                    db.Insert(orderEntity);
                    coderuleService.UseRuleSeed(orderEntity.CreateUserId, "", ((int)CodeRuleEnum.Customer_OrderCode).ToString(), db);//占用单据号
                    //明细
                    foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                    {
                        orderEntryEntity.Create();
                        orderEntryEntity.OrderId = orderEntity.OrderId;
                        db.Insert(orderEntryEntity);
                    }
                    if (orderDescriptionList != null)
                    {
                        //备注明细 
                        foreach (Client_OrderDescriptionEntity orderDescriptionEntity in orderDescriptionList)
                        {
                            orderDescriptionEntity.Create();
                            orderDescriptionEntity.OrderId = orderEntity.OrderId;
                            db.Insert(orderDescriptionEntity);
                        }
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
        #endregion
    }
}