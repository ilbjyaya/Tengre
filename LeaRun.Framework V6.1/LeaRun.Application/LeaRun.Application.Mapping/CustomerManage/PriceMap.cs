using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-08-30 11:00
    /// 描 述：价格表
    /// </summary>
    public class PriceMap : EntityTypeConfiguration<PriceEntity>
    {
        public PriceMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Price");
            //主键
            this.HasKey(t => t.PriceId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
