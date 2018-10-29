$(function () {

    $('#flexigridData').datagrid({
        title: '项目评估列表', //列表的标题
        iconCls: 'icon-site',
        width: '100%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        url: '../CustomerProject/GetSelfProjectData', //获取数据的url
        idField: 'CustomerProjectID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 100 },
             {
                 field: 'CustomerName', title: '客户名称', width: 180, formatter: function (value, rowData) {
                     return '<a href=\'javascript:window.parent.addTabUpdate("' + rowData.CustomerName + '", "/Customer/SelfDetails?ID=' + rowData.CustomerID + '", "tu1425", true);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
                 }
             },
           {
               field: 'ProjectName', title: '职位名称', width: 210, formatter: function (value, rowData) {

                   return '<a href=\'javascript:showWindowTop("项目评估", "/CustomerProject/View?id=' + rowData.CustomerProjectID + '", 650, 600);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
               }
           },
            { field: 'ProjectBudget', title: '薪资范围', width: 120 },
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
            { field: 'CreatedByName', title: '创建人', width: 90 },
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
