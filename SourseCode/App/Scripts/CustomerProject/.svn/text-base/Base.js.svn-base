$(function () {
    $('#EvaluationResult').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        url: '/SysField/GetComboBoxList?ParentID=1605111430335445364b63da025d2',
    });


});


//“查询”按钮，弹出查询框
//将查询条件按照分隔符拼接成字符串
var search = "";
function flexiQuery() {


    //将查询条件按照分隔符拼接成字符串
    search = "";
    var EvaluationResult = $('#EvaluationResult').combobox('getValue');//评估结果
    if (EvaluationResult != '') {
        search = search + "EvaluationResult&" + EvaluationResult + "^";
    }
    var ProjectName = $("#ProjectName").val();//
    if (ProjectName != '') {
        search = search + "ProjectName&" + ProjectName + "^";
    }
    var CustomerName = $("#CustomerName").val();//
    if (CustomerName != '') {
        search = search + "CustomerName&" + CustomerName + "^";
    }

    var SysPersonID = $("#EvaluationPerson").val();//
    if (SysPersonID != '') {
        search = search + "EvaluationPerson&" + SysPersonID + "^";
    }

    if ($("#StartDate").val() != "") {

        search = search + 'StartDate' + "&" + $('#StartDate').val() + '^';
    }
    if ($("#EndDate").val() != "") {
        search = search + 'EndDate' + "&" + $('#EndDate').val() + '^';
    }
    //$('#flexigridData').datagrid('options').url = "../CustomerProject/GetProjectData";
    $("#flexigridData").datagrid("clearSelections");
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
};



//清空
function RemvoeAll() {
    $('#StartDate').datetimebox('setValue', '');
    $('#EndDate').datetimebox('setValue', '');
}
//“选择”按钮，在其他（与此页面有关联）的页面中，此页面以弹出框的形式出现，选择页面中的数据
function flexiSelect() {

    var rows = $('#flexigridData').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }

    var arr = [];
    for (var i = 0; i < rows.length; i++) {
        arr.push(rows[i].CustomerAttributionChangeID);
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
        showWindowTop("项目评估", "/CustomerProject/View?id=" + arr[0].CustomerProjectID, 750, 650);
        //window.location.href = "/CustomerAllocation/Details?ID=" + arr[0].CustomerID;

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;
}


//导航到修改的按钮
function flexiModify() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {
        var ID = arr[0].CustomerProjectID;

        showWindowTop("修改项目", "/CustomerProject/Edit?id=" + ID, 750, 650);
    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;

};

//导航评估按钮
function flexiEvaluation() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {
        if (arr[0].EvaluationResult == "1605111431371348259613423d3c6") {
            var ID = arr[0].CustomerProjectID;

            showWindowTop("项目评估", "/CustomerProject/Evaluation?id=" + ID, 750, 650);
        }
        else {
            $.messager.alert('操作提示', '项目不能重复评估!', 'warning');
        }

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;

};


//删除的按钮
function flexiDelete() {

    var arr = $('#flexigridData').datagrid('getSelections');
    if (arr.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    if (arr.length == 1) {
        var ID = arr[0].CustomerAllocationID;
        $.messager.confirm('操作提示', "确认删除：“ " + arr[0].SysPersonName + " ”吗？", function (r) {
            if (r) {
                $.post("../CustomerAllocation/CancelAllocation", { ID: ID }, function (res) {
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

