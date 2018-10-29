$(function () {
    //行业
    $("#btnVocationName").TypeShow("single", 27, "industry", $("#btnVocationName").val());
    //地址
    $("#btnCityName").TypeShow("single", 27, "city", $("#btnCityName").val());

    $('#pp2').tooltip({
        position: 'bottom',
        content: '<div style="padding:5px;">客户归属人筛选，最多只能支持查询5人</div>',
        onShow: function () {
            $(this).tooltip('tip').css({
                backgroundColor: '#fff000',
                borderColor: '#ff0000',
                boxShadow: '1px 1px 3px #292929'
            });
        },
        onPosition: function () {
            $(this).tooltip('tip').css('left', $(this).offset().left);
            $(this).tooltip('arrow').css('left', 20);
        }
    });


    $('#flexigridData').datagrid({
        title: '客户列表', //列表的标题
        iconCls: 'icon-site',
        width: '99%',
        height: '580px',
        striped: true,
        collapsible: true,
        singleSelect: false,
        url: '../Customer/GetAllData', //获取数据的url
        idField: 'CustomerID',
        sortName: 'SortColumn',
        sortOrder: 'desc',
        queryParams: { "id": $("#SysPersonID").val() },
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 80 },
             {
                 field: 'BelongingDate', title: '创建日期', width: 125, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
             , {
                 field: 'FollowUpDate', title: '跟进日期', width: 125, hidden: true, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
             , { field: 'GiveMe', title: '分享给我的客户', width: 80, hidden: true }
             , { field: 'ToPerson', title: '分享出去的客户', width: 80, hidden: true }
            , { field: 'SysPersonName', title: '归属人/跟进人', width: 90 }
            , {
                field: 'CustomerName', title: '客户名称', width: 180, formatter: function (value, rowData) {

                    //var a = '<a href="/Customer/Details?ID=' + rowData.CustomerID + '"  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
                    var a = '<a href=\'javascript:window.parent.addTabUpdate("' + rowData.CustomerName + '", "/Customer/SelfDetails?ID=' + rowData.CustomerID + '", "tu1425", true);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
                    return a;
                }
            }
            , { field: 'RatingScore', title: '信用等级', width: 80 }
            , { field: 'CompanyNatureCode', title: '企业性质', width: 130 }
            , { field: 'CityName', title: '所在城市', width: 90 }
            , { field: 'CustomerLevel', title: '客户级别', width: 90 }
            , { field: 'CustomerFunnel', title: '客户进程', width: 90 }
            , { field: 'Opportunities', title: '商机判断', width: 77 }
            , { field: 'RecommendSolutionName', title: '推荐方案', width: 177 }
            , { field: 'ContactPerson', title: '联系人', width: 77 }
            , { field: 'ContactPhone', title: '手机号码', width: 120 }
            , { field: 'ContactTel', title: '固定电话', width: 120 }
            , { field: 'EmailAddress', title: '邮箱', width: 120 }
            //, {
            //    field: 'Contact', title: '联系方式', width: 77, formatter: function (value, rowData) {

            //        var a = '<a href="#"  class="easyui-linkbutton" style="border: none; text-decoration: none;"  OnClick="ContactShow(\'' + rowData.CustomerID + '\')">查看</a> ';
            //        return a;
            //    }
            //}
            , {
                field: 'IsRegister', title: '注册', width: 77, formatter: function (value) {
                    if (value == true) {
                        return "是";
                    }
                    else {
                        return "否";
                    }
                }
            }
            , {
                field: 'IsCertification', title: '认证', width: 77, formatter: function (value) {
                    if (value == true) {
                        return "是";
                    }
                    else {
                        return "否";
                    }
                }
            }
            , { field: 'SocialRecruitingQty', title: '社招职位', width: 77 }
            , { field: 'SchoolRecruitingQty', title: '校招职位', width: 77 }
            , {
                field: 'RelieveDate', title: '解除日期', width: 77, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd");
                    }
                }
            }

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
    $('#uType').addClass("DateSelect");
    $('#Keyword').val('');
    $('#btnCityName').val('选择城市');
    $('#CityCode').val('');
    $('#CityName').val('');

    $('#btnVocationName').val('选择行业');
    $('#VocationName').val('');
    $('#VocationCode').val('');
    $('#StartDate').html('');
    $('#EndDate').html('');
    $("[type='checkbox']").removeAttr("checked");
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
    //查询类别 0创建日期or 1跟进日期
    search = search + 'DateType' + "&" + $('#hid_dateType').val() + '^';
    if ($('#hid_dateType').val() == "0") {
        $('#flexigridData').datagrid('showColumn', 'BelongingDate');
        $('#flexigridData').datagrid('hideColumn', 'FollowUpDate');
    }
    else {
        $('#flexigridData').datagrid('showColumn', 'FollowUpDate');
        $('#flexigridData').datagrid('hideColumn', 'BelongingDate');
    }
    if ($("#StartDate").val() != "") {

        search = search + 'StartDate' + "&" + $('#StartDate').val() + '^';
    }
    if ($("#EndDate").val() != "") {
        search = search + 'EndDate' + "&" + $('#EndDate').val() + '^';
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
    //推荐方案
    $("input[name='RecommendSolutionID']:checked").each(function () {
        str += $(this).val() + ",";
    })
    if (str != "") {
        search = search + 'RecommendSolutionID' + "&" + str + '^';
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
    str = "";
    //职位状态
    $("input[name='ReleaseState']:checked").each(function () {
        str += $(this).val() + ",";
    })
    if (str != "") {
        search = search + 'ReleaseState' + "&" + str + '^';
    }
    //未跟进
    if ($("#NotFollow").is(':checked')) {
        search = search + 'NotFollow' + '&1^';
    }
    //分享过来
    if ($("#GiveMe").is(':checked')) {
        search = search + 'GiveMe' + '&1^';
    }
    //分享出去
    if ($("#ToPerson").is(':checked')) {
        search = search + 'ToPerson' + '&1^';
    }

    //var SysPersonId = $("#SysPersonId").val();
    //if (SysPersonId != "" && SysPersonId != undefined) {
    //    var arr = SysPersonId.split('^');
    //    SysPersonId = "";
    //    for (var i = 0; i < arr.length; i++) {
    //        if (arr[i].trim() != "") {
    //            SysPersonId += arr[i].split('&')[0] + ',';
    //        }
    //    }
    //    if (SysPersonId.length > 0) {

    //        search = search + 'SysPersonID' + "&" + SysPersonId.substring(0, SysPersonId.length - 1) + '^';
    //    }
    //}
    var SysPersonId = "";

    if ($("input[name='SysPersonIdList']").length > 0) {
        $("input[name='SysPersonIdList']:checked").each(function () {
            SysPersonId += $(this).val() + ',';
        })
    }
    else {
        //取得所有选中的节点，返回节点对象的集合
        //var iframe = $(window.frames["iframeDeptTree"].document).find("#myTree").tree("getChecked");
        //var iframe1 = $("#iframeDeptTree");
        //var nodes = $("#iframeDeptTree").find("#myTree").tree("getChecked");
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
    //未联系时长
    str = $("#NoContact").val();
    if (str != '' && str != undefined && str != null) {
        search = search + 'NoContact' + "&" + str + '^';
    }
    $("#flexigridData").datagrid("clearSelections");
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}



//导航到修改的按钮
function flexiModify() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {
        window.parent.addTabUpdate(arr[0].CustomerName, "/Customer/Details?ID=" + arr[0].CustomerID, "tu1425", true);
    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;

};