using LeaRun.Application.Busines.CustomerManage;
using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-01 15:48
    /// 描 述：商品造型
    /// </summary>
    public class Product_StyleController : MvcControllerBase
    {
        private Product_StyleBLL product_stylebll = new Product_StyleBLL();

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
            var data = product_stylebll.GetPageList(pagination, keyword);
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
            var data = product_stylebll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="ProductCode"></param> 
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJsonByCondation(string ProductCode)
        {
            Busines.CustomerManage.PriceBLL priceBLL = new Busines.CustomerManage.PriceBLL();
            var data = product_stylebll.GetList("").Where (t=>t.ProductCode== ProductCode).OrderBy(t => t.ItemName); 
            var dataprice = priceBLL.GetList(""); 

            var sg = from c in data
                     join t in dataprice
            on new { id = c.ProductCode, id1 = c.ItemValue }
            equals new { id = t.ProductId, id1 = t.ItemDetailId }
                     select new
                     { ProductName=c.ProductName,
                         StyleId = c.StyleId,
                         ItemValue = c.ItemValue,
                         ItemName = c.ItemName,
                         Price = t.SalePrice == null ? 0 : t.SalePrice
                     };
            return Content(sg.ToJson());
             
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = product_stylebll.GetEntity(keyValue);
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
            product_stylebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, Product_StyleEntity entity)
        {
            product_stylebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
