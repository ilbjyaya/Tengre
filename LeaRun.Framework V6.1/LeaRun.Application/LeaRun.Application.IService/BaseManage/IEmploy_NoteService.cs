using LeaRun.Application.Entity.BaseManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-31 09:56
    /// 描 述：系统日志表
    /// </summary>
    public interface IEmploy_NoteService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<Employ_NoteEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary> 
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<Employ_NoteEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Employ_NoteEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        IEnumerable<Employ_Note_DetailEntity> GetDetails(string keyValue);
        /// <summary>
        /// 获取子表详细信息
        /// </summary> 
        /// <returns></returns>
        IEnumerable<Employ_Note_DetailEntity> GetDetails();
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, Employ_NoteEntity entity, Employ_Note_DetailEntity entryList);
 void SaveFormApi(string keyValue, Employ_NoteEntity entity, Employ_Note_DetailEntity entryList);
        #endregion
    }
}
