using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace TengreSer
{
    /// <summary>
    /// tengreser 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class tengreser : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public void CheckLoginJson(string username, string password)
        //{
        //    try
        //    {
        //        byte[] sor = Encoding.UTF8.GetBytes(password);
        //        MD5 md5 = MD5.Create();
        //        byte[] result = md5.ComputeHash(sor);
        //        StringBuilder strbul = new StringBuilder(40);
        //        for (int i = 0; i < result.Length; i++)
        //        {
        //            strbul.Append(result[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位

        //        }
        //        password = strbul.ToString();


        //        UserEntity userEntity = new UserBLL().CheckLogin(username, password);
        //        if (userEntity == null)
        //        {
        //            var jsonDataErr = new
        //            {
        //                type = 0,
                       
        //            };
        //            Context.Response.Write(jsonDataErr);
        //            Context.Response.End();
        //            return;
        //            // return new HttpResponseMessage { Content = new StringContent(new AjaxResult { type = ResultType.error, message = "登陆失败" }.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") }.ToString();
        //        }

        //        var jsonData = new
        //        { 
        //            UserId = userEntity.UserId, //用户id
        //            Code = userEntity.EnCode,//用户编码
        //            Account = userEntity.Account,//登陆账户
        //            UserName = userEntity.RealName,//用户名
        //            Password = userEntity.Password,//密码
        //            Secretkey = userEntity.Secretkey,//密码秘钥
        //            CompanyId = userEntity.OrganizeId,//公司主键
        //            DepartmentId = userEntity.DepartmentId,//部门主键
        //            RoleId = userEntity.RoleId,//角色主键
        //        };
 
                 
        //        return Content(data.ToJson());

        //        //Context.Response.Write(jsonData);
        //        //Context.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
        //        //Context.Response.End();
        //        //return jsonData.ToString();
        //        // return new HttpResponseMessage { Content = new StringContent(new AjaxResult { type = ResultType.success, message = "登陆成功", resultdata = jsonData }.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") }.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //       // return new HttpResponseMessage { Content = new StringContent(new AjaxResult { type = ResultType.error, message = "登陆失败" }.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") }.ToString();
        //    }
        //}
 
    }


}
