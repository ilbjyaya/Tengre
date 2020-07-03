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
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� �����ܴ���
    /// �� �ڣ�2016-03-12 10:50
    /// �� ����������Ϣ
    /// </summary>
    public class ChanceController : ApiController
    {
        private ChanceBLL chancebll = new ChanceBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        private UserBLL userBLL = new UserBLL();
        private Base_FlowBLL base_flowbll = new Base_FlowBLL();
        Client_ChanceDescriptionBLL chanceDescriptionBLL = new Client_ChanceDescriptionBLL();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
                    return new HttpResponseMessage { Content = new StringContent("��ʱ�������µ�¼��", Encoding.GetEncoding("UTF-8"), "application/json") };
                }
                HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(datas.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                ////���Ǿ���ʱ��ֻ�ܿ����Լ�������
                if (!string.IsNullOrEmpty(user.ManagerId))
                {
                    lstuid = CommonMethod.GetUserList(loginid);

                    if (lstuid == null)
                    {
                        return new HttpResponseMessage { Content = new StringContent("��ǰ�û�ID������", Encoding.GetEncoding("UTF-8"), "application/json") };

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
        /// ��ȡ����ʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
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
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public HttpResponseMessage GetFlowListJson(string queryJson)
        {
            string userid = queryJson.ToJObject()["loginid"].ToString();
            var datas = base_flowbll.GetListApi(queryJson);
            var user = userBLL.GetEntity(userid);
            var lstuid = new List<string>();
            

            if (userid == null)
            {
                return new HttpResponseMessage { Content = new StringContent("��ʱ�������µ�¼��", Encoding.GetEncoding("UTF-8"), "application/json") };
            }
 
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(datas.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            ////���Ǿ���ʱ��ֻ�ܿ����Լ�������
            if (!string.IsNullOrEmpty(user.ManagerId))
            {
                lstuid = CommonMethod.GetUserList(userid);

                if (lstuid == null)
                {
                    return new HttpResponseMessage { Content = new StringContent("��ǰ�û�ID������", Encoding.GetEncoding("UTF-8"), "application/json") };
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
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public HttpResponseMessage GetFlowDetailListJson(string queryJson)
        {
            string userid = queryJson.ToJObject()["loginid"].ToString();
            string flowname = queryJson.ToJObject()["FlowName"].ToString();
            var datas = base_flowbll.GetDetailListApi(queryJson);
            var user = userBLL.GetEntity(userid);
            var lstuid = new List<string>();

            if (flowname == "����") flowname = "·��";
            if (userid == null)
            {
                return new HttpResponseMessage { Content = new StringContent("��ʱ�������µ�¼��", Encoding.GetEncoding("UTF-8"), "application/json") };
            }

            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(datas.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            ////���Ǿ���ʱ��ֻ�ܿ����Լ�������
            if (!string.IsNullOrEmpty(user.ManagerId))
            {
                lstuid = CommonMethod.GetUserList(userid);

                if (lstuid == null)
                {
                    return new HttpResponseMessage { Content = new StringContent("��ǰ�û�ID������", Encoding.GetEncoding("UTF-8"), "application/json") };
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
        /// ��ȡ����ʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public HttpResponseMessage GetFlowFormJson(string keyValue)
        {
            var data = base_flowbll.GetEntity(keyValue);
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �������Ʋ����ظ�
        /// </summary>
        /// <param name="FullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        [HttpGet]
        public bool ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = chancebll.ExistFullName(FullName, keyValue);
            return IsOk;
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public void RemoveForm(string keyValue)
        {
            chancebll.RemoveForm(keyValue);

        }
        /// <summary>
        /// ���汸�������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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
        /// ������������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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
        /// ɾ����������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpGet]
        public void RemoveFlowForm(string keyValue)
        {
            base_flowbll.RemoveForm(keyValue);
        }
        ///// <summary>
        ///// ��������
        ///// </summary>
        ///// <param name="keyValue">����ֵ</param>
        ///// <returns></returns>
        //[HttpPost]
        //[HttpGet]
        //public ActionResult Invalid(string keyValue)
        //{
        //    chancebll.Invalid(keyValue);
        //    return Success("���ϳɹ���");
        //}
        /// <summary>
        /// ����ת���ͻ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
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
