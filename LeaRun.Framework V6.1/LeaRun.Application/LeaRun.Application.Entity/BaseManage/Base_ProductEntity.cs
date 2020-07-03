using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-07-23 07:50
    /// 描 述：商品表
    /// </summary>
    public class Base_ProductEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 商品主键
        /// </summary>
        /// <returns></returns>
        public string ProductId { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        /// <returns></returns>
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <returns></returns>
        public string ProductName { get; set; }
        /// <summary>
        /// 快速查询
        /// </summary>
        /// <returns></returns>
        public string QuickQuery { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        /// <returns></returns>
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        /// <returns></returns>
        public decimal? SalePrice { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        /// <returns></returns>
        public string Brand { get; set; }
        /// <summary>
        /// 色号
        /// </summary>
        /// <returns></returns>
        public string ColorCode { get; set; }
        /// <summary>
        /// StyleCode
        /// </summary>
        /// <returns></returns>
        public string StyleCode { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        /// <returns></returns>
        public string BrandName { get; set; }
        /// <summary>
        /// 色号
        /// </summary>
        /// <returns></returns>
        public string ColorName { get; set; }
        /// <summary>
        /// StyleCode
        /// </summary>
        /// <returns></returns>
        public string StyleName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ProductId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ProductId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}