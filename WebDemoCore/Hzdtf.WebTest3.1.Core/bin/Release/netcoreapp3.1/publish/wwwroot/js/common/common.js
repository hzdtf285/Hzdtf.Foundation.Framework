/**
 * 页面加载完后
 */
$(function () {
    var loadHtml = "<div class='sk-spinner sk-spinner-chasing-dots' id='divLoading' style='display:none;'>"
        + "<div class='sk-dot1'></div>"
        + "<div class='sk-dot2'></div>"
        + "</div>";
    $(document.body).append(loadHtml);
});

/**
 * 分页大小
 */
var PAGE_SIZE = 30;

/**
 * 查询请求类型
 * */
var QUERY_REQUEST_TYPE = "get";

/**
 * 添加请求类型
 * */
var ADD_REQUEST_TYPE = "post";

/**
 * 修改请求类型
 * */
var MODIFY_REQUEST_TYPE = "put";

/**
 * 移除请求类型
 * */
var REMOVE_REQUEST_TYPE = "delete";

/**
 * 查询编码
 * */
var QUERY_CODE = "Query";

/**
 * 添加编码
 * */
var ADD_CODE = "Add";

/**
 * 编辑编码
 * */
var EDIT_CODE = "Edit";

/**
 * 移除编码
 * */
var REMOVE_CODE = "Remove";

/**
 * 保存编码
 * */
var SAVE_CODE = "Save";

/**
 * 申请编码
 * */
var APPLY_CODE = "Apply";

/**
 * 导入Excel编码
 * */
var IMPORT_EXCEL_CODE = "ImportExcel";

/**
 * 导出Excel编码
 * */
var EXPORT_EXCEL_CODE = "ExportExcel";

/**
 * 重置密码编码
 * */
var RESET_PASSWORD_CODE = "ResetPassword";

/**
 * 上传编码
 * */
var UPLOAD_CODE = "Upload";

/**
 * 下载编码
 * */
var DOWNLOAD_CODE = "DownLoad";

/**
 * 审核编码
 * */
var AUDIT_CODE = "Audit";

/**
 * 撤消编码
 * */
var UNDO_CODE = "Undo";

/**
 * 重做编码
 * */
var REDO_CODE = "Redo";

/**
 * 强制删除编码
 * */
var FORCE_REMOVE_CODE = "ForceRemove";

/**
 * 返回信息
 * @param {*} returnInfo 
 */
function returnInfoHandle(returnInfo, status) {
    if (status && !valiAuthFromStatus(status)) {
        return;
    }

    // 0代表成功
    if (returnInfo.code == 0) {
        return;
    }
    else {
        showToastr({
            text: returnInfo.msg,
            type: "error"
        });
    }
}

/** 
 * 根据状态值验证授权
 * @param {any} status 状态
 */
function valiAuthFromStatus(status) {
    if (status == 401 || status == 403) { //401,403表示没权限，需要跳转到登录页面
        showToastr({
            text: "Sorry,您还未登录或已失效,请您重新登录",
            type: "error"
        }, function () {
            document.location.href = "/login.html";
        });

        return false;
    }

    return true;
}

(function ($) {
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
})(jQuery);

/**
 * 判断一个元素是否在数组里
 * 
 * @param {any} array 数组
 * @param {any} item 元素
 */
function arrayContains(array, item) {
    return array === undefined || array === null || array.indexOf(item) === -1 ? false : true;
}

/**
 * 将布尔值转换为文本
 * 
 * @param {any} boolValue 布尔值
 */
function toBoolText(boolValue) {
    return toBoolStatus(boolValue, function () {
        return "是";
    }, function () {
        return "否";
    });
}

/**
 * 转换布尔状态
 * 
 * @param {any} boolValue 布尔值
 * @param {any} trueCallback true回调
 * @param {any} falseCallback false回调
 * @param {any} otherCallback 其他回调
 */
