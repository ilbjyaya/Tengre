using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-08-22 11:54
    /// �� �������������
    /// </summary>
    public class FlowModelService : RepositoryFactory, IFlowModelService
    {
        #region ��ȡ����
        
        //��ȡ���꣬����ȵ��ŵ�����
        public IEnumerable<FlowModel> GetDetailListApi(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();
            //SellerName ������
            //SellerName,�����ŵ�
            //PaymentMode  �������ͣ��ŵꣿ��Ա
            strSql.Append(@"SELECT  f.FlowName,f.CreateUserId,o.FullName OrganizeName,u.RealName UserName,f.FlowCount
                            FROM    Base_Flow f  
                            LEFT JOIN Base_User u ON f.CreateUserId = u.UserId
                            LEFT JOIN Base_Organize o  ON o.OrganizeId = u.OrganizeId
                            WHERE   1 = 1");

            //  ��ʼ���ڣ������������� 



            //������ʼ����
            if (!queryParam["StartTime"].IsEmpty())
            {
                strSql.Append(" AND f.CreateDate >= @StartTime ");
                parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
            }

            //������������
            if (!queryParam["EndTime"].IsEmpty())
            {
                strSql.Append(" AND f.CreateDate <= @EndTime ");
                parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
            }


            return   this.BaseRepository().FindList<FlowModel>(strSql.ToString(), parameter.ToArray());
        }
 
        #endregion 
    }
}
