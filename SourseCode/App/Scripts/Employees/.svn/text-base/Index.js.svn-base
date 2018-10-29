
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function toDate(str) {
    var sd = str.split("-");
    return new Date(sd[0], sd[1], sd[2]);
}

//按条件查询
function SearchToday() {
    var search = "";

    var EmployessName = $("#txtEmployessName").val();
    var CompanyName = $("#txtCompanyName").val();
    var CardID = $("#txtCardID").val();
    var BankID = $("#txtBankID").val();
    var PhoneNum = $("#txtPhoneNum").val();

    var JobStatus = $('input:radio:checked').val();

    search = 'EmployessName&' + EmployessName + '^';
    search += 'CompanyName&' + CompanyName + '^';
    search += 'PhoneNum&' + PhoneNum + '^';
    search += 'CardID&' + CardID + '^';
    search += 'BankID&' + BankID + '^';
    search += 'JobStatus&' + JobStatus + '^';
    $("#flexigridData").datagrid("clearSelections");
    //执行查询
    $('#flexigridData').datagrid('load', { search: search });
}

//“导出”按钮     在6.0版本中修改
function flexiExport() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length > 0) {
        $.messager.confirm('提示', '是否导出所选项?', function (r) {
            if (r) {
                var empids = "";
                for (var i = 0; i < arr.length; i++) {
                    empids += arr[i].EmpID + ";";
                }

                $.post("../Pay/Export",
                    {
                        search: "1$" + empids
                    }, function (res) {
                        window.location.href = res;

                    });
            }

        });
    } else {
        $.messager.confirm('提示', '是否导出全部?', function (r) {
            if (r) {
                var searchs = "";

                var EmployessName = $("#txtEmployessName").val();
                var CompanyName = $("#txtCompanyName").val();
                var CardID = $("#txtCardID").val();
                var BankID = $("#txtBankID").val();
                var PhoneNum = $("#txtPhoneNum").val();

                var JobStatus = $('input:radio:checked').val();

                searchs = 'EmployessName&' + EmployessName + '^';
                searchs += 'CompanyName&' + CompanyName + '^';
                searchs += 'PhoneNum&' + PhoneNum + '^';
                searchs += 'CardID&' + CardID + '^';
                searchs += 'BankID&' + BankID + '^';
                searchs += 'JobStatus&' + JobStatus + '^';

                $.post("../Pay/Export",
                    {
                        search: "0$" + searchs
                    }, function (res) {
                        window.location.href = res;

                    });
            }

        });
    }


};


//新增
function flexiCreate() {
    $('#w_add').window('open');
    $('#addiframe').attr("src", "../Pay/CreateInfo");
}

//修改
function OpenEditWindow(cid) {
    $('#w_edit').window('open');
    $('#editiframe').attr("src", "../Pay/UpdateInfo?EmpID=" + cid);
}

//查看
function OpenLookWindow(cid) {
    $('#w_look').window('open');
    $('#lookiframe').attr("src", "../Pay/ShowInfo?EmpID=" + cid);
}

//导入
function OpenImport() {
    $('#w_import').window('open');
    $('#importiframe').attr("src", "../Pay/ImportExcel");
}

//离职
function OpenFireInfoWindow() {
    $('#w_fire').window('open');
    $('#fireiframe').attr("src", "../Pay/FireExcel");
}



//搜索清空
function RemvoeAll() {

    $("#txtEmployessName").val('');
    $("#txtCompanyName").val('');
    $("#txtCardID").val('');
    $("#txtBankID").val('');
    $("#txtPhoneNum").numberbox('clear');
    $("#checkall").attr('checked', 'checked');
    $("#flexigridData").datagrid("clearSelections");
}
//离职
function OpenDeleteWindow(eid) {
    $.messager.confirm('提示', '是否离职?', function (r) {
        if (r) {
            $.post("../Pay/FireEmployeese",
                {
                    parm: eid
                }, function (data) {
                    if (data.IsSuccess == true) {
                        $.messager.alert('提示', '离职成功！', "info");
                        $("#flexigridData").datagrid("reload");
                    } else {
                        $.messager.alert('提示', '离职失败！', "error");
                    }

                });
        }

    });
}
