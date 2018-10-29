$(function () {

    $('#flexigridData').datagrid({
        title: '呼叫报表', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',
        striped: true,
        url: '../CallRecording/GetReportsData', //获取数据的url
        idField: 'uniqueid',
        fitColumns: true,
        columns: [[
            { field: 'FcodeUserName', title: '姓名', width: $(this).width() * 0.3, align: 'center', resizable: false }
          , { field: 'src', title: '号码', width: $(this).width() * 0.3, align: 'center', resizable: false }
          , { field: 'Dial', title: '拨出电话数', width: $(this).width() * 0.2, align: 'center', resizable: false }
          , { field: 'Connect', title: '接通电话数', width: $(this).width() * 0.2, align: 'center', resizable: false }
          , { field: 'Valid', title: '有效电话数', width: $(this).width() * 0.3, align: 'center', resizable: false }
          , { field: 'Efficiency', title: '接通率', width: $(this).width() * 0.2, align: 'center', resizable: false }
          , { field: 'CallTotal', title: '总时长（分钟）', width: $(this).width() * 0.2, align: 'center', resizable: false }
        ]],
        onLoadSuccess: function () {
            $(".datagrid-header .datagrid-cell").css('text-align', 'center').css('color', '#173967');
        },
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        toolbar: '#tb'
    });

    $("#beginDate").datebox({
        onSelect: function () {
            if ($("#endDate").datebox('getValue') != "") {
                if (toDate($("#beginDate").datebox('getValue')) > toDate($("#endDate").datebox('getValue'))) {
                    $.messager.alert('提示', '开始日期不得晚于结束日期！', "info");
                    $("#beginDate").datebox("setValue", "");
                    return false;
                };
            }
        }
    });

    $("#endDate").datebox({
        onSelect: function () {
            if ($("#beginDate").datebox('getValue') != "") {
                if (toDate($("#endDate").datebox('getValue')) < toDate($("#beginDate").datebox('getValue'))) {
                    $.messager.alert('提示', '结束日期不得早于开始日期！', "info");
                    $("#endDate").datebox("setValue", "");
                    return false;
                } else {
                    var beginDate = $('#beginDate').datebox('getValue');
                    var endDate = $('#endDate').datebox('getValue');
                    var selectdate = beginDate + "," + endDate;

                }
            }

        }
    });

});
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function toDate(str) {
    var sd = str.split("-");
    return new Date(sd[0], sd[1], sd[2]);
}

//按条件查询
function SearchCall() {
    var callseconds = $('#callseconds').val();//通话时长

    var beginDate = $('#beginDate').datebox('getValue');//开始时间
    var endDate = $('#endDate').datebox('getValue');//结束时间

    var SysPersonId = "";
    var search = "";//归属人

    if ($("input[name='SysPersonIdList']").length > 0) {
        $("input[name='SysPersonIdList']:checked").each(function () {
            SysPersonId += $(this).val() + ',';
        })
    }
    else {
        //取得所有选中的节点，返回节点对象的集合
        var nodes = $("#myTree").tree("getChecked");
        if (nodes != null && nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                SysPersonId += nodes[i].id + ",";
            }
            if ($('input:radio[name="rdoSelect"]:checked').val() == "person") {
                //人员选择
                search = '0';
            }
            else {
                //部门选择
                search = '1';
            }
        }
    }

    if (SysPersonId.length > 0) {
        search = search + "&" + SysPersonId.substring(0, SysPersonId.length - 1);
    }
    else {
        search = search + "&" + $("#SysPersonID").val();
    }
    if (search == "&undefined") {
        search = "";
    }

    var parm = beginDate + "," + endDate + "$" + callseconds + "^" + search;

    var queryParam = $('#flexigridData').datagrid('options').queryParams;
    queryParam.state = parm;
    $('#flexigridData').datagrid('options').queryParams = queryParam;
    $('#flexigridData').datagrid('reload');

}

function ExportInfo() {
    var callseconds = $('#callseconds').val();//通话时长

    var beginDate = $('#beginDate').datebox('getValue');//开始时间
    var endDate = $('#endDate').datebox('getValue');//结束时间

    var SysPersonId = "";
    var search = "";//归属人

    if ($("input[name='SysPersonIdList']").length > 0) {
        $("input[name='SysPersonIdList']:checked").each(function () {
            SysPersonId += $(this).val() + ',';
        })
    }
    else {
        //取得所有选中的节点，返回节点对象的集合
        var nodes = $("#myTree").tree("getChecked");
        if (nodes != null && nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                SysPersonId += nodes[i].id + ",";
            }
            if ($('input:radio[name="rdoSelect"]:checked').val() == "person") {
                //人员选择
                search = '0';
            }
            else {
                //部门选择
                search = '1';
            }
        }
    }

    if (SysPersonId.length > 0) {
        search = search + "&" + SysPersonId.substring(0, SysPersonId.length - 1);
    }
    else {
        search = search + "&" + $("#SysPersonID").val();
    }
    if (search == "&undefined") {
        search = "";
    }

    var parm = beginDate + "," + endDate + "$" + callseconds + "^" + search;

    $.messager.confirm('提示', '是否导出?', function (r) {
        if (r) {
            var parm = beginDate + "," + endDate + "$" + callseconds + "^" + search;
            $.post("../CallRecording/Export",
                {
                    search: parm
                }, function (res) {
                    window.location.href = res;

                });
        }

    });

};


//搜索清空
function RemvoeAll() {

    $('#callseconds').val('');

    $('#beginDate').datebox('setValue', '');
    $('#endDate').datebox('setValue', '');
}

