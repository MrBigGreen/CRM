$(function () {


    $('#TheMonth').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        url: '/RPOPerf/GetMonth',
    });


    $('#flexigridData').datagrid({
        title: '开票列表', //列表的标题
        iconCls: 'icon-site',
        width: '100%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        url: '../RPOPerf/GetBillingData', //获取数据的url
        idField: 'BillingID',
        sortName: 'TheMonth',
        sortOrder: 'desc',
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 100 },
            { field: 'TheMonth', title: '月份', width: 110 },
            {
                field: 'TheWeek', title: '周', width: 110, formatter: function (value) {
                    if (value == "1") {
                        return "第一周";
                    }
                    else if (value == "2") {
                        return "第二周";
                    }
                    else if (value == "3") {
                        return "第三周";
                    }
                    else if (value == "4") {
                        return "第四周";
                    }
                    else if (value == "5") {
                        return "第五周";
                    }

                }
            },
            { field: 'BillingAmountSJ', title: '本周实际', width: 120 },
            { field: 'BillingAmountYJ', title: '下周预计', width: 120 },
             
        ]],
        pagination: true,
        rownumbers: true,

    });
    var parent = window.dialogArguments; //获取父页面
    if (parent == "undefined" || parent == null) {
        //首先获取iframe标签的id值

        var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");

        //然后关闭AJAX相应的缓存
        $.ajaxSetup({
            cache: false
        });

        //获取按钮值
        $.getJSON("../Home/GetToolbar", { id: iframeid }, function (data) {
            if (data == null) {
                return;
            }
            $('#flexigridData').datagrid("addToolbarItem", data);
        });
    }


});






//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    var TheMonth = $("#TheMonth").datetimebox('getValue');

    if (TheMonth != "") {
        search = search + 'TheMonthDDL_String' + "&" + TheMonth + '^';
    }
    $('#flexigridData').datagrid('options').url = "../RPOPerf/GetBillingData";
    $("#flexigridData").datagrid("clearSelections");
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}


//清空
function RemvoeAll() {

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

//导航到创建的按钮
function flexiCreate() {
    showWindowTop("新增开票", "/RPOPerf/BillingCreate", 500, 600);
    return false;
}


//导航到查看详细的按钮
function getView() {
    var arr = $('#flexigridData').datagrid('getSelections');
    if (arr.length == 1) {
        showWindowTop("查看开票信息", "/RPOPerf/BillingView?id=" + arr[0].BillingID, 500, 600);
    }
    else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;
}

//导航到修改的按钮
function flexiModify() {
    var arr = $('#flexigridData').datagrid('getSelections');
    if (arr.length == 1) {
        showWindowTop("编辑开票信息", "/RPOPerf/BillingEdit?id=" + arr[0].BillingID, 500, 600);
    }
    else {
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

            }
        });

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
};




