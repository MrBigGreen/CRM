

$(function () {
    ///获取最大合同编号
    $("input[type=checkbox]").change(function () {

        var ids = "";
        $("input[name='RecommendSolutionID']:checked").each(function () {
            ids = $(this).val();

            $.ajax({
                url: "/Contract/GetMaxContractNO",
                type: "POST",
                data: { SolutionID: ids },
                success: function (data) {
                    if (data.Result == 1) {
                        $("#ContractNO").val(data.MaxContractNO);
                    }
                    else {
                        $.messager.alert('操作提示', '获取合同编号失败，错误：' + data.Msg, 'info');
                    }
                },
                error: function () {
                    $.messager.alert('操作提示', '获取合同编号失败！', 'info');
                }

            });


        });



    });


});
$(function () {

    //城市选择
    //  $("#btnCityName").TypeShow("single", 27, "city", $("#btnCityName").val());//地址

    $('#EffectiveDate').datebox('calendar').calendar({
        validator: function (date) {
            var now = new Date();
            var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
            var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 10);
            return d1 <= date;
        }
    });

    $('#Deadline').datebox('calendar').calendar({
        validator: function (date) {
            var now = new Date();
            var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
            var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 10);
            return d1 <= date;
        }
    });

    $("#EffectiveDate").datebox({
        onSelect: function () {
            if ($("#Deadline").datebox('getValue') != "") {
                if (toDate($("#EffectiveDate").datebox('getValue')) > toDate($("#Deadline").datebox('getValue'))) {
                    $.messager.alert('提示', '开始日期不得晚于结束日期！', "info");
                    $("#EffectiveDate").datebox("setValue", "");
                    return false;
                };
            }
            //$(".datebox> a").css("background", "none");
        }
    });

    $("#Deadline").datebox({
        onSelect: function () {
            if ($("#EffectiveDate").datebox('getValue') != "") {
                if (toDate($("#Deadline").datebox('getValue')) < toDate($("#EffectiveDate").datebox('getValue'))) {
                    $.messager.alert('提示', '结束日期不得早于开始日期！', "info");
                    $("#Deadline").datebox("setValue", "");
                    return false;
                }
            }

        }
    });




    //选择查询变色
    //$("#ttPackage").find("a").eq(0).addClass("bgred");
    //$("#hid_userPackages").val($("#ttPackage").find("a").eq(0).attr("data-value"));
    //$("#ttPackage").find("a").each(function () {
    //    $(this).click(function () {

    //        $(this).parent().children("a").removeClass("bgred");
    //        $(this).addClass("bgred");
    //        $("#hid_" + $(this).attr("rel")).val($(this).attr("data-value"));

    //    })
    //});

    $('#company').datagrid({
        //  title: '温馨提示：深蓝色背景数据为往日未呼任务！~', //列表的标题
        iconCls: 'icon-site',
        width: 'auto',
        height: 'auto',

        //nowrap: false,
        striped: true,
        //collapsible: true,
        url: '../Contract/GetCompanyNameByCompanyName', //获取数据的url

        idField: 'CustomerID',
        fitColumns: true,
        singleSelect: true,
        columns: [[
              {
                  field: 'CustomerName', title: '公司名称', width: $(this).width() * 0.2, align: 'center', resizable: false,
                  formatter: function (value, rowData, rowIndex) {
                      var a = '<a href="#"  class="easyui-linkbutton" style="border: none; text-decoration: none;"  OnClick="OpenModalDialog(\'' + rowData.CustomerID + '\',0)">' + rowData.CustomerName + '</a> ';
                      return a;
                  }
              }

            , { field: 'CityCode', title: '城市ID', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false, hidden: true }
            , { field: 'CityName', title: '城市', width: $(this).width() * 0.2, sortable: true, align: 'center', resizable: false }
            , {
                field: 'State', title: '操作', width: $(this).width() * 0.2, align: 'center', resizable: false,
                formatter: function (value, rowData, rowIndex) {
                    var a = '<input type="button" value="确认" OnClick="CheckCompany(\'' + rowData.CustomerID + '\',\'' + rowData.CustomerName + '\',\'' + rowData.CityCode + '\',\'' + rowData.CityName + '\')"/>';

                    return a;

                }
            }
        ]],
        onLoadSuccess: function () {
            $(".datagrid-header .datagrid-cell").css('text-align', 'center').css('color', '#173967');
        },
        pagination: true,
        rownumbers: true

    });


    $('#lblName').hide();
    $('#tc').hide();
    $('#txtSearchName').hide();
    $('#upShow').hide();
});



