$(
  function()
  {
    console.log($.cookie('Account'))
    $("#Account").val($.cookie('Account'));
  }
);

function LoginApp()
{
  var UserName = $("#Account").val();
  var Password = $("#Password").val();
  $.ajax({
      		type:"get",
      		url:"http://123.206.255.74:8089/api/Login/CheckLoginJson",
      		data: { username: UserName, password: Password},
      		async:true,
      		dataType: "json",
           success: function (data) {
           	console.log(data)
           	$.cookie('ManagerId',data.ManagerId,{expires:1});
           	$.cookie('Account',data.Account,{expires:1});
           	$.cookie('RealName',data.RealName,{expires:1});
            location.href='order.html'

           	//location.href='index.html';
  				}
      	});


}