function toBoolStatus(boolValue, trueCallback, falseCallback, otherCallback) {
    if (typeof (boolValue) == "boolean") {
        return boolValue ? trueCallback() : falseCallback();
    } if (typeof (boolValue) == "string") {
        if (boolValue) {
            var str = boolValue.toLowerCase();
            if (str == "true") {
                return trueCallback();
            }
            else if (str == "false") {
                return falseCallback();
            }
        }
    }

    if (typeof (otherCallback) == "function") {
        return otherCallback(boolValue);
    }

    return "";
}

/**
 * 将布尔值转换为性别
 * 
 * @param {any} boolValue 布尔值
 */
function toBoolSex(boolValue) {
    return toBoolStatus(boolValue, function () {
        return "男";
    }, function () {
        return "女";
    });
}

/**
 * 将布尔文本转换为布尔值
 * 
 * @param {any} boolText 布尔文本
 */
function toBoolValue(boolText) {
    if (boolText == undefined || boolText == null || boolText == "") {
        return false;
    }

    var v = boolText.toLowerCase();

    return v == "true" || v == "是" || v == "男" || v == "yes";
}

String.prototype.replaceAll = function (f, e) {
    var reg = new RegExp(f, "g"); 
    return this.replace(reg, e);
}

/**
 * 判断是否有小数位
 * 
 * @param {any} num
 */
function isNumPoint(num) {
    return (num + "").indexOf(".") != -1;
}

/**
 * 将金额由元转换为分
 * 
 * @param {any} amount 金额
 */
function yuanToFen(amount) {
    if (amount && amount != "" && amount != 0) {
        return parseInt(amount * 100);
    }

    return amount;
}

/**
 * 将金额由分转换为元
 * 
 * @param {any} amount 金额
 */
function fenToYuan(amount) {
    if (amount && amount != "" && amount != 0) {
        var str = amount + "";
        if (str.length == 1) {
            return parseFloat("0.0" + str);
        }
        else if (str.length == 2) {
            return parseFloat("0." + str);
        }
        else {
            var pre, fix;

            var pointIndex = str.indexOf(".");
            // 无小数位，直接向左移2位
            if (pointIndex == -1) {
                var splitIndex = str.length - 2;
                pre = str.substring(0, splitIndex);
                fix = str.substring(splitIndex, str.length);
            }
            else {
                // 新小数位向左移2位
                var newPoint = pointIndex - 2;
                pre = str.substring(0, newPoint);
                str = str.replace(".", "");
                fix = str.substr(pre.length, str.length);
            }

            return parseFloat(parseFloat(pre + "." + fix).toFixed(2));
        }
    }

    return amount;
}

/**
 * 过滤文本
 * 
 * @param {any} text 文本
 */
function filterText(text) {
    return text ? text : "";
}

/**
 * 加载状态
 * @param {*} isDisplay 是否显示
 */
function loadingStatus(isDisplay) {
    $("#divLoading").css("display", isDisplay ? "" : "none");
}

/**
 * AJAX JSON异步请求
 * @param url URL
 * @param method 方法
 * @param data 提交数据
 * @param callbackHandlerSuccess 回调处理成功函数
 * @param callbackHandlerSuccess 回调处理失败函数
 * @returns
 */
function ajaxJsonAsync(url, method, data, callbackHandlerSuccess, callbackHandlerFailure) {
    if (loadingStatus) {
        loadingStatus(true);
    }

    $.ajax({
        cache: false,
        dataType: "json",
        data: JSON.stringify(data),
        error: function (status) {
            if (loadingStatus) {
                loadingStatus(false);
            }

            if (status.responseJSON.errors 
                && status.responseJSON.errors.Text && status.responseJSON.errors.Text.length > 0) {
                var err = status.responseJSON.errors.Text[0];
                showToastr({
                    text: err,
                    type: "error"
                });

                return;
            }


            if (!valiAuthFromStatus(status.status)) {
                return;
            }
            showToastr({
                text: "请求失败" + status.status,
                type: "error"
            });
        },
        type: method,
        url: url,
        scriptCharset: "utf-8",
        contentType: "application/json;charset=utf-8",
        success: function (returnInfo, status, xhr) {
            if (loadingStatus) {
                loadingStatus(false);
            }

            if (returnInfo) {
                if (typeof (returnInfo) == "string") {
                    returnInfo = $.parseJSON(returnInfo);
                }
                if (returnInfo.code == 0) {
                    if (typeof (callbackHandlerSuccess) == "function") {
                        var data;
                        if (returnInfo.data) {
                            data = returnInfo.data;
                        }
                        callbackHandlerSuccess(returnInfo, returnInfo.data);
                    }
                    else {
                        showToastr({
                            text: "请求处理成功",
                            type: "success"
                        });
                    }
                }
                else {
                    if (typeof (callbackHandlerFailure) == "function") {
                        callbackHandlerFailure(returnInfo);
                    }
                    else {
                        showToastr({
                            text: returnInfo.msg,
                            type: "error"
                        });
                    }
                }
            }
            else {
                showToastr({
                    text: "返回数据格式错误" + returnInfo,
                    type: "error"
                });
            }
        }
    });
};

