using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-08-22 11:54
    /// �� �������������
    /// </summary>
    public class Base_FlowMap : EntityTypeConfiguration<Base_FlowEntity>
    {
        public Base_FlowMap()
        {
            #region ������
            //��
            this.ToTable("Base_Flow");
            //����
            this.HasKey(t => t.FlowId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
