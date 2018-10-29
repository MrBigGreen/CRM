$(function () {
    /*添加水印*/
    $("#txtSearch").watermark("请输入公司名称");

    $('#flexigridData').datagrid({
        title: '我的任务', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',

        // nowrap: false,
        striped: true,
        //  collapsible: true,
        url: '../TodayTasks/GetData', //获取数据的url
        //sortName: 'FollowUpDate',
        //sortOrder: 'desc',
        idField: 'CustomerFollowID',
        fitColumns: true,
        //singleSelect: true,
        columns: [[
              { field: 'ck', title: '全选', checkbox: true, width: 80 }
            , { field: 'ReservationDate', title: '日期', width: $(this).width() * 0.3, align: 'center', resizable: false }
            , {
                field: 'CustomerName', title: '公司名称', width: $(this).width() * 0.3, align: 'center', resizable: false,
                formatter: function (value, rowData, rowIndex) {
                    //var a = '<a href="#"  class="easyui-linkbutton" style="border: none; text-decoration: none;"  OnClick="OpenCustomerInfo(\'' + rowData.CustomerID + '\')">' + rowData.CustomerName + '</a> ';
                    //return a;
                    return '<a href=\'javascript:window.parent.addTabUpdate("' + rowData.CustomerName + '", "/Customer/SelfDetails?ID=' + rowData.CustomerID + '", "tu1425", true);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + rowData.CustomerName + '</a> ';
                }
            }
              , {
                  field: 'PositionLink', title: '客户来源', width: $(this).width() * 0.3, align: 'center', resizable: false,
                  formatter: function (value, rowData, rowIndex) {
                      var a = "无";
                      if (value != undefined && value != "" && value != null) {
                          if (checkURL(value))
                          {
                              a = '<a href="' + value + '" target="_blank" class="easyui-linkbutton" style="border: none; text-decoration: none;" >查看</a> ';
                          }
                          else {
                              a = value;
                          }                          
                      }
                      return a;
                  }
              }
            , { field: 'CityName', title: '所在城市', width: $(this).width() * 0.2, align: 'center', resizable: false }
             , { field: 'CustomerFunnelName', title: '客户进程', width: $(this).width() * 0.3, align: 'center', resizable: false }
             , {
                 field: 'FollowBack', title: '最新客户需求备注', width: $(this).width() * 0.3, align: 'center', resizable: false, formatter: function (value, rowData, rowIndex) {
                     if (value && value.length > 20) {
                         var DCatRemark = value.substring(0, 20) + "...";
                         return '<div id="Remark-' + rowIndex + '" style="width:auto;">' + DCatRemark + '</div>';
                     } else {
                         return value;
                     }
                 }
             }
            , {
                field: 'State', title: '操作', width: $(this).width() * 0.2, align: 'center', resizable: false,
                formatter: function (value, rowData, rowIndex) {

                    //var a = '<input type="button" value="跟进记录" OnClick="OpenEditWindow(\'' + rowData.CustomerFollowID + '\',\'' + rowData.CustomerID + '\',\'' + rowData.CityName + '\',\'' + rowData.CityCode + '\',\'' + rowData.AddressDetails + '\',\'' + rowData.ProvinceName + '\',\'' + rowData.ProvinceCode + '\',\'' + rowData.DistrictName + '\',\'' + rowData.DistrictCode + '\',\'' + rowData.TelephoneExt + '\')"/>';
                    var a = '<input type="button" value="跟进记录" OnClick="OpenEditWindow(\'' + rowData.CustomerFollowID + '\',\'' + rowData.CustomerID + '\',\'' + rowData.CustomerName + '\',\'' + rowData.TelephoneExt + '\')"/>';
                    return a;

                }
            }
        ]],
        onLoadSuccess: function (data1) {
            //
            $(".datagrid-header .datagrid-cell").css('text-align', 'center').css('color', '#173967');
            var tooltipReason = "";
            var toolReason = "";
            for (var i = 0; i < data1.rows.length; i++) {
                if (data1.rows[i].FollowBack != undefined) {

                    var reason = data1.rows[i].FollowBack;
                    toolReason = "<tr><td>详细备注：" + reason
                            + " </td></tr>";
                }
                //拼写tooltip的内容 
                tooltipReason = "<table style='height:55px;width:465px;color:black'>"
                        + toolReason + "</table>";
                addTooltip(tooltipReason, 'Remark-' + i);
            }
        },
        pagination: true,
        rownumbers: true,
        toolbar: '#tb'

    });



    function addTooltip(tooltipContentStr, tootipId) {
        //添加相应的tooltip     
        $('#' + tootipId).tooltip({
            position: 'bottom',
            content: tooltipContentStr,
            onShow: function () {
                $(this).tooltip('tip').css({
                    backgroundColor: 'white',
                    borderColor: '#97CBFF'
                });
            }
        });
    }


    ///*******************************************归属人设置列表**************************************///
    $('#FollowBatchData').datagrid({
        title: '批量设置跟进的客户', //列表的标题
        iconCls: 'icon-site',
        width: '95%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        idField: 'CustomerFollowID',
        sortName: 'CustomerFollowID',
        sortOrder: 'desc',
        columns: [[
              { field: 'ReservationDate', title: '日期', width: 150, align: 'center', resizable: false }
            , { field: 'CustomerName', title: '公司名称', width: 190, align: 'center', resizable: false }
            , { field: 'CityName', title: '所在城市', width: 150, align: 'center', resizable: false }
            , { field: 'ProvinceName', title: '省', width: 100, align: 'center', resizable: false, hidden: true }
            , { field: 'DistrictName', title: '区', width: 100, align: 'center', resizable: false, hidden: true }
            , { field: 'AddressDetails', title: '详细地址', width: 100, align: 'center', resizable: false, hidden: true }
            , { field: 'CustomerLevel', title: '客户级别', width: 100, align: 'center', resizable: false }
            , { field: 'MyName', title: '跟进人', width: 100, align: 'center', resizable: false }

        ]],
        pagination: false,//分页工具
        rownumbers: true

    });

    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    $("#beginDate").datebox({
        onSelect: function () {
            if ($("#endDate").datebox('getValue') != "") {
                if (toDate($("#beginDate").datebox('getValue')) > toDate($("#endDate").datebox('getValue'))) {
                    $.messager.alert('提示', '开始日期不得晚于结束日期！', "info");
                    $("#beginDate").datebox("setValue", "");
                    return false;
                };
            }
            //$(".datebox> a").css("background", "none");
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

    //选择查询变色
    $("#ttSearch").find("a").each(function () {
        $(this).click(function () {
            //if ($(this).id) {
            $(this).parent().children("a").removeClass("bgred");
            $(this).addClass("bgred");
            $("#hid_" + $(this).attr("rel")).val($(this).attr("data-value"));
            //}

            //alert($(this).val());
        })
    });





    $('#pType').addClass("bgred");
    $('#cType').addClass("bgred");

    $('#hid_userCallType').val('0');


});
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function toDate(str) {
    var sd = str.split("-");
    return new Date(sd[0], sd[1], sd[2]);
}
function RemoveCheckClass() {
    $('#pType').removeClass("bgred");
}
function AddCheckClass() {
    $('#pType').addClass("bgred");
    $("table [rel='userCallPerson']").each(function () {
        $(this).attr("checked", false);
    })
}
//按条件查询
function SearchToday() {
    var search = "";

    var callPerson = "";//跟进人
    var callType = "";//$('#hid_userCallType').val();//跟进方式
    var callPurpose = $('#CustomerFunnel').val();//客户进程
    var searchValue = $('#txtSearch').val();//查询条件
    var beginDate = $('#beginDate').datebox('getValue');//开始时间
    var endDate = $('#endDate').datebox('getValue');//结束时间
    if (callPurpose == "--请选择--") {
        callPurpose = "";
    }

    search = 'CustomerName&' + $('#txtSearch').val() + '^';
    search += 'StartDate&' + beginDate + '^';
    search += 'EndDate&' + endDate + '^';
    search += 'CustomerFunnel&' + callPurpose + '^';
    $("#flexigridData").datagrid("clearSelections");
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}

//“导出”按钮     在6.0版本中修改
function ExportInfo() {
    var rows = $('#flexigridData').datagrid("getSelections");
    var rids = "";
    for (var i = 0; i < rows.length; i++) {
        rids += rows[i].CustomerFollowID + ",";
    }
    rids = rids.substr(0, rids.length - 1);

    var callPerson = "";//跟进人
    var callType = $('#hid_userCallType').val();//跟进方式
    var callPurpose = $('#sCallPurpose').combobox('getValue');//跟进目的
    var searchValue = $('#txtSearch').val();//查询条件
    var beginDate = $('#beginDate').datebox('getValue');//开始时间
    var endDate = $('#endDate').datebox('getValue');//结束时间

    $("table [rel='userCallPerson']").each(function () {
        if ($(this).attr("checked") == "checked") {
            $('#pType').removeClass("bgred");
            var a = $(this).attr("data-value");
            if (a != 0) {
                callPerson += a + ",";
            }

        }
    })

    if (callPurpose == "--请选择--") {
        purpose = "";
    }

    $.messager.confirm('提示', '是否导出?', function (r) {
        if (r) {
            var parm = rids + "&" + "0" + "^" + beginDate + "," + endDate + "$" + callPerson + "$" + callPurpose + "$" + callType + "$" + searchValue;
            $.post("../TodayTasks/Export",
                {
                    search: parm
                }, function (res) {
                    window.location.href = res;

                });
        }

    });

};

//填写跟进记录
function OpenEditWindow(fid, cid, CustomerName, fcode) {

    var href="/CustomerFollow/FollowBack?CustomerID=" + cid + "&FollowID=" + fid + "&Fcode=" + fcode;
    showWindowTop('填写【' + CustomerName + '】跟进记录', href, 950, 620);

}

//查看联系方式
function OpenCallWindow(cid, fid) {
    $('#w_message').window('open');
    $('#messageiframe').attr("src", "../TodayTasks/ShowContactsInfo?CustomerID=" + cid + "&&fID=" + fid);
}

//查看公司信息
function OpenCustomerInfo(cid) {
    $('#companyInfoiframe').attr("src", "../Customer/SelfDetails?id=" + cid + "&tab=客户跟进记录卡");

    var win = window.top.$("<div class='easyui-dialog'>" + $("#w_companyInfo").html() + "</div>").appendTo(window.top.document.body);
    win.window({
        title: "公司详情",
        closed: false,
        cache: false,
        resizable: true,
        modal: true,
        //top: 60,
        width: 1180,
        height: 700,
        onClose: function () {
            win.window('destroy');//关闭即销毁
        }
    });
}

//任务调整
function ChangeInfo() {

    var rows = $('#flexigridData').datagrid("getSelections");
    $('#FollowBatchData').datagrid('loadData', { total: 0, rows: [] });//清空下方DateGrid
    $("#w_changeInfo").find(".panel.datagrid").hide();

    if (rows.length == 0) {
        $.messager.alert('提示', '请选择数据！', "info");
    }
    else if (rows.length == 1) {
        var fid = rows[0].CustomerFollowID;
        $('#w_changeInfo').dialog({
            title: '任务调整',
            width: 680,
            height: 580,
            closed: false,
            cache: false,
            resizable: true,
            modal: true,
            top: 60,
            left: 70,
        });
        $('#changeInfoiframe').attr("src", "../CustomerFollow/Edit?ID=" + fid);
    } else {
        //       
        var ids = "";
        for (var m = 0; m <= rows.length - 1; m++) {
            ids += rows[m].CustomerFollowID + ",";
            $('#FollowBatchData').datagrid("appendRow", {
                ReservationDate: rows[m].ReservationDate,
                CustomerName: rows[m].CustomerName,
                CityName: rows[m].CityName,
                ProvinceName: rows[m].ProvinceName,
                DistrictName: rows[m].DistrictName,
                AddressDetails: rows[m].AddressDetails,
                CustomerLevel: rows[m].CustomerLevel,
                MyName: rows[m].MyName
            });
        }
        $("#CustomerFollowID").val(ids);
        $("#w_changeInfo").find(".panel.datagrid").show();
        $('#w_changeInfo').find("iframe").attr("src", "/CustomerFollow/BatchEdit?ID=" + ids);
        $('#w_changeInfo').dialog({
            title: '批量设置任务调整',
            width: 830,
            height: 540,
            closed: false,
            cache: false,
            resizable: true,
            modal: true,
            top: 60,
            left: 60,
        });
    }
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
//搜索清空
function RemvoeAll() {
    $("#ttSearch").find("a").each(function () {
        $(this).parent().children("a").removeClass("bgred");
        $("#hid_" + $(this).attr("rel")).val('0');
    });
    $('#pType').addClass("bgred");
    $("table [rel='userCallPerson']").each(function () {
        $(this).attr("checked", false);
    })
    $('#cType').addClass("bgred");
    $('#hid_userCallPerson').val('0');
    $('#hid_userCallType').val('0');

    //$('#sCallPurpose').combobox('setValue', '--请选择--');

    $('#txtSearch').val('');

    $('#beginDate').datebox('setValue', '');
    $('#endDate').datebox('setValue', '');
    $("#flexigridData").datagrid("clearSelections");
}


//////*******************************************释放客户************************************************/
function Release() {
    var rows = $('#flexigridData').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('操作提示', '请至少选择一条数据!', 'warning');
        return false;
    }
    var ids = "";
    var names = "";
    for (var i = 0; i < rows.length; i++) {
        ids += rows[i].CustomerID + ",";
        names += rows[i].CustomerName + ",";
    }
    if (ids == "") {
        return false;
    }
    $.messager.confirm('提示', '您确定要释放“' + names + '”客户吗?', function (r) {
        if (r) {
            $.ajax({
                url: "/Customer/Release",
                type: "POST",
                data: { IDs: ids },
                success: function (data) {
                    if (data.Result == 1) {
                        //刷新整个表格
                        $('#flexigridData').datagrid('unselectAll');
                        $('#flexigridData').datagrid('reload');
                        $.messager.alert('操作提示', '客户释放成功。', 'info');
                    }
                    else {
                        $.messager.alert('操作提示', '客户释放失败，错误：' + data.Msg, 'info');
                    }
                },
                error: function () {
                    $.messager.alert('操作提示', '客户释放失败，网络异常！', 'info');
                }

            });
        }
    });
}