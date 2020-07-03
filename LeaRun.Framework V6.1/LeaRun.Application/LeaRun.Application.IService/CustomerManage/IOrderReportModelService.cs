using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单管理
    /// </summary>
    public interface IOrderReportModelService
    {
        #region 获取数据
        
        /// <summary>
        /// 获取列表
        /// </summary> 
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<OrderReportModel> GetList(string queryJson);
         
        #endregion
         
    }
}