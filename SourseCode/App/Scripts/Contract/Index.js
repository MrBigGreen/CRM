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
            , {
                field: 'CustomerName', title: '公司名称', width: 170, align: 'left', resizable: false,
                formatter: function (value, rowData, rowIndex) {
                    var a = '<a href=\'javascript:window.parent.addTabUpdate("' + rowData.CustomerName + '", "/Customer/SelfDetails?ID=' + rowData.CustomerID + '", "tu1425", true);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + rowData.CustomerName + '</a> ';
                    return a;
                }
            }
            , { field: 'ContractNO', title: '合同编号', width: 150, align: 'left' }
            , { field: 'ContractMoney', title: '合同金额', width: 80, align: 'left' }
            , { field: 'RecommendSolutionName', title: '服务类型', width: 90, align: 'left' }
            , {
                field: 'EffectiveDate', title: '开始日期', width: 75, align: 'center', hidden: false, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd");
                    }
                }
            }
            , {
                field: 'Deadline', title: '结束日期', width: 75, align: 'center', hidden: false, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd");
                    }
                }
            }
            , { field: 'SysPersonName', title: '签约人', width: 70, align: 'left' }
            , { field: 'SigningCompanyName', title: '签约公司', width: 170, align: 'left' }
            , {
                field: 'CreatedTime', title: '创建时间', width: 125, align: 'center', hidden: false, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
        ]],
        onLoadSuccess: function () {
            $(".datagrid-header .datagrid-cell").css('text-align', 'center').css('color', '#173967');
        },
        pagination: true,
        rownumbers: true, 

    });

    var frameId = window.frameElement && window.frameElement.id || '';
    if (frameId == "iframeWindowTop" || frameId == "iframeWindow") {
        //添加选择按钮
        $('#flexigridData').datagrid("addToolbarItem", [{ "text": "选择", "iconCls": "icon-ok", handler: function () { flexiSelect(); } }]);
    }
    else {
        //如果列表页出现在弹出框中，则只显示查询和选择按钮
        var parent = window.dialogArguments; //获取父页面
        if (parent == "undefined" || parent == null) {
            //首先获取iframe标签的id值
            var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");
            //然后关闭AJAX相应的缓存
            $.ajaxSetup({
                cache: false
            });
            if (iframeid != "") {
                //获取按钮值
                $.getJSON("../Home/GetToolbar", { id: iframeid }, function (data) {
                    if (data == null) {
                        return;
                    }
                    $('#flexigridData').datagrid("addToolbarItem", data);
                });
            }
        }
    };

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

    var str = "";

    //推荐方案
    $("input[name='RecommendSolutionID']:checked").each(function () {
        str += $(this).val() + ",";
    })
    if (str != "") {
        str = str.substring(0, str.length - 1);
        search = search + 'RecommendSolutionID' + "&" + str + '^';
    }


    var SysPersonId = "";

    if ($("input[name='SysPersonIdList']").length > 0) {
        $("input[name='SysPersonIdList']:checked").each(function () {
            SysPersonId += $(this).val() + ',';
        })
    }
    else {
        var nodes = $("#myTree").tree("getChecked");
        if (nodes != null && nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                SysPersonId += nodes[i].id + ",";
            }
            if ($('input:radio[name="rdoSelect"]:checked').val() == "person") {
                //人员选择
                search = search + 'FindType&0^';
            }
            else {
                //部门选择
                search = search + 'FindType&1^';
            }
        }
    }

    if (SysPersonId.length > 0) {
        search = search + 'SysPersonID' + "&" + SysPersonId.substring(0, SysPersonId.length - 1) + '^';
    }
    else {
        search = search + 'SysPersonID' + "&" + $("#SysPersonID").val() + '^';
    }

    $("#flexigridData").datagrid("clearSelections");
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}





//“导出”按钮     在6.0版本中修改
function flexiExport() {
    var rows = $('#flexigridData').datagrid("getSelections");
    var rids = "";
    for (var i = 0; i < rows.length; i++) {
        rids += rows[i].ContractID + ",";
    }
    rids = rids.substr(0, rids.length - 1);


    var searchValue = $('#txtSearch').val();//查询条件
    var cityCode = $('#CityCode').val();//城市CODE
    var vocationCode = $('#VocationCode').val();//行业CODE
    var callPerson = "";//跟进人
    $("table [rel='userCallPerson']").each(function () {
        if ($(this).attr("checked") == "checked") {
            $('#pType').removeClass("bgred");
            var a = $(this).attr("data-value");
            if (a != 0) {
                callPerson += a + ",";
            }

        }
    });
    var checkContact = $('#hid_userCheckContract').val();//合同类别

    var packages = "";//购买服务
    $("table [rel='userPackages']").each(function () {
        if ($(this).attr("checked") == "checked") {
            $('#gType').removeClass("bgred");
            var a = $(this).attr("data-value");
            if (a != 0) {
                packages += a + ",";
            }

        }
    });

    $.messager.confirm('提示', '是否导出?', function (r) {
        if (r) {
            var parm = rids + "&" + cityCode + "$" + vocationCode + "$" + callPerson + "$" + checkContact + "$" + packages + "$" + searchValue;
            $.post("../Contract/Export",
                {
                    search: parm
                }, function (res) {
                    window.location.href = res;

                });
        }

    });
};

//新增合同
function flexiCreate() {
    showWindowTop('新增客户合同', "/Contract/CreateContractInfo", 680,560);
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


