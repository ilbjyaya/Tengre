using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Entity.BaseManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web; 

namespace WebApi.Controllers
{
    public static class CommonMethod
    {
        public static System.Web.Caching.Cache cache = HttpRuntime.Cache;

        public static List<string> GetUserList(string userid)
        {
            if (cache[userid + "user"] != null)
            {
                return  (List<string>)cache[userid + "user"];
            }

            DepartmentBLL departmentBLL = new DepartmentBLL();
            OrganizeBLL organizeBLL = new OrganizeBLL();
            UserBLL userBLL = new UserBLL(); 
            
            UserEntity userEntity = new UserBLL().GetEntity(userid);
            if (userEntity == null)
            {
                return null;
            }

            var orgnizedata = organizeBLL.GetList().ToList();
            List<OrganizeEntity> lstOrg = new List<OrganizeEntity>();
         
            foreach (OrganizeEntity org in orgnizedata)
            {
                if (org.ManagerId == userEntity.UserId)
                {
                    // operators.LstOrganizeId.Add(org.OrganizeId); //登录用户作为负责人的公司
                    lstOrg = (Reorg(orgnizedata, org.OrganizeId));//登录用户作为负责人的公司及下属
                    lstOrg.Add(org);
                }
            }
         var   LstOrganizeId = new List<string>();
            foreach (OrganizeEntity org in lstOrg)
            {
                LstOrganizeId.Add(org.OrganizeId);
            }



            var departmentdata = departmentBLL.GetList().ToList();
            List<DepartmentEntity> lstDep = new List<DepartmentEntity>();
            foreach (DepartmentEntity dep in departmentdata)
            {  
                if (dep.ManagerId == userEntity.UserId)
                {
                    // operators.LstOrganizeId.Add(dep.DepartmentId);//登录用户作为负责人的部门 
                    lstDep = (Redep(departmentdata, dep.DepartmentId));//登录用户作为负责人的部门及下属 
                    lstDep.Add(dep);
                }
            }
          var  LstDepartmentId = new List<string>();
            foreach (DepartmentEntity dep in lstDep)
            {
                LstDepartmentId.Add(dep.DepartmentId);
            }

            var userdata = userBLL.GetList().ToList();
            List<UserEntity> lstUser = new List<UserEntity>();

            foreach (UserEntity usr in userdata)
            {
                if (usr.ManagerId == userEntity.UserId)
                {
                    //operators.LstStaffId.Add(usr.UserId);//登录用户上级主管的用户
                    lstUser = (Reusr(userdata, userEntity.UserId));//登录用户作为负责人的部门及下属 
                }
            }
           var LstStaffId = new List<string>();
            foreach (UserEntity usr in lstUser)
            {
                LstStaffId.Add(usr.UserId);
            } 

            if (LstOrganizeId.Count > 0)
            {//获取公司及下属公司人员
                var depuser1 = userdata.FindAll(a => LstOrganizeId.Contains(Convert.ToString(a.OrganizeId)));

                foreach (var ent in depuser1)
                {
                    LstStaffId.Add(ent.UserId);
                }
            }
            if (LstDepartmentId.Count > 0)
            {//获取部门及下属部门人员
                var depuser2 = userdata.FindAll(a => LstDepartmentId.Contains( Convert.ToString( a.DepartmentId)));
                foreach (var ent in depuser2)
                {
                    LstStaffId.Add(ent.UserId);
                }
            }
            if (LstStaffId.Count > 0)
            {////获取人员及下属人员
                var depuser3 = userdata.FindAll(a => LstStaffId.Contains(Convert.ToString(a.UserId)));
                foreach (var ent in depuser3)
                {
                    LstStaffId.Add(ent.UserId);
                }
            }
            LstStaffId.Add(userid); 
         
            cache.Insert(userid + "user", LstStaffId.Distinct().ToList(), null, DateTime.Now.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);

            return  LstStaffId.Distinct().ToList(); ;

        }


        public static List<string> GetOrgnizeList(string userid)
        {
            if (cache[userid + "organize"] != null)
            {
                return (List<string>)cache[userid + "organize"];
            }

            DepartmentBLL departmentBLL = new DepartmentBLL();
            OrganizeBLL organizeBLL = new OrganizeBLL();
            UserBLL userBLL = new UserBLL();

            UserEntity userEntity = new UserBLL().GetEntity(userid);
            if (userEntity == null)
            {
                return null;
            }

            var orgnizedata = organizeBLL.GetList().ToList();
            List<OrganizeEntity> lstOrg = new List<OrganizeEntity>();

            foreach (OrganizeEntity org in orgnizedata)
            {
                if (org.ManagerId == userEntity.UserId)
                {
                    // operators.LstOrganizeId.Add(org.OrganizeId); //登录用户作为负责人的公司
                    lstOrg = (Reorg(orgnizedata, org.OrganizeId));//登录用户作为负责人的公司及下属
                    lstOrg.Add(org);
                }
            }
            var LstOrganizeId = new List<string>();
            foreach (OrganizeEntity org in lstOrg)
            {
                LstOrganizeId.Add(org.OrganizeId);
            }

            cache.Insert(userid+"organize", LstOrganizeId.Distinct().ToList(), null, DateTime.Now.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);
            return LstOrganizeId.Distinct().ToList(); ;

        }

        //获取公司及下属公司
        private static List<OrganizeEntity> Reorg(List<OrganizeEntity> lstu, string orgid, List<OrganizeEntity> lstret = null)
        {
            if (lstret == null) lstret = new List<OrganizeEntity>();
            List<OrganizeEntity> lst = lstu.Where(a => a.ParentId == orgid).OrderBy(a => a.SortCode).ToList();
            foreach (OrganizeEntity ent in lst)
            {
                lstret = lst.Union(lstu.Where(a => a.OrganizeId == orgid)).Union(Reorg(lstu, ent.OrganizeId)).ToList();
            }

            return lstret;// lst.Union(lstu.Where(a => a.OrganizeId == orgid)).ToList();
        }

        //获取部门及下属部门
        private static List<DepartmentEntity> Redep(List<DepartmentEntity> lstu, string depid, List<DepartmentEntity> lstret = null)
        {
            if (lstret == null) lstret = new List<DepartmentEntity>();
            List<DepartmentEntity> lst = lstu.Where(a => a.ParentId == depid).OrderBy(a => a.SortCode).ToList();
            foreach (DepartmentEntity ent in lst)
            {
                lstret = lst.Union(lstu.Where(a => a.DepartmentId == depid)).Union(Redep(lstu, ent.DepartmentId)).ToList();
            }

            return lstret;// lst.Union(lstu.Where(a => a.DepartmentId == depid)).ToList();
        }

        //获取本人下属员工
        private static List<UserEntity> Reusr(List<UserEntity> lstu, string usrid, List<UserEntity> lstret = null)
        {
            if (lstret == null) lstret = new List<UserEntity>();

            List<UserEntity> lst = lstu.Where(a => a.ManagerId == usrid).OrderBy(a => a.SortCode).ToList();
            foreach (UserEntity ent in lst)
            {
                //return lst.Union(lstu.Where(a => a.UserId == usrid)).Union(Reusr(lstu, ent.UserId)).ToList();
                lstret = lst.Union(lstu.Where(a => a.UserId == usrid)).Union(Reusr(lstu, ent.UserId, lstret)).ToList();
            }
            return lstret;
        }

    }
}