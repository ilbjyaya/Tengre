using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-23 07:50
    /// 描 述：商品表
    /// </summary>
    public class Base_ProductMap : EntityTypeConfiguration<Base_ProductEntity>
    {
        public Base_ProductMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Product");
            //主键
            this.HasKey(t => t.ProductId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
