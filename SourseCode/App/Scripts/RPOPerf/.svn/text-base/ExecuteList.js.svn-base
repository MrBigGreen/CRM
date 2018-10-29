$(function () {

    $('#flexigridData').datagrid({
        title: 'RPO业绩列表', //列表的标题
        iconCls: 'icon-site',
        width: '100%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
       // url: '../RPOPerf/GetData', //获取数据的url
        idField: 'CustomerProjectID',
        sortName: 'CustomerName',
        sortOrder: 'desc',
        showFooter: true,//定义是否显示一行页脚。
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 100 },
             {
                 field: 'CustomerName', title: '客户名称', width: 180, sortable: true, formatter: function (value, rowData) {
                     return '<a href=\'javascript:window.parent.addTabUpdate("' + rowData.CustomerName + '", "/Customer/SelfDetails?ID=' + rowData.CustomerID + '", "tu1425", true);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
                 }
             },
            {
                field: 'ProjectName', title: '职位名称', width: 210, sortable: true, formatter: function (value, rowData) {
                    if (value == "合计")
                    {
                        return value;
                    }
                    return '<a href=\'javascript:showWindowTop("项目评估", "/CustomerProject/View?id=' + rowData.CustomerProjectID + '", 750, 600);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
                }
            },
            { field: 'DownLoadQty', title: '下载简历数', width: 100, sortable: true },
            { field: 'ContactPersonQty', title: '联系候选人数', width: 100, sortable: true },
            { field: 'AppInterviewQty', title: '预约面试数', width: 100, sortable: true },
            { field: 'InterviewQty', title: '实际面试数', width: 100, sortable: true },
            { field: 'OfferQty', title: 'Offer数', width: 100, sortable: true },
            { field: 'OnBoardQty', title: '上岗数', width: 100, sortable: true },
            { field: 'OverProbationQty', title: '过试用期数', width: 100, sortable: true },
            {
                field: 'op', title: '操作', width: 120, formatter: function (value, rowData, rowIndex) {
                    if (rowData.ProjectName == "合计") {
                        return "";
                    }
                    var a = '<input type="button" value="增加" OnClick="flexiCreate(\'' + rowData.CustomerProjectID + '\')"/>';
                    return a;
                }
            },
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
    var startDate = $("#StartDate").val();
    var EndDate = $("#EndDate").val();
    if (startDate != "") {
        search = search + 'StartDate' + "&" + startDate + '^';
    }
    if (EndDate != "") {
        search = search + 'EndDate' + "&" + EndDate + '^';
    }
    var CustomerName = $("#CustomerName").val();
    if (CustomerName != "") {
        search = search + 'CustomerName' + "&" + CustomerName + '^';
    }
    $('#flexigridData').datagrid('options').url = "../RPOPerf/GetData";
    $("#flexigridData").datagrid("clearSelections");
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}



//导航到创建的按钮
function flexiCreate(id) {
    showWindowTop("增加RPO业绩", "/RPOPerf/Create?id=" + id, 600, 500);
    return false;
}



//删除的按钮
function GetProjectFinish() {

    var arr = $('#flexigridData').datagrid('getSelections');
    if (arr.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    if (arr.length > 0) {
        var prjIDs = "";
        var prjNames = "";
        for (var i = 0; i < arr.length; i++) {

            prjIDs += arr[i].CustomerProjectID + ",";
            prjNames += arr[i].ProjectName + ",";

        }

        $.messager.confirm('操作提示', "您确定要结束“ " + prjNames + " ”项目吗？", function (r) {
            if (r) {
                var parm = "ProjectID=" + prjIDs;
                $.ajax({
                    type: "POST",
                    url: "../RPOPerf/GetProjectFinish/",
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
            }
        });

    } else {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
    }
};

