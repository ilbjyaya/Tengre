using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.CustomerManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Cache;
using LeaRun.Application.Entity.AuthorizeManage.ViewModel;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace WebApi.Controllers
{
    public class ChanceModelClass
    {
        public string keyValue { get; set; }
        public string chanceJson { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
    }

    public class FlowModelClass
    {
        public string keyValue { get; set; }
        public string flowJson { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
    }
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-12 10:50
    /// 描 述：备案信息
    /// </summary>
    public class ChanceController : ApiController
    {
        private ChanceBLL chancebll = new ChanceBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        private UserBLL userBLL = new UserBLL();
        private Base_FlowBLL base_flowbll = new Base_FlowBLL();
        Client_ChanceDescriptionBLL chanceDescriptionBLL = new Client_ChanceDescriptionBLL();
        #region 获取数据
        /// <summary>
        /// 获取备案列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetChanceListJson(string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var datas = chancebll.GetList(queryJson);
            var loginid = queryJson.ToJObject()["loginid"].ToString();
            var user = userBLL.GetEntity(loginid);
            var lstuid = new List<string>();
            try
            {
                if (loginid == null)
                {
                    return new HttpResponseMessage { Content = new StringContent("超时，请重新登录。", Encoding.GetEncoding("UTF-8"), "application/json") };
                }
                HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(datas.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                ////不是经理时，只能看到自己的数据
                if (!string.IsNullOrEmpty(user.ManagerId))
                {
                    lstuid = CommonMethod.GetUserList(loginid);

                    if (lstuid == null)
                    {
                        return new HttpResponseMessage { Content = new StringContent("当前用户ID不存在", Encoding.GetEncoding("UTF-8"), "application/json") };

                    }
                    var data = datas.Where(p => lstuid.Contains(p.CreateUserId));

                    ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                }
                return ut;
            }
            catch
            {
                HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(new List<string>().ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                return ut;
            }
        }
        /// <summary>
        /// 获取备案实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public HttpResponseMessage GetChanceFormJson(string keyValue)
        {
            var jsonData = new
            {
                chance = chancebll.GetEntity(keyValue),
                descriptionEntry = chanceDescriptionBLL.GetList_(keyValue),
            };

           // var data = chancebll.GetEntity(keyValue); 
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(jsonData.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }

        /// <summary>
        /// 获取客流列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetFlowListJson(string queryJson)
        {
            string userid = queryJson.ToJObject()["loginid"].ToString();
            var datas = base_flowbll.GetListApi(queryJson);
            var user = userBLL.GetEntity(userid);
            var lstuid = new List<string>();
            

            if (userid == null)
            {
                return new HttpResponseMessage { Content = new StringContent("超时，请重新登录。", Encoding.GetEncoding("UTF-8"), "application/json") };
            }
 
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(datas.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            ////不是经理时，只能看到自己的数据
            if (!string.IsNullOrEmpty(user.ManagerId))
            {
                lstuid = CommonMethod.GetUserList(userid);

                if (lstuid == null)
                {
                    return new HttpResponseMessage { Content = new StringContent("当前用户ID不存在", Encoding.GetEncoding("UTF-8"), "application/json") };
                }
                var datat = datas.Where(p => lstuid.Contains(p.CreateUserId));

                var data = datat.GroupBy(t => t.FlowName)
                        .Select(g => new
                        {
                            FlowCount = g.Sum(t => Convert.ToInt32(t.FlowCount)),
                            FlowName = Convert.ToString(g.First().FlowName)
                        });
                ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            }
            else
            {
                var data = datas.GroupBy(t => t.FlowName)
                       .Select(g => new
                       {
                           FlowCount = g.Sum(t => Convert.ToInt32(t.FlowCount)),
                           FlowName = Convert.ToString(g.First().FlowName)
                       });
                ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            }
            return ut;
        }

        /// <summary>
        /// 获取客流列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetFlowDetailListJson(string queryJson)
        {
            string userid = queryJson.ToJObject()["loginid"].ToString();
            string flowname = queryJson.ToJObject()["FlowName"].ToString();
            var datas = base_flowbll.GetDetailListApi(queryJson);
            var user = userBLL.GetEntity(userid);
            var lstuid = new List<string>();

            if (flowname == "进店") flowname = "路过";
            if (userid == null)
            {
                return new HttpResponseMessage { Content = new StringContent("超时，请重新登录。", Encoding.GetEncoding("UTF-8"), "application/json") };
            }

            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(datas.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            ////不是经理时，只能看到自己的数据
            if (!string.IsNullOrEmpty(user.ManagerId))
            {
                lstuid = CommonMethod.GetUserList(userid);

                if (lstuid == null)
                {
                    return new HttpResponseMessage { Content = new StringContent("当前用户ID不存在", Encoding.GetEncoding("UTF-8"), "application/json") };
                }
                var datat = datas.Where(p => lstuid.Contains(p.CreateUserId) && p.FlowName== flowname);

                var data = datat.GroupBy(t => t.FlowName)
                        .Select(g => new
                        {
                            FlowCount = g.Sum(t => Convert.ToInt32(t.FlowCount)),
                            OrganizeName = Convert.ToString(g.First().OrganizeName),
                            FlowName = Convert.ToString(g.First().FlowName)
                        });
                ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            }
            else
            {
                var datat = datas.Where(p =>   p.FlowName == flowname);
                var data = datat.GroupBy(t => new { t.FlowName, t.OrganizeName })
                       .Select(g => new
                       {
                           FlowCount = g.Sum(t => Convert.ToInt32(t.FlowCount)),
                           OrganizeName = Convert.ToString(g.First().OrganizeName) ,
                           FlowName = Convert.ToString(g.First().FlowName)
                       });
                ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            }
            return ut;
        }

        /// <summary>
        /// 获取客流实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public HttpResponseMessage GetFlowFormJson(string keyValue)
        {
            var data = base_flowbll.GetEntity(keyValue);
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 备案名称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public bool ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = chancebll.ExistFullName(FullName, keyValue);
            return IsOk;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除备案数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public void RemoveForm(string keyValue)
        {
            chancebll.RemoveForm(keyValue);

        }
        /// <summary>
        /// 保存备案表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public string SaveChanceForm(string keyValue, string chanceJson, string userid, string username)
        {
            CustomerBLL customerBll = new CustomerBLL();
            LogEntity logEntity = new LogEntity();
            try
            {
                var entity = chanceJson.ToObject<ChanceEntity>();
                chancebll.SaveFormApi(keyValue, entity, userid, username);


                return entity.ChanceId;
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                throw ex;
            }
        }
        [HttpPost]
        [Route("api/Chance/SaveChanceFormP")]
        public string SaveChanceFormP([FromBody]  ChanceModelClass chanceclassJson)
        //string keyValue, string chanceJson, string userid, string username)
        {
            CustomerBLL customerBll = new CustomerBLL();
            LogEntity logEntity = new LogEntity();
            try
            {
                var entity = chanceclassJson.chanceJson.ToObject<ChanceEntity>();
                string keyValue = chanceclassJson.keyValue;
                string userid = chanceclassJson.userid;
                string username = chanceclassJson.username;
                chancebll.SaveFormApi(keyValue, entity, userid, username);

                return entity.ChanceId;
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                throw ex;
            }
        }

        /// <summary>
        /// 保存客流表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Chance/SaveFlowForm")]
        public string SaveFlowForm([FromBody] FlowModelClass flowclass)
        {
            string keyValue = flowclass.keyValue;
            string userid = flowclass.userid;
            string username = flowclass.username;
            Base_FlowEntity entity = flowclass.flowJson.ToObject<Base_FlowEntity>();
            keyValue = "";
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.CreateUserId = userid;
                entity.CreateUserName = username;
            }
            else
            {
                entity.ModifyUserId = userid;
                entity.ModifyUserName = username;
            }
            entity.FlowCount = "1";           
            base_flowbll.SaveFormApi(keyValue, entity);
            return entity.FlowId;
        }

        /// <summary>
        /// 删除客流数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public void RemoveFlowForm(string keyValue)
        {
            base_flowbll.RemoveForm(keyValue);
        }
        ///// <summary>
        ///// 备案作废
        ///// </summary>
        ///// <param name="keyValue">主键值</param>
        ///// <returns></returns>
        //[HttpPost]
        //[HttpGet]
        //public ActionResult Invalid(string keyValue)
        //{
        //    chancebll.Invalid(keyValue);
        //    return Success("作废成功。");
        //}
        /// <summary>
        /// 备案转换客户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public void ToCustomer(string keyValue)
        {
            chancebll.ToCustomer(keyValue);
        }
        #endregion
    }
}
