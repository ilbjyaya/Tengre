using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Busines.BaseManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.BaseManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-07-31 09:56
    /// �� ����ϵͳ��־��
    /// </summary>
    public class Employ_NoteController : MvcControllerBase
    {
        private Employ_NoteBLL employ_notebll = new Employ_NoteBLL();

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
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = employ_notebll.GetPageList(pagination, queryJson);
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
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = employ_notebll.GetEntity(keyValue);
            var childData = employ_notebll.GetDetails(keyValue); var jsonData = new { entity = data, childEntity = childData }; return ToJsonResult(jsonData);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            var data = employ_notebll.GetDetails(keyValue);
            return ToJsonResult(data);
        }
        ///// <summary>
        ///// ��ȡ�ӱ���ϸ��Ϣ 
        ///// </summary>
        ///// <returns>���ض���Json</returns>
        //[HttpGet]
        //public ActionResult GetDetailsJson()
        //{
        //    var data = employ_notebll.GetDetails();
        //    return ToJsonResult(data);
        //}
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
            employ_notebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string content1)
        {
            var entity = strEntity.ToObject<Employ_NoteEntity>();
            // var childEntitys = strChildEntitys.ToList<Employ_Note_DetailEntity>();
            var childEntitys = new Employ_Note_DetailEntity();

            // childEntitys.NoteId = entity.NoteId;
            childEntitys.OperateTime = System.DateTime.Now;
            childEntitys.OperateType = "�ظ�";
            childEntitys.OperateTypeId = "1";
            if (string.IsNullOrEmpty(keyValue))
            {
                childEntitys.UserId = OperatorProvider.Provider.Current().UserId;
                childEntitys.UserName = OperatorProvider.Provider.Current().UserName;
            }
            childEntitys.DeleteMark = 0;
            childEntitys.EnabledMark = 1;
            childEntitys.Content1 = content1;


            employ_notebll.SaveForm(keyValue, entity, childEntitys);
            return Success("�����ɹ���");
        }
        #endregion
    }
}
