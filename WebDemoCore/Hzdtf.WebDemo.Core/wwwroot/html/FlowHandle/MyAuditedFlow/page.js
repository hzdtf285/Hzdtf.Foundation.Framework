var basicManage;

var controllUrl = "/api/MyAuditedFlow";

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

        basicManage = new BasicManage({
            pageDataUrl: controllUrl + "/PageData",
            listUrl: controllUrl,
            caption: "我审核的流程列表",
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
                { name: "applyNo", sortable: true, label: "申请单号" },
                { name: "title", sortable: true, label: "标题" },
                { name: "createTime", sortable: true, label: "申请时间", formatter: dateToDateTimeString },
                {
                    name: "flowStatus", sortable: true, label: "流程状态", formatter: function (cellvalue, options, rowObject) {
                        return getFlowStatusText(cellvalue);
                    }
                },
                { name: "currConcreteCensorships", sortable: false, label: "当前关卡" },
                { name: "currHandlers", sortable: false, label: "当前处理人" },
                {
                    label: "操作", sortable: false, width: 100, formatter: function (cellvalue, options, rowObject) {
                        var html = "";
                        var handle = rowObject.handles[0];

                        if (isPermission(basicManage.pageData, QUERY_CODE)) {
                            html += GetIconHtml({
                                iconType: DETAIL_ICON,
                                click: "getFlowDetail(0,\"" + controllUrl + "/GetFlowDetail/" + rowObject.id + "/" + handle.id + "\", \"" + QUERY_CODE + "\", false)",
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