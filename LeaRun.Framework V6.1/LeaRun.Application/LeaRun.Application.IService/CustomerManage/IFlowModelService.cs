using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-08-22 11:54
    /// �� ����������
    /// </summary>
    public interface IFlowModelService
    {
        #region ��ȡ���� 
        IEnumerable<FlowModel> GetDetailListApi(string queryJson);
      
        #endregion 
    }
}
