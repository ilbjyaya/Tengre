using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.Busines.CustomerManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using LeaRun.Application.Code;
using System.Collections.Generic;
using System;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-27 08:05
    /// 描 述：备案跟进记录
    /// </summary>
    public class ChanceDescriptionController : MvcControllerBase
    {
        private Client_ChanceDescriptionBLL client_Chancedescriptionbll = new Client_ChanceDescriptionBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Client_ChanceDescriptionIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Client_ChanceDescriptionForm()
        {
            //if (Request["keyValue"] == null)
            //{
                ViewBag.userinfor = OperatorProvider.Provider.Current();
            //}
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = client_Chancedescriptionbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        [HttpGet]
        public ActionResult GetListJsonTime(string objectId)
        {
            var data = client_Chancedescriptionbll.GetList_(objectId);
            Dictionary<string, string> dictionaryDate = new Dictionary<string, string>();
            foreach (Client_ChanceDescriptionEntity item in data)
            {
                string key = item.CreateDate.ToDate().ToString("yyyy-MM-dd");
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
                if (item.CreateDate.ToDate().ToString("yyyy-MM-dd") == currentTime)
                {
                    key = "今天";
                }
                if (!dictionaryDate.ContainsKey(key))
                {
                    dictionaryDate.Add(key, item.CreateDate.ToDate().ToString("yyyy-MM-dd"));
                }
            }
            var jsonData = new
            {
                timeline = dictionaryDate,
                rows = data,
            };
            return ToJsonResult(jsonData);
        }


        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = client_Chancedescriptionbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            client_Chancedescriptionbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Client_ChanceDescriptionEntity entity)
        {
            client_Chancedescriptionbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
