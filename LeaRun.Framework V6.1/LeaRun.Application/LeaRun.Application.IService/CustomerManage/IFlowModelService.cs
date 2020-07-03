using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-08-22 11:54
    /// 描 述：客流表
    /// </summary>
    public interface IFlowModelService
    {
        #region 获取数据 
        IEnumerable<FlowModel> GetDetailListApi(string queryJson);
      
        #endregion 
    }
}
