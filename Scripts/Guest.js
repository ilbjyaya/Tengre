var configurl = 'http://118.25.197.197:8089';
var HaveManage = false;
var ProductJson = [];
var stringStyleCodeJson='';
var IsApp = false;
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "H+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
    if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}


//http://118.25.197.197:8089
$(document).ready(function(e) {
	
	$("#PassGate").parent().html($("#PassGate").parent().html().replace('路过','进店'));
	$(".aui-list-item-middle .aui-list-item-title").html($(".aui-list-item-middle .aui-list-item-title").html().replace('路过','进店'));
		UserId =   localStorage.getItem("UserId");
     UserName =  localStorage.getItem("RealName");
	   Account= localStorage.getItem("Account");
	 OrganizeId = localStorage.getItem("OrganizeId");
	 SellerName = localStorage.getItem("SellerName");
	
	 $.growl({ title: "", message: "数据加载中",location:"middle" , duration:200});
    $("#StartDate").val((new Date().Format("yyyy-MM-01")));
	GetGuestList();	
	GetFlowList();
	
	$("#PassGate").bind("click",function()
	{
		$.ajax({
         		type:"get",
	               
				    url: configurl+"/api/Chance/GetFlowDetailListJson",
				   data:{queryJson:JSON.stringify({
                        loginid: UserId
                        , StartTime: $("#StartDate").val(), EndTime: $("#EndDate").val(),FlowName:'路过'
                    })  },

				Type:"GET",
                secureuri: false,
                dataType: 'json',
                success: function (data) {
                    console.log(data)
					var message = ''
					 $.each(data,function(item,value)
					  {
						  message = message+"<div style='margin-top:10px;color:#C00; font-size: medium;'>"+value.OrganizeName+':'+value.FlowCount+"</div>"
					  });
					
						 $.growl({ title: "进店数据统计", message: message,location:"middle" ,duration:5000 });
				
					
                }
                });
	})
 
   });
var UserId;
var Account;
var UserName;
var OrganizeId;
	function GetFlowList()
	{ 
		$.ajax({
         		type:"get",
	               url: configurl+"/api/Chance/GetFlowListJson",
				    //url: configurl+"/api/Chance/GetFlowDetailListJson",
				   data:{queryJson:JSON.stringify({
                        loginid: UserId
                        , StartTime: $("#StartDate").val(), EndTime: $("#EndDate").val(),FlowName:'备案'
                    })  },

				Type:"GET",
                secureuri: false,
                dataType: 'json',
                success: function (data) {
                    console.log(data)
					if(data.length>0)
					$("#PassGate").html(data[0].FlowCount)
				else
					$("#PassGate").html(0)
                }
                });
	}
   function GetGuestList()
   {

     var json = {loginid: UserId, ParamCommon:''
   ,StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),'':'',CompanyName:'',CustTypeId:'',Intention:'',IsToCustom:''};
    var stringJson = JSON.stringify(json);

     $.ajax({
         		type:"get",
         		url:configurl+"/api/Chance/GetChanceListJson",
         		data: { queryJson:stringJson},
         		async:true,
         		dataType: "json",
              success: function (data)
              {
				console.log(data)
                $(".AddTable tr").not(':eq(0)').remove()
				$(".AddTable").css('font-size','13px')
				
                Intention = 0
                NotToCustomer = 0
                ToCustomer = 0
                Record = 0
				$.each(data,function(item,value)
				{
					if(value.Intention=='A' || value.Intention=='B')
					{
						Intention = Intention+1;
					}
					if(value.IsToCustom==1)
					{
						ToCustomer = ToCustomer+1;
					}
					if(value.IsToCustom==0)
					{
						NotToCustomer = NotToCustomer+1;
					}
					Record = Record+1;
				});
				
				$("#Intention").html(Intention)
				$("#NotToCustomer").html(NotToCustomer)
				$("#ToCustomer").html(ToCustomer)
				$("#Record").html(Record)
			  },
			  error: function (XMLHttpRequest, textStatus, errorThrown) {
				  console.log(textStatus)
				  console.log(errorThrown);
			  }
		});


   }

   var popup = new auiPopup();
       function showPopup(){
           popup.show(document.getElementById("Screening"));
       }
   //清空input
   function Reset(){
      for(var i=0;i<document.all.length;i++){
       if(document.all[i].type == "text" || document.all[i].type == "date"||document.all[i].type == "radio")
       {
         document.all[i].value = "";
       }
       }
   }
   
   function ADDLG()
   {
	    if (confirm("您确认要新增进店人次吗？"))
		{
			var flowJsonO= {    "FlowName": '路过' };//object类型 
			var flowJson = JSON.stringify(flowJsonO);//string类型  
			flowclass={flowJson:flowJson,  userid: UserId, username: UserName};    
			$.ajax({
				url: configurl+"/api/Chance/SaveFlowForm",
                data :flowclass, 
				type:'POST',
                dataType: 'json',
                success: function (data) {
                    console.log(data)
					GetGuestList();	
					GetFlowList();
                }
                });
		}
   }
   var OrderId=''
   //Add
   function Add(){
	   
	 
     document.getElementById("Add").style.display="block";
     document.getElementById("Daui-Scree").style.display="none";
     $(".aui-title").text("新增备案");
     OrderId = '';
	 totletr = '';
	 $('#Add :input').attr("disabled",false);
	 $('#Add :input').val("");
	 $("input[type=radio][name=CustTypeId][data-text=设计师]").prop('checked',true);
	 $("#IsToCustom").prop("checked",false);
	
   }
 
var totletr = '';
   function Modify(paraOrderId){
     document.getElementById("Add").style.display="block";
     document.getElementById("Daui-Scree").style.display="none";
     $(".aui-title").text("编辑备案");
      OrderId = paraOrderId;
	  
	  $.ajax(
	  {
			type:"get",
			url:configurl+"/api/Chance/GetChanceFormJson",
			data: { keyValue:OrderId},
			async:true,
			dataType: "json",
              success: function (data)
              {
               console.log(data)
				$("#CompanyName").val(data.CompanyName);
				$("#CompanyAddress").val(data.CompanyAddress);
				$("#Mobile").val(data.Mobile);
				$("#Wechat").val(data.Wechat);
				$("input[type=radio][name=CustTypeId][data-text="+data.CustTypeId+"]").prop('checked',true);
				
				$("#Intention").val(data.Intention);
				if(data.IsToCustom==1)
					$("#IsToCustom").prop("checked",true);
				else
					$("#IsToCustom").prop("checked",false);
				$("#Description").val(data.Description);
				
			  },
			error: function (XMLHttpRequest, textStatus, errorThrown) {
				   console.log(textStatus);
				   console.log(errorThrown);
			}
			
			
	  });
			
	  
	  
   }

      function Cancel(){
    $("#Daui-Scree").trigger("click")
   }
		function Query()
		{
			 $("#Daui-Scree").trigger("click")
			
			GetGuestList();	
			 GetFlowList();
		}


  
 