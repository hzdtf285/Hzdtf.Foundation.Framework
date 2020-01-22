/**
 * 流程表单
 * */
function FlowForm(param) {
    /**
     * 保存
     * */
    this.save = function (formData) {
        submit(formData, param.controllerUrl + "/Save", SAVE_CODE);
    }

    /**
     * 申请
     * */
    this.apply = function (formData) {
        submit(formData, param.controllerUrl + "/Apply", APPLY_CODE);
    }

    /**
     * 根据工作流ID移除
     * 
     * @param {any} workflowId 工作流ID
     */
    this.removeByWorkflowId = function (workflowId) {
        execBeforeCallback(REMOVE_CODE, workflowId);

        confirm({
            text: "确定要移除吗？",
            closeOnConfirm: false,
            confirmCallback: function () {
                ajaxJsonAsync(param.controllerUrl + "/RemoveByWorkflowId" + "?workflowId=" + workflowId, REMOVE_REQUEST_TYPE, null, function (returnInfo, data) {
                    showToastr({
                        text: returnInfo.msg,
                        type: "success"
                    });

                    execSuccessCallback(REMOVE_CODE, data);
                });
            }
        });        
    }

    /**
     * 根据工作流ID强制移除
     * 
     * @param {any} workflowId 工作流ID
     */
    this.forceRemoveByWorkflowId = function (workflowId) {
        execBeforeCallback(FORCE_REMOVE_CODE, workflowId);

        confirm({
            text: "确定要强制移除吗？",
            closeOnConfirm: false,
            confirmCallback: function () {
                ajaxJsonAsync(param.controllerUrl + "/ForceRemoveByWorkflowId" + "?workflowId=" + workflowId, REMOVE_REQUEST_TYPE, null, function (returnInfo, data) {
                    showToastr({
                        text: returnInfo.msg,
                        type: "success"
                    });

                    execSuccessCallback(FORCE_REMOVE_CODE, data);
                });
            }
        });
    }

    /**
     * 根据工作流ID撤消
     * 
     * @param {any} workflowId 工作流ID
     */
    this.undoByWorkflowId = function (workflowId) {
        execBeforeCallback(UNDO_CODE, workflowId);

        confirm({
            text: "确定要撤消吗？",
            closeOnConfirm: false,
            confirmCallback: function () {
                ajaxJsonAsync(param.controllerUrl + "/UndoByWorkflowId" + "?workflowId=" + workflowId, REMOVE_REQUEST_TYPE, null, function (returnInfo, data) {
                    showToastr({
                        text: returnInfo.msg,
                        type: "success"
                    });

                    execSuccessCallback(UNDO_CODE, data);
                });
            }
        });
    }

    /**
     * 执行成功回调
     * 
     * @param {any} actionType 动作类型
     * @param {any} data 数据
     */
    function execSuccessCallback(actionType, data) {
        if (typeof (param.callbackSuccess) == "function") {
            param.callbackSuccess(actionType, data);
        }
    }

    /**
     * 执行前回调
     * 
     * @param {any} actionType 动作类型
     * @param {any} data 数据
     */
    function execBeforeCallback(actionType, data) {
        if (typeof (param.beforeCallback) == "function") {
            param.beforeCallback(actionType, data);
        }
    }

    /**
     * 提交
     * 
     * @param {any} formData 表单数据
     * @param {any} submitUrl 提交URL
     * @param {any} actionType 动作类型
     */
    function submit(formData, submitUrl, actionType) {
        if (formData == undefined || formData == null) {
            var form = new Form(param.formId);
            formData = form.getJsonObj();
        }
        var workFlow = {};
        workFlow.formData = formData;
        if (formData.workflowId) {
            workFlow.id = formData.workflowId;
        }
        if (formData.applyNo) {
            workFlow.applyNo = formData.applyNo;
        }
        if (formData.title) {
            workFlow.title = formData.title;
        }
        if (formData.idea) {
            workFlow.idea = formData.idea;
        }

        execBeforeCallback(actionType, workFlow);

        ajaxJsonAsync(submitUrl, ADD_REQUEST_TYPE, workFlow, function (returnInfo, data) {
            showToastr({
                text: returnInfo.msg,
                type: "success"
            });

            execSuccessCallback(actionType, data);
        });
    }
}