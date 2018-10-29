/* 未能缩小。正在返回未缩小的内容。
(1245,13-22): run-time error JS1300: Strict-mode does not allow assignment to undefined variables: maxHeight
(1229,17-26): run-time error JS1300: Strict-mode does not allow assignment to undefined variables: hideTimer
(1213,25-34): run-time error JS1300: Strict-mode does not allow assignment to undefined variables: maxHeight
(1185,25-34): run-time error JS1300: Strict-mode does not allow assignment to undefined variables: hideTimer
(1187,24-33): run-time error JS1300: Strict-mode does not allow assignment to undefined variables: maxHeight
(389,17-22): run-time error JS1300: Strict-mode does not allow assignment to undefined variables: cents
(386,13-18): run-time error JS1300: Strict-mode does not allow assignment to undefined variables: cents
(384,13-17): run-time error JS1300: Strict-mode does not allow assignment to undefined variables: sign
 */
/*!
 * 版 本 LearunADMS V6.1.1.7 (http://www.learun.cn)
 * Copyright 2011-2016 Learun, Inc.
 * LR215
 */
(function ($) {
    "use strict";
    //基础方法
    var uiBase = {
        //加载提示
        loading: function (ops) {//加载动画显示与否
            var ajaxbg = top.$("#loading_background,#loading_manage");
            if (ops.isShow) {
                ajaxbg.show();
            } else {
                if (top.$("#loading_manage").attr('istableloading') == undefined) {
                    ajaxbg.hide();
                    top.$(".ajax-loader").remove();
                }
            }
            if (!!ops.text) {
                top.$("#loading_manage").html(ops.text);
            } else {
                top.$("#loading_manage").html("正在拼了命为您加载…");
            }
            top.$("#loading_manage").css("left", (top.$('body').width() - top.$("#loading_manage").width()) / 2 - 54);
            top.$("#loading_manage").css("top", (top.$('body').height() - top.$("#loading_manage").height()) / 2);
        },
        ajaxLoading: function (isShow) {
            var $obj = $('#ajaxLoader');
            if (isShow) {
                $obj.show();
            }
            else {
                $obj.fadeOut();
            }
        },
        //获取窗口Id
        tabiframeId: function () {//tab窗口Id
            return top.$(".LRADMS_iframe:visible").attr("id");
        },
        //获取当前窗口
        currentIframe: function () {
            if ((top.frames[uiBase.tabiframeId()].contentWindow != undefined) && (uiBase.getBrowserName() == "Chrome" || uiBase.getBrowserName() == "FF")) {
                return top.frames[uiBase.tabiframeId()].contentWindow;
            }
            else {
                return top.frames[uiBase.tabiframeId()];
            }
        },
        //获取iframe窗口
        getIframe: function (Id) {
            var obj = frames[Id];
            if (obj != undefined) {
                if ((obj.contentWindow != undefined) && (uiBase.getBrowserName() == "Chrome" || uiBase.getBrowserName() == "FF")) {
                    return obj.contentWindow;
                }
                else {
                    return obj;
                }
            }
            else {
                return null;
            }
        },
        //刷新页面
        reload: function () {
            location.reload();
            return false;
        },
        //提示框
        dialogTop: function (opt) {
            $(".tip_container").remove();
            var bid = parseInt(Math.random() * 100000);
            $("body").prepend('<div id="tip_container' + bid + '" class="container tip_container"><div id="tip' + bid + '" class="mtip"><i class="micon"></i><span id="tsc' + bid + '"></span><i id="mclose' + bid + '" class="mclose"></i></div></div>');
            var $this = $(this);
            var $tip_container = $("#tip_container" + bid);
            var $tip = $("#tip" + bid);
            var $tipSpan = $("#tsc" + bid);
            //先清楚定时器
            clearTimeout(window.timer);
            //主体元素绑定事件
            $tip.attr("class", opt.type).addClass("mtip");
            $tipSpan.html(opt.msg);
            $tip_container.slideDown(300);
            //提示层隐藏定时器
            window.timer = setTimeout(function () {
                $tip_container.slideUp(300);
                $(".tip_container").remove();
            }, 4000);
            $("#tip_container" + bid).css("left", ($(window).width() - $("#tip_container" + bid).width()) / 2);
        },
        dialogOpen: function (opt) {
            uiBase.loading({ isShow: true });
            var opt = $.extend({
                id: null,
                title: '系统窗口',
                width: "100px",
                height: "100px",
                url: '',
                shade: 0.3,
                btn: ['确认', '关闭'],
                callBack: null
            }, opt);
            var _url = opt.url;
            var _width = top.$.learunUIBase.windowWidth() > parseInt(opt.width.replace('px', '')) ? opt.width : top.$.learunUIBase.windowWidth() + 'px';
            var _height = top.$.learunUIBase.windowHeight() > parseInt(opt.height.replace('px', '')) ? opt.height : top.$.learunUIBase.windowHeight() + 'px';
            top.layer.open({
                id: opt.id,
                type: 2,
                shade: opt.shade,
                title: opt.title,
                fix: false,
                area: [_width, _height],
                content: top.contentPath + _url,
                btn: opt.btn,
                yes: function () {
                    opt.callBack(opt.id);
                }, cancel: function () {
                    if (opt.cancel != undefined) {
                        opt.cancel();
                    }
                    return true;
                }
            });
        },
        dialogContent: function (opt) {
            var opt = $.extend({
                id: null,
                title: '系统窗口',
                width: "100px",
                height: "100px",
                content: '',
                btn: ['确认', '关闭'],
                callBack: null
            }, opt);
            top.layer.open({
                id: opt.id,
                type: 1,
                title: opt.title,
                fix: false,
                area: [opt.width, opt.height],
                content: opt.content,
                btn: opt.btn,
                yes: function () {
                    opt.callBack(opt.id);
                }
            });
        },
        dialogAlert: function (opt) {
            if (opt.type == -1) {
                opt.type = 2;
            }
            top.layer.alert(opt.msg, {
                icon: opt.type,
                title: "力软提示"
            });
        },
        dialogConfirm: function (opt) {
            top.layer.confirm(opt.msg, {
                icon: 7,
                title: "力软提示",
                btn: ['确认', '取消'],
            }, function () {
                opt.callBack(true);
            }, function () {
                opt.callBack(false);
            });
        },
        dialogMsg: function (opt) {
            if (opt.type == -1) {
                opt.type = 2;
            }
            top.layer.msg(opt.msg, { icon: opt.type, time: 4000, shift: 5 });
        },
        dialogClose: function () {
            
            try {
                var index = top.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                var $IsdialogClose = top.$("#layui-layer" + index).find('.layui-layer-btn').find("#IsdialogClose");
                var IsClose = $IsdialogClose.is(":checked");
                if ($IsdialogClose.length == 0) {
                    IsClose = true;
                }
                if (IsClose) {
                    top.layer.close(index);
                } else {
                    location.reload();
                }
            } catch (e) {
                alert(e)
            }
        },
        //下载文件
        downFile: function (opt) {
            if (opt.url && opt.data) {
                opt.data = typeof opt.data == 'string' ? opt.data : jQuery.param(opt.data);
                var inputs = '';
                $.each(opt.data.split('&'), function () {
                    var pair = this.split('=');
                    inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
                });
                $('<form action="' + url + '" method="' + (opt.method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove();
            };
        },
        //获取url参数值
        request: function (keyValue) {
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
        },
        //改变url参数值
        changeUrlParam: function (url, key, value) {
            var newUrl = "";
            var reg = new RegExp("(^|)" + key + "=([^&]*)(|$)");
            var tmp = key + "=" + value;
            if (url.match(reg) != null) {
                newUrl = url.replace(eval(reg), tmp);
            } else {
                if (url.match("[\?]")) {
                    newUrl = url + "&" + tmp;
                }
                else {
                    newUrl = url + "?" + tmp;
                }
            }
            return newUrl;
        },
        //获取游览器名称
        getBrowserName: function () {
            var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
            var isOpera = userAgent.indexOf("Opera") > -1;
            if (isOpera) {
                return "Opera"
            }; //判断是否Opera浏览器
            if (userAgent.indexOf("Firefox") > -1) {
                return "FF";
            } //判断是否Firefox浏览器
            if (userAgent.indexOf("Chrome") > -1) {
                if (window.navigator.webkitPersistentStorage.toString().indexOf('DeprecatedStorageQuota') > -1) {
                    return "Chrome";
                } else {
                    return "360";
                }
            }//判断是否Chrome浏览器//360浏览器
            if (userAgent.indexOf("Safari") > -1) {
                return "Safari";
            } //判断是否Safari浏览器
            if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) {
                return "IE";
            }//判断是否IE浏览器
        },
        //改变树状tab状态
        changeStandTab: function (opt) {
            $(".standtabactived").removeClass("standtabactived");
            $(opt.obj).addClass("standtabactived");
            $('.standtab-pane').css('display', 'none');
            $('#' + opt.id).css('display', 'block');
        },
        //获取窗口宽
        windowWidth: function () {
            return $(window).width();
        },
        //获取窗口高度
        windowHeight: function () {
            return $(window).height();
        },
        //ajax通信方法
        ajax: {
            asyncGet: function (opt) {
                var data = null;
                var opt = $.extend({
                    type: "GET",
                    dataType: "json",
                    async: false,
                    success: function (d) {
                        data = d;
                    }
                }, opt);
                $.ajax(opt);
                return data;
            }
        },
        //创建一个GUID
        createGuid: function () {
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20)) guid += "-";
            }
            return guid;
        },
        //判断是否为空
        isNullOrEmpty: function (obj) {
            if ((typeof (obj) == "string" && obj == "") || obj == null || obj == undefined) {
                return true;
            }
            else {
                return false;
            }
        },
        //判断是否为数字
        isNumber: function (obj) {
            $("#" + obj).bind("contextmenu", function () {
                return false;
            });
            $("#" + obj).css('ime-mode', 'disabled');
            $("#" + obj).keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        },
        //判断是否是金钱
        isMoney: function (obj) {
            $("#" + obj).bind("contextmenu", function () {
                return false;
            });
            $("#" + obj).css('ime-mode', 'disabled');
            $("#" + obj).bind("keydown", function (e) {
                var key = window.event ? e.keyCode : e.which;
                if (isFullStop(key)) {
                    return $(this).val().indexOf('.') < 0;
                }
                return (isSpecialKey(key)) || ((isNumber(key) && !e.shiftKey));
            });
            function isNumber(key) {
                return key >= 48 && key <= 57
            }
            function isSpecialKey(key) {
                return key == 8 || key == 46 || (key >= 37 && key <= 40) || key == 35 || key == 36 || key == 9 || key == 13
            }
            function isFullStop(key) {
                return key == 190 || key == 110;
            }
        },
        //日期格式化yyyy-
        formatDate : function (v, format) {
            if (!v) return "";
            var d = v;
            if (typeof v === 'string') {
                if (v.indexOf("/Date(") > -1)
                    d = new Date(parseInt(v.replace("/Date(", "").replace(")/", ""), 10));
                else
                    d = new Date(Date.parse(v.replace(/-/g, "/").replace("T", " ").split(".")[0]));//.split(".")[0] 用来处理出现毫秒的情况，截取掉.xxx，否则会出错
            }
            var o = {
                "M+": d.getMonth() + 1,  //month
                "d+": d.getDate(),       //day
                "h+": d.getHours(),      //hour
                "m+": d.getMinutes(),    //minute
                "s+": d.getSeconds(),    //second
                "q+": Math.floor((d.getMonth() + 3) / 3),  //quarter
                "S": d.getMilliseconds() //millisecond
            };
            if (/(y+)/.test(format)) {
                format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
            }
            for (var k in o) {
                if (new RegExp("(" + k + ")").test(format)) {
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
                }
            }
            return format;
        },
        //转化成十进制
        toDecimal: function (num) {
            if (num == null) {
                num = "0";
            }
            num = num.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "0";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            cents = num % 100;
            num = Math.floor(num / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                num = num.substring(0, num.length - (4 * i + 3)) + '' +
                        num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + num + '.' + cents);
        },
        //数组复制
        arrayCopy: function (data) {
            return $.map(data, function (obj) {
                return $.extend(true, {}, obj);
            });
        },
        //检验是否选中行
        checkedRow: function (id) {
            var isOK = true;
            if (id == undefined || id == "" || id == 'null' || id == 'undefined') {
                isOK = false;
                uiBase.dialogMsg({ msg: '您没有选中任何数据项,请选中后再操作！', type: 0 });
            } else if (id.split(",").length > 1) {
                isOK = false;
                uiBase.dialogMsg({ msg: '很抱歉,一次只能选择一条记录！', type: 0 });
            }
            return isOK;
        },
        //表单操作
        saveForm: function (opt) {
            var opt = $.extend({
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                loading: "正在处理数据...",
                success: null,
                close: true
            }, opt);
            uiBase.loading({ isShow: true, text: opt.loading });
            if ($('[name=__RequestVerificationToken]').length > 0) {
                opt.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
            }
            window.setTimeout(function () {
                $.ajax({
                    url: opt.url,
                    data: opt.param,
                    type: opt.type,
                    dataType: opt.dataType,
                    success: function (data) {
                        if (data.type == "3") {
                            uiBase.dialogAlert({ msg: data.message, type: -1 });
                        } else {
                            uiBase.loading({ isShow: false });
                            uiBase.dialogMsg({ msg: data.message, type: 1 });
                            opt.success(data);
                            if (opt.close == true) {
                                uiBase.dialogClose();
                            }
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        uiBase.loading({ isShow: false });
                        uiBase.dialogMsg({ msg: errorThrown, type: -1 });
                    },
                    beforeSend: function () {
                        uiBase.loading({ isShow: true, text: opt.loading });
                    },
                    complete: function () {
                        uiBase.loading({ isShow: false });
                    }
                });
            }, 500);
        },
        setForm: function (opt) {
            var opt = $.extend({
                url: "",
                param: [],
                type: "get",
                dataType: "json",
                success: null,
                async:false
            }, opt);
            $.ajax({
                url: opt.url,
                data: opt.param,
                type: opt.type,
                dataType: opt.dataType,
                async: opt.async,
                success: function (data) {
                    if (data != null && data.type == "3") {
                        uiBase.dialogAlert({ msg: data.message, type: -1 });
                    } else {
                        opt.success(data);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    uiBase.dialogMsg({ msg: errorThrown, type: -1 });
                }, beforeSend: function () {
                    uiBase.loading({ isShow: true });
                },
                complete: function () {
                    uiBase.loading({ isShow: false });
                }
            });
        },
        removeForm: function (opt) {
            var opt = $.extend( {
                msg: "注：您确定要删除吗？该操作将无法恢复",
                loading: "正在删除数据...",
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                success: null
            }, opt);
            uiBase.dialogConfirm({
                msg:opt.msg,
                callBack: function (r) {
                    if (r) {
                        uiBase.loading({ isShow: true, text: opt.loading });
                        window.setTimeout(function () {
                            var postdata = opt.param;
                            if ($('[name=__RequestVerificationToken]').length > 0) {
                                postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
                            }
                            $.ajax({
                                url: opt.url,
                                data: postdata,
                                type: opt.type,
                                dataType: opt.dataType,
                                success: function (data) {
                                    if (data.type == "3") {
                                        uiBase.dialogAlert({ msg: data.message, type: -1 });
                                    } else {
                                        uiBase.dialogMsg({ msg: data.message, type: 1 });
                                        opt.success(data);
                                    }
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    uiBase.loading({ isShow: false });
                                    uiBase.dialogMsg({ msg: errorThrown, type: -1 });
                                },
                                beforeSend: function () {
                                    uiBase.loading({ isShow: true, text: opt.loading });
                                },
                                complete: function () {
                                    uiBase.loading({ isShow: false });
                                }
                            });
                        }, 500);
                    }
                }
            });
        },
        confirmAjax: function (opt) {
            var opt = $.extend({
                msg: "提示信息",
                loading: "正在处理数据...",
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                success: null
            }, opt);
            uiBase.dialogConfirm({
                msg: opt.msg,
                callBack: function (r) {
                    if (r) {
                        uiBase.loading({ isShow: true, text: opt.loading });
                        window.setTimeout(function () {
                            var postdata = opt.param;
                            if ($('[name=__RequestVerificationToken]').length > 0) {
                                postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
                            }
                            $.ajax({
                                url: opt.url,
                                data: postdata,
                                type: opt.type,
                                dataType: opt.dataType,
                                success: function (data) {
                                    uiBase.loading({ isShow: false });
                                    if (data.type == "3") {
                                        uiBase.dialogAlert({ msg: data.message, type: -1 });
                                    } else {
                                        uiBase.dialogMsg({ msg: data.message, type: 1 });
                                        opt.success(data);
                                    }
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    uiBase.loading({ isShow: false });
                                    uiBase.dialogMsg({ msg: errorThrown, type: -1 });
                                },
                                beforeSend: function () {
                                    uiBase.loading({ isShow: true, text: opt.loading });
                                },
                                complete: function () {
                                    uiBase.loading({ isShow: false });
                                }
                            });
                        }, 200);
                    }
                }
            });
        },
        existField:function (controlId, url, param) {
            var $control = $("#" + controlId);
            if (!$control.val()) {
                return false;
            }
            var data = {
                keyValue: uiBase.request('keyValue')
            };
            data[controlId] = $control.val();
            var options = $.extend(data, param);
            $.ajax({
                url: url,
                data: options,
                type: "get",
                dataType: "text",
                async: false,
                success: function (data) {
                    if (data.toLocaleLowerCase() == 'false') {
                        ValidationMessage($control, '已存在,请重新输入');
                        $control.attr('fieldexist', 'yes');
                    } else {
                        $control.attr('fieldexist', 'no');
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    uiBase.dialogMsg({ msg: errorThrown, type: -1 });
                }
            });
        },
        getDataForm:function (opt)
        {
            var opt = $.extend({
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                loading: "正在获取数据...",
                success: null,
                async: false
            }, opt);
            uiBase.loading({ isShow: true, text: opt.loading });
            if ($('[name=__RequestVerificationToken]').length > 0) {
                opt.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
            }
            $.ajax({
                url: opt.url,
                data: opt.param,
                type: opt.type,
                dataType: opt.dataType,
                async: opt.async,
                success: function (data) {
                    if (data != null && data.type == "3") {
                        uiBase.dialogAlert({ msg: data.message, type: -1 });
                    } else {
                        opt.success(data);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    uiBase.dialogMsg({ msg: errorThrown, type: -1 });
                }, beforeSend: function () {
                    uiBase.loading({ isShow: true });
                },
                complete: function () {
                    uiBase.loading({ isShow: false });
                }
            });
        },
        //系统表单函数
        getSysFormData: function (Id) {
            var formIframe = uiBase.getIframe(Id);
            formIframe.$('body').find('[data-wfhide]').hide();
            if (!!formIframe.getWfData) {
                return formIframe.getWfData();//{ "field": id, "label": name, 'type': type, 'girdId': girdId }
            }
            else {
                return [];
            }
        },
        setSysFormData: function (Id, url) {
            var _iframe = document.getElementById(Id);
            var _formIframeLoaded = function () {
                var formIframe = uiBase.getIframe(Id);
                formIframe.$('body').find('[data-wfhide]').hide();
                uiBase.loading({"isShow":false});
            };
            if (_iframe.attachEvent) {
                _iframe.attachEvent("onload", _formIframeLoaded);
            } else {
                _iframe.onload = _formIframeLoaded;
                $('#' + Id).attr('src', url);
            }
        },
        //创建一个流程
        createProcess: function (postData, callBack) {
            postData.processId = uiBase.createGuid();
            postData.moduleId = top.$.cookie('currentmoduleId');

            uiBase.getDataForm({
                url: "../../FlowManage/FlowLaunch/CreateProcessInstance",
                param: postData,
                loading: "正在创建流程",
                success: function () {
                    callBack(postData.processId);
                }
            });
        }
    }

    //下拉框
    $.fn.comboBox = function (opt) {//下拉框
        var $select = $(this);
        var selectId = $select.attr('id');
        if (!selectId) {
            return false;
        }
        var opt = $.extend({
            //请选择
            description: "==请选择==",
            //字段
            id: "id",
            text: "text",
            title: "title",
            //展开最大高度
            maxHeight: null,
            //宽度
            width: null,
            //是否允许搜索
            allowSearch: false,
            //访问数据接口地址
            url: null,
            //访问数据接口参数
            param: null,
            //下拉选择数据
            data: null
        }, opt);
        if ($select.removeAttr("data-value")) {

            $select.removeAttr("data-value");
            $select.removeAttr("data-text");
            $select.find('.ui-select-text').remove();

        }
        var dom = {
            rendering: function () {
                if ($select.find('.ui-select-text').length == 0) {
                    $select.html("<div class=\"ui-select-text\" style='color:#999;'>" + opt.description + "</div>");
                }
                //渲染下拉选项框
                var optionHtml = "<div class=\"ui-select-option\">";
                optionHtml += "<div class=\"ui-select-option-content\" style=\"max-height: " + opt.maxHeight + "\"></div>";
                if (opt.allowSearch) {
                    optionHtml += "<div class=\"ui-select-option-search\"><input type=\"text\" class=\"form-control\" placeholder=\"搜索关键字，按回车查询\" /><span class=\"input-query\" title=\"Search\"><i class=\"fa fa-search\" title=\"按回车查询\"></i></span></div>";
                }
                optionHtml += "</div>";
                var $optionHtml = $(optionHtml);
                $optionHtml.attr('id', selectId + '-option');
                
                var $obj = $("#" + selectId + "-option");
                if ($obj.length != undefined && $obj.length == 1) {
                    if (opt.maxHeight != null)
                    {
                        $obj.find('.ui-select-option-content').css('max-height', opt.maxHeight);
                    }
                    if (opt.allowSearch) {
                        if ($obj.find('.ui-select-option-search').length != 1)
                        {
                            $obj.append("<div class=\"ui-select-option-search\"><input type=\"text\" class=\"form-control\" placeholder=\"搜索关键字，按回车查询\" /><span class=\"input-query\" title=\"Search\"><i class=\"fa fa-search\" title=\"按回车查询\"></i></span></div>");
                        }
                    }
                }
                else {
                    $('body').prepend($optionHtml);
                }
               
                return $("#" + selectId + "-option");
            },
            renderingData: function ($option, setting, searchValue) {
                if (setting.data != undefined && setting.data.length > 0) {
                    var $_html = $('<ul></ul>');
                    if (setting.description) {
                        $_html.append('<li data-value="">' + setting.description + '</li>');
                    }
                    $.each(setting.data, function (i, row) {
                        var title = row[setting.title];
                        if (title == undefined) {
                            title = "";
                        }
                        if (searchValue != undefined) {
                            if (row[setting.text].indexOf(searchValue) != -1) {
                                $_html.append('<li data-value="' + row[setting.id] + '" title="' + title + '">' + row[setting.text] + '</li>');
                            }
                        }
                        else {
                            $_html.append('<li data-value="' + row[setting.id] + '" title="' + title + '">' + row[setting.text] + '</li>');
                        }
                    });
                    $option.find('.ui-select-option-content').html($_html);
                    $option.find('li').css('padding', "0 5px");
                    $option.find('li').click(function (e) {
                        var $this = $(this);
                        $select.attr("data-value", $this.attr('data-value')).attr("data-text", $this.text());
                        $select.find('.ui-select-text').html($this.text()).css('color', '#000');
                        $option.slideUp(150);
                        $select.trigger("change");
                        e.stopPropagation();
                    }).hover(function (e) {
                        if (!$(this).hasClass('liactive')) {
                            $(this).toggleClass('on');
                        }
                        e.stopPropagation();
                    });
                }
            },
            loadData: function () {
                if (!!opt.url) {
                    opt.data = uiBase.ajax.asyncGet({
                        url: opt.url,
                        data: opt.param,
                    });
                }
                else {
                    var $lilists = $select.find('li');
                    if ($lilists.length > 0) {
                        opt.data = [];
                        $lilists.each(function (e) {
                            var $li = $(this);
                            var point = {};
                            point[opt.id] = $li.attr('data-value');
                            point[opt.title] = $li.attr('title');
                            point[opt.text] = $li.html();
                            opt.data.push(point);
                        });
                    }
                }
            }
        };
        dom.loadData();
        var $option = dom.rendering();
        dom.renderingData($option, opt);

        //操作搜索事件
        if (opt.allowSearch) {
            $option.find('.ui-select-option-search').find('input').bind("keypress", function (e) {
                e = event ? event : (window.event ? window.event : null);
                if (e.keyCode == "13") {
                    var $this = $(this);
                    dom.renderingData($option, $this[0].opt, $this.val());
                }
            }).focus(function () {
                $(this).select();
            })[0]["opt"] = opt;
        }

        $select.unbind('click');
        $select.bind("click", function (e) {
            if ($select.attr('readonly') == 'readonly' || $select.attr('disabled') == 'disabled') {
                return false;
            }
            $(this).addClass('ui-select-focus');
            if ($option.is(":hidden")) {
                $select.find('.ui-select-option').hide();
                $('.ui-select-option').hide();
                var left = $select.offset().left;
                var top = $select.offset().top + 29;
                var width = $select.width();
                if (opt.width) {
                    width = opt.width;
                }
                if (($option.height() + top) < $(window).height()) {
                    $option.slideDown(150).css({ top: top, left: left, width: width });
                } else {
                    var _top = (top - $option.height() - 32)
                    $option.show().css({ top: _top, left: left, width: width });
                    $option.attr('data-show', true);
                }
                $option.css('border-top', '1px solid #ccc');
                $option.find('li').removeClass('liactive');
                $option.find('[data-value=' + $select.attr('data-value') + ']').addClass('liactive');
                $option.find('.ui-select-option-search').find('input').select();
            } else {
                if ($option.attr('data-show')) {
                    $option.hide();
                } else {
                    $option.slideUp(150);
                }
            }
            e.stopPropagation();
        });
        $(document).click(function (e) {
            var e = e ? e : window.event;
            var tar = e.srcElement || e.target;
            if (!$(tar).hasClass('form-control')) {
                if ($option.attr('data-show')) {
                    $option.hide();
                } else {
                    $option.slideUp(150);
                }
                $select.removeClass('ui-select-focus');
                e.stopPropagation();
            }
        });
        return $select;
    };
    $.fn.comboBoxSetValue = function (value) {
        if (uiBase.isNullOrEmpty(value)) {
            return;
        }
        
        var $select = $(this);
        var $option = $("#" + $select.attr('id') + "-option");
        $select.attr('data-value', value);
        var data_text = $option.find('ul').find('[data-value=' + value + ']').html();
        if (data_text) {
            $select.attr('data-text', data_text);
            $select.find('.ui-select-text').html(data_text).css('color', '#000');
            $option.find('ul').find('[data-value=' + value + ']').addClass('liactive')
        }
        return $select;
    };
    //下拉框树形
    $.fn.comboBoxTree = function (opt) {
        //opt参数：description,height,allowSearch,appendTo,click,url,param,method,icon
        var $select = $(this);
        var selectId = $select.attr('id');
        if (!selectId) {
            return false;
        }

        var opt = $.extend({
            //请选择
            description: "==请选择==",
            //字段
            id: "id",
            text: "text",
            title: "title",
            //展开最大高度
            maxHeight: null,
            //宽度
            width: null,
            //是否允许搜索
            allowSearch: false,
            //访问数据接口地址
            url: false,
            //访问数据接口参数
            param: null,
            //接口请求的方法
            method: "GET",
            //加载到哪个标签里面
            appendTo: null,
            //选择点击事件
            click: null,
            //是否移除图标
            icon: false,
            data: null,
        }, opt);

        var dom = {
            rendering: function () {
                if ($select.find('.ui-select-text').length == 0) {
                    $select.html("<div class=\"ui-select-text\" style='color:#999;'>" + opt.description + "</div>");
                }
                //渲染下拉选项框
                var optionHtml = "<div class=\"ui-select-option\">";
                optionHtml += "<div class=\"ui-select-option-content\" style=\"max-height: " + opt.maxHeight + "\"></div>";
                if (opt.allowSearch) {
                    optionHtml += "<div class=\"ui-select-option-search\"><input type=\"text\" class=\"form-control\" placeholder=\"搜索关键字，按回车查询\" /><span class=\"input-query\" title=\"Search\"><i class=\"fa fa-search\" title=\"按回车查询\"></i></span></div>";
                }
                optionHtml += "</div>";
                var $optionHtml = $(optionHtml);
                $optionHtml.attr('id', selectId + '-option');
                if (opt.appendTo) {
                    $(opt.appendTo).prepend($optionHtml);
                } else {
                    $('body').prepend($optionHtml);
                }
                return $("#" + selectId + "-option");
            },
            loadtreeview: function (setting) {
                $option_content.treeview({
                    onnodeclick: function (item) {
                        $select.attr("data-value", item.id).attr("data-text", item.text);
                        $select.find('.ui-select-text').html(item.text).css('color', '#000');
                        $select.trigger("change");
                        if (setting.click) {
                            setting.click(item);
                        }
                    },
                    height: setting.maxHeight,
                    url: setting.url,
                    param: setting.param,
                    method: setting.method,
                    data:setting.data,
                    description: setting.description
                });
                
            }
        };

        var $option = dom.rendering();
        var $option_content = $("#" + selectId + "-option").find('.ui-select-option-content');
        dom.loadtreeview(opt);

        if (opt.allowSearch) {
            $option.find('.ui-select-option-search').find('input').attr('data-url', opt.url);
            $option.find('.ui-select-option-search').find('input').bind("keypress", function (e) {
                e = event ? event : (window.event ? window.event : null);
                if (e.keyCode == "13") {
                    var $this = $(this);
                    var value = $(this).val();
                    $this[0].opt.url = changeUrlParam($option.find('.ui-select-option-search').find('input').attr('data-url'), "keyword", escape(value));
                    dom.loadtreeview($this[0].opt);
                }
            }).focus(function () {
                $(this).select();
            })[0]["opt"] = opt;;
        }
        if (opt.icon) {
            $option.find('i').remove();
            $option.find('img').remove();
        }
        $select.find('.ui-select-text').unbind('click');
        $select.find('.ui-select-text').bind("click", function (e) {
            if ($select.attr('readonly') == 'readonly' || $select.attr('disabled') == 'disabled') {
                return false;
            }
            $(this).parent().addClass('ui-select-focus');
            if ($option.is(":hidden")) {
                $select.find('.ui-select-option').hide();
                $('.ui-select-option').hide();
                var left = $select.offset().left;
                var top = $select.offset().top + 29;
                var width = $select.width();
                if (opt.width) {
                    width = opt.width;
                }
                if (($option.height() + top) < $(window).height()) {
                    $option.slideDown(150).css({ top: top, left: left, width: width });
                } else {
                    var _top = (top - $option.height() - 32);
                    $option.show().css({ top: _top, left: left, width: width });
                    $option.attr('data-show', true);
                }
                $option.css('border-top', '1px solid #ccc');
                if (opt.appendTo) {
                    $option.css("position", "inherit")
                }
                $option.find('.ui-select-option-search').find('input').select();
            } else {
                if ($option.attr('data-show')) {
                    $option.hide();
                } else {
                    $option.slideUp(150);
                }
            }
            e.stopPropagation();
        });
        $select.find('li div').click(function (e) {
            var e = e ? e : window.event;
            var tar = e.srcElement || e.target;
            if (!$(tar).hasClass('bbit-tree-ec-icon')) {
                $option.slideUp(150);
                e.stopPropagation();
            }
        });
        $(document).click(function (e) {
            var e = e ? e : window.event;
            var tar = e.srcElement || e.target;
            if (!$(tar).hasClass('bbit-tree-ec-icon') && !$(tar).hasClass('form-control')) {
                if ($option.attr('data-show')) {
                    $option.hide();
                } else {
                    $option.slideUp(150);
                }
                $select.removeClass('ui-select-focus');
                e.stopPropagation();
            }
        });
        return $select;
    };
    $.fn.comboBoxTreeSetValue = function (value) {
        if (uiBase.isNullOrEmpty(value)) {
            return;
        }
        var $select = $(this);
        var $option = $("#" + $select.attr('id') + "-option");
        $select.attr('data-value', value);
        var data_text = $option.find('ul').find('[data-value=' + value + ']').html();
        if (data_text) {
            $select.attr('data-text', data_text);
            $select.find('.ui-select-text').html(data_text).css('color', '#000');
            $option.find('ul').find('[data-value=' + value + ']').parent().parent().addClass('bbit-tree-selected');
        }
        return $select;
    };
    //获取、设置表单数据
    $.fn.getWebControls = function (keyValue) {
        var reVal = "";
        $(this).find('input,select,textarea,.ui-select').each(function (r) {
            var id = $(this).attr('id');
            var type = $(this).attr('type');
            switch (type) {
                case "checkbox":
                    if ($("#" + id).is(":checked")) {
                        reVal += '"' + id + '"' + ':' + '"1",'
                    } else {
                        reVal += '"' + id + '"' + ':' + '"0",'
                    }
                    break;
                case "select":
                    var value = $("#" + id).attr('data-value');
                    if (value == "") {
                        value = "&nbsp;";
                    }
                    reVal += '"' + id + '"' + ':' + '"' + $.trim(value) + '",'
                    break;
                case "selectTree":
                    var value = $("#" + id).attr('data-value');
                    if (value == "") {
                        value = "&nbsp;";
                    }
                    reVal += '"' + id + '"' + ':' + '"' + $.trim(value) + '",'
                    break;
                default:
                    var value = $("#" + id).val();
                    if (value == "") {
                        value = "&nbsp;";
                    }
                    reVal += '"' + id + '"' + ':' + '"' + $.trim(value) + '",'
                    break;
            }
        });
        reVal = reVal.substr(0, reVal.length - 1);
        if (!keyValue) {
            reVal = reVal.replace(/&nbsp;/g, '');
        }
        reVal = reVal.replace(/\\/g, '\\\\');
        reVal = reVal.replace(/\n/g, '\\n');
        var postdata = jQuery.parseJSON('{' + reVal + '}');
        return postdata;
    };
    $.fn.setWebControls = function (data) {
        var $id = $(this)
        for (var key in data) {
            var id = $id.find('#' + key);
            if (id.attr('id')) {
                var type = id.attr('type');
                if (id.hasClass("input-datepicker")) {
                    type = "datepicker";
                }
                var value = $.trim(data[key]).replace(/&nbsp;/g, '');
                switch (type) {
                    case "checkbox":
                        if (value == 1) {
                            id.attr("checked", 'checked');
                        } else {
                            id.removeAttr("checked");
                        }
                        break;
                    case "select":
                        id.comboBoxSetValue(value);
                        break;
                    case "selectTree":
                        id.comboBoxTreeSetValue(value);
                        break;
                    case "datepicker":
                        id.val(formatDate(value, 'yyyy-MM-dd'));
                        break;
                    default:
                        id.val(value);
                        break;
                }
            }
        }
    };
    $.fn.getSysFormControls = function () {
        var postdata = [];
        $(this).find('[data-wfname]').each(function (r) {
            var $obj = $(this);
            var name = $obj.attr('data-wfname');
            var id = $obj.attr('id');
            var type = $obj.attr('type');
            if (id == undefined)
            {
                id = $obj.attr('data-id');
            }
            var girdId = $obj.attr('data-girdid');
            postdata.push({ "field": id, "label": name, 'type': type, 'girdId': girdId });
        });
        return postdata;
    };
    //右键菜单
    $.fn.conTextMenu = function () {
        var element = $(this);
        var oMenu = $('.contextmenu');
        $(document).click(function () {
            oMenu.hide();
        });
        $(document).mousedown(function (e) {
            if (3 == e.which) {
                oMenu.hide();
            }
        })
        var aUl = oMenu.find("ul");
        var aLi = oMenu.find("li");
        var showTimer = hideTimer = null;
        var i = 0;
        var maxWidth = maxHeight = 0;
        var aDoc = [document.documentElement.offsetWidth, document.documentElement.offsetHeight];
        oMenu.hide();
        for (i = 0; i < aLi.length; i++) {
            //为含有子菜单的li加上箭头
            aLi[i].getElementsByTagName("ul")[0] && (aLi[i].className = "sub");
            //鼠标移入
            aLi[i].onmouseover = function () {
                var oThis = this;
                var oUl = oThis.getElementsByTagName("ul");
                //鼠标移入样式
                oThis.className += " active";
                //显示子菜单
                if (oUl[0]) {
                    clearTimeout(hideTimer);
                    showTimer = setTimeout(function () {
                        for (i = 0; i < oThis.parentNode.children.length; i++) {
                            oThis.parentNode.children[i].getElementsByTagName("ul")[0] &&
                            (oThis.parentNode.children[i].getElementsByTagName("ul")[0].style.display = "none");
                        }
                        oUl[0].style.display = "block";
                        oUl[0].style.top = oThis.offsetTop + "px";
                        oUl[0].style.left = oThis.offsetWidth + "px";

                        //最大显示范围					
                        maxWidth = aDoc[0] - oUl[0].offsetWidth;
                        maxHeight = aDoc[1] - oUl[0].offsetHeight;

                        //防止溢出
                        maxWidth < getOffset.left(oUl[0]) && (oUl[0].style.left = -oUl[0].clientWidth + "px");
                        maxHeight < getOffset.top(oUl[0]) && (oUl[0].style.top = -oUl[0].clientHeight + oThis.offsetTop + oThis.clientHeight + "px")
                    }, 300);
                }
            };
            //鼠标移出	
            aLi[i].onmouseout = function () {
                var oThis = this;
                var oUl = oThis.getElementsByTagName("ul");
                //鼠标移出样式
                oThis.className = oThis.className.replace(/\s?active/, "");

                clearTimeout(showTimer);
                hideTimer = setTimeout(function () {
                    for (i = 0; i < oThis.parentNode.children.length; i++) {
                        oThis.parentNode.children[i].getElementsByTagName("ul")[0] &&
                        (oThis.parentNode.children[i].getElementsByTagName("ul")[0].style.display = "none");
                    }
                }, 300);
            };
        }
        //自定义右键菜单
        $(element).bind("contextmenu", function () {
            var event = event || window.event;
            oMenu.show();
            oMenu.css('top', event.clientY + "px");
            oMenu.css('left', event.clientX + "px");
            //最大显示范围
            maxWidth = aDoc[0] - oMenu.width();
            maxHeight = aDoc[1] - oMenu.height();
            //防止菜单溢出
            if (oMenu.offset().top > maxHeight) {
                oMenu.css('top', maxHeight + "px");
            }
            if (oMenu.offset().left > maxWidth) {
                oMenu.css('left', maxWidth + "px");
            }
            return false;
        }).bind("click", function () {
            oMenu.hide();
        });
    };
    //翻页插件扩展
    $.fn.panginationEx = function (opt) {
        var $pager = $(this);
        if (!$pager.attr('id')) {
            return false;
        }
        var opt = $.extend({
            firstBtnText: '首页',
            lastBtnText: '尾页',
            prevBtnText: '上一页',
            nextBtnText: '下一页',
            showInfo: true,
            showJump: true,
            jumpBtnText: '跳转',
            showPageSizes: true,
            infoFormat: '{start} ~ {end}条，共{total}条',
            sortname: '',
            url: "",
            success: null,
            beforeSend: null,
            complete: null
        }, opt);
        var params = $.extend({ sidx: opt.sortname, sord: "asc" }, opt.params);
        opt.remote = {
            url: opt.url,  //请求地址
            params: params,       //自定义请求参数
            beforeSend: function (XMLHttpRequest) {
                if (opt.beforeSend != null) {
                    opt.beforeSend(XMLHttpRequest);
                }
            },
            success: function (result, pageIndex) {
                //回调函数
                //result 为 请求返回的数据，呈现数据
                if (opt.success != null) {
                    opt.success(result.rows, pageIndex);
                }
            },
            complete: function (XMLHttpRequest, textStatu) {
                if (opt.complete != null) {
                    opt.complete(XMLHttpRequest, textStatu);
                }
                //...
            },
            pageIndexName: 'page',    //请求参数，当前页数，索引从0开始
            pageSizeName: 'rows',     //请求参数，每页数量
            totalName: 'records'      //指定返回数据的总数据量的字段名
        };
        $pager.page(opt);
    };
    //发送邮件左侧列表项
    $.fn.leftListShowOfEmail = function (opt) {
        var $list = $(this);
        if (!$list.attr('id')) {
            return false;
        }
        $list.append('<ul  style="padding-top: 10px;"></ul>');
        var opt = $.extend({
            id: "id",
            name: "text",
            img: "fa fa-file-o",

        }, opt);
        $list.height(opt.height);
        $.ajax({
            url: opt.url,
            data: opt.param,
            type: "GET",
            dataType: "json",
            async: false,
            success: function (data) {
                $.each(data, function (i, item) {
                    var $_li = $('<li class="" data-value="' + item[opt.id] + '"  data-text="' + item[opt.name] + '" ><i class="' + opt.img + '" style="vertical-align: middle; margin-top: -2px; margin-right: 8px; font-size: 14px; color: #666666; opacity: 0.9;"></i>' + item[opt.name] + '</li>');
                    if (i == 0) {
                        $_li.addClass("active");
                    }
                    $list.find('ul').append($_li);
                });
                $list.find('li').click(function () {
                    var key = $(this).attr('data-value');
                    var value = $(this).attr('data-text');
                    $list.find('li').removeClass('active');
                    $(this).addClass('active');
                    opt.onnodeclick({ id: key, name: value });
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                dialogMsg(errorThrown, -1);
            }
        });
    };
    //权限按钮、列表数据列
    $.fn.authorizeButton = function () {
        var $element = $(this);
        $element.find('a.btn').attr('authorize', 'no')
        $element.find('ul.dropdown-menu').find('li').attr('authorize', 'no')
        var moduleId = uiBase.tabiframeId().substr(6);
        var data = top.authorizeButtonData[moduleId];
        if (data != undefined) {
            $.each(data, function (i) {
                $element.find("#" + data[i].F_EnCode).attr('authorize', 'yes');
            });
        }
        $element.find('[authorize=no]').remove();
    };
    $.fn.authorizeColModel = function () {
        var $element = $(this);
        var columnModel = $element.jqGrid('getGridParam', 'colModel');
        $.each(columnModel, function (i) {
            if (columnModel[i].name != "rn") {
                $element.hideCol(columnModel[i].name);
            }
        });
        var moduleId = tabiframeId().substr(6);
        var data = top.authorizeColumnData[moduleId];
        if (data != undefined) {
            $.each(data, function (i) {
                $element.showCol(data[i].F_EnCode);
            });
        }
    };
    //jqgird
    $.fn.jqGridEx = function (opt) {
        var $jqGrid = $(this);
        var _selectedRowIndex;
        if (!$jqGrid.attr('id')) {
            return false;
        }
        var opt = $.extend({
            url: "",
            datatype: "json",
            height: $(window).height() - 139.5,
            autowidth: true,
            colModel: [],
            viewrecords: true,
            rowNum: 30,
            rowList: [30, 50, 100],
            pager: "#gridPager",
            sortname: 'F_CreateDate desc',
            rownumbers: true,
            shrinkToFit: false,
            gridview: true,
            onSelectRow: function () {
                _selectedRowIndex = $("#" + this.id).getGridParam('selrow');
            },
            gridComplete: function () {
                $("#" + this.id).setSelection(_selectedRowIndex, false);
            }
        }, opt);
        $jqGrid.jqGrid(opt);
    };
    $.fn.jqGridRowValue = function (code) {
        var $jgrid = $(this);
        var json = [];
        var selectedRowIds = $jgrid.jqGrid("getGridParam", "selarrrow");
        if (selectedRowIds != undefined && selectedRowIds != "") {
            var len = selectedRowIds.length;
            for (var i = 0; i < len ; i++) {
                var rowData = $jgrid.jqGrid('getRowData', selectedRowIds[i]);
                json.push(rowData[code]);
            }
        } else {
            var rowData = $jgrid.jqGrid('getRowData', $jgrid.jqGrid('getGridParam', 'selrow'));
            json.push(rowData[code]);
        }
        return String(json);
    };
    $.fn.jqGridRow = function () {
        var $jgrid = $(this);
        var json = [];
        var selectedRowIds = $jgrid.jqGrid("getGridParam", "selarrrow");
        if (selectedRowIds != "") {
            var len = selectedRowIds.length;
            for (var i = 0; i < len ; i++) {
                var rowData = $jgrid.jqGrid('getRowData', selectedRowIds[i]);
                json.push(rowData);
            }
        } else {
            var rowData = $jgrid.jqGrid('getRowData', $jgrid.jqGrid('getGridParam', 'selrow'));
            json.push(rowData);
        }
        return json;
    };
    //附件上传插件初始化
    //附件上传插件初始化
    $.fn.leaUploadify = function (opt) {
        var $leaUploadify = $(this);
        var leaUploadifyId = $leaUploadify.attr('id');
        if (!leaUploadifyId) {
            return false;
        }
        var opt = $.extend({
            btnName: "上传附件",
            url: "",
            onUploadSuccess: false,
            cancel: false
        }, opt);

        $leaUploadify.uploadify({
            method: 'post',
            uploader: opt.url,
            swf: top.contentPath + '/Content/scripts/plugins/uploadify/uploadify.swf',
            buttonText: opt.btnName,
            height: 30,
            width: 90,
            fileTypeExts: '*.avi;*.mp3;*.mp4;*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.psd; *.rar;*.zip;*.swf;*.log;*.pdf;*.doc;*.docx;*.ppt;*.pptx;*.txt; *.xls; *.xlsx;',
            removeCompleted: false,
            onSelect: function (file) {

                //$("#" + file.id).prepend('<div style="float:left;width:50px;margin-right:2px;"><img src="/Content/images/filetype/' + file.type.replace('.', '') + '.png" style="width:40px;height:40px;" /></div>');
                //$('.border').hide();

                $("#" + file.id).prepend('<div style="float:left;width:50px;margin-right:2px;"><img src="/Content/images/filetype/' + file.type.replace('.', '') + '.png" style="width:40px;height:40px;" /></div>');
                $(".uploadify-queue-item").find('.cancel').find('a').html('<i class="fa fa-trash-o "></i>');
                $(".uploadify-queue-item").find('.cancel').find('a').attr('title', '删除');
                $(".uploadify-queue-item").find('.down').find('a').html('<i class="fa fa-download"></i>');
                $(".uploadify-queue-item").find('.down').find('a').attr('title', '下载');
                $(".uploadify-queue-item").hover(function () {
                    $(this).find('.cancel').find('a').show();
                }, function () {
                    $(this).find('.cancel').find('a').hide();
                });

                $(".uploadify-queue-item").find('.cancel').on('click', function () {
                    var fileId = $("#" + file.id).attr("data-fileId");
                    if ($('#' + leaUploadifyId + '-queue').find('.uploadify-queue-item').length == 0) {
                        $('#' + leaUploadifyId + '-queue').hide();
                    }
                    uiBase.setForm(
                    {
                        url: "/Utility/RemoveFile?fileId=" + fileId,
                        param: { keyValue: keyValue },
                        success: function (data) {
                            if (data.code == 1) {
                                $("#" + fileId).remove();
                                if ($('#' + leaUploadifyId + '-queue').find('.uploadify-queue-item').length == 0) {
                                    $('#' + leaUploadifyId + '-queue').hide();
                                }
                            }
                        }
                    });
                });
            },
            onUploadSuccess: function (file, data) {
                $("#" + file.id).find('.uploadify-progress').remove();
                $("#" + file.id).find('.data').html(' 恭喜您，上传成功！');
                $("#" + file.id).prepend('<a class="succeed" title="成功"><i class="fa fa-check-circle"></i></a>');

                var dataJson = JSON.parse(data);

                $("#" + file.id).attr("data-fileId", dataJson.fileId);

                if (opt.onUploadSuccess) {
                    opt.onUploadSuccess(dataJson);
                }

                $.learunUIBase.loading({ isShow: false });
            },
            onUploadError: function (file) {
                $("#" + file.id).removeClass('uploadify-error');
                $("#" + file.id).find('.uploadify-progress').remove();
                $("#" + file.id).find('.data').html(' 很抱歉，上传失败！');
                $("#" + file.id).prepend('<span class="error" title="失败"><i class="fa fa-exclamation-circle"></i></span>');
            },
            onUploadStart: function () {
                $('#' + leaUploadifyId + '-queue').show();
            },
            onCancel: function (file) {
                //console.log("cbb", $('#' + leaUploadifyId + '-queue').find('uploadify-queue-item'));

                //.hide();
            }
        });
        $("#" + leaUploadifyId + "-button").prepend('<i style="opacity: 0.6;" class="fa fa-cloud-upload"></i>&nbsp;');
        $('#' + leaUploadifyId + '-queue').hide();
        var dataWfname = $leaUploadify.attr('data-wfname');
        if (dataWfname != undefined)
        {
            $('#' + leaUploadifyId).attr('data-wfname', dataWfname);
            $('#' + leaUploadifyId).attr('type', 'file');
        }
        
    }
    $.fn.leaUploadifyShow = function (opt) {
        var $leaUploadify = $(this);
        var leaUploadifyId = $leaUploadify.attr('id');
        if (!leaUploadifyId) {
            return false;
        }
        $.each(opt.data, function (id, item) {
            $('#' + leaUploadifyId + '-queue').show();
            //console.log(item);
            var _html = '<div id="' + item.F_FileId + '"  class="uploadify-queue-item olduploadify-queue-item" ><a class="succeed" title="成功"><i class="fa fa-check-circle"></i></a><div style="float:left;width:50px;margin-right:2px;"><img src="/Content/images/filetype/' + item.F_FileType + '.png" style="width:40px;height:40px;"></div>';
            _html += '<div class="cancel remove" data-fileId="' + item.F_FileId + '"><a title="删除" style="display: none;"><i class="fa fa-trash-o "></i></a></div>';
            _html += '<div class="cancel down" data-fileId="' + item.F_FileId + '"><a title="下载" style="display: none;"><i class="fa fa-download"></i></a></div>';
            _html += '<span class="fileName">' + item.F_FileName + '</span><span class="data"></span></div>';
            $('#' + leaUploadifyId + '-queue').append(_html);
        });
        $(".uploadify-queue-item").hover(function () {
            $(this).find('.cancel').find('a').show();
        }, function () {
            $(this).find('.cancel').find('a').hide();
        });
        $(".olduploadify-queue-item").find('.remove').on('click', function () {
            var fileId = $(this).attr("data-fileId");
            if ($('#' + leaUploadifyId + '-queue').find('.uploadify-queue-item').length == 0) {
                $('#' + leaUploadifyId + '-queue').hide();
            }
            uiBase.setForm(
            {
                url: "/Utility/RemoveFile?fileId=" + fileId,
                param: { keyValue: keyValue },
                success: function (data) {
                    if (data.code == 1) {
                        $("#" + fileId).remove();
                        if ($('#' + leaUploadifyId + '-queue').find('.uploadify-queue-item').length == 0) {
                            $('#' + leaUploadifyId + '-queue').hide();
                        }
                    }
                }
            });
        });
        $(".olduploadify-queue-item").find('.down').on('click', function () {
            var fileId = $(this).attr("data-fileId");
            $.download("/Utility/DownFile", "fileId=" + fileId, 'post');
        });
    }

    //对Date属性增加一个方法
    Date.prototype.DateAdd = function (strInterval, Number) {
        //y年 q季度 m月 d日 w周 h小时 n分钟 s秒 ms毫秒
        var dtTmp = this;
        switch (strInterval) {
            case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));
            case 'n': return new Date(Date.parse(dtTmp) + (60000 * Number));
            case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));
            case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));
            case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
            case 'q': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
            case 'm': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
            case 'y': return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        }
    };

    $.learunUIBase = uiBase;
})(window.jQuery);;
/*!
 * 版 本 LearunADMS V6.1.1.7 (http://www.learun.cn)
 * Copyright 2011-2016 Learun, Inc.
 * LR215
 */
