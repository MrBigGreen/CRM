$(function () {
    $('#StartDate').datetimebox({
        showSeconds: false,
    });
    $('#EndDate').datetimebox({
        showSeconds: false,
    });

    $('#flexigridData').datagrid({
        title: '客户跟进反馈', //列表的标题
        iconCls: 'icon-site',
        width: '99%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        url: '../CustomerFollow/GetFollowData', //获取数据的url
        idField: 'CustomerFollowID',
        sortName: 'FollowUpDate',
        sortOrder: 'desc',
        queryParams: { "id": $("#CustomerID").val() },
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 80 },
             {
                 field: 'FollowUpDate', title: '跟进日期', width: 125, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
            , { field: 'SysPersonName', title: '跟进人', width: 90 }
            , { field: 'FollowUpMode', title: '跟进方式', width: 90 }
            , {
                field: 'ContactState', title: '联系状态', width: 90, formatter: function (value, rec) {
                    
                    if (value == 0) {
                        return "未联系";
                    }
                    else if (value == 1) {
                        return "已联系上";
                    }
                    else if (value == 2) {
                        return "未联系上";
                    }
                    else {
                        return "";
                    }

                }
            }
            , { field: 'ContactName', title: '联系人', width: 90 }
            , { field: 'OtherLevel', title: '对方级别', width: 90 }
            , { field: 'CustomerFeedback', title: '客户反馈', width: 77 }
            , { field: 'Remark', title: '客户需求描述', width: 77 }
            , { field: 'CustomerFunnel', title: '客户进程', width: 77 }
            , { field: 'ProcessMode', title: '处理方式', width: 77 }
            , { field: 'Opportunities', title: '商机判断', width: 77 }
            , { field: 'ContactPerson', title: '联系人', width: 77 }
            , { field: 'ContactPhone', title: '联系电话', width: 120 }
            , {
                field: 'NextFollowDate', title: '下次跟进日期', width: 77, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
            , { field: 'NextFollowUpMode', title: '下次跟进方式', width: 77 }

        ]],
        onDblClickRow: function () {
            flexiModify();
        },
        pagination: true,
        rownumbers: true

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
    } else {

        //添加选择按钮
        $('#flexigridData').datagrid("addToolbarItem", [{ "text": "选择", "iconCls": "icon-ok", handler: function () { flexiSelect(); } }]);
    }
});


//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    var startDate = $("#StartDate").datetimebox('getValue');
    var EndDate = $("#EndDate").datetimebox('getValue');

    if (startDate != "") {
        search = search + 'StartDate' + "&" + startDate + ' 00:00:00^';
    }
    if (EndDate != "") {
        search = search + 'EndDate' + "&" + EndDate + ' 23:59:59^';
    }

    var str = "";
    //联系状态
    str = $("input:radio[name='ContactState']:checked").val();
    if (str != null || str != "") {
        search = search + 'ContactState' + "&" + str + '^';
    }

    str = $("#FollowUpMode").val();
    //跟进方式 
    if (str != "") {
        search = search + 'FollowUpMode' + "&" + str + '^';
    }

    //执行查询
    $('#flexigridData').datagrid('load', { id: $("#CustomerID").val(), search: search });
}


//清空
function RemvoeAll() {
    $('#StartDate').datetimebox('setValue', '');
    $('#EndDate').datetimebox('setValue', '');
    $("input[type=radio][name=ContactState][value='']").attr("checked", true);
    $("#FollowUpMode").val('');

}
//“选择”按钮，在其他（与此页面有关联）的页面中，此页面以弹出框的形式出现，选择页面中的数据
function flexiSelect() {


}
//导航到查看详细的按钮
function getView() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {

        window.location.href = "/Customer/Details?ID=" + arr[0].CustomerID;

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;
}

//导航到创建的按钮
function flexiCreate() {
    window.location.href = "../Customer/Create";
    return false;
}
//导航到修改的按钮
function flexiModify() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {
        var ID = arr[0].CustomerID;
        //window.location.href = "../Customer/Edit/" + ID;
        window.location.href = "/Customer/Details?ID=" + arr[0].CustomerID;
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
        var ID = arr[0].CustomerID;
        $.messager.confirm('操作提示', "确认删除：“ " + arr[0].CustomerName + " ”吗？", function (r) {
            if (r) {
                $.post("../Resume/Delete", { query: arr.join(",") }, function (res) {
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

 