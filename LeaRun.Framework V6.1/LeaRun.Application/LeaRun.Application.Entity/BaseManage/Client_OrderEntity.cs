using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-07-25 11:02
    /// �� ������������
    /// </summary>
    public class Client_OrderEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        public string CustomerId { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        public string CustomerName { get; set; }
        /// <summary>
        /// ������ԱId
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        /// <summary>
        /// ������Ա
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// ���ݱ��
        /// </summary>
        /// <returns></returns>
        public string OrderCode { get; set; }
        /// <summary>
        /// �Żݽ��
        /// </summary>
        /// <returns></returns>
        public decimal? DiscountSum { get; set; }
        /// <summary>
        /// Ӧ�ս��
        /// </summary>
        /// <returns></returns>
        public decimal? Accounts { get; set; }
        /// <summary>
        /// ���ս��
        /// </summary>
        /// <returns></returns>
        public decimal? ReceivedAmount { get; set; }
        /// <summary>
        /// �տ�����
        /// </summary>
        /// <returns></returns>
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// �տʽ
        /// </summary>
        /// <returns></returns>
        public string PaymentMode { get; set; }
        /// <summary>
        /// �տ�״̬��1-δ�տ�2-�����տ�3-ȫ���տ
        /// </summary>
        /// <returns></returns>
        public int? PaymentState { get; set; }
        /// <summary>
        /// ���۷���
        /// </summary>
        /// <returns></returns>
        public decimal? SaleCost { get; set; }
        /// <summary>
        /// ժҪ��Ϣ
        /// </summary>
        /// <returns></returns>
        public string AbstractInfo { get; set; }
        /// <summary>
        /// ��ͬ���
        /// </summary>
        /// <returns></returns>
        public string ContractCode { get; set; }
        /// <summary>
        /// ��ͬ����
        /// </summary>
        /// <returns></returns>
        public string ContractFile { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ��Ч��־
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// �׸�����
        /// </summary>
        /// <returns></returns>
        public decimal? Subscription { get; set; }
        /// <summary>
        /// ��ϵ��id
        /// </summary>
        /// <returns></returns>
        public DateTime? ContecterId { get; set; }
        /// <summary>
        /// ��ϵ������
        /// </summary>
        /// <returns></returns>
        public string ContecterName { get; set; }
        /// <summary>
        /// ��ϵ�˵绰
        /// </summary>
        /// <returns></returns>
        public string ContecterTel { get; set; }
        /// <summary>
        /// ��ϵ���ֻ�
        /// </summary>
        /// <returns></returns>
        public string ContecterCellPhone { get; set; }
        /// <summary>
        /// �տ���id
        /// </summary>
        /// <returns></returns>
        public string PayeeUserId { get; set; }
        /// <summary>
        /// �տ�������
        /// </summary>
        /// <returns></returns>
        public string PayeeUserName { get; set; }
        /// <summary>
        /// �տʽid
        /// </summary>
        /// <returns></returns>
        public string PayeeModeId { get; set; }
        /// <summary>
        /// �տʽ����
        /// </summary>
        /// <returns></returns>
        public string PayeeModeName { get; set; }
        /// <summary>
        /// ��������id
        /// </summary>
        /// <returns></returns>
        public string DistributionChannelId { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        /// <returns></returns>
        public string DistributionChannelName { get; set; }
        /// <summary>
        /// ������Դid
        /// </summary>
        /// <returns></returns>
        public string OrderSourceId { get; set; }
        /// <summary>
        /// ������Դ����
        /// </summary>
        /// <returns></returns>
        public string OrderSourceName { get; set; }
        /// <summary>
        /// ����״̬id
        /// </summary>
        /// <returns></returns>
        public string OrderStatusId { get; set; }
        /// <summary>
        /// ����״̬������
        /// </summary>
        /// <returns></returns>
        public string OrderStatusName { get; set; }
        /// <summary>
        /// ʩ���Ŷ�id
        /// </summary>
        /// <returns></returns>
        public string OpGroupId { get; set; }
        /// <summary>
        /// ʩ���Ŷ�����
        /// </summary>
        /// <returns></returns>
        public string OpGroupName { get; set; }
        /// <summary>
        /// �Ƿ�ӵ�
        /// </summary>
        /// <returns></returns>
        public int? OpFlg { get; set; }
        /// <summary>
        /// Ԥ��ʩ��ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? OpDateEstimate { get; set; }
        /// <summary>
        /// ʩ����ַ
        /// </summary>
        /// <returns></returns>
        public string OpAddress { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string MainOrderCode { get; set; }
        /// <summary>
        /// ��������id
        /// </summary>
        /// <returns></returns>
        public string OrderTypeId { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        /// <returns></returns>
        public string OrderTypeName { get; set; }
        /// <summary>
        /// ׷����id
        /// </summary>
        /// <returns></returns>
        public string TraceUserId { get; set; }
        /// <summary>
        /// ׷��������
        /// </summary>
        /// <returns></returns>
        public string TraceUserName { get; set; }
        /// <summary>
        /// �ϻ���id
        /// </summary>
        /// <returns></returns>
        public string PartnerId { get; set; }
        /// <summary>
        /// �ϻ�������
        /// </summary>
        /// <returns></returns>
        public string PartnerName { get; set; }
        /// <summary>
        /// Ӷ������������
        /// </summary>
        /// <returns></returns>
        public string CommissionName { get; set; }
        /// <summary>
        /// Ӷ�������˵绰
        /// </summary>
        /// <returns></returns>
        public string CommissionerTel { get; set; }
        /// <summary>
        /// Ӷ������
        /// </summary>
        /// <returns></returns>
        public string CommissionerDescription { get; set; }
        /// <summary>
        /// ��ȷ��ʱ��
        /// </summary>
        /// <returns></returns>
        public int? ConfirmTime { get; set; }
        /// <summary>
        /// ���۶�
        /// </summary>
        /// <returns></returns>
        public string SaleAccounts { get; set; }
        /// <summary>
        /// ׷�ӿ���
        /// </summary>
        /// <returns></returns>
        public string AddAccounts { get; set; }
        /// <summary>
        /// �����id
        /// </summary>
        /// <returns></returns>
        public string AuditingUserId { get; set; }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        public string AuditingUserName { get; set; }
        /// <summary>
        /// �տ����
        /// </summary>
        /// <returns></returns>
        public decimal? PayeeProgress { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.OrderId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OrderId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}