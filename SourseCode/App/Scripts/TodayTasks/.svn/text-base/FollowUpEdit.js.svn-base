
$(function () {
    var customerFollowID = $('#hiddCustomerFollowID').val();
    var customerID = $('#hiddCustomerID').val();
    //$('#hiddCustomerFollowID').val(customerFollowID);
    //$('#hiddCustomerID').val(customerID);
    var d = new Date();
    s = d.getFullYear() + "-";             //取年份
    s = s + (d.getMonth() + 1) + "-";//取月份
    s += d.getDate() + " ";         //取日期
    s += d.getHours() + ":";       //取小时
    s += d.getMinutes() + ":";    //取分
    s += d.getSeconds();         //取秒

    $('#recordDate').text(s);

    $('#NextType_1').attr("checked", "checked");

    $('#nextCallDate').datetimebox('calendar').calendar({
        validator: function (date) {
            var now = new Date();
            var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
            var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 10);
            return d1 <= date;
        }
    });
    $('#nextCallDate').datetimebox({
        //onSelect: function () {
        //    $('#sPurpose').show();
        //},
        showSeconds: false


    });
    $('#customerLevel').combobox({
        url: '../TodayTasks/GetCombox?type=0&&parm=1506121005392690303c5640a411a',
        valueField: 'id',
        textField: 'text',
        editable: false
    });
    //$('#customerFeedback').combobox({
    //    url: '../TodayTasks/GetCombox?type=0&&parm=15061210070906278800aad1ca67e',
    //    valueField: 'id',
    //    textField: 'text',
    //    editable: false
    //});
    $('#customerFunnel').combobox({
        url: '../TodayTasks/GetCombox?type=0&&parm=1506121008181709094ed4907cf81',
        valueField: 'id',
        textField: 'text',
        editable: false
    });
    //$('#customerHandle').combobox({
    //    url: '../TodayTasks/GetCombox?type=0&&parm=15061210110963081062d99812671',
    //    valueField: 'id',
    //    textField: 'text',
    //    editable: false
    //});
    $('#customerOffer').combobox({
        url: '../TodayTasks/GetCombox?type=0&&parm=150612100955842681016eb929de8',
        valueField: 'id',
        textField: 'text',
        editable: false
    });

    $('#customerPhone').combobox({
        url: '../TodayTasks/GetCombox?type=1&&parm=' + customerID,
        valueField: 'id',
        textField: 'text',
        editable: false
    });

    $('#customerPurpose').combobox({
        url: '../TodayTasks/GetCombox?type=0&&parm=15061210014976927076a3fd215ab',
        valueField: 'id',
        textField: 'text',
        editable: false
    });
    showInfo(0);
});

function checkLength(obj) {
    var maxChars = $(obj).attr("maxlength");
    if ($(obj).val().length > maxChars)
        $(obj).val($(obj).val().substring(0, maxChars));
    var curr = maxChars - $(obj).val().length;
    $("#leave_letter").text(curr.toString());
}



