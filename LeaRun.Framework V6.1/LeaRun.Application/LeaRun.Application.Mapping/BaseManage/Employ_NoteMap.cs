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
    public class Employ_NoteMap : EntityTypeConfiguration<Employ_NoteEntity>
    {
        public Employ_NoteMap()
        {
            #region ������
            //��
            this.ToTable("Employ_Note");
            //����
            this.HasKey(t => t.NoteId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
