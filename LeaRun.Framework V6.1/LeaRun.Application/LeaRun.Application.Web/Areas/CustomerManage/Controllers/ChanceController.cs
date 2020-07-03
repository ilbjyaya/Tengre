using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.Busines.CustomerManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;

namespace LeaRun.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-12 10:50
    /// 描 述：备案信息
    /// </summary>
    public class ChanceController : MvcControllerBase
    {
        private ChanceBLL chancebll = new ChanceBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        Client_ChanceDescriptionBLL chancedescriptionbll = new Client_ChanceDescriptionBLL();

        #region 视图功能
        /// <summary>
        /// 备案列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 备案表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            if (Request["keyValue"] == null)
            {
                ViewBag.EnCode = codeRuleBLL.GetBillCode(SystemInfo.CurrentUserId, SystemInfo.CurrentModuleId);
            }
            return View();
        }
        /// <summary>
        /// 备案明细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 打开备注页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ChanceDescription()
        {
            //ViewBag.userinfor = OperatorProvider.Provider.Current();
            ViewBag.DepartmentName = OperatorProvider.Provider.Current().DepartmentName;
            ViewBag.CreateUserName = OperatorProvider.Provider.Current().UserName;
            ViewBag.CreateUserId = OperatorProvider.Provider.Current().UserId;
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取备案列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = chancebll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取备案实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var jsonData = new
            {
                order = chancebll.GetEntity(keyValue),
                descriptionEntry = chancedescriptionbll.GetList_(keyValue)
            };

            //var data = chancebll.GetEntity(keyValue);
            //descriptionEntry = orderdescriptionbll.GetList_(keyValue)

            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取列表（订单备注表）
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetChanceDescriptionListJson(string orderId)
        {
            var data = chancedescriptionbll.GetList(orderId);
            return ToJsonResult(data);
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
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = chancebll.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除备案数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            chancebll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存备案表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ChanceEntity entity, string chanceDescriptionJson)
        {
            var chanceDescriptionList = chanceDescriptionJson.ToList<Client_ChanceDescriptionEntity>();

            chancebll.SaveForm(keyValue, entity, chanceDescriptionList);
            return Success("操作成功。");
        }
        /// <summary>
        /// 备案作废
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult Invalid(string keyValue)
        {
            chancebll.Invalid(keyValue);
            return Success("作废成功。");
        }
        /// <summary>
        /// 备案转换客户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult ToCustomer(string keyValue)
        {
            chancebll.ToCustomer(keyValue);
            return Success("转换成功。");
        }
        #endregion
    }
}
