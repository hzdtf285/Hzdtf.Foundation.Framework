/**
 * 添加图标
 * */
var ADD_ICON = "add";

/**
 * 编辑图标
 * */
var EDIT_ICON = "edit";

/**
 * 移除图标
 * */
var REMOVE_ICON = "remove";

/**
 * 查询图标
 * */
var QUERY_ICON = "query";

/**
 * 重置图标
 * */
var RESET_ICON = "reset";

/**
 * 重置密码图标
 * */
var RESET_PASSWORD_ICON = "reset_password";

/**
 * 明细图标
 * */
var DETAIL_ICON = "detail";

/**
 * 保存图标
 * */
var SAVE_ICON = "save";

/**
 * 关闭图标
 * */
var CLOSE_ICON = "close";

/**
 * 选中图标
 * */
var CHECKED_ICON = "checked";

/**
 * 操作记录图标
 * */
var OPER_RECORD_ICON = "record";

/**
 * 导出Excel图标
 * */
var EXCEL_EXPORT_ICON = "excel_export";

/**
 * 导入Excel图标
 * */
var EXCEL_IMPORT_ICON = "excel_import";

/**
 * 向左图标
 * */
var ARROW_LEFT_ICON = "arrow_left";

/**
 * 向右图标
 * */
var ARROW_RIGHT_ICON = "arrow_right";

/**
 * 未读邮件
 * */
var MAIL_UNREAD_ICON = "mail_unread";

/**
 * 已读邮件
 * */
var MAIL_READED_ICON = "mail_readed";

/**
 * 审核
 * */
var AUDIT_ICON = "audit";

/**
 * 下载
 * */
var DOWNLOAD_ICON = "download";

/**
 * 强制移除
 * */
var FORCE_REMOVE_ICON = "force_remove";

/**
 * 撤消
 * */
var UNDO_ICON = "undo";

/**
 * 重做
 * */
var REDO_ICON = "redo";


/**
 * 根据图标类型获取URL地址
 * 
 * @param {any} iconType 图标类型
 */
function GetIconUrl(iconType, size) {
    var sizehtml = size ? size : "24";
    var iconUrlFormat = "/img/icon/{ICON_TYPE}_" + sizehtml + ".ico";
    return iconUrlFormat.replace("{ICON_TYPE}", iconType);
}

/**
 * 根据图标类型获取图标标题
 * 
 * @param {any} iconType 图标类型
 */
function GetIconTitle(iconType) {
    switch (iconType) {
        case ADD_ICON:
            return "添加";

        case EDIT_ICON:
            return "编辑";

        case REMOVE_ICON:
            return "移除";

        case QUERY_ICON:
            return "查询";

        case RESET_ICON:
            return "重置";

        case RESET_PASSWORD_ICON:
            return "重置密码";

        case OPER_RECORD_ICON:
            return "操作记录";

        case DETAIL_ICON:
            return "明细";

        case SAVE_ICON:
            return "保存";

        case CLOSE_ICON:
            return "关闭";

        case CHECKED_ICON:
            return "选中";

        case EXCEL_EXPORT_ICON:
            return "导出Excel";

        case EXCEL_IMPORT_ICON:
            return "导入Excel";

        case ARROW_LEFT_ICON:
            return "向左";

        case ARROW_LEFT_ICON:
            return "向右";

        case MAIL_UNREAD_ICON:
            return "未读";

        case MAIL_READED_ICON:
            return "已读";

        case AUDIT_ICON:
            return "审核";

        case DOWNLOAD_ICON:
            return "下载";

        case FORCE_REMOVE_ICON:
            return "强制移除";

        case UNDO_ICON:
            return "撤消";

        case REDO_ICON:
            return "重做";

        default:
            return "";
    }
}

/**
 * 根据图标类型获取图标HTML
 * 
 * @param {any} paraObj 参数对象
 */
function GetIconHtml(paraObj) {
    var url = GetIconUrl(paraObj.iconType, paraObj.size);
    var title = paraObj.title ? paraObj.title : GetIconTitle(paraObj.iconType);

    var clickHtml = "";
    var styleAppendHtml = "";
    if (paraObj.click) {
        clickHtml = " onclick='" + paraObj.click + "'";
        styleAppendHtml = "cursor:pointer;";
    }
    else {
        if (paraObj.hand) {
            styleAppendHtml += "cursor:pointer;";
        }
    }
    if (paraObj.hide) {
        styleAppendHtml += "display:none";
    }
    if (paraObj.styleAppendHtml) {
        styleAppendHtml += ";" + paraObj.styleAppendHtml;
    }

    var appendHtml = "";
    if (paraObj.isAddSpace) {
        appendHtml = "&nbsp;";
    }

    var id = paraObj.id ? paraObj.id : "img_" + Math.random();

    return "<img id='" + id + "' src='" + url + "' title='" + title + "'" + clickHtml + " style='" + styleAppendHtml + "' " + paraObj.otherAttrHtml +" />" + appendHtml;
}