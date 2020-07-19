var basicManage;
var flowForm;

$(function () {
    $.validator.setDefaults({
        submitHandler: function (form) {
            var formId = $(form).attr("id");
            if (formId == "editForm") {
                return basicManage.submitForm();
            }

            return false;
        }
    });

    var controllUrl = "/api/TestForm";

    basicManage = new BasicManage({
        pageDataUrl: controllUrl + "/PageData",
        listUrl: controllUrl,
        batchDeleteUrl: controllUrl + "/BatchRemove",
        formUrl: "form.html",
        addUrl: controllUrl,
        modifyByIdUrl: controllUrl,
        removeByIdUrl: controllUrl,
        caption: "测试表单列表",
        tableId: "table_list",
        pagerId: "pager_list",
        multiselect: true,
        multiselectShowCell: function (cellvalue, options, rowObject) {
            return rowObject.flowStatus == 0;
        },
        formDialogLoadCompleted: function () {
            flowForm = new FlowForm({
                controllerUrl: "/api/TestFormWorkflow",
                callbackSuccess: function (actionType, data) {
                    switch (actionType) {
                        case SAVE_CODE:
                        case APPLY_CODE:
                            $("#editForm")[0].reset();

                            break;
                    }
                   
                    $("button[triggerClose]").trigger('click');
                    basicManage.reLoadList(new Form("searchForm").getString());
                }
            });

            $("#btnApply").click(function () {
                var data = new Form("editForm").getJsonObj();
                flowForm.apply(data);
            });

            $("#btnSave").click(function () {
                var data = new Form("editForm").getJsonObj();
                flowForm.save(data);
            });
        },
        columnModels: [
            { name: "id", sortable: false, key: true, hidden: true },
            { name: "applyNo", sortable: true, label: "申请单号" },
            { name: "code", sortable: true, label: "编码" },
            { name: "name", sortable: true, label: "名称" },
            {
                name: "flowStatus", sortable: true, label: "流程状态", formatter: function (cellvalue, options, rowObject) {
                    return getFlowStatusText(cellvalue);
                }
            },
            {
                label: "操作", sortable: false, width: 100, formatter: function (cellvalue, options, rowObject) {
                    var html = "";

                    var canEditOrDel = rowObject.flowStatus == 0;
                    if (isPermission(basicManage.pageData, EDIT_CODE) && canEditOrDel) {
                        html += GetIconHtml({
                            iconType: EDIT_ICON,
                            click: "basicManage.modifyById(" + rowObject.id + ")"
                        });
                    }
                    if (isPermission(basicManage.pageData, REMOVE_CODE) && canEditOrDel) {
                        html += GetIconHtml({
                            iconType: REMOVE_ICON,
                            click: "flowForm.removeByWorkflowId(" + rowObject.workflowId + ")"
                        });
                    }
                    if (isPermission(basicManage.pageData, FORCE_REMOVE_CODE)) {
                        html += GetIconHtml({
                            iconType: FORCE_REMOVE_ICON,
                            click: "flowForm.forceRemoveByWorkflowId(" + rowObject.workflowId + ")"
                        });
                    }
                    if (isPermission(basicManage.pageData, UNDO_CODE) && rowObject.flowStatus == FLOW_STATUS_AUDITING) {
                        html += GetIconHtml({
                            iconType: UNDO_ICON,
                            click: "flowForm.undoByWorkflowId(" + rowObject.workflowId + ")"
                        });
                    }
                    if (isPermission(basicManage.pageData, QUERY_CODE)) {
                        html += GetIconHtml({
                            iconType: DETAIL_ICON,
                            click: "basicManage.detailById(" + rowObject.id + ")"
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
    $("#btnAdd").click(function () {
        basicManage.add();
    });
});