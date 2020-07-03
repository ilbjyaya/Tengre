var configurl = 'http://118.25.197.197:8089';
var reporttype=1;
var legend='销售金额';
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
	reporttype = $(".aui-title").html();
	
	
	bodyHeight = $(window).height();
	headerHeight  = $('header').height()
	footerHeight = $('footer').height()
	

	divHeight = bodyHeight-headerHeight-footerHeight
	$("#report").css('height',divHeight+'px');
	
	if(reporttype=='销售排行报表')
	{
		reporttype = 1
		legend = '销售金额'
	}
	else
	{
		reporttype =2
		legend = '回款金额'
	}

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
	
	GetSalesReport();
	
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


		//Cancel
   function Cancel(){
    $("#Daui-Scree").trigger("click")
   }
		function Query()
		{
			 $("#Daui-Scree").trigger("click")
			GetSalesReport();	
		}
		
		
		function GetSalesReport()
		{
console.log(reporttype)
			var json = {UserId:UserId,RptAccountType:reporttype,RptSellerType: '1',OpFinishFLg:'', StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val()};
			var queryJson = JSON.stringify(json);

			 $.ajax({
					type:"get",
					url:configurl+"/api/Report/GetSalesJson",
					data: {queryJson:queryJson},         		
					dataType: "json",
					  success: function (data)
					  {
						var xAxisJson = [];
						var serialData = [];
						console.log(data)
						$.each(data,function(item,value)
						{
							console.log(value.SellerName)
							xAxisJson.push(value.SellerName)
							serialData.push(value.Accounts)
						});
						var datalength = data.length;
						var rate = 8/datalength;
						if(rate>1)
							rate = 100;
						else if(rate<0.01)
							rate = 1;
						else
							rate=rate*100
						// 基于准备好的dom，初始化echarts实例
						var myChart = echarts.init(document.getElementById('report'));
					    myChart.clear();
						// 指定图表的配置项和数据
						var option = {
							title: {
								text: '',
								 x: 'center'
							},									
							grid: {
								left:'18%',
								bottom: 90
							},
							xAxis: {
									type : 'category',
								data: xAxisJson,
								axisLabel:{interval:0,height:200,rotate:45,formatter:function(value,index){
									if(value.length>5)
										return value.substr(0,5)+'\n'+value.substr(5)
									
									else
										return value
								/* if (index % 2 != 0) {
									//return '\n\n' + value;
									return value;
								}
								else {
									return value;
								} */

							}},
											splitArea:{show:true},
								position:'bottom',
								 boundaryGap: false,
								nameLocation:'start'
							},
							 dataZoom: [
							{
								show: true,
								realtime: true,
								start: 0,
								end: rate
							}
						],
							yAxis: {
								
							},
							 legend: {
										data:[legend],
										x:'center',
										top:10
									},
							tooltip : {
								show:true,
											trigger: 'axis'
										},

							series: [{
								name: legend,
								type: 'line',
								data: serialData,
								label:{show:true}
							   
							}]
						};
		
			   
						// 使用刚指定的配置项和数据显示图表。
						myChart.setOption(option);
						},
				error: function (XMLHttpRequest, textStatus, errorThrown) 
				{
					  console.log(textStatus)
					  console.log(errorThrown);
			  }
		});
	}
			
		
		