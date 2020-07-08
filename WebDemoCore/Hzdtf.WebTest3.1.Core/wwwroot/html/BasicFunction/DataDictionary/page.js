var basicManage;

var currDataDictionaryId;

$(function () {
    $.validator.setDefaults({
        submitHandler: function (form) {
            var formId = $(form).attr("id");
            if (formId == "editForm") {
                return basicManage.submitForm(function (obj) {
                    obj.dataDictionaryId = currDataDictionaryId;
                });
            }

            return false;
        }
    }); 

    var controllUrl = "/api/DataDictionaryItem";

    basicManage = new BasicManage({
        pageDataUrl: controllUrl + "/PageData",
        listUrl: controllUrl,
        caption: "数据字典子项列表",
        formUrl: "form.html",
        addUrl: controllUrl,
        modifyByIdUrl: controllUrl,
        removeByIdUrl: controllUrl,
        beforeModifyById: function (id) {
            basicManageExpand.reLoadList(undefined, 1, controllUrl + "/PageExpandList?dataDictionaryItemId=" + id);
        },
        beforeDetailById: function (id) {
            basicManageExpand.reLoadList(undefined, 1, controllUrl + "/PageExpandList?dataDictionaryItemId=" + id);
        },
        tableId: "table_list",
        pagerId: "pager_list",
        formDialogLoadCompleted: function (pageData) {
            $("#btnSaveAndAdd").click(function () {
                if ($("#editForm").valid()) {
                    basicManage.submitForm(function (obj) {
                        obj.dataDictionaryId = currDataDictionaryId;
                    }, false);
                }
            });
        },
        columnModels: [
            { name: "id", sortable: false, key: true, hidden: true },
            { name: "text", sortable: true, label: "文本" }, 
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
        ],
        lazyLoadList: true,
    });

    basicManage.init(); 

    $("#treeMenu").tree({
        url: controllUrl + "/DataDictionarys",
        method: "get",
        lines: true,
        onSelect: function (node) {
            // 当前数据字典不等于选择的节点才触发
            if (node.id != currDataDictionaryId) {
                currDataDictionaryId = node.id;
                basicManage.reLoadList(undefined, 1, controllUrl + "?dataDictionaryId=" + node.id);
            }
        },
        onLoadError: function (error) {
            showToastr({
                text: error.statusText,
                type: "error"
            })
        }
    });

    $("#btnAdd").click(function () {
        if (currDataDictionaryId == undefined) {
            showToastr({
                type: "warning",
                text: "请先选择数据字典"
            });

            return;
        }

        $("#btnSaveAndAdd").show();
        basicManageExpand.reLoadList(undefined, 1, controllUrl + "/PageExpandList?dataDictionaryItemId=0");
        basicManage.add();
    });
});

/**
 * 获取选中的菜单功能ID数组
 * */
function getCheckMenuFunctionIds() {
    var nodes = $('#treeMenu').tree('getChecked');
    var ids = [];

    for (var i = 0; i < nodes.length; i++) {
        if (nodes[i].type == 2) {
            ids.push(nodes[i].menuFunctionId);
        }
    }

    return ids;
}

/**
 * 遍历树节点
 * 
 * @param {any} treeID 树ID
 * @param {any} nodeCallback 节点回调
 * @param {any} target 节点目标
 */
function eachTreeNode(treeID, nodeCallback, target) {//参数为树的ID，注意不要添加#
    var roots = $('#' + treeID).tree('getRoots', target), children, i, j;
    if (roots && roots.length > 0) {
        for (i = 0; i < roots.length; i++) {
            nodeCallback(roots[i]);
            eachChildTreeNode(treeID, nodeCallback, roots[i].target)
        }
    }
}  

/**
 * 遍历子树节点
 * 
 * @param {any} treeID 树ID
 * @param {any} nodeCallback 节点回调
 * @param {any} target 节点目标
 */
function eachChildTreeNode(treeID, nodeCallback, target) {//参数为树的ID，注意不要添加#
    var roots = $('#' + treeID).tree('getChildren', target), children, i, j;
    if (roots && roots.length > 0) {
        for (i = 0; i < roots.length; i++) {
            nodeCallback(roots[i]);
            eachChildTreeNode(treeID, nodeCallback, roots[i].target)
        }
    }
}  