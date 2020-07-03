using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-09-01 15:45
    /// �� ������Ʒɫ��
    /// </summary>
    public class Product_ColorMap : EntityTypeConfiguration<Product_ColorEntity>
    {
        public Product_ColorMap()
        {
            #region ������
            //��
            this.ToTable("Product_Color");
            //����
            this.HasKey(t => t.ColorId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
