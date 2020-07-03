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
$(".ClickA3").html($(".ClickA3").html().replace('定金','回款'));
$("#DepartFinish").html($("#DepartFinish").html().replace("BillSort('DepartFinish')'","BillSort('DepartFinish')")); 

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

	
	GetPersonSaleList(1);
	GetPersonFinishList(1);
	GetDepartSaleList(1);
	GetDepartFinishList(1);
	$("#PersonSale th").css('text-align','center')
	$("#PersonFinish th").css('text-align','center')
	$("#DepartSale th").css('text-align','center')
	$("#DepartFinish th").css('text-align','center')
	 //完工两项隐藏
	$("#PersonFinish .ClickA3").hide();	
	$("#PersonFinish tr").find("td:eq(4)").hide();
	$("#DepartFinish .ClickA3").hide();
	$("#DepartFinish tr").find('td:eq(4)').hide();
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


		var PersonSaleJson;
		var PersonFinishJson;
		var DepartSaleJson;
		var DepartFinishJson;
		//Cancel
   function Cancel(){
    $("#Daui-Scree").trigger("click")
   }
   
   
	function Query()
	{
			$("#Daui-Scree").trigger("click")
			GetPersonSaleList(1);
			GetPersonFinishList(1);
			GetDepartSaleList(1);
			GetDepartFinishList(1);
			
			//完工两项隐藏
			$("#PersonFinish .ClickA3").hide();	
			$("#PersonFinish tr").find("td:eq(4)").hide();
			$("#DepartFinish .ClickA3").hide();
			$("#DepartFinish tr").find('td:eq(4)').hide();
	}
		
	function BindList(data,id)
		{
			
			$("#"+id+" tr").not(':eq(0)').remove()
			
			 $.each(data,function(item,value)
			  {
				  if(value.Subscription==null){
					  value.Subscription = 0;
					  }
				$("#"+id).append("<tr ><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td><td >"+value.OrderCount+"</td><td >"+value.Subscription+"</td></tr>");
			  }
		  );
						
		}
		
		
	function GetPersonSaleList(OrderParam)
		{
			//UserName: UserName,
			var json = {UserId:UserId,RptAccountType:'1',RptSellerType: '1',OpFinishFLg:'', StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),OrderParam:OrderParam};
			var queryJson = JSON.stringify(json);

			 $.ajax({
					type:"get",
					url:configurl+"/api/Report/GetSalesJson",
					data: {queryJson:queryJson},         		
					dataType: "json",
					  success: function (data)
					  {
						console.log(data)
						
					   PersonSaleJson = data;
					   //alert(JSON.stringify(data));
					   BindPersonSaleList(data,'PersonSale');
						 //BindList(data,'PersonSale');
						},
					   error: function (XMLHttpRequest, textStatus, errorThrown) 
						{
							  console.log(textStatus)
							  console.log(errorThrown);
					  }
				});
		}
			
	function GetPersonFinishList(OrderParam)
		{
			//UserName: UserName,
			var json = {UserId:UserId,RptAccountType:'1',RptSellerType: '1',OpFinishFLg:'3', StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),OrderParam:OrderParam};
			var queryJson = JSON.stringify(json);

			 $.ajax({
					type:"get",
					url:configurl+"/api/Report/GetSalesJson",
					async:false,
					data: {queryJson:queryJson},         		
					dataType: "json",
					  success: function (data)
					  {
						console.log(data)
						
					   PersonFinishJson = data;
						 BindPersonFinishList(data,'PersonFinish');
						},
					   error: function (XMLHttpRequest, textStatus, errorThrown) 
						{
							  console.log(textStatus)
							  console.log(errorThrown);
					  }
				});
			}
			
			function GetDepartSaleList(OrderParam)
		{
			//UserName: UserName,
			var json = {UserId:UserId,RptAccountType:'1',RptSellerType: '2',OpFinishFLg:'', StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),OrderParam:OrderParam};
			var queryJson = JSON.stringify(json);

			 $.ajax({
					type:"get",
					url:configurl+"/api/Report/GetSalesJson",
					data: {queryJson:queryJson},         		
					dataType: "json",
					  success: function (data)
					  {
						console.log(data)
						
					   DepartSaleJson = data;
						 BindShopSalesList(data,'DepartSale');
						},
					   error: function (XMLHttpRequest, textStatus, errorThrown) 
						{
							  console.log(textStatus)
							  console.log(errorThrown);
					  }
				});
			}
			
				function GetDepartFinishList(OrderParam)
		{
			//UserName: UserName,
			var json = {UserId:UserId,RptAccountType:'1',RptSellerType: '2',OpFinishFLg:'3', StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),OrderParam:OrderParam};
			var queryJson = JSON.stringify(json);

			 $.ajax({
					type:"get",
					url:configurl+"/api/Report/GetSalesJson",
					async:false,
					data: {queryJson:queryJson},         		
					dataType: "json",
					  success: function (data)
					  {
						console.log(data)
						
					   DepartFinishJson = data;
						 //BindList(data,'DepartFinish');
						 BindShopFinishedSalesList(data,'DepartFinish');
						},
					   error: function (XMLHttpRequest, textStatus, errorThrown) 
						{
							  console.log(textStatus)
							  console.log(errorThrown);
					  }
				});
			}
			
			
				
			
			function GetJsonByType(id,OrderParam)
			{
				if(id=='PersonSale')
				{
					GetPersonSaleList(OrderParam)
					return PersonSaleJson;
				}
				else if(id=='PersonFinish')
				{
					GetPersonFinishList(OrderParam)
					return PersonFinishJson;
				}
				else if(id=='DepartSale')
				{
					GetDepartSaleList(OrderParam)
					return DepartSaleJson;
				}
				else if(id=='DepartFinish')
				{
					GetDepartFinishList(OrderParam)
					return DepartFinishJson;
				}			
		
			}

			function MoneySort(id){
				/* var datajson = GetJsonByType(id);
				console.log($("#"+id+" th a.ClickA1 img").attr("src"))
				
				if($("#"+id+" th a.ClickA1 img").attr("src")=="images/down.png"){
					
				 datajson.sort(function(a,b){return b.Accounts-a.Accounts});
				
				 $("#"+id+" th a.ClickA1 img").attr("src","images/up.png");
				}
				else
				{					
					datajson.sort(function(a,b){return a.Accounts-b.Accounts});	
					
					$("#"+id+" th a.ClickA1 img").attr("src","images/down.png");
				}
				
				BindList(datajson,id); */
				var datajson = GetJsonByType(id,1)
				BindList(datajson,id);
				
			}
		//开单
		function BillSort(id){
				/* var datajson = GetJsonByType(id);
			if($("#"+id+" th a.ClickA2 img").attr("src")=="images/down.png"){
					 datajson = datajson.sort(function(a,b){return b.OrderCount-a.OrderCount});
			 $("#"+id+" th a.ClickA2 img").attr("src","images/up.png");
			}
			else
			{
				 datajson = datajson.sort(function(a,b){return a.OrderCount-b.OrderCount});		
				$("#"+id+" th a.ClickA2 img").attr("src","images/down.png");
			}
			BindList(datajson,id); */
			var datajson = GetJsonByType(id,2)
				BindList(datajson,id);
		
		}
		//回款
		function DepositSort(id){
				/* var datajson = GetJsonByType(id);
			if($("#"+id+" th a.ClickA3 img").attr("src")=="images/down.png"){
			 datajson = datajson.sort(function(a,b){return b.Subscription-a.Subscription});
			 $("#"+id+" th a.ClickA3 img").attr("src","images/up.png");
			}
			else
			{
				 datajson = datajson.sort(function(a,b){return a.Subscription-b.Subscription});	
				
				$("#"+id+" th a.ClickA3 img").attr("src","images/down.png");
			}
			BindList(datajson,id); */
			var datajson = GetJsonByType(id,3)
				BindList(datajson,id);
		}
		
		
		
		
		function BindPersonSaleList(data,id)
		{
			
			$("#"+id+" tr").not(':eq(0)').remove()
			
			 $.each(data,function(item,value)
			  {
				   if(value.Subscription==null){
				  		value.Subscription = 0;
				  		}
				$("#"+id).append("<tr onclick=\"ShowSellerDetail(this)\"><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td><td >"+value.OrderCount+"</td><td >"+value.Subscription+"</td></tr>");
				$("#"+id).append("<tr id=\""+value.SellerName+"\" style=\"display: none\"><td colspan=\"5\" width=\"100%\"><div id=\""+value.SellerName+"listContainer\" style=\"width:100%;  overflow:auto;\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" id=\""+value.SellerName+"OrderTable\" ></table></div></td></tr>");
				
			  }
		  );
						
		}
		
		function ShowSellerDetail(o){
			
			var name = o.nextElementSibling.id.toString();
			
			GetSellerOrders(name);
			o.nextElementSibling.style.display="";
			
			
		}
		//从数据库获得某个销售在限定时间内的回款退款列表，写入页面中
		function GetSellerOrders(TheSellerName)
		{
			
			
			var json = {SellerName:TheSellerName,StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),UserId:UserId};
			var stringJson = JSON.stringify(json);
			//alert(stringJson);
			$.ajax({
		 		type:"get",
		 		url:configurl+"/api/Report/GetPaymentRecordByOrderDate",
		 		data: { queryJson:stringJson},
		 		async:false,
		 		dataType: "json",
				success: function (data)
		      	{
					
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
					
				  	$("#"+TheSellerName+"OrderTable").append("<tr><td colspan='4'>合计回款："+TotalReceivedAmount.toFixed(0)+"</td></tr>");
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
		
		function BindShopSalesList(data,id)
		{
			
			$("#"+id+" tr").not(':eq(0)').remove()
			
			 $.each(data,function(item,value)
			  {
				$("#"+id).append("<tr onclick=\"ShowShopSalesDetail(this)\" ><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td><td >"+value.OrderCount+"</td><td >"+value.Subscription+"</td></tr>");
				
				$("#"+id).append("<tr id=\""+value.SellerId+"\" style=\"display: none\"><td colspan=\"5\" width=\"100%\"><div id=\""+value.SellerId+"listContainer\" style=\"width:100%;  overflow:auto;\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" id=\""+value.SellerId+"OrderTable\" ></table></div></td></tr>");
				
				
			  });
						
		}
		
		function ShowShopSalesDetail(o){
			
			var shopid = o.nextElementSibling.id.toString();
			
			GetShopSalesOrders(shopid);
			
			o.nextElementSibling.style.display="";	
			
		}
		
		
		//从数据库获得某个门店在限定时间内的回款退款列表，写入页面中
		function GetShopSalesOrders(shopid)
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
					
					$("#"+shopid+"OrderTable tr").remove();
		
		        	var TotalReceivedAmount = 0;
				  	var TheDate="";
					var StartDate = new Date($("#StartDate").val().substring(0,10));
					var EndDate = new Date($("#EndDate").val().substring(0,10));
					//alert(StartDate.toString()+EndDate.toString());
					
					$("#"+shopid+"OrderTable").css('font-size','13px');
					$("#"+shopid+"OrderTable").append("<tr ><td style='width:35%;'>订单编码</td><td>销售</td><td>客户</td><td >金额</td><td>下单日期</td></tr>");
					
					$.each(data.receive,function(i,value)
					{
						//TheDate = value.PaymentTime.substring(0,10);
						TheDate = data.receivableListOrderList[i].OrderDate.substring(0,10);
						var orderDate = new Date(data.receivableListOrderList[i].OrderDate.substring(0,10));
						if(orderDate>= StartDate && orderDate <= EndDate){
							$("#"+shopid+"OrderTable").append("<tr ><td >"+data.receivableListOrderList[i].OrderCode+"</td><td>"+data.receivableListOrderList[i].SellerName+"</td><td >"+data.receivableListOrderList[i].CustomerName+"</td><td >"+value.PaymentPrice+"</td><td >"+TheDate+"</td></tr>");
							TotalReceivedAmount = TotalReceivedAmount +	value.PaymentPrice;	
						}
						
					});
					
					$.each(data.drawback,function(i,value)
					{
						TheDate = data.receivableListOrderList[i].OrderDate.substring(0,10);
						var orderDate = new Date(data.receivableListOrderList[i].OrderDate.substring(0,10));
						if(orderDate>= StartDate && orderDate<= EndDate){
							$("#"+shopid+"OrderTable").append("<tr ><td >"+data.drawbackListOrderList[i].OrderCode+"</td><td>"+data.drawbackListOrderList[i].SellerName+"</td><td >"+data.drawbackListOrderList[i].CustomerName+"</td><td >-"+value.DrawbackPrice+"</td><td >"+TheDate+"</td></tr>");
							TotalReceivedAmount = TotalReceivedAmount -	value.DrawbackPrice;	
						}
					});
					
				  	$("#"+shopid+"OrderTable").append("<tr><td colspan='5'>合计回款："+TotalReceivedAmount.toFixed(0)+"</td></tr>");
			  },
				error: function (XMLHttpRequest, textStatus, errorThrown) {
						  
		                  console.log(textStatus);
		                  console.log(errorThrown);
		              }
				
				
				});
		}
		
		function BindPersonFinishList(data,id)
		{
			
			$("#"+id+" tr").not(':eq(0)').remove()
			
			 $.each(data,function(item,value)
			  {
				   if(value.Subscription==null){
				  		value.Subscription = 0;
				  		}
				$("#"+id).append("<tr onclick=\"ShowSellerFinishDetail(this)\"><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td><td >"+value.OrderCount+"</td><td >"+value.Subscription+"</td></tr>");
				$("#"+id).append("<tr id=\""+value.SellerName+"FINISH\" style=\"display: none\"><td colspan=\"5\" width=\"100%\"><div id=\""+value.SellerName+"listContainer\" style=\"width:100%;  overflow:auto;\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" id=\""+value.SellerName+"FINISHOrderTable\" ></table></div></td></tr>");
				
			  }
		  );
						
		}
		
		function ShowSellerFinishDetail(o){
			
			var name = o.nextElementSibling.id.toString();
			name = name.substring(0,name.length-6);
			GetSellerFinishOrders(name);
			//$("#"+name+"detail").toggle();
			o.nextElementSibling.style.display="";
			
			
		}
		//从数据库获得某个销售在限定时间内的完工数据列表，写入页面中
		function GetSellerFinishOrders(TheSellerName)
		{
			
			
			var json = {SellerName:TheSellerName,StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),UserId:UserId};
			var stringJson = JSON.stringify(json);
			//alert(stringJson);
			$.ajax({
		 		type:"get",
		 		url:configurl+"/api/Report/GetPaymentRecordByFinishStatus",
		 		data: { queryJson:stringJson},
		 		async:false,
		 		dataType: "json",
				success: function (data)
		      	{
					
					$("#"+TheSellerName+"FINISHOrderTable tr").remove();
		
		        	var TotalReceivedAmount = 0;
				  	var TheDate="";
					var OrderCodeList = new Array();
					
					$("#"+TheSellerName+"FINISHOrderTable").css('font-size','13px');
					$("#"+TheSellerName+"FINISHOrderTable").append("<tr ><td style='width:35%;'>订单编码</td><td>客户</td><td >总金额</td><td>发生日期</td></tr>");
					
					$.each(data.receive,function(i,value)
					{
						try{
							
							if(OrderCodeList.indexOf(data.receivableListOrderList[i].OrderCode.toString()) == -1){
								TheDate = data.receivableListOrderList[i].FinishDate.substring(0,10);
								$("#"+TheSellerName+"FINISHOrderTable").append("<tr ><td >"+data.receivableListOrderList[i].OrderCode+"</td><td>"+data.receivableListOrderList[i].CustomerName+"</td><td >"+data.receivableListOrderList[i].ReceivedAmount+"</td><td >"+TheDate+"</td></tr>");
								TotalReceivedAmount = TotalReceivedAmount +	data.receivableListOrderList[i].ReceivedAmount;	
								OrderCodeList.push(data.receivableListOrderList[i].OrderCode.toString());
							}
						} catch(err) {
								alert(err); 
						}
						
						
					});
					
					
				  	$("#"+TheSellerName+"FINISHOrderTable").append("<tr><td colspan='4'>合计完工回款："+TotalReceivedAmount.toFixed(0)+"</td></tr>");
			  },
				error: function (XMLHttpRequest, textStatus, errorThrown) {
						  
		                  console.log(textStatus);
		                  console.log(errorThrown);
		              }
				
				
				});
		}	
		
	/////////////////////////////////////////////////////////
		
		function BindShopFinishedSalesList(data,id)
		{
			
			$("#"+id+" tr").not(':eq(0)').remove()
			
			 $.each(data,function(item,value)
			  {
				$("#"+id).append("<tr onclick=\"ShowShopFinishedSalesDetail(this)\" ><td>"+value.RankingValue+"</td><td >"+value.SellerName+"</td><td >"+value.Accounts+"</td><td >"+value.OrderCount+"</td><td >"+value.Subscription+"</td></tr>");
				
				$("#"+id).append("<tr id=\""+value.SellerId+"Finish\" style=\"display: none\"><td colspan=\"5\" width=\"100%\"><div id=\""+value.SellerId+"listContainer\" style=\"width:100%;  overflow:auto;\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" id=\""+value.SellerId+"FinishedOrderTable\" ></table></div></td></tr>");
				
				
			  });
						
		}
		
		function ShowShopFinishedSalesDetail(o){
			
			var shopid = o.nextElementSibling.id.toString();
			shopid = shopid.substring(0,shopid.length-6);
			GetShopFinishedSalesOrders(shopid);
			
			o.nextElementSibling.style.display="";	
			
		}
		
		
		//从数据库获得某个门店在限定时间内的回款退款列表，写入页面中
		function GetShopFinishedSalesOrders(shopid)
		{
			
			
			var json = {OrganizationId:shopid,StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),UserId:UserId};
			var stringJson = JSON.stringify(json);
			//alert(stringJson);
			$.ajax({
		 		type:"get",
		 		url:configurl+"/api/Report/GetShopFinishedRecordByFinishStatus",
		 		data: { queryJson:stringJson},
		 		async:false,
		 		dataType: "json",
				success: function (data)
		      	{
					alert(JSON.stringify(data));
					$("#"+shopid+"FinishedOrderTable tr").remove();
		
		        	var TotalReceivedAmount = 0;
				  	var TheDate="";
					var OrderCodeList = new Array();
					
					$("#"+shopid+"FinishedOrderTable").css('font-size','13px');
					$("#"+shopid+"FinishedOrderTable").append("<tr ><td style='width:35%;'>订单编码</td><td>销售</td><td>客户</td><td >总金额</td><td>发生日期</td></tr>");
					
					$.each(data.receive,function(i,value)
					{
						
						
						try{
							
							if(OrderCodeList.indexOf(data.receivableListOrderList[i].OrderCode.toString()) == -1){
								TheDate = data.receivableListOrderList[i].FinishDate.substring(0,10);
								$("#"+shopid+"FinishedOrderTable").append("<tr ><td >"+data.receivableListOrderList[i].OrderCode+"</td><td>"+data.receivableListOrderList[i].SellerName+"</td><td >"+data.receivableListOrderList[i].CustomerName+"</td><td >"+data.receivableListOrderList[i].ReceivedAmount+"</td><td >"+TheDate+"</td></tr>");
								TotalReceivedAmount = TotalReceivedAmount +	data.receivableListOrderList[i].ReceivedAmount;	
								OrderCodeList.push(data.receivableListOrderList[i].OrderCode.toString());
							}
						} catch(err) {
								alert(err); 
						}
						
					});
					
					
				  	$("#"+shopid+"FinishedOrderTable").append("<tr><td colspan='5'>合计完工回款："+TotalReceivedAmount.toFixed(0)+"</td></tr>");
			  },
				error: function (XMLHttpRequest, textStatus, errorThrown) {
						  
		                  console.log(textStatus);
		                  console.log(errorThrown);
		              }
				
				
				});
		}	