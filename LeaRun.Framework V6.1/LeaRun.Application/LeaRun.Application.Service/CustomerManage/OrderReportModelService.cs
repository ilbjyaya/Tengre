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
    public class OrderReportModelService : RepositoryFactory, IOrderReportModelService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderReportModel> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();

            if (Convert.ToString(queryParam["RptSellerType"]) == "2")
            {
                //门店报表
                strSql.Append(@"SELECT  ");
                if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                {
                    ////strSql.Append(@"   sum(isnull(o.ReceivedAmount,0)) Accounts,  ");
                    //if (Convert.ToString(queryParam["OrderParam"]) == "3")
                    //{
                    //    strSql.Append(@"   sum(isnull(o.Accounts,0)) Accounts,  ");
                    //}
                    //else
                    //{
                        strSql.Append(@"   sum(isnull(cr.PaymentPrice,0) - isnull(cr.DrawbackPrice,0)) Accounts,  ");
                    //}

                    strSql.Append(@"    RANK() OVER (ORDER BY sum(isnull(cr.PaymentPrice,0) - isnull(cr.DrawbackPrice,0)) DESC) AS RankingValue,  ");


                    strSql.Append(@"  count( distinct o.OrderId)  OrderCount, sum(isnull(cr.PaymentPrice,0) - isnull(cr.DrawbackPrice,0)) Subscription, case when bo2.FullName is null then bo.FullName else  bo2.FullName end as  SellerName  ,u.OrganizeId SellerId                               
                                FROM     ");

                    strSql.Append(@"   (select  sum(isnull(PaymentPrice,0) )  PaymentPrice,sum(  isnull(DrawbackPrice,0)) DrawbackPrice, orderid from Client_Receivable where 1=1 ");
                    //订单开始日期
                    if (!queryParam["StartTime"].IsEmpty())
                    {
                        if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                        {
                            strSql.Append(" AND PaymentTime >= @StartTime ");
                        }

                        parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
                    }

                    //订单结束日期
                    if (!queryParam["EndTime"].IsEmpty())
                    {
                        if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                        {
                            strSql.Append(" AND  PaymentTime <= @EndTime ");
                        }
                        parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
                    }
                    strSql.Append(@"  group by orderid) cr ");

                    strSql.Append(@"  left join  Client_Order o   ON o.OrderId = cr.OrderId  ");
                }
                else
                {
                    strSql.Append(@"   sum(isnull(o.Accounts,0)) Accounts,  ");
                    strSql.Append(@"    RANK() OVER (ORDER BY sum(isnull(o.Accounts,0)) DESC) AS RankingValue,  ");

                    strSql.Append(@"  count( distinct o.OrderId)  OrderCount, sum(isnull(PaymentPrice,0) - isnull(DrawbackPrice,0)) Subscription, case when bo2.FullName is null then bo.FullName else  bo2.FullName end as  SellerName  ,u.OrganizeId SellerId                               
                                FROM    Client_Order o   ");

                    strSql.Append(@"  left join  (select sum(isnull(PaymentPrice,0) ) PaymentPrice,sum(  isnull(DrawbackPrice,0)) DrawbackPrice, orderid from Client_Receivable where 1=1 ");
                    //订单开始日期
                    if (!queryParam["StartTime"].IsEmpty())
                    {
                        if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                        {
                            strSql.Append(" AND PaymentTime >= @StartTime ");
                        }

                        parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
                    }

                    //订单结束日期
                    if (!queryParam["EndTime"].IsEmpty())
                    {
                        if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                        {
                            strSql.Append(" AND  PaymentTime <= @EndTime ");
                        }
                        parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
                    }
                    strSql.Append(@"  group by orderid) cr ON o.OrderId = cr.OrderId  ");
                }

                strSql.Append(@"           LEFT JOIN Base_User u ON u.UserId = o.SellerId
                                        LEFT JOIN Base_Organize bo ON bo.OrganizeId = o.SellerId
                                        LEFT JOIN Base_Organize bo2 ON u.OrganizeId = bo2.OrganizeId

                                WHERE   1 = 1");
            }
            else
            {
                //SellerName 销售代表数据           
                strSql.Append(@"SELECT ");
                if ((Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3"))//回款数据
                {
                    //// strSql.Append(@"   sum(isnull(o.ReceivedAmount,0)) Accounts,  ");
                    //if (Convert.ToString(queryParam["OrderParam"]) == "3")
                    //{
                    //    strSql.Append(@"   sum(isnull(o.Accounts,0)) Accounts,  ");
                    //}
                    //else
                    //{
                        strSql.Append(@"   sum(isnull(cr.PaymentPrice,0) - isnull(cr.DrawbackPrice,0)) Accounts,  ");
                    //}
                    strSql.Append(@"    RANK() OVER (ORDER BY sum(isnull(cr.PaymentPrice,0)) DESC) AS RankingValue,  ");
                    strSql.Append(@"   count( distinct o.OrderId)  OrderCount, sum(isnull(cr.PaymentPrice,0)- isnull(cr.DrawbackPrice,0)) Subscription, o.SellerName, o.SellerId");
                    strSql.Append(@"    FROM     ");
                    strSql.Append(@"  (select sum( isnull(PaymentPrice,0) ) PaymentPrice,sum(  isnull(DrawbackPrice,0)) DrawbackPrice, orderid from Client_Receivable  where 1=1 ");
                    //订单开始日期
                    if (!queryParam["StartTime"].IsEmpty())
                    {
                        if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                        {
                            strSql.Append(" AND PaymentTime >= @StartTime ");
                        }

                        parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
                    }

                    //订单结束日期
                    if (!queryParam["EndTime"].IsEmpty())
                    {
                        if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                        {
                            strSql.Append(" AND  PaymentTime <= @EndTime ");
                        }
                        parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
                    }
                    strSql.Append(@"  group by orderid) cr ");
                    strSql.Append(@"  left join  Client_Order o   ON o.OrderId = cr.OrderId  ");

                }
                else
                {
                    strSql.Append(@"   sum(isnull(o.Accounts,0)) Accounts,  ");
                    strSql.Append(@"    RANK() OVER (ORDER BY sum(isnull(o.Accounts,0)) DESC) AS RankingValue,  ");
                    strSql.Append(@"   count( distinct o.OrderId)  OrderCount, sum(isnull(cr.PaymentPrice,0)- isnull(cr.DrawbackPrice,0)) Subscription, o.SellerName, o.SellerId");

                    strSql.Append(@"    FROM Client_Order o       ");
                    strSql.Append(@"  left join  (select sum( isnull(PaymentPrice,0) ) PaymentPrice,sum(  isnull(DrawbackPrice,0)) DrawbackPrice, orderid from Client_Receivable  where 1=1 ");
                    //订单开始日期
                    if (!queryParam["StartTime"].IsEmpty())
                    {
                        if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                        {
                            strSql.Append(" AND PaymentTime >= @StartTime ");
                        }

                        parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
                    }

                    //订单结束日期
                    if (!queryParam["EndTime"].IsEmpty())
                    {
                        if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                        {
                            strSql.Append(" AND  PaymentTime <= @EndTime ");
                        }
                        parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
                    }
                    strSql.Append(@"  group by orderid) cr ON o.OrderId = cr.OrderId");

                }
                // strSql.Append(@"        case when bo.FullName is null then u.RealName else  case when bo2.FullName is null then bo.FullName else  bo2.FullName end end as  SellerName      

                strSql.Append(@"    LEFT JOIN Client_Customer cc ON cc.CustomerId = o.CustomerId
                                    LEFT JOIN Base_User u ON u.UserId = o.SellerId
                                    LEFT JOIN Base_Organize bo ON bo.OrganizeId = o.SellerId
                                    LEFT JOIN Base_Organize bo2 ON u.OrganizeId = bo2.OrganizeId 
                                WHERE   1 = 1 and bo.FullName is null ");
            }

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
                if (Convert.ToString(queryParam["OpFinishFLg"]) == "3")
                {
                    strSql.Append(" AND o.FinishDate >= @StartTime ");
                }
                else
                {
                    if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                    {
                        //   strSql.Append(" AND cr.PaymentTime >= @StartTime ");
                    }
                    else
                    {
                        strSql.Append(" AND o.OrderDate >= @StartTime ");
                    }
                }


                //   parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
            }

            //订单结束日期
            if (!queryParam["EndTime"].IsEmpty())
            {
                if (Convert.ToString(queryParam["OpFinishFLg"]) == "3")
                {
                    strSql.Append(" AND o.FinishDate <= @EndTime ");
                }
                else
                {
                    if (Convert.ToString(queryParam["RptAccountType"]) == "2" || Convert.ToString(queryParam["OrderParam"]) == "3")//回款数据
                    {
                        //    strSql.Append(" AND  cr.PaymentTime <= @EndTime ");
                    }
                    else
                    {
                        strSql.Append(" AND o.OrderDate <= @EndTime ");
                    }
                }

                // parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
            }

            //门店(公司名)
            if (!queryParam["SellerName"].IsEmpty())
            {
                strSql.Append(" AND (bo.FullName like @CompanyName");
                //    parameter.Add(DbParameters.CreateDbParameter("@CompanyName", '%' + queryParam["CompanyName"].ToString() + '%'));
                //}

                ////销售人员
                //if (!queryParam["SellerName"].IsEmpty())
                //{
                strSql.Append(" OR o.SellerName like @SellerName");
                parameter.Add(DbParameters.CreateDbParameter("@SellerName", '%' + queryParam["SellerName"].ToString() + '%'));
            }

            //订单状态
            if (!queryParam["OrderStatusName"].IsEmpty())
            {
                strSql.Append(" AND o.OrderStatusName like @OrderStatusName");
                parameter.Add(DbParameters.CreateDbParameter("@OrderStatusName", '%' + queryParam["OrderStatusName"].ToString() + '%'));
            }

            //完工状态，订单状态
            if (!queryParam["OrderStatusName"].IsEmpty())
            {
                strSql.Append(" AND o.OrderStatusName like @OrderStatusName");
                parameter.Add(DbParameters.CreateDbParameter("@OrderStatusName", '%' + queryParam["OrderStatusName"].ToString() + '%'));
            }

            //完工状态，订单状态
            if (!queryParam["OpFinishFLg"].IsEmpty())
            {
                strSql.Append(" AND o.OrderStatusId = @OpFinishFLg");
                parameter.Add(DbParameters.CreateDbParameter("@OpFinishFLg", queryParam["OpFinishFLg"].ToString()));
            }

            if (Convert.ToString(queryParam["RptSellerType"]) == "2")
            {
                strSql.Append(" GROUP BY  ");
                strSql.Append(" case when bo2.FullName is null then bo.FullName else  bo2.FullName end ,u.OrganizeId   ");
            }
            else
            {
                strSql.Append(" GROUP BY  ");
                strSql.Append(" o.SellerName, o.SellerId  ");
                //strSql.Append(" case when bo.FullName is null then u.RealName else  case when bo2.FullName is null then bo.FullName else  bo2.FullName end end ,      ");
                //strSql.Append(" case when bo.FullName is null then '销售代表' else  '销售门店' end       ");
                //strSql.Append(" HAVING      ");
                //strSql.Append(" case when bo.FullName is null then '销售代表' else  '销售门店' end =  '销售代表'     ");

            }
            if (Convert.ToString(queryParam["RptSellerType"]) != "2")
            {
                strSql.Append(" having  SellerId is not null ");
            }

            if (Convert.ToString(queryParam["RptAccountType"]) == "2")//回款数据
            {
                //strSql.Append(" ORDER BY sum(isnull( o.ReceivedAmount ,0)) DESC");
                strSql.Append(" ORDER BY sum(isnull( cr.PaymentPrice ,0)) DESC");
            }
            else
            {
                strSql.Append(" ORDER BY sum(isnull( o.Accounts ,0)) DESC");
            }
            return this.BaseRepository().FindList<OrderReportModel>(strSql.ToString(), parameter.ToArray());
        }
        #endregion
    }
}