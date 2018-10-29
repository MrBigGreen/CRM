
$(function () {
    BindProvinceCity();
    var frameId = window.frameElement && window.frameElement.id || '';
    if (frameId == "iframeWindowTop" || frameId == "iframeWindow") {
        //添加选择按钮
        $('#shebaoDatagrid').datagrid("addToolbarItem", [{ "text": "选择", "iconCls": "icon-ok", handler: function () { flexiSelect(); } }]);
    }
    else {
        //如果列表页出现在弹出框中，则只显示查询和选择按钮
        var parent = window.dialogArguments; //获取父页面
        if (parent == "undefined" || parent == null) {
            //首先获取iframe标签的id值

            var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");

            //然后关闭AJAX相应的缓存
            $.ajaxSetup({
                cache: false
            });
            if (iframeid != "") {
                //获取按钮值
                $.getJSON("../Home/GetToolbar", { id: iframeid }, function (data) {
                    if (data == null) {
                        return;
                    }
                    $('#shebaoDatagrid').datagrid("addToolbarItem", data);
                });
            }
        }
    }
});



//“查询”按钮，弹出查询框
function flexiQuery() {
    //将查询条件按照分隔符拼接成字符串
    var search = "";
    var SocialInsurName = $("#SocialInsurName").val();
    if (SocialInsurName != "") {
        search = search + 'SocialInsurName' + "&" + SocialInsurName + '^';
    }
    var ProvinceCode = $("#ProvinceCode").val();
    if (ProvinceCode != "") {
        search = search + 'ProvinceCode' + "&" + ProvinceCode + '^';
    }
    var CityCode = $("#CityCode").val();
    if (CityCode != "") {
        search = search + 'CityCode' + "&" + CityCode + '^';
    }
    var DistrictCode = $("#DistrictCode").val();
    if (DistrictCode != "") {
        search = search + 'DistrictCode' + "&" + DistrictCode + '^';
    }
    $('#shebaoDatagrid').datagrid('options').url = "../PaySheBao/GetSheBaoData";
    $("#shebaoDatagrid").datagrid("clearSelections");
    //执行查询
    $('#shebaoDatagrid').datagrid('load', { search: search });
}


//清空
function RemvoeAll() {

}
//导航到查看详细的按钮
function getView() {

    var arr = $('#shebaoDatagrid').datagrid('getSelections');

    if (arr.length == 1) {
        var ID = arr[0].SocialInsurID;
        showWindowTop("修改销售业绩", "/PaySheBao/SheBaoView?id=" + ID, 600, 600);
    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;
}

//导航到创建的按钮
function flexiCreate() {
    showWindowTop("新增社保", "/PaySheBao/SheBaoCreate", 600, 600);
    return false;
}
//导航到修改的按钮
function flexiModify() {

    var arr = $('#shebaoDatagrid').datagrid('getSelections');

    if (arr.length == 1) {
        var ID = arr[0].SocialInsurID;

        showWindowTop("修改销售业绩", "/PaySheBao/SheBaoEdit?id=" + ID, 600, 600);
    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;

};
//删除的按钮
function flexiDelete() {

    var arr = $('#shebaoDatagrid').datagrid('getSelections');
    if (arr.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    if (arr.length == 1) {
        var ID = arr[0].SocialInsurID;
        $.messager.confirm('操作提示', "确认删除：“ " + arr[0].SocialInsurName + " ”吗？", function (r) {
            if (r) {
                $.ajax({
                    url: "/PaySheBao/GetSheBaoDelete",
                    type: "post",
                    data: { "ids": ID },
                    datatype: "json",
                    success: function (data) {

                        if ($.messager) {
                            $.messager.alert('操作提示', data, "info", function (r) {
                                $('#shebaoDatagrid').datagrid("reload");
                                $('#shebaoDatagrid').datagrid('unselectAll');

                            });
                        }
                    }

                });
            }
        });

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
};


//“选择”按钮，在其他（与此页面有关联）的页面中，此页面以弹出框的形式出现，选择页面中的数据
function flexiSelect() {

    var rows = $('#shebaoDatagrid').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    debugger;
    var id = $('#SocialInsurID', window.parent.document);
    if (id != undefined) {
        id.val(rows[0].SocialInsurID);
    }
    var name = $('#SocialInsurName', window.parent.document);
    if (name != undefined) {
        name.val(rows[0].SocialInsurName);
    }
    window.parent.$('#myWindow').window('close');
}