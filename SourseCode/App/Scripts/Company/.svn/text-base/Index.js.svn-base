$(function () {
    $('#CompanyData').datagrid({
        title: '公司列表', //列表的标题
        iconCls: 'icon-site',
        width: '98%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        url: '../Company/GetAllData', //获取数据的url
        idField: 'CompanyID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        columns: [[
             { field: 'CompanyID', title: '客户编号', width: 100 }
            , { field: 'CompanyName', title: '客户名称', width: 390 }
            , {
                field: 'CreatedTime', title: '创建日期', width: 125, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
        ]],
        onDblClickRow: function () {
            flexiModify();
        },
        pagination: true,
        rownumbers: true

    });
    //添加选择按钮
    $('#CompanyData').datagrid("addToolbarItem", [{ "text": "选择", "iconCls": "icon-ok", handler: function () { flexiSelect(); } }]);
    //var parent = window.dialogArguments; //获取父页面
    //if (parent == "undefined" || parent == null) {
    //    //首先获取iframe标签的id值

    //    var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");

    //    //然后关闭AJAX相应的缓存
    //    $.ajaxSetup({
    //        cache: false
    //    });

    //    //获取按钮值
    //    $.getJSON("../Home/GetToolbar", { id: iframeid }, function (data) {
    //        if (data == null) {
    //            return;
    //        }
    //        $('#CompanyData').datagrid("addToolbarItem", data);

    //    });
    //} else {

    //    //添加选择按钮
    //    $('#CompanyData').datagrid("addToolbarItem", [{ "text": "选择", "iconCls": "icon-ok", handler: function () { flexiSelect(); } }]);
    //}
});

//清空
function RemvoeAll() {
    $('#Keyword').val('');
}


//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    if ($('#Keyword').val() != "") {
        search = search + 'Keyword' + "&" + $('#Keyword').val() + '^';
    }
    //执行查询
    $('#CompanyData').datagrid('load', { search: search });
}

//“选择”按钮，在其他（与此页面有关联）的页面中，此页面以弹出框的形式出现，选择页面中的数据
function flexiSelect() {

    var rows = $('#CompanyData').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    if (rows.length == 1) {
        $("#CompanyName").val(rows[0].CompanyName);
        $("#CompanyID").val(rows[0].CompanyID);
        $(".easyui-window").window('close');
    }
    else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
        return false;
    }
}