//填写记录保存
function UpdateEdit() {

    var customerFollowID = $('#hiddCustomerFollowID').val();//跟进记录ID
    var customerID = $('#hiddCustomerID').val();//客户ID
    var isConnectRadio = document.getElementsByName("ckIsConnect");
    var isLine = "";//是否接通
    for (var i = 0; i < isConnectRadio.length; i++) {
        if (isConnectRadio[i].checked) {
            isLine = isConnectRadio[i].value;
            break;
        }
    }

    var isNextRadio = document.getElementsByName("ckIsNextType");
    var isNextType = "";//跟进方式
    for (var i = 0; i < isNextRadio.length; i++) {
        if (isNextRadio[i].checked) {
            isNextType = isNextRadio[i].value;
            break;
        }
    }

    var customerPhone = $('#customerPhone').combobox('getValue');//联系人
    var customerLevel = $('#customerLevel').combobox('getValue');//对方级别
    // var customerFeedback = $('#customerFeedback').combobox('getValue');//客户反馈

    var customerFunnel = $('#customerFunnel').combobox('getValue');//客户进程
    var customerOffer = $('#customerOffer').combobox('getValue');//商机判断
    // var customerHandle = $('#customerHandle').combobox('getValue');//处理方式
    var customerPurpose = $('#customerPurpose').combobox('getValue');//跟进目的

    var callDate = $("#nextCallDate").datetimebox('getValue');//下次跟进时间


    var remark = $('#editRemark').val();//备注
    var cityName = $('#hiddCityName').val();
    var cityCode = $('#hiddCityCode').val();
    var addressDetails = $('#hiddAddressDetails').val();

    var provinceName = $('#hiddProvinceName').val();
    var provinceCode = $('#hiddProvinceCode').val();
    var districtName = $('#hiddDistrictName').val();
    var districtCode = $('#hiddDistrictCode').val();
    if (provinceName==null) {
        provinceName = "";
    }
    if (provinceCode == null) {
        provinceCode = "";
    }
    if (districtName == null) {
        districtName = "";
    }
    if (districtCode == null) {
        districtCode = "";
    }

    var Fcode = $("#hiddFcode").val();
    if (Fcode==null) {
        Fcode = "";
    }
    if (customerLevel.indexOf("请选择") != -1) {
        customerLevel = "";
    }

    if (customerOffer.indexOf("请选择") != -1) {
        customerOffer = "";
    }
    if (customerPhone.indexOf("请选择") != -1) {
        $.messager.alert('提示', '请选择联系人！', "info");
    }
    else if (customerFunnel.indexOf("请选择") != -1) {
        $.messager.alert('提示', '请选择客户进程！', "info");
    }
    else if (remark.length < 5 && isLine == 1) {
        $.messager.alert('提示', '处理方式至少输入5个字！', "info");
    }
    else if (callDate == "") {
        $.messager.alert('提示', '请选择下次跟进时间！', "info");
    }
    else if (callDate != "" && customerPurpose.indexOf("请选择") != -1) {
        $.messager.alert('提示', '请选择跟进目的！', "info");
    }
    else {
        if (isLine == 2) {
            remark = "";
        }
        $.messager.confirm('提示', '是否保存本次跟进记录?', function (r) {
            if (r) {
                $.post('../TodayTasks/AddFollowUpInfo', {
                    customerFollowID: customerFollowID, isLine: isLine, isNextType: isNextType, customerPhone: customerPhone, customerLevel: customerLevel,
                    customerFunnel: customerFunnel, customerOffer: customerOffer, callDate: callDate, remark: remark, customerID: customerID, cityName: cityName, cityCode: cityCode, customerPurpose: customerPurpose, addressDetails: addressDetails, provinceName: provinceName, provinceCode: provinceCode, districtName: districtName, districtCode: districtCode, Fcode: Fcode
                }, function (data) {

                    if (data.IsSuccess == true) {
                        parent.$.messager.alert('提示', '记录成功！', "info");
                        parent.$('#today').datagrid("reload");
                        parent.$('#today').datagrid('unselectAll');
                        parent.$('#w_edit').window('close');
                        if (customerFunnel == "150718162712213888586d8311e52") {
                            $('#editiframe').attr("src", "../Contract/CreateContractInfo");
                            $('#w_edit').dialog({
                                title: '填写【' + CustomerName + '】合同记录',
                                width: 780,
                                height: 620,
                                closed: false,
                                cache: false,
                                resizable: true,
                                left: 25,
                                top: 15,
                                modal: true,
                            });
                        }
                    }
                    else {
                        $.messager.alert('提示', '记录失败！', "info");
                    }
                });
            }

        });
    }

}

function toDate(str) {
    var sd = str.split("-");
    return new Date(sd[0], sd[1], sd[2]);
}

function showInfo(id) {
    if (id == 0) {
        // $('#sNext').hide();
        //  $('#sPurpose').hide();
        //$('#a').show();
        $('#b').show();
        //  $('#c').show();
        // $('#d').show();
        $('#e').show();
        //  $('#f').show();
        $('#g').show();

    } else {
        //$('#sNext').show();
        // $('#sPurpose').hide();

        //$('#a').hide();
        $('#b').hide();
        //  $('#c').hide();
        //  $('#d').hide();
        $('#e').hide();
        //  $('#f').hide();
        $('#g').hide();

    }

    $("#nextCallDate").datebox('setValue', '');
    //$("#nextLineDate").val('');
}
