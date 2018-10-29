//function onChangeTheme(themeName) {
//    var themePath;
//    alert("sdfsdf");
//    if (themeName.value) {
//        themePath = themeName.value;
//        $.ajax({
//            type: "get",
//            url: '/Home/SaveTheme/?theme=' + themePath,
//            cache: false,
//            beforeSend: function (XMLHttpRequest) {

//            },
//            success: function (data, textStatus) {
//                console.log("皮肤保存成功");
//            },
//            complete: function (XMLHttpRequest, textStatus) {

//            },
//            error: function () {

//            }
//        });
//    } else {
//        themePath = themeName;
//    }
//    var $easyuiTheme = $('#easyuiTheme');
//    var url = $easyuiTheme.attr('href');
//    var href = url.substring(0, url.indexOf('themes')) + 'themes/' + themePath + '/easyui.css';
//    $easyuiTheme.attr('href', href);
//    //$('#mainCss').attr('href', 'Content/default.css');
//    var $iframe = $('iframe');
//    if ($iframe.length > 0) {
//        for (var i = 0; i < $iframe.length; i++) {
//            var ifr = $iframe[i];
//            $(ifr).contents().find('#easyuiTheme').attr('href', href);
//        }
//    }
//    $.cookie('easyuiThemeName', themePath, {
//        expires: 60
//    });
//    $.parser.parse();
//};
//var th = $.cookie('easyuiThemeName');
//if (th != undefined && th != '') {
//    onChangeTheme($.cookie('easyuiThemeName'));
//}