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
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� �����ܴ���
    /// �� �ڣ�2016-03-12 10:50
    /// �� ����������Ϣ
    /// </summary>
    public class ChanceController : MvcControllerBase
    {
        private ChanceBLL chancebll = new ChanceBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        Client_ChanceDescriptionBLL chancedescriptionbll = new Client_ChanceDescriptionBLL();

        #region ��ͼ����
        /// <summary>
        /// �����б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// ������ҳ��
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
        /// ������ϸҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// �򿪱�עҳ��
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

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ����ʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
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
        /// ��ȡ�б�������ע��
        /// </summary>
        /// <param name="orderId">����Id</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetChanceDescriptionListJson(string orderId)
        {
            var data = chancedescriptionbll.GetList(orderId);
            return ToJsonResult(data);
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
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = chancebll.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            chancebll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ���汸�������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ChanceEntity entity, string chanceDescriptionJson)
        {
            var chanceDescriptionList = chanceDescriptionJson.ToList<Client_ChanceDescriptionEntity>();

            chancebll.SaveForm(keyValue, entity, chanceDescriptionList);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult Invalid(string keyValue)
        {
            chancebll.Invalid(keyValue);
            return Success("���ϳɹ���");
        }
        /// <summary>
        /// ����ת���ͻ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult ToCustomer(string keyValue)
        {
            chancebll.ToCustomer(keyValue);
            return Success("ת���ɹ���");
        }
        #endregion
    }
}
