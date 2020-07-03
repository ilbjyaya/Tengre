using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-25 11:02
    /// 描 述：订单管理
    /// </summary>
    public class Client_OrderMap : EntityTypeConfiguration<Client_OrderEntity>
    {
        public Client_OrderMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_Order");
            //主键
            this.HasKey(t => t.OrderId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
