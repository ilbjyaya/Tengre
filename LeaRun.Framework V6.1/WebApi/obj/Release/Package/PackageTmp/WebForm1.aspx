<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApi.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--框架必需start-->
    <link href="Content/scripts/plugins/jquery-ui/jquery-ui.min.css" rel="stylesheet" />
    <link href="Content/scripts/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <script src="Content/scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="Content/scripts/plugins/jquery-ui/jquery-ui.min.js"></script>
    <script src="Content/scripts/plugins/uploadify/ajaxfileupload.js"></script>
    <!--框架必需end-->
    <!--bootstrap组件start-->
    <link href="Content/scripts/bootstrap/bootstrap.extension.css" rel="stylesheet" />
    <script src="Content/scripts/bootstrap/bootstrap.min.js"></script> 
    <link href="Content/scripts/plugins/tree/tree.css" rel="stylesheet" />
    <script src="Content/scripts/plugins/tree/tree.js"></script>
    <script> 
          //销售人员
        $("#SellerId").ComboBoxTree({
            url: "../../BaseManage/User/GetTreeJson",
            description: "==请选择==",
            height: "360px",
            width: "280px",
            allowSearch: true
        });

        function uploadfile() {
            var imgfiles = [];
            imgfiles.push('uploadFile');
            imgfiles.push('uploadFile1');
            imgfiles.push('uploadFile2');


            //上传应用图标
            $.ajaxFileUpload({
                url: "http://123.206.255.74:8089/api/Order/UploadOrderPhoto",
                data: { userid: '2222', username: '3333', orderid: '' },
                secureuri: false,
                fileElementId: imgfiles,//['uploadFile', 'uploadFile1', 'uploadFile2'],
                dataType: 'json',
                 async:false,
                 
                success: function (data) {
                    dialogMsg(data, 1);
                }
            });

        }


        function OrderApi(btn) {
           var url = "http://localhost:8644/api/Order/";
            //var url = "http://116.3.200.209:8087/api/Order/";  //ilbjpy
            //var url = "http://123.206.255.74:8089/api/Order/";           //云服务器
            var orderJsonO, orderJson, orderEntryJsonO, orderEntryJson, paradata, atype,orderClassJson;
            var ctlname = btn.id;
            url = url + ctlname;

            switch (ctlname) {
                case "GetListJson":
                    orderJsonO = {
                        loginid: '7cf7280f-0111-49d6-ad76-74beaeefab3b', ParamCommon: ''
                        , StartTime: '2017-01-01', EndTime: '2018-09-01', SellerName: '销售员'
                        , CompanyName: '门店', OrderStatusName: ''
                    };
                    queryJson = JSON.stringify(orderJsonO);//string类型 
                    paradata = { queryJson };
                    atype = "GET";
                    break;

                case "GetOrderFormJson":
                    paradata = { orderId: 'ed151945-b409-4e1b-afc6-d142ce311516' };
                    atype = "GET";
                    break;
                case "SaveOrderForm":
                    var orderJsonO = {"CustomerName":"","ContecterTel":"","DistrictAddress":"","OpAddress":"","Accounts":"","Description":"","OrderStatusName":"未完工","SellerId":"28c0e59b-c5f0-4bd5-924e-db940790e49a","SellerName":"test","OrderDate":"","PhotoPath":""};//订单obj       
                    var orderJson = JSON.stringify(orderJsonO);//订单string
                    var orderEntryJsonO = [{ "ProductName": 'ProductName1' }, { "ProductName": 'ProductName1' }];//订单详细obj
                    var orderEntryJson = JSON.stringify(orderEntryJsonO);//订单详细string 
                    var receivableEntryJsonO = [{
                        "ReceivableId": "",
                        "OrderId": "",
                       
                        "PaymentPrice": "99",
                        "Description": "回款描述" 
                    },
                    {
                        "ReceivableId": "",
                        "OrderId": "",
                       
                        "PaymentPrice": "88",
                        "Description": "回款描述" 
                    }];//回款obj    

                    var receivableEntryJson =JSON.stringify(receivableEntryJsonO);//回款string 
                    paradata = { userid: '28c0e59b-c5f0-4bd5-924e-db940790e49a', username: 'test', keyValue: '', orderJson, orderEntryJson, receivableEntryJson  };
                    atype = "GET";
                    break; 
                     case "SaveOrderFormP":
                    var orderJsonO = {"CustomerName":"客户信息","ContecterTel":"123456789012","DistrictAddress":"小区名称","OpAddress":"详细地址","Accounts":"30000","Description":"第一次备注","OrderStatusName":"未完工","SellerId":"28c0e59b-c5f0-4bd5-924e-db940790e49a","SellerName":null,"OrderDate":"","PhotoPath":""};//订单obj       
                    var orderJson = JSON.stringify(orderJsonO);//订单string
                    var orderEntryJsonO = [{ "ProductName": 'ProductName1' }, { "ProductName": 'ProductName1' }];//订单详细obj
                    var orderEntryJson = JSON.stringify(orderEntryJsonO);//订单详细string 
                    var receivableEntryJsonO = [
                    {
                        "ReceivableId": "",
                        "OrderId": "",
                        "PaymentTime": "2018-08-09 18:10:20",
//                        "CreateDate": "2018-08-14 22:14:53.7162611",
                        "PaymentPrice": "11",
                        "Description": "回款描述" 
                    },
                    {
                        "ReceivableId": "",
                        "OrderId": "",
                        "PaymentTime": "2018-08-09 18:10:20",
//                        "CreateDate": "2018-08-14 22:15:53.7162611",
                        "PaymentPrice": "12",
                        "Description": "回款描述" 
                    },
                    {
                        "ReceivableId": "",
                        "OrderId": "",
                        "PaymentTime": "2018-08-09 18:10:20",
                        "PaymentPrice": "99",
                        "Description": "回款描述" 
                    },
                    {
                        "ReceivableId": "",
                        "OrderId": "",
                        "PaymentTime": "2018-08-09 18:10:20",
                        "PaymentPrice": "88",
                        "Description": "回款描述" 
                    }];//回款obj    

                    var receivableEntryJson =JSON.stringify(receivableEntryJsonO);//回款string 
                    orderClassJson =  { userid: '28c0e59b-c5f0-4bd5-924e-db940790e49a', username: 'test', keyValue: '', orderJson, orderEntryJson:[], receivableEntryJson:[]  };
                    paradata = orderClassJson;
                    atype = "POST";
                    break;
            }
            $.ajax({
                url: url,
                data:paradata,//{ userid: 'system', username: 'username', keyvalue: '3694d3b2-424e-4d91-b7e4-98ab03b1bce7', orderJson, orderEntryJson },
                type: atype,
                dataType: "json",
                async:false,
                success: function (data) {
 
                    console.log("同步成功");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        }

        function BaseApi(btn) {
            var url = "http://localhost:8644/api/Base/";
           // var url = "http://116.3.200.209:8087/api/Base/";  //ilbjpy
           // var url = "http://123.206.255.74:8089/api/Base/";           //云服务器
            var orderJsonO, orderJson, orderEntryJsonO, orderEntryJson, paradata, atype;
            var ctlname = btn.id;
            url = url + ctlname;

            switch (ctlname) {
                case "GetOrganizeTreeListJson":
                    atype = "GET";
                    break;
                case "GetOrganizeTreeListJsonByLogin":
paradata = { orgid: '207fa1a9-160c-4943-a89b-8fa4db0547ce' };
                    atype = "GET";
                    break;
                case "GetProdcutlistjson":
                    atype = "GET";
                    break;
                case "GetUserListJson":
                    atype = "GET";
                    break;
                case "GetUserTreeJson":
                   // paradata = { keyword: '53298b7a-404c-4337-aa7f-80b2a4ca6681' };
                    atype = "GET";
                    break;
 case "GetUserTreeJsonByorgid":
                    paradata = { orgid: '207fa1a9-160c-4943-a89b-8fa4db0547ce' };
                    atype = "GET";
                    break;
                case "GetBaseItemListJson":
                    paradata = { encode: 'Client_OpPositionCodeOption' };
                    atype = "GET";
                    break;
 case "GetBaseItemListJson":
                    paradata = {queryJson:{ 
UserName:'',
StartTime:'',
EndTime:''}  };
                    atype = "GET";
                    break;

            }
            $.ajax({
                url: url,
                data: paradata,//{ userid: 'system', username: 'username', keyvalue: '3694d3b2-424e-4d91-b7e4-98ab03b1bce7', orderJson, orderEntryJson },
                type: atype,
                dataType: "json",
                success: function (data) {
                    console.log("同步成功");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        }


        function EmployNote(btn) {
           // var url = "http://localhost:8644/api/EmployNote/";
           //var url = "http://116.3.200.209:8087/api/EmployNote/";  //ilbjpy
           var url = "http://123.206.255.74:8089/api/EmployNote/";           //云服务器
            var orderJsonO, orderJson, orderEntryJsonO, orderEntryJson, paradata, atype;
            var ctlname = btn.id;
            url = url + ctlname;

            switch (ctlname) {
               
 case "GetNoteListJson":
                    paradata = {queryJson:JSON.stringify({ UserName:'',StartTime:'',EndTime:''})  };
                    atype = "GET";
                    break;
 case "GetEmployNoteJson":
                    paradata = {keyValue:'8b14baf6-4865-4b80-87bd-a10dbd75f79b'  };
                    atype = "GET";
                    break;
 case "SaveEmployNote":
                     var noteEntityJsonO= {  
                                            "Content1": 'Content1' ,
                                            "Content2": 'Content2' ,
                                            "Content3": 'Content3' ,
                                            "Content4": 'Content14' ,
                                            "UserId": 'UserId' ,
                                            "UserName": 'UserName' 
 
                                            };//object类型
 
                    var noteEntityJson = JSON.stringify(noteEntityJsonO);//string类型 

                    paradata = {userid: 'system', username: 'username', keyvalue: '', noteEntityJson, content1:''};
                    atype = "GET";
                    break;
            }
            $.ajax({
                url: url,
                data: paradata,//{ userid: 'system', username: 'username', keyvalue: '3694d3b2-424e-4d91-b7e4-98ab03b1bce7', orderJson, orderEntryJson },
                type: atype,
                dataType: "json",
                success: function (data) {
                    console.log("同步成功");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        }

         function Chance(btn) {
            var url = "http://localhost:8644/api/Chance/";
           // var url = "http://116.3.200.209:8087/api/Chance/";  //ilbjpy
            //var url = "http://123.206.255.74:8089/api/Chance/";           //云服务器
            var orderJsonO, orderJson, orderEntryJsonO, orderEntryJson, paradata, atype;
            var ctlname = btn.id;
            url = url + ctlname;

            switch (ctlname) {
               
                    case "GetChanceListJson":
                    paradata = {queryJson:JSON.stringify({
                        loginid: 'system', ParamCommon: ''
                        , StartTime: '', EndTime: '', SellerName: '', CompanyName: '', CompanyNatureId:'',SuccessRate:'',IsToCustom:''
                    })  };
                    atype = "GET";
                    break;
 case "GetChanceFormJson":
                    paradata = {keyValue:'f35b9cac-444b-45ae-9f1d-bb3d75306a45'  };
                    atype = "GET";
                    break;
 case "SaveChanceForm":
                     var chanceJsonO= {  
                                            "CompanyName": '客户信息' ,                                            
                                            "CompanyAddress": '地址' ,
                                            "Mobile": '12345' ,                                            
                                            "Wechat": '微信号 ' ,  
                                            "CompanyNatureId": '客户类型 ' ,  
                                            "IsToCustom": '1' ,
                                            "SuccessRate": '50' ,
                                            "Description": '备注 ' ,                                       
                                            "TraceUserId": '跟进人员ID' ,
                                            "TraceUserName": '跟进人员姓名' 
 
                                            };//object类型
 
                    var chanceJson = JSON.stringify(chanceJsonO);//string类型 

                //  chanceJson=  JSON.stringify( {"CompanyName":"1","CompanyAddress":"2","Mobile":"3","Wechat":"4","CompanyNatureId":"0","IsToCustom":0,"SuccessRate":"","Description":"5"});

                    paradata = {userid: '73f43f0a-e632-4a96-ad3d-52a0ed257f2c', username: '门店1店长', keyvalue: '6fc88d03-d20b-4b92-a124-358ae0521699', chanceJson};
                    atype = "GET";
                    break;
                     case "SaveChanceFormP":
                     var chanceJsonO= {  
                                            "CompanyName": '客户信息' ,                                            
                                            "CompanyAddress": '地址' ,
                                            "Mobile": '12345' ,                                            
                                            "Wechat": '微信号 ' ,  
                                            "CompanyNatureId": '客户类型 ' ,  
                                            "IsToCustom": '1' ,
                                            "SuccessRate": '50' ,
                                            "Description": '备注 ' ,                                       
                                            "TraceUserId": '跟进人员ID' ,
                                            "TraceUserName": '跟进人员姓名' 
 
                                            };//object类型
 
                    var chanceJson = JSON.stringify(chanceJsonO);//string类型 

                 // chanceJson=  JSON.stringify( {"CompanyName":"1","CompanyAddress":"2","Mobile":"3","Wechat":"4","CompanyNatureId":"0","IsToCustom":0,"SuccessRate":"","Description":"5"});

                 chanceclassJson={chanceJson, keyvalue: '', userid: '73f43f0a-e632-4a96-ad3d-52a0ed257f2c', username: '门店1店长'};
                    paradata= chanceclassJson;
                    atype = "POST";
                    break;
            } 
           
            $.ajax({
                url: url,
                data: paradata,//{ userid: 'system', username: 'username', keyvalue: '3694d3b2-424e-4d91-b7e4-98ab03b1bce7', orderJson, orderEntryJson },
                type: atype,
                dataType: "json",
                success: function (data) {
                    console.log("同步成功");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        } 
        

         function Report(btn) {
            var url = "http://localhost:8644/api/Report/";
           // var url = "http://116.3.200.209:8087/api/Report/";  //ilbjpy
            var url = "http://123.206.255.74:8089/api/Report/";           //云服务器
            var orderJsonO, orderJson, orderEntryJsonO, orderEntryJson, paradata, atype;
            var ctlname = btn.id;
            url = url + ctlname;

            switch (ctlname) {
               
                    case "GetSalesJson":
                    paradata = {queryJson:JSON.stringify({ UserId: '7d4aeda1-a849-48b0-9ee3-44dc35e5b300',
                        RptAccountType: '1', RptSellerType: '2'
                        , StartTime: '', EndTime: '',OpFinishFLg:''
                    })  };
                    atype = "GET";
                    break;
            case "GetChanceFormJson":
                    paradata = {keyValue:'8b14baf6-4865-4b80-87bd-a10dbd75f79b'  };
                    atype = "GET";
                    break; 
            }
            $.ajax({
                url: url,
                data: paradata,//{ userid: 'system', username: 'username', keyvalue: '3694d3b2-424e-4d91-b7e4-98ab03b1bce7', orderJson, orderEntryJson },
                type: atype,
                dataType: "json",
                success: function (data) {
                    console.log("同步成功");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        } 
        

        //修改密码
        function SubmitResetPassword()
        {
         $.ajax({
                url: "http://localhost:8644/api/Login/SubmitResetPassword",
                data: { userid: 'f3c1ef6f-c8c7-49d4-b037-6cdd8e123666', password: '5b00aa719b8d70aa96ac49002e868ced', oldpassword: '0000', newpassword: '1234', confirmpassword:'1234',secretkey:'cb0d8213f3d570c8'  },

                type: "GET",
                dataType: "json",
                success: function (data) {
                    console.log("同步成功");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        }

         function login()
            {
             $.ajax({
                    url: "http://localhost:8644/api/Login/CheckLoginJson",
                    data: { username: 'shanghai', password: '1234' },
                     type: "GET",
                    dataType: "json",
                    success: function (data) {
                        console.log("同步成功");
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(textStatus);
                    }
                });
            }


        function test() {
            orderJsonO = {
                "CustomerName": 'CustomerName'
            };
            orderJson = JSON.stringify(orderJsonO);//string类型
            orderEntryJsonO = [{ "ProductName": 'api测试商品32' }, { "ProductName": 'api测试商品42' }];//object类型
            orderEntryJson = JSON.stringify(orderEntryJsonO);//string类型 

            $.ajax({
                url: "http://123.206.255.74:8089/api/Order/SaveOrderForm",
                data: { userid: 'system', username: 'username', keyValue: '', orderJson, orderEntryJson },

                type: "GET",
                dataType: "json",
                success: function (data) {
                    console.log("同步成功");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        }
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style2
        {
            width: 321px;
        }
        
        .auto-style3
        {
            width: 421px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td class="formValue">
                <div id="SellerId" type="selectTree" class="ui-select" isvalid="yes">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                OrderController
            </td>
            <td class="auto-style2">
                <div class="btn-group">
                    <div class="btn btn-default" id="GetListJson" onclick="OrderApi(this)">
                        获取订单列表GetListJson</div>
                    <div class="btn btn-default" id="GetOrderFormJson" onclick="OrderApi(this)">
                        获取订单详细GetOrderFormJson</div>
                    <div class="btn btn-default" id="SaveOrderForm" onclick="OrderApi(this)">
                        保存订单SaveOrderForm</div>
                    <div class="btn btn-default" id="SaveOrderFormP" onclick="OrderApi(this)">
                        POST 保存订单SaveOrderFormP</div>
                    <div class="btn btn-default" id="test" onclick="test()">
                        测试</div>
                </div>
                <div class="btn-group">
                    <div class="btn btn-default" id="GetProductListJson" onclick="BaseApi(this)">
                        商品列表</div>
                    <div class="btn btn-default" id="GetUserListJson" onclick="BaseApi(this)">
                        用户列表全部</div>
                    <div class="btn btn-default" id="GetOrganizeTreeListJson" onclick="BaseApi(this)">
                        公司列表</div>
                    <div class="btn btn-default" id="GetOrganizeTreeListJsonByLogin" onclick="BaseApi(this)">
                        公司列表根据登录信息</div>
                    <div class="btn btn-default" id="GetUserTreeJson" onclick="BaseApi(this)">
                        用户列表</div>
                    <div class="btn btn-default" id="GetUserTreeJsonByorgid" onclick="BaseApi(this)">
                        用户列表根据登录信息</div>
                    <div class="btn btn-default" id="GetBaseItemListJson" onclick="BaseApi(this)">
                        辅助列表</div>
                </div>
                <div class="btn-group">
                    <div class="btn btn-default" id="GetNoteListJson" onclick="EmployNote(this)">
                        日志列表</div>
                    <div class="btn btn-default" id="GetEmployNoteJson" onclick="EmployNote(this)">
                        日志表单</div>
                    <div class="btn btn-default" id="SaveEmployNote" onclick="EmployNote(this)">
                        保存日志表单</div>
                </div>
                <div class="btn-group">
                    <div class="btn btn-default" id="GetChanceListJson" onclick="Chance(this)">
                        备案列表</div>
                    <div class="btn btn-default" id="GetChanceFormJson" onclick="Chance(this)">
                        备案表单</div>
                    <div class="btn btn-default" id="SaveChanceForm" onclick="Chance(this)">
                        备案更新</div>
                   <div class="btn btn-default" id="SaveChanceFormP" onclick="Chance(this)">
                        备案更新P</div>
                </div>
                <div class="btn-group">
                    <div class="btn btn-default" id="GetSalesJson" onclick="Report(this)">
                        销售报表</div>
                </div>
                <div class="btn-group">
                    <div class="btn btn-default" id="SubmitResetPassword" onclick="SubmitResetPassword()">
                        修改密码</div>
                </div>
                <div class="btn-group">
                    <div class="btn btn-default" id="btnlogin" onclick="login()">
                        登录</div>
                </div>
            </td>
            <td class="auto-style3">
                <div>
                    <div style="margin-top: 40px; text-align: center;">
                        <div class="file" style="width: 100px; height: 100px;">
                            <img id="uploadPreview" style="width: 100px; height: 100px; border-radius: 100px;"
                                src="~/Content/images/logo-headere47d5.png" />
                            <input type="file" name="uploadFile" id="uploadFile" />
                        </div>
                        <div class="file" style="width: 100px; height: 100px;">
                            <img id="uploadPreview1" style="width: 100px; height: 100px; border-radius: 100px;"
                                src="~/Content/images/logo-headere47d5.png" />
                            <input type="file" name="uploadFile1" id="uploadFile" />
                        </div>
                        <div class="file" style="width: 100px; height: 100px;">
                            <img id="uploadPreview2" style="width: 100px; height: 100px; border-radius: 100px;"
                                src="~/Content/images/logo-headere47d5.png" />
                            <input type="file" name="uploadFile2" id="uploadFile" />
                        </div>
                        <div id="btnuploadFile" style="margin-top: 30px; line-height: 14px; color: #75777A;
                            text-align: center;" onclick="uploadfile()">
                            开始上传
                        </div>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </form>
</body>
</html>
