using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-27 08:05
    /// 描 述：商机跟进记录
    /// </summary>
    public class Client_OrderDescriptionMap : EntityTypeConfiguration<Client_OrderDescriptionEntity>
    {
        public Client_OrderDescriptionMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_OrderDescription");
            //主键
            this.HasKey(t => t.DescriptionId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
