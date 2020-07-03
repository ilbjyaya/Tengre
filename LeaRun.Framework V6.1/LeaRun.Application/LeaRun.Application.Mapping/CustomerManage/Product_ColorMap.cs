using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-01 15:45
    /// 描 述：商品色号
    /// </summary>
    public class Product_ColorMap : EntityTypeConfiguration<Product_ColorEntity>
    {
        public Product_ColorMap()
        {
            #region 表、主键
            //表
            this.ToTable("Product_Color");
            //主键
            this.HasKey(t => t.ColorId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
