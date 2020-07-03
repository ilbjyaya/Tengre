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

if(window.plus){
	//plusReady();
}else{
	//document.addEventListener('plusready',plusReady,false);
}

function plusReady(){
	   
	IsApp == true;
	UserId =   plus.navigator.getCookie('UserId');
	Account=	plus.navigator.getCookie('Account');
	UserName =   plus.navigator.getCookie('RealName');
	OrganizeId = plus.navigator.getCookie('OrganizeId');
	HaveManage = plus.navigator.getCookie('ManagerId')
	HaveManage = (HaveManage==null ||HaveManage=="" )?false:true;
	SellerName =  plus.navigator.getCookie('RealName');

	
}

//http://118.25.197.197:8089
$(document).ready(function(e) {
	
	
	$("#StartDate").val((new Date().Format("yyyy-MM-01")));
	
	
		UserId =   localStorage.getItem("UserId");
		UserName =  localStorage.getItem("RealName");
		Account= localStorage.getItem("Account");
		OrganizeId = localStorage.getItem("OrganizeId");
		HaveManage = (localStorage.getItem("ManagerId")==null || localStorage.getItem("ManagerId")=="")?false:true;
		SellerName = localStorage.getItem("SellerName");
		console.log(HaveManage)
		
		if(OrganizeId=="78530640-cb09-478f-a846-02dd621a3d46"){
			$(".sales").hide();
		}
		//alert($("#slides li:eq(1)").html());
		$("#slides li:eq(1)").html('企业价值观<br><br>实事求是、专注专业、立诚守信、服务至上');
		
	
   });