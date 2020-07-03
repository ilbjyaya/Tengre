using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-01 15:48
    /// 描 述：商品造型
    /// </summary>
    public class Product_StyleMap : EntityTypeConfiguration<Product_StyleEntity>
    {
        public Product_StyleMap()
        {
            #region 表、主键
            //表
            this.ToTable("Product_Style");
            //主键
            this.HasKey(t => t.StyleId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
