var configurl = 'http://123.206.255.74:8089';
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

if(window.plus){
	//plusReady();
}else{
	//document.addEventListener('plusready',plusReady,false);
}

function plusReady(){
	   
	 IsApp == true;
	 
	  UserId =   plus.navigator.getCookie('UserId');
	  Account=plus.navigator.getCookie('Account');
     UserName =   plus.navigator.getCookie('RealName');
	 OrganizeId = plus.navigator.getCookie('OrganizeId');
	  HaveManage = plus.navigator.getCookie('ManagerId')
	 HaveManage = (HaveManage==null ||HaveManage=="" )?false:true;
	 SellerName =  plus.navigator.getCookie('RealName');

	
}

//http://123.206.255.74:8089
$(document).ready(function(e) {
	console.log($("#AddTable").html())
	$(".AddTable tr").eq(0).find("th").eq(1).after('<th style="text-align: center;">备案人</th>');
	  bodyHeight = $(window).height();
	headerHeight  = $('header').height()
	footerHeight = $('footer').height()
	var addAreaHeight = $("#operateArea").height();
	
	listheight =  $("#listContainer").height()
	divHeight = bodyHeight-headerHeight-footerHeight-addAreaHeight+listheight
	$("#listContainer").css('height',divHeight+'px');

	//$(".aui-content").removeClass("aui-margin-b-15")
//$("#ContentList").removeClass("aui-margin-b-15")
$(".aui-popup-content").css('padding-bottom','1rem')
	
    $("#StartDate").val((new Date().Format("yyyy-MM-01")));

		UserId =   localStorage.getItem("UserId");
     UserName =  localStorage.getItem("RealName");
	   Account= localStorage.getItem("Account");
	 OrganizeId = localStorage.getItem("OrganizeId");
	 SellerName = localStorage.getItem("SellerName");
	
	 $.growl({ title: "", message: "数据加载中",location:"middle" });
    
	GetRecordList();
	GetOrganizeTreeListJsonByLogin();
	
	/* $('#Add :input').css('opacity',1)
					 $('#Add :input').css('color','black')
					 $('#Add :input').css('-webkit-opacity',1)	
					 $('#Add :input').css('-webkit-text-fill-color','black') */
 
   });
var UserId;
var Account;
var UserName;
var OrganizeId;
   function GetRecordList()
   {

   var CompanyNameFilter = $("#SellerNameDept").find("option:selected").text()=="请选择"?'':$("#SellerNameDept").find("option:selected").text()
var SellerNameFilter = $("#SellerNamePerson").find("option:selected").text()=="请选择"?'':$("#SellerNamePerson").find("option:selected").text()
var IsToCustomValue = $("#FilterIsToCustom").val();
var FilterIntention = $("#FilterIntention").val();
var FilterCustTypeId = $("#FilterCustTypeId").val();
   var conditioncommon =  $("#CommonCondition").val();
     var json = {loginid: UserId, ParamCommon:conditioncommon
   ,StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),SellerName:SellerNameFilter,CompanyName:CompanyNameFilter,CustTypeId:FilterCustTypeId,Intention:FilterIntention,IsToCustom:IsToCustomValue};
    var stringJson = JSON.stringify(json);
console.log(stringJson)
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
				$.each(data,function(item,value)
				{
					var IsSuccessResult = '';
					if(value.IsToCustom==1)
					{
						IsSuccessResult='成交'
					}
					else
					{
						IsSuccessResult='未成交'
					}
	
                  $(".AddTable").append("<tr onClick=Modify('"+value.ChanceId+"') ><td >"+value.CompanyName+"</td><td >"+value.CustTypeName+"</td><td>"+value.CreateUserName+"</td><td>"+IsSuccessResult+"</td></tr>");
				});
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
   var OrderId=''
   //Add
   function Add(){
	   
	 
     document.getElementById("Add").style.display="block";
     document.getElementById("Daui-Scree").style.display="none";
     $(".aui-title").text("新增备案");
     OrderId = '';
	 totletr = '';
	  $('#Add :input').removeAttr("readonly");
	 $('#Add :input').val("");
	 $("input[type=radio][name=CustTypeId][data-text=设计师]").prop('checked',true);
	 $("#IsToCustom").prop("checked",false);
	$(".aui-popup-content #listcomment").remove()
   }
 
 
