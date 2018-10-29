$(function () {

    $('#flexigridData').datagrid({
        title: '信用等级评分历史', //列表的标题
        iconCls: 'icon-site',
        width: '100%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        url: '../CustomerRating/GetCustomerRatingData', //获取数据的url
        idField: 'CustomerRatingID',
        sortName: 'LastUpdatedTime',
        sortOrder: 'desc',
        queryParams: { "id": $("#CustomerID").val() },
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 100 },
            { field: 'CompanyNatureText', title: '企业性质', width: 230 },
            { field: 'RegisterCapitalText', title: '注册资金', width: 120 },
            { field: 'SalesRevenueText', title: '销售收入', width: 120 },
             { field: 'ContractPeriodText', title: '合同账期', width: 120 },
             { field: 'OverdueAdvancesText', title: '逾期垫款', width: 120 },
             { field: 'RatingScore', title: '信用等级', width: 90, align: 'center' },
             { field: 'LastUpdatedBy', title: '评估人', width: 90 },
             {
                 field: 'LastUpdatedTime', title: '时间', width: 125, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
        ]],
        pagination: true,
        rownumbers: true,

    });

    $('#flexigridData').datagrid("addToolbarItem", [{ "text": "等级测评", "iconCls": "icon-add", "handler": "flexiCreate" }]);


});


//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    var startDate = $("#StartDate").datetimebox('getValue');
    var EndDate = $("#EndDate").datetimebox('getValue');

    if (startDate != "") {
        search = search + 'StartDate' + "&" + startDate + '^';
    }
    if (EndDate != "") {
        search = search + 'EndDate' + "&" + EndDate + '^';
    }

    //执行查询
    $('#flexigridData').datagrid('load', { search: search, id: $("#CustomerID").val() });
}

//导航到创建的按钮
function flexiCreate() {
    showWindowTop("新增信用评分", "/CustomerRating/Create?id=" + $("#CustomerID").val(), 500, 400);
    return false;
}




