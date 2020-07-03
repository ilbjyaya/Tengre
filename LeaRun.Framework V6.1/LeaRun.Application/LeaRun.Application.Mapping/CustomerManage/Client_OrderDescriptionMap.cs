using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-07-27 08:05
    /// �� �����̻�������¼
    /// </summary>
    public class Client_OrderDescriptionMap : EntityTypeConfiguration<Client_OrderDescriptionEntity>
    {
        public Client_OrderDescriptionMap()
        {
            #region ������
            //��
            this.ToTable("Client_OrderDescription");
            //����
            this.HasKey(t => t.DescriptionId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
