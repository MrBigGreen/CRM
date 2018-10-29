
var LastLeftID = "";

function menuFix() {
    var obj = document.getElementById("nav").getElementsByTagName("li");

    for (var i = 0; i < obj.length; i++) {
        obj[i].onmouseover = function() {
            this.className += (this.className.length > 0 ? " " : "") + "sfhover";
        }
        obj[i].onMouseDown = function() {
            this.className += (this.className.length > 0 ? " " : "") + "sfhover";
        }
        obj[i].onMouseUp = function() {
            this.className += (this.className.length > 0 ? " " : "") + "sfhover";
        }
        obj[i].onmouseout = function() {
            this.className = this.className.replace(new RegExp("( ?|^)sfhover\\b"), "");
        }
    }
}

function DoMenu(emid) {
    var obj = document.getElementById(emid);
    obj.className = (obj.className.toLowerCase() == "expanded" ? "collapsed" : "expanded");
    if ((LastLeftID != "") && (emid != LastLeftID)) //关闭上一个Menu
    {
        document.getElementById(LastLeftID).className = "collapsed";
      //  $("#" + LastLeftID).prev().text("123123");
    }
    else {

    }
    LastLeftID = emid;
}

function GetMenuID() {

    var MenuID = "";
    var _paramStr = new String(window.location.href);

    var _sharpPos = _paramStr.indexOf("#");

    if (_sharpPos >= 0 && _sharpPos < _paramStr.length - 1) {
        _paramStr = _paramStr.substring(_sharpPos + 1, _paramStr.length);
    }
    else {
        _paramStr = "";
    }

    if (_paramStr.length > 0) {
        var _paramArr = _paramStr.split("&");
        if (_paramArr.length > 0) {
            var _paramKeyVal = _paramArr[0].split("=");
            if (_paramKeyVal.length > 0) {
                MenuID = _paramKeyVal[1];
            }
        }

    }

    if (MenuID != "") {
        DoMenu(MenuID)
    }
}

function setLeftNav(Mid, Pid) {

    DoMenu(Mid);
    $($("#" + Mid).children("li")[Pid]).children("a").addClass("liselect");
}


$(document).ready(function() {
    GetMenuID(); //*这两个function的顺序要注意一下，不然在Firefox里GetMenuID()不起效果
    menuFix();
});
