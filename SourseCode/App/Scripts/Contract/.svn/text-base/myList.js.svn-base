$(function () {

    $('#flexigridData').datagrid({
        title: '合同列表', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',
        striped: true,
        collapsible: true,
        url: '../Contract/GetData', //获取数据的url
        idField: 'CustomerID',
        fitColumns: true,
        columns: [[
              { field: 'ck', title: '全选', checkbox: true, width: 80 }
            , { field: 'CustomerName', title: '公司名称', width: 160, align: 'left', resizable: false}
            , { field: 'ContractNO', title: '合同编号', width: 110, align: 'left' }
            , { field: 'RecommendSolutionName', title: '服务类型', width: 70, align: 'left' }
            , { field: 'SysPersonName', title: '签约人', width: 50, align: 'left' }
            , { field: 'SigningCompanyName', title: '签约公司', width: 120, align: 'left' }
            , {
                field: 'CreatedTime', title: '创建时间', width: 125, align: 'center', hidden: false, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
        ]],
        pagination: true,
        rownumbers: true,

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


//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    if ($('#CustomerName').val() != "") {
        search = search + 'CustomerName' + "&" + $('#CustomerName').val() + '^';
    }

    if ($("#StartDate").val() != "") {

        search = search + 'StartDate' + "&" + $('#StartDate').val() + '^';
    }
    if ($("#EndDate").val() != "") {
        search = search + 'EndDate' + "&" + $('#EndDate').val() + '^';
    }
     
     

    $("#flexigridData").datagrid("clearSelections");
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}

 

//新增合同
function flexiCreate() {
    //$('#w_add').window('open');
    //$('#addiframe').attr("src", "../Contract/CreateContractInfo");

    var href = "/Contract/CreateNew";
    showWindowTop('新增客户合同', href, 550, 420);

}
 



//导航到查看详细的按钮
function getView() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {
        var ID = arr[0].ContractID;
        showWindowTop("查看合同", "/Contract/ViewContract?id=" + ID, 680, 550);
    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;
}





