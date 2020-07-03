using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.Busines.CustomerManage;
using System.Linq;
using System.Web.Mvc;
using LeaRun.Util.WebControl;
using LeaRun.Util;

namespace LeaRun.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-01 15:45
    /// 描 述：商品色号
    /// </summary>
    public class Product_ColorController : MvcControllerBase
    {
        private Product_ColorBLL product_colorbll = new Product_ColorBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string keyword)
        {
            var watch = CommonHelper.TimerStart();
            var data = product_colorbll.GetPageList(pagination, keyword);
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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = product_colorbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        [HttpGet]
        public ActionResult GetListJsonByProduct(string ProductCode)
        {
            var data = product_colorbll.GetList("");
            data= data.Where(t => t.ProductCode == ProductCode).OrderBy(t=>t.ItemName);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = product_colorbll.GetEntity(keyValue);
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
            product_colorbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, Product_ColorEntity entity)
        {
            product_colorbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
