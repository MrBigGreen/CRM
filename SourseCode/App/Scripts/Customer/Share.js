$(function () {
    ///*******************************************分享客户列表**************************************///
    $('#shareData').datagrid({
        title: '分享客户', //列表的标题
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
             { field: 'SysPersonName', title: '归属人/跟进人', width: 140 }
            , { field: 'CustomerName', title: '客户名称', width: 180 }
            , { field: 'CityName', title: '所在城市', width: 120 }
            , { field: 'CustomerLevel', title: '客户级别', width: 120 }

        ]],
        pagination: false,//分页工具
        rownumbers: true

    });
    //
    $('#sharePersonData').datagrid({
        title: '', //列表的标题
        iconCls: 'icon-site',
        striped: true,
        collapsible: true,
        //url: '../SysPerson/GetData', //获取数据的url
        sortName: 'Id',
        sortOrder: 'desc',
        idField: 'Id',
        fitColumns: true,
        queryParams: { "Data": 'All' },
        pagination: true,
        rownumbers: true,
        onDblClickRow: function (rowIndex, rowData, value) {

            $("#sharePersonName").val(rowData.MyName);
            $("#sharePersonID").val(rowData.Id);
            //$("#ShareRemark").val(rowData.Id);
            $("#shareDeptName").val(rowData.SysDepartmentId);

        }

    });



});



///*******************************************分享客户列表**************************************///
function getSharePerson() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    search = search + "MyName&" + $("#shareMyName").val() + "^";
    $('#sharePersonData').datagrid('options').url = "../SysPerson/GetData";
    $("#sharePersonData").datagrid("clearSelections");
    //执行查询
    $('#sharePersonData').datagrid('load', { search: search, Data: "All" });
}



//////*******************************************分享************************************************/
function share() {
    $('#shareData').datagrid('loadData', { total: 0, rows: [] });//清空下方DateGrid 
    $("#shareCustomerID").val('');
    $("#sharePersonName").val('');
    $("#sharePersonID").val('');
    $("#ShareRemark").val('');

    var rows = $('#flexigridData').datagrid('getSelections');
    var ids = "";
    if (rows.length > 0) {
        var ids = "";

        for (var i = 0; i < rows.length; i++) {
            //if (rows[i].GiveMe > 0) {
            //    $.messager.alert('操作提示', rows[i].CustomerName + '，不能二次分享!', 'warning');
            //    continue;
            //}
            ids += rows[i].CustomerID + ",";
            $('#shareData').datagrid("appendRow", {
                CustomerID: rows[i].CustomerID,
                SysPersonName: rows[i].SysPersonName,
                CustomerName: rows[i].CustomerName,
                CityName: rows[i].CityName,
                CustomerLevel: rows[i].CustomerLevel,
                CustomerFunnel: rows[i].CustomerFunnel,
                Opportunities: rows[i].Opportunities,
                ContactPerson: rows[i].ContactPerson,
                ContactPhone: rows[i].ContactPhone,
                Contact: rows[i].Contact,
                IsRegister: rows[i].IsRegister,
                IsCertification: rows[i].IsCertification
            });
        }
        if (ids == "") {
            return false;
        }
        $("#shareCustomerID").val(ids);


        $('#shareDialog').dialog({
            title: '客户分享设置',
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

//保存客户分享
function SaveShare() {

    //
    //1 获取要分享的人员
    var persons = $("#sharePersonName").val();

    if (persons == "" || persons == undefined || persons == null) {
        $.messager.alert('提示', '请选择分享人！', "info");
        return false;
    }
    var authority = $('input[name="authority"]:checked').val()
    persons = $("#sharePersonID").val() + "," + authority + "^";
    //
    var customerID = $("#shareCustomerID").val();
    if (customerID == "" || customerID == undefined || customerID == null) {
        $.messager.alert('提示', '请选择要分享的客户！', "info");
        return false;
    }
    var ShareRemark = $("#ShareRemark").val();
    $.messager.confirm('提示', '确定要分享客户吗?', function (r) {
        if (!r) {
            return;
        }
        var parm = "SysPersonIDs=" + persons + "&CustomerID=" + customerID + "&ShareRemark=" + ShareRemark;
        $.ajax({
            type: "POST",
            url: "../CustomerShare/Share/",
            data: parm,
            success: function (msg) {

                if (msg == "OK") {

                    //刷新整个表格
                    $('#flexigridData').datagrid('unselectAll');
                    $('#flexigridData').datagrid('reload');
                    $.messager.alert('提示', '分享成功！', "info", function () {
                        $(".easyui-dialog").window("close");
                    });
                }
                else if (msg == "") {
                    $.messager.alert('操作提示', '分享失败!请联系管理员。', 'info');
                }
                else {
                    $.messager.alert('错误', msg, "error");
                }
            }

        });
    });



}
