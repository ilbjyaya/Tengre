var configurl = 'http://118.25.197.197:8089';

function HideFilter()
{
	var IsTopOrg = localStorage.getItem("IsTopOrg")
	console.log(IsTopOrg)
	if(IsTopOrg=='false' || IsTopOrg==false)
	{
		$(".aui-list-item .aui-list-item-input").each(function(){
		if($(this).html().indexOf('本年')>-1)
		{
			$(this).parent().parent().hide()
		}
		
		if($(this).html().indexOf('本季')>-1)
		{
			$(this).parent().parent().hide()
		}
		
		if($(this).html().indexOf('上季')>-1)
		{
			$(this).parent().parent().hide()
		}
		
		if($(this).html().indexOf('开始月份')>-1)
		{
			$(this).parent().parent().hide()
		}
		
	});
	}
	
}

$(document).ready(function(e) {
	HideFilter();
	$(".Tabmenu li").eq(0).addClass("on");
		  $(".Tabmenu li").click(function(){
		   $(this).addClass("on");
		   $(".Tabmenu li").not($(this)).removeClass("on");
		   
		   var idx=$(this).index(".Tabmenu li");
			
		   $(".tabDiv .Block").eq(idx).show();
		   $(".tabDiv .Block").not($(".tabDiv .Block").eq(idx)).hide();
		 });
	SetDataRange('BY')
	UserId =   localStorage.getItem("UserId");
     UserName =  localStorage.getItem("RealName")
	   Account= localStorage.getItem("Account")
	 OrganizeId = localStorage.getItem("OrganizeId")
	 HaveManage = (localStorage.getItem('ManagerId')==null ||localStorage.getItem('ManagerId')=="")?false:true;
	 SellerName = localStorage.getItem("SellerName")
	 
	 	bodyHeight = $(window).height();
	headerHeight  = $('header').height()
	footerHeight = $('footer').height()
	var addAreaHeight = $("#operateArea").height()-$("#listContainer").height();
	divHeight = bodyHeight-headerHeight-footerHeight-addAreaHeight
	console.log(addAreaHeight)
	
	GetPersonDepositList();
	GetDepartDepositList();
	
});

        var popup = new auiPopup();
		
        function showPopup(){
            popup.show(document.getElementById("Screening"));
        }
		
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


		var PersonDepositJson;
		var DepartDepositJson;

		
		
		

		
		function GetPersonDepositList()
		{
			//UserName: UserName,
			var json = {UserId:UserId,RptAccountType:'2',RptSellerType: '1',OpFinishFLg:'', StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),OrderParam:''};
			var queryJson = JSON.stringify(json);

			 $.ajax({
					type:"get",
					url:configurl+"/api/Report/GetSalesJson",
					data: {queryJson:queryJson}, 
					async:false,
					dataType: "json",
					
					  success: function (data)
					  {
						console.log(data)
						
						 PersonDepositJson = data;
						//请求销售回款数据
						
						 BindSellerListV2(data,'PersonDeposit');
						},
					   error: function (XMLHttpRequest, textStatus, errorThrown) 
						{
							alert(XMLHttpRequest.status+" 1");
							  console.log(textStatus)
							  console.log(errorThrown);
					  }
				});
		}
		
		function BindSellerListV2(data,id)
		{
			
			$("#"+id+" tr").not(':eq(0)').remove()
			
			var sellertotal= 0;
			 $.each(data,function(item,value)
			  {
				  
				//$("#"+id).append("<tr onclick=\"ShowDetail("+value.SellerName+"\")><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td></tr>");
				 
				$("#"+id).append("<tr onclick=\"ShowSellerDetail(this)\"><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td></tr>");
				$("#"+id).append("<tr id=\""+value.SellerName+"\" style=\"display: none\"><td colspan=\"3\" width=\"100%\"><div id=\""+value.SellerName+"listContainer\" style=\"width:100%;  overflow:auto;\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" id=\""+value.SellerName+"OrderTable\" ></table></div></td></tr>");
				
				sellertotal+=value.Accounts;
			  });
			$("#"+id).append("<tr><td colspan='3'>个人合计回款："+sellertotal.toFixed(0)+" 元</td></tr>");				
		}	
		function ShowSellerDetail(o){
			
			var name = o.nextElementSibling.id.toString();
			
			GetSellerOrdersV2(name);
			//$("#"+name+"detail").toggle();
			o.nextElementSibling.style.display="";
			
			
		}
		
		//从数据库获得某个销售在限定时间内的回款退款列表，写入页面中
		function GetSellerOrdersV2(TheSellerName)
		{
			
			
			var json = {SellerName:TheSellerName,StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),UserId:UserId};
			var stringJson = JSON.stringify(json);
			//alert(stringJson);
			$.ajax({
		 		type:"get",
		 		url:configurl+"/api/Report/GetPaymentRecordByDateAndName",
		 		data: { queryJson:stringJson},
		 		async:false,
		 		dataType: "json",
				success: function (data)
		      	{
					//alert(JSON.stringify(data));
					//alert('V2')
					$("#"+TheSellerName+"OrderTable tr").remove();
		
		        	var TotalReceivedAmount = 0;
				  	var TheDate="";
					
					$("#"+TheSellerName+"OrderTable").css('font-size','13px');
					$("#"+TheSellerName+"OrderTable").append("<tr ><td style='width:35%;'>订单编码</td><td>客户</td><td >金额</td><td>发生日期</td></tr>");
					
					$.each(data.receive,function(i,value)
					{
						TheDate = value.PaymentTime.substring(0,10);
						$("#"+TheSellerName+"OrderTable").append("<tr ><td >"+data.receivableListOrderList[i].OrderCode+"</td><td>"+data.receivableListOrderList[i].CustomerName+"</td><td >"+value.PaymentPrice+"</td><td >"+TheDate+"</td></tr>");
						TotalReceivedAmount = TotalReceivedAmount +	value.PaymentPrice;	
					});
					
					$.each(data.drawback,function(i,value)
					{
						TheDate = value.PaymentTime.substring(0,10);
						$("#"+TheSellerName+"OrderTable").append("<tr ><td >"+data.drawbackListOrderList[i].OrderCode+"</td><td>"+data.drawbackListOrderList[i].CustomerName+"</td><td >-"+value.DrawbackPrice+"</td><td >"+TheDate+"</td></tr>");
						TotalReceivedAmount = TotalReceivedAmount -	value.DrawbackPrice;	
					});
					
				  	$("#"+TheSellerName+"OrderTable").append("<tr><td colspan='4'>合计回款："+TotalReceivedAmount.toFixed(0)+" 元</td></tr>");
			  },
				error: function (XMLHttpRequest, textStatus, errorThrown) {
						  //alert(XMLHttpRequest.status);
						  //alert(errorThrown);
		                  //alert(XMLHttpRequest.readyState);
		                  console.log(textStatus);
		                  console.log(errorThrown);
		              }
				
				
				});
		}	
		function GetDepartDepositList()
		{
//UserName: UserName,
			var json = {UserId:UserId,RptAccountType:'2',RptSellerType: '2',OpFinishFLg:'', StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),OrderParam:''};
			var queryJson = JSON.stringify(json);

			 $.ajax({
					type:"get",
					url:configurl+"/api/Report/GetSalesJson",
					data: {queryJson:queryJson},
					async:false,
					dataType: "json",
					  success: function (data)
					  {
							console.log(data)
							//alert(JSON.stringify(data));
					   		DepartDepositJson = data;
						 	//BindList(data,'DepartDeposit');
						  	BindShopList(data,'DepartDeposit');
						},
					   error: function (XMLHttpRequest, textStatus, errorThrown) 
						{
							  console.log(textStatus)
							  console.log(errorThrown);
					  }
				});
			}
		function BindShopList(data,id)
		{
			
			$("#"+id+" tr").not(':eq(0)').remove()
			var shoptotal = 0;
			 $.each(data,function(item,value)
			  {
				  
				//$("#"+id).append("<tr onclick=\"ShowDetail("+value.SellerName+"\")><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td></tr>");
				 
				$("#"+id).append("<tr onclick=\"ShowShopDetail(this)\"><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td></tr>");
				$("#"+id).append("<tr id=\""+value.SellerId+"\" style=\"display: none\"><td colspan=\"3\" width=\"100%\"><div id=\""+value.SellerId+"listContainer\" style=\"width:100%;  overflow:auto;\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" id=\""+value.SellerId+"OrderTable\" ></table></div></td></tr>");
				shoptotal=shoptotal+value.Accounts;
				
			  });
			$("#"+id).append("<tr><td colspan='3'>门店合计回款："+shoptotal.toFixed(0)+" 元</td></tr>");		
		}
		function ShowShopDetail(o){
			
			var shopid = o.nextElementSibling.id.toString();
			
			GetShopOrders(shopid);
			
			o.nextElementSibling.style.display="";
			
			
			
		}
		//从数据库获得某个门店在限定时间内的回款退款列表，写入页面中
		function GetShopOrders(shopid)
		{
			
			
			var json = {OrganizationId:shopid,StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),UserId:UserId};
			var stringJson = JSON.stringify(json);
			//alert(stringJson);
			$.ajax({
		 		type:"get",
		 		url:configurl+"/api/Report/GetPaymentRecordByDateAndShop",
		 		data: { queryJson:stringJson},
		 		async:false,
		 		dataType: "json",
				success: function (data)
		      	{
					//alert(JSON.stringify(data));
					//alert('V2')
					$("#"+shopid+"OrderTable tr").remove();
		
		        	var TotalReceivedAmount = 0;
				  	var TheDate="";
					
					$("#"+shopid+"OrderTable").css('font-size','13px');
					$("#"+shopid+"OrderTable").append("<tr ><td style='width:35%;'>订单编码</td><td>销售</td><td>客户</td><td >金额</td><td>发生日期</td></tr>");
					
					$.each(data.receive,function(i,value)
					{
						TheDate = value.PaymentTime.substring(0,10);
						$("#"+shopid+"OrderTable").append("<tr ><td >"+data.receivableListOrderList[i].OrderCode+"</td><td>"+data.receivableListOrderList[i].SellerName+"</td><td >"+data.receivableListOrderList[i].CustomerName+"</td><td >"+value.PaymentPrice+"</td><td >"+TheDate+"</td></tr>");
						TotalReceivedAmount = TotalReceivedAmount +	value.PaymentPrice;	
					});
					
					$.each(data.drawback,function(i,value)
					{
						TheDate = value.PaymentTime.substring(0,10);
						$("#"+shopid+"OrderTable").append("<tr ><td >"+data.drawbackListOrderList[i].OrderCode+"</td><td>"+data.drawbackListOrderList[i].SellerName+"</td><td >"+data.drawbackListOrderList[i].CustomerName+"</td><td >-"+value.DrawbackPrice+"</td><td >"+TheDate+"</td></tr>");
						TotalReceivedAmount = TotalReceivedAmount -	value.DrawbackPrice;	
					});
					
				  	$("#"+shopid+"OrderTable").append("<tr><td colspan='5'>合计回款："+TotalReceivedAmount.toFixed(0)+" 元</td></tr>");
			  },
				error: function (XMLHttpRequest, textStatus, errorThrown) {
						  //alert(XMLHttpRequest.status);
						  //alert(errorThrown);
		                  //alert(XMLHttpRequest.readyState);
		                  console.log(textStatus);
		                  console.log(errorThrown);
		              }
				
				
				});
		}	
			
			function GetJsonByType(id)
			{
				if(id=='PersonDeposit')
				{
					return PersonDepositJson;
				}
				else 
				{
					return DepartDepositJson;
				}
						
			}
			
		function BindSellerList(data,id)
		{
			
			$("#"+id+" tr").not(':eq(0)').remove()
			
			 $.each(data,function(item,value)
			  {
				  
				//$("#"+id).append("<tr onclick=\"ShowDetail("+value.SellerName+"\")><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td></tr>");
				 
				$("#"+id).append("<tr onclick=\"ShowSellerDetail(this)\"><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td></tr>");
				$("#"+id).append("<tr id=\""+value.SellerName+"detail\" style=\"display: none\";><td colspan=\"3\" width=\"100%\"><div id=\""+value.SellerName+"listContainer\" style=\"width:100%;  overflow:auto;\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" id=\""+value.SellerName+"OrderTable\" ></table></div></td></tr>");
				GetSellerOrders(value.SellerName);
			  });
						
		}	
		/*
			function GetSellerOrders(TheSellerName)
			{
				
				var UserId 		= localStorage.getItem("UserId");
				var UserName 	= localStorage.getItem("RealName");
				var Account		= localStorage.getItem("Account");
				var OrganizeId 	= localStorage.getItem("OrganizeId");
				var HaveManage 	= (localStorage.getItem("ManagerId")==null || localStorage.getItem("ManagerId")=="")?false:true;
				
				var json = {loginid: UserId, ParamCommon:'',StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),SellerName:TheSellerName,CompanyName:'',OrderStatusName:''};
				
				var stringJson = JSON.stringify(json);
				//console.log(stringJson);
			
				$.ajax({
			 		type:"get",
			 		url:configurl+"/api/Order/GetListJson",
			 		data: { queryJson:stringJson},
			 		async:true,
			 		dataType: "json",
					success: function (data)
			      	{
					  $("#"+TheSellerName+"OrderTable tr").remove();
			        
			        	var TotalCount = 0;
			        	var TotalReceivedAmount = 0;
						var TotalRealSize = 0;
					  	var TotalArea=0;
					  	var TheDate="";
						$("#"+TheSellerName+"OrderTable").css('font-size','13px');
						$("#"+TheSellerName+"OrderTable").append("<tr ><td style='width:35%;'>订单编码</td><td>客户</td><td >金额</td><td style='width:20%;display:none;'>订单状态</td><td>订单日期</td></tr>");
					
			    	 	$.each(data,function(item,value)
			      		{
			       			if(value.OrderDate != null){
							
								TheDate = value.OrderDate;
							}
							else{
							
								TheDate = value.CreateDate;
							}
						
							TheDate=TheDate.substring(0,TheDate.length-8);
						
			        		TotalCount = 1+item;
							if(value.ReceivedAmount!=null &&　value.ReceivedAmount!='')
			        		{
			            		TotalReceivedAmount=TotalReceivedAmount+value.ReceivedAmount;
							}
			
			        		if(value.RealSize!=null &&　value.RealSize!='')
			        		{
			            		TotalArea=TotalArea+value.RealSize;
			        		}
					
			          		$("#"+TheSellerName+"OrderTable").append("<tr ><td >"+value.OrderCode+"</td><td>"+value.CustomerName+"</td><td >"+value.ReceivedAmount+"</td><td style='width:20%;display:none;'>"+value.OrderStatusName+"</td><td >"+TheDate+"</td></tr>");
			
			     	 	});
					  
					  	$("#"+TheSellerName+"OrderTable").append("<tr><td colspan='5'>合计：    回款："+TotalReceivedAmount.toFixed(0)+"    总面积："+TotalArea.toFixed(0)+"m<sup>2</sup>    订单数量："+TotalCount+"</td></tr>");
				  },
					error: function (XMLHttpRequest, textStatus, errorThrown) {
			                  console.log(textStatus);
			                  console.log(errorThrown);
			              }
					
					
					});
			}
		*/
	   function DepositSort(id){
	   	/* var datajson = GetJsonByType(id);
	   	console.log(id)
	   	console.log(datajson)
	   	if($("#"+id+" th a.ClickA1 img").attr("src")=="images/down.png"){
	   	 datajson = datajson.sort(function(a,b){return b.Accounts-a.Accounts});
	   	 $("#"+id+" th a.ClickA1 img").attr("src","images/up.png");
	   	}
	   	else
	   	{
	   		datajson = datajson.sort(function(a,b){return a.Accounts-b.Accounts});	
	   		
	   		$("#"+id+" th a.ClickA1 img").attr("src","images/down.png");
	   	}
	   	BindList(datajson,id); */
	   }
	   //Cancel
	   function Cancel(){
	   	$("#Daui-Scree").trigger("click")
	   }
	   
	   
	   function Query()
	   {
	   	 $("#Daui-Scree").trigger("click")
	   	GetPersonDepositList();
	   	GetDepartDepositList();
	   	
	   }
	   
	   function BindList(data,id)
	   {
	   	$("#"+id+" tr").not(':eq(0)').remove()
	   	 $.each(data,function(item,value)
	   	  {
	   		$("#"+id).append("<tr ><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td></tr>");
	   	  }
	     );
	   				
	   }