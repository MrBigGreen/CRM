
$(function () {
    var frameId = window.frameElement && window.frameElement.id || '';
    if (frameId == "iframeWindowTop" || frameId == "iframeWindow") {
        //添加选择按钮
        $('#customerDatagrid').datagrid("addToolbarItem", [{ "text": "选择", "iconCls": "icon-ok", handler: function () { flexiSelect(); } }]);
    }
    else {
        //如果列表页出现在弹出框中，则只显示查询和选择按钮
        var parent = window.dialogArguments; //获取父页面
        if (parent == "undefined" || parent == null) {
            //首先获取iframe标签的id值

            var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");

            //然后关闭AJAX相应的缓存
            $.ajaxSetup({
                cache: false
            });
            if (iframeid != "") {
                //获取按钮值
                $.getJSON("../Home/GetToolbar", { id: iframeid }, function (data) {
                    if (data == null) {
                        return;
                    }
                    $('#customerDatagrid').datagrid("addToolbarItem", data);
                });
            }
        }
    }
});

//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";

    var CustomerName = $("#CustomerName").val();
    if (CustomerName != "") {
        search = search + 'CustomerName' + "&" + CustomerName + '^';
    }

    $('#customerDatagrid').datagrid('options').url = "../Customer/GetServiceList";
    $("#customerDatagrid").datagrid("clearSelections");
    //执行查询
    $('#customerDatagrid').datagrid('load', { search: search });
}




//“选择”按钮，在其他（与此页面有关联）的页面中，此页面以弹出框的形式出现，选择页面中的数据
function flexiSelect() {

    var rows = $('#customerDatagrid').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    debugger;
    var id = $('#CustomerID', window.parent.document);
    if (id != undefined) {
        id.val(rows[0].CustomerID);
    }
    var name = $('#CustomerName', window.parent.document);
    if (name != undefined) {
        name.val(rows[0].CustomerName);
    }
    window.parent.$('#myWindow').window('close');
}

