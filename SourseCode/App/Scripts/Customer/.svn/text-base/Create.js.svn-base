
$(function () {
    $('.easyui-combobox').combobox({
        width: 243,
    });
    $("form").submit(function (form) {

        if ($(this).form('validate')) {
            if (form.result) {
                ajaxFrom(this, this.action);
            }
        }
        return false;
    });
    //按钮样式
    $('.a2').mouseover(function () { this.style.color = "#ae1121"; }).mouseout(function () { this.style.color = "#333"; });

});

$(function () {
    //$('#ContactTel').attr("class", "input-validation-error");
    $('#ContactTel').attr("data-val", "true");
    $('#ContactTel').attr("data-val-regex-pattern", "^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$");
    $('#ContactTel').attr("data-val-regex", "电话号码格式有误。");

    //$('#ContactPhone').attr("class", "input-validation-error");
    $('#ContactPhone').attr("data-val", "true");
    $('#ContactPhone').attr("data-val-regex-pattern", "^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$");
    $('#ContactPhone').attr("data-val-regex", "手机号码格式有误。");




    BindProvinceCity();

    $("#btnJobIndustryName").TypeShow("single", 27, "industry", $("#btnJobIndustryName").val());//行业
    $("#CustomerName").blur(function () {
        $("#CustomerName").val($("#CustomerName").val().trim());
    });
    $("#CustomerName").change(function () {
        $("#CustomerName").val($("#CustomerName").val().trim());
    });

    $('#RepeatData').datagrid({
        title: '', //列表的标题
        iconCls: 'icon-edit',
        width: 'auto',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        //onClickRow: onClickRow,
        //url: '../Customer/GetAllData', //获取数据的url
        idField: 'CustomerID',
        sortName: 'CustomerID',
        sortOrder: 'desc',
        columns: [[
             { field: 'CustomerID', title: '客户编号', width: 195, hidden: true }
            , { field: 'CustomerName', title: '客户名称', width: 185 }

            , { field: 'MyName', title: '归属人', width: 125 }
            , { field: 'EmailAddress', title: '归属人邮箱', width: 195 }
            , { field: 'ProvinceName', title: '省份', width: 100 }
            , { field: 'CityName', title: '城市', width: 100 }
              , {
                  field: 'IsDelete', title: '状态', width: 100, formatter: function (value, rows) {
                      debugger;
                      if (value == true) {
                          return "无效";
                      }
                      else if (rows.IsFrozen == true)
                      {
                          return "公共池";
                      }
                      else if (rows.AuditStatus == 2) {
                          return "待审核";
                      }
                      else if (rows.AuditStatus == 3) {
                          return "审核失败";
                      }
                      else {
                          return "正常";
                      }
                  }
              }
            //, { field: 'VerificationCode', title: '验证码', width: 125, editor: 'text' }
        ]],
        //toolbar: [
        //    { text: '编辑完成', iconCls: 'icon-save', handler: function () { accept(); } }

        //]
    });
});


