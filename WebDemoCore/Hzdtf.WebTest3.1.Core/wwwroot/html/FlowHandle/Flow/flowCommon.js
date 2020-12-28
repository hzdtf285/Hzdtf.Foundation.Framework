// 页面加载完
$().ready(function () {
    if (typeof (initControlStyle) == "function") {
        initControlStyle();
    }
});

/**
 * 流程状态（草稿）
 * */
var FLOW_STATUS_DRAFT = 0;

/**
 * 流程状态（审核中）
 * */
var FLOW_STATUS_AUDITING = 1;

/**
 * 流程状态（审核通过）
 * */
var FLOW_STATUS_AUDIT_PASS = 2;

/**
 * 流程状态（审核驳回）
 * */
var FLOW_STATUS_AUDIT_NOPASS = 3;

/**
 * 流程状态（已撤消）
 * */
var FLOW_STATUS_REVERSED = 4;

/**
 * 根据流程状态ID获取流程状态文本
 * 
 * @param {any} flowStatusId 流程状态ID
 */
function getFlowStatusText(flowStatusId) {
    switch (flowStatusId) {
        case FLOW_STATUS_DRAFT:
            return "草稿";

        case FLOW_STATUS_AUDITING:
            return "审核中";

        case FLOW_STATUS_AUDIT_PASS:
            return "审核通过";

        case FLOW_STATUS_AUDIT_NOPASS:
            return "审核驳回";

        case FLOW_STATUS_REVERSED:
            return "已撤消";

        default:
            return "";
    }
}

/**
 * 根据处理状态ID获取处理状态文本
 * 
 * @param {any} handleStatusId 处理状态ID
 * @param {any} handleTypeId 处理类型ID
 */
function getHandleStatus(handleStatusId, handleTypeId) {
    if (handleTypeId == 0) {
        return "通知";
    }
    else if (handleTypeId == 1) {
        switch (handleStatusId) {
            case 0:
                return "未处理";

            case 1:
                return "已送件";

            case 2:
                return "已退件";

            case 3:
                return "已失效";

            default:
                return "";
        }
    }
    else if (handleTypeId == 2) {
        return "申请";
    }
    else {
        return "";
    }
}

/**
 * 流程表单
 * 
 * */
var flowForm;

/**
 * 根据ID获取流程明细
 * 
 * @param handleId 处理ID
 * @param {any} url URL
 * @param funCode 功能编码
 * @param changeReaded 改变为已读
 */
function getFlowDetail(handleId, url, funCode, changeReaded) {
    $("#auditForm")[0].reset();
    $("#handleId").val(handleId);

    if (handleId != 0 && callReaded) {
        callReaded(handleId, "mail_" + handleId, function () {
            getFlowDetailToRemote(url, funCode);
        });
    }
    else {
        getFlowDetailToRemote(url, funCode);
    }
}

/**
 * 根据ID远程获取流程明细
 *
 * @param {any} url URL
 * @param funCode 功能编码
 */
function getFlowDetailToRemote(url, funCode) {
    ajaxJsonAsync(url, QUERY_REQUEST_TYPE, null, function (returnInfo, data) {
        if (funCode == AUDIT_CODE) {
            $("#fieldAudit").show();
        }
        else {
            $("#fieldAudit").hide();
        }

        var form = new Form("flowInfoForm");
        form.fill(data, function (con, name, v) {
            if (name != "flowStatus") {
                return false;
            }

            con.val(getFlowStatusText(v));

            return true;
        });

        if (data.handles && data.handles.length > 0) {
            var rowHtml = "";
            for (var i = 0; i < data.handles.length; i++) {
                var rowData = data.handles[i];
                var handleResultText = getHandleStatus(rowData.handleStatus, rowData.handleType);

                rowHtml += "<tr>";

                rowHtml += "<td>" + rowData.concreteConcrete + "</td>";
                rowHtml += "<td>" + rowData.handler + "</td>";
                rowHtml += "<td>" + handleResultText + "</td>";
                rowHtml += "<td>" + dateToDateTimeString(rowData.handleTime) + "</td>";
                rowHtml += "<td>" + filterText(rowData.idea) + "</td>";

                rowHtml += "</tr>";
            }

            $("#tableRowFlowInfo").html(rowHtml);
        }

        // 加载表单数据
        if (data.workflowDefine.form.formUrl && data.workflowDefine.form.formUrl != "") {

            $("#btnFlowDetailDialog").trigger("click");

            $("#spanFormInfo").load(data.workflowDefine.form.formUrl, function () {
                if (typeof (initControlStyle) == "function") {
                    initControlStyle();
                }

                var detailDataUrl = data.workflowDefine.form.formGetDetailUrl;
                if (detailDataUrl != "") {
                    detailDataUrl += "?workflowId=" + data.id;
                    ajaxJsonAsync(detailDataUrl, QUERY_REQUEST_TYPE, null, function (returnInfo, data) {
                        if (data) {
                            var form = new Form("formData");
                            form.fill(data, null, true);

                            if (typeof (fillFormData) == "function") {
                                fillFormData(form, data);
                            }
							
                            initControlStyle();
                        }
                        else {
                            showToastr({
                                text: "找不到表单数据，可能已删除！",
                                type: "warning"
                            });
                        }    
                    });
                }
            });
        }
    });
}