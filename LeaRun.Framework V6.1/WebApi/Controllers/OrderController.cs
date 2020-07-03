using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.CustomerManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Entity.AuthorizeManage.ViewModel;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace WebApi.Controllers
{
    public class OrderModelClass
    { 
        public string keyValue { get; set; }
        public string orderEntryJson { get; set; }
        public string orderJson { get; set; }
        public string receivableEntryJson { get; set; }
      //  public string drawbackEntryJson { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
    }
    public class OrderController : ApiController
    {
        private UserBLL userBLL = new UserBLL();
        private OrderBLL orderBLL = new OrderBLL();
        private OrderEntryBLL orderEntryBLL = new OrderEntryBLL();
        private Client_OrderDescriptionBLL orderdescriptionbll = new Client_OrderDescriptionBLL();
        
        Product_ColorBLL product_colorbll = new Product_ColorBLL();
        Product_StyleBLL product_stylebll = new Product_StyleBLL();
        PriceBLL priceBLL = new PriceBLL();
        // GET api/GetPageListJson
        /// <summary>
        /// 获取列表（订单表）
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetPageListJson(string queryJson, Pagination pagination)
        {
            try
            {
                var watch = CommonHelper.TimerStart();

                var data = orderBLL.GetPageList(pagination, queryJson);
                var jsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(jsonData.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                return ut;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { Content = new StringContent(ex.Message, Encoding.GetEncoding("UTF-8"), "application/json") };
            }
            // return ToJsonResult(jsonData);
        }

        [HttpGet]
        public HttpResponseMessage GetListJson(string queryJson)
        {
            try
            {
                HttpResponseMessage ut;
                var watch = CommonHelper.TimerStart();
                var loginid = queryJson.ToJObject()["loginid"].ToString();
                var user = userBLL.GetEntity(loginid);
                var datas = orderBLL.GetList(queryJson);
                var lstuid = new List<string>();


                if (loginid == null)
                {
                    return new HttpResponseMessage { Content = new StringContent("超时，请重新登录。", Encoding.GetEncoding("UTF-8"), "application/json") };
                }
                ut = new HttpResponseMessage { Content = new StringContent(datas.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                ////不是经理时，只能看到自己的数据
                if (!string.IsNullOrEmpty(user.ManagerId))
                {
                    lstuid = CommonMethod.GetUserList(loginid);

                    if (lstuid == null)
                    {
                        return new HttpResponseMessage { Content = new StringContent("当前用户ID不存在", Encoding.GetEncoding("UTF-8"), "application/json") };

                    }
                    var data = datas.Where(p => lstuid.Contains(p.SellerId));

                    ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                }

                return ut; 
             }
            catch
            {
                HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(new List<string>().ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                return ut;
            }
    // return ToJsonResult(jsonData);
}

        /// <summary>
        /// 获取列表（订单明细表）
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetOrderEntryListJson(string orderId)
        {
            var data = orderEntryBLL.GetList(orderId);
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }

        /// <summary>
        /// 获取实体 （主表+明细）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public HttpResponseMessage GetOrderFormJson(string orderId)
        {
            ReceivableBLL receivebll = new ReceivableBLL();
            var jsonData = new
            {
                order = orderBLL.GetEntity(orderId),
                orderEntry = orderEntryBLL.GetList(orderId),
                descriptionEntry = orderdescriptionbll.GetList_(orderId),
                receivableList = receivebll.GetPaymentRecord(orderId).Where(x => (x.PaymentPrice==null? 0 : x.PaymentPrice) > 0),
                drawbackList = receivebll.GetPaymentRecord(orderId).Where(x => (x.DrawbackPrice == null ? 0 : x.DrawbackPrice) > 0),
            };
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(jsonData.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }
        /// <summary>
        /// 获取色号列表
        /// </summary>
        /// <param name="ProductCode"></param> 
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetColorJson(string ProductCode)
        {
            var data = product_colorbll.GetList("");
            data = data.Where(t => t.ProductCode == ProductCode).OrderBy(t => t.ItemName);
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut; 
        }

        /// <summary>
        /// 获取造型列表
        /// </summary>
        /// <param name="ProductCode"></param> 
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetStyleJson(string ProductCode)
        { 
            var data = orderBLL.GetStyleList_("").Where(t => t.ProductCode == ProductCode).OrderBy(t => t.ItemName);
            //var dataprice = priceBLL.GetList("");

           
            //         join t in dataprice
            //on new { id = c.ProductCode, id1 = c.ItemValue }
            //equals new { id = t.ProductId, id1 = t.ItemDetailId }s
            //         select new
            //         {
            //             ProductName = c.ProductName,
            //             StyleId = c.StyleId,
            //             ItemName = c.ItemName,
            //             Price = t.SalePrice == null ? 0 : t.SalePrice
            //         };
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;

        }

        /// <summary>
        /// 保存订单表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="orderEntryJson">明细实体对象Json</param>
        /// <returns></returns>       
        [HttpPost]
        [Route("api/Order/SaveOrderFormP")]
        public string SaveOrderFormP([FromBody] OrderModelClass orderClassJson )
        {
            LogEntity logEntity = new LogEntity();
            try
            {
                var orderclassj =  orderClassJson ; 
                var entity = orderclassj.orderJson.ToObject<OrderEntity>();
                var orderEntryList = orderclassj.orderEntryJson.ToList<OrderEntryEntity>();
                var receivableEntryList = orderclassj.receivableEntryJson.ToList<ReceivableEntity>();
              //  var drawbackEntryList = orderclassj.drawbackEntryJson.ToList<ReceivableEntity>();

              //var  receivableEntryListP = receivableEntryList.Union(drawbackEntryList).ToList<ReceivableEntity>();
                //  var receivableEntryList = new  List< ReceivableEntity>().Add( receivableEntryJson.ToObject<ReceivableEntity>());
                // var orderDescriptionList = orderDescriptionJson.ToList<Client_OrderDescriptionEntity>();      
                orderBLL.SaveFormApi(orderclassj.userid, orderclassj.username, orderclassj.keyValue, entity, orderEntryList, receivableEntryList);

                return entity.OrderId;
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                throw ex;
            }

        }

        [HttpPost]
        [HttpGet]
        public string SaveOrderForm(string keyValue, string orderEntryJson, string orderJson, string receivableEntryJson, string userid, string username)
        {
            LogEntity logEntity = new LogEntity();
            try
            {
                var entity = orderJson.ToObject<OrderEntity>();
                var orderEntryList = orderEntryJson.ToList<OrderEntryEntity>();
                var receivableEntryList = receivableEntryJson.ToList<ReceivableEntity>(); 
                //  var receivableEntryList = new  List< ReceivableEntity>().Add( receivableEntryJson.ToObject<ReceivableEntity>());
                // var orderDescriptionList = orderDescriptionJson.ToList<Client_OrderDescriptionEntity>();      
                orderBLL.SaveFormApi(userid, username, keyValue, entity, orderEntryList, receivableEntryList);
                return entity.OrderId;
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                throw ex;
            }

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

        /// <summary>
        /// 上传照片
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage UploadOrderPhoto()
        {
            string userid = HttpContext.Current.Request["userid"];
            string username = HttpContext.Current.Request["username"];
            string orderid = HttpContext.Current.Request["orderid"];

            LogEntity logEntity = new LogEntity();
            try
            { 

                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //没有文件上传，直接返回
                if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
                {
                    //return NotFound();
                    return null;
                }

                StringBuilder lstfilenames = new StringBuilder();
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName)) { continue; }
                    string FileEextension = Path.GetExtension(files[i].FileName);
                    string filename = Guid.NewGuid().ToString("N");
                    string virtualPath = ""; 
                    virtualPath = string.Format("/Resource/PhotoFile/{0}{1}", filename, FileEextension);
                    
                    string fullFileName = System.Web.Hosting.HostingEnvironment.MapPath("~" + virtualPath);
                    //创建文件夹，保存文件
                    string path = Path.GetDirectoryName(fullFileName);
                    Directory.CreateDirectory(path);
                    files[i].SaveAs(fullFileName);
                    //OrderEntity orderEntity = new OrderEntity();
                    //orderEntity.PhotoPath = path;
                    //orderBLL.SaveFormApi(userid, username, orderid, orderEntity, null);
                    lstfilenames.Append(fullFileName);
                    lstfilenames.Append(",");
                }
                return new HttpResponseMessage{ Content = new StringContent(lstfilenames.ToString(), Encoding.GetEncoding("UTF-8"), "application/json") };
               // HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(jsonData.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
               // return ut;
                //  return lstfilenames.ToJson();
                //return Ok("上传成功。");
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                throw ex;
            }

        }

    }
}
