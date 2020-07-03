using LeaRun.Application.Busines.CustomerManage;
using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-09-01 15:48
    /// �� ������Ʒ����
    /// </summary>
    public class Product_StyleController : MvcControllerBase
    {
        private Product_StyleBLL product_stylebll = new Product_StyleBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = product_stylebll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="ProductCode"></param> 
        /// <returns>�����б�Json</returns>
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
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = product_stylebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            product_stylebll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Product_StyleEntity entity)
        {
            product_stylebll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion
    }
}
