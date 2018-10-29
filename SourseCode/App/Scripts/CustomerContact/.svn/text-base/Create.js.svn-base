$(function () {

    //$('#CompanyTel2').attr("class", "input-validation-error");
    $('#CompanyTel2').attr("data-val", "true");
    $('#CompanyTel2').attr("data-val-regex-pattern", "^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$");
    $('#CompanyTel2').attr("data-val-regex", "电话格式有误。");

    //$('#CompanyTel').attr("class", "input-validation-error");
    $('#CompanyTel').attr("data-val", "true");
    $('#CompanyTel').attr("data-val-regex-pattern", "^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$");
    $('#CompanyTel').attr("data-val-regex", "电话格式有误。");

    //$('#PhoneNumber1').attr("class", "input-validation-error");
    $('#PhoneNumber1').attr("data-val", "true");
    $('#PhoneNumber1').attr("data-val-regex-pattern", "^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$");
    $('#PhoneNumber1').attr("data-val-regex", "电话格式有误。");

    //$('#PhoneNumber2').attr("class", "input-validation-error");
    $('#PhoneNumber2').attr("data-val", "true");
    $('#PhoneNumber2').attr("data-val-regex-pattern", "^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$");
    $('#PhoneNumber2').attr("data-val-regex", "电话格式有误。");




});
function SubmitValidation() {
    var flag = true;

    if ($("#ContactName").val() == "") {
        $("#ContactName").removeAttr("class").addClass("input-validation-error");
        flag = false;
    }
    if ($("#CompanyTel").val() == "" && $("#CompanyTel2").val() == "" && $("#PhoneNumber1").val() == "" && $("#PhoneNumber2").val() == "") {
        $("#CompanyTel").removeAttr("class").addClass("input-validation-error");
        $("#CompanyTel2").removeAttr("class").addClass("input-validation-error");
        $("#PhoneNumber1").removeAttr("class").addClass("input-validation-error");
        $("#PhoneNumber2").removeAttr("class").addClass("input-validation-error");
        flag = false;
        $.messager.alert('操作提示', '请至少输入填写一个联系方式!', 'warning');
    }

    var param = /^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$/;
   
    if ($("#CompanyTel").val() != "") {
        if (!$("#CompanyTel").val().match(param)) {
            $('#CompanyTel').attr("class", "input-validation-error");
            flag = false;
            $.messager.alert('操作提示', '电话号码格式不正确!', 'warning');
        }
    }
    if ($("#CompanyTel2").val() != "") {
        if (!$("#CompanyTel2").val().match(param)) {
            $('#CompanyTel2').attr("class", "input-validation-error");
            flag = false;
            $.messager.alert('操作提示', '电话号码格式不正确!', 'warning');
        }
    }
    //先验证联系电话
    if ($("#PhoneNumber1").val() != "") {
        if (!$("#PhoneNumber1").val().match(param)) {
            $('#PhoneNumber1').attr("class", "input-validation-error");
            flag = false;
            $.messager.alert('操作提示', '手机号码格式不正确!', 'warning');
        }
    }

    //先验证联系电话
    if ($("#PhoneNumber2").val() != "") {
        if (!$("#PhoneNumber2").val().match(param)) {
            $('#PhoneNumber2').attr("class", "input-validation-error");
            flag = false;
            $.messager.alert('操作提示', '手机号码格式不正确!', 'warning');
        }
    }
    if (flag) {
        $('#submit').click();
    }
}
