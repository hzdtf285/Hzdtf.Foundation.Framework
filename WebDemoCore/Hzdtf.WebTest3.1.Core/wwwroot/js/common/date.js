Date.prototype.format = function(fmt) { 
    var o = { 
       "M+" : this.getMonth()+1,                 //月份 
       "d+" : this.getDate(),                    //日 
       "h+" : this.getHours(),                   //小时 
       "m+" : this.getMinutes(),                 //分 
       "s+" : this.getSeconds(),                 //秒 
       "q+" : Math.floor((this.getMonth()+3)/3), //季度 
       "S"  : this.getMilliseconds()             //毫秒 
   };
    
    if(/(y+)/.test(fmt))   
        fmt=fmt.replace(RegExp.$1, (this.getFullYear()+"").substr(4 - RegExp.$1.length));   
    for(var k in o)   
       if(new RegExp("("+ k +")").test(fmt))   
    	   fmt = fmt.replace(RegExp.$1, (RegExp.$1.length==1) ? (o[k]) : (("00"+ o[k]).substr((""+ o[k]).length)));   
    return fmt;   
};
    
/**
 * 将字符串转换为日期时间字符串
 * @param str
 * @returns
 */
function dateToDateTimeString(str) {  
	if (str && str.length > 0) {
		return new Date(str).format("yyyy-MM-dd hh:mm:ss");
	}
	
	return "";
}

/**
 * 将字符串转换为日期字符串
 * @param str
 * @returns
 */
function dateToDateString(str) {
    if (str && str.length > 0) {
        var dateStr = new Date(str).format("yyyy-MM-dd");
        if (dateStr == "01-01-01") {
            return "";
        }

        return dateStr;
    }

    return "";
}

/**
 * 将字符串转换为年月字符串
 * @param str
 * @returns
 */
function dateToYMString(str) {
    if (str && str.length > 0) {
        var dateStr = new Date(str).format("yyyy-M");
        if (dateStr == "1-1") {
            return "";
        }

        return dateStr;
    }

    return "";
}  

/**
 * 获取默认日期控件对象
 * 
 * @param {any} controllId 控件ID
 */
function getDefaultDateControllObj(controllId) {
    return {
        elem: "#" + controllId,
        format: "YYYY-MM-DD",
        min: "1970-01-01",
        max: "2099-12-31",
        istime: false,
        event: "focus",
        isclear: true, 
        istoday: true
    };
}

/**
 * 根据年月获取最后一天
 * 
 * @param {any} year 年
 * @param {any} month 月
 */
function getLastDay(year, month) {
    var day = 30;
    switch (month) {
        case 1:
        case 3:
        case 5:
        case 7:
        case 8:
        case 10:
        case 12:
            day = 31;

            break;

        case 2:
            day = year % 4 == 0 && year % 100 != 0 || year % 400 == 0 ? 29 : 28;

            break;
    }

    return day;
}

/**
 * 获取日期字符串
 * 
 * @param {any} year 年
 * @param {any} month 月
 * @param {any} day 日
 */
function getDateString(year, month, day) {
    return year + "-" + month + "-" + day;
}