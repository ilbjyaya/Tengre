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
	//console.log($("#AddTable").html())
	
	$(".ExpenseMenu li").css("width","20%")
	
	
	bodyHeight 		= $(window).height();
	headerHeight  = $('header').height();
	footerHeight 	= $('footer').height();
	var addAreaHeight = $("#operateArea").height();
	
	listheight =  $("#listContainer").height();
	divHeight = bodyHeight-headerHeight-footerHeight-addAreaHeight+listheight;
	$("#listContainer").css('height',divHeight+'px');

	divHeight = bodyHeight-headerHeight-footerHeight-addAreaHeight;
	$("#listContainer").css('height',divHeight+'px');
  $(".Tabmenu li").eq(0).addClass("on");
	
  $(".Tabmenu li").click(function(){
			$(this).addClass("on");
			$(".Tabmenu li").not($(this)).removeClass("on");
			var idx=$(this).index(".Tabmenu li");
			$(".tabDiv .Block").eq(idx).show();
			$(".tabDiv .Block").not($(".tabDiv .Block").eq(idx)).hide();
  });
			
	//$(".aui-content").removeClass("aui-margin-b-15")
	//$("#ContentList").removeClass("aui-margin-b-15")
	$(".aui-popup-content").css('padding-bottom','1rem')
	
	$("#StartDate").val((new Date().Format("yyyy-MM-01")));
	
	
	UserId =   localStorage.getItem("UserId");
	UserName =  localStorage.getItem("RealName");
	Account= localStorage.getItem("Account");
	OrganizeId = localStorage.getItem("OrganizeId");
	HaveManage = (localStorage.getItem("ManagerId")==null || localStorage.getItem("ManagerId")=="")?false:true;
	SellerName = localStorage.getItem("SellerName");
	console.log(HaveManage)
	
	if(HaveManage==false)
	 {
		
		$("#ExpenseType").append("<option value='ALITA'>ALITA</option>");
		//alert("A");
		$("#FilterExpenseType").append("<option value='ALITA'>ALITA</option>");
		
	 }
	
	$.growl({ title: "", message: "数据加载中",location:"middle" });
    
	GetExpenseList();
	GetOrganizeTreeListJsonByLogin();
	
					/* $('#Add :input').css('opacity',1)
					 $('#Add :input').css('color','black')
					 $('#Add :input').css('-webkit-opacity',1)	
					 $('#Add :input').css('-webkit-text-fill-color','black') */
					
					
					 var h=1;
					 filechooser = document.getElementById("choose");
					 //    用于压缩图片的canvas
					 canvas = document.createElement("canvas");
					
					 ctx = canvas.getContext('2d');
					 //    瓦片canvas
					 tCanvas = document.createElement("canvas");
					 tctx = tCanvas.getContext("2d");
					 maxsize = 100 * 1024;
					 
					 $("#upload").on("click", function(){
					       filechooser.click();
					     })
					     .on("touchstart", function(){
					       $(this).addClass("touch")
					     })
					     .on("touchend", function(){
					       $(this).removeClass("touch")
					     });
						 
					 filechooser.onchange = function(){
					   if (!this.files.length) return;
					   var files = Array.prototype.slice.call(this.files);
							if (files.length > 9){
								alert("最多同时只可上传9张图片");
								return;
							}
					   
					   
					           
						files.forEach(function(file, i){
							if (!/\/(?:jpeg|png|gif)/i.test(file.type)) return;
							var reader = new FileReader();
							var li = document.createElement("li");
							//  获取图片大小
					     
							li.innerHTML = '<div ><input type="hidden" name="picpath" /></div>'+'<img src="images/delete.png" class="delete" id="ImgDel'+h+'" alt="" onClick="DelImg('+h+')" />';
							$(".img-list").append($(li));
							h++;
							reader.onload = function(){
							
								var result = this.result;
								var img = new Image();
								img.src = result;
								$(li).css("background-image", "url(" + result + ")");
								//如果图片大小小于100kb，则直接上传
								if (result.length <= maxsize){
							  
									img = null;
									upload(result, file.type, $(li),file.name);
									return;
								}
									// 图片加载完毕之后进行压缩，然后上传
								if (img.complete){
							   
									callback();
								} else{
							   
									img.onload = callback;
								}
								
								function callback(){
							   		
									var data = compress(img);
									upload(data, file.type, $(li),file.name);
									img = null;
								}
							};
							reader.readAsDataURL(file);
						})
					   
					  		   
					 };
					 
	$("#previewimage").bind("click",function()
		{
		var u = navigator.userAgent;
		var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Adr') > -1; //android终端
		var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
		//alert('是否是Android：'+isAndroid);//true/false
		//alert('是否是iOS：'+isiOS);//true/false
		downloadImage(isiOS);
		})
   });
	 
