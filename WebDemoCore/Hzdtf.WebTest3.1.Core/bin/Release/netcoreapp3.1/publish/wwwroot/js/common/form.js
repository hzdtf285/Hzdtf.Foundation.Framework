/**
 * 表单
 */
function Form(formId) {
    /**
     * 获取控件集合
     */
    function getControlls() {
        var controlls;
        var controlEle = "input,select,textarea";
        if (formId) {
            controlls = $("#" + formId).find(controlEle);
        }
        else {
            controlls = $("form").find(controlEle);
        }

        return controlls;
    }

    // 表单控件集合
    var controlls = getControlls();

    /**
     * 遍历控件集合
     */
    this.eachControlls = function (callback) {
        if (controlls && controlls.length > 0) {
            for (var i = 0; i < controlls.length; i++) {
                callback($(controlls[i]));
            }
        }
    }

    /**
     * 根据控件获取值
     * 
     * @param {any} con 控件
     */
    function getValueByCon(con) {
        var name = con.attr("name");
        var val = "";
        var tag = con.get(0).tagName.toUpperCase();
        if (tag == "SELECT" && con.attr("multiple")) {
            var vals = con.val();
            if (vals && vals.length > 0) {
                for (var j = 0; j < vals.length; j++) {
                    val += vals[j] + ",";
                }
                if (val.length > 0) {
                    val = val.substring(0, val.length - 1);
                }
            }
            else {
                return "";
            }
        }
        else if (tag == "INPUT" && con.attr("type") == "radio") {
            val = $("input[name='" + name + "']:checked").val();
        }
        else {
            val = $.trim(con.val());
        }

        if (val != "") {
            var format = con.attr("format");
            switch (format) {
                case "int":
                val = parseInt(val);

                    break;

                case "float":
                    val = parseFloat(val);
                    
                    break;

                case "num":
                case "number":
                    val = Number(val);

                    break;
            }
            if (con.attr("format") == "fenToYuan" && val && val != "") {
                val = yuanToFen(val);
            }
            else if (con.attr("format") == "boolean") {
                val = toBoolValue(val);
            }
        }

        return val;
    }

    /**
     * 获取JSON对象
     */
    this.getJsonObj = function () {
        var obj = {};
        this.eachControlls(function (con, val) {
            var val = getValueByCon(con);
            if (val != undefined) {
                obj[con.attr("name")] = val;
            }
        });
        
        return obj;
    }

    /**
     * 获取字符串
     */
    this.getString = function () {
        var obj = "";
        this.eachControlls(function (con) {
            var val = getValueByCon(con);
            if (val != undefined) {
                obj += con.attr("name") + "=" + val + "&";
            }
        });
        if (obj.length > 0) {
            obj = obj.substring(0, obj.length - 1);
        }

        return obj;
    }

    /**
     * 填充
     */
    this.fill = function (obj, eachCallback, disabled) {
        this.eachControlls(function (con) {
            if (disabled || con.attr("initDisabled")) {
                con.attr("disabled", "disabled");
            }
            else {
                con.removeAttr("disabled");
            }

            if (obj == undefined || obj == null) {
                return;
            }

            var name = con.attr("name");
            var v = obj[name];
            if (typeof (eachCallback) == "function" && eachCallback(con, name, v)) {
                return;
            } 
            if (con.attr("format") == "date") {
                v = dateToDateString(v);
                var labId = "#span" + con.attr("name");
                try {
                    if (disabled || con.attr("initDisabled")) {
                        $(labId).hide();
                    }
                    else {
                        $(labId).show();
                    }
                }
                catch (e) { }
            }
            if (con.attr("format") == "ym") {
                v = dateToYMString(v);
            }
            else if (con.attr("format") == "datetime") {
                v = dateToDateTimeString(v);
                var labId = "#span" + con.attr("name");
                try {
                    if (disabled || con.attr("initDisabled")) {
                        $(labId).hide();
                    }
                    else {
                        $(labId).show();
                    }
                }
                catch (e) { }
            }
            else if (con.attr("format") == "fenToYuan") {
                v = fenToYuan(v);
            }

            if (con.attr("type") == "radio") {
                $("#" + name + "_" + (v + "").toLowerCase()).prop("checked", true);
                return;
            }

            con.val(v);
        });
    }
}

/**
 * 初始化控件样式
 * */
function initControlStyle() {
    try {
        $(".i-checks").iCheck({
            checkboxClass: "icheckbox_square-green",
            radioClass: "iradio_square-green",
        })
    }
    catch (e) {
        console.log(e);
    }
}