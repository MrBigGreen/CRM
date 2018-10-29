



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


function checkSubmit() {

    var ids = "";
    $("input[name='RecommendSolutionID']:checked").each(function () {
        ids += $(this).val() + ",";
    });
    $("#RecommendSolutionIDs").val(ids);
    $('#submit').click();


    // $("#submit").attr("disabled", "disabled");
}