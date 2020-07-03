using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单管理
    /// </summary>
    public class OrderEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// 客户主键
        /// </summary>
        /// <returns></returns>
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        public string CustomerName { get; set; }
        /// <summary>
        /// 销售人员Id
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售人员
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
        /// <summary>
        /// 销售类型 （"":销售代表，1:门店）
        /// </summary>
        /// <returns></returns>
        public string SellerType { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        /// <returns></returns>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        /// <returns></returns>
        public string OrderCode { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        /// <returns></returns>
        public decimal? DiscountSum { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        /// <returns></returns>
        public decimal? Accounts { get; set; }
        /// <summary>
        /// 已收金额
        /// </summary>
        /// <returns></returns>
        public decimal? ReceivedAmount { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        /// <returns></returns>
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// 收款方式
        /// </summary>
        /// <returns></returns>
        public string PaymentMode { get; set; }
        /// <summary>
        /// 收款状态（1-未收款2-部分收款3-全部收款）
        /// </summary>
        /// <returns></returns>
        public int? PaymentState { get; set; }
        /// <summary>
        /// 销售费用
        /// </summary>
        /// <returns></returns>
        public decimal? SaleCost { get; set; }
        /// <summary>
        /// 摘要信息
        /// </summary>
        /// <returns></returns>
        public string AbstractInfo { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        /// <returns></returns>
        public string ContractCode { get; set; }
        /// <summary>
        /// 合同附件
        /// </summary>
        /// <returns></returns>
        public string ContractFile { get; set; }
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
        /// 首付订金
        /// </summary>angs
        /// <returns></returns>
        public decimal? Subscription { get; set; }
        /// <summary>
        /// 联系人id
        /// </summary>
        /// <returns></returns>
        public DateTime? ContecterId { get; set; }
        /// <summary>
        /// 联系人名称
        /// </summary>
        /// <returns></returns>
        public string ContecterName { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        /// <returns></returns>
        public string ContecterTel { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        /// <returns></returns>
        public string ContecterCellPhone { get; set; }
        /// <summary>
        /// 收款人id
        /// </summary>
        /// <returns></returns>
        public string PayeeUserId { get; set; }
        /// <summary>
        /// 收款人名称
        /// </summary>
        /// <returns></returns>
        public string PayeeUserName { get; set; }
        /// <summary>
        /// 收款方式id
        /// </summary>
        /// <returns></returns>
        public string PayeeModeId { get; set; }
        /// <summary>
        /// 收款方式名称
        /// </summary>
        /// <returns></returns>
        public string PayeeModeName { get; set; }
        /// <summary>
        /// 销售渠道id
        /// </summary>
        /// <returns></returns>
        public string DistributionChannelId { get; set; }
        /// <summary>
        /// 销售渠道名称
        /// </summary>
        /// <returns></returns>
        public string DistributionChannelName { get; set; }
        /// <summary>
        /// 订单来源id
        /// </summary>
        /// <returns></returns>
        public string OrderSourceId { get; set; }
        /// <summary>
        /// 订单来源名称
        /// </summary>
        /// <returns></returns>
        public string OrderSourceName { get; set; }
        /// <summary>
        /// 订单状态id
        /// </summary>
        /// <returns></returns>
        public string OrderStatusId { get; set; }
        /// <summary>
        /// 订单状态改名称
        /// </summary>
        /// <returns></returns>
        public string OrderStatusName { get; set; }
        /// <summary>
        /// 施工团队id
        /// </summary>
        /// <returns></returns>
        public string OpGroupId { get; set; }
        /// <summary>
        /// 施工团队名称
        /// </summary>
        /// <returns></returns>
        public string OpGroupName { get; set; }
        /// <summary>
        /// 是否接单
        /// </summary>
        /// <returns></returns>
        public int? OpFlg { get; set; }
        /// <summary>
        /// 预计施工时间
        /// </summary>
        /// <returns></returns>
        public DateTime? OpDateEstimate { get; set; }
        /// <summary>
        /// 施工地址
        /// </summary>
        /// <returns></returns>
        public string OpAddress { get; set; }
        /// <summary>
        /// 主单号码
        /// </summary>
        /// <returns></returns>
        public string MainOrderCode { get; set; }
        /// <summary>
        /// 订单类型id
        /// </summary>
        /// <returns></returns>
        public string OrderTypeId { get; set; }
        /// <summary>
        /// 订单类型名称
        /// </summary>
        /// <returns></returns>
        public string OrderTypeName { get; set; }
        /// <summary>
        /// 追踪人id
        /// </summary>
        /// <returns></returns>
        public string TraceUserId { get; set; }
        /// <summary>
        /// 追踪人名称
        /// </summary>
        /// <returns></returns>
        public string TraceUserName { get; set; }
        /// <summary>
        /// 合伙人id
        /// </summary>
        /// <returns></returns>
        public string PartnerId { get; set; }
        /// <summary>
        /// 合伙人名称
        /// </summary>
        /// <returns></returns>
        public string PartnerName { get; set; }
        /// <summary>
        /// 佣金领用人名称
        /// </summary>
        /// <returns></returns>
        public string CommissionName { get; set; }
        /// <summary>
        /// 佣金领用人电话
        /// </summary>
        /// <returns></returns>
        public string CommissionerTel { get; set; }
        /// <summary>
        /// 佣金描述
        /// </summary>
        /// <returns></returns>
        public string CommissionerDescription { get; set; }
        /// <summary>
        /// 待确认时间
        /// </summary>
        /// <returns></returns>
        public int? ConfirmTime { get; set; }
        /// <summary>
        /// 销售额
        /// </summary>
        /// <returns></returns>
        public string SaleAccounts { get; set; }

 /// <summary>
        /// 施工面积
        /// </summary>
        /// <returns></returns>
        public decimal? RealSize { get; set; }
        /// <summary>
        /// 追加款项
        /// </summary>
        /// <returns></returns>
        public string AddAccounts { get; set; }
        /// <summary>
        /// 审核人id
        /// </summary>
        /// <returns></returns>
        public string AuditingUserId { get; set; }
        /// <summary>
        /// 审核人名称
        /// </summary>
        /// <returns></returns>
        public string AuditingUserName { get; set; }
        /// <summary>
        /// 收款进度
        /// </summary>
        /// <returns></returns>
        public decimal? PayeeProgress { get; set; }  
        /// <summary>
        /// 小区名称
        /// </summary>
        /// <returns></returns>
        public string DistrictAddress { get; set; }

        /// <summary>
        /// 照片路径
        /// </summary>
        /// <returns></returns>
        public string PhotoPath { get; set; }

        /// <summary>
        /// 完工状态
        /// </summary>
        /// <returns></returns>
        public string OpFinishFlg { get; set; }

        /// <summary>
        /// 完工状态名
        /// </summary>
        /// <returns></returns>
        public string OpFinishName { get; set; }

        /// <summary>
        /// 完工日期
        /// </summary>
        /// <returns></returns>
        public DateTime? FinishDate { get; set; }

        #endregion

        #region 扩展操作


        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.OrderId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId =  OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.ReceivedAmount = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OrderId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }


        /// <summary>
        /// 新增调用
        /// </summary>
        public void CreateApi(string userid , string username )
        {
            this.OrderId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = userid;
            this.CreateUserName = username;
            this.ReceivedAmount = 0;
        }
        ///// <summary>
        ///// 编辑调用
        ///// </summary>
        ///// <param name="keyValue"></param>
        ///// <param name="userid"></param>
        ///// <param name="username"></param>
        //public void ModifyApi(string keyValue, string userid , string username )
        //{
        //    this.OrderId = keyValue;
        //    this.ModifyDate = DateTime.Now;
        //    this.ModifyUserId = userid;
        //    this.ModifyUserName = username;
        //}
        #endregion
    }
}