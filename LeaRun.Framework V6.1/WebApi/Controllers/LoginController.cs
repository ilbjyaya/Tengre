using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage.ViewModel;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace WebApi.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/CheckLoginJson
        [HttpGet]
        public HttpResponseMessage CheckLoginJson(string username, string password)
        {
            try
            {
                RoleBLL roleBLL = new RoleBLL();
                DepartmentBLL departmentBLL = new DepartmentBLL(); 
                OrganizeBLL organizeBLL = new OrganizeBLL();
                UserBLL userBLL = new UserBLL();

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
                    return new HttpResponseMessage { Content = new StringContent("查询失败", Encoding.GetEncoding("UTF-8"), "application/json") };
                }

                UserUrlModel userurlmodel = new UserUrlModel();

                EntityToEntity(userurlmodel,userEntity);

               OrganizeEntity orgent= organizeBLL.GetEntity(userEntity.OrganizeId);
                if( string.IsNullOrEmpty( orgent.ParentId) || orgent.ParentId=="0")
                {
                    userurlmodel.IsTopOrg = true;
                }

                //var orgnizedata = organizeBLL.GetList().ToList();
                //List<OrganizeEntity> lstOrg = new List<OrganizeEntity>();
                 Operator operators = new Operator();

                //foreach (OrganizeEntity org in orgnizedata)
                //{
                //    if (org.ManagerId == userurlmodel.UserId)
                //    {
                //        // operators.LstOrganizeId.Add(org.OrganizeId); //登录用户作为负责人的公司
                //        lstOrg=(Reorg(orgnizedata, org.OrganizeId));//登录用户作为负责人的公司及下属
                //    }
                //}
                //operators.LstOrganizeId = new List<string>();
                //foreach (OrganizeEntity org in lstOrg)
                //{
                //    operators.LstOrganizeId.Add(org.OrganizeId);
                //}

                //var departmentdata = departmentBLL.GetList().ToList();
                //List<DepartmentEntity> lstDep = new List<DepartmentEntity>();
                //foreach (DepartmentEntity dep in departmentdata)
                //{
                //    if (dep.DepartmentId == userurlmodel.DepartmentId)
                //    {
                //        userurlmodel.DepartmentName = dep.FullName;
                //    }
                //    if (dep.ManagerId == userurlmodel.UserId)
                //    {
                //       // operators.LstOrganizeId.Add(dep.DepartmentId);//登录用户作为负责人的部门 
                //        lstDep=(Redep(departmentdata, dep.DepartmentId));//登录用户作为负责人的部门及下属 
                //    }
                //}
                //operators.LstDepartmentId = new List<string>();
                //foreach (DepartmentEntity dep in lstDep)
                //{
                //    operators.LstDepartmentId.Add(dep.DepartmentId);
                //}



                //var userdata = userBLL.GetList().ToList();
                //List<UserEntity> lstUser = new List<UserEntity>();

                //foreach (UserEntity usr in userdata)
                //{
                //    if (usr.ManagerId == userurlmodel.UserId) 
                //    {
                //        //operators.LstStaffId.Add(usr.UserId);//登录用户上级主管的用户
                //        lstUser=(Reusr(userdata, userurlmodel.UserId));//登录用户作为负责人的部门及下属 
                //    }
                //}
                //operators.LstStaffId = new List<string>();
                //foreach (UserEntity usr in lstUser)
                //{
                //    operators.LstStaffId.Add(usr.UserId);
                //}
                ////var depuser1 = new List<string>();
                ////var depuser2 = new List<UserEntity>();
                ////var depuser3 = new List<UserEntity>();

                //if (operators.LstOrganizeId.Count > 0)
                //{//获取公司及下属公司人员
                //      var  depuser1 = userdata.FindAll(a => operators.LstOrganizeId.Contains(a.OrganizeId.ToString())) ;

                //    foreach(var ent in depuser1)
                //    {
                //        operators.LstStaffId.Add(ent.UserId);
                //    }
                //}
                //if (operators.LstDepartmentId.Count > 0)
                //{//获取部门及下属部门人员
                // var   depuser2 = userdata.FindAll(a => operators.LstDepartmentId.Contains(a.DepartmentId.ToString())) ;
                //    foreach (var ent in depuser2)
                //    {
                //        operators.LstStaffId.Add(ent.UserId);
                //    }
                //}
                //if (operators.LstStaffId.Count > 0)
                //{////获取人员及下属人员
                //  var  depuser3 = userdata.FindAll(a => operators.LstStaffId.Contains(a.UserId.ToString())) ;
                //    foreach (var ent in depuser3)
                //    {
                //        operators.LstStaffId.Add(ent.UserId);
                //    }
                //} 

                operators.UserId = userEntity.UserId;
                operators.UserName = userEntity.RealName;
                operators.Token = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                operators.LogTime = DateTime.Now;
                OperatorProvider.Provider.AddCurrent(operators);                 

                var roledata = roleBLL.GetList();

                foreach (RoleEntity dep in roledata)
                {
                    if (dep.RoleId == userEntity.RoleId)
                    {
                        userurlmodel.RoleName = dep.FullName;
                        userurlmodel.RoleCode = dep.EnCode;
                    }
                }
                CommonMethod.cache.Remove(userEntity.UserId + "organize");
                CommonMethod.cache.Remove(userEntity.UserId + "user");
                CommonMethod.GetUserList(userEntity.UserId);
                CommonMethod.GetOrgnizeList(userEntity.UserId);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string str = serializer.Serialize(userurlmodel);
                HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
                return ut;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { Content = new StringContent(ex.Message, Encoding.GetEncoding("UTF-8"), "application/json") };
            }
        }

        //获取公司及下属公司
        private List<OrganizeEntity> Reorg(List<OrganizeEntity> lstu, string orgid, List<OrganizeEntity> lstret = null)
        {
            if (lstret == null) lstret = new List<OrganizeEntity>();
            List<OrganizeEntity> lst = lstu.Where(a => a.ParentId == orgid    ).OrderBy(a=> a.SortCode).ToList();
            foreach(OrganizeEntity ent in lst)
            {
                lstret= lst.Union(lstu.Where(a => a.OrganizeId == orgid)).Union( Reorg(lstu, ent.OrganizeId)).ToList();
            }

            return lstret;// lst.Union(lstu.Where(a => a.OrganizeId == orgid)).ToList();
        }

        //获取部门及下属部门
        private List<DepartmentEntity> Redep(List<DepartmentEntity> lstu, string depid, List<DepartmentEntity> lstret=null)
        {
            if (lstret == null) lstret = new List<DepartmentEntity>();
            List<DepartmentEntity> lst = lstu.Where(a => a.ParentId == depid  ).OrderBy(a => a.SortCode).ToList();
            foreach (DepartmentEntity ent in lst)
            {
                lstret= lst.Union(lstu.Where(a => a.DepartmentId == depid)).Union(Redep(lstu, ent.DepartmentId)).ToList();
            }

            return lstret;// lst.Union(lstu.Where(a => a.DepartmentId == depid)).ToList();
        }

        //获取本人下属员工
        private List<UserEntity> Reusr(List<UserEntity> lstu, string usrid,List<UserEntity> lstret=null)
        {
            if (lstret == null) lstret = new List<UserEntity>();

            List<UserEntity> lst = lstu.Where(a => a.ManagerId == usrid  ).OrderBy(a => a.SortCode).ToList();
            foreach (UserEntity ent in lst)
            {
                //return lst.Union(lstu.Where(a => a.UserId == usrid)).Union(Reusr(lstu, ent.UserId)).ToList();
                lstret = lst.Union(lstu.Where(a => a.UserId == usrid)).Union(Reusr(lstu, ent.UserId, lstret)).ToList();
            } 
            return lstret;
        }

        /// <summary>
        /// 验证旧密码
        /// </summary>
        /// <param name="OldPassword"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage ValidationOldPassword(string OldPassword,string confirmPassword)
        {
            string msg = "";
            //OldPassword = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5(OldPassword, 32).ToLower(), OperatorProvider.Provider.Current().Secretkey).ToLower(), 32).ToLower();
            if (OldPassword != confirmPassword)
            {

                msg="原密码错误，请重新输入";
            }
            else
            {
                msg="通过信息验证";
            }

            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(msg, Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }
        /// <summary>
        /// 提交修改密码
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="password">新密码</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="confirmPassword">输入的确认旧密码</param>
        /// <param name="secretkey">加密码</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage SubmitResetPassword(string userid, string password, string oldpassword, string newpassword, string confirmpassword,string secretkey)
        {
              UserBLL userBLL = new UserBLL();
          string tt=  DESEncrypt.Decrypt(password,secretkey);


            string msg = "";

            oldpassword = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5(oldpassword, 32).ToLower(), secretkey).ToLower(), 32).ToLower();
           

            if (oldpassword != password)
            {
                msg = "原密码错误，请重新输入";
                return new HttpResponseMessage { Content = new StringContent(msg, Encoding.GetEncoding("UTF-8"), "application/json") };
               
            }

            if (newpassword != confirmpassword)
            {
                msg = "两次密码不相同，请重新输入";
                return new HttpResponseMessage { Content = new StringContent(msg, Encoding.GetEncoding("UTF-8"), "application/json") };

            }

            userBLL.RevisePassword(userid, Md5Helper.MD5(newpassword, 32).ToLower()); 
            msg = "密码修改成功。 ";
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(msg, Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }

        [HttpGet]
        public HttpResponseMessage testres()
        {
            string msg = "";
            
                msg = "通过信息验证"; 

            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(msg, Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }

        public void EntityToEntity(object objectsrc, object objectdest, params string[] excudeFields)
        {
            var sourceType = objectsrc.GetType();
            var destType = objectdest.GetType();
            foreach (var item in sourceType.GetProperties())
            {
                if (excudeFields != null)
                {
                    if (excudeFields.Any(x => x.ToUpper() == item.Name))
                        continue;
                }
                item.SetValue(objectdest, sourceType.GetProperty(item.Name).GetValue(objectsrc, null), null);
            }
        }

        public static void EntityToEntity(object target, object source)
        {
            Type type1 = target.GetType();
            Type type2 = source.GetType();
            foreach (var mi in type2.GetProperties())
            {
                var des = type1.GetProperty(mi.Name);
                if (des != null)
                {
                    try
                    {
                        des.SetValue(target, mi.GetValue(source, null), null);
                    }
                    catch
                    { }
                }
            }
        }
    }
}
