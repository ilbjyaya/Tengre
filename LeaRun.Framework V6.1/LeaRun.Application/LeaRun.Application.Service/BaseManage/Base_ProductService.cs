using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.BaseManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LeaRun.Application.Service.BaseManage
{
    /// <summary>
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 ����ӭ��������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2018-07-23 07:50
    /// �� ������Ʒ��
    /// </summary>
    public class Base_ProductService : RepositoryFactory<Base_ProductEntity>, Base_ProductIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Base_ProductEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Base_ProductEntity>();
            var queryParam = queryJson.ToJObject();

            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT  
o.ProductId ,
o.ProductCode ,
                                    o.ProductName ,
                                    b.ItemName Brand                                   
                            FROM    Base_Product o                                   
                                    LEFT JOIN (select b.* from Base_DataItemDetail b inner join Base_DataItem c on b.ItemId= c.ItemId and c.ItemCode ='Client_BrandOption' ) b ON b.ItemValue = o.Brand   
                            WHERE   1 = 1");

            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "ProductCode":
                        strSql.Append(" AND o.ProductCode like @ProductCode");
                        parameter.Add(DbParameters.CreateDbParameter("@ProductCode", '%' + keyword + '%'));
                        break;
                    case "ProductName":           //��Ʒ��
                        strSql.Append(" AND o.ProductName like @ProductName");
                        parameter.Add(DbParameters.CreateDbParameter("@ProductName", '%' + keyword + '%'));
                        break;
                    case "Brand":              //Ʒ��
                        strSql.Append(" AND b.ItemValue like @ItemValue");
                        parameter.Add(DbParameters.CreateDbParameter("@ItemValue", '%' + keyword + '%'));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Base_ProductEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Base_ProductEntity>(); 
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT  
                                    o.ProductId ,
                                    o.ProductCode ,
                                    o.ProductName ,
                                    o.Brand BrandId ,    
                                    b.ItemName Brand                                   
                            FROM    Base_Product o                                   
                                    LEFT JOIN (select b.* from Base_DataItemDetail b inner join Base_DataItem c on b.ItemId= c.ItemId and c.ItemCode ='Client_BrandOption' ) b ON b.ItemValue = o.Brand   
                            WHERE   1 = 1");

            //��ѯ����
            if (queryJson != "")
            {
                var queryParam = queryJson.ToJObject();
                if (queryParam != null)
                {
                    if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                    {
                        string condition = queryParam["condition"].ToString();
                        string keyword = queryParam["keyword"].ToString();
                        switch (condition)
                        {
                            case "ProductCode":
                                strSql.Append(" AND o.ProductCode like @ProductCode");
                                parameter.Add(DbParameters.CreateDbParameter("@ProductCode", '%' + keyword + '%'));
                                break;
                            case "ProductName":           //��Ʒ��
                                strSql.Append(" AND o.ProductName like @ProductName");
                                parameter.Add(DbParameters.CreateDbParameter("@ProductName", '%' + keyword + '%'));
                                break;
                            case "Brand":              //Ʒ��
                                strSql.Append(" AND b.ItemValue like @ItemValue");
                                parameter.Add(DbParameters.CreateDbParameter("@ItemValue", '%' + keyword + '%'));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());

           // return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Base_ProductEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Base_ProductEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
