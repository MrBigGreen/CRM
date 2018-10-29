$(function () {
    /*添加水印*/
    $("#txtSearch").watermark("请输入被叫号码/企业名称(不支持模糊查询)");

    $('#CallTime').combobox({
        url: '../CallRecording/GetCombox?type=151026152954449980429ade5bf72',
        valueField: 'id',
        textField: 'text',
        editable: false
    });
    $('#CallType').combobox({
        url: '../CallRecording/GetCombox?type=151026153116857693866d8117e23',
        valueField: 'id',
        textField: 'text',
        editable: false
    });


    $('#flexigridData').datagrid({
        title: '通话记录', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',
        striped: true,
        url: '../CallRecording/GetData', //获取数据的url
        idField: 'uniqueid',
        fitColumns: true,
        columns: [[
            //   { field: 'ck', title: '全选', checkbox: true, width: 80 }
            //,
            { field: 'calldate', title: '通话日期', width: $(this).width() * 0.3, align: 'center', resizable: false }
            , {
                field: 'src', title: '主叫号码', width: $(this).width() * 0.3, align: 'center', resizable: false
            }
              , {
                  field: 'FcodeUserName', title: '归属人', width: $(this).width() * 0.2, align: 'center', resizable: false
              }
            , { field: 'dst', title: '被叫号码', width: $(this).width() * 0.2, align: 'center', resizable: false }
            , { field: 'CompanyName', title: '公司名称', width: $(this).width() * 0.3, align: 'center', resizable: false }
             , {
                 field: 'billsec', title: '通话时长', width: $(this).width() * 0.2, align: 'center', resizable: false
             }
                 , {
                     field: 'disposition', title: '通话状态', width: $(this).width() * 0.2, align: 'center', resizable: false
                 }
            , {
                field: 'State', title: '操作', width: $(this).width() * 0.5, align: 'center', resizable: false,
                formatter: function (value, rowData, rowIndex) {
                    if (rowData.disposition == "已接听") {
                        var a = '<input type="button" value="播放" OnClick="OpenSoundRecordingWindow(\'' + rowData.userField + '\')"/>';
                        var b = '<input type="button" value="批注" OnClick="OpenEditCommentWindow(\'' + rowData.uniqueid + '\',\'' + rowData.userField + '\',\'' + rowData.src + '\')"/>';
                        return a + b;
                    } else {
                        return "无";
                    }
                 }
            }
        ]],
        onLoadSuccess: function () {
            $(".datagrid-header .datagrid-cell").css('text-align', 'center').css('color', '#173967');
        },
        pagination: true,
        rownumbers: true,
        singleSelect: true
        //,toolbar: '#tb'
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

var isSelect = 0;
//全选
function allSelect() {
    if (isSelect == 0) {
        isSelect = 1;
        $('#flexigridData').datagrid('selectAll');
    } else {
        isSelect = 0;
        $('#flexigridData').datagrid('unselectAll');
    }
}

//按条件查询
function SearchCall() {


    var calltime = $('#CallTime').combobox('getValue');//通话时长类型
    var callseconds = $('#callseconds').val();//通话时长

    var callType = $('#CallType').combobox('getValue');//通话状态

    // var leaderRemark = $('#LeaderRemark').combobox('getValue');//通话状态

    var searchValue = $('#txtSearch').val();//查询条件
    var beginDate = $('#beginDate').datebox('getValue');//开始时间
    var endDate = $('#endDate').datebox('getValue');//结束时间

    if (calltime == "--请选择--") {
        calltime = "";
    }
    if (callType == "--请选择--") {
        callType = "";
    }
    //if (leaderRemark == "--请选择--") {
    //    leaderRemark = "";
    //}
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

    var parm = beginDate + "," + endDate + "$" + calltime + "$" + callseconds + "$" + callType + "$" + searchValue + "^" + search;

    var queryParam = $('#flexigridData').datagrid('options').queryParams;
    queryParam.state = parm;
    $('#flexigridData').datagrid('options').queryParams = queryParam;
    $('#flexigridData').datagrid('reload');

}

//查看批注
function OpenLookCommentWindow(cid) {
    $('#w_lookComment').window('open');
    $('#lookCommentiframe').attr("src", "../CallRecording/LookCommentWindow?cid=" + cid);
}

//编辑批注
function OpenEditCommentWindow(cid, url, Fcode) {
    $('#w_editComment').window('open');
    $('#editCommentiframe').attr("src", "../CallRecording/EditCommentWindow?cid=" + cid + "&&url=" + url + "&&Fcode=" + Fcode);
}
//查看录音
function OpenSoundRecordingWindow(url) {
    var urlFile = "http://192.168.1.254/play_files_ec.php?f_path=" + url;

    $('#w_soundRecording').window('open');
    $('#w_soundRecordingiframe').attr("src", "../CallRecording/ShowCallRecording?FileUrl=" + urlFile);
}


//搜索清空
function RemvoeAll() {

    //alert(111);
    $('#CallTime').combobox('setValue', '--请选择--');
    $('#CallType').combobox('setValue', '--请选择--');
    // $('#LeaderRemark').combobox('setValue', '--请选择--');
    $('#callseconds').val('');
    $('#txtSearch').val('');
    // $('#myTree').find('.tree-node-selected').removeClass('tree-node-selected');
    $('#beginDate').datebox('setValue', '');
    $('#endDate').datebox('setValue', '');
}
function ExportSound() {
    var rows = $('#flexigridData').datagrid("getSelections");
    var rids = "";
    for (var i = 0; i < rows.length; i++) {
        if (rows[i].disposition == "已接听") {
            rids += rows[i].userField + ",";
        }
       
    }
    rids = rids.substr(0, rids.length - 1);
    if (rids.length=0) {
        $.messager.alert('提示', '请选择已接听的录音！', "info");
    } else {
        $.messager.confirm('提示', '是否导出录音?', function (r) {
            if (r) {
                var parm = rids;
                $.post("../CallRecording/ExportSound",
                    {
                        search: parm
                    }, function (res) {
                        window.location.href = res;

                    });
            }

        });
    }
 

};
