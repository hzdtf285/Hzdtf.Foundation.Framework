$().ready(function () {
    var tempKeyAmount;
    $("input[amount]").keydown(function (e) {
        return isKeyInAmount(e.keyCode, $(this).val());
    });
    $("input[amount]").keypress(function (e) {
        tempKeyAmount = e.target.value;
        return true;
    });
    $("input[amount]").keyup(function (e) {
        var val = e.target.value;
        if (val && val.length > 0) {
            var pointIndex = val.indexOf(".");
            if (pointIndex == -1) {
                return true;
            }

            var lastValue = val.substring(pointIndex + 1, val.length);
            var result = lastValue.length > 2 ? false : true;
            if (!result) {
                $(this).val(tempKeyAmount);
            }

            return result;
        }

        return true;
    });
    $("input[amount]").change(function (e) {
        var val = $(this).val();
        if ($.trim(val) == "") {
            $(this).val("0");
        }

        var changeTrigger = $(this).attr("changeTrigger");
        if (changeTrigger && changeTrigger != "") {
            eval(changeTrigger);
        }

        return true;
    });
});