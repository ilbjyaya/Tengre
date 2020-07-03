using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单明细
    /// </summary>
    public class OrderEntryEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 订单明细主键
        /// </summary>
        /// <returns></returns>
        public string OrderEntryId { get; set; }
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        /// <returns></returns>
        public string ProductId { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        /// <returns></returns>
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <returns></returns>
        public string ProductName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        /// <returns></returns>
        public string BrandId { get; set; }

        /// <summary>
        /// 色号
        /// </summary>
        /// <returns></returns>
        public string ColorCodeId { get; set; }

        /// <summary>
        /// 花型
        /// </summary>
        /// <returns></returns>
        public string StyleCodeId { get; set; }


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
        /// 花型
        /// </summary>
        /// <returns></returns>
        public string StyleCode { get; set; }

        /// <summary>
        /// 施工位置Id
        /// </summary>
        /// <returns></returns>
        public string OpPositionId { get; set; }

        /// <summary>
        /// 施工位置
        /// </summary>
        /// <returns></returns>
        public string OpPosition { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        /// <returns></returns>
        public decimal? OffRate { get; set; }

        /// <summary>
        /// 预估面积
        /// </summary>
        /// <returns></returns>
        public decimal? EstimateSize { get; set; }

        /// <summary>
        /// 预估金额
        /// </summary>
        /// <returns></returns>
        public decimal? EstimateAmount { get; set; }

        /// <summary>
        /// 实际面积
        /// </summary>
        /// <returns></returns>
        public decimal? RealSize { get; set; }

        /// <summary>
        /// 实际金额
        /// </summary>
        /// <returns></returns>
        public decimal? RealAmount { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        public string UnitId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        public decimal? Qty { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>
        public decimal? Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        /// <returns></returns>
        public decimal? Amount { get; set; }
        /// <summary>
        /// 含税单价
        /// </summary>
        /// <returns></returns>
        public decimal? Taxprice { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        /// <returns></returns>
        public decimal? TaxRate { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        /// <returns></returns>
        public decimal? Tax { get; set; }
        /// <summary>
        /// 含税金额
        /// </summary>
        /// <returns></returns>
        public decimal? TaxAmount { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.OrderEntryId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OrderEntryId = keyValue;
        }
        #endregion
    }
}