var basicManage;

$(function () {
    $.validator.setDefaults({
        submitHandler: function (form) {
            ajaxSubmit("editForm", "/AttachmentEx/Upload", ADD_REQUEST_TYPE, function (returnInfo) {
                $("#editForm")[0].reset();
                $("button[triggerClose]").trigger('click');
                showToastr({
                    text: returnInfo.msg,
                    type: "success"
                });
                basicManage.reLoadList(new Form("searchForm").getString());
            });
            return false;
        }
    });
    var controllUrl = "/api/Attachment";

    basicManage = new BasicManage({
        pageDataUrl: controllUrl + "/PageData",
        listUrl: controllUrl,
        formUrl: "form.html",
        addUrl: controllUrl,
        batchRemoveUrl: controllUrl + "/BatchRemove",
        modifyByIdUrl: controllUrl,
        removeByIdUrl: controllUrl,
        caption: "附件列表",
        tableId: "table_list",
        pagerId: "pager_list",
        columnModels: [
            { name: "id", sortable: false, key: true, hidden: true },
            { name: "fileName", sortable: true, label: "文件名" },
            { name: "expandName", sortable: true, label: "扩展名" },
            { name: "fileSize", sortable: true, label: "文件大小（KB）" },
            { name: "title", sortable: true, label: "标题" },
            { name: "ownerType", sortable: true, label: "归属类型" },
            {
                name: "ownerId", sortable: true, label: "归属ID", formatter: function (cellvalue, options, rowObject) {
                    switch (rowObject.ownerId) {
                        case 1:
                            return "业务1";

                        case 2:
                            return "业务2";
                    }
                }
            },
            { name: "memo", sortable: false, label: "备注" },
            {
                label: "操作", sortable: false, width: 100, formatter: function (cellvalue, options, rowObject) {                    
                    var html = "";
                    if (isPermission(basicManage.pageData, DOWNLOAD_CODE) && rowObject.fileAddress != "") {
                        html += "<a href='" + rowObject.fileAddress + "' target='_blank'>" + GetIconHtml({
                            iconType: DOWNLOAD_ICON
                        }) + "</a>";
                    }

                    if (isPermission(basicManage.pageData, REMOVE_CODE)) {
                        html += GetIconHtml({
                            iconType: REMOVE_ICON,
                            click: "basicManage.removeById(" + rowObject.id + ")"
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