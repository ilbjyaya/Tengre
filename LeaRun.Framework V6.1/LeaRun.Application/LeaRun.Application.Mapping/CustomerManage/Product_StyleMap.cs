using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-09-01 15:48
    /// �� ������Ʒ����
    /// </summary>
    public class Product_StyleMap : EntityTypeConfiguration<Product_StyleEntity>
    {
        public Product_StyleMap()
        {
            #region ������
            //��
            this.ToTable("Product_Style");
            //����
            this.HasKey(t => t.StyleId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
