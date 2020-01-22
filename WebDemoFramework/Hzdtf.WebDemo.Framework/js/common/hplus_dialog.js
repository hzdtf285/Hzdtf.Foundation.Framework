/**
 * 重写alert
 */

// 延迟执行时间（毫秒）
var delayExecTime = 5000;

/**
 * 弹出确定框
 * 此框是异步弹出
 * 可以设置callback回调函数，点击确定或超过5秒都会执行，只执行一次
 *
 * 类型参数名：type，默认为信息
 * 成功：success
 * 警告：warning
 * 错误：error
 * 信息：info
 * 
 */
window.alert = function (param, callback) {
    // 是否已执行
    var isExeced = false;
    if (typeof(param) === "string") {
        swal({
            title: getTitle(),
            text: param,
            confirmButtonText: "确定"
        });
    }
    else { 
        swal({
            title: getTitle(param.title),
            text: param.text,
            confirmButtonText: "确定",           
            type: param.type     
        }, function () {
            if (typeof(callback) == "function" && !isExeced) {
            	if (isExeced) {
            		return;
            	}
                isExeced = true;
                callback();
            }
        });
    }

    if (typeof(callback) == "function" && !isExeced)  {
        setTimeout(function () {
        	if (isExeced) {
        		return;
        	}
            isExeced = true;
            callback();
        }, delayExecTime);
    }
};

/**
 * 显示提示
 * 
 * @param {any} param 参数
 * @param {any} callback 点击文本或超时回调
 * 类型参数名：type，默认为信息
 * 成功：success
 * 警告：warning
 * 错误：error
 * 信息：info
 * 
 * 位置样式参数名：positionClass，默认为顶部居中
 * 右上：toast-top-right
 * 右下：toast-bottom-right
 * 左下：toast-bottom-left
 * 左上：toast-top-left
 * 顶部全宽：toast-top-full-width
 * 底部全宽：toast-bottom-full-width
 * 顶部居中：toast-top-center
 * 底部居中：toast-bottom-center
 */
function showToastr(param, callback) {
    // 是否已执行
    var isExeced = false;

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "progressBar": false,
        "positionClass": "toast-top-center",
        "showDuration": "400",
        "hideDuration": "1000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    var title, text, type;
    if (typeof (param) === "string") {
        title = getTitle();
        text = param;
        type = "info";
    }
    else {
        title = getTitle(param.title);
        text = param.text;
        type = param.type ? param.type : "info";

        if (param.timeout && !isNaN(param.timeout)) {
            toastr.options.timeOut = param.timeout;
        }
    }

    if (typeof (callback) == "function") {
        toastr.options.onclick = function () {
            if (isExeced) {
                return;
            }

            isExeced = true;
            callback();
        };
    }
    else {
        toastr.options.onclick = undefined;
    }

    if (param.positionClass && param.positionClass != "") {
        toastr.options.positionClass = param.positionClass;
    }

    switch (type) {
        case "success":
            if (toastr.options.timeOut == undefined) {
                toastr.options.timeOut = 3000;
            }
            toastr.success(text, title);

            break;

        case "warning":
            if (toastr.options.timeOut == undefined) {
                toastr.options.timeOut = 4000;
            }
            toastr.warning(text, title);

            break;

        case "error":
            if (toastr.options.timeOut == undefined) {
                toastr.options.timeOut = 5000;
            }
            toastr.error(text, title);

            break;

        default:
            if (toastr.options.timeOut == undefined) {
                toastr.options.timeOut = 3000;
            }
            toastr.info(text, title);

            break;
    }

    if (typeof (callback) == "function") {
        setTimeout(function () {
            if (isExeced) {
                return;
            }

            isExeced = true;
            callback();
        }, toastr.options.timeOut);
    }
}

/**
 * 执行显示提示的回调
 * 
 * @param {any} callback 回调 
 */
function execShowToastrCallback(callback) {
    console.log(arguments);
    return;
    var index = execShowToastrCallbackMap.indexOf(callback);
    if (index != -1) {
        execShowToastrCallbackMap.splice(index, 1);
    }

    callback();
}

/**
 * 重写确认框
 */
window.confirm = function (param) {
    if (!param.confirmButtonText) {
        param.confirmButtonText = "确认";
    } 
    if (!param.cancelButtonText) {
        param.cancelButtonText = "取消";
    }

    var closeOnConfirm = param.closeOnConfirm == undefined ? true : param.closeOnConfirm;
    var closeOnCancel = param.closeOnCancel == undefined ? true : param.closeOnCancel;
    
    swal({
        title: getTitle(param.title),
        text: param.text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: param.confirmButtonText,
        cancelButtonText: param.cancelButtonText,
        closeOnConfirm: closeOnConfirm,
        closeOnCancel: closeOnCancel                
    }, function (isConfirm) {
        swal.close();
        if (isConfirm) {
            if (typeof(param.confirmCallback) == "function") {
                param.confirmCallback();
            }
        }
        else {
            if (typeof(param.cancelCallback) == "function") {
                param.cancelCallback();
            }
        }
    });
};

/**
 * 获取标题
 * @param {*} title 
 */
function getTitle(title) {
    if (title) {
        return title;
    }

    return "温馨提示";
};