$(function () {
    window.onload = function () {
        $.learunUIBase.loading({ "isShow": true });
    }
    $(".ui-filter-text").click(function () {
        if ($(this).next('.ui-filter-list').is(":hidden")) {
            $(this).css('border-bottom-color', '#fff');
            $(".ui-filter-list").slideDown(10);
            $(this).addClass("active")
        } else {
            $(this).css('border-bottom-color', '#ccc');
            $(".ui-filter-list").slideUp(10);
            $(this).removeClass("active")
        }
    });
    $(".profile-nav li").click(function () {
        $(".profile-nav li").removeClass("active");
        $(".profile-nav li").removeClass("hover");
        $(this).addClass("active");
    }).hover(function () {
        if (!$(this).hasClass("active")) {
            $(this).addClass("hover");
        }
    }, function () {
        $(this).removeClass("hover");
    });
});


/*兼容之前版本的js,新加的功能请不要使用里面的方法,之后的版本更新会去掉这部分*/
(function ($) {
    $.fn.ComboBox = function (options) {
        options.maxHeight = options.height;
        return $(this).comboBox(options);
    }
    $.fn.ComboBoxSetValue = function (value) {
        return $(this).comboBoxSetValue(value);
    }
    $.fn.ComboBoxTree = function (options) {
        options.maxHeight = options.height;
        return $(this).comboBoxTree(options);
    }
    $.fn.ComboBoxTreeSetValue = function (value) {
        return $(this).comboBoxTreeSetValue(value);
    }
    $.fn.GetWebControls = function (keyValue) {
        return $(this).getWebControls(keyValue);
    };
    $.fn.SetWebControls = function (data) {
        $(this).setWebControls(data);
    }
    $.fn.Contextmenu = $.fn.contextmenu;
    $.fn.LeftListShowOfemail = $.fn.leftListShowOfemail;

    $.SaveForm = function (options) {
        $.learunUIBase.saveForm(options);
    }
    $.SetForm = function (options) {
        $.learunUIBase.setForm(options);
    }
    $.RemoveForm = function (options) {
        $.learunUIBase.removeForm(options);
    }
    $.ConfirmAjax = function (options) {
        $.learunUIBase.confirmAjax(options);
    }
    $.ExistField = function (controlId, url, param) {
        $.learunUIBase.existField(controlId, url, param);
    }
    $.getDataForm = function (options) {
        $.learunUIBase.getDataForm(options);
    }

})(window.jQuery);
Loading = function (bool, text) {
    $.learunUIBase.loading({ isShow: bool, text: text });
}
tabiframeId = function () {
    return $.learunUIBase.tabiframeId();
}
dialogTop = function (msg, type) {
    $.learunUIBase.dialogTop({ msg: msg, type: type });
}
dialogAlert = function (msg, type) {
    $.learunUIBase.dialogAlert({ msg: msg, type: type });
}
dialogMsg = function (msg, type) {
    $.learunUIBase.dialogMsg({ msg: msg, type: type });
}
dialogOpen = function (options) {
    $.learunUIBase.dialogOpen(options);
}
dialogContent = function (options) {
    $.learunUIBase.dialogContent(options);
}
dialogConfirm = function (msg, callBack) {
    $.learunUIBase.dialogConfirm({ msg: msg, callBack: callBack });
}
dialogClose = function () {
    $.learunUIBase.dialogClose();
}
reload = function () {
    location.reload();
    return false;
}
newGuid = function () {
    return $.learunUIBase.newGuid();
}
formatDate = function (v, format) {
    return $.learunUIBase.formatDate(v, format);
};
toDecimal = function (num) {
    return $.learunUIBase.toDecimal(num);
}
request = function (keyValue) {
    return $.learunUIBase.request(keyValue);
}
changeUrlParam = function (url, key, value) {
    return $.learunUIBase.changeUrlParam(url, key, value);
}
$.currentIframe = function () {
    return $.learunUIBase.currentIframe();
}
$.isbrowsername = function () {
    return $.learunUIBase.isbrowsername();
}
$.download = function (url, data, method) {
    $.learunUIBase.downFile({ url: url, data: data, method: method });
};
$.standTabchange = function (object, forid) {
    $.learunUIBase.changeStandTab({ obj: object, id: forid });
}
$.isNullOrEmpty = function (obj) {
    return $.learunUIBase.isNullOrEmpty(obj);
}
IsNumber = function (obj) {
    return $.learunUIBase.isNumber(obj);
}
IsMoney = function (obj) {
    return $.learunUIBase.isMoney(obj);
}
$.arrayClone = function (data) {
    return $.learunUIBase.arrayCopy(data);
}
$.windowWidth = function () {
    return $(window).width();
}
$.windowHeight = function () {
    return $(window).height();
}
checkedArray = function (id) {
    return $.learunUIBase.checkedArray(id);
}
checkedRow = function (id) {
    return $.learunUIBase.checkedRow(id);
}



;
