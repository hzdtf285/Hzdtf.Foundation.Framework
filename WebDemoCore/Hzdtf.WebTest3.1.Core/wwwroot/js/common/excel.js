/**
 * 读取Excel
 *
 * @param fileControl 文件控件
 * @param callbackReadedProtoData 回调读取到原生数据
 * @param callbackReadedDataTable 回调读取到表格数据
 * @param callbackGetColumnName 回调获取列名称
 */
function readerExcel(fileControl, callbackReadedProtoData, callbackReadedDataTable, callbackGetColumnName) {
    if(!fileControl.files || (callbackReadedProtoData == undefined || callbackReadedDataTable == undefined)) {
        return;
    }

    for (var i = 0; i < fileControl.files.length; i++) {
        var f = fileControl.files[i];
        var reader = new FileReader();
        reader.onload = function (e) {
            var data = e.target.result;
            wb = XLSX.read(data, {
                type: 'binary'
            });

            //wb.SheetNames[0]是获取Sheets中第一个Sheet的名字
            //wb.Sheets[Sheet名]获取第一个Sheet的数据
            let result = XLSX.utils.sheet_to_json(wb.Sheets[wb.SheetNames[0]]);

            let funProtoData = callbackReadedProtoData;
            let funDataTable = callbackReadedDataTable;

            if (result == undefined || result.length == 0) {
                if (callbackReadedProtoData != undefined) {
                    callbackReadedProtoData(result);
                }
                if (callbackReadedDataTable != undefined) {
                    callbackReadedDataTable(new DataTable());
                }

                return;
            }
            if (callbackReadedProtoData != undefined) {
                callbackReadedProtoData(result);
            }
            if (callbackReadedDataTable == undefined) {
                return;
            }

            // Excel列名映射自定义的列名
            let excelColNameMapCustomerName = [];

            let dt = new DataTable();
            // 将第一行的KEY作为列
            for (let key in result[0]) {
                let colName = undefined;
                if (callbackGetColumnName == undefined) {
                    colName = key;
                }
                else {
                    colName = callbackGetColumnName(key);
                }
                excelColNameMapCustomerName.push({ key: key, value: colName });

                dt.addColumn(colName, key);
            }

            // 填充行
            for (let i = 0; i < result.length; i++) {
                let resRow = result[i];
                let row = dt.newRow();

                for (let key in resRow) {
                    let col = key;
                    for (let j = 0; j < excelColNameMapCustomerName.length; j++) {
                        if (excelColNameMapCustomerName[j].key == key) {
                            col = excelColNameMapCustomerName[j].value;

                            break;
                        }
                    }

                    row.setValueByColumnName(col, resRow[key]);
                }

                dt.addRow(row);
            }
            callbackReadedDataTable(dt);
        };

        reader.readAsBinaryString(f);
    }
}