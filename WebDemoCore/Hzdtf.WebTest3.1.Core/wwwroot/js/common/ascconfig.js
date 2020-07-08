/**
 * 数字1范围
 * */
var ASCII_NUM_CODE_RANGE1 = [96, 105];

/**
 * 数字2范围
 * */
var ASCII_NUM_CODE_RANGE2 = [48, 57];

/**
 * 小数点
 * */
var ASCII_POINT_CODE = 110;

/**
 * 回退键
 * */
var BACK_CODE = 8;

/**
 * 删除键
 * */
var DELETE_CODE = 46;

/**
 * 方向左键 
 * */
var LEFT_CODE = 37;

/**
 * 方向右键 
 * */
var RIGTH_CODE = 39;

/**
 * 判断是否是数字ASC编码
 * 
 * @param {any} ascCode ASC编码
 */
function isNumAsc(ascCode) {
    if (isIntegerAsc(ascCode) || ascCode == ASCII_POINT_CODE) {
        return true;
    }

    return false;
}

/**
 * 判断是否整数ASC编码
 * 
 * @param {any} ascCode ASC编码
 */
function isIntegerAsc(ascCode) {
    if ((ascCode >= ASCII_NUM_CODE_RANGE1[0] && ascCode <= ASCII_NUM_CODE_RANGE1[1])
        || (ascCode >= ASCII_NUM_CODE_RANGE2[0] && ascCode <= ASCII_NUM_CODE_RANGE2[1])) {
        return true;
    }

    return false;
}

/**
 * 判断键盘是否输入数字
 * 
 * @param {any} ascCode 编码
 */
function isKeyInNum(ascCode) {
    if (isNumAsc(ascCode)
        || (ascCode == BACK_CODE || ascCode == DELETE_CODE || ascCode == LEFT_CODE || ascCode == RIGTH_CODE)) {
        return true;
    }

    return false;
}

/**
 * 判断键盘是否输入金额
 * 
 * @param {any} ascCode 编码
 * @param existsValue 已经存在的值
 */
function isKeyInAmount(ascCode, existsValue) {
    if (isKeyInNum(ascCode)) {
        // 如果已经存在小数点，再输入小数点不行
        if (existsValue && existsValue.indexOf(".") != -1 && ascCode == ASCII_POINT_CODE) {
            return false;
        }
        return true;
    }

    return false;
}