var totletr = '';
   function Modify(paraOrderId){
     document.getElementById("Add").style.display="block";
     document.getElementById("Daui-Scree").style.display="none";
     $(".aui-title").text("编辑备案");
      OrderId = paraOrderId;
	  $('#Add :input').removeAttr("readonly");
	  $.ajax(
	  {
			type:"get",
			url:configurl+"/api/Chance/GetChanceFormJson",
			data: { keyValue:OrderId},
			async:false,
			dataType: "json",
              success: function (data)
              {
               console.log(data)
				$("#CompanyName").val(data.chance.CompanyName);
				if(data.chance.CompanyName!="")
				{
					$("#CompanyName").attr("readonly","readonly");
				}
				$("#CompanyAddress").val(data.chance.CompanyAddress);
				if(data.chance.CompanyAddress!="")
				{
					$("#CompanyAddress").attr("readonly","readonly");
				}
				$("#Mobile").val(data.chance.Mobile);
				if(data.chance.Mobile!="")
				{
					$("#Mobile").attr("readonly","readonly");
				}
				$("#Wechat").val(data.chance.Wechat);
				if(data.chance.Wechat!="")
				{
					$("#Wechat").attr("readonly","readonly");
				}
 
				
				$("input[type=radio][name=CustTypeId][data-text="+data.chance.CustTypeId+"]").prop('checked',true);
				
				$("#Intention").val(data.chance.Intention);
				if(data.chance.IsToCustom==1)
					$("#IsToCustom").prop("checked",true);
				else
					$("#IsToCustom").prop("checked",false);
				$("#Description").val('');
				
				$(".aui-popup-content #listcomment").remove()
				
				var ulcomment=''
				$.each(data.descriptionEntry,function(item,value)
				{
					ulcomment = ulcomment+'<ul class="commentDiv" ><li >备注:'+value.DescriptionContent+'(备注人:'+value.CreateUserName+')<div class="commentTime" >时间:'+value.CreateDate+'</div></li></ul>'
				})
				ulcomment=" <div id='listcomment' class='aui-content aui-margin-b-15' ><ul class='aui-list aui-form-list'><li class='aui-list-header aui-Childtitle'>备注列表</li></ul>"+ulcomment+"</div>"
				$(".aui-popup-content").append(ulcomment);
				console.log($("#listcomment").html());
				
				//
				
			  },
			error: function (XMLHttpRequest, textStatus, errorThrown) {
				   console.log(textStatus);
				   console.log(errorThrown);
			}
			
			
	  });
			
	  
	  
   }

   //Cancel
   function Cancel(){
     document.getElementById("Add").style.display="none";
     document.getElementById("Daui-Scree").style.display="block";
	  $(".aui-title").text("备案查询");
   }
   
   function CloseQuery()
   {
	   $("#Daui-Scree").trigger("click")
	    $(".aui-title").text("备案查询");
   }
  
  
   //提交
   function msg(){
	   if (confirm("您确认要提交吗？"))   
		{
			var IsToCustomValue = $("#IsToCustom").prop('checked')==false?0:1;
			var CustTypeId = $("input[name=CustTypeId]:checked").attr('data-text');
			var chanceJsonO ={CompanyName: $("#CompanyName").val() ,CompanyAddress: $("#CompanyAddress").val() ,Mobile: $("#Mobile").val() ,'Wechat': $("#Wechat").val() , 'CustTypeId': CustTypeId , 
			IsToCustom: IsToCustomValue ,Intention: $("#Intention").val() ,Description: $("#Description").val(),TraceUserId:'',TraceUserName:''}
			
			var chanceJson = JSON.stringify(chanceJsonO)
			$.ajax({
				url:  configurl+"/api/Chance/SaveChanceForm",
				data: { userid: UserId, username: UserName, keyValue: OrderId,chanceJson:chanceJson},
				async:true,
				type: "get",
				dataType: "json",
				success: function (data) {
				  
				  $.growl.notice({ title: "提示", message: "备案提交完成!",location:"middle", duration:1500});
					   console.log("同步成功")
					   Cancel();
					   GetRecordList();

				},
				error: function (XMLHttpRequest, textStatus, errorThrown) {
				  Cancel();
					   console.log(textStatus);
					   console.log(errorThrown);
				}
			});
			
	   }
   }
   
   //门店数据
	function GetOrganizeTreeListJsonByLogin()
	{
		  
		$.ajax({ 
			url: configurl+"/api/Base/GetOrganizeTreeListJsonByLogin",                      
			type: "GET",
			async:true,
			data:{orgid:OrganizeId},
			dataType: "json",
			success: function (data) {
				
				$.growl.close();
				$("#SellerNameDept").bind("change",function()
				{
					GetUserTreeJson($("#SellerNameDept").val())
				})
				
				$.each(data,function(item,value)
				{
					
						$("#SellerNameDept").append("<option value='"+value.OrganizeId+"'>"+value.FullName+"</option>")
					
					
					
				})
			},
			error: function (XMLHttpRequest, textStatus, errorThrown) {
				console.log(textStatus);
			} 
		}); 
		
	}
      
	  //用户数据
	function GetUserTreeJson(paraOrganizeId)
	{
		$.ajax({ 
			url: configurl+"/api/Base/GetUserTreeJsonByorgid",                      
			type: "GET",
			data:{orgid:paraOrganizeId},
			dataType: "json",
			async:true,
			success: function (data) {
				var countItem = 0;
		
				$("#SellerNamePerson option").not(':eq(0)').remove();
				$.each(data,function(item,value)
				{
					
					countItem = countItem+1;
					$("#SellerNamePerson").append("<option value='"+value.OrganizeId+"' >"+value.RealName+"</option>")
					
				})
				
			},
			error: function (XMLHttpRequest, textStatus, errorThrown) {
				console.log(textStatus);
			} 
		}); 
		
		
	}

   //ok
   function ok() {
	   document.getElementById("Screening").style.display="none";
		 $(".aui-mask").removeClass("aui-mask-in");
		 
		
		 $("#Daui-Scree").text("筛选");
		 $("#Daui-Scree").trigger("click")
		 GetRecordList();
       }

	
	
	function GEtJsonItem(obj,Id)
	{
		 var returnvalue = [];
		 $.each(obj,function(n,value) {
				if (value.ProductId==Id)
				{
					 returnvalue = value
					return false;				 
				 }		
			  });

		return returnvalue;
	}
	

  
  function SetMessageByProduct(obj)
  {
	   var SelectProduct = GEtJsonItem(ProductJson,$(obj).val());
	
	  $(this).parent().parent().find('input[name=Price]').val(SelectProduct.SalePrice)
  }
  

  
 