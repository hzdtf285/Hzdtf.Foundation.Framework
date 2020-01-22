/**
 * 选项面板
 * */
function TabPanel(param) {
    var currTabOption;
    $("#" + param.tabId + " a[tabOptionTitle]").click(function () {
        var tab = $(this).attr("href").replace("#", "");
        if (currTabOption == tab) {
            return;
        }

        currTabOption = tab;
        if (typeof (param.change) == "function") {
            param.change(tab);
        }
    });
}