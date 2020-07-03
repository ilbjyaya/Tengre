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
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-31 09:56
    /// 描 述：系统日志表
    /// </summary>
    public class Employ_NoteController : MvcControllerBase
    {
        private Employ_NoteBLL employ_notebll = new Employ_NoteBLL();

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
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
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
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = employ_notebll.GetEntity(keyValue);
            var childData = employ_notebll.GetDetails(keyValue); var jsonData = new { entity = data, childEntity = childData }; return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取子表详细信息 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            var data = employ_notebll.GetDetails(keyValue);
            return ToJsonResult(data);
        }
        ///// <summary>
        ///// 获取子表详细信息 
        ///// </summary>
        ///// <returns>返回对象Json</returns>
        //[HttpGet]
        //public ActionResult GetDetailsJson()
        //{
        //    var data = employ_notebll.GetDetails();
        //    return ToJsonResult(data);
        //}
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
            employ_notebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string content1)
        {
            var entity = strEntity.ToObject<Employ_NoteEntity>();
            // var childEntitys = strChildEntitys.ToList<Employ_Note_DetailEntity>();
            var childEntitys = new Employ_Note_DetailEntity();

            // childEntitys.NoteId = entity.NoteId;
            childEntitys.OperateTime = System.DateTime.Now;
            childEntitys.OperateType = "回复";
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
            return Success("操作成功。");
        }
        #endregion
    }
}
