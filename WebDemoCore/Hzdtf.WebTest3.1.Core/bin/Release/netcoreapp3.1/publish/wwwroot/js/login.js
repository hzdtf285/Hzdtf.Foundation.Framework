/**
 * 页面加载完
 */
$(function () {
    var auth = new Authorization();
    $.validator.setDefaults({
        submitHandler: function () {
            var auth = new Authorization();
            var loginInfo = new Form("loginForm").getJsonObj();
            auth.login(loginInfo, function (isSuccess) {
                if (isSuccess) {
                    return;
                }

                $("#loginForm")[0].reset();
            });
            return false;
        }
    });

    $("a[logout]").click(function () {
        auth.logout();
        return false;
    });

   auth.checkIsVerificationCode();

    $("#imgCheckCode").click(function () {
        refreshVerificationCode();
    });

    $("form").validate();
});