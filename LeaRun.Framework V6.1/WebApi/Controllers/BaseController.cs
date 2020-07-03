using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.CustomerManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Cache;
using LeaRun.Application.Entity.AuthorizeManage.ViewModel;
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
using System.Web.Script.Serialization;

namespace WebApi.Controllers
{
    public class BaseController : ApiController
    {
        private UserBLL userBLL = new UserBLL();
        private UserCache userCache = new UserCache();
        private OrganizeBLL organizeBLL = new OrganizeBLL();
        private OrganizeCache organizeCache = new OrganizeCache();
        private DepartmentBLL departmentBLL = new DepartmentBLL();
        private DepartmentCache departmentCache = new DepartmentCache();
        private Base_ProductBLL base_productbll = new Base_ProductBLL();
        private DataItemDetailBLL busines = new DataItemDetailBLL();
        private PriceBLL priceBLL = new PriceBLL();

        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetOrganizeTreeListJson()
        {
            var data = organizeBLL.GetList().ToList();
            var treeList = new List<TreeGridEntity>();
            foreach (OrganizeEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.OrganizeId) == 0 ? false : true;
                tree.id = item.OrganizeId;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                tree.entityJson = item.ToJson();
                treeList.Add(tree);
            }
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(treeList.TreeJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }


