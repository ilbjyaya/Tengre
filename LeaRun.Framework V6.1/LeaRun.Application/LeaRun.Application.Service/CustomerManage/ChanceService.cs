using LeaRun.Application.Code;
using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using LeaRun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using LeaRun.Data;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-12 10:50
    /// 描 述：商机信息
    /// </summary>
    public class ChanceService : RepositoryFactory<ChanceEntity>, IChanceService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        private ITrailRecordService trailRecordService = new TrailRecordService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ChanceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ChanceEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //商机编号
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":            //商机名称
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    case "Contacts":            //联系人
                        expression = expression.And(t => t.Contacts.Contains(keyword));
                        break;
                    case "Mobile":              //手机
                        expression = expression.And(t => t.Mobile.Contains(keyword));
                        break;
                    case "Tel":                 //电话
                        expression = expression.And(t => t.Tel.Contains(keyword));
                        break;
                    case "QQ":                  //QQ
                        expression = expression.And(t => t.QQ.Contains(keyword));
                        break;
                    case "Wechat":              //微信
                        expression = expression.And(t => t.Wechat.Contains(keyword));
                        break;
                    case "All":
                        expression = expression.And(t => t.FullName.Contains(keyword)
                            || t.Contacts.Contains(keyword)
                            || t.Mobile.Contains(keyword)
                            || t.Tel.Contains(keyword)
                            || t.QQ.Contains(keyword)
                            || t.Wechat.Contains(keyword)
                            || t.CompanyName.Contains(keyword)
                            || t.Contacts.Contains(keyword)
                            || t.City.Contains(keyword)
                            );
                        break;
                    default:
                        break;
                }
            }
            //expression = expression.And(t => t.IsToCustom != 1);
            return this.BaseRepository().FindList(expression, pagination);
        }


        public IEnumerable<ChanceEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            StringBuilder strSql = new StringBuilder();
            //SellerName 销售人
            //SellerName,销售门店
            //PaymentMode  销售类型：门店？人员
            strSql.Append(@"SELECT  o.*  
                            FROM    Client_Chance o   
                                    LEFT JOIN Base_User u ON u.UserId = o.CreateUserId 
                                    LEFT JOIN Base_Organize bo2 ON u.OrganizeId = bo2.OrganizeId
                            WHERE   1 = 1");

            // UserAccount，通用条件（订单编号 / 客户姓名 / 手机 / 小区名称）,订单开始日期，订单结束日期，门店，销售代表，完工状态

            //订单单号
            if (!queryParam["ParamCommon"].IsEmpty())
            {

                ////客户名称

                strSql.Append(" AND ( o.CompanyName like @ParamCommon");
                // parameter.Add(DbParameters.CreateDbParameter("@CustomerName", '%' + queryParam["CustomerName"].ToString() + '%'));
                //}

                //手机或电话

                strSql.Append(" OR o.Mobile like @ParamCommon OR o.Tel like @ParamCommon ");
                // parameter.Add(DbParameters.CreateDbParameter("@Mobile",  queryParam["Mobile"].ToString() ));
                //}

                ////地址 
                strSql.Append(" OR o.CompanyAddress like @ParamCommon )");
                parameter.Add(DbParameters.CreateDbParameter("@ParamCommon", '%' + queryParam["ParamCommon"].ToString() + '%'));
            }


            //订单开始日期
            if (!queryParam["StartTime"].IsEmpty())
            {
                strSql.Append(" AND o.CreateDate >= @StartTime ");
                parameter.Add(DbParameters.CreateDbParameter("@StartTime", (queryParam["StartTime"].ToString() + " 00:00").ToDate()));
            }

            //订单结束日期
            if (!queryParam["EndTime"].IsEmpty())
            {
                strSql.Append(" AND o.CreateDate <= @EndTime ");
                parameter.Add(DbParameters.CreateDbParameter("@EndTime", (queryParam["EndTime"].ToString() + " 23:59").ToDate()));
            }

            //门店(公司名)
            if (!queryParam["CompanyName"].IsEmpty())
            {
                strSql.Append(" AND bo2.FullName like @CompanyName");
                parameter.Add(DbParameters.CreateDbParameter("@CompanyName", '%' + queryParam["CompanyName"].ToString() + '%'));
            }

            //销售人员
            if (!queryParam["SellerName"].IsEmpty())
            {
                strSql.Append(" AND o.CreateUserName like @SellerName");
                parameter.Add(DbParameters.CreateDbParameter("@SellerName", '%' + queryParam["SellerName"].ToString() + '%'));
            }

            //客户性质
            if (!queryParam["CustTypeId"].IsEmpty())
            {
                strSql.Append(" AND o.CustTypeId like @CustTypeId");
                parameter.Add(DbParameters.CreateDbParameter("@CustTypeId", '%' + queryParam["CustTypeId"].ToString() + '%'));
            }

            //意向基数，成功率
            if (!queryParam["Intention"].IsEmpty())
            {
                strSql.Append(" AND o.Intention = @Intention");
                parameter.Add(DbParameters.CreateDbParameter("@Intention", queryParam["Intention"].ToString()));
            }

            //是否成交   （是否转换成客户）
            if (!queryParam["IsToCustom"].IsEmpty())
            {
                strSql.Append(" AND o.IsToCustom = @IsToCustom");
                parameter.Add(DbParameters.CreateDbParameter("@IsToCustom", queryParam["IsToCustom"].ToString()));
            }

            strSql.Append(" Order BY o.CreateDate DESC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ChanceEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 商机名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<ChanceEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ChanceId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<ChanceEntity>(keyValue);
                db.Delete<TrailRecordEntity>(t => t.ObjectId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ChanceEntity entity, List<Client_ChanceDescriptionEntity> chanceDescriptionList = null)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                  

                    if (chanceDescriptionList != null)
                    {
                        ////备注明细
                        //db.Delete<Client_ChanceDescriptionEntity>(t => t.OrderId.Equals(keyValue));
                        foreach (Client_ChanceDescriptionEntity chanceDescriptionEntity in chanceDescriptionList)
                        {
                            if (GetEntity(keyValue).Description != chanceDescriptionEntity.DescriptionContent && !string.IsNullOrEmpty(chanceDescriptionEntity.DescriptionContent))
                            {//备注和之前不同时，插入备注列表
                                chanceDescriptionEntity.Create();
                                chanceDescriptionEntity.OrderId = entity.ChanceId;
                                if (chanceDescriptionEntity.DescriptionId == string.Empty) chanceDescriptionEntity.DescriptionId = Guid.NewGuid().ToString();

                                db.Insert(chanceDescriptionEntity);
                            }
                        }
                    }

                    db.Update(entity);
                }
                else
                { 
                    entity.Create();
                    db.Insert(entity);
                    //占用单据号
                    coderuleService.UseRuleSeed(entity.CreateUserId, SystemInfo.CurrentModuleId, "", db);

                    if (chanceDescriptionList != null)
                    {
                        //备注明细 
                        foreach (Client_ChanceDescriptionEntity chanceDescriptionEntity in chanceDescriptionList)
                        {
                            chanceDescriptionEntity.Create();
                            chanceDescriptionEntity.OrderId = entity.ChanceId;
                            db.Insert(chanceDescriptionEntity);
                        }
                    } 
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        public void SaveFormApi(string keyValue, ChanceEntity entity, string userid, string username)
        {
            ICodeRuleService service = new CodeRuleService();
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            try
            {

                if (!string.IsNullOrEmpty(keyValue))
                {
                    if (GetEntity(keyValue).Description != entity.Description && !string.IsNullOrEmpty(entity.Description))
                    {//备注和之前不同时，插入备注列表
                        Client_ChanceDescriptionEntity chanceDescriptionEntity = new Client_ChanceDescriptionEntity();

                        chanceDescriptionEntity.CreateDate = DateTime.Now;
                        chanceDescriptionEntity.CreateUserId = userid;
                        chanceDescriptionEntity.CreateUserName = username;
                        chanceDescriptionEntity.DescriptionId = Guid.NewGuid().ToString();
                        chanceDescriptionEntity.OrderId = keyValue;
                        chanceDescriptionEntity.DescriptionContent = entity.Description;
                        db.Insert(chanceDescriptionEntity);
                    }

                    // entity.Modify(keyValue);
                    entity.ChanceId = keyValue;
                    entity.ModifyDate = DateTime.Now;
                    entity.ModifyUserId = userid;
                    entity.ModifyUserName = username;
                    entity.CustTypeName = entity.CustTypeId;
                    this.BaseRepository().Update(entity); 
                }
                else
                {
                    entity.EnCode = service.SetBillCodeApi(userid, "", ((int)(CodeRuleEnum.Customer_ChanceCode)).ToString());
                    entity.FullName = entity.EnCode;
                    entity.ChanceId = Guid.NewGuid().ToString();
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserId = userid;
                    entity.CreateUserName = username;
                    entity.CustTypeName = entity.CustTypeId;

                    db.Insert(entity);
                    //占用单据号
                    coderuleService.UseRuleSeed(entity.CreateUserId, "", ((int)(CodeRuleEnum.Customer_ChanceCode)).ToString(), db);

                    if (!string.IsNullOrEmpty(entity.Description))
                    {
                        Client_ChanceDescriptionEntity chanceDescriptionEntity = new Client_ChanceDescriptionEntity();

                        chanceDescriptionEntity.CreateDate = DateTime.Now;
                        chanceDescriptionEntity.CreateUserId = userid;
                        chanceDescriptionEntity.CreateUserName = username;
                        chanceDescriptionEntity.DescriptionId = Guid.NewGuid().ToString();
                        chanceDescriptionEntity.OrderId = entity.ChanceId;
                        chanceDescriptionEntity.DescriptionContent = entity.Description;
                        db.Insert(chanceDescriptionEntity);
                    }
                }

                ////IRepository dbc = null;
                //////dbc = new RepositoryFactory().BaseRepository() ;

                //if (Convert.ToString(entity.IsToCustom) == "1")
                //{
                //    CustomerService customerService = new CustomerService();

                //    if (customerService.GetEntityByCondation(entity.CompanyName, entity.Mobile).Count() == 0)
                //    {
                //        CustomerEntity customerEntity = new CustomerEntity();

                //        customerEntity.CreateUserId = Guid.NewGuid().ToString();
                //        customerEntity.CreateDate = DateTime.Now;
                //        customerEntity.CreateUserId = entity.CreateUserId;
                //        customerEntity.CreateUserName = entity.CreateUserName;

                //        // customerEntity.EnCode = coderuleService.SetBillCodeApi(customerEntity.CreateUserId, "", ((int)CodeRuleEnum.Customer_CustomerCode).ToString(), db);
                //        customerEntity.FullName = entity.CompanyName;
                //        customerEntity.TraceUserId = entity.TraceUserId;
                //        customerEntity.TraceUserName = entity.TraceUserName;
                //        customerEntity.CustIndustryId = entity.CompanyNatureId;
                //        //customerEntity.CompanySite = entity.CompanySite;
                //        //customerEntity.CompanyDesc = entity.CompanyDesc;
                //        customerEntity.CompanyAddress = entity.CompanyAddress;
                //        //customerEntity.Province = entity.Province;
                //        //customerEntity.City = entity.City;
                //        //customerEntity.Contact = entity.Contacts;
                //        customerEntity.Mobile = entity.Mobile;
                //        customerEntity.Tel = entity.Mobile;
                //        //customerEntity.Fax = entity.Fax;
                //        //customerEntity.QQ = entity.QQ;
                //        //customerEntity.Email = entity.Email;
                //        customerEntity.Wechat = entity.Wechat;
                //        //customerEntity.Hobby = entity.Hobby;
                //        customerEntity.Description = entity.Description;
                //        customerEntity.CustLevelId = "C";
                //        customerEntity.CustDegreeId = "往来客户";

                //        customerService.SaveFormApi("", customerEntity);
                //    }
                //}



                db.Commit();

                if (Convert.ToString(entity.IsToCustom) == "1")
                {
                    CustomerService customerService = new CustomerService();

                    if (customerService.GetEntityByCondation(entity.CompanyName, entity.Mobile).Count() == 0)
                    {
                        ToCustomerApi(entity.ChanceId);
                    }
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }


        }

        /// <summary>
        /// 商机作废
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void Invalid(string keyValue)
        {
            ChanceEntity entity = new ChanceEntity();
            entity.Modify(keyValue);
            entity.ChanceState = 0;
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// 商机转换客户
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ToCustomer(string keyValue)
        {
            ChanceEntity chanceEntity = this.GetEntity(keyValue);
            IEnumerable<TrailRecordEntity> trailRecordList = trailRecordService.GetList(keyValue);
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                chanceEntity.Modify(keyValue);
                chanceEntity.IsToCustom = 1;
                db.Update<ChanceEntity>(chanceEntity);

                CustomerEntity customerEntity = new CustomerEntity();
                customerEntity.Create();
                customerEntity.EnCode = coderuleService.SetBillCode(customerEntity.CreateUserId, "", ((int)CodeRuleEnum.Customer_CustomerCode).ToString(), db);
                customerEntity.FullName = chanceEntity.CompanyName;
                customerEntity.TraceUserId = chanceEntity.TraceUserId;
                customerEntity.TraceUserName = chanceEntity.TraceUserName;
                customerEntity.CustIndustryId = chanceEntity.CompanyNatureId;
                customerEntity.CompanySite = chanceEntity.CompanySite;
                customerEntity.CompanyDesc = chanceEntity.CompanyDesc;
                customerEntity.CompanyAddress = chanceEntity.CompanyAddress;
                customerEntity.Province = chanceEntity.Province;
                customerEntity.City = chanceEntity.City;
                customerEntity.Contact = chanceEntity.Contacts;
                customerEntity.Mobile = chanceEntity.Mobile;
                customerEntity.Tel = chanceEntity.Tel;
                customerEntity.Fax = chanceEntity.Fax;
                customerEntity.QQ = chanceEntity.QQ;
                customerEntity.Email = chanceEntity.Email;
                customerEntity.Wechat = chanceEntity.Wechat;
                customerEntity.Hobby = chanceEntity.Hobby;
                customerEntity.Description = chanceEntity.Description;
                customerEntity.CustLevelId = "C";
                customerEntity.CustDegreeId = "往来客户";
                db.Insert<CustomerEntity>(customerEntity);

                foreach (TrailRecordEntity item in trailRecordList)
                {
                    item.TrailId = Guid.NewGuid().ToString();
                    item.ObjectId = customerEntity.CustomerId;
                    item.ObjectSort = 2;
                    db.Insert<TrailRecordEntity>(item);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        public void ToCustomerApi(string keyValue)
        {
            ChanceEntity chanceEntity = this.GetEntity(keyValue);
            IEnumerable<TrailRecordEntity> trailRecordList = trailRecordService.GetList(keyValue);
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {

                CustomerEntity customerEntity = new CustomerEntity();
                customerEntity.CustomerId = Guid.NewGuid().ToString();
                customerEntity.CreateDate = DateTime.Now;
                customerEntity.CreateUserId = chanceEntity.CreateUserId;
                customerEntity.CreateUserName = chanceEntity.CreateUserName;

                customerEntity.EnCode = coderuleService.SetBillCodeApi(customerEntity.CreateUserId, "", ((int)CodeRuleEnum.Customer_CustomerCode).ToString());
                customerEntity.FullName = chanceEntity.CompanyName;
                customerEntity.TraceUserId = chanceEntity.TraceUserId;
                customerEntity.TraceUserName = chanceEntity.TraceUserName;
                customerEntity.CustIndustryId = chanceEntity.CompanyNatureId;
                customerEntity.CompanySite = chanceEntity.CompanySite;
                customerEntity.CompanyDesc = chanceEntity.CompanyDesc;
                customerEntity.CompanyAddress = chanceEntity.CompanyAddress;
                customerEntity.Province = chanceEntity.Province;
                customerEntity.City = chanceEntity.City;
                customerEntity.Contact = chanceEntity.Contacts;
                customerEntity.Mobile = chanceEntity.Mobile;
                customerEntity.Tel = chanceEntity.Tel;
                customerEntity.Fax = chanceEntity.Fax;
                customerEntity.QQ = chanceEntity.QQ;
                customerEntity.Email = chanceEntity.Email;
                customerEntity.Wechat = chanceEntity.Wechat;
                customerEntity.Hobby = chanceEntity.Hobby;
                customerEntity.Description = chanceEntity.Description;
                customerEntity.CustLevelId = "C";
                customerEntity.CustDegreeId = "往来客户";
                db.Insert<CustomerEntity>(customerEntity);

                foreach (TrailRecordEntity item in trailRecordList)
                {
                    item.TrailId = Guid.NewGuid().ToString();
                    item.ObjectId = customerEntity.CustomerId;
                    item.ObjectSort = 2;
                    db.Insert<TrailRecordEntity>(item);
                }
                // db.Commit();
            }
            catch (Exception)
            {
                //  db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
