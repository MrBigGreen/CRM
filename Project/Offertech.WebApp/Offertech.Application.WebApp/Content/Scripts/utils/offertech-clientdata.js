$(function () {
    $.getclientdata();
})
var clientdataItem = [];
var clientorganizeData = [];
var clientdepartmentData = [];
var clientpostData = [];
var clientroleData = [];
var clientuserGroup = [];
var clientuserData = [];
var authorizeMenuData = [];
var authorizeButtonData = [];
var authorizeColumnData = [];
var clientAreaData = [];
$.getclientdata = function () {
    $.ajax({
        url: "/ClientData/GetClientDataJson",
        type: "post",
        dataType: "json",
        async: false,
        success: function (data) {

            clientdataItem = data.dataItem;
            clientorganizeData = data.organize;
            clientdepartmentData = data.department;
            clientpostData = data.post;
            clientroleData = data.role;
            clientuserGroup = data.userGroup;
            clientuserData = data.user;
            authorizeMenuData = data.authorizeMenu;
            authorizeButtonData = data.authorizeButton;
            authorizeColumnData = data.authorizeColumn;
            clientAreaData = data.area;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            dialogMsg(errorThrown, -1);
        }
    });
}



request = function (keyValue) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == keyValue) {
            if (unescape(ar[1]) == 'undefined') {
                return "";
            } else {
                return unescape(ar[1]);
            }
        }
    }
    return "";
}