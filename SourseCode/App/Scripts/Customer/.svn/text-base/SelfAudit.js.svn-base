$(function () {
    

    $('#allData').datagrid({
        title: '客户列表', //列表的标题
        iconCls: 'icon-site',
        width: '99%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: false,
        url: '../Customer/GetAuditAllData', //获取数据的url
        idField: 'CustomerID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        queryParams: { "id": "Self" },
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 80 }
            , { field: 'CustomerName', title: '客户名称', width: 140 }
            , { field: 'CreatedBy', title: '创建人', width: 100 }
            , {
                field: 'CreatedTime', title: '创建日期', width: 125, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
            , { field: 'AuditPerson', title: '审核人', width: 90 }
            , {
                field: 'AuditTime', title: '审核时间', width: 125, formatter: function (value, rec) {

                    if (value == null || value == "" || value == undefined) {
                        return "";
                    }
                    else {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
             , {
                 field: 'AuditStatus', title: '审核状态', width: 100, formatter: function (value, rowData, rowIndex) {
                     if (value == "1") {
                         return "审核通过";
                     }
                     else if (value == "2") {
                         return "待审核";                         
                     }
                     else if (value == "3") {
                         return "未通过";
                     }
                 }
             }
              , { field: 'Reason', title: '审核原因', width: 100 }

        ]],
        pagination: true,
        rownumbers: true

    });

    $('#PendingAuditData').datagrid({
        title: '待审核客户列表', //列表的标题
        iconCls: 'icon-site',
        width: '99%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: false,
        url: '../Customer/GetAuditAllData', //获取数据的url
        idField: 'CustomerID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        queryParams: { "search": "AuditStatus&2^", "id": "Self" },
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 80 }
            , { field: 'CustomerName', title: '客户名称', width: 140 }
            , { field: 'CreatedBy', title: '创建人', width: 100 }
            , {
                field: 'CreatedTime', title: '创建日期', width: 125, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
            , { field: 'AuditPerson', title: '审核人', width: 90 }
            , {
                field: 'AuditTime', title: '审核时间', width: 125, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
            , {
                field: 'AuditStatus', title: '审核状态', width: 100, formatter: function (value, rowData, rowIndex) {
                    if (value == "1") {
                        return "审核通过";
                    }
                    else if (value == "2") {
                        return "待审核";
                    }
                    else if (value == "3") {
                        return "未通过";
                    }
                }
            }

        ]],
        pagination: true,
        rownumbers: true

    });

    $('#AuditThroughData').datagrid({
        title: '审核通过客户列表', //列表的标题
        iconCls: 'icon-site',
        width: '99%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: false,
        url: '../Customer/GetAuditAllData', //获取数据的url
        idField: 'CustomerID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        queryParams: { "search": "AuditStatus&1^", "id": "Self" },
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 80 }
            , { field: 'CustomerName', title: '客户名称', width: 140 }
            , { field: 'CreatedBy', title: '创建人', width: 100 }
            , {
                field: 'CreatedTime', title: '创建日期', width: 125, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
            , { field: 'AuditPerson', title: '审核人', width: 90 }
            , {
                field: 'AuditTime', title: '审核时间', width: 125, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
            , {
                field: 'AuditStatus', title: '审核状态', width: 100, formatter: function (value, rec) {
                    if (value == "1") {
                        return "审核通过";
                    }
                    else if (value == "2") {
                        return "待审核";
                    }
                    else if (value == "3") {
                        return "未通过";
                    }
                }
            }

        ]],
        pagination: true,
        rownumbers: true

    });

    $('#NotByData').datagrid({
        title: '未通过客户列表', //列表的标题
        iconCls: 'icon-site',
        width: '99%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: false,
        url: '../Customer/GetAuditAllData', //获取数据的url
        idField: 'CustomerID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        queryParams: { "search": "AuditStatus&3^", "id": "Self" },
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 80 }
            , { field: 'CustomerName', title: '客户名称', width: 140 }
            , { field: 'CreatedBy', title: '创建人', width: 100 }
            , {
                field: 'CreatedTime', title: '创建日期', width: 125, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
            , { field: 'AuditPerson', title: '审核人', width: 90 }
            , {
                field: 'AuditTime', title: '审核时间', width: 125, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
            , {
                field: 'AuditStatus', title: '审核状态', width: 100, formatter: function (value, rec) {
                    if (value == "1") {
                        return "审核通过";
                    }
                    else if (value == "2") {
                        return "待审核";
                    }
                    else if (value == "3") {
                        return "未通过";
                    }
                }
            }
             , { field: 'Reason', title: '审核原因', width: 100 }

        ]],
        pagination: true,
        rownumbers: true

    });
});


//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    if ($('#Keyword').val() != "") {
        search = search + 'Keyword' + "&" + $('#Keyword').val() + '^';
    }

    //执行查询
    $('#allData').datagrid('load', { search: search, "id": "Self" });

    $('#PendingAuditData').datagrid('load', { search: search + "AuditStatus&2^", "id": "Self" });

    $('#AuditThroughData').datagrid('load', { search: search + "AuditStatus&1^", "id": "Self" });

    $('#NotByData').datagrid('load', { search: search + "AuditStatus&3^", "id": "Self" });
}

//全部审核列表---审核
function getAuditAllData(customerID, customerName, rowIndex) {
    $("#CustomerID").val();
    $("#txtReason").val();
    if (rowIndex < 0) {
        $.messager.alert('操作提示', "请选择一行编辑！", 'warning');
        return;
    }
    var datarow = $("#allData").datagrid('getData').rows[rowIndex];
    $("#form_Customer").form("load", datarow);
    getAuditDialog(customerID, customerName);
}
//待审核列表---审核
function getPendingAuditData(customerID, customerName, rowIndex) {
    $("#CustomerID").val();
    $("#txtReason").val();
    if (rowIndex < 0) {
        $.messager.alert('操作提示', "请选择一行编辑！", 'warning');
        return;
    }
    var datarow = $("#PendingAuditData").datagrid('getData').rows[rowIndex];
    $("#form_Customer").form("load", datarow);
    getAuditDialog(customerID, customerName);
}
 