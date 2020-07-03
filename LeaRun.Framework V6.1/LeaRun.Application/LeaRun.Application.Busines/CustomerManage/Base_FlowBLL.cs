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
    /// 创 建：超级管理员
    /// 日 期：2018-08-22 11:54
    /// 描 述：行政区域表
    /// </summary>
    public class Base_FlowBLL
    {
        private Base_FlowIService service = new Base_FlowService(); 
        private IFlowModelService flowmodeservice = new FlowModelService(); 
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Base_FlowEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

  public IEnumerable<Base_FlowEntity> GetListApi(string queryJson)
        {
            return service.GetListApi(queryJson);
        }

  public IEnumerable<FlowModel> GetDetailListApi(string queryJson)
        {
            return flowmodeservice.GetDetailListApi(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Base_FlowEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Base_FlowEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveFormApi(string keyValue, Base_FlowEntity entity)
        {
            try
            {
                service.SaveFormApi(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
