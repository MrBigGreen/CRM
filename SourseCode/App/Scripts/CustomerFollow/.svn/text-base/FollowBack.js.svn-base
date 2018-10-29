
$(function () {
    var customerID = $('#CustomerID').val();
    ////联系人
    //$('#customerPhone').combobox({
    //    url: '../TodayTasks/GetCombox?type=1&&parm=' + customerID,
    //    valueField: 'id',
    //    textField: 'text',
    //    editable: false,
    //    width: 130,
    //    onSelect: function (newValue) {

    //        ////改变联系人
    //        //var arr = newValue.id.split(',');
    //        //$("#CallPhone").val(arr[1]);
    //        //$("#CustomerContactName").val(arr[0]);
    //        $("#CallPhone").val(newValue.id);
    //        $("#CustomerContactName").val(newValue.text);
    //    }
    //});
    //$('#customerPhone').combobox("setValue", $("#CustomerContactName").val());

    //modify by chand 2016-03-14  临时关闭
    //获取最后一次联系人联系电话
    $.ajax({
        url: "/CustomerFollow/GetLastContactInfo",
        type: "post",
        data: { id: customerID },
        datatype: "json",
        success: function (data) {
            if (data.Result == 1) {

                //$("#CustomerContactName").val(data.ContactName);
                //$("#CallPhone").val(data.CallPhone);
                //$("#CustomerEmail").val(data.CustomerEmail);
                //$("#OtherLevel").val(data.OtherLevel);
                //$("#IsKP").val(data.IsKP);
                $("#CustomerFunnel").val(data.CustomerFunnel);
            }
        }

    });

    /****************************弹出选择联系人列表****************************************/
    $('#dd').tooltip({
        content: $('<div style="width:600px"></div>'),
        showEvent: 'click',
        onUpdate: function (content) {
            content.panel({
                width: 580,
                border: false,
                title: '选择联系人',
                href: '/CustomerContact/MinList?ID=' + customerID
            });
        },
        onShow: function () {
            var t = $(this);
            t.tooltip('tip').unbind().bind('mouseenter', function () {
                t.tooltip('show');
            }).bind('mouseleave', function () {
                t.tooltip('hide');
            });
        }
    });
});
/****************************点击联系方式添加到文本框中****************************************/
function getSelectContact(ContactPerson, CallPhone, Email, OtherLevel, IsKP) {

    $("#CustomerContactName").val(ContactPerson);
    $("#CallPhone").val(CallPhone);
    $("#CustomerEmail").val(Email);
    $("#OtherLevel").val(OtherLevel);
    $("#IsKP").val(IsKP);
}

function showInfo(id) {
    if (id == 0) {
        $('#b').show();
        $('#c').show();
        $('#d').show();
        $('#g').show();

    } else {
        $('#b').hide();
        $('#c').hide();
        $('#d').hide();
        $('#g').hide();
    }

    $("#nextCallDate").datebox('setValue', '');
}


function checkLength(obj) {
    var maxChars = $(obj).attr("maxlength");
    if ($(obj).val().length > maxChars)
        $(obj).val($(obj).val().substring(0, maxChars));
    var curr = maxChars - $(obj).val().length;
    $("#leave_letter").text(curr.toString());
}

function checkSubmit() {

    var ContactState = $("input[type='radio'][name='ContactState']:checked").val();
    var demo = "";
    demo = $("#CustomerContactName").val();
    if (demo == null || demo == undefined || demo == "") {
        $.messager.alert('提示', '请选择联系人！', "info");
        return false;
    }
  
    ////如果客户进程变成“冰冻客户”，客户跟进记录中“下次跟进时间”、“跟进目的”、“跟进方式”不为必填项
    //if ($("#CustomerFunnel").val() != "1508130916502901730ae4a07422b") {
    //    demo = $("#NextFollowDate").val();//下次跟进时间
    //    if (demo == null || demo == undefined || demo == "") {
    //        $.messager.alert('提示', '请选择下次跟进时间！', "info");
    //        return false;
    //    }
    //    demo = $("#NextFollowUpPurpose").val();
    //    if (demo == null || demo == undefined || demo == "" || demo == "--请选择--") {
    //        $.messager.alert('提示', '请选择跟进目的！', "info");
    //        return false;
    //    }
    //}
    //
    if (ContactState == 2) {
        progress();
        //未联系上
        $('#btnSubmit').click();
    }
    else {
        //已联系上
        demo = $("#CustomerFunnel").val();
        if (demo == null || demo == undefined || demo == "" || demo == "--请选择--") {
            $.messager.alert('提示', '请选择客户进程！', "info");
            return false;
        }
        demo = $("#Remark").val();
        if (demo == null || demo == undefined || demo == "" || demo.length < 5) {
            $.messager.alert('提示', '请输入至少5个字的客户反馈信息！', "info");
            return false;
        }
        var ids = "";
        $("input[name='RecommendSolutionID']:checked").each(function () {
            ids += $(this).val() + ",";
        });
        $("#RecommendSolutionIDNew").val(ids);
        progress();
        $('#btnSubmit').click();

    }
    $("#submit").attr("disabled", "disabled");
}
function progress() {
    var win = $.messager.progress({
        title: 'Please waiting',
        msg: 'Loading data...'
    });
    setTimeout(function () {
        $.messager.progress('close');
    }, 5000)
}

///****************************************电话呼叫**********************************************///
function CallTel() {
    var tel = $("#CallPhone").val();
    if (tel == '' || tel == null || tel == undefined) {
        $.messager.alert('提示', '电话不能为空！', "info");
        return false;
    }
    //var isMobile = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1})|(17[0-9]{1})|(14[0-9]{1}))+\d{8})$/;
    //var isPhone = /^(?:(?:0\d{2,3})-)?(?:\d{7,8})(-(?:\d{3,}))?$/;;


    //if (!tel.match(/^(((13[0-9]{1})|159|153)+\d{8})$/)) {
    //    alert("手机号码格式不正确！");
    //    //$("#moileMsg").html("<font color='red'>手机号码格式不正确！请重新输入！</font>"); 
    //    $("#mobile").focus();
    //    return false;
    //}
    $.post("../TodayTasks/CallTel",
              {
                  tel: tel, personTelCode: ''
              }, function (data) {
                  if (data.IsSuccess == true) {
                      $.messager.alert('提示', '呼叫成功！', "info");
                  }
                  else {
                      $.messager.alert('提示', '呼叫失败！', "info");
                  }
              });

}