function ajaxFrom(form, url) {

    var isOK = true;
    var vocationName = $("#VocationCode").val();


    var comments = $("#Comments").val();
    if (comments == "") {
        $("#Comments").removeAttr("class").addClass("input-validation-error");
        isOK = false;
    }
    if ($("#ContactName").val() == "") {
        $("#ContactName").removeAttr("class").addClass("input-validation-error");
        isOK = false;
    }
    if ($("#Post").val() == "") {
        $("#Post").removeAttr("class").addClass("input-validation-error");
        isOK = false;
    }

    if ($("#ContactPhone").val() == "" && $("#ContactTel").val() == "") {

        $("#ContactPhone").removeAttr("class").addClass("input-validation-error");
        $("#ContactTel").removeAttr("class").addClass("input-validation-error");
        isOK = false;
        $.messager.alert('操作提示', '请至少输入填写一个联系方式!', 'warning');
    }
    else {
        var param = /^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$/;
        //先验证联系电话
        if ($("#ContactPhone").val() != "") {
            if (!$("#ContactPhone").val().match(param)) {
                $('#ContactPhone').attr("class", "text-box single-line tooltip-f input-validation-error");
                isOK = false;
                $.messager.alert('操作提示', '手机号码格式不正确!', 'warning');
            }
        }
        if ($("#ContactTel").val() != "") {
            if (!$("#ContactTel").val().match(param)) {
                $('#ContactTel').attr("class", "text-box single-line tooltip-f input-validation-error");
                isOK = false;
                $.messager.alert('操作提示', '电话号码格式不正确!', 'warning');
            }
        }
    }

    if (!isOK) {
        return false;
    }
    var CompanyNatureCode = $("#CompanyNatureCode").val();
    if (CompanyNatureCode == "") {
        $.messager.alert('操作提示', '请选择企业性质！', 'warning');
        return false;
    }

    var RegisterCapital = $("#RegisterCapital").val();
    if (RegisterCapital == "") {
        $.messager.alert('操作提示', '请选择注册资金！', 'warning');
        return false;
    }

    var SalesRevenue = $("#SalesRevenue").val();
    if (SalesRevenue == "") {
        $.messager.alert('操作提示', '请选择销售收入！', 'warning');
        return false;
    }
    if (vocationName == "") {
        $.messager.alert('操作提示', '请选择所属行业！', 'warning');
        return false;
    } 

    var CompanySize = $("#CompanySize").val();
    if (CompanySize == "") {
        $.messager.alert('操作提示', '请选择公司规模！', 'warning');
        return false;
    }

    var flag = checkSubmit();
    if (flag) {
        $.ajax({
            url: url,
            type: "Post",
            data: $(form).serialize(),
            dataType: "json",
            success: function (data) {
                if (data.Result == 1) {
                    $.messager.alert('操作提示', "创建成功！", "info", function (r) {
                        window.parent.removePanel("新建客户");
                    });
                    $(".easyui-linkbutton").removeAttr("disabled");
                }
                else {
                    $.messager.alert('操作提示', data.returnValue, "info", function (r) {
                        //window.location.href = 'javascript:history.go(-1)';
                    });
                    $(".easyui-linkbutton").removeAttr("disabled");
                }

            }
        });
    }
}


/*************************************** //绑定省份、城市、行政区信息*************************************************/
function BindProvinceCity() {

    var province = $('#Province').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        url: '/Customer/GetAllProvince',
        onChange: function (newValue, oldValue) {
            console.log("onChange");
            $.get('/Customer/GetCitysByProvinceName', { provinceName: newValue }, function (data) {
                city.combobox("clear").combobox('loadData', data);
                $("#City").combobox("setText", "--请选择城市--");
                $("#CityCode").val('');
                $("#CityName").val('');
                $("#DistrictCode").val('');
                $("#DistrictName").val('');
                district.combobox("clear")
            }, 'json');
         
        }, onSelect: function (newValue) {
            console.log("onSelect");
            //改变省份
            $("#ProvinceCode").val(newValue.Value);
            $("#ProvinceName").val(newValue.Text);
           
        }, onLoadSuccess: function () {
            $("#Province").combobox("setText", "--请选择省份--");
        }
    });

    var city = $('#City').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        onChange: function (newValue, oldValue) {
            $.get('/Customer/GetDistrictByCityName', { cityName: newValue }, function (data) {
                district.combobox("clear").combobox('loadData', data);                
                $("#District").combobox("setText", "--请选择城区--");
                $("#DistrictCode").val('');
                $("#DistrictName").val('');
            }, 'json');
           
        },
        onSelect: function (newValue, oldValue) {
            //改变城市
            $("#CityCode").val(newValue.Value);
            $("#CityName").val(newValue.Text);
        }
    });

    var district = $('#District').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        onSelect: function (newValue, oldValue) {
            $("#DistrictCode").val(newValue.Value);
            $("#DistrictName").val(newValue.Text);
        }
    });
}

