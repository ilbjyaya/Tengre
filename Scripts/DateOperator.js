//js获取某个月的天数
function days(year,month){
    var dayCount;
    now = new Date(year,month, 0);
    dayCount = now.getDate();
    return dayCount;
}
//console.log(days(2017,10))
//js计算系统当前日期是星期几的几种方法
//console.log(new Date().getDay())
//console.log(new Date(2017,7,13).getDay())
 
//格式化日期：yyyy-MM-dd
function formatDate(date) {
	console.log(date)
var myyear = date.getFullYear();
var mymonth = date.getMonth()+1;
var myweekday = date.getDate();
 
if(mymonth < 10){
mymonth = "0" + mymonth;
}
if(myweekday < 10){
myweekday = "0" + myweekday;
}
return (myyear+"-"+mymonth + "-" + myweekday);
}
//获取本周第一天的日期
function showWeekFirstDay(selectweek){    
    var now = new Date();    
    var nowTime = now.getTime() ;
    var day = now.getDay();
    var oneDayTime = 24*60*60*1000 ;
    //显示周一
    if(day == 0){
      day = 7;
    }
    var MondayTime = nowTime - (day-1)*oneDayTime + selectweek* 7*oneDayTime;
    return formatDate(new Date(MondayTime));    
}

//获得本周的结束日期
function showWeekEndDay(selectweek) {
    var now = new Date();    
    var nowTime = now.getTime() ;
    var day = now.getDay();
    var oneDayTime = 24*60*60*1000 ;
    //显示周一
    if(day == 0){
      day = 7;
    }
    var SundayTime =  nowTime + (7-day)*oneDayTime + selectweek* 7*oneDayTime ;
    return formatDate(new Date(SundayTime));
}

//获取本月第一天
function showMonthFirstDay(monthselect){    
    var Nowdate=new Date();    
    var MonthFirstDay=new Date(Nowdate.getFullYear(),monthselect,1);    
    M=Number(MonthFirstDay.getMonth())+1  
    if(M < 10){
		M = "0" + M;
	}
    
    return MonthFirstDay.getFullYear()+"-"+M+"-"+'01';    
}

//获取本月的最后一天
function showMonthLastDay(monthselect){    
    var Nowdate=new Date();    
    
    var MonthNextFirstDay=new Date(Nowdate.getFullYear(),monthselect,1);    
     console.log(MonthNextFirstDay)
    var MonthLastDay=new Date(MonthNextFirstDay-86400000);   
    console.log(MonthLastDay)
    M=Number(MonthLastDay.getMonth())+1
    if(M < 10){
		M = "0" + M;
	}
    return MonthLastDay.getFullYear()+"-"+M+"-"+MonthLastDay.getDate();    
}

function SetDataRange(type)
{
	var date = new Date();
	var month = date.getMonth()+1;
	console.log(Number(date.getMonth())+1)
	if(type=='BN')
	{	console.log(date)
		$("#StartDate").val(date.getFullYear()+'-01-01')
		$("#EndDate").val(date.getFullYear()+'-12-31')
	}
	else if(type=='BJ')
	{
		if(month>=1 && month<=3)
		{
			$("#StartDate").val(date.getFullYear()+'-01-01')
			$("#EndDate").val(date.getFullYear()+'-03-31')
		}
		else if(month>=4 && month<=6)
		{
			$("#StartDate").val(date.getFullYear()+'-04-01')
			$("#EndDate").val(date.getFullYear()+'-06-30')
		}
		else if(month>=7 && month<=9)
		{
			$("#StartDate").val(date.getFullYear()+'-07-01')
			$("#EndDate").val(date.getFullYear()+'-09-30')
		}
		else 
		{
			$("#StartDate").val(date.getFullYear()+'-10-01')
			$("#EndDate").val(date.getFullYear()+'-12-31')
		}
	}
	else if(type=='BY')
	{
		$("#StartDate").val(showMonthFirstDay(month-1))
		$("#EndDate").val(showMonthLastDay(month))
	}
	else if(type=='BZ')
	{
		$("#StartDate").val(showWeekFirstDay(0))
		$("#EndDate").val(showWeekEndDay(0))
	}
	else if(type=='SJ')
	{
		if(month>=1 && month<=3)
		{
			$("#StartDate").val((date.getFullYear()-1)+'-10-01')
			$("#EndDate").val((date.getFullYear()-1)+'-12-31')
		}
		else if(month>=4 && month<=6)
		{
			$("#StartDate").val(date.getFullYear()+'-01-01')
			$("#EndDate").val(date.getFullYear()+'-03-31')
		}
		else if(month>=7 && month<=9)
		{
			$("#StartDate").val(date.getFullYear()+'-04-01')
			$("#EndDate").val(date.getFullYear()+'-06-30')
		}
		else 
		{
			$("#StartDate").val(date.getFullYear()+'-07-01')
			$("#EndDate").val(date.getFullYear()+'-09-30')
		}
	}
	else if(type=='SY')
	{
		$("#StartDate").val(showMonthFirstDay(month-2))
		$("#EndDate").val(showMonthLastDay(month-1))
	}
	else if(type=='SZ')
	{
		$("#StartDate").val(showWeekFirstDay(-1))
		$("#EndDate").val(showWeekEndDay(-1))
	}
	else if(type=='BR')
	{
		$("#StartDate").val(getNowFormatDate())
		$("#EndDate").val(getNowFormatDate())
	}
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

//获取当天日期
function getNowFormatDate() {
        var date = new Date();
        var seperator1 = "-";
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var strDate = date.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var currentdate = year + seperator1 + month + seperator1 + strDate;
        return currentdate;
    }