function cutImageSuffix(imageUrl) {
	
	var index = imageUrl.lastIndexOf('.');
	return imageUrl.substring(index);
	
}

function downloadImage(isiOS) {
	
	if(isiOS)
	{
			var src =$("#previewimage").attr("src")
			//alert(src)
			var Suffix = cutImageSuffix(src)
			var dtask = plus.downloader.createDownload( src, { method: 'GET'},   function ( d, status ) {
			// 下载完成
			if ( status == 200 ) {
			//	alert(d.filename)
				//var sd_path = plus.io.convertLocalFileSystemURL(d.filename);
			plus.runtime.openFile(d.filename, {}, function (e) {
											
										});
			} else {
			 alert( "Download failed: " + status );
			}  
			});
		dtask.start();
	}
	else
	{
		var src =$("#previewimage").attr("src")
		var $a = $("<a></a>").attr("href", src).attr("download","expensepic.jpeg");
		$a[0].click();
	}			
//dtask.addEventListener( "statechanged", onStateChanged, false );
}	 
	 
	 
var UserId;
var Account;
var UserName;
var OrganizeId;

//从服务器获得报销列表 待修改!!!!!!!!!!!!!!
function GetExpenseList(){

		var CompanyNameFilter = $("#SellerNameDept").find("option:selected").text()=="请选择"?'':$("#SellerNameDept").find("option:selected").text()
		var SellerNameFilter = $("#SellerNamePerson").find("option:selected").text()=="请选择"?'':$("#SellerNamePerson").find("option:selected").text()
		var ExpenseAmount = $("#ExpenseAmount").val();
		var stageId = $("#FilterExpenseStatus").val();
		var FilterExpenseType = $("#FilterExpenseType").val();
		var conditioncommon =  $("#CommonCondition").val();
		
    var json = {loginid: UserId, ParamCommon:conditioncommon
		,StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),StageId:stageId,SellerName:SellerNameFilter,CompanyName:CompanyNameFilter,ExpenseType:FilterExpenseType,Amount:ExpenseAmount};
    var stringJson = JSON.stringify(json);
		console.log(stringJson)
		
    $.ajax({
         		type:"get",
         		url:configurl+"/api/Expense/GetExpenseListJson",
         		data: { queryJson:stringJson},
         		async:false,
         		dataType: "json",
				success: function (data){
								console.log(data)
								$(".AddTable tr").not(':eq(0)').remove()
								$(".AddTable").css('font-size','13px')
								
								var TotalAmount = 0;
								
								$.each(data,function(item,value)
									{
										//alert(value.StageId);
										//$(".AddTable").append("<tr onClick=Modify('"+value.ChanceId+"') ><td >"+value.ExpenseDate.substring(0,10)+"</td><td >"+value.CreateUserName+"</td><td>"+value.ExpenseType+"</td><td>"+value.Amount+"</td><td><font color=\"blue\">"+value.StageId+"</font></td></tr>");
										$(".AddTable").append("<tr onClick=Modify('"+value.ChanceId+"') ><td >"+value.ExpenseDate.substring(0,10)+"</td><td >"+value.CreateUserName+"</td><td>"+value.ExpenseType+"</td><td>"+value.Amount+"</td><td>"+value.StageId+"</td></tr>");
										TotalAmount = TotalAmount +	value.Amount;	
									});
								
								$(".AddTable").append("<tr><td colspan='5'>费用合计："+TotalAmount.toFixed(0)+"</td></tr>");	
								
								//alert('blue');
								$(".AddTable tr td:nth-child(5)").each(function(i){
										
									if($(this).html().toString() == "已审核"){
										
										//alert('blue');
										$(this).css("color","green");
									} else if($(this).html().toString() == "未报销"){
										$(this).css("color","red");
									}
									
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

var ExpenseId=''
var totletr = '';

//Add  待修改 

function Add(){
	   
	 
    document.getElementById("Add").style.display="block";
    document.getElementById("Daui-Scree").style.display="none";
		$(".aui-title").text("新增报销");
		ExpenseId = '';
		totletr = '';
		$("#NoDone").prop('checked', true);
		$('#Add :input').removeAttr("readonly");
		$('#Add :input').val("");
		$(".aui-popup-content #listcomment").remove()
		$(".img-list li").not(':eq(0)').remove();
		if(HaveManage==false)
		 {
			$("#DealDateLi").show();
			$("#NoDoneLabel").show();
			$("#DoneLabel").show();
			$("#CashedLabel").show();
		 }
		 else
		 {
			$("#DealDateLi").hide();
			$("#NoDoneLabel").show();
			$("#DoneLabel").hide();
			$("#CashedLabel").hide();
		 }
	$("#ExpenseDate").val(getFormatDate().substring(0,10));	 
}
 
 
var IsFinish= 0;


//待修改 修改报销
function Modify(paraExpenseId){
     document.getElementById("Add").style.display="block";
     document.getElementById("Daui-Scree").style.display="none";
     $(".aui-title").text("编辑报销");
      ExpenseId = paraExpenseId;
	  //alert(expenseId);
	  $('#Add :input').removeAttr("readonly");
	  $.ajax(
	  {
			type:"get",
			url:configurl+"/api/Expense/GetExpenseFormJson",
			data: { keyValue:ExpenseId},
			async:true,
			dataType: "json",
              success: function (data)
              {
               console.log(data)
			  
				//alert(data.expense.ExpenseDate.substring(0,10));
				$("#ExpenseDate").val(data.expense.ExpenseDate.substring(0,10)) ;
				if(data.expense.DealDate!=null && data.expense.DealDate!=''){
					$("#DealDate").val(data.expense.DealDate.substring(0,10)) ;
				}
				$("#ExpenseName").val(data.expense.ExpenseName) ;
				$("#ExpenseType").val(data.expense.ExpenseType) ;
				$("#Amount").val(data.expense.Amount) ;
				//alert(JSON.stringify(data.expense));
				if(HaveManage==false)
				 {
					$("#NoDoneLabel").show();
					$("#DoneLabel").show();
					$("#CashedLabel").show();
				 }
				 else
				 {
					
				 }
				 
				if(data.expense.StageId == "未报销"){
					if(HaveManage==false)
					 {
						$("#NoDoneLabel").show();
						$("#DoneLabel").show();
						$("#CashedLabel").show();
					 }
					 else
					 {
						$("#NoDoneLabel").show();
						$("#DoneLabel").hide();
						$("#CashedLabel").hide();
					 }
					$("#NoDone").prop('checked', true);
					$("#Done").prop('checked', false);
					$("#Cashed").prop('checked', false);
					$("#DealDate").val(getFormatDate().substring(0,10)) ;
					
				} else if(data.expense.StageId == "已审核"){
					if(HaveManage==false)
					 {
						$("#NoDoneLabel").show();
						$("#DoneLabel").show();
						$("#CashedLabel").show();
					 }
					 else
					 {
						$("#NoDoneLabel").hide();
						$("#DoneLabel").show();
						$("#CashedLabel").hide();
					 }
					 
					$("#NoDone").prop('checked', false);
					$("#Done").prop('checked', true);
					$("#Cashed").prop('checked', false);
				} else{
					if(HaveManage==false)
					 {
						$("#NoDoneLabel").show();
						$("#DoneLabel").show();
						$("#CashedLabel").show();
					 }
					 else
					 {
						$("#NoDoneLabel").hide();
						$("#DoneLabel").hide();
						$("#CashedLabel").show();
					 }
					$("#NoDone").prop('checked', false);
					$("#Done").prop('checked', false);
					$("#Cashed").prop('checked', true);
				}
				
				
				$(".img-list li").not(':eq(0)').remove();
				h=1;
				if(data.expense.PhotoPath!=null && data.expense.PhotoPath!=''){
					$.each(data.expense.PhotoPath.split(','),function(item,value)
					{
						 var li = document.createElement("li");
					//          获取图片大小
								     
						li.innerHTML = '<div ><input type="hidden" name="picpath" value='+value+' /></div>'+'<img src="images/delete.png" class="delete" id="ImgDel'+h+'" alt="" onClick="DelImg('+h+')" />';
						$(".img-list").append($(li));
						var url = encodeURI(configurl+'/'+value );
						
						$(li).css("background-image", "url(" +url + ")");
						h++;
						 $(li).click(function(){
						  $(".succ-pop").show();
						  $(".fade").show();
						  $("#previewimage").attr("src",url);
						 // convertImgToDataURLviaCanvas(url,'image/jpeg')
						 
						});
						
					});
				}				
				/**
								$("#CompanyName").val(data.expense.CompanyName);
								if(data.expense.CompanyName!="")
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
								*/				
				/**
				$("#Intention").val(data.chance.Intention);
				if(data.chance.IsToCustom==1)
					$("#IsToCustom").prop("checked",true);
				else
					$("#IsToCustom").prop("checked",false);
				*/
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
				
				if(data.expense.StageId == "已审核"){
					
					if(HaveManage==false)
					 {
						
					 }
					 else
					 {
						$("#NoDoneLabel").hide();
						$("#CashedLabel").hide();
						$('#Add :input').attr("readonly","readonly");
					 }
					 
				} else if(data.expense.StageId == "已出纳"){
					if(HaveManage==false)
					 {
						
					 }
					 else
					 {
						$("#NoDoneLabel").hide();
						$("#DoneLabel").hide();
						$('#Add :input').attr("readonly","readonly");
					 }
				}
				
				
			  },
			error: function (XMLHttpRequest, textStatus, errorThrown) {
				   console.log(textStatus);
				   console.log(errorThrown);
			}
			
			
	  });
			
	  
	  
}


var dataURL;

function convertImgToDataURLviaCanvas(url, outputFormat){
    var img = new Image();
    img.crossOrigin = 'Anonymous';
    img.onload = function(){
        var canvas = document.createElement('CANVAS');
        var ctx = canvas.getContext('2d');
        
        canvas.height = this.height;
        canvas.width = this.width;
        ctx.drawImage(this, 0, 0);
        dataURL = canvas.toDataURL(outputFormat);
		//dataURL = dataURLtoBlob(dataURL);
		//alert(dataURL)
		//dataURL = dataURL.replace(/^data:[^;]*;/, 'data:attachment/file;');
		//alert(dataURL)
        //$("#setpicurl").attr("href",dataURL);
		
        canvas = null; 
    };
    img.src = url;
}

function dataURLtoBlob(dataurl) {
            var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
            bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
            while(n--){
              u8arr[n] = bstr.charCodeAt(n);
            }
          return new Blob([u8arr], {type:mime});
} 


   //Cancel
   function Cancel(){
     document.getElementById("Add").style.display="none";
     document.getElementById("Daui-Scree").style.display="block";
	  $(".aui-title").text("报销管理");
   }
   
   function CloseQuery()
   {
			$("#Daui-Scree").trigger("click")
	    $(".aui-title").text("报销管理");
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
		 GetExpenseList();
}

//    使用canvas对大图片进行压缩
function compress(img){
    var initSize = img.src.length;
    var width = img.width;
    var height = img.height;
    //如果图片大于四百万像素，计算压缩比并将大小压至400万以下
    var ratio;
    if ((ratio = width * height / 4000000) > 1){
      ratio = Math.sqrt(ratio);
      width /= ratio;
      height /= ratio;
    } else{
      ratio = 1;
    }
    canvas.width = width;
    canvas.height = height;
	//        铺底色
    ctx.fillStyle = "#fff";
    ctx.fillRect(0, 0, canvas.width, canvas.height);
    //如果图片像素大于100万则使用瓦片绘制
    var count;
    if ((count = width * height / 1000000) > 1){
      count = ~~(Math.sqrt(count) + 1); //计算要分成多少块瓦片
	//            计算每块瓦片的宽和高
      var nw = ~~(width / count);
      var nh = ~~(height / count);
      tCanvas.width = nw;
      tCanvas.height = nh;
      for (var i = 0; i < count; i++){
        for (var j = 0; j < count; j++){
          tctx.drawImage(img, i * nw * ratio, j * nh * ratio, nw * ratio, nh * ratio, 0, 0, nw, nh);
          ctx.drawImage(tCanvas, i * nw, j * nh, nw, nh);
        }
      }
    } else{
      ctx.drawImage(img, 0, 0, width, height);
    }
    //进行最小压缩
    var ndata = canvas.toDataURL('image/jpeg', 0.8);
    console.log('压缩前：' + initSize);
    console.log('压缩后：' + ndata.length);
    console.log('压缩率：' + ~~(100 * (initSize - ndata.length) / initSize) + "%");
    tCanvas.width = tCanvas.height = canvas.width = canvas.height = 0;
    return ndata;
}


// 待修改   图片上传，将base64的图片转成二进制对象，塞进formdata上传  
function upload(basestr, type, $li,filename){
	
    var text = window.atob(basestr.split(",")[1]);
    var buffer = new Uint8Array(text.length);
    var pecent = 0, loop = null;
    for (var i = 0; i < text.length; i++){
      buffer[i] = text.charCodeAt(i);
    }
    var blob = getBlob([buffer], type);
    var xhr = new XMLHttpRequest();
	var formdata = getFormData();
	
    formdata.append('imagefile', blob,filename);
    //console.log("filename:"+filename);
    //xhr.open('post', 'http://118.25.197.197:8081/api/values/'+filename);
		
		//待修改 后台代码
		xhr.open('post',configurl+'/api/Expense/UploadExpensePhoto?userid='+UserId+'&username='+UserName+'&expenseid='+ExpenseId);
		xhr.onreadystatechange = function(){
		
			//var xhrstatus = xhr.status.toString().charAt(0);	
			
			if (xhr.readyState == 4 && xhr.status == 200){
			//var imagedata = JSON.parse(xhr.responseText);
			var picpath = xhr.responseText.replace(',','');

		
			fullname = picpath.substr(picpath.indexOf('Resource'));
			console.log(fullname)
			$li.find('input[name=picpath]').val(fullname);
		  // var text = imagedata.path ? '上传成功' : '上传失败';
		
			clearInterval(loop);
		   //当收到该消息时上传完毕
		/*    $li.find(".progress span").animate({'width': "100%"}, pecent < 95 ? 200 : 0, function(){
		     $(this).html(text);
		   }); */
		   //console.log($li.parent().html())
			$li.click(function(){
		
				$(".succ-pop").show();
				$(".fade").show();
				$("#previewimage").attr("src",configurl+'/'+fullname);
						   		  
				});
		   $.growl.notice({ title: "提示", message: "图片上传成功! "+xhr.status+" "+xhr.readyState,location:"middle", duration:500});
		 }
		
		if(xhr.status != 200)
		{
			console.log("xhr.status:"+xhr.status)
		 	$li.remove();
			$.growl.notice({ title: "异常提示", message: "图片上传失败，检查网络! "+xhr.status+" "+xhr.readyState ,location:"middle", duration:1500});
		}
		
    };
	xhr.send(formdata);
    //数据发送进度，前50%展示该进度
    xhr.upload.addEventListener('progress', function(e){
      if (loop) return;
      pecent = ~~(100 * e.loaded / e.total) / 2;
      $li.find(".progress span").css('width', pecent + "%");
      if (pecent == 50){
        mockProgress();
      }
    }, false);
    //数据后50%用模拟进度
    function mockProgress(){
      if (loop) return;
      loop = setInterval(function(){
        pecent++;
        $li.find(".progress span").css('width', pecent + "%");
        if (pecent == 99){
          clearInterval(loop);
        }
      }, 100)
    }
    
}
  


  /**
   * 获取blob对象的兼容性写法
   * @param buffer
   * @param format
   * @returns{*}
   */
function getBlob(buffer, format){
    try{
      return new Blob(buffer,{type: format});
    } catch (e){
      var bb = new (window.BlobBuilder || window.WebKitBlobBuilder || window.MSBlobBuilder);
      buffer.forEach(function(buf){
        bb.append(buf);
      });
      return bb.getBlob(format);
    }
}
  /**
   * 获取formdata
   */
function getFormData(){
    var isNeedShim = ~navigator.userAgent.indexOf('Android')
        && ~navigator.vendor.indexOf('Google')
        && !~navigator.userAgent.indexOf('Chrome')
        && navigator.userAgent.match(/AppleWebKit\/(\d+)/).pop() <= 534;
    return isNeedShim ? new FormDataShim() : new FormData()
}
  /**
   * formdata 补丁, 给不支持formdata上传blob的android机打补丁
   * @constructor
   */
function FormDataShim(){
    console.warn('using formdata shim');
    var o = this,
        parts = [],
        boundary = Array(21).join('-') + (+new Date() * (1e16 * Math.random())).toString(36),
        oldSend = XMLHttpRequest.prototype.send;
    this.append = function(name, value, filename){
      parts.push('--' + boundary + '\r\nContent-Disposition: form-data; name="' + name + '"');
      if (value instanceof Blob){
        parts.push('; filename="' + (filename || 'blob') + '"\r\nContent-Type: ' + value.type + '\r\n\r\n');
        parts.push(value);
      }
      else{
        parts.push('\r\n\r\n' + value);
      }
      parts.push('\r\n');
    };
    // Override XHR send()
    XMLHttpRequest.prototype.send = function(val){
      var fr,
          data,
          oXHR = this;
      if (val === o){
        // Append the final boundary string
        parts.push('--' + boundary + '--\r\n');
        // Create the blob
        data = getBlob(parts);
        // Set up and read the blob into an array to be sent
        fr = new FileReader();
        fr.onload = function(){
          oldSend.call(oXHR, fr.result);
        };
        fr.onerror = function(err){
          throw err;
        };
        fr.readAsArrayBuffer(data);
        // Set the multipart content type and boudary
        this.setRequestHeader('Content-Type', 'multipart/form-data; boundary=' + boundary);
        XMLHttpRequest.prototype.send = oldSend;
      }
      else{
        oldSend.call(this, val);
      }
    };
  }
  
  
  
 //提交  
  function msg(){
  	   if (confirm("您确认要提交吗？"))   
  		{
  			
  			var  picpaths = '';
  			var count = 0;
  			$('input[name=picpath]').each(function(item,value){
  			count = count+1;
  			picpaths = picpaths+','+$(value).val();
  			});
  			
  			if(count>0){
  				picpaths = picpaths.substr(1);	
  			}
  			var stageId = $("input[name=StageId]:checked").attr('data-text');
			var dealDate = null;
			if(stageId == "已审核") {
				dealDate = $("#DealDate").val();
			}
			//alert(dealDate);
  			var ExpenseJsonO ={ ExpenseDate: $("#ExpenseDate").val() ,DealDate:dealDate, StageId:stageId, ExpenseName:$("#ExpenseName").val(),ExpenseType:$("#ExpenseType").val(), Amount:$("#Amount").val(), Description: $("#Description").val(),PhotoPath:picpaths}
  			
  			var ExpenseJson = JSON.stringify(ExpenseJsonO);
			//alert(ExpenseJson);
  			$.ajax({
  				//待修改 后台代码
  				url:  configurl+"/api/Expense/SaveExpenseForm",
  				data: { userid:UserId, username:UserName, keyValue:ExpenseId,expenseJson:ExpenseJson},
  				async:true,
  				type: "get",
  				dataType: "json",
  				success: function (data) {
  				  
  							$.growl.notice({ title: "提示", message: "备案提交完成!",location:"middle", duration:1500});
  							console.log("同步成功")
  							Cancel();
  							GetExpenseList();
  
  				},
  				error: function (XMLHttpRequest, textStatus, errorThrown) {
							alert(XMLHttpRequest.status);
							alert(XMLHttpRequest.readyState);
							alert(errorThrown);
  							Cancel();
  							console.log(textStatus);
  							console.log(errorThrown);
  				}
  			});
  			
  	   }
  }
	
$(document).ready(function(e) {
	  
    $(".img-list .delete").click(function (){
		alert("Deleted");
		 if(IsFinish == 1)
			   return;
		$(this).parent("li").remove();
	
		})
});


function DelImg(imgId){
	alert("Deleted");
	 if(IsFinish == 1)
			   return;
			   
	$("img#ImgDel"+imgId).parent("li").remove();
	
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
  
  function getFormatDate(){
      var nowDate = new Date();
      var year = nowDate.getFullYear();
      var month = nowDate.getMonth() + 1 < 10 ? "0" + (nowDate.getMonth() + 1) : nowDate.getMonth() + 1;
      var date = nowDate.getDate() < 10 ? "0" + nowDate.getDate() : nowDate.getDate();
      var hour = nowDate.getHours()< 10 ? "0" + nowDate.getHours() : nowDate.getHours();
      var minute = nowDate.getMinutes()< 10 ? "0" + nowDate.getMinutes() : nowDate.getMinutes();
      var second = nowDate.getSeconds()< 10 ? "0" + nowDate.getSeconds() : nowDate.getSeconds();
      return year + "-" + month + "-" + date+" "+hour+":"+minute+":"+second;
  }

 