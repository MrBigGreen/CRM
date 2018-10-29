$(function () {
    //行业
    $("#btnVocationName").TypeShow("single", 27, "industry", $("#btnVocationName").val());
    //地址
    $("#btnCityName").TypeShow("single", 27, "city", $("#btnCityName").val());

    $('#flexigridData').datagrid({
        title: '公共客户池列表', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: '580px',
        striped: true,
        collapsible: true,
        singleSelect: false,
        url: '../Customer/GetPublicData', //获取数据的url
        idField: 'CustomerID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        columns: [[
             {
                 field: 'CreatedTime', title: '创建日期', width: 135, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
             , {
                 field: 'LastUpdatedTime', title: '释放日期', width: 135, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
           // , { field: 'SysPersonName', title: '原归属人', width: 90 }
            , {
                field: 'CustomerName', title: '客户名称', width: 260, formatter: function (value, rowData) {

                    var a = '<a href="#"  class="easyui-linkbutton" style="border: none; text-decoration: none;"  OnClick="CustomerShow(\'' + rowData.CustomerID + '\')">' + value + '</a> ';
                    return a;
                }
            }
             , { field: 'RatingScore', title: '信用等级', width: 80 }
            , { field: 'CompanyNatureCode', title: '企业性质', width: 130 }
            , { field: 'CityName', title: '所在城市', width: 90 }
            , { field: 'CustomerLevel', title: '客户级别', width: 90 }
             , {
                 field: 'PositionLink', title: '客户来源', width: 150, align: 'center', resizable: false,
                 formatter: function (value, rowData, rowIndex) {
                     var a = "无";
                     if (value != undefined && value != "" && value != null) {
                         if (checkURL(value)) {
                             a = '<a href="' + value + '" target="_blank" class="easyui-linkbutton" style="border: none; text-decoration: none;" >查看</a> ';
                         }
                         else {
                             a = value;
                         }
                     }
                     return a;
                 }
             }
                  //, {
                  //    field: 'IsFrozen', title: '冻结', width: 77, formatter: function (value) {
                  //        if (value == true) {
                  //            return "是";
                  //        }
                  //        else {
                  //            return "否";
                  //        }
                  //    }
                  //}


        ]],
        onDblClickRow: function () {
            flexiModify();
        },
        pagination: true,
        rownumbers: true

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
    } else {

        //添加选择按钮
        $('#flexigridData').datagrid("addToolbarItem", [{ "text": "选择", "iconCls": "icon-ok", handler: function () { flexiSelect(); } }]);
    }
});



//清空
function RemvoeAll() {
    $("#ttSearch").find("a").each(function () {
        $(this).parent().children("a").removeClass("bgred");
        $("#hid_" + $(this).attr("rel")).val('0');
    });
    $('#Keyword').val('');
    $('#btnCityName').val('选择城市');
    $('#CityCode').val('');
    $('#CityName').val('');

    $('#btnVocationName').val('选择行业');
    $('#VocationName').val('');
    $('#VocationCode').val('');

    $("[type='radio'][name='IsFrozen']").removeAttr("checked");
    $("[type='radio'][name='IsFrozen'][value='']").attr("checked", true);

    $("#flexigridData").datagrid("clearSelections");

}



//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    if ($('#Keyword').val() != "") {
        search = search + 'Keyword' + "&" + $('#Keyword').val() + '^';
    }
    if ($("#CityCode").val() != "") {
        search = search + 'CityCode' + "&" + $('#CityCode').val() + '^';
    }
    if ($("#VocationCode").val() != "") {
        search = search + 'VocationCode' + "&" + $('#VocationCode').val() + '^';
    }


    var str = "";
    //客户级别
    $("input[name='CustomerLevel']:checked").each(function () {
        str += $(this).val() + ",";
    })
    if (str != "") {
        search = search + 'CustomerLevel' + "&" + str + '^';
    }
    str = "";
    //企业性质
    $("input[name='CompanyNatureCode']:checked").each(function () {
        str += $(this).val() + ",";
    })
    if (str != "") {
        search = search + 'CompanyNatureCode' + "&" + str + '^';
    }
    str = "";
    //客户进程
    $("input[name='CustomerFunnel']:checked").each(function () {
        str += $(this).val() + ",";
    })
    if (str != "") {
        search = search + 'CustomerFunnel' + "&" + str + '^';
    }
    str = "";
    //合同套餐
    $("input[name='Package']:checked").each(function () {
        str += $(this).val() + ",";
    })
    if (str != "") {
        search = search + 'Package' + "&" + str + '^';
    }
    str = "";
    //认证状态
    $("input[name='IsCertification']:checked").each(function () {
        if ($(this).val() == 'true') {
            str += "1";
        }
        else {
            str += "0";
        }

    })
    if (str != "" && str.length <= 1) {

        search = search + 'IsCertification' + "&" + str + '^';
    }

    //未跟进
    if ($("#NotFollow").is(':checked')) {
        search = search + 'NotFollow' + '&1^';
    }
    str = $("#SysPersonID").val();
    if (str != '' && str != undefined && str != null) {
        search = search + 'SysPersonID' + "&" + str + '^';
    }
    $("#flexigridData").datagrid("clearSelections");
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}