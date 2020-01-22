/**
 * 数据表
 * 
 * @param {any} name 表名
 */
function DataTable(name) {
    /**
     * 表名
     * */
    var name = name;

    /**
     * 数据列集合
     * */
    var dataColumns = [];

    /**
     * 数据行集合
     * */
    var dataRows = [];

    /**
     * 获取表名
     * */
    this.getName = function () {
        return name;
    }

    /**
     * 添加列
     * 
     * @param {any} columnName 列名
     * @param {any} columnText 列文本
     */
    this.addColumn = function (columnName, columnText) {
        dataColumns.push(new DataColumn(columnName, columnText));
    }

    /**
     * 获取列集合
     * */
    this.getColumns = function () {
        return dataColumns;
    }

    /**
     * 根据列索引获取列
     * 
     * @param {any} columnIndex 列索引
     */
    this.getColumnByColumnIndex = function (columnIndex) {
        if (columnIndex < 0 || columnIndex >= dataColumns.length) {
            return;
        }

        return dataColumns[columnIndex];
    }

    /**
     * 根据列名获取列
     * 
     * @param {any} columnName 列名
     */
    this.getColumnByColumnName = function (columnName) {
        return dataColumns(getDataColumnIndexByColumnName(columnName));
    }

    /**
     * 新建行
     * */
    this.newRow = function () {
        return new DataRow(dataColumns);
    }

    /**
     * 添加行
     * 
     * @param {any} dataRow 数据行
     */
    this.addRow = function (dataRow) {
        dataRows.push(dataRow);
    }

    /**
     * 获取行集合
     * */
    this.getRows = function () {
        return dataRows;
    }

    /**
     * 根据行索引获取行
     * 
     * @param {any} rowIndex 行索引
     */
    this.getRowByRowIndex = function (rowIndex) {
        if (rowIndex < 0 || rowIndex >= dataRows.length) {
            return;
        }

        return dataRows[rowIndex];
    }

    /**
     * 根据列名获取列索引
     * 
     * @param {any} columnName 列名
     */
    function getDataColumnIndexByColumnName(columnName) {
        for (var i = 0; i < dataColumns.length; i++) {
            var dc = dataColumns[i];
            if (dc.getName() == columnName) {
                return i;
            }
        }

        return -1;
    }

    /**
     * 转换JSON
     * */
    this.toJson = function () {
        var rowArray = [];

        if (dataRows.length == 0) {
            return rowArray;
        }

        for (var i = 0; i < dataRows.length; i++) {
            var rowJson = dataRows[i].toJson();
            rowArray.push(rowJson);
        }

        return rowArray;
    }    
}

/**
 * 数据列
 * 
 * @param {any} name 列名
 * @param {any} text 文本
 */
function DataColumn(name, text) {
    /**
     * 列名
     * */
    var name = name;

    /**
     * 文本
     * */
    var text = text;

    /**
     * 获取列名
     * */
    this.getName = function () {
        return name;
    }

    /**
     * 获取文本
     * */
    this.getText = function () {
        return text;
    }

    /**
     * 转换JSON
     * */
    this.toJson = function () {
        return {
            name: name,
            text: text
        }
    }
}

/**
 * 数据行
 * 
 * @param {any} dataColumns 数据列集合
 */
function DataRow(dataColumns) {
    /**
     * 数据列集合
     * */
    var dataColumns = dataColumns;

    /**
     * 值集合
     * */
    var values = new Array(dataColumns.length);

    /**
     * 根据列索引获取值
     * 
     * @param {any} columnIndex 列索引
     */
    this.getValueByColumnIndex = function (columnIndex) {
        if (columnIndex < 0 || columnIndex >= dataColumns.length) {
            return;
        }

        return values[columnIndex];
    }

    /**
     * 根据列名获取值
     * 
     * @param {any} columnName 列名
     */
    this.getValueByColumnName = function (columnName) {
        return this.getValueByColumnIndex(getDataColumnIndexByColumnName(columnName));
    }

    /**
     * 根据列索引设置值
     * 
     * @param {any} columnIndex 列索引
     * @param {any} value 值
     */
    this.setValueByColumnIndex = function (columnIndex, value) {
        if (columnIndex < 0 || columnIndex >= dataColumns.length) {
            return;
        }

        values[columnIndex] = value;
    }

    /**
     * 根据列名设置值
     * 
     * @param {any} columnName 列名
     * @param {any} value 值
     */
    this.setValueByColumnName = function (columnName, value) {
        return this.setValueByColumnIndex(getDataColumnIndexByColumnName(columnName), value);
    }

    /**
     * 根据列名获取列
     * 
     * @param {any} columnName 列名
     */
    function getDataColumnByColumnName(columnName) {
        for (var i = 0; i < dataColumns.length; i++) {
            var dc = dataColumns[i];
            if (dc.getName() == columnName) {
                return dc;
            }
        }
    }

    /**
     * 根据列名获取列索引
     * 
     * @param {any} columnName 列名
     */
    function getDataColumnIndexByColumnName(columnName) {
        for (var i = 0; i < dataColumns.length; i++) {
            var dc = dataColumns[i];
            if (dc.getName() == columnName) {
                return i;
            }
        }

        return -1;
    }

    /**
     * 转换JSON
     * */
    this.toJson = function () {
        var obj = {};

        if (dataColumns == undefined || dataColumns.length == 0
            || values == undefined || values.length == 0) {
            return obj;
        }

        for (var i = 0; i < values.length; i++) {
            obj[dataColumns[i].getName()] = values[i];
        }

        return obj;
    }
}