/**
 * jqGrid封装
 */
function AJqGrid(param) {
    // 设置表格默认样式
    $.jgrid.defaults.styleUI = "Bootstrap";
    /*
     * 当前Grid行数据
     * */
    this.currAJqGridRowData = [];

    var rows;
    if (param.pageSize) {
    	rows = param.pageSize;
    }
    else {
        rows = PAGE_SIZE;
    }
   
    var tableId = "#" + param.tableId;

    var thisObj = this;

    /**
     * key是否是数字，默认为是
     * */
    this.isKeyNum = true;

    /**
     * 获取复选框HTML
     * 
     * @param {any} value 值
     * @param {any} tag 标签
     * 
     */
    function getCheckHtml(value, tag) {
        return "<input type='checkbox' checktag='" + tag + "' class='i-checks' value='" + value + "' />";
    }

    // 多选
    if (param.multiselect) {
        var colKey;
        for (var i = 0; i < param.columnModels.length; i++) {
            if (param.columnModels[i].key) {
                colKey = param.columnModels[i].name;
                break;
            }
        }

        var col = {
            width: 40,
            sortable: false,
            label: "<a id='checkall" + param.tableId + "' href='#' onclick='triggerCheckAll(this.id, \"" + param.tableId + "\", \"" + param.tableId + "\")'>全选</a>",
            formatter: function (cellvalue, options, rowObject) {
                if (typeof (param.multiselectShowCell) == "function") {
                    if (!param.multiselectShowCell(cellvalue, options, rowObject)) {
                        return "";
                    }
                }

                var keyValue = rowObject[colKey];

                return getCheckHtml(keyValue, param.tableId);
            }
        };
        param.columnModels.splice(0, 0, col);
    }

    // 渲染表格
    $(tableId).jqGrid({
        url: param.url,
        datatype: "json",
        height: "100%",
        width: "100%",
        autowidth: true,
        shrinkToFit: true,
        colModel: param.columnModels,
        rowNum: rows,
        pager: param.pagerId,
        rownumbers: true,
        mtype: "get",
        onSelectRow: function (id) {
            if (typeof (param.onSelectRow) == "function") {
                param.onSelectRow(id);
            }
        },
        loadComplete: function (xhr) {
            thisObj.currAJqGridRowData = [];
            returnInfoHandle(xhr);
            if (xhr.rows) {
                thisObj.currAJqGridRowData = xhr.rows;

                if (param.mergeColumns && param.mergeColumns.length > 0) {
                    for (var i = 0; i < param.mergeColumns.length; i++) {
                        merge(param.mergeColumns[i]);
                    }
                }

                if (typeof (param.loadCompleteRows) == "function") {
                    param.loadCompleteRows(thisObj.currAJqGridRowData);
                }
            }

            if (typeof (initControlStyle) == "function") {
                initControlStyle();
            }
        },
        loadError: function (xhr, status, error) {
            if (!valiAuthFromStatus(xhr.status)) {
                return;
            }

            showToastr({
        		text: status + "：" + error,
        		type: "error"
        	});
        },
        viewrecords: true,
        caption: param.caption,
        ajaxGridOptions: {
        	scriptCharset: "utf-8",
    		contentType: "application/x-www-form-urlencoded;charset=utf-8"
        },
        autoencode: true,
        footerrow: param.footerrow,
        gridComplete: param.gridComplete
    });

    /**
     * 根据主键获取行数据
     */
    this.getRowData = function (pk) {
        return $(tableId).jqGrid("getRowData", pk);
    }

    /**
     * 追加URL参数并重新加载数据
     */
    this.appendUrlParamAndReLoadData = function (param2, page, url) {
        if (!url || url == "") {
            var temp;
            var index = param.url.indexOf("?");
            if (index == -1) {
                temp = "?";
            }
            else {
                var arr = param.url.substring(index + 1).split(',');
                for (var i = 0; i < arr.length; i++) {
                    var pr = arr[i].split('=');
                    var pLow = pr[0].toLowerCase();
                }
                temp = "?&";
            }

            if (index == -1) {
                url = param.url + temp + param2;
            }
            else {
                url = param.url.substring(0, index) + temp + param2;
            }
        }    	
        
        var gridPara = {
            url: url
        }
        if (page) {
            gridPara.page = page;
        }

        $(tableId).jqGrid("setGridParam", gridPara).trigger("reloadGrid");     
    }

    /**
     * 重新加载数据
     */
    this.reLoadData = function () {
        $(tableId).trigger("reloadGrid");
     }

     /**
      * 根据ID获取完整行数据
      * 
      * @param {any} id ID
      */
    this.getFullRowDataById = function (id) {
        if (thisObj.currAJqGridRowData && thisObj.currAJqGridRowData.length > 0) {
            for (var i = 0; i < thisObj.currAJqGridRowData.length; i++) {
                if (thisObj.currAJqGridRowData[i].id == id) {
                     return thisObj.currAJqGridRowData[i];
                 }
             }
         }
    }

    /**
     * 合并列
     * 
     * @param {any} cellName 列名
     */
    function merge(cellName) {
        //得到显示到界面的id集合
        var tabObj = $(tableId);
        var mya = tabObj.getDataIDs();
        //当前显示多少条
        var length = mya.length;
        for (var i = 0; i < length; i++) {
            //从上到下获取一条信息
            var before = tabObj.jqGrid('getRowData', mya[i]);
            //定义合并行数
            var rowSpanTaxCount = 1;
            for (j = i + 1; j <= length; j++) {
                //和上边的信息对比 如果值一样就合并行数+1 然后设置rowspan 让当前单元格隐藏
                var end = tabObj.jqGrid('getRowData', mya[j]);
                if (before[cellName] == end[cellName]) {
                    rowSpanTaxCount++;
                    tabObj.setCell(mya[j], cellName, '', { display: 'none' });
                } else {
                    rowSpanTaxCount = 1;
                    break;
                }

                $("#" + cellName + mya[i]).attr("rowspan", rowSpanTaxCount);
            }
        }
    }
    
    /**
     * 获取选中的多个主键
     * 
     */
    this.multiselectKeys = function () {
        var checkArrays = $("#" + param.tableId + " input[type='checkbox'][checktag='" + param.tableId + "']");
        if (checkArrays && checkArrays.length > 0) {
            var keys = [];
            for (var i = 0; i < checkArrays.length; i++) {
                if ($(checkArrays[i]).prop("checked")) {
                    var key = $(checkArrays[i]).val();
                    var val = undefined;
                    if (this.isKeyNum) {
                        val = parseInt(key);
                    }
                    else {
                        val = key;
                    }

                    keys.push(val);
                }
            }

            return keys;
        }

        return [];
    }

    /**
     * 获取数据
     * */
    this.getData = function () {
        return this.currAJqGridRowData;
    }
}

/**
 * 触发全选
 * 
 * @param {any} id ID
 * @param {any} tableId 表格ID
 * @param {any} tag 标签
 */
function triggerCheckAll(id, tableId, tag) {
    var checkArrays = $("#" + tableId + " input[type='checkbox'][checktag='" + tag + "']");
    var obj = $("#" + id);
    var isChecked;

    if (obj.text() == "全选") {
        obj.text("取消");
        isChecked = true;
    }
    else {
        obj.text("全选");
        isChecked = false;
    }

    if (checkArrays && checkArrays.length > 0) {
        for (var i = 0; i < checkArrays.length; i++) {
            $(checkArrays[i]).prop({ checked: isChecked});
        }

        if (typeof (initControlStyle) == "function") {
            initControlStyle();
        }
    }
}