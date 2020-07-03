using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-08-30 11:00
    /// �� �����۸��
    /// </summary>
    public class PriceMap : EntityTypeConfiguration<PriceEntity>
    {
        public PriceMap()
        {
            #region ������
            //��
            this.ToTable("Base_Price");
            //����
            this.HasKey(t => t.PriceId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
