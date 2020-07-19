/**
 * 
 * 授权
 */
function Authorization() {
    /**
     * 登录
     * 
     * @param {any} loginInfo 登录信息
     * @param {any} callback 回调
     */
    this.login = function (loginInfo, callback) {
        ajaxJsonAsync("/api/Authorization/Login", ADD_REQUEST_TYPE, loginInfo, function (returnInfo) {
            document.location.href = "/";

            if (typeof (callback) == "function") {
                callback(true);
            }
        }, function (returnInfo) {
            if (returnInfo.data) {
                controlVerificationCode(returnInfo.data.isVerificationCode);
            }
            showToastr({
                text: returnInfo.msg,
                type: "error"
            });

            if (typeof (callback) == "function") {
                callback(false);
            }
        });
    }

    /**
     * 登出
     * */
    this.logout = function () {
        confirm({
            text: "确定要退出吗?",
            confirmCallback: function () {
                ajaxJsonAsync("/api/Authorization/Logout", REMOVE_REQUEST_TYPE, null, function (returnInfo) {
                    document.location.href = "/login.html";
                }, function (returnInfo) {
                    showToastr({
                        text: returnInfo.msg,
                        type: "error"
                    });
                });
            },
            closeOnConfirm: false
        });
    }

    /**
     * 检查是否需要验证码
     * */
    this.checkIsVerificationCode = function() {
        var url = "/api/Authorization/GetIsVerificationCode?ts=" + Math.random();
        ajaxJsonAsync(url, QUERY_REQUEST_TYPE, null, function (returnInfo, data) {
            controlVerificationCode(data);
        });
    }

    /**
     * 控制验证码
     * 
     * @param {any} isVerificationCode 是否需要验证码
     */
    function controlVerificationCode(isVerificationCode) {
        if (isVerificationCode) {
            $("#spanCheckCode").show();
            $("input[name='verificationCode']").attr("required", "");
            this.refreshVerificationCode();
        }
        else {
            $("#spanCheckCode").hide();
            $("input[name='verificationCode']").removeAttr("required");
        }
    }
}

/**
 * 刷新验证码
 * */
function refreshVerificationCode() {
    $("#imgCheckCode").attr("src", "/api/image/BuilderCheckCode?ts=" + Math.random());
}