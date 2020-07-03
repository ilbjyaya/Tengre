using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Cache.Factory;
using LeaRun.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LeaRun.SOA.SSO.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 大连迎达软件有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.09.01 13:32
    /// 描 述：单点登录
    /// </summary>
    public class LoginController : ApiControllerBase
    {
        /// <summary>
        /// 测试是否连接成功
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="system">系统</param>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckLogin(string system, string account, string password)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 1;
            logEntity.OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Login);
            logEntity.OperateAccount = account;
            logEntity.OperateUserId = account;
            logEntity.Module = system;

            try
            {
                //验证账户
                UserEntity userEntity = new UserBLL().CheckLogin(account, password);

                //生成票据
                var ticket = Guid.NewGuid().ToString();
                //写入票据
                CacheFactory.Cache().WriteCache(userEntity, ticket, DateTime.Now.AddHours(8));

                //写入日志
                logEntity.ExecuteResult = 1;
                logEntity.ExecuteResultJson = "登录成功";
                logEntity.WriteLog();

                return Success("登录成功", ticket);
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 票据验证
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckTicket(string ticket)
        {
            UserEntity userEntity = CacheFactory.Cache().GetCache<UserEntity>(ticket);
            if (userEntity != null)
            {
                return Success("通过", userEntity);
            }
            else
            {
                return Error("错误");
            }
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckLoginJson(string username, string password)
        {
            try
            {
                UserEntity userEntity = new UserBLL().CheckLogin(username, password);
                if (userEntity == null)
                { 
                    return Error("登录失败");
                }
                //var departmentdata = departmentBLL.GetList();

                //var depname = string.Empty;
                //foreach (DepartmentEntity dep in departmentdata)
                //{
                //    if (dep.DepartmentId == userEntity.DepartmentId)
                //    {
                //        depname = dep.FullName;
                //    }
                //}

                var jsonData = new
                {
                    retcode = "1",
                    message = "登录成功",
                    UserId = userEntity.UserId, //用户id
                    Code = userEntity.EnCode,//用户编码
                    Account = userEntity.Account,//登陆账户
                    UserName = userEntity.RealName,//用户名
                    Password = userEntity.Password,//密码
                    Secretkey = userEntity.Secretkey,//密码秘钥
                    CompanyId = userEntity.OrganizeId,//公司主键
                    DepartmentId = userEntity.DepartmentId,//部门主键
                    RoleId = userEntity.RoleId,//角色主键
                }; 
                return Success("",jsonData); 
            }
            catch (Exception ex)
            {
                return Error("登录失败");
            }
        }
    }
}