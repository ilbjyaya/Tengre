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
    /// 描 述：订单管理
    /// </summary>
    public class OrderBLL
    {
        private IOrderService service = new OrderService();
        private IOrderModelService ordermodelservice = new OrderModelService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary> 
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderModel>  GetList(string queryJson) 
        {
            return ordermodelservice.GetList( queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OrderEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取前单、后单 数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="type">类型（1-前单；2-后单）</param>
        /// <returns>返回实体</returns>
        public OrderEntity GetPrevOrNextEntity(string keyValue, int type)
        {
            return service.GetPrevOrNextEntity(keyValue, type);
        }

        public IEnumerable<Product_StyleModelEntity> GetStyleList_(string queryJson)
        {
            return ordermodelservice.GetStyleList_(queryJson);
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
        /// <param name="entitys">明细实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, OrderEntity entity, List<OrderEntryEntity> entitys, List<Client_OrderDescriptionEntity> orderDescriptionEntitys=null)
        {
            try
            {
                    service.SaveForm(keyValue, entity, entitys, orderDescriptionEntitys);
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
        /// <param name="entitys">明细实体对象</param>
        /// <returns></returns>
        public void SaveFormApi(string userid, string username, string keyValue, OrderEntity entity, List<OrderEntryEntity> entitys, List<ReceivableEntity> receivableentityList=null)
        {
            try
            {

                service.SaveFormApi(keyValue, entity, entitys, receivableentityList, userid,username);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}