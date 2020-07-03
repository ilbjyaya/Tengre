using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-07-31 09:56
    /// �� ����ϵͳ��־��
    /// </summary>
    public class Employ_Note_DetailEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��־����
        /// </summary>
        /// <returns></returns>
        [Column("NOTEID")]
        public string NoteId { get; set; }
        /// <summary>
        /// �ظ�����
        /// </summary>
        /// <returns></returns>
        [Column("CONTENT1")]
        public string Content1 { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [Column("OPERATETIME")]
        public DateTime? OperateTime { get; set; }
        /// <summary>
        /// �����û�Id
        /// </summary>
        /// <returns></returns>
        [Column("USERID")]
        public string UserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("USERNAME")]
        public string UserName { get; set; }
        /// <summary>
        /// ��������Id
        /// </summary>
        /// <returns></returns>
        [Column("OPERATETYPEID")]
        public string OperateTypeId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("OPERATETYPE")]
        public string OperateType { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        [Column("DELETEMARK")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ��Ч��־
        /// </summary>
        /// <returns></returns>
        [Column("ENABLEDMARK")]
        public int? EnabledMark { get; set; }
        /// <summary>
        /// NoteDetailId
        /// </summary>
        /// <returns></returns>
        [Column("NOTEDETAILID")]
        public string NoteDetailId { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.NoteDetailId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.NoteDetailId = keyValue;
                                            }
        #endregion
    }
}