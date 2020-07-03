var configurl = 'http://118.25.197.197:8089';

$(document).ready(function(e) {
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
$(".aui-content").removeClass("aui-margin-b-15")
$("#ContentList").removeClass("aui-margin-b-15")
	$("#listContainer").css('height',divHeight+'px');
	$(".aui-popup-content").css('padding-bottom','0rem')
	$("#Add").css('height',(bodyHeight-footerHeight)+'px');
	 $("#StartDate").val((new Date().Format("yyyy-MM-01")));



	GetNoteList();
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


		//清空input
		function Reset(){
			 for(var i=0;i<document.all.length;i++){
				if(document.all[i].type == "text" || document.all[i].type == "date"||document.all[i].type == "radio")
				{
				  document.all[i].value = "";
				}
			  }
		}
		var keyValue = '';
		//Add
		function OpenDetail(NoteId){
			$("#Content1").css('height','3rem')
			$("#Content2").css('height','3rem')
			$("#Content3").css('height','3rem')
			$("#Content4").css('height','3rem')
			$("#Content1").removeAttr('readonly')
			$("#Content2").removeAttr('readonly')
			$("#Content3").removeAttr('readonly')
			$("#Content4").removeAttr('readonly')
			if(NoteId=='')
			{
				$('#Add :input').val("");
				$(".aui-title").text("新增日志");
				$("#ContentList").hide();
			}
			else
			{
				$.ajax({
					type:"get",
					url:configurl+"/api/EmployNote/GetEmployNoteJson",
					data: {keyValue:NoteId},         		
					dataType: "json",
					success: function (data)
					{
						console.log(data)
						$("#Content1").val(data.entity.Content1);
						$("#Content2").val(data.entity.Content2);
						$("#Content3").val(data.entity.Content3);
						$("#Content4").val(data.entity.Content4);
					
						$("#Content1").css('height',document.getElementById('Content1').scrollHeight)
						$("#Content2").css('height',document.getElementById('Content2').scrollHeight)
						$("#Content3").css('height',document.getElementById('Content3').scrollHeight)
						$("#Content4").css('height',document.getElementById('Content4').scrollHeight)
						
						if(data.entity.UserId != UserId)
						{
							$("#Content1").attr("readonly","readonly");
							$("#Content2").attr("readonly","readonly");
							$("#Content3").attr("readonly","readonly");
							$("#Content4").attr("readonly","readonly");
						/* 	$("#Content1").css('opacity',1)
							$("#Content2").css('opacity',1)
							$("#Content3").css('opacity',1)
							$("#Content4").css('opacity',1)
							$("#Content1").css('color','black')
							$("#Content2").css('color','black')
							$("#Content3").css('color','black')
							$("#Content4").css('color','black')
							$("#Content1").css('-webkit-opacity',1)
							$("#Content2").css('-webkit-opacity',1)
							$("#Content3").css('-webkit-opacity',1)
							$("#Content4").css('-webkit-opacity',1)
							$("#Content1").css('-webkit-text-fill-color','black')
							$("#Content2").css('-webkit-text-fill-color','black')
							$("#Content3").css('-webkit-text-fill-color','black')
							$("#Content4").css('-webkit-text-fill-color','black') */
							
							 
						}
						
						
						
						$("#Content").val('');
						$("#ContentList").show();
						$("#ContentList ul li").not(':eq(0)').remove();
						$.each(data.childEntity,function(item,value)
						{
							//console.log(value)
							$("#ContentList ul").append("<li style='font-size:20px; border-bottom-style:dotted;border-width:1px;'><span style='margin-left:10px;'>"+value.OperateTime+":</span><span style='margin-left:20px;'>"+value.Content1+"(点评人:"+value.UserName+")</span></li>");
						})
					},
					error: function (XMLHttpRequest, textStatus, errorThrown) 
					{
							  console.log(textStatus)
							  console.log(errorThrown);
					}
				});
			}
			
			keyValue = NoteId;
			$("#Add").show();
			$("#Daui-Scree").hide();
			document.getElementById("Add").style.display="block";
			document.getElementById("Daui-Scree").style.display="none";
			
		
		}
		//Cancel
		function Cancel(){
			$("#Daui-Scree").show();
			$("#Add").hide();
			document.getElementById("Add").style.display="none";
			document.getElementById("Daui-Scree").style.display="block";
		}
		//ok
		function ok() {
          
			$("#Daui-Scree").text("筛选");
			 var noteEntityJson = {};
		  
		  noteEntityJson["NoteId"]=keyValue;
		  noteEntityJson["Content1"]=$("#Content1").val();
		noteEntityJson["Content2"] = $("#Content2").val();
		noteEntityJson["Content3"] =$("#Content3").val();
		noteEntityJson["Content4"]=$("#Content4").val();
		noteEntityJson["UserId"]=UserId;
		noteEntityJson["UserName"]=UserName;
		noteEntityJson["OperateTime"]=(new Date().Format("yyyy-MM-dd HH:mm:ss"));		
		//console.log("CreateDate:"+$(this).find('input[name=CreateDate]').val())
		noteClassJson= {userid: UserId, username: UserName, keyValue: keyValue, noteEntityJson:JSON.stringify(noteEntityJson),content1:$("#Content").val()};
			$.ajax({
                    url:  configurl+"/api/EmployNote/SaveEmployNote",
                    data: noteClassJson,
					async:true,
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                       Cancel();
					  $.growl.notice({ title: "提示", message: "日志保存完成!",location:"middle", duration:1500});
                           console.log("同步成功")
                          GetNoteList();

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                      Cancel();
                           console.log(textStatus);
                           console.log(errorThrown);
                    }
                });

        }
		
		
		function Query()
		{
			 $("#Daui-Scree").trigger("click")
			GetNoteList();
		}
		
		function GetNoteList()
		{
			 {

   
   
     var json = {loginid:UserId,UserName: '', StartTime:$("#StartDate").val(),EndTime:$("#EndDate").val()};
    var queryJson = JSON.stringify(json);
//console.log(stringJson)
     $.ajax({
         		type:"get",
         		url:configurl+"/api/EmployNote/GetNoteListJson",
         		data: {queryJson:JSON.stringify(json)},         		
         		dataType: "json",
              success: function (data)
              {
				console.log(data)
                $(".AddTable tr").not(':eq(0)').remove()
               
            	 $.each(data,function(item,value)
				  {
					$(".AddTable").append("<tr onClick=OpenDetail('"+value.NoteId+"') ><td >"+value.UserName+"</td><td >"+value.OperateTime+"</td></tr>");
				  });
				},
               error: function (XMLHttpRequest, textStatus, errorThrown) {
                          console.log(textStatus)
                          console.log(errorThrown);
                      }
				});
			}

		}