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
    public class Client_ChanceDescriptionMap : EntityTypeConfiguration<Client_ChanceDescriptionEntity>
    {
        public Client_ChanceDescriptionMap()
        {
            #region ������
            //��
            this.ToTable("Client_ChanceDescription");
            //����
            this.HasKey(t => t.DescriptionId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
