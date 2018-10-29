$(function () {
    $(".window-mask").live("click", function () {

        $(".easyui-dialog").dialog("close");
        $(".easyui-window").window("close");
    });
    /**
 * 扩展combox验证，easyui原始只验证select text的值，不支持value验证
    使用方式：validtype="comboxValidate['Province','请选择']"
 */
    $.extend($.fn.validatebox.defaults.rules, {
        comboxValidate: {
            validator: function (value, param, missingMessage) {
                if ($('#' + param).combobox('getValue') != '' && $('#' + param).combobox('getValue') != null) {
                    return true;
                }
                return false;
            },
            message: "{1}"
        }
    });
    //选择查询变色
    $("#ttSearch").find("a").each(function () {
        $(this).click(function () {
            if ($(this).attr("class") != "anUnderLine") {
                $(this).parent().children("a").removeClass("bgred");
                $(this).addClass("bgred");
                $("#hid_" + $(this).attr("rel")).val($(this).attr("data-value"));

            }
        })
    });

    $('#ContactPhone').tooltip({
        position: 'bottom',
        showEvent: 'click',
        content: '<div style="padding:5px;">请输入正确的电话号码  <br/> 如：0591-6487256 或 0591-6487256-123 或 15005059587</div>',
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
    $('#ContactTel').tooltip({
        position: 'bottom',
        showEvent: 'click',
        content: '<div style="padding:5px;">请输入正确的电话号码  <br/> 如：0591-6487256 或 0591-6487256-123 或 15005059587</div>',
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

    ///*******************************************批量设置跟进的客户**************************************///
    $('#FollowBatchData').datagrid({
        title: '批量设置跟进的客户', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        idField: 'CustomerID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        columns: [[
             { field: 'SysPersonName', title: '归属人/跟进人', width: 90 }
            , { field: 'CustomerName', title: '客户名称', width: 90 }
            , { field: 'CityName', title: '所在城市', width: 90 }
            , { field: 'CustomerLevel', title: '客户级别', width: 90 }
            , { field: 'CustomerFunnel', title: '客户进程', width: 90 }
            //, { field: 'Opportunities', title: '商机判断', width: 77 }
            //, { field: 'ContactPerson', title: '联系人', width: 77 }
            //, { field: 'ContactPhone', title: '联系电话', width: 120 }
            //, { field: 'Contact', title: '联系方式', width: 77 }
            //, { field: 'IsRegister', title: '注册', width: 77 }
            //, { field: 'IsCertification', title: '认证', width: 77 }
            //, { field: 'SocialRecruitingQty', title: '社招职位', width: 77 }
            //, { field: 'SchoolRecruitingQty', title: '校招职位', width: 77 }
            //, { field: 'RelieveDate', title: '解除日期', width: 77 }

        ]],
        pagination: false,//分页工具
        rownumbers: true

    });

    

});


//“选择”按钮，在其他（与此页面有关联）的页面中，此页面以弹出框的形式出现，选择页面中的数据
function flexiSelect() {

    var rows = $('#flexigridData').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }

    var arr = [];
    for (var i = 0; i < rows.length; i++) {
        arr.push(rows[i].CustomerID);
    }
    arr.push("^");
    for (var i = 0; i < rows.length; i++) {
        arr.push(rows[i].CustomerName);
    }

    //主键列和显示列之间用 ^ 分割   每一项用 , 分割
    if (arr.length > 0) {//一条数据和多于一条
        returnParent(arr.join("&")); //每一项用 & 分割
    }
}
//导航到查看详细的按钮
function getView() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {

        window.location.href = "/Customer/Details?ID=" + arr[0].CustomerID;

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;
}

//导航到创建的按钮
function flexiCreate() {
    $.ajax({
        url: "/Customer/IsCreate",
        type: "POST",
        datatype: "json",
        success: function (data) {
            if (data.Result == 1) {
                //window.location.href = "../Customer/Create";
                window.parent.addTabUpdate("新建客户", "/Customer/Create", "tu1425", true);
            }
            else {
                $.messager.alert('操作提示', '您没有创建客户权限或超出创建客户数!', 'warning');
            }
        },
        error: function () {
            $.messager.alert('操作提示', '数据异常，请刷新重试!', 'warning');

        }

    });
    return false;
}

//删除的按钮
function flexiDelete() {

    var arr = $('#flexigridData').datagrid('getSelections');
    if (arr.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    if (arr.length == 1) {
        var ID = arr[0].CustomerID;
        $.messager.confirm('操作提示', "确认删除：“ " + arr[0].CustomerName + " ”吗？", function (r) {
            if (r) {
                $.post("../Resume/Delete", { query: arr.join(",") }, function (res) {
                    if (res == "OK") {
                        //移除删除的数据

                        $.messager.alert('操作提示', '删除成功!', 'info');
                        $("#flexigridData").datagrid("reload");
                        $("#flexigridData").datagrid("clearSelections");
                    }
                    else {
                        if (res == "") {
                            $.messager.alert('操作提示', '删除失败!请查看该数据与其他模块下的信息的关联，或联系管理员。', 'info');
                        }
                        else {
                            $.messager.alert('操作提示', res, 'info');
                        }
                    }
                });
            }
        });

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }



};

//////*******************************************跟进记录************************************************/
function FollowRecord() {
    $("#FollowSet").find(".panel.datagrid").hide();
    var arr = $('#flexigridData').datagrid('getSelections');
    if (arr.length == 1) {
        $('#FollowSet').find("iframe").attr("src", "/CustomerFollow/FollowBackHistory?CustomerID=" + arr[0].CustomerID + "&page=1");
        //$('#FollowSet').dialog({
        //    title: '客户跟进记录',
        //    width: 880,
        //    height: 600,
        //    closed: false,
        //    cache: false,
        //    resizable: true,
        //    modal: true
        //});
        var win = window.top.$("<div class='easyui-dialog' style='height:100%;width:100%'  >" + $("#FollowSet").html() + "</div>").appendTo(window.top.document.body);
        win.window({
            title: "客户跟进记录",
            closed: false,
            cache: false,
            resizable: true,
            modal: true,
            top: 60,
            width: 880,
            height: 600,
            onClose: function () {
                win.window('destroy');//关闭即销毁
            }
        });
    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;
}

function FollowSet() {
    $('#FollowBatchData').datagrid('loadData', { total: 0, rows: [] });//清空下方DateGrid
    $("#FollowSet").find(".panel.datagrid").hide();

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    else if (arr.length == 1) {
        //检查是否有跟进未联系
        $.ajax({
            url: "/CustomerFollow/IsContact",
            type: "POST",
            data: { id: arr[0].CustomerID },
            datatype: "json",
            success: function (data) {
                if (data.state == 0) {
                    $.messager.alert('操作提示', '已有跟进未完成!', 'warning');
                }
                else {
                    $('#FollowSet').find("iframe").attr("src", "/CustomerFollow/CreateTask?ID=" + arr[0].CustomerID);
                    $('#FollowSet').window({
                        title: '客户跟进设置',
                        //href: "/CustomerFollow/Create",
                        width: 780,
                        height: 600,
                        closed: false,
                        cache: false,
                        resizable: true,
                        modal: true,
                        top: 60,
                        left: 60,
                    });

                    //var win = window.top.$("<div> " + $("#FollowSet").html() + "</div>").appendTo(window.top.document.body);
                    //win.window({
                    //    title: "客户跟进设置",
                    //    closed: false,
                    //    cache: false,
                    //    resizable: true,
                    //    modal: true,
                    //    top: 60,
                    //    width: 780,
                    //    height: 600,
                    //    onClose: function () {
                    //        win.window('destroy');//关闭即销毁
                    //    }
                    //});

                }
            }
        });
    } else if (arr.length > 1) {

        var ids = "";
        for (var m = 0; m <= arr.length - 1; m++) {
            $.ajax({
                url: "/CustomerFollow/IsContact",
                type: "POST",
                data: { id: arr[m].CustomerID },
                datatype: "json",
                async: false,
                success: function (data) {
                    if (data.state == 1) {
                        ids += arr[m].CustomerID + ",";
                        $('#FollowBatchData').datagrid("appendRow", {
                            CustomerID: arr[m].CustomerID,
                            SysPersonName: arr[m].SysPersonName,
                            CustomerName: arr[m].CustomerName,
                            CityName: arr[m].CityName,
                            CustomerLevel: arr[m].CustomerLevel,
                            CustomerFunnel: arr[m].CustomerFunnel,
                            Opportunities: arr[m].Opportunities,
                            ContactPerson: arr[m].ContactPerson,
                            ContactPhone: arr[m].ContactPhone,
                            Contact: arr[m].Contact,
                            IsRegister: arr[m].IsRegister,
                            IsCertification: arr[m].IsCertification
                        });
                    }
                }
            });

        }
        $("#FollowSet").find(".panel.datagrid").show();

        $('#followSet_iframe').attr("src", "/CustomerFollow/BatchCreate?ID=" + ids);
        $('#FollowSet').window({
            title: '批量设置客户跟进',
            width: 780,
            height: 600,
            closed: false,
            cache: false,
            resizable: true,
            modal: true,
            top: 60,
            left: 60,
        });

        //var win = window.top.$(sss.html()).appendTo(window.top.document.body);
        //var win = window.top.$("<div  class='easyui-dialog' style='width: 600px; height: 560px;'>" + sss.html() + "</div>").appendTo(window.top.document.body);
        //win.window({
        //    title: "客户跟进设置",
        //    closed: false,
        //    cache: false,
        //    resizable: true,
        //    modal: true,
        //    top: 60,
        //    width: 780,
        //    height: 600,
        //    onClose: function () {
        //        win.window('destroy');//关闭即销毁
        //    }
        //});

    }
    return false;
}


//////*******************************************查看联系人************************************************/
function ContactShow(id) {

    $('#ContactShow').find("iframe").attr("src", "/CustomerContact/ViewList?ID=" + id);
    $('#ContactShow').dialog({
        title: '查看联系人',
        width: 880,
        height: 450,
        closed: false,
        cache: false,
        resizable: true,
        modal: true,
        //href: "/CustomerContact/ViewList?ID=" + id
    });

    return false;
}

//////*******************************************查看客户基本信息************************************************/
function CustomerShow(id) {
    $('#CustomerShow').find("iframe").attr("src", "/Customer/SelfDetails?ID=" + id);
    $('#CustomerShow').dialog({
        title: '客户信息',
        width: 992,
        height: 616,
        closed: false,
        cache: false,
        resizable: true,
        modal: true,
        left: 30,
        top: 30,
        //toolbar: [{
        //    text: '编辑',
        //    iconCls: 'icon-edit',
        //    handler: function () {
        //        window.location.href = '/Customer/SelfDetails?ID=' + id;
        //    }
        //}],
    });
    return false;
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
        //if (rows[i].GiveMe > 0) {
        //    $.messager.alert('操作提示', '只能释放自己客户!', 'error');
        //    continue;
        //}
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

//////*******************************************提取客户************************************************/
function Extraction() {
    var rows = $('#flexigridData').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('操作提示', '请至少选择一条数据!', 'warning');
        return false;
    }
    var ids = "";
    for (var i = 0; i < rows.length; i++) {
        ids += rows[i].CustomerID + ",";
    }
    $.ajax({
        url: "/Customer/Extraction",
        type: "POST",
        data: { IDs: ids },
        success: function (data) {
            if (data.Result == 1) {
                //刷新整个表格
                $('#flexigridData').datagrid('unselectAll');
                $('#flexigridData').datagrid('reload');
                $.messager.alert('操作提示', '客户提取成功。', 'info');

            }
            else {
                $.messager.alert('操作提示', '客户提取失败，错误：' + data.Msg, 'info');
            }
        },
        error: function () {
            $.messager.alert('操作提示', '客户提取失败，网络异常！', 'info');
        }

    });
}


//////*******************************************修改客户名称************************************************/
function ShowUpdateCustomerName() {
    var rows = $('#flexigridData').datagrid('getSelections');
    if (rows.length == 1) {
        $("#OldCustomerName").val(rows[0].CustomerName);
        $("#hidCustomerID").val(rows[0].CustomerID);
        $("#NewCustomerName").val('');

        $('#divUpdateCustomerName').dialog({
            title: '修改客户名称',
            width: 780,
            height: 600,
            closed: false,
            cache: false,
            resizable: true,
            modal: true
        });


    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;

}
function GetUpdateCustomerName() {
    var newCustomerName = $("#NewCustomerName").val().trim();
    if (newCustomerName.length < 3) {
        $.messager.alert('操作提示', '客户名称长度不能小于三位。', 'error');
        return false;
    }
    var OldCustomerName = $("#OldCustomerName").val();
    if (newCustomerName == OldCustomerName) {
        $.messager.alert('操作提示', '新客户名称和原客户名称一样。', 'error');
        return false;
    }
    reg = /^([\u4e00-\u9fa5]*[(（]?[\u4e00-\u9fa5]+[)）]?[\u4e00-\u9fa5]*)$/
    if (!reg.test(newCustomerName)) {
        $.messager.alert('操作提示', '客户名称格式有误。', 'error');
        return false;
    }



    $.messager.confirm('提示', '您确定把原“' + $("#OldCustomerName").val() + '”改成“' + newCustomerName + '”吗?', function (r) {
        if (r) {
            $.ajax({
                url: "/Customer/GetUpdateCustomerName",
                type: "POST",
                data: { CustomerID: $("#hidCustomerID").val(), CustomerName: newCustomerName },
                success: function (data) {
                    if (data.Result == 1) {
                        //刷新整个表格
                        $.messager.alert('操作提示', '修改成功。', 'info');
                        $('#flexigridData').datagrid('reload');
                    }
                    else {
                        $.messager.alert('操作提示', '修改失败，' + data.Msg, 'info');
                    }
                },
                error: function () {
                    $.messager.alert('操作提示', '修改失败，网络异常！', 'info');
                }
            });
        }
    });
}




//////*******************************************注销客户************************************************/
function Logoff() {
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
    $.messager.confirm('提示', '您确定要注销“' + names + '”客户吗?', function (r) {
        if (r) {
            $.ajax({
                url: "/Customer/GetLogoff",
                type: "POST",
                data: { IDs: ids },
                success: function (data) {
                    if (data.Result == 1) {
                        //刷新整个表格
                        $('#flexigridData').datagrid('unselectAll');
                        $('#flexigridData').datagrid('reload');
                        $.messager.alert('操作提示', '客户注销成功。', 'info');
                    }
                    else {
                        $.messager.alert('操作提示', '客户注销失败，错误：' + data.Msg, 'info');
                    }
                },
                error: function () {
                    $.messager.alert('操作提示', '客户注销失败，网络异常！', 'info');
                }

            });
        }
    });
}


//////*******************************************激活客户************************************************/
function Activating() {
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
    $.messager.confirm('提示', '您确定要激活“' + names + '”客户吗?', function (r) {
        if (r) {
            $.ajax({
                url: "/Customer/GetActivating",
                type: "POST",
                data: { IDs: ids },
                success: function (data) {
                    if (data.Result == 1) {
                        //刷新整个表格
                        $('#flexigridData').datagrid('unselectAll');
                        $('#flexigridData').datagrid('reload');
                        $.messager.alert('操作提示', '客户激活成功。', 'info');
                    }
                    else {
                        $.messager.alert('操作提示', '客户激活失败，错误：' + data.Msg, 'info');
                    }
                },
                error: function () {
                    $.messager.alert('操作提示', '客户激活失败，网络异常！', 'info');
                }

            });
        }
    });
}
