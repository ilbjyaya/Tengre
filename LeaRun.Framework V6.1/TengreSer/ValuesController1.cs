using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace TengreSer
{
    public class ValuesController1 : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public UserEntity CheckLoginJson(string username, string password)
        {
            try
            {
                byte[] sor = Encoding.UTF8.GetBytes(password);
                MD5 md5 = MD5.Create();
                byte[] result = md5.ComputeHash(sor);
                StringBuilder strbul = new StringBuilder(40);
                for (int i = 0; i < result.Length; i++)
                {
                    strbul.Append(result[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
                }
                password = strbul.ToString();


                UserEntity userEntity = new UserBLL().CheckLogin(username, password);
                if (userEntity == null)
                {
                    return null;
                   
                    // return new HttpResponseMessage { Content = new StringContent(new AjaxResult { type = ResultType.error, message = "登陆失败" }.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") }.ToString();
                }

                return userEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}