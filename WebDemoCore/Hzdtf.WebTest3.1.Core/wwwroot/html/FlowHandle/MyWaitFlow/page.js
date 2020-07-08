var basicManage;

var controllUrl = "/api/MyWaitFlow";

$(function () {
    laydate(getDefaultDateControllObj("startCreateTime"));
    laydate(getDefaultDateControllObj("endCreateTime"));

    $.validator.setDefaults({
        submitHandler: function (form) {
            var formId = $(form).attr("id");
            if (formId == "editForm") {
                return basicManage.submitForm();
            }

            return false;
        }
    });

    $("#flowDetail").load("/html/FlowHandle/flowDetail.html", null, function () {
        if (typeof (initControlStyle) == "function") {
            initControlStyle();
        }

        $("#btnSendSubmit").click(function () {
            submitAudit(1);
        });
        $("#btnReturnSubmit").click(function () {
            submitAudit(2);
        });

        basicManage = new BasicManage({
            pageDataUrl: controllUrl + "/PageData",
            listUrl: controllUrl,
            caption: "我的待办流程",
            tableId: "table_list",
            pagerId: "pager_list",
            loadPageDataCompleted: function (data) {
                if (data.startDate && data.startDate != "") {
                    $("input[name='startCreateTime']").val(dateToDateString(data.startDate));
                }
                if (data.endDate && data.endDate != "") {
                    $("input[name='endCreateTime']").val(dateToDateString(data.endDate));
                }
            },
            columnModels: [
                { name: "id", sortable: false, key: true, hidden: true },
                {
                    name: "", width: 25, sortable: false, formatter: function (cellvalue, options, rowObject) {
                        var handle = rowObject.handles[0];
                        var id = "mail_" + handle.id;
                        var click;
                        var title;
                        if (!handle.isReaded) {
                            title = "点击改为已读";
                            click = "callReaded(" + handle.id + ", \"" + id + "\")";
                        }

                        return GetIconHtml({
                            iconType: handle.isReaded ? MAIL_READED_ICON : MAIL_UNREAD_ICON,
                            size: 16,
                            id: id,
                            click: click,
                            title: title
                        });
                    }
                },
                { name: "applyNo", sortable: true, label: "申请单号" },
                { name: "title", sortable: true, label: "标题" },
                { name: "creater", sortable: true, label: "申请人" },
                { name: "createTime", sortable: true, label: "申请时间", formatter: dateToDateTimeString },
                {
                    name: "flowStatus", sortable: true, label: "流程状态", formatter: function (cellvalue, options, rowObject) {
                        return getFlowStatusText(cellvalue);
                    }
                },
                { name: "currConcreteCensorships", sortable: false, label: "当前关卡" },
                {
                    label: "操作", sortable: false, width: 100, formatter: function (cellvalue, options, rowObject) {
                        var html = "";
                        var handle = rowObject.handles[0];

                        // 只有处理类型为审核且流程状态是审核中才显示审核图标
                        if (isPermission(basicManage.pageData, AUDIT_CODE) && handle.handleType == 1 && rowObject.flowStatus == 1) {
                            html += GetIconHtml({
                                iconType: AUDIT_ICON,
                                click: "getFlowDetail(" + handle.id + ",\"" + controllUrl + "/GetAuditDetail/" + rowObject.id + "/" + handle.id + "\", \"" + AUDIT_CODE + "\", true)",
                            });
                        }
                        else if (isPermission(basicManage.pageData, QUERY_CODE)) {
                            html += GetIconHtml({
                                iconType: DETAIL_ICON,
                                click: "getFlowDetail(" + handle.id + ",\"" + controllUrl + "/GetFlowDetail/" + rowObject.id + "/" + handle.id + "\", \"" + QUERY_CODE + "\", true)",
                            });
                        }

                        return html;
                    }
                }
            ]
        });

        basicManage.init();

        $("#btnQuery").click(function () {
            basicManage.reLoadList(new Form("searchForm").getString());
        });
    });
});

/**
 * 回调为已读
 * 
 * @param {any} id ID
 * @param {any} imgId 图片ID
 */
function callReaded(id, imgId, callsuccess) {
    var imgObj = $("#" + imgId);
    var readedIconUrl = GetIconUrl(MAIL_READED_ICON, 16);    
    if (imgObj.attr("src") == readedIconUrl) {
        if (typeof (callsuccess) == "function") {
            callsuccess();
        }
        return;
    }

    ajaxJsonAsync(controllUrl + "/ModifyReadedByHandleId/" + id, MODIFY_REQUEST_TYPE, null, function (returnInfo, data) {
        var imgObj = $("#" + imgId);
        imgObj.attr("src", readedIconUrl);
        imgObj.attr("onclick", "").unbind("click");
        imgObj.css("cursor", "");
        imgObj.attr("title", "已读");

        if (typeof (callsuccess) == "function") {
            callsuccess();
        }
    });
}

/**
 * 提交审核
 * 
 * */
function submitAudit(actionType) {
    var obj = new Form("auditForm").getJsonObj();
    obj.actionType = actionType;
    obj.handlerId = $("#handlerId").val();

    ajaxJsonAsync(controllUrl + "/ExecAudit", ADD_REQUEST_TYPE, obj, function (returnInfo, data) {
        showToastr({
            text: returnInfo.msg,
            type: "success"
        });

        $("button[triggerClose]").trigger('click');
        $("#table_list").trigger("reloadGrid");
    });
}