using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-31 09:56
    /// 描 述：系统日志表
    /// </summary>
    public class Employ_NoteMap : EntityTypeConfiguration<Employ_NoteEntity>
    {
        public Employ_NoteMap()
        {
            #region 表、主键
            //表
            this.ToTable("Employ_Note");
            //主键
            this.HasKey(t => t.NoteId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
