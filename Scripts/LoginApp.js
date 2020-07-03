$(
  function()
  {
      $("#Account").val(localStorage.getItem('Account'));		
  }
);

if(window.plus){
	plusReady();
}else{
	document.addEventListener('plusready',plusReady,false);
}
var IsApp = false;
function plusReady(){
	$("#Account").val(localStorage.getItem("Account"));
	IsApp = true;
}
var configurl = 'http://118.25.197.197:8089';
function LoginApp()
{
  var UserName = $("#Account").val();
  var Password = $("#Password").val();
  if(UserName=='' || Password=='')
  {
	  $.growl.notice({ title: "提示", message: "请输入用户名，密码!",location:"middle", duration:2000});
	  return;
  }
  $.ajax({
      		type:"get",
      		url:configurl+"/api/Login/CheckLoginJson",
      		data: { username: UserName, password: Password},
      		async:true,
      		dataType: "json",
           success: function (data) {
           	console.log(data)
			
			localStorage.setItem("password",data.Password)
			localStorage.setItem("secretkey",data.Secretkey)
				localStorage.setItem("ManagerId",data.ManagerId)
				localStorage.setItem("Account",data.Account)
				localStorage.setItem("RealName",data.RealName)
				localStorage.setItem("UserId",data.UserId)
				localStorage.setItem("OrganizeId",data.OrganizeId)
				localStorage.setItem("IsTopOrg",data.IsTopOrg)
				/* $.cookie('ManagerId',data.ManagerId,{expires:1000});
				$.cookie('Account',data.Account,{expires:1000});
				$.cookie('RealName',data.RealName,{expires:1000});
				$.cookie('UserId',data.UserId,{expires:1000});
				$.cookie('OrganizeId',data.OrganizeId,{expires:1000});
				if(IsApp)
				{
					plus.navigator.setCookie('Account', data.Account);
					plus.navigator.setCookie('RealName', data.RealName);
					plus.navigator.setCookie('ManagerId', data.ManagerId);
					plus.navigator.setCookie('UserId', data.UserId);
					plus.navigator.setCookie('OrganizeId', data.OrganizeId);
				} */
				location.href='index.html'
			
           

           
			},
			error:function(XMLHttpRequest, textStatus, errorThrown)
			{
				$.growl.notice({ title: "异常提示", message: "登录失败，用户名或密码错误!",location:"middle", duration:2000});
				console.log(errorThrown)
				console.log(textStatus)
			}

      	});


}
