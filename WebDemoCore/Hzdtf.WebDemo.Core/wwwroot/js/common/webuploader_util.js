/**
 * Web上传辅助类
 * */
function WebUploaderUtil(param) {
    /**
     * 重新注册选择文件事件
     * 
     */
    this.reRegisterSelectFileEvent = function() {
        var divId = "#" + param.filePickerId;
        var divObj = $(divId + " div:eq(0)");
        divObj.click(function () {
            $(divId + " div input[type='file']").click();
            return false;
        });
    }
}