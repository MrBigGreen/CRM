$(function () {

    $('#RepeatData').datagrid({
        title: '', //列表的标题
        iconCls: 'icon-edit',
        width: 'auto',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        //onClickRow: onClickRow,
        //url: '../Customer/GetAllData', //获取数据的url
        idField: 'CustomerID',
        sortName: 'CustomerID',
        sortOrder: 'desc',
        columns: [[
             { field: 'CustomerID', title: '客户编号', width: 195, hidden: true }
            , { field: 'CustomerName', title: '客户名称', width: 245 }
            , { field: 'MyName', title: '归属人', width: 85 }
            , { field: 'EmailAddress', title: '归属人邮箱', width: 195 }
            , { field: 'ProvinceName', title: '省份', width: 85 }
            , { field: 'CityName', title: '城市', width: 85 }
              , {
                  field: 'IsDelete', title: '状态', width: 100, formatter: function (value, rows) {
                      debugger;
                      if (value == true) {
                          return "无效";
                      }
                      else if (rows.IsFrozen == true) {
                          return "公共池";
                      }
                      else if (rows.AuditStatus == 2) {
                          return "待审核";
                      }
                      else if (rows.AuditStatus == 3) {
                          return "审核失败";
                      }
                      else {
                          return "正常";
                      }
                  }
              }

        ]],
    });


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
        sortOrder: 'asc',
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
                         //return "待审核";
                         var a = '<input type="button" value="审核" OnClick="getAuditAllData(\'' + rowData.CustomerID + '\',\'' + rowData.CustomerName + '\',' + rowIndex + ')"/>';

                         return a;
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
        sortOrder: 'asc',
        queryParams: { "search": "AuditStatus&2^" },
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
                        //return "待审核";
                        var a = '<input type="button" value="审核" OnClick="getPendingAuditData(\'' + rowData.CustomerID + '\',\'' + rowData.CustomerName + '\',' + rowIndex + ')"/>';

                        return a;
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
        queryParams: { "search": "AuditStatus&1^" },
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
        queryParams: { "search": "AuditStatus&3^" },
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
    $('#allData').datagrid('load', { search: search });

    $('#PendingAuditData').datagrid('load', { search: search + "AuditStatus&2^" });

    $('#AuditThroughData').datagrid('load', { search: search + "AuditStatus&1^" });

    $('#NotByData').datagrid('load', { search: search + "AuditStatus&3^" });
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
//审核
function getAuditDialog(customerID, customerName) {



    //检查是否有相似
    $.ajax({
        url: "/Customer/CustomerRepeat",
        type: "POST",
        data: { CustomerName: customerName, CustomerID: customerID },
        datatype: "json",
        async: false,
        success: function (data) {
            if (data.Result == 0) {
                //可以创建
                flag = true;
                $.messager.alert('操作提示', '客户可以创建！', 'info');
            }
            else if (data.Result == -1) {
                //不能创建
                $.messager.alert('操作提示', '客户名称已被其他人创建过，您不能重复创建!', 'warning');
                flag = false;
            }
            else if (data.Result == -2) {
                //不能创建
                $.messager.alert('操作提示', '您已经创建过此客户!', 'warning');
                flag = false;
            }
            else {
                $('#RepeatData').datagrid('loadData', { total: 0, rows: [] });//清空下方DateGrid 
                //需要验证
                $('#RepeatData').datagrid('loadData', data.Data);
                $('#repeatDialog').dialog({
                    title: '客户查重验证',
                    width: 980,
                    height: 550,
                    closed: false,
                    cache: false,
                    resizable: true,
                    modal: true,
                    shadow: true,
                    top: 65,
                    left: 65
                });
                flag = false;
            }
        }
    });

}

function getAudit(isPass) {
    var customerID = $("#CustomerID").val();
    if (customerID.length < 1) {
        $.messager.alert('操作提示', '数据有误，审核不成功!', 'warning');
        return false;
    }
    if (isPass == 0) {
        //审核拒绝
        var reason = $("#txtReason").val();
        if (reason.length < 1) {
            $.messager.alert('操作提示', '拒绝原因不能为空', 'warning');
            return false;
        }
    }

    $.ajax({
        url: "/Customer/GetAuditCustomer",
        datatype: "json",
        type: "post",
        data: { isPass: isPass, CustomerID: customerID, Reason: reason },
        success: function (data) {
            if (data.Result == 1) {
                $('#repeatDialog').dialog("close");
                $('#allData').datagrid('reload');
                $('#PendingAuditData').datagrid('reload');
            }
            else {
                $.messager.alert('操作提示', '审核失败' + data.Msg, 'warning');

            }
        }
    });
}
