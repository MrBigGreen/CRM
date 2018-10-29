$(function () {
    //行业
    $("#btnJobType").TypeShow("single", 27, "industry", $("#btnJobType").val());
    //地址
    $("#btnCityName").TypeShow("single", 27, "city", $("#btnCityName").val());


    $('#flexigridData').datagrid({
        title: '客户列表', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: false,
        url: '/JobList/GetAllData', //获取数据的url
        idField: 'CompanyInterviewJobID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 80 },
              {
                  field: 'JobTitle', title: '职位名称', width: 177, formatter: function (value, rowData) {

                      var a = '<a href="http://www.9191offer.com/VissCommDetail/PositionDetail?CompanyInterviewJobID=' + rowData.CompanyInterviewJobID + '"  class="easyui-linkbutton" style="border: none; text-decoration: none;" target="_blank">' + value + '</a> ';
                      return a;
                  }
              }
             , {
                 field: 'CreatedTime', title: '创建时间', width: 125, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
            , { field: 'JobCheckStatusText', title: '审核结果', width: 90 }
            , { field: 'WorkExperenceText', title: '工作年限', width: 90 }
            , { field: 'WorkAddress', title: '工作地点', width: 120 }
             , { field: 'CustomerID', title: '客户编号', width: 90, hidden: true }
            , {
                field: 'CompanyName', title: '公司名称', width: 190, formatter: function (value, rowData) {
                    return "<a href='javascript:void(0)' onclick=ShowCustomer(" + rowData.CustomerID + ")>" + value + "</a>";

                }
            }
            , { field: 'CustomerLevel', title: '客户级别', width: 90 }
            , { field: 'CustomerBelongingName', title: '客户归属', width: 77 }
            , { field: 'ReceiveQty', title: '收到简历', width: 77 }
            , { field: 'RecommendQty', title: '推荐简历', width: 77 }
            //, { field: 'RecommendQty', title: '面试', width: 77 }
            //, { field: 'RecommendQty', title: 'offer', width: 77 }


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
    $('#Keyword').val('');
    $('#btnCityName').val('选择城市');
    $('#CityCode').val('');
    $('#CityName').val('');

    $('#btnJobType').val('选择行业');
    $('#JobTypeCode').val('');
    $('#JobTypeName').val('');
    $('#StartDate').html('');
    $('#EndDate').html('');

    $("[type='checkbox']").removeAttr("checked");

    $("#WorkExperenceCode").find("option[value='']").attr("selected", true);
    $("#JobCheckStatusCode").find("option[value='']").attr("selected", true);
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
    if ($("#JobTypeCode").val() != "") {
        search = search + 'JobTypeCode' + "&" + $('#JobTypeCode').val() + '^';
    }
    if ($("#WorkExperenceCode").val() != "") {
        search = search + 'WorkExperenceCode' + "&" + $('#WorkExperenceCode').val() + '^';
    }
    if ($("#JobCheckStatusCode").val() != "") {
        search = search + 'JobCheckStatusCode' + "&" + $('#JobCheckStatusCode').val() + '^';
    }
    if ($("#StartDate").val() != "") {
        search = search + 'StartDate' + "&" + $('#StartDate').val() + '^';
    }
    if ($("#EndDate").val() != "") {
        search = search + 'EndDate' + "&" + $('#EndDate').val() + '^';
    }

    var SysPersonId = $("#SysPersonId").val();
    if (SysPersonId != "" && SysPersonId != undefined) {
        var arr = SysPersonId.split('^');
        SysPersonId = "";
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].trim() != "") {
                SysPersonId += arr[i].split('&')[0] + ',';
            }
        }
        if (SysPersonId.length > 0) {

            search = search + 'SysPersonID' + "&" + SysPersonId.substring(0, SysPersonId.length - 1) + '^';
        }
    }
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}


//////*******************************************查看客户资料************************************************/
function ShowCustomer(id) {
    if (id == "" || id == null || id != undefined) {

        $.messager.alert('操作提示', '没找到客户信息，可能未关联!', 'warning');
        return false;
    }
    else {
        $('.easyui-dialog').find("iframe").attr("src", "/Customer/SelfDetails?ID=" + id);
        $('.easyui-dialog').dialog({
            title: '公司详情',
            width: 1100,
            height: 780,
            closed: false,
            cache: false,
            resizable: true,
            modal: true,
            top: 60,
            left: 70,
        });
    }
    return false;
}