//查询
function Search(value) {
    $('#company').show();

    var queryParam = $('#company').datagrid('options').queryParams;
    parm = value.toString();
    queryParam.state = parm;
    $('#company').datagrid('options').queryParams = queryParam;
    $('#company').datagrid('reload');
}

function CheckCompany(id, name, cid, cname) {
    $('#txtSearchName').show();
    $('#lblName').show();
    $('#tc').show();

    $('#CustomerID').val(id);
    $('#txtSearchName').text(name);


    $('#CityCode').val(cid);
    $('#CityName').text(cname);


    $.ajax({
        url: "/Contract/GetRatingScore",
        data: { id: id },
        type: "post",
        datatype: "json",
        success: function (data) {
            if (data.Result == "") {
                $("#btnSave").hide();
                $.messager.alert("提示", "请完善客户基本信息，才能创建合同！", "info");
            }
            else {

                $("#btnSave").show();
            }
        }
    });


    $.ajax({
        url: "/Contract/GetCustomerService",
        type: "post",
        data: { id: id },
        datatype: "json",
        success: function (data) {
            var list = [];
            for (var i = 0; i < data.length; i++) {
                var newSysOperationId = $("#SysPersonID").val();
                $("#SysPersonID").val(newSysOperationId + (newSysOperationId.length > 0 ? "^" : "") + data[i].SysPersonID + "&" + data[i].SysPersonName);
                list.push(data[i].SysPersonID);
            }
            $("#SysPersonIDOld").val($("#SysPersonID").val());
            $('#selPerson').combobox('setValues', list);
        }


    });
}

//填写记录保存
function AddInfo() {

    var ids = "";
    $("input[name='RecommendSolutionID']:checked").each(function () {
        ids += $(this).val() + ",";
    });
    $("#RecommendSolutionIDs").val(ids);

    var companyName = $('#txtSearchName').text();//公司名称
    if (companyName == "" || $("#CustomerID").val() == "") {
        $.messager.alert('提示', '请选择合同公司！', "info");
        return false;
    }

    if ($("form").valid()) {
        $.ajax({
            url: "/Contract/Create",
            type: "Post",
            data: $("form").serialize(),
            dataType: "json",
            success: function (data) {

                if ($.messager) {
                    $.messager.defaults.ok = '确定';
                    $.messager.defaults.cancel = '继续';
                    $.messager.confirm('操作提示', data, function (r) {
                        parent.$('#flexigridData').datagrid("reload");
                        parent.$('#flexigridData').datagrid('unselectAll');
                        closeWindow();

                    });
                }

            },
            error: function () {
            }
        });

    }





    //var companyName = $('#txtSearchName').text();//公司名称
    //var companyCode = $('#CustomerID').val();//公司code

    //var cityCode = $('#CityCode').val();//城市code
    //var cityName = $('#CityName').text();//城市名称

    //var packages = $('#hid_userPackages').val();//套餐ID

    //var beginDate = $('#beginDate').datebox('getValue');//开始时间
    //var endDate = $('#endDate').datebox('getValue');//结束时间

    //var packageMoney = $('#contractMoney').val();//金额

    //var uploadPackage = $('#hid_uploadPackage').val();//合同ID

    //if (companyName == "") {
    //    $.messager.alert('提示', '请选择合同公司！', "info");
    //}
    //else if (beginDate == "" || endDate == "") {
    //    $.messager.alert('提示', '请选择生效日期！', "info");
    //}
    //else if (packageMoney == "") {
    //    $.messager.alert('提示', '请输入金额！', "info");
    //}
    //else if (uploadPackage == "") {
    //    $.messager.alert('提示', '请上传合同！', "info");
    //}
    //else {
    //    $.messager.confirm('提示', '是否保存?', function (r) {
    //        if (r) {

    //            $.post('../Contract/AddPackageInfo', {
    //                companyName: companyName, companyCode: companyCode, cityCode: cityCode, cityName: cityName, packages: packages,
    //                beginDate: beginDate, endDate: endDate, packageMoney: packageMoney, uploadPackage: uploadPackage
    //            }, function (data) {
    //                if (data.IsSuccess == true) {
    //                    parent.$.messager.alert('提示', '保存成功！', "info");
    //                    parent.$('#contract').datagrid("reload");
    //                    parent.$('#contract').datagrid('unselectAll');
    //                    parent.$('#w_add').window('close');

    //                }
    //                else {
    //                    $.messager.alert('提示', '记录失败！', "info");
    //                }
    //            });
    //        }

    //    });
    //}


}

function toDate(str) {
    var sd = str.split("-");
    return new Date(sd[0], sd[1], sd[2]);
}
