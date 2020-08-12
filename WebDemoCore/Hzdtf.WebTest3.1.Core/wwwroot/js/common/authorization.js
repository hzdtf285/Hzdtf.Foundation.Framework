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
     * @param {any} loginUrl 登录URL
     */
    this.login = function (loginInfo, callback, loginUrl) {
        if (loginUrl == undefined || loginUrl == null || loginUrl == "") {
            loginUrl = "/api/Authorization/Login";
        }
        var returnUrl = getUrlParamValue("returnUrl");
        if (returnUrl != "") {
            loginInfo.returnUrl = decodeURIComponent(returnUrl);
        }
        ajaxJsonAsync(loginUrl, ADD_REQUEST_TYPE, loginInfo, function (returnInfo, data) {
            var reUrl = "/";
            if (data && data.returnUrl && data.returnUrl != "") {
                reUrl = data.returnUrl;
            }
            else if (loginInfo.returnUrl && loginInfo.returnUrl != "") {
                reUrl = loginInfo.returnUrl;
            }
            document.location.href = reUrl;

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
     * @ param logoutUrl 登出URL
     * */
    this.logout = function (logoutUrl) {
        if (logoutUrl == undefined || logoutUrl == null || logoutUrl == "") {
            logoutUrl = "/api/Authorization/Logout";
        }
        confirm({
            text: "确定要退出吗?",
            confirmCallback: function () {
                ajaxJsonAsync(logoutUrl, REMOVE_REQUEST_TYPE, null, function (returnInfo) {
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
    this.checkIsVerificationCode = function (url) {
        if (url == undefined || url == null || url == "") {
            url = "/api/Authorization/GetIsVerificationCode?ts=" + Math.random();
        }
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
function refreshVerificationCode(url) {
    if (url == undefined || url == null || url == "") {
        url = "/api/image/BuilderCheckCode?ts=" + Math.random();
    }
    $("#imgCheckCode").attr("src", url);
}