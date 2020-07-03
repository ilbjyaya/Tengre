using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-31 09:56
    /// 描 述：系统日志表
    /// </summary>
    public class Employ_Note_DetailEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 日志主键
        /// </summary>
        /// <returns></returns>
        [Column("NOTEID")]
        public string NoteId { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        /// <returns></returns>
        [Column("CONTENT1")]
        public string Content1 { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        /// <returns></returns>
        [Column("OPERATETIME")]
        public DateTime? OperateTime { get; set; }
        /// <summary>
        /// 操作用户Id
        /// </summary>
        /// <returns></returns>
        [Column("USERID")]
        public string UserId { get; set; }
        /// <summary>
        /// 操作用户
        /// </summary>
        /// <returns></returns>
        [Column("USERNAME")]
        public string UserName { get; set; }
        /// <summary>
        /// 操作类型Id
        /// </summary>
        /// <returns></returns>
        [Column("OPERATETYPEID")]
        public string OperateTypeId { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        /// <returns></returns>
        [Column("OPERATETYPE")]
        public string OperateType { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("DELETEMARK")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
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

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.NoteDetailId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.NoteDetailId = keyValue;
                                            }
        #endregion
    }
}