/**
 * Ajax提交
 * 
 * @param {any} formId 表单ID
 * @param url url
 * @param {any} callbackHandlerSuccess 回调处理成功函数
 * @param {any} callbackHandlerSuccess 回调处理失败函数
 */
function ajaxSubmit(formId, url, method, callbackHandlerSuccess, callbackHandlerFailure) {
    if (loadingStatus) {
        loadingStatus(true);
    }

    if (formId == undefined) {
        formId = "editForm";
    }
    var formObj = $("#" + formId); 
    if (!url) {
        url = formObj.attr("action");
    }
    if (!method) {
        method = formObj.attr("method");
    }

    formObj.ajaxSubmit({
        type: method,
        url: url,
        success: function (returnInfo) {
            if (loadingStatus) {
                loadingStatus(false);
            }
            if (returnInfo) {
                if (typeof (returnInfo) == "string") {
                    returnInfo = $.parseJSON(returnInfo);
                }
                if (returnInfo.code == 0) {
                    if (typeof (callbackHandlerSuccess) == "function") {
                        var data;
                        if (returnInfo.data) {
                            data = returnInfo.data;
                        }
                        callbackHandlerSuccess(returnInfo, returnInfo.data);
                    }
                    else {
                        showToastr({
                            text: returnInfo.msg,
                            type: "success"
                        });
                    }
                }
                else {
                    if (typeof (callbackHandlerFailure) == "function") {
                        callbackHandlerFailure(returnInfo);
                    }
                    else {
                        showToastr({
                            text: returnInfo.msg,
                            type: "error"
                        });
                    }
                }
            }
            else {
                showToastr({
                    text: "返回数据格式错误" + returnInfo,
                    type: "error"
                });
            }

            return false;
        },
        error: function (obj, msg) {
            showToastr(obj.status + "," + msg);
        }
    });
}


/**
 * 过滤掉两边多余的双引号字符串
 * @param {*} str 字符串
 */
function filterTrimSurplusDoubleMarks(str) {
    if (str && str.length > 1 && str.charAt(0) === "\"" && str.charAt(str.length - 1) === "\"") {
        return str.substring(1, str.length - 1);
    }

    return str;
};
$(function () {
    $(document.body).append("<div id='divShowDialog' title='温馨提示'></div>");
});

function show(title, text) {
    var dialog = $("#divShowDialog");
    if (title != undefined) {
        dialog.attr("title", title);
    }
    dialog.text(text);
    dialog.dialog();
}

/**
 * 判断是否有权限
 * 
 * @param {any} pageData 页面数据
 * @param {any} functionCode 功能编码
 */
function isPermission(pageData, functionCode) {
    if (pageData && pageData.functions && pageData.functions.length > 0) {
        for (var i = 0; i < pageData.functions.length; i++) {
            if (pageData.functions[i].code == functionCode) {
                return true;
            }
        }
    }

    return false;
}

/**
 * 获取URL参数值
 * 
 * @param {any} paraName 参数名
 */
function getUrlParamValue(paraName) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0].toLowerCase() == paraName.toLowerCase()) { return pair[1]; }
    }
    return "";
}