/********************************************创建客户*******************************************/
function create(isConfirm) {
    $("#VerificationCode").val('');
    var isOK = true;
    
    if ($("form").valid()) {

        var vocationName = $("#VocationName").val();
        if (vocationName == "") {
            $.messager.alert('操作提示', '请选择行业类别！', 'warning');
            return false;
        }
        //公司简介
        var comments = $("#Comments").val();
        if (comments == "") {
            $("#Comments").removeAttr("class").addClass("input-validation-error");
            isOK = false;
        }
        //联系人
        if ($("#ContactName").val() == "") {
            $("#ContactName").removeAttr("class").addClass("text-box single-line tooltip-f input-validation-error");
            isOK = false;
        }
        //职位
        if ($("#Post").val() == "") {
            $("#Post").removeAttr("class").addClass("text-box single-line tooltip-f input-validation-error");
            isOK = false;
        }
        //联系方式
        if ($("#ContactPhone").val() == "" && $("#ContactTel").val() == "") {

            $("#ContactPhone").removeAttr("class").addClass("text-box single-line tooltip-f input-validation-error");
            $("#ContactTel").removeAttr("class").addClass("text-box single-line tooltip-f input-validation-error");
            isOK = false;
            $.messager.alert('操作提示', '请至少输入填写一个联系方式!', 'warning');
        }
        else {
            var param = /^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$/;
            //先验证联系电话
            if ($("#ContactPhone").val() != "") {
                if (!$("#ContactPhone").val().match(param)) {
                    $('#ContactPhone').attr("class", "text-box single-line tooltip-f input-validation-error");
                    isOK = false;
                    $.messager.alert('操作提示', '手机号码格式不正确!', 'warning');
                }
            }
            if ($("#ContactTel").val() != "") {
                if (!$("#ContactTel").val().match(param)) {
                    $('#ContactTel').attr("class", "text-box single-line tooltip-f input-validation-error");
                    isOK = false;
                    $.messager.alert('操作提示', '电话号码格式不正确!', 'warning');
                }
            }
        }

        if (!isOK) {
            return false;
        }
        $("#btnCustomerCreate").attr("disabled", "disabled");
        if (!isConfirm) {
            var flag = getCustomerRepeat();
            if (flag == 0) {
                console.log("可以创建");
            }
            else if (flag > 0) {
                $("#btnConfirmCreate").show();
                return false;
            }
            else if (flag == -1) {
                $.messager.alert('操作提示', '客户名称已被其他人创建过，您不能重复创建!', 'warning');
                return false;
            }
            else if (flag == -2) {
                $.messager.alert('操作提示', '您已经创建过此客户!', 'warning');
                return false;
            }
            else {
                return false;
            }
        }
        
        $.ajax({
            url: "/Customer/Create",
            type: "Post",
            data: $("form").serialize(),
            dataType: "json",
            success: function (data) {
                $(".easyui-linkbutton").removeAttr("disabled");

                if (data.Result == 1) {
                    $.messager.alert('操作提示', "创建成功！", "info", function (r) {
                        window.parent.removePanel("新建客户");
                    });
                }
                else {
                    $.messager.alert('操作提示', data.returnValue, "info", function (r) {
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest.status);
                console.log(XMLHttpRequest.readyState);
                console.log(textStatus);
                $(".easyui-linkbutton").removeAttr("disabled");
            }
        });
    }
}

/********************************************客户查重验证*******************************************/
function checkRepeat() {

    var flag = getCustomerRepeat();
    if (flag == 0) {
        $.messager.alert('操作提示', '客户可以创建！', 'info');
    }
    if (flag == -1) {
        $.messager.alert('操作提示', '客户名称已被其他人创建过，您不能重复创建!', 'warning');
        return false;
    }
    else if (flag == -2) {
        $.messager.alert('操作提示', '您已经创建过此客户!', 'warning');
        return false;
    }

    return flag;
}
function getCustomerRepeat() {
    $("#btnConfirmCreate").hide();
    var flag = -99;
    var customerName = $("#CustomerName").val();
    if (customerName == "") {
        $.messager.alert('操作提示', '客户名称不能为空!', 'warning');
        return -100;
    }
    if (customerName.length <= 4) {
        $.messager.alert('操作提示', '客户名称格式有误，请输入正确的公司名称', 'warning');
        return -100;
    }
    //检查是否有相似
    $.ajax({
        url: "/Customer/CustomerRepeat",
        type: "POST",
        data: { CustomerName: customerName, CustomerID: "" },
        datatype: "json",
        async: false,
        success: function (data) {
            if (data.Result == 0) {
                //可以创建
                flag = data.Result;
                //$.messager.alert('操作提示', '客户可以创建！', 'info');
            }
            else if (data.Result == -1) {
                //不能创建
                //$.messager.alert('操作提示', '客户名称已被其他人创建过，您不能重复创建!', 'warning');
                flag = data.Result;
            }
            else if (data.Result == -2) {
                //不能创建
                //$.messager.alert('操作提示', '您已经创建过此客户!', 'warning');
                flag = data.Result;
            }
            else if (data.Result > 0) {
                $('#RepeatData').datagrid('loadData', { total: 0, rows: [] });//清空下方DateGrid 
                //需要验证
                $('#RepeatData').datagrid('loadData', data.Data);
                $('#repeatDialog').dialog({
                    title: '客户查重验证',
                    width: 980,
                    height: 450,
                    closed: false,
                    cache: false,
                    resizable: true,
                    modal: true,
                    shadow: true,
                    top: 65,
                    left: 65
                });
                //$.messager.alert('操作提示', '数据异常!', 'warning');                
                flag = data.Result;
            }
        }
    });
    console.log(flag);
    return flag;
}
/********************************************发送验证码给相关归属人*******************************************/
function SendVerification() {

    $.ajax({
        url: "/Customer/SendVerification",
        type: "POST",
        data: { CustomerName: $("#CustomerName").val() },
        datatype: "json",
        success: function (data) {
            if (data.Result == 1) {
                $.messager.alert('操作提示', '发送成功，请联系相关客户归属人!', 'info');
            }
            else {
                $.messager.alert('操作提示', '发送失败，' + data.ErrorMsg + '!', 'info');

            }
        }
    });
}

/********************************************提交验证创建客户*******************************************/
function Submit() {
    //accept();
    //var rows = $("#RepeatData").datagrid("getRows");
    //var demo = "";
    //for (var i = 0; i < rows.length; i++) {
    //    //获取每一行的数据
    //    demo += rows[i].CustomerID + "&" + rows[i].VerificationCode + "^";
    //}
    $("#VerificationCode").val("0");
    $(".easyui-window").window("close");
    $(".easyui-dialog").dialog("close");
    $("#submit").click();
}



//var editIndex = undefined;
//function endEditing() {
//    if (editIndex == undefined) { return true }
//    if ($('#RepeatData').datagrid('validateRow', editIndex)) {
//        $('#RepeatData').datagrid('endEdit', editIndex);
//        editIndex = undefined;
//        return true;
//    } else {
//        return false;
//    }
//}
//function onClickRow(index) {
//    if (editIndex != index) {
//        if (endEditing()) {
//            $('#RepeatData').datagrid('selectRow', index)
//                    .datagrid('beginEdit', index);
//            editIndex = index;
//        } else {
//            $('#RepeatData').datagrid('selectRow', editIndex);
//        }
//    }
//}
//保存
//function accept() {
//    if (endEditing()) {
//        $('#RepeatData').datagrid('acceptChanges');
//    }
//}
