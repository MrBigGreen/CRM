$(function () {
    /*添加水印*/
    $("#Keyword").watermark("搜索客户名称、联系人、联系电话");
    $("#BackRemark").watermark("搜索跟进反馈内容");

    //城市选择
    $("#btnCityName").TypeShow("multiple", 27, "city", $("#btnCityName").val());//地址

    //////////////////////电话联系///
    $('#call').datagrid({
        //  title: '温馨提示：深蓝色背景数据为往日未呼任务！~', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',

        //nowrap: false,
        striped: true,
        //collapsible: true,
        url: '../TodayTasks/GetCallHistroy', //获取数据的url

        idField: 'CustomerFollowID',
        fitColumns: true,
        columns: [[
              { field: 'ck', title: '全选', checkbox: true, width: 80 }
               , {
                   field: 'FollowUpDate', title: '跟进日期', width: $(this).width() * 0.2, align: 'center', resizable: false,
                   formatter: function (value, rowData, rowIndex) {
                       if (value) {
                           return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                       }
                   }
               }
            , {
                field: 'CustomerName', title: '公司名称', width: $(this).width() * 0.3, align: 'center', resizable: false,
                formatter: function (value, rowData, rowIndex) {
                    var a = '<a href="#"  class="easyui-linkbutton" style="border: none; text-decoration: none;"  OnClick="OpenCustomerInfo(\'' + rowData.CustomerID + '\')">' + rowData.CustomerName + '</a> ';
                    return a;
                }
            }
            , { field: 'ContactName', title: '联系人', width: $(this).width() * 0.2, align: 'center', resizable: false }
            //, { field: 'CustomerLevel', title: '级别', width: $(this).width() * 0.2, align: 'center', resizable: false }
            , {
                field: 'CallPhone', title: '联系方式', width: $(this).width() * 0.2, align: 'center', resizable: false
            }
            , {
                field: 'ContactState', title: '是否接通', width: $(this).width() * 0.2, align: 'center', resizable: false,
                formatter: function (value, rowData, rowIndex) {
                    var isContact = rowData.ContactState;
                    if (isContact == 1) {
                        return "是";
                    } else
                        return "否";

                }
            }
            //, { field: 'Opportunities', title: '商机判断', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false }
            , { field: 'CustomerFunnel', title: '客户进程', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false }
            , { field: 'Remark', title: '跟进反馈', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false }
            , { field: 'MyName', title: '跟进人', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false }
            , { field: 'CityCode', title: '城市ID', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false, hidden: true }
            , { field: 'CityName', title: '城市', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false, hidden: true }
            , {
                field: 'State', title: '操作', width: $(this).width() * 0.2, align: 'center', resizable: false,
                formatter: function (value, rowData, rowIndex) {
                    var a = '<input type="button" value="查看" OnClick="OpenModalDialog(\'' + rowData.CustomerID + '\',\'' + rowData.CustomerName + '\',1)"/>';

                    return a;

                }
            }
        ]],
        onLoadSuccess: function () {
            $(".datagrid-header .datagrid-cell").css('text-align', 'center').css('color', '#173967');
        },
        pagination: true,
        rownumbers: true,

        toolbar: '#tb'

    });

    //////////////////////见面约谈///
    $('#meeting').datagrid({
        //title: '温馨提示：选择日期可自动进行搜索查询', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',
        //nowrap: false,
        striped: true,
        //collapsible: true,
        url: '../TodayTasks/GetMeetingHistroy', //获取数据的url

        idField: 'CustomerFollowID',
        fitColumns: true,

        columns: [[
             { field: 'ck', title: '全选', checkbox: true, width: 80 }
            , {
                field: 'FollowUpDate', title: '拜访日期', width: $(this).width() * 0.2, align: 'center', resizable: false,
                formatter: function (value, rowData, rowIndex) {
                    var followDate = parseInt(rowData.FollowUpDate.replace(/\D/igm, ""));

                    var d = new Date(followDate);

                    return d.toLocaleDateString().replace('/', '-').replace('/', '-');
                }
            }
            , {
                field: 'CustomerName', title: '公司名称', width: $(this).width() * 0.2, align: 'center', resizable: false,
                formatter: function (value, rowData, rowIndex) {
                    var a = '<a href="#"  class="easyui-linkbutton" style="border: none; text-decoration: none;"  OnClick="OpenCustomerInfo(\'' + rowData.CustomerID + '\')">' + rowData.CustomerName + '</a> ';
                    return a;
                }
            }
            , { field: 'ContactName', title: '拜访人', width: $(this).width() * 0.2, align: 'center', resizable: false }
            , { field: 'CustomerLevel', title: '拜访人级别', width: $(this).width() * 0.2, align: 'center', resizable: false }
            , { field: 'CustomerFunnel', title: '客户进程', width: $(this).width() * 0.2, align: 'center', resizable: false }
            , { field: 'Opportunities', title: '商机判断', width: $(this).width() * 0.2, align: 'center', resizable: false }
            , { field: 'MyName', title: '跟进人', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false }
            , { field: 'CityCode', title: '城市ID', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false, hidden: true }
            , { field: 'CityName', title: '城市', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false, hidden: true }
            , { field: 'CallPhone', title: '联系电话', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false, hidden: true }
            , {
                field: 'State', title: '操作', width: $(this).width() * 0.2, align: 'center', resizable: false,
                formatter: function (value, rowData, rowIndex) {
                    var a = '<input type="button" value="查看" OnClick="OpenModalDialog(\'' + rowData.CustomerID + '\',\'' + rowData.CustomerName + '\',1)"/>';

                    return a;
                }
            }
        ]],
        onLoadSuccess: function () {
            $(".datagrid-header .datagrid-cell").css('text-align', 'center').css('color', '#173967');
        },
        pagination: true,
        rownumbers: true,

        toolbar: '#tbb'

    });



    /////////////////////////////////////////////////////////////////////////////////////////////////////////

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
    $('#ContactState').val('');


    $('#CustomerFunnel').combobox({
        url: '../TodayTasks/GetCombox?type=0&&parm=1506121008181709094ed4907cf81',
        valueField: 'id',
        textField: 'text',
        editable: false
    });

    $("#StartDate").datebox({
        onSelect: function () {
            if ($("#EndDate").datebox('getValue') != "") {
                if (toDate($("#StartDate").datebox('getValue')) > toDate($("#EndDate").datebox('getValue'))) {
                    $.messager.alert('提示', '开始日期不得晚于结束日期！', "info");
                    $("#StartDate").datebox("setValue", "");
                    return false;
                };
            }
        }
    });

    $("#EndDate").datebox({
        onSelect: function () {
            if ($("#StartDate").datebox('getValue') != "") {
                if (toDate($("#EndDate").datebox('getValue')) < toDate($("#StartDate").datebox('getValue'))) {
                    $.messager.alert('提示', '结束日期不得早于开始日期！', "info");
                    $("#EndDate").datebox("setValue", "");
                    return false;
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
    if ($('#Keyword').val() != "") {
        search = search + 'Keyword' + "&" + $('#Keyword').val() + '^';
    }
    if ($('#BackRemark').val() != "") {
        search = search + 'BackRemark' + "&" + $('#BackRemark').val() + '^';
    }
    var ContactState = $('#ContactState').val();
    if (ContactState != "") {
        search = search + 'ContactState' + "&" + ContactState + '^';
    }
    var customerFunnel = $('#CustomerFunnel').combobox('getValue');//客户进程
    if (customerFunnel != "") {
        search = search + 'CustomerFunnel' + "&" + customerFunnel + '^';
    }
    if ($('#CityCode').val() != "") {
        search = search + 'CityCode' + "&" + $('#CityCode').val() + '^';
    }

    var callPerson = "";//跟进人
    if ($("input[name='SysPersonIdList']").length > 0) {
        $("input[name='SysPersonIdList']:checked").each(function () {
            callPerson += $(this).val() + ',';
        })
    }
    
    
    if (callPerson != "") {
        search = search + 'SysPersonID' + "&" + callPerson.substring(0, callPerson.length - 1) + '^';
    }
    else {
        search = search + 'SysPersonID' + "&" + $('#SysPersonID').val() + '^';
    }
    
    var beginDate = $('#StartDate').datebox('getValue');//开始时间
    var endDate = $('#EndDate').datebox('getValue');//结束时间
    if (beginDate != "") {
        search = search + 'StartDate' + "&" + beginDate + '^';
    }
    if (endDate != "") {
        search = search + 'EndDate' + "&" + endDate + '^';
    }
    var pp = $('#worktabs').tabs('getSelected');
    var tab = pp.panel('options').id;
      
    if (tab == "divMeet") {
        $('#meeting').datagrid('load', { search: search });

    } else {
        $('#call').datagrid('load', { search: search });
    }


}

//“导出”按钮     在6.0版本中修改
function CallExportInfo() {
    var rows = $('#call').datagrid("getSelections");
    var rids = "";
    for (var i = 0; i < rows.length; i++) {
        rids += rows[i].CustomerFollowID + ",";
    }
    rids = rids.substr(0, rids.length - 1);


    var callPerson = "";//跟进人
    var isLine = $('#ContactState').val();//是否接通
    var customerOffer = $('#CustomerFunnel').combobox('getValue');//商机判断
    var searchValue = $('#Keyword').val();//查询条件
    var beginDate = $('#StartDate').datebox('getValue');//开始时间
    var endDate = $('#EndDate').datebox('getValue');//结束时间
    $("table [rel='userCallPerson']").each(function () {
        if ($(this).attr("checked") == "checked") {
            $('#pType').removeClass("bgred");
            var a = $(this).attr("data-value");
            if (a != 0) {
                callPerson += a + ",";
            }

        }
    })

    var cityCode = $('#CityCode').val();
    var cityName = $('#CityName').val();


    $.messager.confirm('提示', '是否导出?', function (r) {
        if (r) {
            var parm = rids + "&" + "1" + "^" + beginDate + "," + endDate + "$" + callPerson + "$" + customerOffer + "$" + isLine + "$" + cityCode + "$" + searchValue;
            $.post("../TodayTasks/Export",
                {
                    search: parm
                }, function (res) {
                    window.location.href = res;

                });
        }

    });
};

function MeetingExportInfo() {
    var rows = $('#call').datagrid("getSelections");
    var rids = "";
    for (var i = 0; i < rows.length; i++) {
        rids += rows[i].CustomerFollowID + ",";
    }
    rids = rids.substr(0, rids.length - 1);


    var callPerson = "";//跟进人
    var isLine = $('#ContactState').val();//是否接通
    var customerOffer = $('#CustomerFunnel').combobox('getValue');//商机判断
    var searchValue = $('#Keyword').val();//查询条件
    var beginDate = $('#StartDate').datebox('getValue');//开始时间
    var endDate = $('#EndDate').datebox('getValue');//结束时间
    $("table [rel='userCallPerson']").each(function () {
        if ($(this).attr("checked") == "checked") {
            $('#pType').removeClass("bgred");
            var a = $(this).attr("data-value");
            if (a != 0) {
                callPerson += a + ",";
            }

        }
    })

    var cityCode = $('#CityCode').val();
    var cityName = $('#CityName').val();

    $.messager.confirm('提示', '是否导出?', function (r) {
        if (r) {
            var parm = rids + "&" + "2" + "^" + beginDate + "," + endDate + "$" + callPerson + "$" + customerOffer + "$" + isLine + "$" + cityCode + "$" + searchValue;
            $.post("../TodayTasks/Export",
                {
                    search: parm
                }, function (res) {
                    window.location.href = res;

                });
        }

    });
};

var isCallSelect = 0;
//全选
function allCallSelect() {
    if (isCallSelect == 0) {
        isCallSelect = 1;
        $('#call').datagrid('selectAll');
    } else {
        isCallSelect = 0;
        $('#call').datagrid('unselectAll');
    }
}

var isMeetingSelect = 0;
function allMeetingSelect() {
    if (isMeetingSelect == 0) {
        isMeetingSelect = 1;
        $('#meeting').datagrid('selectAll');
    } else {
        isMeetingSelect = 0;
        $('#meeting').datagrid('unselectAll');
    }
}

//查看公司信息
function OpenCustomerInfo(cid) {
    //$('#w_companyInfo').window({
    //    title: '公司详情',
    //    width: 1100,
    //    height: 780,
    //    closed: false,
    //    cache: false,
    //    resizable: true,
    //    modal: true,
    //    top: 60,
    //    left: 70,
    //});
    //$('#companyInfoiframe').attr("src", "../Customer/SelfDetails?id=" + cid);

    $('#companyInfoiframe').attr("src", "../Customer/SelfDetails?id=" + cid );
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

//查看跟进日志
function OpenModalDialog(fid,customerName) {
    //$('#w_followInfo').window('open');
    $('#followInfoiframe').attr("src", "../CustomerFollow/FollowBackHistory?CustomerID=" + fid + "&page=1");        
    $('#w_followInfo').dialog({
        title: '查看【' + customerName + '】跟进记录',
        width: 962,
        height: 616,
        closed: false,
        cache: false,
        resizable: true,
        modal: true,
        left: 30,
        top: 30,
    });
    return false;
}

//查看录音
function OpenCallRecordingWindow(url) {
    $('#w_callRecording').window('open');
    $('#callRecordingiframe').attr("src", "../TodayTasks/ShowCallRecording?FileUrl=" + url);
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
    $('#ContactState').val('')

    $('#CustomerFunnel').combobox('setValue', '--请选择--');

    $('#Keyword').val('');
    $("#BackRemark").val('');
    $('#btnCityName').val("选择城市");
    $('#CityCode').val('');
    $('#CityName').val('');

    $('#StartDate').datebox('setValue', '');
    $('#EndDate').datebox('setValue', '');
}

