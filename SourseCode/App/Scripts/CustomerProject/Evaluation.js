$(function () {
    
    $("input:radio[name='rdoResult']").change(function () { //拨通

        $("#EvaluationResult").val($("input:radio[name='rdoResult']:checked").val());
    });
});
function checkSubmit() {


    var demo = "";
    demo = $("#EvaluationDesc").val();
    if (demo == null || demo == undefined || demo == "") {
        $.messager.alert('提示', '评估说明不能为空！', "error");
        return false;
    }
    var Result = $("input[type='radio'][name='rdoResult']:checked").val();
    if (Result == null || Result == undefined || Result == "") {
        $.messager.alert('提示', '请选择评估结果！', "error");
        return false;
    }
    
    $('#btnSubmit').click(); 
}