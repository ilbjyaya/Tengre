var configurl = 'http://123.206.255.74:8089';
var HaveManage = false;
var ProductJson = [];

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
	plusReady();
}else{
	document.addEventListener('plusready',plusReady,false);
}

function plusReady(){
	   
/* 	 IsApp == true;
	 
	  UserId =   plus.navigator.getCookie('UserId');
	  Account=plus.navigator.getCookie('Account');
     UserName =   plus.navigator.getCookie('RealName');
	 OrganizeId = plus.navigator.getCookie('OrganizeId');
	  HaveManage = plus.navigator.getCookie('ManagerId')
	 HaveManage = (HaveManage==null ||HaveManage=="" )?false:true;
	 SellerName =  plus.navigator.getCookie('RealName'); */

	
}


var fistload;
//http://123.206.255.74:8089
$(document).ready(function(e) {
	
	fistload = true;
	var FinishDateHtml = "<li class='aui-list-item' id='ModifyFinishDate'><div class='aui-list-item-inner'><div class='aui-list-item-label'>完工日期修改</div><div class='aui-list-item-input'><input type='date' id='FinishDate' placeholder='完工日期'></div></div></li>"
	  $("#ModifyOrderDate").after(FinishDateHtml)
	  
	  $("#AddTr").html("产品")
	  $("#AddMonyTr").html("回款")
	  var tk = '&nbsp;<div class=" aui-btn btn-state" id="AddMonyTr" onClick="addTK()">退款</div>'
	  $("#AddMonyTr").after(tk);
	  $(".Tabmenu li").eq(1).after("<li >退款列表</li>");
	  $(".ordermenu li").css("width","20%")
	  var tktable = '<div class="Block" > <table cellpadding="0" cellspacing="0" border="0" width="100%" id="AddTKTable" class="AddTable OrderTable" ><tr ><th width="30%" >退款</th> <th width="20%" >退款金额</th><th width="30%" >退款日期</th><th width="20%" >操作</th> </tr> </table></div>';
	  $("#AddMonyTable").parent().after(tktable);
	  $(".tabDiv").css("padding-bottom","0.5rem")
	  
	bodyHeight = $(window).height();
	headerHeight  = $('header').height()
	footerHeight = $('footer').height()
	var addAreaHeight = $("#operateArea").height();

	$(".aui-content").removeClass("aui-margin-b-15")
$("#ContentList").removeClass("aui-margin-b-15")
$(".aui-popup-content").css('padding-bottom','0rem')

	divHeight = bodyHeight-headerHeight-footerHeight-addAreaHeight
	$("#listContainer").css('height',divHeight+'px');
     $(".Tabmenu li").eq(0).addClass("on");
     $(".Tabmenu li").click(function(){
        $(this).addClass("on");
        $(".Tabmenu li").not($(this)).removeClass("on");

        var idx=$(this).index(".Tabmenu li");

        $(".tabDiv .Block").eq(idx).show();
        $(".tabDiv .Block").not($(".tabDiv .Block").eq(idx)).hide();
      });
      
     
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
       // $("#ModifyOrderDate").show();
      }
	  else
	  {
		//  $("#ModifyOrderDate").hide();
	  }
	
	
	
	 $.growl({ title: "", message: "数据加载中",location:"middle" });
	
	
	
    
	GetProduct();

	GetPositionCodeCode();
	$("input[name=radio][data-text='未完工']").prop('checked',true);
	GetOrderList();
	GetOrganizeTreeListJsonByLogin();
		
	
	
      h=1;
 filechooser = document.getElementById("choose");
 //    用于压缩图片的canvas
 canvas = document.createElement("canvas");

 ctx = canvas.getContext('2d');
 //    瓦片canvas
 tCanvas = document.createElement("canvas");
 tctx = tCanvas.getContext("2d");
 maxsize = 100 * 1024;
 
 //var strA = "<a  id='setpicurl' target='_blank' download='22.png' href=''  ></a>";
 //$("#previewimage").wrapAll(strA);

  
 
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
//          获取图片大小
     
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
//      图片加载完毕之后进行压缩，然后上传
       if (img.complete){
		   
         callback();
       } else{
		   
         img.onload = callback;
       }
       function callback(){
		   
		  /*  var imgfiles = []; 
            imgfiles.push('choose'); //File 控件ID
                  
            //上传应用图标
            $.ajaxFileUpload({ 
                url: configurl+"/api/Order/UploadOrderPhoto",
                data: { userid: UserId, username: UserName, orderid: '95bbdd1d-34fb-45b1-acae-c0f72575403d' },
                secureuri: false,
                fileElementId: imgfiles,//['uploadFile', 'uploadFile1', 'uploadFile2'],
                dataType: 'json',
                success: function (data) {
                    console.log(data)
                },
				error:function(XMLHttpRequest, textStatus, errorThrown)
				{
					console.log(textStatus)
					console.log(errorThrown)
				}
			});

				return; */
				
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
		var $a = $("<a></a>").attr("href", src).attr("download","orderpic.jpeg");
		$a[0].click();
	}			
	
  
 

//dtask.addEventListener( "statechanged", onStateChanged, false );

}
var UserId;
var Account;
var UserName;
var OrganizeId;
   function GetOrderList()
   {

   var conditioncommon =  $("#CommonCondition").val();
var CompanyNameFilter = $("#SellerNameDept").find("option:selected").text()=="请选择"?'':$("#SellerNameDept").find("option:selected").text()
var SellerNameFilter = $("#SellerNamePerson").find("option:selected").text()=="请选择"?'':$("#SellerNamePerson").find("option:selected").text()
var statusName = $('input[type=radio][name=radio]:checked').attr('data-text');
if(typeof(statusName)=='undefined' || statusName=='')
{
	statusName = '全部'
}
if(fistload==true)
{
	statusName = ''
	fistload = false;
}
     var json = {loginid: UserId, ParamCommon:conditioncommon
   ,StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val(),SellerName:SellerNameFilter,CompanyName:CompanyNameFilter,OrderStatusName:statusName};
    var stringJson = JSON.stringify(json);
console.log(stringJson)
     $.ajax({
         		type:"get",
         		url:configurl+"/api/Order/GetListJson",
         		data: { queryJson:stringJson},
         		async:true,
         		dataType: "json",
              success: function (data)
              {
				  // console.log(data)
                $(".ListTable tr").remove()
                
                var TotalCount = 0;
                var TotalReceivedAmount = 0;
				var TotalRealSize = 0;
            　var TotalArea=0;
			$(".ListTable").css('font-size','13px')
			$(".ListTable").append("<tr ><td style='width:35%;'>订单编码</td><td>客户</td><td >用户</td><td style='width:20%;display:none;'>订单状态</td><td>订单日期</td></tr>")
            	 $.each(data,function(item,value)
              {
               
                TotalCount = 1+item;
                if(value.ReceivedAmount!=null &&　value.ReceivedAmount!='')
                {
                    TotalReceivedAmount=TotalReceivedAmount+value.ReceivedAmount;
                }

                if(value.RealSize!=null &&　value.RealSize!='')
                {
                    TotalArea=TotalArea+value.RealSize;
                }
				
                  $(".ListTable").append("<tr onClick=Modify('"+value.OrderId+"') ><td >"+value.OrderCode+"</td><td>"+value.CustomerName+"</td><td >"+value.SellerName+"</td><td style='width:20%;display:none;'>"+value.OrderStatusName+"</td><td >"+(value.OrderDate ==null?value.CreateDate:value.OrderDate)+"</td></tr>");

              });
              $(".ListTable").append("<tr><td colspan='5'>合计：&nbsp;&nbsp;&nbsp;&nbsp;回款："+TotalReceivedAmount.toFixed(0)+"&nbsp;&nbsp;&nbsp;&nbsp;总面积："+TotalArea.toFixed(0)+"m<sup>2</sup>&nbsp;&nbsp;&nbsp;&nbsp;订单数量："+TotalCount+"</td></tr>")
				
			  
		
              	//location.href='index.html';
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
     $(".aui-title").text("新增订单");
     OrderId = '';
	 totletr = '';
	 $('#Add :input').removeAttr("readonly");
	 $("#Add select").removeAttr("disabled");
	 $('#Add :input').val("");
	 $("input[name=OrderStatusName][data-text='未完工']").prop('checked',true);
	  $("input[type=radio][name=OrderStatusName][data-text=未完工]").prop('disabled',false);
	 $(".img-list li").not(':eq(0)').remove();
	   $("input[type=radio][name=OrderStatusName][data-text=已完工]").css('background-color','')
				h=1;
	 $("#AddMonyTable tr").not(':eq(0)').remove();
	 $("#AddTKTable tr").not(':eq(0)').remove();
	 $("#DescriptionList").empty();
	 $("#AddTable tr").not(':eq(0)').remove();
	 IsFinish = 0;
   }
   var IsFinish= 0;
var totletr = '';
   function Modify(paraOrderId){
     document.getElementById("Add").style.display="block";
     document.getElementById("Daui-Scree").style.display="none";
     $(".aui-title").text("编辑订单");
      OrderId = paraOrderId;
	  
	  $.ajax(
	  {
			type:"get",
			url:configurl+"/api/Order/GetOrderFormJson",
			data: { orderId:OrderId},
			async:true,
			dataType: "json",
              success: function (data)
              {
               //console.log(data)
				$("#CustomerName").val(data.order.CustomerName);
				$("#ContecterTel").val(data.order.ContecterTel);
				$("#DistrictAddress").val(data.order.DistrictAddress);
				$("#OpAddress").val(data.order.OpAddress);
				$("#Accounts").val(data.order.Accounts);
				//$("#Description").val(data.order.Description);
				$("#Description").val('');
				$("#OrderDate").val('');
				$("#FinishDate").val('');
				if(data.order.OrderDate!=null && data.order.OrderDate.length>=10)
				{
					$("#OrderDate").val(data.order.OrderDate.substring(0,10));
				}
				
				if(data.order.FinishDate!=null && data.order.FinishDate.length>=10)
				{
					$("#FinishDate").val(data.order.FinishDate.substring(0,10));
				}
				
				$("input[type=radio][name=OrderStatusName][data-text="+data.order.OrderStatusName+"]").prop('checked',true);
				
				$(".img-list li").not(':eq(0)').remove();
				h=1;
				if(data.order.PhotoPath!=null && data.order.PhotoPath!='')
				{
					$.each(data.order.PhotoPath.split(','),function(item,value)
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
				
				//绑定回款
				$("#AddMonyTable tr").not(':eq(0)').remove();
				
				
				var index = 1;
				totletr = '';
				$.each(data.receivableList,function(name,item)
				{
					var PaymentTime = '';
					if(item.PaymentTime!=null && item.PaymentTime.length>=10)
					{
						PaymentTime = item.PaymentTime.substring(0,10);
					}
					var rowMony = '<tr class="MonyTr_' + index + '">'
					   + '<td><input type="text" name="Description" value="'+item.Description+'" placeholder="回款描述" /></td>'
					   + '<td><input type="number" name="PaymentPrice" value="'+(item.PaymentPrice==null?"":item.PaymentPrice)+'"  placeholder="回款金额" /></td>'
					   + '<td><input type="date" name="PaymentTime" value="'+PaymentTime+'" placeholder="回款日期" /></td>'
					   + '<td><a href="#" class="delete" onclick=delMony(this) >删除</a><input type="hidden" name="CreateDate" value="'+item.CreateDate+'" /><input type="hidden" name="CreateUserId" value="'+item.CreateUserId+'" /><input type="hidden" name="CreateUserName" value="'+item.CreateUserName+'" /></td>'
					   + '</tr>';
					index = index+1;
			   
					$("#AddMonyTable").append(rowMony);
				})
				
				//绑定退款
				$("#AddTKTable tr").not(':eq(0)').remove();
				index = 1;
				totletr = '';
				$.each(data.drawbackList,function(name,item)
				{
					var PaymentTime = '';
					
					if(item.PaymentTime!=null && item.PaymentTime.length>=10)
					{
						PaymentTime = item.PaymentTime.substring(0,10);
					}
					var rowTK = '<tr class="MonyTr_' + index + '">'
					   + '<td><input type="text" name="Description" value="'+item.Description+'" placeholder="退款描述" /></td>'
					   + '<td><input type="number" name="PaymentPrice" value="'+(item.DrawbackPrice==null?"":item.DrawbackPrice)+'"  placeholder="退款金额" /></td>'
					   + '<td><input type="date" name="PaymentTime" value="'+PaymentTime+'" placeholder="退款日期" /></td>'
					   + '<td><a href="#" class="delete" onclick=delTK(this) >删除</a><input type="hidden" name="CreateDate" value="'+item.CreateDate+'" /><input type="hidden" name="CreateUserId" value="'+item.CreateUserId+'" /><input type="hidden" name="CreateUserName" value="'+item.CreateUserName+'" /></td>'
					   + '</tr>';
					index = index+1;
			   
					$("#AddTKTable").append(rowTK);
				})
				
				//绑定备注
				$("#DescriptionList").empty();
				$.each(
					data.descriptionEntry,function(name,item)
					{
						var ulcomment = '<ul class="commentDiv" ><li >备注:'+item.DescriptionContent+'(备注人:'+item.CreateUserName+')<div class="commentTime" >时间:'+item.CreateDate+'</div></li></ul>'
						$("#DescriptionList").append(ulcomment);
					}
				);
			  	
				var productAmount = 0;
				var productArea = 0;
				
				//绑定产品
				$("#AddTable tr").not(':eq(0)').remove();
				index = 1
				$.each(data.orderEntry,function(name,item)
				{
					
					 var rowTem = '<tr class="tr_' + index + '">'
					   + '<td>'+stringProductJson+'<span ><select name="ColorCodeId"><option>请选择</option></select></span></td>'
					   + '<td ><select name="StyleCodeId"  onchange="SetPrice(this)"><option>请选择</option></select><span >'+stringPositionCodeJson+'</td>'
					   + '<td><input type="number"  name="Price"  placeholder="单价" /><span ><input type="number" name="RealSize"  placeholder="面积" /></span></td>'
					   + '<td><input type="number"  name="Amount" onclick="CalculateAmount(this)" placeholder="金额" /><span ><a href="#"  class="delete" onclick=delRow(this) >删除</a></span></td>'
					   + '</tr>';
					index = index+1;
					
					if(item.Price!=null &&item.Price!='')
					{
						productAmount=productAmount+item.Amount;
					}
					
					if(item.RealSize!=null && item.RealSize!='')
					{
						productArea=productArea+item.RealSize;
					}
					
					$("#AddTable").append(rowTem);
					var rowObj = $("#AddTable tr").last();
					rowObj.find('select[name=ProductId]').val(item.ProductId)
					SetMessageByProduct(rowObj.find('select[name=ProductId]'))
					rowObj.find('select[name=ColorCodeId]').val(item.ColorCodeId)
					rowObj.find('select[name=OpPositionId]').val(item.OpPositionId)
					rowObj.find('select[name=StyleCodeId]').val(item.StyleCodeId)
					rowObj.find('input[name=Price]').val(item.Price)
					rowObj.find('input[name=RealSize]').val(item.RealSize)
					rowObj.find('input[name=Amount]').val(item.Amount)
					
				})
				totletr = "<tr id='ProductTotal'><td>合计:</td><td>金额:"+productAmount+"</td><td colspan='2'>面积(m<sup>2</sup>):"+productArea.toFixed(2)+"</td></tr>"
				$("#AddTable").append(totletr);
				
				if(data.order.OrderStatusName=='已完工')
				{
					//
					 IsFinish = 1;
					 $('#Add :input').attr("readonly","readonly");
					$("#Add select").attr("disabled",true);
					 // $('#Add :input').css('opacity',1)
					 // $('#Add :input').css('color','black')
					 // $('#Add :input').css('-webkit-opacity',1)	
					 // $('#Add :input').css('-webkit-text-fill-color','black')

							
							
					  $('#Description').removeAttr("readonly");
					 $("input[type=radio][name=OrderStatusName][data-text=已完工]").css('background-color','#03a9f4')
					
					 
					$("input[type=radio][name=OrderStatusName][data-text=未完工]").prop('readonly','readonly');
					$("input[type=radio][name=OrderStatusName][data-text=未完工]").prop('disabled',true);
					
				}
				else
				{
					 IsFinish = 0;
					 $("input[type=radio][name=OrderStatusName][data-text=未完工]").prop('disabled',false);
					$('#Add :input').removeAttr("readonly");
					$("#Add select").removeAttr("disabled");
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
	  $(".aui-title").text("订单查询");
   }
   
   function CloseQuery()
   {
	   $("#Daui-Scree").trigger("click")
   }
  
  function GetPaymentPrice()
  {
	  var datas=[];
	 
	  $("#AddMonyTable tr").not(':eq(0)').each(function () {
		
		  var data = {};
		  
		  data["ReceivableId"]=$(this).find('input[name=ReceivableId]').val();
		  data["OrderId"]=OrderId
		data["Description"] = $(this).find('input[name=Description]').val();
		data["PaymentPrice"] =$(this).find('input[name=PaymentPrice]').val();
		data["PaymentTime"]=$(this).find('input[name=PaymentTime]').val();
		data["CreateDate"]=$(this).find('input[name=CreateDate]').val();
		data["CreateUserId"]=$(this).find('input[name=CreateUserId]').val();
		data["CreateUserName"]=$(this).find('input[name=CreateUserName]').val();		
		//console.log("CreateDate:"+$(this).find('input[name=CreateDate]').val())
		datas.push(data);
	  });
	  
	  $("#AddTKTable tr").not(':eq(0)').each(function () {
		
		  var data = {};
		  
		  data["ReceivableId"]=$(this).find('input[name=ReceivableId]').val();
		  data["OrderId"]=OrderId
		data["Description"] = $(this).find('input[name=Description]').val();
		data["DrawbackPrice"] =$(this).find('input[name=PaymentPrice]').val();
		data["PaymentTime"]=$(this).find('input[name=PaymentTime]').val();
		data["CreateDate"]=$(this).find('input[name=CreateDate]').val();
		data["CreateUserId"]=$(this).find('input[name=CreateUserId]').val();
		data["CreateUserName"]=$(this).find('input[name=CreateUserName]').val();		
		//console.log("CreateDate:"+$(this).find('input[name=CreateDate]').val())
		datas.push(data);
	  });

	 return datas;
	  
  }
   //提交
   function msg(){
   if (confirm("您确认要提交吗？"))   
    {
	
	
    var receivableEntryJson = JSON.stringify(GetPaymentPrice());
	
	 
var  picpaths = '';
var count = 0;
$('input[name=picpath]').each(function(item,value)
{
	count = count+1;
	picpaths = picpaths+','+$(value).val();
});
if(count>0)
{
	picpaths = picpaths.substr(1);	
}
		var OrderDate = $("#OrderDate").val()
		var FinishDate = $("#FinishDate").val()
		 if(HaveManage==true)
      {
		 // OrderDate=''
	  }
      var orderJsonO = {CustomerName:$("#CustomerName").val(),ContecterTel:$("#ContecterTel").val(),DistrictAddress:$("#DistrictAddress").val(),OpAddress:$("#OpAddress").val(),Accounts:$("#Accounts").val(),Description:$("#Description").val()
    ,OrderStatusName:$('input[type=radio][name=OrderStatusName]:checked').attr('data-text'),SellerId:UserId,SellerName:SellerName,OrderDate:OrderDate,FinishDate:FinishDate,PhotoPath:picpaths };//object类型

        var orderJson =  JSON.stringify(orderJsonO);//string类型
       // var orderEntryJsonO = [{ "ProductName": 'ProductName1' }, { "ProductName": 'ProductName1'}];//object类型
        var orderEntryJson =  JSON.stringify(GetOrderEntryList());//string类型
var orderClassJson={ userid: UserId, username: UserName, keyValue: OrderId, orderJson:orderJson, orderEntryJson:orderEntryJson,receivableEntryJson:receivableEntryJson}
                $.ajax({
                    url:  configurl+"/api/Order/SaveOrderFormP",
                    data: orderClassJson,
					async:true,
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                      
					  $.growl.notice({ title: "提示", message: "订单提交完成!",location:"middle", duration:1500});
                           console.log("同步成功")
                           Cancel();
                           GetOrderList();

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                      Cancel();
                           console.log(textStatus);
                           console.log(errorThrown);
                    }
                });




        }
   }
       var i = 1;
       //Addtr
       function addRow() {
		    if(IsFinish == 1)
			   return;
		   $("#ProductTotal").remove();
           i++;
           var rowTem = '<tr class="tr_' + i + '">'
               + '<td>'+stringProductJson+'<span><select name="ColorCodeId"><option>请选择</option></select></span></td>'
               + '<td><select name="StyleCodeId" onchange="SetPrice(this)"><option>请选择</option></select><span >'+stringPositionCodeJson+'</td>'
               + '<td><input type="number"  name="Price"  placeholder="单价" /><span ><input type="number" name="RealSize"  placeholder="面积" /></span></td>'
               + '<td><input type="number"  name="Amount" onclick="CalculateAmount(this)" placeholder="金额" /><span ><a href="#" onclick=delRow(this) >删除</a></span></td>'
               + '</tr>';
          //var tableHtml = $("#table tbody").html();
          // tableHtml += rowTem;
             $("#AddTable").append(rowTem);
         //  $("#table tbody").html(tableHtml);
     
       }
       //delete
       function delRow(obj) {
		    if(IsFinish == 1)
			   return;
           $(obj).parent().parent().parent().remove();
           i--;
       }

       var j = 1;
       //AddMonyTr
       function addMony() {
		   if(IsFinish == 1)
			   return;
           j++;
           var rowMony = '<tr class="MonyTr_' + j + '">'
               + '<td><input type="text" name="Description" placeholder="回款描述" /></td>'
               + '<td><input type="number" name="PaymentPrice"  placeholder="回款金额" /></td>'
               + '<td><input type="date" name="PaymentTime" placeholder="回款日期" /></td>'
               + '<td><a href="#" onclick=delMony(this) >删除</a><input type="hidden" name="CreateDate"  /><input type="hidden" name="CreateUserId"  /><input type="hidden" name="CreateUserName"  /></td>'
               + '</tr>';
          //var tableHtml = $("#table tbody").html();
          // tableHtml += rowTem;
             $("#AddMonyTable").append(rowMony);
         //  $("#table tbody").html(tableHtml);
       }
	   var t = 1;
	   function addTK() {
		   if(IsFinish == 1)
			   return;
           t++;
           var rowTK = '<tr class="TKTr_' + t + '">'
               + '<td><input type="text" name="Description" placeholder="退款描述" /></td>'
               + '<td><input type="number" name="PaymentPrice"  placeholder="退款金额" /></td>'
               + '<td><input type="date" name="PaymentTime" placeholder="退款日期" /></td>'
               + '<td><a href="#" onclick=delTK(this) >删除</a><input type="hidden" name="CreateDate"  /><input type="hidden" name="CreateUserId"  /><input type="hidden" name="CreateUserName"  /></td>'
               + '</tr>';
          //var tableHtml = $("#table tbody").html();
          // tableHtml += rowTem;
             $("#AddTKTable").append(rowTK);
         //  $("#table tbody").html(tableHtml);
       }
	   
       //delete
       function delMony(obj) {
		    if(IsFinish == 1)
			   return;
           $(obj).parent().parent().remove();
           j--;
       }
	   
	    function delTK(obj) {
		    if(IsFinish == 1)
			   return;
           $(obj).parent().parent().remove();
           t--;
       }
   //ok
   function ok() {
           document.getElementById("Screening").style.display="none";
     $(".aui-mask").removeClass("aui-mask-in");
	 
	
     $("#Daui-Scree").text("筛选");
	 $("#Daui-Scree").trigger("click")
	 GetOrderList();
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
  //    图片上传，将base64的图片转成二进制对象，塞进formdata上传
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
    //xhr.open('post', 'http://123.206.255.74:8081/api/values/'+filename);
	
	xhr.open('post',configurl+'/api/Order/UploadOrderPhoto?userid='+UserId+'&username='+UserName+'&orderid='+OrderId)
    xhr.onreadystatechange = function(){

      if (xhr.readyState == 4 && xhr.status == 200){
        //var imagedata = JSON.parse(xhr.responseText);
		var picpath = xhr.responseText.replace(',','');

		
var fullname = picpath.substr(picpath.indexOf('Resource'));
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
        $.growl.notice({ title: "提示", message: "图片上传成功!",location:"middle", duration:500});
      }

      if(xhr.status != 200)
      {
		console.log("xhr.status:"+xhr.status)
      	$li.remove();
      	$.growl.notice({ title: "异常提示", message: "图片上传失败，检查网络!",location:"middle", duration:1500});
      }
    };
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
    xhr.send(formdata);
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
  $(document).ready(function(e) {
	  
    $(".img-list .delete").click(function (){
		 if(IsFinish == 1)
			   return;
		$(this).parent("li").remove();
	
		})
});
function DelImg(imgId){
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
	
function GetOrderEntryList()
  {
	  var datas=[];
	 var productIndex = 1;
	  $("#AddTable tr[id!='ProductTotal']").not(':eq(0)').each(function () {
		
		console.log(productIndex)
		  var data = {};
		  var SelectProduct = GEtJsonItem(ProductJson,$(this).find('select[name=ProductId]').val());
		 
		  data["ProductId"]=SelectProduct.ProductId;
		  data["ProductCode"]=SelectProduct.ProductCode;
		  data["ProductName"] = SelectProduct.ProductName;
		  data["BrandId"] =SelectProduct.Brand;
		  
		   data["ColorCodeId"] =$(this).find('select[name=ColorCodeId]').val();
		    data["ColorCode"] =$(this).find('select[name=ColorCodeId]').find("option:selected").text();
			
			data["OpPositionId"] =$(this).find('select[name=OpPositionId]').val();
		    data["OpPosition"] =$(this).find('select[name=OpPositionId]').find("option:selected").text();
			
			
			data["StyleCodeId"] =$(this).find('select[name=StyleCodeId]').val();
		    data["StyleCode"] =$(this).find('select[name=StyleCodeId]').find("option:selected").text();
			
			data["Price"] =$(this).find('input[name=Price]').val();
			data["RealSize"] =$(this).find('input[name=RealSize]').val();
			data["Amount"] =$(this).find('input[name=Amount]').val();
			data["sortcode"] = productIndex;
			productIndex = productIndex+1;
		
		
		datas.push(data);
	  });

	 return datas;
	  
  }
  
  function SetMessageByProduct(obj)
  {
	   var SelectProduct = GEtJsonItem(ProductJson,$(obj).val());
	   
	GetStyleCode(SelectProduct.ProductCode);
	GetColorCode(SelectProduct.ProductCode);

	 $(obj).parent().parent().find('select[name=StyleCodeId]').html(stringStyleCodeJson)
	$(obj).parent().parent().find('select[name=ColorCodeId]').html(stringColorCodeJson)
	 $(obj).parent().parent().find('input[name=Price]').val('')
	  $(obj).parent().parent().find('input[name=Amount]').val('')
	 // $(this).parent().parent().find('input[name=Price]').val(SelectProduct.SalePrice)
  }
  
  function SetPrice(obj)
  {
	  var Price = $(obj).find("option:selected").attr("price")
	 
	  $(obj).parent().parent().find('input[name=Price]').val(Price)
  }
  
  function CalculateAmount(obj)
  {
	  var amount = parseFloat($(obj).parent().parent().find('input[name=Price]').val())*parseFloat($(obj).parent().parent().find('input[name=RealSize]').val());
	   amount = amount.toFixed(0)
	  $(obj).val(amount);
  }
  
  function GetProduct()
	{
		  
			$.ajax({ 
                    url: configurl+"/api/Base/GetProductListJson",                      
                    type: "GET",
					async:false,
                    dataType: "json",
                    success: function (data) {
						//console.log(data)
						ProductJson = data;
						stringProductJson = '<select name="ProductId" onchange="SetMessageByProduct(this)"><option value="">请选择</option>';
						$.each(data,function(name,item)
						{
							stringProductJson =stringProductJson+'<option value="'+item.ProductId+'">'+item.ProductName+'</option>';
						}
						);
						stringProductJson=stringProductJson+'</select>'
                      
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
						stringProductJson='<select name="ProdcutName"><option value="">请选择<option></select>';
						console.log(errorThrown)                        
                    } 
                }); 
	}
	var stringStyleCodeJson = '';
	function GetStyleCode(ProductCode)
	{
		  
			$.ajax({ 
                    url: configurl+"/api/Order/GetStyleJson",                      
                    type: "GET",
					async:false,					
                    data: {ProductCode :ProductCode},  
                    dataType: "json",
                    success: function (data) {
						
						stringStyleCodeJson = '<select name="StyleCodeId" ><option value="">请选择</option>';
						$.each(data,function(name,item)
						{							
							stringStyleCodeJson =stringStyleCodeJson+'<option price="'+item.Price+'" value="'+item.StyleId+'">'+item.ItemName+'</option>';
						}
						);
						stringStyleCodeJson=stringStyleCodeJson+'</select>'
						
						
                       
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
						stringStyleCodeJson='<select name="ProdcutName"><option value="">请选择<option></select>';
						console.log(errorThrown)                        
                    } 
                }); 
	}
	
	var stringColorCodeJson = '';
	function GetColorCode(ProductCode)
	{
		  
			$.ajax({ 
                    url: configurl+"/api/Order/GetColorJson",                      
                    type: "GET",
					async:false,					
                    data:{ProductCode :ProductCode},   
                    dataType: "json",
                    success: function (data) {
						
						stringColorCodeJson = '<select name="ColorCodeId" ><option value="">请选择</option>';
						$.each(data,function(name,item)
						{
							stringColorCodeJson =stringColorCodeJson+'<option value="'+item.ColorId+'">'+item.ItemName+'</option>';
						}
						);
						stringColorCodeJson=stringColorCodeJson+'</select>'
					
					
                       
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
						stringColorCodeJson='<select name="ProdcutName"><option value="">请选择<option></select>';
						console.log(errorThrown)                        
                    } 
                }); 
	}
	
	
	function GetPositionCodeCode()
	{
		  
			$.ajax({ 
                    url: configurl+"/api/Base/GetBaseItemListJson",                      
                    type: "GET",	
					async:false,					
                    data:{queryJson:JSON.stringify({ encode:'Client_OpPositionCodeOption' ,keyword:''})},  
                    dataType: "json",
                    success: function (data) {
						
						stringPositionCodeJson = '<select name="OpPositionId" ><option value="">请选择</option>';
						$.each(data,function(name,item)
						{
							stringPositionCodeJson =stringPositionCodeJson+'<option value="'+item.ItemDetailId+'">'+item.ItemName+'</option>';
						}
						);
						stringPositionCodeJson=stringPositionCodeJson+'</select>'
						
					
                       
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
						stringPositionCodeJson='<select name="ProdcutName"><option value="">请选择<option></select>';
						console.log(errorThrown)                        
                    } 
                }); 
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