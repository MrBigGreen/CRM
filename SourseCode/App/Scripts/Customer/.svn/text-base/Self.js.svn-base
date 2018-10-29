$(function () {
    $('#flexigridData').datagrid({
        title: '<span class="Square" style="background-color: #E0FFFF;"></span>其他销售分享过来的客户  |  <span class="Square" style="background-color: #FFF0F5;"></span>分享给其他销售的客户  |  <lable class="red">&hearts;</lable>跟进未完成', //列表的标题
        iconCls: 'icon-site',
        width: '99%',
        height: '580px',
        striped: true,
        collapsible: true,
        singleSelect: false,
        url: '../Customer/GetCustomerSelfData', //获取数据的url
        idField: 'CustomerID',
        sortName: 'SortColumn',
        sortOrder: 'desc',
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
                 field: 'FollowUpDate', title: '跟进日期', width: 125,  formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
            , { field: 'GiveMe', title: '分享给我的客户', width: 80, hidden: true }
            , { field: 'ToPerson', title: '分享出去的客户', width: 80, hidden: true }
            , { field: 'SysPersonName', title: '归属人/跟进人', width: 90 }
            , {
                field: 'CustomerName', title: '客户名称', width: 180, styler: cellStyler, formatter: function (value, rowData) {
                    if (rowData.FollowCount > 0) {
                        //已有跟进的客户
                        return '<lable class="red">&hearts;</lable> <a href=\'javascript:window.parent.addTabUpdate("' + rowData.CustomerName + '", "/Customer/SelfDetails?ID=' + rowData.CustomerID + '", "tu1425", true);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
                    }
                    else {
                        return '<a href=\'javascript:window.parent.addTabUpdate("' + rowData.CustomerName + '", "/Customer/SelfDetails?ID=' + rowData.CustomerID + '", "tu1425", true);\'  class="easyui-linkbutton" style="border: none; text-decoration: none;"  >' + value + '</a> ';
                    }
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


function cellStyler(value, row, index) {
    if (row.GiveMe > 0) {
        //其他销售分享过来的客户
        return 'background-color:#E0FFFF;';
    }
    else if (row.ToPerson > 0) {
        //分享给其他销售
        return 'background-color:#FFF0F5;';
    }
}
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
    //if ($('#hid_dateType').val() == "0") {
    //    $('#flexigridData').datagrid('showColumn', 'BelongingDate');
    //    $('#flexigridData').datagrid('hideColumn', 'FollowUpDate');
    //}
    //else {
    //    $('#flexigridData').datagrid('showColumn', 'FollowUpDate');
    //    $('#flexigridData').datagrid('hideColumn', 'BelongingDate');
    //}
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
    //str = $("#SysPersonID").val();
    //if (str != '' && str != undefined && str != null) {
    //    search = search + 'SysPersonID' + "&" + str + '^';
    //}
    //未联系时长
    str = $("#NoContact").val();
    if (str != '' && str != undefined && str != null) {
        search = search + 'NoContact' + "&" + str + '^';
    }


    $('#flexigridData').datagrid('options').url = "../Customer/GetCustomerSelfData";
    
    $("#flexigridData").datagrid("clearSelections");
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}
//导航到修改的按钮
function flexiModify() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {
        window.parent.addTabUpdate(arr[0].CustomerName, "/Customer/SelfDetails?ID=" + arr[0].CustomerID, "tu1425", true);

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;

};

//选择要分享的人
function showModalShare(me, url, dialogWidth, callback) { //弹出窗体，多选   
    if (dialogWidth == null || dialogWidth == "undefined" || dialogWidth == "") {
        dialogWidth = 968;
    }
    var reValue = window.showModalDialog(url, window, "dialogHeight:500px; dialogWidth:" + dialogWidth + "px;  status:off; scroll:auto");

    if (reValue == null || reValue == "undefined" || reValue == "") {
        return; //如果返回值为空，就返回
    }
    var index = reValue.split("^"); //分割符 ^ 的位置
    if (index[0] == null || index[0] == "undefined" || index[0].length < 1) {
        return;
    }
    var hid = index[0].split('&'); //为隐藏控件赋值
    var view = index[1].split('&'); //显示值
    var content = ""; //需要添加到check中的内容

    for (var i = 0; i < hid.length - 1; i++) {
        if (hid[i] != "undefined" && hid[i] != "" && view[i + 1] != "undefined" && view[i + 1] != "") {

            var tableId = document.getElementById(hid[i]); //获取表格
            if (tableId == null) {
                content += '<tr><td style="width:151px"><span class="deleteStyle"><img id="img_' + hid[i] + '" title="点击删除" onclick="deleteShareTable(this);" alt="删除" src="../../../Images/deleteimge.png">';
                content += '<span>' + view[i + 1] + '</span><input type="hidden" id="' + hid[i] + '" name="sharePersonID" value="' + hid[i] + '" />';
                content += '</span></td><td style="width:251px"><label style="margin: 0px 10px 0px 0px;"><input name="' + hid[i] + '" type="radio" value="1" checked="checked">只读';
                content += '</label><label style="margin: 0px 10px 0px 0px;"><input name="' + hid[i] + '" type="radio" value="2">编辑</label></td></tr>';
            }
        }
    }

    var c = document.getElementById("check" + me);
    c.innerHTML += content;
    if (callback != null) {
        callback()
    }
}

//选择要分享的人
function showModalOnlyShare(me, url, dialogWidth, callback) { //弹出窗体，单选
    var table = document.getElementById("check" + me); //获取隐藏的控件
    
    if (table != null && table.rows.length >= 2) {
        alert("此处为单选，请先删除已有的选项，再次尝试选择。");
        return;
    }

    if (dialogWidth == null || dialogWidth == "undefined" || dialogWidth == "") {
        dialogWidth = 968;
    }
    var reValue = window.showModalDialog(url, window, "dialogHeight:500px; dialogWidth:" + dialogWidth + "px;  status:off; scroll:auto");

    if (reValue == null || reValue == "undefined" || reValue == "") {
        return; //如果返回值为空，就返回
    }
    var index = reValue.split("^"); //分割符 ^ 的位置
    if (index[0] == null || index[0] == "undefined" || index[0].length < 1) {
        return;
    }
    var hid = index[0].split('&'); //为隐藏控件赋值
    var view = index[1].split('&'); //显示值
    var content = ""; //需要添加到check中的内容
    
    if (hid.length != 2) {

        alert("请只选择一条数据。");
        return;
    }
    for (var i = 0; i < hid.length - 1; i++) {
        if (hid[i] != "undefined" && hid[i] != "" && view[i + 1] != "undefined" && view[i + 1] != "") {

            var tableId = document.getElementById(hid[i]); //获取表格
            if (tableId == null) {
                content += '<tr><td style="width:151px"><span class="deleteStyle"><img id="img_' + hid[i] + '" title="点击删除" onclick="deleteShareTable(this);" alt="删除" src="../../../Images/deleteimge.png">';
                content += '<span>' + view[i + 1] + '</span><input type="hidden" id="' + hid[i] + '" name="sharePersonID" value="' + hid[i] + '" />';
                content += '</span></td><td style="width:251px"><label style="margin: 0px 10px 0px 0px;"><input name="' + hid[i] + '" type="radio" value="1" checked="checked">只读';
                content += '</label><label style="margin: 0px 10px 0px 0px;"><input name="' + hid[i] + '" type="radio" value="2">编辑</label></td></tr>';
            }
        }
    }

    var c = document.getElementById("check" + me);
    c.innerHTML += content;
    if (callback != null) {
        callback()
    }
}


function deleteShareTable(share) {
    $(share).parent().parent().parent().remove();
}