$(function () {

    $('#flexigridData').datagrid({
        title: '分享进来', //列表的标题
        iconCls: 'icon-site',
        width: '100%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        url: '../CustomerShare/GetShareInData', //获取数据的url
        idField: 'CustomerShareID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        queryParams: { "id": $("#CustomerID").val() },
        columns: [[
            { field: 'SysPersonIDFrom', title: '分享人', width: 90 },
            { field: 'SysPersonIDTo', title: '接收分享', width: 90 },
            {
                field: 'CustomerName', title: '客户名称', width: 280, formatter: function (value, rowData) {
                    return '<a href=\'javascript:window.parent.addTabUpdate("' + rowData.CustomerName + '", "/Customer/SelfDetails?ID=' + rowData.CustomerID + '", "tu1425", true);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
                }
            },
            { field: 'ShareRemark', title: '说明', width: 290 }
             , {
                 field: 'LastUpdatedTime', title: '分享时间', width: 165, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
        ]],
        pagination: true,
        rownumbers: true

    });

});



//清空
function RemvoeAll() {
    $('#CustomerName').val('');
    $('#SysPersonIDFrom').val('');
    $('#SysPersonIDTo').val('');
    $('#StartDate').val('');
    $('#EndDate').val('');
}



//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    var CustomerName = $("#CustomerName").val();
    if (CustomerName != "") {
        search = search + 'CustomerName' + "&" + CustomerName + '^';
    }
    var SysPersonIDFrom = $("#SysPersonIDFrom").val();
    if (SysPersonIDFrom != "") {
        search = search + 'SysPersonIDFrom' + "&" + SysPersonIDFrom + '^';
    }
    var SysPersonIDTo = $("#SysPersonIDTo").val();
    if (SysPersonIDTo != "") {
        search = search + 'SysPersonIDTo' + "&" + SysPersonIDTo + '^';
    }

    var startDate = $("#StartDate").val();
    var EndDate = $("#EndDate").val();

    if (startDate != "") {
        search = search + 'StartDate' + "&" + startDate + '^';
    }
    if (EndDate != "") {
        search = search + 'EndDate' + "&" + EndDate + '^';
    }

    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}


