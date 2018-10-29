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
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 100 },
             {
                 field: 'CustomerName', title: '客户名称', width: 180,   formatter: function (value, rowData) {
                     return '<a href=\'javascript:window.parent.addTabUpdate("' + rowData.CustomerName + '", "/Customer/SelfDetails?ID=' + rowData.CustomerID + '", "tu1425", true);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
                 }
             },
            {
                field: 'ProjectName', title: '职位名称', width: 210, formatter: function (value, rowData) {
                    
                    return '<a href=\'javascript:showWindowTop("项目评估", "/CustomerProject/View?id=' + rowData.CustomerProjectID + '", 750, 600);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
                }
            },
            { field: 'ProjectBudget', title: '薪资范围', width: 130 },
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

    $('#AllocationData').datagrid({
        title: '', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        idField: 'CustomerProjectID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        columns: [[
            { field: 'ProjectName', title: '职位名称', width: 210 },
            { field: 'ProjectDesc', title: '职位描述', width: 130 },
            { field: 'EvaluationDesc', title: '评估说明', width: 130 },
            { field: 'EvaluationResultText', title: '评估结果', width: 90 },
             {
                 field: 'CreatedTime', title: '创建时间', width: 125, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }

        ]],
        pagination: false,//分页工具
        rownumbers: true

    });

    $('#ExecuteData').datagrid({
        title: '', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        idField: 'CustomerProjectID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        columns: [[
            { field: 'ProjectName', title: '职位名称', width: 210 },
            { field: 'ProjectDesc', title: '职位描述', width: 130 },
            { field: 'EvaluationDesc', title: '评估说明', width: 130 },
            { field: 'EvaluationResultText', title: '评估结果', width: 90 },
             {
                 field: 'CreatedTime', title: '创建时间', width: 125, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }

        ]],
        pagination: false,//分页工具
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
    }


    $('#AllocationPersonData').datagrid({
        title: '', //列表的标题
        iconCls: 'icon-site',
        striped: true,
        collapsible: true,
        url: '../SysPerson/GetData', //获取数据的url
        sortName: 'Id',
        sortOrder: 'desc',
        idField: 'Id',
        fitColumns: true,
        queryParams: { "Data": 'All' },
        pagination: true,
        rownumbers: true,
        onDblClickRow: function (rowIndex, rowData, value) {

            $("#AllocationPersonName").val(rowData.MyName);
            $("#AllocationPersonID").val(rowData.Id);
            $("#AllocationDeptName").val(rowData.SysDepartmentId);


        }

    });


    $('#ExecutePersonData').datagrid({
        title: '', //列表的标题
        iconCls: 'icon-site',
        striped: true,
        collapsible: true,
        url: '../SysPerson/GetData', //获取数据的url
        sortName: 'Id',
        sortOrder: 'desc',
        idField: 'Id',
        fitColumns: true,
        queryParams: { "Data": 'All' },
        pagination: true,
        rownumbers: true,
        onDblClickRow: function (rowIndex, rowData, value) {

            $("#ExecutePersonName").val(rowData.MyName);
            $("#ExecutePersonID").val(rowData.Id);
            $("#ExecuteDeptName").val(rowData.SysDepartmentId);


        }

    });
});
 
 

 

///*******************************************分配客户列表**************************************///
function getPerson() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    search = search + "MyName&" + $("#AllocationMyName").val() + "^";
    $('#AllocationPersonData').datagrid('options').url = "../SysPerson/GetData";
    $("#AllocationPersonData").datagrid("clearSelections");
    //执行查询
    $('#AllocationPersonData').datagrid('load', { search: search, Data: "All" });
}




///*******************************************项目执行人员筛选**************************************///
function getExecutePerson() {

    //将查询条件按照分隔符拼接成字符串 
    var search = "";
    search = search + "MyName&" + $("#ExecuteMyName").val() + "^";
    $('#ExecutePersonData').datagrid('options').url = "../SysPerson/GetData";
    $("#ExecutePersonData").datagrid("clearSelections");
    //执行查询
    $('#ExecutePersonData').datagrid('load', { search: search, Data: "All" });
}



