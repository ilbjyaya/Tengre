using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-08-22 11:54
    /// 描 述：行政区域表
    /// </summary>
    public class Base_FlowMap : EntityTypeConfiguration<Base_FlowEntity>
    {
        public Base_FlowMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Flow");
            //主键
            this.HasKey(t => t.FlowId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
