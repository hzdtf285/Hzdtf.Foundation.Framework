var basicManageExpand;

$(function () {
    var controllUrl = "/api/DataDictionaryItem";

    basicManageExpand = new BasicManage({
        pageDataUrl: controllUrl + "/PageData",
        listUrl: controllUrl + "/PageExpandList",
        caption: "扩展属性列表",
        tableId: "table_list_expand",
        pagerId: "pager_list_expand",
        columnModels: [
            { name: "id", sortable: false, key: true, hidden: true },
            { name: "name", sortable: true, label: "名称", width: 300 }, 
            { name: "text", sortable: true, label: "文本", width: 300}
        ],
        lazyLoadList: true,
    });

    basicManageExpand.init(); 
});