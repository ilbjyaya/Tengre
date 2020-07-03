using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.CustomerManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Entity.AuthorizeManage.ViewModel;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace WebApi.Controllers
{
    public class ReportController : ApiController
    {
        OrderReportBLL reportbll = new OrderReportBLL();
        OrganizeBLL organizeBLL = new OrganizeBLL();
        UserBLL userBLL = new UserBLL();
        /// <summary>
        /// 获取销售报表,回款数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetSalesJson(string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var datas = reportbll.GetList(queryJson);
            var queryParam = queryJson.ToJObject();
            var loginid = queryJson.ToJObject()["UserId"].ToString();
            var orderparam = Convert.ToString(queryJson.ToJObject()["OrderParam"]);
            var user = userBLL.GetEntity(loginid);
            var lstuserMainCom = userBLL.GetList();
            var orgnizedata = organizeBLL.GetList().ToList();

            //if (Convert.ToString(queryParam["RptSellerType"]) == "1")
            //{
            try
            {
                ////不是经理时，只能看到自己的数据
                if (!string.IsNullOrEmpty(user.ManagerId))
                {


                    // orgnizedata.Where(p => p.OrganizeId == "");
                    var lstuid = CommonMethod.GetUserList(Convert.ToString(queryParam["UserId"]));
                    var lstorg = CommonMethod.GetOrgnizeList(Convert.ToString(queryParam["UserId"]));

                    //if (Convert.ToString(queryParam["RptSellerType"]) == "2") //门店
                    //{
                    //    lstuid = CommonMethod.GetOrgnizeList(Convert.ToString(queryParam["UserId"]));
                    //}

                    if (lstuid == null)
                    {
                        return new HttpResponseMessage { Content = new StringContent("当前用户ID不存在", Encoding.GetEncoding("UTF-8"), "application/json") };

                    }
                    if (!lstorg.Contains("207fa1a9-160c-4943-a89b-8fa4db0547ce"))
                    {
                        if (Convert.ToString(queryParam["RptSellerType"]) != "2") //门店\
                        {
                            var lstuserMainC = lstuserMainCom.Where(p => p.OrganizeId != "207fa1a9-160c-4943-a89b-8fa4db0547ce").Select(p => p.UserId).ToList();

                            datas = datas.Where(p => lstuserMainC.Contains(p.SellerId));
                        }
                        else
                        {
                            datas = datas.Where(p => p.SellerId != "207fa1a9-160c-4943-a89b-8fa4db0547ce");
                        }  
                    }
                }

                var data = datas;

                data = datas.OrderByDescending(n => n.Accounts).ToList();
                if (!string.IsNullOrEmpty(orderparam))
                {
                    switch (orderparam)
                    {
                        case "1": //金额
                            data = datas.OrderByDescending(n => n.Accounts).ToList();
                            break;
                        case "2"://开单数
                            data = datas.OrderByDescending(n => n.OrderCount).ToList();
                            break;
                        case "3"://回款
                            data = datas.OrderByDescending(n => n.Subscription).ToList();
                            break;
                    }
                }
                //}




                int ranval = 1;
                decimal preaccount = -10;
                decimal? orderaccount = 0;
                foreach (OrderReportModel obj in data)
                {
                    orderaccount = obj.Accounts;
                    if (!string.IsNullOrEmpty(orderparam))
                    {
                        switch (orderparam)
                        {
                            case "1": //金额
                                orderaccount = obj.Accounts;
                                break;
                            case "2"://开单数
                                orderaccount = obj.OrderCount;
                                break;
                            case "3"://回款
                                orderaccount = obj.Subscription;
                                break;
                        }
                    }

                    if (preaccount == Convert.ToDecimal(orderaccount))
                    {
                        obj.RankingValue = ranval - 1;
                    }
                    else
                    {

                        obj.RankingValue = ranval;
                        ranval = ranval + 1;

                    }

                    preaccount = Convert.ToDecimal(orderaccount);
                }
                HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                return ut;

            }
            catch
            {
                HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(new List<string>().ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                return ut;
            }
        }

    }
}
