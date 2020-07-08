var basicManage;

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

    var controllUrl = "/api/Role";

    basicManage = new BasicManage({
        pageDataUrl: controllUrl + "/PageData",
        listUrl: controllUrl,
        batchRemoveUrl: controllUrl + "/BatchRemove",
        formUrl: "form.html",
        addUrl: controllUrl,
        modifyByIdUrl: controllUrl,
        removeByIdUrl: controllUrl,
        caption: "角色列表",
        tableId: "table_list",
        pagerId: "pager_list",
        multiselect: true,
        multiselectShowCell: function (cellvalue, options, rowObject) {
            return !rowObject.systemInlay;
        },
        formDialogLoadCompleted: function (pageData) {
            $("#btnSaveAndAdd").click(function () {
                if ($("#editForm").valid()) {
                    basicManage.submitForm(undefined, false);
                }
            });
        },
        columnModels: [
            { name: "id", sortable: false, key: true, hidden: true },
            { name: "code", sortable: true, label: "编码" },
            { name: "name", sortable: true, label: "名称" },
            { name: "memo", sortable: false, label: "备注" },
            {
                label: "操作", sortable: false, width: 100, formatter: function (cellvalue, options, rowObject) {                    
                    var html = "";
                    if (!rowObject.systemInlay && isPermission(basicManage.pageData, EDIT_CODE)) {
                        html += GetIconHtml({
                            iconType: EDIT_ICON,
                            click: "basicManage.modifyById(" + rowObject.id + ")"
                        });
                    }
                    if (!rowObject.systemInlay && isPermission(basicManage.pageData, REMOVE_CODE)) {
                        html += GetIconHtml({
                            iconType: REMOVE_ICON,
                            click: "basicManage.removeById(" + rowObject.id + ")"
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
        $("#btnSaveAndAdd").show();
        basicManage.add();
    });
    $("#downExcel").click(function () {
        $("#downExcel").attr("href", controllUrl + "/Export?" + new Form("searchForm").getString());
        return true;
    });
});