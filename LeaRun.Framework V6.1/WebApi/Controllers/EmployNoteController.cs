using LeaRun.Application.Busines.BaseManage;
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
    public class NoteModelClass
    {
        public string keyValue { get; set; }
        public string noteEntityJson { get; set; }
        public string content1 { get; set; }       
        public string userid { get; set; }
        public string username { get; set; }
    }
    public class EmployNoteController : ApiController
    {
        private Employ_NoteBLL employ_notebll = new Employ_NoteBLL();
        private UserBLL userBLL = new UserBLL();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetOrderPageListJson(string queryJson, Pagination pagination)
        {
            var watch = CommonHelper.TimerStart();
            var data = employ_notebll.GetPageList(pagination, queryJson);
            if (pagination == null)
            {
                pagination = new Pagination();
                pagination.rows = 20;
                pagination.page = 1;
            }
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

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param> 
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetNoteListJson(string queryJson)
        {

            var watch = CommonHelper.TimerStart();
            var loginid = queryJson.ToJObject()["loginid"].ToString();
            var datas = employ_notebll.GetList( queryJson);
            var user = userBLL.GetEntity(loginid);

            var lstuid = new List<string>();

            if (loginid == null)
            {
                return new HttpResponseMessage { Content = new StringContent("超时，请重新登录。", Encoding.GetEncoding("UTF-8"), "application/json") };
            }
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(datas.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };

            ////不是经理时，只能看到自己的数据
            if (!string.IsNullOrEmpty(user.ManagerId))
            {
                lstuid = CommonMethod.GetUserList(loginid);

                if (lstuid == null)
                {
                    return new HttpResponseMessage { Content = new StringContent("当前用户ID不存在", Encoding.GetEncoding("UTF-8"), "application/json") };

                }
                var data = datas.Where(p => lstuid.Contains(p.UserId));
                ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            } 
             
            return ut;
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public HttpResponseMessage GetEmployNoteJson(string keyValue)
        {
            var data = employ_notebll.GetEntity(keyValue);
            var childData = employ_notebll.GetDetails(keyValue);
            var jsonData = new { entity = data, childEntity = childData };
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(jsonData.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }
        /// <summary>
        /// 获取子表详细信息 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public HttpResponseMessage GetDetailsJson(string keyValue)
        {
            var data = employ_notebll.GetDetails(keyValue);
            HttpResponseMessage ut = new HttpResponseMessage { Content = new StringContent(data.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ut;
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/EmployNote/SaveEmployNote")]
        public void SaveEmployNote([FromBody] NoteModelClass noteClassJson)
        {
            var noteclassj = noteClassJson;
            var entity = noteclassj.noteEntityJson.ToObject<Employ_NoteEntity>(); 
            var childEntitys = new Employ_Note_DetailEntity();

            if (string.IsNullOrEmpty(noteclassj.keyValue))
            {
                entity.UserId = noteclassj.userid;// OperatorProvider.Provider.Current().UserId;
                entity.UserName = noteclassj.username;//.Provider.Current().UserName; 
            }
            else
            {
                entity.UserId = null;
                entity.UserName = null;
            }
            childEntitys.UserId = noteclassj.userid;// OperatorProvider.Provider.Current().UserId;
            childEntitys.UserName = noteclassj.username;//.Provider.Current().UserName; 
            childEntitys.Content1 = noteclassj.content1; 
            if( string.IsNullOrEmpty(noteclassj.content1))
            {
                employ_notebll.SaveFormApi(noteclassj.keyValue, entity, null);
            }
            else
            {
                employ_notebll.SaveFormApi(noteclassj.keyValue, entity, childEntitys);
            }
        }
        #endregion

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
