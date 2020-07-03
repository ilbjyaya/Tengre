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
using LeaRun.Application.Entity.SystemManage;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单管理
    /// </summary>
    public class OrderService : RepositoryFactory<OrderEntity>, IOrderService
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


        public IEnumerable<OrderEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();
            //SellerName 销售人
            //SellerName,销售门店
            //PaymentMode  销售类型：门店？人员
            strSql.Append(@"SELECT  o.OrderCode ,
                                    o.Description,
                                    case when bo.FullName is null then u.RealName else  bo.FullName end as  SellerName,
                                    case when bo2.FullName is null then bo.FullName else  bo2.FullName end as  CompanyName,
                                    case when bo.FullName is null then '销售代表' else  '销售门店' end as  PaymentMode                                    
                            FROM    Client_Order o                                   
                                    LEFT JOIN Client_Customer c ON c.CustomerId = o.CustomerId
                                    LEFT JOIN Base_User u ON u.UserId = o.SellerId
                                    LEFT JOIN Base_Organize bo ON bo.OrganizeId = o.SellerId
                                    LEFT JOIN Base_Organize bo2 ON u.OrganizeId = bo2.OrganizeId
                            WHERE   1 = 1");

            // UserAccount，通用条件（订单编号 / 客户姓名 / 手机 / 小区名称）,订单开始日期，订单结束日期，门店，销售代表，完工状态

            //订单单号
            if (!queryParam["OrderCode"].IsEmpty())
            {
                strSql.Append(" AND o.OrderCode like @OrderCode");
                parameter.Add(DbParameters.CreateDbParameter("@OrderCode", '%' + queryParam["OrderCode"].ToString() + '%'));
            }
            //客户编号
            if (!queryParam["CustomerCode"].IsEmpty())
            {
                strSql.Append(" AND c.CustomerCode like @CustomerCode");
                parameter.Add(DbParameters.CreateDbParameter("@CustomerCode", '%' + queryParam["CustomerCode"].ToString() + '%'));
            }

            //客户名称
            if (!queryParam["CustomerName"].IsEmpty())
            {
                strSql.Append(" AND c.FullName like @CustomerName");
                parameter.Add(DbParameters.CreateDbParameter("@CustomerName", '%' + queryParam["CustomerName"].ToString() + '%'));
            }

            //手机或电话
            if (!queryParam["Mobile"].IsEmpty())
            {
                strSql.Append(" AND c.Mobile = @Mobile");
                parameter.Add(DbParameters.CreateDbParameter("@Mobile", queryParam["Mobile"].ToString()));
            }

            //地址
            if (!queryParam["CompanyAddress"].IsEmpty())
            {
                strSql.Append(" AND c.CompanyAddress like @CompanyAddress");
                parameter.Add(DbParameters.CreateDbParameter("@CompanyAddress", '%' + queryParam["CompanyAddress"].ToString() + '%'));
            }


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

            //门店(公司名)
            if (!queryParam["CompanyName"].IsEmpty())
            {
                strSql.Append(" AND (bo2.FullName like @CompanyName or bo.FullName like @CompanyName）");
                parameter.Add(DbParameters.CreateDbParameter("@CompanyName", '%' + queryParam["CompanyName"].ToString() + '%'));
            }

            //销售人员
            if (!queryParam["SellerName"].IsEmpty())
            {
                strSql.Append(" AND o.SellerName like @SellerName ");
                parameter.Add(DbParameters.CreateDbParameter("@SellerName", '%' + queryParam["SellerName"].ToString() + '%'));
            }

            //完工状态，订单状态
            if (!queryParam["OrderStatusName"].IsEmpty())
            {
                strSql.Append(" AND o.OrderStatusName like @OrderStatusName");
                parameter.Add(DbParameters.CreateDbParameter("@OrderStatusName", '%' + queryParam["OrderStatusName"].ToString() + '%'));
            }

            strSql.Append(" ORDER BY o.CreateDate DESC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取前单、后单 数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="type">类型（1-前单；2-后单）</param>
        /// <returns>返回实体</returns>
        public OrderEntity GetPrevOrNextEntity(string keyValue, int type)
        {
            OrderEntity entity = this.GetEntity(keyValue);
            if (type == 1)
            {
                entity = this.BaseRepository().IQueryable().Where(t => t.CreateDate > entity.CreateDate).OrderBy(t => t.CreateDate).FirstOrDefault();
            }
            else if (type == 2)
            {
                entity = this.BaseRepository().IQueryable().Where(t => t.CreateDate < entity.CreateDate).OrderByDescending(t => t.CreateDate).FirstOrDefault();
            }
            return entity;
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
                db.Delete<ReceivableEntity>(t => t.OrderId.Equals(keyValue));
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
                decimal realsize = 0;
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    orderEntity.Modify(keyValue);
                    //明细
                    db.Delete<OrderEntryEntity>(t => t.OrderId.Equals(keyValue));
                    foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                    {
                        realsize = realsize + Convert.ToDecimal(orderEntryEntity.RealSize == null ? 0 : orderEntryEntity.RealSize);
                        orderEntryEntity.Create();
                        orderEntryEntity.OrderId = orderEntity.OrderId;
                        db.Insert(orderEntryEntity);
                    }
                    orderEntity.RealSize = realsize;//施工面积



                    if (orderDescriptionList != null)
                    {
                        //    //备注明细
                        //    db.Delete<Client_OrderDescriptionEntity>(t => t.OrderId.Equals(keyValue));
                        foreach (Client_OrderDescriptionEntity orderDescriptionEntity in orderDescriptionList)
                        {
                            if (GetEntity(keyValue).Description != orderDescriptionEntity.DescriptionContent && !string.IsNullOrEmpty(orderDescriptionEntity.DescriptionContent))
                            {//备注和之前不同时，插入备注列表
                                orderDescriptionEntity.Create();
                                orderDescriptionEntity.OrderId = orderEntity.OrderId;
                                if (orderDescriptionEntity.DescriptionId == string.Empty) orderDescriptionEntity.DescriptionId = Guid.NewGuid().ToString();
                                //orderDescriptionEntity.CreateDate = DateTime.Now;
                                //orderDescriptionEntity.CreateUserId = OperatorProvider.Provider.Current().UserId;
                                //orderDescriptionEntity.CreateUserName = OperatorProvider.Provider.Current().UserName;

                                db.Insert(orderDescriptionEntity);
                            }
                        }
                    }
                    db.Update(orderEntity);

                }
                else
                {
                    //主表
                    orderEntity.Create();
                    coderuleService.UseRuleSeed(orderEntity.CreateUserId, "", ((int)CodeRuleEnum.Customer_OrderCode).ToString(), db);//占用单据号
                    //明细
                    foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                    {
                        realsize = realsize + Convert.ToDecimal(orderEntryEntity.RealSize == null ? 0 : orderEntryEntity.RealSize);
                        orderEntryEntity.Create();
                        orderEntryEntity.OrderId = orderEntity.OrderId;
                        db.Insert(orderEntryEntity);
                    }
                    orderEntity.RealSize = realsize; //施工面积
                    db.Insert(orderEntity);

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

        public void SaveFormApi(string keyValue, OrderEntity orderEntity, List<OrderEntryEntity> orderEntryList, List<ReceivableEntity> receivableentityList, string userid, string username)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {


                decimal realsize = 0;
                if (!string.IsNullOrEmpty(orderEntity.PhotoPath))
                {
                    if (orderEntity.PhotoPath.ToString().Substring(orderEntity.PhotoPath.Length - 1, 1) == ",")
                    {
                        orderEntity.PhotoPath = orderEntity.PhotoPath.Substring(0, orderEntity.PhotoPath.Length - 1);
                    }
                }
                if (!string.IsNullOrEmpty(keyValue))
                {

                    decimal recA = 0;
                    ////主表
                    // orderEntity.Modify(keyValue); 
                    orderEntity.OrderId = keyValue;
                    if (receivableentityList != null)
                    {
                        //收款
                        db.Delete<ReceivableEntity>(t => t.OrderId.Equals(keyValue));
                        foreach (ReceivableEntity receivableEntity in receivableentityList)
                        {
                            System.Threading.Thread.Sleep(10);
                            receivableEntity.PaymentTime = receivableEntity.PaymentTime == null ? DateTime.Now : receivableEntity.PaymentTime;
                            receivableEntity.CreateDate = receivableEntity.CreateDate == null ? DateTime.Now : receivableEntity.CreateDate;
                            receivableEntity.CreateUserId = receivableEntity.CreateUserId == null ? userid : receivableEntity.CreateUserId;
                            receivableEntity.CreateUserName = receivableEntity.CreateUserName == null ? username : receivableEntity.CreateUserName;


                            recA = recA + Convert.ToDecimal(receivableEntity.PaymentPrice) - Convert.ToDecimal(receivableEntity.DrawbackPrice);
                            receivableEntity.ReceivableId = Guid.NewGuid().ToString();
                            receivableEntity.OrderId = orderEntity.OrderId;
                            //添加收款 
                            db.Insert(receivableEntity);
                        }
                    }

                    if (orderEntryList != null)
                    {
                        //明细
                        db.Delete<OrderEntryEntity>(t => t.OrderId.Equals(keyValue));
                        foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                        {
                            System.Threading.Thread.Sleep(10);
                            orderEntryEntity.RealSize = orderEntryEntity.RealSize == null ? 0 : orderEntryEntity.RealSize;
                            orderEntryEntity.RealAmount = orderEntryEntity.RealAmount == null ? 0 : orderEntryEntity.RealAmount;
                            realsize = realsize + Convert.ToDecimal(orderEntryEntity.RealSize == null ? 0 : orderEntryEntity.RealSize);
                            orderEntryEntity.EstimateSize = 0;
                            orderEntryEntity.EstimateAmount = 0;
                            orderEntryEntity.OrderEntryId = Guid.NewGuid().ToString();
                            orderEntryEntity.OrderId = orderEntity.OrderId;
                            db.Insert(orderEntryEntity);
                        }
                    }

                    if (orderEntity.OrderStatusName.Contains("已"))
                    {
                        orderEntity.OrderStatusId = "3";
                        if(string.IsNullOrEmpty( Convert.ToString( orderEntity.FinishDate)))
                            {
                            orderEntity.FinishDate = DateTime.Today;
                        }
                    }
                    else
                    {
                        orderEntity.OrderStatusId = "1";
                    }

                    //if (Convert.ToString(orderEntity.OrderStatusId) != "3")
                    //{
                    //    orderEntity.FinishDate = null;
                    //}

                    if (recA > 0)
                    {
                        //更改订单状态 
                        if (GetEntity(keyValue).Accounts >= recA)
                        {
                            orderEntity.PaymentState = 3;
                        }
                        else
                        {
                            orderEntity.PaymentState = 2;
                        }

                        if (orderEntity.PayeeModeId == null)
                        {
                            orderEntity.PayeeModeId = "1";
                            orderEntity.PayeeModeName = "现金";
                        }
                    }

                    db.Update(orderEntity);

                    orderEntity.SellerId = null;
                    orderEntity.SellerName = null;
                    orderEntity.ModifyDate = DateTime.Now;
                    orderEntity.ModifyUserId = userid;
                    orderEntity.ModifyUserName = username;
                    orderEntity.ReceivedAmount = recA;
                    orderEntity.RealSize = realsize; //施工面积
                    db.Update(orderEntity);

                    if (GetEntity(keyValue).Description != orderEntity.Description && !string.IsNullOrEmpty(orderEntity.Description))
                    {//备注和之前不同时，插入备注列表
                        Client_OrderDescriptionEntity orderDescriptionEntity = new Client_OrderDescriptionEntity();

                        orderDescriptionEntity.CreateDate = DateTime.Now;
                        orderDescriptionEntity.CreateUserId = userid;
                        orderDescriptionEntity.CreateUserName = username;
                        orderDescriptionEntity.DescriptionId = Guid.NewGuid().ToString();
                        orderDescriptionEntity.OrderId = orderEntity.OrderId;
                        orderDescriptionEntity.DescriptionContent = orderEntity.Description;
                        db.Insert(orderDescriptionEntity);
                    }
                }
                else
                {
                    CustomerService customerService = new CustomerService();

                    if (customerService.GetEntityByCondation(orderEntity.CustomerName, orderEntity.ContecterTel).Count() == 0)
                    {
                        CustomerEntity customerEntity = new CustomerEntity();
                        customerEntity.FullName = orderEntity.CustomerName;
                        customerEntity.Tel = orderEntity.ContecterTel;
                        customerEntity.Mobile = orderEntity.ContecterTel;

                        customerService.SaveFormApi("", customerEntity);
                        orderEntity.CustomerId = customerService.GetEntityByCondation(orderEntity.CustomerName, orderEntity.ContecterTel).ToList()[0].CustomerId;
                    }

                    //主表 
                    orderEntity.SellerId = userid;
                    orderEntity.SellerName = username;
                    if (orderEntity.OrderDate == null)
                    {
                        orderEntity.OrderDate = DateTime.Now;
                    }
                    //orderEntity.OrderDate = DateTime.Now;

                    orderEntity.CreateUserId = userid;
                    orderEntity.CreateUserName = username;
                    orderEntity.OrderId = Guid.NewGuid().ToString();
                    orderEntity.CreateDate = DateTime.Now;
                    orderEntity.OrderCode = coderuleService.SetBillCodeApi(userid, "", ((int)CodeRuleEnum.Customer_OrderCode).ToString());
                    coderuleService.UseRuleSeed(orderEntity.CreateUserId, "", ((int)CodeRuleEnum.Customer_OrderCode).ToString(), db);//占用单据号

                    decimal recA = 0;
                    if (receivableentityList != null)
                    {
                        //收款
                        // db.Delete<ReceivableEntity>(t => t.OrderId.Equals(keyValue));
                        foreach (ReceivableEntity receivableEntity in receivableentityList)
                        {
                            System.Threading.Thread.Sleep(10);
                            receivableEntity.CreateDate = DateTime.Now;
                            receivableEntity.PaymentTime = receivableEntity.PaymentTime == null ? DateTime.Now : receivableEntity.PaymentTime;
                            receivableEntity.CreateUserId = userid;
                            receivableEntity.CreateUserName = username;
                            recA = recA + Convert.ToDecimal(receivableEntity.PaymentPrice) - Convert.ToDecimal(receivableEntity.DrawbackPrice);
                            receivableEntity.ReceivableId = Guid.NewGuid().ToString();
                            receivableEntity.OrderId = orderEntity.OrderId;
                            //添加收款 
                            db.Insert(receivableEntity);
                        }
                    }


                    if (orderEntryList != null)
                    {
                        //明细
                        foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                        {
                            System.Threading.Thread.Sleep(10);
                            orderEntryEntity.RealSize = orderEntryEntity.RealSize == null ? 0 : orderEntryEntity.RealSize;
                            orderEntryEntity.RealAmount = orderEntryEntity.RealAmount == null ? 0 : orderEntryEntity.RealAmount;
                            realsize = realsize + Convert.ToDecimal(orderEntryEntity.RealSize == null ? 0 : orderEntryEntity.RealSize);
                            orderEntryEntity.EstimateSize = 0;
                            orderEntryEntity.EstimateAmount = 0;
                            orderEntryEntity.OrderEntryId = Guid.NewGuid().ToString();
                            orderEntryEntity.OrderId = orderEntity.OrderId;
                            db.Insert(orderEntryEntity);
                        }
                    }
                    if (orderEntity.OrderStatusName.Contains("已"))
                    {
                        orderEntity.OrderStatusId = "3";
                        if (string.IsNullOrEmpty(Convert.ToString(orderEntity.FinishDate)))
                        {
                            orderEntity.FinishDate = DateTime.Today;
                        }
                    }
                    else
                    {
                        orderEntity.OrderStatusId = "1";
                    }
                    if (recA > 0)
                    {
                        //更改订单状态 
                        if (orderEntity.Accounts >= recA)
                        {
                            orderEntity.PaymentState = 3;
                        }
                        else
                        {
                            orderEntity.PaymentState = 2;
                        }
                        if (orderEntity.PayeeModeId == null)
                        {
                            orderEntity.PayeeModeId = "1";
                            orderEntity.PayeeModeName = "现金";
                        }
                    }


                    orderEntity.RealSize = realsize; //施工面积
                    orderEntity.ReceivedAmount = recA;
                    db.Insert(orderEntity);
                    if (!string.IsNullOrEmpty(orderEntity.Description))
                    {
                        Client_OrderDescriptionEntity orderDescriptionEntity = new Client_OrderDescriptionEntity();

                        orderDescriptionEntity.CreateDate = DateTime.Now;
                        orderDescriptionEntity.CreateUserId = userid;
                        orderDescriptionEntity.CreateUserName = username;
                        orderDescriptionEntity.DescriptionId = Guid.NewGuid().ToString();
                        orderDescriptionEntity.OrderId = orderEntity.OrderId;
                        orderDescriptionEntity.DescriptionContent = orderEntity.Description;
                        db.Insert(orderDescriptionEntity);
                    }
                }
                db.Commit();


            }
            catch
            {
                db.Rollback();

                throw;
            }
        }


        #endregion
    }
}