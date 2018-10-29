$(function () {
    $('#flexigridData').datagrid({
        title: '客户联系人', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        url: '/CustomerContact/GetData', //获取数据的url
        idField: 'CustomerContactID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        queryParams: { "id": $("#CustomerID").val() },
        columns: [[
            // { field: 'ck', title: '全选', checkbox: true, width: 80 }
              { field: 'ContactName', title: '联系人', width: 90 }
            , { field: 'CompanyTel2', title: '前台电话', width: 90 }
            , { field: 'BirthDate', title: '生日', width: 90 }
            , { field: 'Interested', title: '感兴趣', width: 90 }
            , { field: 'Department', title: '部门', width: 90 }
            , { field: 'Post', title: '职务', width: 90 }
            , { field: 'CompanyTel', title: '固定电话', width: 90 }
            , { field: 'PhoneNumber1', title: '手机', width: 90 }
            , { field: 'Email1', title: '邮箱', width: 90 }
            , { field: 'QQID', title: 'QQ号码', width: 77 }

        ]],
        pagination: true,
        rownumbers: true,
        onDblClickRow: function () {
            getView();
        },
        toolbar: [
             {
                 text: '详细',
                 iconCls: 'icon-details',
                 handler: function () {
                     return getView();
                 }
             },
              {
                  text: '创建',
                  iconCls: 'icon-add',
                  handler: function () {
                      return flexiCreate();
                  }
              },
               {
                   text: '删除',
                   iconCls: 'icon-remove',
                   handler: function () {
                       return flexiDelete();
                   }
               },
                {
                    text: '修改',
                    iconCls: 'icon-edit',
                    handler: function () {
                        return flexiModify();
                    }
                }

        ]

    });
    var parent = window.dialogArguments; //获取父页面
    if (parent == "undefined" || parent == null) {
        //首先获取iframe标签的id值

    } else {

        //添加选择按钮
        $('#flexigridData').datagrid("addToolbarItem", [{ "text": "选择", "iconCls": "icon-ok", handler: function () { flexiSelect(); } }]);
    }
});




//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    $('#divQuery').find(":text,:selected,select,textarea,:hidden,:checked,:password").each(function () {
        if (this.id)
            search = search + this.id + "&" + this.value + "^";
        else
            search = search + this.name + "&" + this.value + "^";
    });
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
};
//“导出”按钮     在6.0版本中修改
function flexiExport() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    $('#divQuery').find(":text,:selected,select,textarea,:hidden,:checked,:password").each(function () {
        search = search + this.id + "&" + this.value + "^";
    });

    var p = $('#flexigridData').datagrid('options').columns[0];
    var field = [];//所有的列名
    var title = [];//所有的标题名称
    $(p).each(function () {
        field.push(this.field);
        title.push(this.title);
    });

    $.post("../CustomerContact/Export",
        {
            title: title.join(","),
            field: field.join(","),
            sortName: $('#flexigridData').datagrid('options').sortName,
            sortOrder: $('#flexigridData').datagrid('options').sortOrder,
            search: search
        }, function (res) {
            window.location.href = res;

        });
};


//“选择”按钮，在其他（与此页面有关联）的页面中，此页面以弹出框的形式出现，选择页面中的数据
function flexiSelect() {

    var rows = $('#flexigridData').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }

    var arr = [];
    for (var i = 0; i < rows.length; i++) {
        arr.push(rows[i].Id);
    }
    arr.push("^");
    for (var i = 0; i < rows.length; i++) {
        arr.push(rows[i].MyName);
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
        $('#ContactIframe').attr("src", "/CustomerContact/Details?Id=" + arr[0].CustomerContactID);
        $('#contactDialog').dialog({
            title: '查看联系人',
            width: 750,
            height: 500,
            closed: false,
            cache: false,
            resizable: true,
            modal: true
        });

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;
}
//导航到创建的按钮
function flexiCreate() {

    $('#ContactIframe').attr("src", "/CustomerContact/Create?Id=" + $("#CustomerID").val());
    $('#contactDialog').dialog({
        title: '创建联系人',
        width: 750,
        height: 500,
        closed: false,
        cache: false,
        resizable: true,
        modal: true
    });
    return false;
}
//导航到修改的按钮
function flexiModify() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {
        $('#ContactIframe').attr("src", "/CustomerContact/Edit?Id=" + arr[0].CustomerContactID);
        $('#contactDialog').dialog({
            title: '修改联系人',
            width: 750,
            height: 500,
            closed: false,
            cache: false,
            resizable: true,
            modal: true
        });
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
        $.messager.confirm('操作提示', "确认删除联系人“ " + arr[0].ContactName + " ”吗？", function (r) {
            if (r) {
                $.post("/CustomerContact/Delete", { ID: arr[0].CustomerContactID, ContactName: arr[0].ContactName }, function (res) {
                    if (res == "OK") {
                        //移除删除的数据
                        $.messager.alert('操作提示', '删除成功!', 'info');
                        $("#flexigridData").datagrid("reload");
                        $("#flexigridData").datagrid("clearSelections");
                    }
                    else {
                        if (res == "") {
                            $.messager.alert('操作提示', '删除失败!请查检查网络或联系管理员。', 'info');
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
    return false;

};
