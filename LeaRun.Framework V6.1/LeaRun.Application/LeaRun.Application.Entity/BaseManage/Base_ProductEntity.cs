using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-07-23 07:50
    /// �� ������Ʒ��
    /// </summary>
    public class Base_ProductEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        public string ProductId { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        public string ProductCode { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        public string ProductName { get; set; }
        /// <summary>
        /// ���ٲ�ѯ
        /// </summary>
        /// <returns></returns>
        public string QuickQuery { get; set; }
        /// <summary>
        /// ��ƴ
        /// </summary>
        /// <returns></returns>
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// �ۼ�
        /// </summary>
        /// <returns></returns>
        public decimal? SalePrice { get; set; }
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
        /// Ʒ��
        /// </summary>
        /// <returns></returns>
        public string Brand { get; set; }
        /// <summary>
        /// ɫ��
        /// </summary>
        /// <returns></returns>
        public string ColorCode { get; set; }
        /// <summary>
        /// StyleCode
        /// </summary>
        /// <returns></returns>
        public string StyleCode { get; set; }

        /// <summary>
        /// Ʒ��
        /// </summary>
        /// <returns></returns>
        public string BrandName { get; set; }
        /// <summary>
        /// ɫ��
        /// </summary>
        /// <returns></returns>
        public string ColorName { get; set; }
        /// <summary>
        /// StyleCode
        /// </summary>
        /// <returns></returns>
        public string StyleName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ProductId = Guid.NewGuid().ToString();
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
            this.ProductId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}