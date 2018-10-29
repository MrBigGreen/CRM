$(function () {

    $('#flexigridData').datagrid({
        title: '项目评估列表', //列表的标题
        iconCls: 'icon-site',
        width: '100%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        url: '../CustomerProject/GetProjectData', //获取数据的url
        idField: 'CustomerProjectID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        queryParams: { "id": $("#CustomerID").val() },
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 100 },
            { field: 'ProjectName', title: '职位名称', width: 210 },
            { field: 'ProjectBudget', title: '薪资范围', width: 210 },
            { field: 'EvaluationDesc', title: '评估说明', width: 230 },
             { field: 'EvaluationResultText', title: '评估结果', width: 90 },
            { field: 'EvaluationPersonName', title: '评估人', width: 90 },
            {
                field: 'EvaluationTime', title: '评估时间', width: 125, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            },
             {
                 field: 'CreatedTime', title: '创建时间', width: 125, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
        ]],
        pagination: true,
        rownumbers: true,
        onDblClickRow: function () {
            getView();
        },

    });

    //var iframeid = "1605171743285078969765ef21869";

    ////然后关闭AJAX相应的缓存
    //$.ajaxSetup({
    //    cache: false
    //});

    ////获取按钮值
    //$.getJSON("../Home/GetToolbar", { id: iframeid }, function (data) {
    //    if (data == null) {
    //        return;
    //    }
    //    $('#flexigridData').datagrid("addToolbarItem", data);
    //});
    $('#flexigridData').datagrid("addToolbarItem", [{ "text": "创建", "iconCls": "icon-add", "handler": "flexiCreate" }, { "text": "修改", "iconCls": "icon-edit", "handler": "flexiModify" }]);
   

     
});


//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    var startDate = $("#StartDate").datetimebox('getValue');
    var EndDate = $("#EndDate").datetimebox('getValue');

    if (startDate != "") {
        search = search + 'StartDate' + "&" + startDate + '^';
    }
    if (EndDate != "") {
        search = search + 'EndDate' + "&" + EndDate + '^';
    }

    //执行查询
    $('#flexigridData').datagrid('load', { search: search, id: $("#CustomerID").val() });
}


//清空
function RemvoeAll() {
    $('#StartDate').datetimebox('setValue', '');
    $('#EndDate').datetimebox('setValue', '');
}
//“选择”按钮，在其他（与此页面有关联）的页面中，此页面以弹出框的形式出现，选择页面中的数据
function flexiSelect() {

    var rows = $('#flexigridData').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }

    var arr = [];
    for (var i = 0; i < rows.length; i++) {
        arr.push(rows[i].CustomerAttributionChangeID);
    }
    //主键列和显示列之间用 ^ 分割   每一项用 , 分割
    if (arr.length > 0) {//一条数据和多于一条
        returnParent(arr.join("&")); //每一项用 & 分割
    }
}
//导航到查看详细的按钮
function getView() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {

        window.location.href = "/CustomerShare/Details?ID=" + arr[0].CustomerID;

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;
}

//导航到创建的按钮
function flexiCreate() {
    showWindowTop("新增项目", "/CustomerProject/Create?id=" + $("#CustomerID").val(), 750, 600);
    return false;
}
//导航到修改的按钮
function flexiModify() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {
        var ID = arr[0].CustomerProjectID;

        showWindowTop("修改项目", "/CustomerProject/Edit?id=" + ID, 750, 600);
    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;

};
//删除的按钮
function flexiDelete() {

    var arr = $('#flexigridData').datagrid('getSelections');
    if (arr.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    if (arr.length == 1) {
        var ID = arr[0].CustomerShareID;
        $.messager.confirm('操作提示', "确认删除：“ " + arr[0].SysPersonName + " ”吗？", function (r) {
            if (r) {
                $.post("../CustomerShare/CancelShare", { ID: ID }, function (res) {
                    if (res == "OK") {
                        //移除删除的数据
                        $.messager.alert('操作提示', '删除成功!', 'info');
                        $("#flexigridData").datagrid("reload");
                        $("#flexigridData").datagrid("clearSelections");
                    }
                    else {
                        if (res == "") {
                            $.messager.alert('操作提示', '删除失败!请查看该数据与其他模块下的信息的关联，或联系管理员。', 'info');
                        }
                        else {
                            $.messager.alert('操作提示', res, 'info');
                        }
                    }
                });
            }
        });

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
};




