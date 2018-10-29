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
            , { field: 'PostName', title: '职务', width: 90 }
            , { field: 'CompanyTel', title: '固定电话', width: 90 }
            , { field: 'PhoneNumber1', title: '手机', width: 90 }
            , { field: 'Email1', title: '邮箱', width: 90 }
            , { field: 'QQID', title: 'QQ号码', width: 77 }

        ]],
        pagination: true,
        rownumbers: true,
        onDblClickRow: function () {
            getView();
        }

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
 