//////*******************************************项目分配************************************************/
function flexiAllocation() {
    $('#AllocationData').datagrid('loadData', { total: 0, rows: [] });//清空下方DateGrid 
    $("#AllocationProjectID").val('');
    $("#AllocationPersonName").val('');
    $("#AllocationDeptName").val('');
    $("#AllocationPersonID").val('');

    var rows = $('#flexigridData').datagrid('getSelections');
    var ids = "";
    if (rows.length > 0) {
        var ids = "";
        var flag = true;
        for (var i = 0; i < rows.length; i++) {
            if (rows[i].EvaluationResult == "1605111431371348259613423d3c6") {
                ids += rows[i].CustomerProjectID + ",";
                $('#AllocationData').datagrid("appendRow", {
                    ProjectName: rows[i].ProjectName,
                    ProjectDesc: rows[i].ProjectDesc,
                    EvaluationDesc: rows[i].EvaluationDesc,
                    EvaluationResultText: rows[i].EvaluationResultText,
                    CreatedTime: rows[i].CreatedTime,
                });
            }
            else {
                flag = false;
            }
        }
        if (!flag) {
            $.messager.alert('操作提示', '项目已经评估过的不能再重新分配!', 'warning');
        }
        if (ids == "") {
            return false;
        }
        $("#AllocationProjectID").val(ids);


        $('#AllocationDialog').dialog({
            title: '项目分配',
            width: 780,
            height: 610,
            top: 30,
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



//////*******************************************保存项目分配************************************************/
function SaveAllocation() {

    //
    //1 获取要分配的人员
    var personID = $("#AllocationPersonName").val();

    if (personID == "" || personID == undefined || personID == null) {
        $.messager.alert('提示', '请选择分配人！', "info");
        return false;
    }
    personID = $("#AllocationPersonID").val();
    //
    var CustomerProjectID = $("#AllocationProjectID").val();
    if (CustomerProjectID == "" || CustomerProjectID == undefined || CustomerProjectID == null) {
        $.messager.alert('提示', '请选择要分配的项目！', "info");
        return false;
    }
    var AllocationRemark = $("#AllocationRemark").val();
    $.messager.confirm('提示', '确定要分配项目吗?', function (r) {
        if (!r) {
            return;
        }
        var parm = "PersonID=" + personID + "&ProjectID=" + CustomerProjectID;
        $.ajax({
            type: "POST",
            url: "../CustomerProject/Allocation/",
            data: parm,
            success: function (msg) {

                if (msg == "OK") {

                    //刷新整个表格
                    $('#flexigridData').datagrid('unselectAll');
                    $('#flexigridData').datagrid('reload');
                    $.messager.alert('提示', '分配成功！', "info", function () {
                        $(".easyui-dialog").window("close");
                    });
                }
                else if (msg == "") {
                    $.messager.alert('操作提示', '分配失败!请联系管理员。', 'info');
                }
                else {
                    $.messager.alert('错误', msg, "error");
                }
            }

        });
    });



}





//////*******************************************项目执行************************************************/
function flexiExecute() {
    $('#ExecuteData').datagrid('loadData', { total: 0, rows: [] });//清空下方DateGrid 
    $("#ExecuteProjectID").val('');
    $("#ExecutePersonName").val('');
    $("#ExecuteDeptName").val('');
    $("#ExecutePersonID").val('');

    var rows = $('#flexigridData').datagrid('getSelections');
    var ids = "";
    if (rows.length > 0) {
        var ids = "";
        var flag = true;
        for (var i = 0; i < rows.length; i++) {
            if (rows[i].EvaluationResult == "1605111432278446688fe51d4a449" || row[i].EvaluationResult == "160803152105866457578eb28881e") {
                ids += rows[i].CustomerProjectID + ",";
                $('#ExecuteData').datagrid("appendRow", {
                    ProjectName: rows[i].ProjectName,
                    ProjectDesc: rows[i].ProjectDesc,
                    EvaluationDesc: rows[i].EvaluationDesc,
                    EvaluationResultText: rows[i].EvaluationResultText,
                    CreatedTime: rows[i].CreatedTime,
                });
            }
            else {
                flag = false;
            }
        }
        if (!flag) {
            $.messager.alert('操作提示', '项目必须是可执行才能操作!', 'warning');
        }
        if (ids == "") {
            return false;
        }
        $("#ExecuteProjectID").val(ids);


        $('#ExecuteDialog').dialog({
            title: '项目执行',
            width: 780,
            height: 610,
            top: 30,
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



//////*******************************************保存项目执行************************************************/
function SaveExecute() {

    //
    //1 获取要执行的人员
    var personID = $("#ExecutePersonName").val();

    if (personID == "" || personID == undefined || personID == null) {
        $.messager.alert('提示', '请选择执行人！', "info");
        return false;
    }
    personID = $("#ExecutePersonID").val();
    //
    var CustomerProjectID = $("#ExecuteProjectID").val();
    if (CustomerProjectID == "" || CustomerProjectID == undefined || CustomerProjectID == null) {
        $.messager.alert('提示', '请选择要执行的项目！', "info");
        return false;
    }
    var ExecuteRemark = $("#ExecuteRemark").val();
    $.messager.confirm('提示', '确定要执行项目吗?', function (r) {
        if (!r) {
            return;
        }
        var parm = "PersonID=" + personID + "&ProjectID=" + CustomerProjectID;
        $.ajax({
            type: "POST",
            url: "../CustomerProject/GetExecute/",
            data: parm,
            success: function (msg) {

                if (msg == "OK") {

                    //刷新整个表格
                    $('#flexigridData').datagrid('unselectAll');
                    $('#flexigridData').datagrid('reload');
                    $.messager.alert('提示', '操作成功！', "info", function () {
                        $(".easyui-dialog").window("close");
                    });
                }
                else if (msg == "") {
                    $.messager.alert('操作提示', '操作失败!请联系管理员。', 'info');
                }
                else {
                    $.messager.alert('错误', msg, "error");
                }
            }

        });
    });



}