using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-07-31 09:56
    /// �� ����ϵͳ��־��
    /// </summary>
    public class Employ_Note_DetailMap : EntityTypeConfiguration<Employ_Note_DetailEntity>
    {
        public Employ_Note_DetailMap()
        {
            #region ������
            //��
            this.ToTable("Employ_Note_Detail");
            //����
            this.HasKey(t => t.NoteDetailId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
