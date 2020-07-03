using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Application.Service.CustomerManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System;

namespace LeaRun.Application.Busines.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：销售报表数据
    /// </summary>
    public class OrderReportBLL
    {
        private IOrderService service = new OrderService();
        private IOrderReportModelService ordermodelservice = new OrderReportModelService();

        #region 获取数据
       
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderReportModel>  GetList(string queryJson) 
        {
            return ordermodelservice.GetList( queryJson);
        } 
        #endregion
    }
}