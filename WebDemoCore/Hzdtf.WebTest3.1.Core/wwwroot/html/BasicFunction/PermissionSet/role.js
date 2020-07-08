var basicManage;

$(function () {
    var controllUrl = "/api/RolePermission";
    $("#treeMenu").height($(window).height() - 150);

    basicManage = new BasicManage({
        pageDataUrl: controllUrl + "/PageData",
        listUrl: controllUrl,
        caption: "角色列表",
        tableId: "table_list",
        pagerId: "pager_list",
        columnModels: [
            { name: "id", sortable: false, key: true, hidden: true },
            { name: "name", sortable: true, label: "名称" },
            { name: "memo", sortable: false, label: "备注" }
        ],
        onSelectRow: function (id) {
            ajaxJsonAsync(controllUrl + "/HaveMenuFunctions?roleId=" + id, QUERY_REQUEST_TYPE, null, function (returnInfo, data) {
                eachTreeNode("treeMenu", function (node) {
                    var checkState = "uncheck";
                    if (data && data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].id == node.menuFunctionId) {
                                checkState = "check";
                                break;
                            }
                        }
                    }
                    $('#treeMenu').tree(checkState, node.target);
                });
            });
        }
    });

    basicManage.init(); 

    $("#btnQuery").click(function () {
        basicManage.reLoadList(new Form("searchForm").getString());
    });

    $("#treeMenu").tree({
        url: controllUrl + "/MenuTrees",
        method: "get",
        animate: true,
        lines: true,
        checkbox: true,
        onLoadError: function (error) {
            showToastr({
                text: error.statusText,
                type: "error"
            })
        }
    });

    $("#btnSave").click(function () {
        var roleId = basicManage.getSelectRowId();
        if (roleId == undefined || roleId == null || roleId == "") {
            showToastr({
                type: "warning",
                text: "请先选择角色"
            });

            return;
        }

        var menuFunIds = getCheckMenuFunctionIds();
        ajaxJsonAsync(controllUrl + "/SavePermission?roleId=" + roleId, MODIFY_REQUEST_TYPE, menuFunIds, function (returnInfo, data) {
            showToastr({
                text: returnInfo.msg,
                type: "success"
            });
        });
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