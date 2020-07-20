var basicManage;

$(function () {
    $.validator.setDefaults({
        submitHandler: function (form) {
            var formId = $(form).attr("id");
            if (formId == "editForm") {
                return basicManage.submitForm(function (obj) {
                    if (obj.ownRoles && obj.ownRoles != "") {
                        var str = obj.ownRoles.split(",");
                        var roles = [];
                        for (var i = 0; i < str.length; i++) {
                            roles.push({ id: parseInt(str[i]), name: "guest_role" });
                        }

                        obj.ownRoles = roles;
                    }
                });
            }
            else if (formId == "resetPasswordForm") {
                confirm({
                    text: "重置密码会导致旧密码失效，确认要重置吗？",
                    closeOnConfirm: false,
                    confirmCallback: function () {
                        var form = new Form("resetPasswordForm");
                        var obj = form.getJsonObj();
                        obj.id = $("#txtUserId").val();
                        var url = "/api/User/ResetUserPassword";

                        ajaxJsonAsync(url, MODIFY_REQUEST_TYPE, obj, function (returnInfo, data) {
                            $("button[triggerClose]").trigger('click');
                            showToastr({
                                text: returnInfo.msg,
                                type: "success"
                            });
                        });

                        return false;
                    }
                }); 
            }

            return false;
        }
    });

    var controllUrl = "/api/User";

    basicManage = new BasicManage({
        pageDataUrl: controllUrl + "/PageData",
        listUrl: controllUrl,
        batchRemoveUrl: controllUrl + "/BatchRemove",
        formUrl: "form.html",
        multiselect: true,
        multiselectShowCell: function (cellvalue, options, rowObject) {
            return !rowObject.systemInlay;
        },
        formDialogLoadCompleted: function () {
            appendRoleHtml("ownRoles", basicManage.pageData);

            $("#btnSaveAndAdd").click(function () {
                if ($("#editForm").valid()) {
                    basicManage.submitForm(undefined, false);
                }
            });
        },
        eachEditFormFill: function (controll, name, value) {
            if (name == "ownRoles") {
                if (value && value.length > 0) {
                    var ar = [];
                    for (var i = 0; i < value.length; i++) {
                        ar.push(value[i].id);
                    }

                    controll.val(ar);
                }

                return true;
            }

            return false;
        },
        loadPageDataCompleted: function (data) {
            appendRoleHtml("roleId", data);
        },
        addUrl: controllUrl,
        modifyByIdUrl: controllUrl,
        beforeModifyById: function (id) {
            $("#txtPassword").val("000000000");
            $("#confirmPassword").val("000000000");
            $("#divPassword").hide();
        },
        beforeDetailById: function (id) {
            $("#divPassword").hide();
        },
        removeByIdUrl: controllUrl,
        caption: "用户列表",
        tableId: "table_list",
        pagerId: "pager_list",
        columnModels: [
            { name: "id", sortable: false, key: true, hidden: true },
            { name: "loginId", sortable: false, label: "登录ID", sortable: true },
            { name: "code", sortable: false, label: "编码", sortable: true },
            { name: "name", sortable: false, label: "名称", sortable: true },
            {
                name: "ownRoleName", sortable: false, label: "所属角色", formatter: function (cellvalue, options, rowObject) {
                    if (rowObject.ownRoles && rowObject.ownRoles.length > 0) {
                        var text = "";
                        for (var i = 0; i < rowObject.ownRoles.length; i++) {
                            text += rowObject.ownRoles[i].name + ",";
                        }

                        return text.substring(0, text.length - 1);
                    }
                    return "";
                }
            },
            { name: "enabled", sortable: false, label: "启用", formatter: toBoolText, sortable: true },
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
                    if (isPermission(basicManage.pageData, RESET_PASSWORD_CODE)) {
                        html += GetIconHtml({
                            iconType: RESET_PASSWORD_ICON,
                            click: "basicManage.openResetPassword(" + rowObject.id + ")"
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

    $("#resetPasswordDialog").load("resetPassword.html", function () {
        initControlStyle();
        $("#resetPasswordForm").validate();
    });

    basicManage.init();

    $("#btnQuery").click(function () {
        var condition = new Form("searchForm").getString();
        if ($("#isEnabled").val() != "") {
            condition += "&enabled=" + $("#isEnabled").val();
        }
        basicManage.reLoadList(condition);
    });
    $("#btnAdd").click(function () {
        $("#divPassword").show();
        $("#spanSave").show();
        $("#btnSaveAndAdd").show();

        basicManage.add();
    });
    $("#downExcel").click(function () {
        $("#downExcel").attr("href", controllUrl + "/Export?" + new Form("searchForm").getString());
        return true;
    });

    /**
     * 打开重置密码
     * 
     * @param {any} id
     */
    basicManage.openResetPassword = function (id) {
        var obj = basicManage.getRowData(id);
        $("#resetPasswordForm")[0].reset();
        $("#txtUserId").val(id);
        $("#name").val(obj.name); 
        $("#btnOpenResetPasswordDialog").trigger("click");
    }
});

/**
 * 追加角色HTML
 * 
 * @param {any} controlerName 控件名称
 * @param {any} pageData 网页数据
 */
function appendRoleHtml(controlerName, pageData) {
    if (pageData.roles && pageData.roles.length > 0) {
        var html = "";
        for (var i = 0; i < pageData.roles.length; i++) {
            var role = pageData.roles[i];
            html += "<option value='" + role.id + "'>" + role.name + "</option>";
        }
        $("select[name='" + controlerName +"']").append(html);
    }
}