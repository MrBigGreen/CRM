
function CallTel(tel) {
    var fid = $("#fid").val();
    $.post("../TodayTasks/CallTel",
              {
                  tel: tel, personTelCode: fid
              }, function (data) {
                  if (data.IsSuccess == true) {
                      $.messager.alert('提示', '呼叫成功！', "info");
                  }
                  else {
                      $.messager.alert('提示', '呼叫失败！', "info");
                  }
              });
}


























































