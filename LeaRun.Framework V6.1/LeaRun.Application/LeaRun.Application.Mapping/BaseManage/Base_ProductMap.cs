using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-07-23 07:50
    /// �� ������Ʒ��
    /// </summary>
    public class Base_ProductMap : EntityTypeConfiguration<Base_ProductEntity>
    {
        public Base_ProductMap()
        {
            #region ������
            //��
            this.ToTable("Base_Product");
            //����
            this.HasKey(t => t.ProductId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
