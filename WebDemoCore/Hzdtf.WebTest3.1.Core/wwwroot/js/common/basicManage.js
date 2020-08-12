/**
 * 基本管理对象
 * 提供基本的添加/编辑/删除/查询列表
 * */
function BasicManage(param) {
    var aJqGrid;

    /**
     * 页面数据
     * */
    this.pageData;

    var thisBasicMange = this;

    /**
     * 原始行数据
     * 
     * */
    this.rawRows;

    /**
     * 初始化
     * @param callback 回调
     * */
    this.init = function (callback) {
        // 获取页面数据，包含权限信息
        if (param.pageDataUrl) {
            ajaxJsonAsync(param.pageDataUrl, QUERY_REQUEST_TYPE, null, function (returnInfo, data) {
                thisBasicMange.pageData = data;

                if (!data || !data.functions || data.functions.length == 0) {
                    showToastr({
                        type: "warning",
                        text: "Sorry,您没有此功能的任何权限"
                    });
                    return;
                }

                if (typeof (param.loadPageDataCompleted) == "function") {
                    param.loadPageDataCompleted(data);
                }

                this.functions = data.functions;

                var isLoadForm = false;

                for (var i = 0; i < data.functions.length; i++) {
                    var fun = data.functions[i];
                    $("button[functionCode='" + fun.code + "']").show();
                    $("a[functionCode='" + fun.code + "']").show();

                    if (fun.code == QUERY_CODE) {
                        if (!param.lazyLoadList) {
                            var condition = new Form("searchForm").getString();
                            loadList(condition);
                        }

                        if (param.detailUrl) {
                            $("#detailDialog").load(param.detailUrl);
                        }
                    }
                    if (param.formUrl && !isLoadForm) {
                        isLoadForm = true;
                        $("#formDialog").load(param.formUrl, function () {
                            if (typeof (param.formDialogLoadCompleted) == "function") {
                                param.formDialogLoadCompleted(data);
                            }

                            for (var i = 0; i < data.functions.length; i++) {
                                var fun = data.functions[i];
                                $("button[functionCode='" + fun.code + "']").show();
                            }

                            initControlStyle();
                            $("#editForm").validate();
                        });
                    }

                    if (fun.code == REMOVE_CODE) {
                        $("#btnBatchRemove").click(function () {
                            var keys = thisBasicMange.multiselectKeys();
                            if (keys && keys.length > 0) {
                                confirm({
                                    text: "确认要移除所勾选的数据吗？",
                                    closeOnConfirm: false,
                                    confirmCallback: function () {
                                        ajaxJsonAsync(param.batchRemoveUrl, REMOVE_REQUEST_TYPE, keys, function (returnInfo, data) {
                                            showToastr({
                                                text: returnInfo.msg,
                                                type: "success"
                                            });
                                            $("#" + param.tableId).trigger("reloadGrid");
                                        });
                                    }
                                });
                            }
                            else {
                                showToastr({
                                    type: "warning",
                                    text: "请至少勾选一条数据"
                                });
                            }
                        });
                    }
                }
            });
        }
        else {
            var condition = new Form("searchForm").getString();
            loadList(condition, param.listUrl);
        }

        if (typeof (callback) == "function") {
            callback();
        }
    }

    /**
     * 加载列表
     * */
    function loadList(condition, url) {
        if (!url || url == "") {
            if (condition && condition != "") {
                var temp;
                if (param.listUrl.indexOf("?") == -1) {
                    temp = "?";
                }
                else {
                    temp = "&";
                }
                url = param.listUrl + temp + condition;
            }
            else {
                url = param.listUrl;
            }
        }

        if (param.isKeyNum == undefined) {
            param.isKeyNum = true;
        }

        aJqGrid = new AJqGrid({
            tableId: param.tableId,
            pagerId: param.pagerId,
            url: url,
            multiselect: param.multiselect,
            multiselectShowCell: param.multiselectShowCell,
            caption: param.caption,
            columnModels: param.columnModels,
            mergeColumns: param.mergeColumns,
            isKeyNum: param.isKeyNum,
            onSelectRow: function (id) {
                if (typeof (param.onSelectRow) == "function") {
                    param.onSelectRow(id);
                }
            },
            footerrow: param.footerrow,
            gridComplete: param.gridComplete,
            loadCompleteRows: function (rows) {
                thisBasicMange.rawRows = rows;
            }
        });
    }

    /**
     * 重新加载列表
     * 
     * @param {any} condition 条件
     * @param {any} page 页码
     * @param url URl
     */
    this.reLoadList = function (condition, page, url) {
        if (!page) {
            page = 1;
        }
        if (aJqGrid) {
            aJqGrid.appendUrlParamAndReLoadData(condition, page, url);
        }
        else {
            loadList(condition, url);
        }
    }

    /**
     * 提交表单
     * @param callback 回调
     * @param isCloseDialog 是否关闭对话框，如果没传，则默认关闭
     * 
     * */
    this.submitForm = function (callback, isCloseDialog) {
        if (isCloseDialog == undefined) {
            isCloseDialog = true;
        }

        var form = new Form("editForm");
        var obj = form.getJsonObj();

        var url;
        var method;
        var actionType;
        if (obj.id && obj.id > 0) {
            actionType = EDIT_CODE;
            url = param.modifyByIdUrl;
            if (url.indexOf("?") == -1) {
                url += "?id=" + obj.id;
            }
            else {
                url += "&id=" + obj.id;
            }
            method = MODIFY_REQUEST_TYPE;
        }
        else {
            actionType = ADD_CODE;
            url = param.addUrl;
            method = ADD_REQUEST_TYPE;
        }

        if (typeof (callback) == "function") {
            callback(obj);
        }

        ajaxJsonAsync(url, method, obj, function (returnInfo, data) {
            $("#editForm")[0].reset();

            if (isCloseDialog) {
                $("button[triggerClose]").trigger('click');
            }
            showToastr({
                text: returnInfo.msg,
                type: "success"
            });
            $("#" + param.tableId).trigger("reloadGrid");
        });
    }

    /**
     * 根据ID修改
     * @param {*} id 
     */
    this.modifyById = function (id) {
        if (typeof (param.beforeModifyById) == "function") {
            param.beforeModifyById(id);
        }

        $("#spanSave").show();
        $("#editForm")[0].reset();
        $("#btnSaveAndAdd").hide();
        this.formFill(id, false);

        $("#preFormTitle").text("编辑");

        $("#btnOpenEditDialog").trigger("click");
    }

    /**
     * 根据ID查看明细
     * @param {*} id 
     */
    this.detailById = function (id) {
        if (typeof (param.beforeDetailById) == "function") {
            param.beforeDetailById(id);
        }
        $("#spanSave").hide();

        $("#editForm")[0].reset();
        this.formFill(id, true);

        $("#preFormTitle").text("查看");

        $("#btnOpenEditDialog").trigger("click");
    }

    /**
     * 根据ID删除
     * 
     * @param {any} id ID
     */
    this.removeById = function (id) {
        var url = param.removeByIdUrl;
        if (url.indexOf("?") == -1) {
            url += "?id=" + id;
        }
        else {
            url += "&id=" + id;
        }
        confirm({
            text: "确认要移除吗？",
            closeOnConfirm: false,
            confirmCallback: function () {
                ajaxJsonAsync(url, REMOVE_REQUEST_TYPE, null, function (returnInfo, data) {
                    showToastr({
                        text: returnInfo.msg,
                        type: "success"
                    });
                    $("#" + param.tableId).trigger("reloadGrid");
                });
            }
        }); 
    }

    /**    
     * 表单填充
     * @param {any} id ID
     */
    this.formFill = function (id, disabled) {
        var currRowData = this.getRawRowData(id);
        var form = new Form("editForm");
        form.fill(currRowData, param.eachEditFormFill, disabled);

        initControlStyle();
    }

    /*
     * 添加
     * 
     * */
    this.add = function () {
        $("#spanSave").show();
        $("#editForm")[0].reset();
        $("#preFormTitle").text("添加");
        $("#btnOpenEditDialog").trigger("click");
        $("#editForm input[name='id']").val("0");

        this.formFill(0, false);
    }
    
    /**
     * 获取行数据
     * 
     * @param {any} id ID
     */
    this.getRowData = function (id) {
        return aJqGrid.getFullRowDataById(id);
    }

    /**
     * 获取选中的行ID
     * */
    this.getSelectRowId = function () {
        return $('#' + param.tableId).jqGrid('getGridParam', 'selrow');
    }

    /**
     * 获取选中的多个主键
     * 
     */
    this.multiselectKeys = function () {
        return aJqGrid.multiselectKeys();
    }

    /**
     * 根据ID获取原始行数据
     * 
     * @param {any} id ID
     */
    this.getRawRowData = function (id) {
        if (this.rawRows && this.rawRows.length > 0) {
            for (var i = 0; i < this.rawRows.length; i++) {
                if (this.rawRows[i].id == id) {
                    return this.rawRows[i];
                }
            }
        }
    }

    /**
     * 
     * 选择行回调
     * 
     * @param {any} id ID
     */
    this.selectRowCallback = function (id) {
        if (typeof (param.selectRowCallback) == "function") {
            param.selectRowCallback(aJqGrid.getFullRowDataById(id));
        }
    }
}