        [HttpGet]
        public HttpResponseMessage GetOrganizeTreeListJsonByLogin(string orgid)
        {
            // bool childflg = false;
            var data = organizeBLL.GetList().ToList();
            //var treeList = new List<TreeGridEntity>();
            List<OrganizeEntity> lstorg = new List<OrganizeEntity>();


            lstorg = Reorg(data, orgid);

            foreach (OrganizeEntity item in data)
            {
                if (item.OrganizeId == orgid)
                {
                    //childflg = true;
                    //}

                    //if (childflg)
                    //{
                    //    bool hasChildren = data.Count(t => t.ParentId == item.OrganizeId) == 0 ? false : true;
                    //if (hasChildren) continue;
                    //TreeGridEntity tree = new TreeGridEntity();

                    //    tree.id = item.OrganizeId;
                    //    //tree.hasChildren = hasChildren;
                    //    tree.parentId = item.ParentId;
                    //    tree.expanded = true;
                    //    tree.entityJson = item.ToJson();
                    //    treeList.Add(tree);
                    lstorg.Insert(0, item);
                }
            }
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(lstorg.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }

        /// <summary>
        /// 获取商品列表
        /// </summary> 
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetProductListJson()
        {
            var data = base_productbll.GetList("");
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }

        /// <summary>
        /// 用户列表
        /// </summary> 
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetUserListJson()
        {
            var watch = CommonHelper.TimerStart();
            var data = userBLL.GetList();
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回机构+部门+用户树形Json</returns>
        [HttpGet]
        public HttpResponseMessage GetUserTreeJson()
        {
            var organizedata = organizeCache.GetList();
            var departmentdata = departmentCache.GetList();
            var userdata = userCache.GetList();
            var treeList = new List<TreeEntity>();
            foreach (OrganizeEntity item in organizedata)
            {
                #region 机构
                TreeEntity tree = new TreeEntity();
                bool hasChildren = organizedata.Count(t => t.ParentId == item.OrganizeId) == 0 ? false : true;
                if (hasChildren == false)
                {
                    hasChildren = departmentdata.Count(t => t.OrganizeId == item.OrganizeId) == 0 ? false : true;
                    if (hasChildren == false)
                    {
                        continue;
                    }
                }
                tree.id = item.OrganizeId;
                tree.text = item.FullName;
                tree.value = item.OrganizeId;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.Attribute = "Sort";
                tree.AttributeValue = "Organize";
                treeList.Add(tree);
                #endregion
            }
            foreach (DepartmentEntity item in departmentdata)
            {
                #region 部门
                TreeEntity tree = new TreeEntity();
                tree.id = item.DepartmentId;
                tree.text = item.FullName;
                tree.value = item.DepartmentId;
                if (item.ParentId == "0")
                {
                    tree.parentId = item.OrganizeId;
                }
                else
                {
                    tree.parentId = item.ParentId;
                }
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                tree.Attribute = "Sort";
                tree.AttributeValue = "Department";
                treeList.Add(tree);
                #endregion
            }
            foreach (UserEntity item in userdata)
            {
                #region 用户
                TreeEntity tree = new TreeEntity();
                tree.id = item.UserId;
                tree.text = item.RealName;
                tree.value = item.Account;
                tree.parentId = item.DepartmentId;
                tree.title = item.RealName + "（" + item.Account + "）";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.Attribute = "Sort";
                tree.AttributeValue = "User";
                tree.img = "fa fa-user";
                treeList.Add(tree);
                #endregion
            }
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            //}
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(treeList.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
            // return Content(treeList.TreeToJson());
        }


        private void re(IEnumerable<UserEntity> lstu, string orgid)
        {
            var lst = lstu.Where(a => a.OrganizeId == orgid);

        }

        private List<OrganizeEntity> Reorg(List<OrganizeEntity> lstu, string orgid)
        {
            List<OrganizeEntity> lst = lstu.Where(a => a.ParentId == orgid).OrderBy(a => a.SortCode).ToList();
            foreach (OrganizeEntity ent in lst)
            {
                return lst.Union(Reorg(lstu, ent.OrganizeId)).ToList();
            }

            return lst;
        }
        public HttpResponseMessage GetUserTreeJsonByorgid(string orgid)
        {
            bool childflg = false;
            var organizedata = organizeCache.GetList();
            var departmentdata = departmentCache.GetList();
            var userdata = userCache.GetList();
            var treeList = new List<TreeEntity>();
            List<OrganizeEntity> lstorg = new List<OrganizeEntity>();
            var data = organizeBLL.GetList().ToList();


            lstorg = Reorg(data, orgid);

            foreach (OrganizeEntity item in data)
            {
                if (item.OrganizeId == orgid)
                {
                    lstorg.Insert(0, item);
                }
            }
            List<UserEntity> lstu = new List<UserEntity>();
            foreach (OrganizeEntity item in lstorg)
            {
                lstu = lstu.Union(userdata.Where(a => a.OrganizeId == item.OrganizeId)).ToList();
            }

            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(lstu.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
            // return Content(treeList.TreeToJson());
        }
        ///// <summary>
        ///// 辅助列表
        ///// </summary>
        ///// <param name="EnCode">代码</param>
        ///// <returns>返回树形列表Json</returns>
        //[HttpGet]
        //public HttpResponseMessage GetBaseItemListJson(string encode)
        //{
        //    //EnCode:Client_StyleCodeOption 花型
        //    //EnCode:Client_ColorCodeOption 色号
        //    //EnCode:Client_OpPositionCodeOption 施工位置
        //    //EnCode:Client_PaymentState 施工位置
        //    var data = busines.GetDataItemList().Where(t => t.EnCode == encode).OrderBy(t => t.ItemName);
        //    HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
        //    return ut;
        //}

        /// <summary>
        /// 辅助列表
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetBaseItemListJson(string queryJson)
        {
            //EnCode:Client_StyleCodeOption 花型
            //EnCode:Client_ColorCodeOption 色号
            //EnCode:Client_OpPositionCodeOption 施工位置
            //EnCode:Client_PaymentState 施工位置
            var encode = queryJson.ToJObject()["encode"].ToString();
            var keyword = queryJson.ToJObject()["keyword"].ToString();

            var data = busines.GetDataItemList().Where(t => t.EnCode == encode).OrderBy(t => t.ItemName);
            if (!string.IsNullOrEmpty(keyword))
            {
                data = busines.GetDataItemList().Where(t => t.EnCode == encode).Where(t => t.ItemCode == keyword).OrderBy(t => t.ItemName);
            }

            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };


            if (encode == "Client_StyleCodeOption")
            {
                var dataprice = priceBLL.GetList("");

                var sg = from c in data
                         join t in dataprice
                on new { id = c.ItemCode, id1 = c.ItemValue }
                equals new { id = t.ProductId, id1 = t.ItemDetailId }
                         select new
                         {
                             ItemDetailId = c.ItemDetailId,
                             ItemValue = c.ItemValue,
                             ItemName = c.ItemName,
                             Price = t.SalePrice == null ? 0 : t.SalePrice
                         };

                ut = new HttpResponseMessage { Content = new StringContent(sg.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            }

            return ut;
        }

        #region 实体类之间相同属性赋值

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
        #endregion
    }
}
