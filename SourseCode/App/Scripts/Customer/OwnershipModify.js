
$(function () {


    ///*******************************************归属人设置列表**************************************///
    $('#OwnershipData').datagrid({
        title: '选择的客户', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        idField: 'CustomerID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        columns: [[
             { field: 'SysPersonName', title: '归属人/跟进人', width: 90 }
            , { field: 'CustomerName', title: '客户名称', width: 180 }
            , { field: 'CityName', title: '所在城市', width: 120 }
            , { field: 'CustomerLevel', title: '客户级别', width: 120 }
            //, { field: 'CustomerFunnel', title: '客户进程', width: 90 }
            //, { field: 'Opportunities', title: '商机判断', width: 77 }
            //, { field: 'ContactPerson', title: '联系人', width: 77 }
            //, { field: 'ContactPhone', title: '联系电话', width: 120 }
            //, { field: 'Contact', title: '联系方式', width: 77 }
            //, { field: 'IsRegister', title: '注册', width: 77 }
            //, { field: 'IsCertification', title: '认证', width: 77 }
            //, { field: 'SocialRecruitingQty', title: '社招职位', width: 77 }
            //, { field: 'SchoolRecruitingQty', title: '校招职位', width: 77 }
            //, { field: 'RelieveDate', title: '解除日期', width: 77 }

        ]],
        pagination: false,//分页工具
        rownumbers: true

    });

    $('#OwnershipPersonData').datagrid({
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

            $("#OwnershipSysPersonName").val(rowData.MyName);
            $("#OwnershipSysPersonID").val(rowData.Id);
            $("#OwnershipDeptName").val(rowData.SysDepartmentId);


        }

    });
});


///*******************************************查询归属人**************************************///
function getOwnershipPerson() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    search = search + "MyName&" + $("#OwnershipMyName").val() + "^";
    $('#OwnershipPersonData').datagrid('options').url = "../SysPerson/GetData";
    $("#OwnershipPersonData").datagrid("clearSelections");
    //执行查询
    $('#OwnershipPersonData').datagrid('load', { search: search, Data: "All" });
}



//////*******************************************修改归属************************************************/
function changeOwnership() {
    $('#OwnershipData').datagrid('loadData', { total: 0, rows: [] });//清空下方DateGrid 
    $("#OwnershipCustomerIDs").val('');

    var rows = $('#flexigridData').datagrid('getSelections');
    var ids = "";
    if (rows.length > 0) {
        for (var m = 0; m <= rows.length - 1; m++) {
            ids += rows[m].CustomerID + ",";
            $('#OwnershipData').datagrid("appendRow", {
                CustomerID: rows[m].CustomerID,
                SysPersonName: rows[m].SysPersonName,
                CustomerName: rows[m].CustomerName,
                CityName: rows[m].CityName,
                CustomerLevel: rows[m].CustomerLevel,
                CustomerFunnel: rows[m].CustomerFunnel,
                Opportunities: rows[m].Opportunities,
                ContactPerson: rows[m].ContactPerson,
                ContactPhone: rows[m].ContactPhone,
                Contact: rows[m].Contact,
                IsRegister: rows[m].IsRegister,
                IsCertification: rows[m].IsCertification
            });
            $("#OwnershipCustomerIDs").val(ids);
        }

        $('#Ownership').dialog({
            title: '客户归属设置',
            width: 780,
            height: 610,
            top: 30,
            closed: false,
            cache: false,
            resizable: true,
            modal: true
        });


    } else {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
    }
    return false;

}

//保存客户归属人
function SaveOwnership() {
    var ids = $("#OwnershipCustomerIDs").val();
    var SysPersonID = $('#Ownership').find("#OwnershipSysPersonID").val();
    if (SysPersonID == "") {
        $.messager.alert('提示', '请选择归属人！', "info");

    }
    else if (ids == "" || ids == undefined || ids == null) {
        $.messager.alert('提示', '请选择要修改归属的客户！', "info");
    } else {
        $.messager.confirm('提示', '确定要修改客户归属吗?', function (r) {
            if (!r) {
                return;
            }
            var parm = "SysPersonID=" + SysPersonID + "&&CustomerIDs=" + ids;
            $.ajax({
                type: "POST",
                url: "../CustomerAttributionChange/SetOwnership/",
                data: parm,
                success: function (msg) {

                    if (msg == "OK") {

                        //刷新整个表格
                        $('#flexigridData').datagrid('unselectAll');
                        $('#flexigridData').datagrid('reload');
                        $.messager.alert('提示', '设置成功！', "info", function () {
                            $(".easyui-window").window("close");
                            $(".easyui-dialog").dialog("close");
                        });
                    }
                    else if (msg == "") {
                        $.messager.alert('操作提示', '保存失败!请联系管理员。', 'info');
                    }
                    else {
                        $.messager.alert('错误', msg, "error");
                    }
                }

            });
        });

    }

}
