
(function ($) {
    //行业弹框
    var oneindustry, twoindustry, threeindustry;
    var IndustryObj = {};
    var industryType = {
        init: function (choosetype) {
            industryType.choosetype = choosetype;
            var layerHtml = "<div class='bpopup_type' id='bpopupIndustry'>";
            layerHtml += "<input type='hidden' id='hidReturnID' name='hidReturnID' value='' />  ";
            layerHtml += "   <div class='bpopup_type_main' id='IndustryType'></div>";//定义匹配的id‘IndustryType’
            layerHtml += "</div>";
            $("body").append(layerHtml);
            var titleHtml = "<table class='selected_type' id='industry_selected'>";
            titleHtml += "   <tr>";
            titleHtml += "      <td style='width: 80px; text-align: right;'><b>已选择：&nbsp;&nbsp;</b></td>";
            titleHtml += "      <td><ul></ul></td>";
            titleHtml += "      <td style='width: 63px;'><input type='button' value='' class='selected_type_btn' id='industryOKBtn' /></td>";//确认按钮赋值d
            titleHtml += "   </tr>";
            titleHtml += "</table>";
            $("#IndustryType").append(titleHtml);//根据匹配的id‘IndustryType’赋值
            IndustryObj.titletype = $("#IndustryType .selected_type");
            var mainTable = "<div class='bpopup_type_container'><table class='bpopup_type_table'></table></div>";
            $("#IndustryType").append(mainTable);//根据匹配的id‘IndustryType’赋值
            IndustryObj.mainTable = $("#IndustryType .bpopup_type_table");
            industryType.LoadXml();
            //增加鼠标拖动事件
            var theHandle = document.getElementById("industry_selected");
            theHandle.style.cursor = "move";
            var theRoot = document.getElementById("bpopupIndustry");
            Drag.init(theHandle, theRoot);
        },
        LoadXml: function () {
            $.ajax({
                type: "Get",
                url: "/Content/VocationData.xml",
                dataType: "xml",
                async: false,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error loading XML document:' + errorThrown + ":" + textStatus);
                },
                success: function (data) {
                    oneindustry = $(data).find('OneDimension');
                }
            });
            industryType.LoadIndustry();
        },
        LoadIndustry: function () {
            for (var i = 0; i < oneindustry.length; i++) {
                twoindustry = $(oneindustry[i]).find('TwoDimension');
                var tr = document.createElement("tr");
                if (i == oneindustry.length - 1) {
                    $(tr).addClass("last_tr");
                }
                var _str = "<td class='bigjob'><b>" + $(oneindustry[i]).attr("Name") + "：</b></td><td><table class='three_industry_table'></table></td>";
                $(tr).html(_str);
                IndustryObj.mainTable.append(tr);
                for (var num = 0; num < Math.ceil(twoindustry.length / 3) ; num++) {
                    var tr2 = document.createElement("tr");
                    for (var j = 0; j < 3; j++) {
                        var td = document.createElement("td");
                        if ((3 * num + j) < twoindustry.length) {
                            if (industryType.choosetype == "multiple") {
                                $(td).html("<input type='checkbox' onclick='this.checked=!this.checked' />" + $(twoindustry[3 * num + j]).attr("Name"));
                                $(td).click(function () {
                                    var num = $("#IndustryType .bpopup_type_table input[type='checkbox']:checked").size();
                                    if (num < 5) {
                                        $(this).find("input[type='checkbox']").attr("checked", !$(this).find("input[type='checkbox']").attr("checked"));
                                        industryType.ChooseIndustry(this);
                                    }
                                    else {
                                        if ($(this).find("input[type='checkbox']").attr("checked")) {
                                            $(this).find("input[type='checkbox']").attr("checked", false);
                                            industryType.ChooseIndustry(this);
                                        }
                                        else {
                                            alert("您最多能选择5项！");
                                        }

                                    }


                                });
                            }//多选
                            else {
                                $(td).html($(twoindustry[3 * num + j]).attr("Name"));
                                $(td).click(function () {
                                    industryType.ChooseIndustry(this);
                                });
                            }//单选
                            $(td).attr("data-value-id", $(twoindustry[3 * num + j]).attr("ID"));
                            $(td).attr({ "data-id": i, "data-subindex": 3 * num + j });
                        }
                        else {
                            $(td).html("");
                        }
                        $(tr2).append(td);
                    }
                    $(tr).children(".bigjob").next("td").children("table").append(tr2);
                }
            }
        },
        LoadSubindustry: function (obj, e) {
            //子行业层的创建
            if ($("#industrySubBpopup").size() <= 0) {
                var subIndustryHtml = "<div class='subBpopupType' id='industrySubBpopup'>";
                subIndustryHtml += "  <div class='subTypeMain'>";
                subIndustryHtml += "    <table></table>";
                subIndustryHtml += "  </div>";
                subIndustryHtml += "</div>";
                $("body").append(subIndustryHtml);
            }
            else {
                $(".subTypeMain table").empty();
            }
            //end
            var _oneIndex = $(obj).attr("data-id");
            var _twoIndex = $(obj).attr("data-subindex");
            var _left = $(obj).position().left;
            var _right = $("#IndustryType").width() - $(obj).position().left - 30;//根据匹配的id‘IndustryType’赋值
            var _top = $(obj).position().top + $(obj).height();
            twoindustry = $(oneindustry[_oneIndex]).find('TwoDimension');
            threeindustry = $(twoindustry[_twoIndex]).find('ThreeDimension');
            for (var i = 0; i < Math.ceil(threeindustry.length / 2) ; i++) {
                var tr = document.createElement("tr");
                for (var j = 0; j < 2; j++) {
                    var td = document.createElement("td");
                    if ((2 * i + j) < threeindustry.length) {
                        var input = document.createElement("input");
                        //var label = document.createElement("label");
                        var span = document.createElement("span");
                        $(input).attr({ "type": "radio", "name": "job_type", "data-id": $(threeindustry[2 * i + j]).attr("ID") });
                        $(span).html($(threeindustry[2 * i + j]).attr("Name"));
                        $(td).append(input);
                        $(td).append(span);
                        $(td).click(function () {
                            industryType.ChooseIndustry(this);
                        });
                    }
                    else {
                        $(td).empty();
                    }
                    $(tr).append(td);

                }
                $("#industrySubBpopup .subTypeMain table").append(tr);
                industryType.SubLayerPositon(e);
            }
        },
        SubLayerPositon: function (e) {
            var ex = e.clientX;
            var ey = e.clientY;
            var bl = document.body.scrollLeft || document.documentElement.scrollLeft;
            var bt = document.body.scrollTop || document.documentElement.scrollTop;
            var bw = document.body.offsetWidth || document.documentElement.offsetWidth;
            var bh = document.documentElement.clientHeight;
            var ow = $("#industrySubBpopup").width();
            var oh = $("#industrySubBpopup").height();
            var _left = ex + ow > bw ? ex + bl - ow : ex + bl;
            _left = Math.max(0, bl, _left);
            var _top = ey + oh > bh ? ey + bt - oh : ey + bt;
            _top = Math.max(0, bt, _top);
            $("#industrySubBpopup").css({ "left": _left, "top": _top }).show();
        },
        //行业类别选择事件
        ChooseIndustry: function (obj) {

            if (industryType.choosetype != "multiple") {
                IndustryObj.titletype.find("ul").empty();//单选时清空列表
            }
            if ($(obj).find("input[type='checkbox']").size() != 0 && !$(obj).find("input[type='checkbox']").attr("checked")) {
                var _id = $(obj).attr("data-value-id");
                var liobj = IndustryObj.titletype.find("ul").find("span#" + _id).parent("li").remove();
                return false;
            }
            var objId = $(obj).attr("data-value-id");
            var objText = $(obj).text();
            var listHtml = "<li>";
            listHtml += "  <span id='" + objId + "'>" + objText + "&nbsp;</span>";
            listHtml += "  <img src='/Content/images/delete_icon.png' />";
            listHtml += "</li>";
            IndustryObj.titletype.find("ul").append(listHtml);
            IndustryObj.titletype.find("ul").children("li").click(function () { industryType.removeItem(this); });
        },
        removeItem: function (obj) {
            var _id = $(obj).find("span").attr("id");
            $(obj).remove();
            $("td[data-value-id=" + _id + "]").find("input[type='checkbox']").attr("checked", false);
        }//移除已选择项目方法

    }

    //end -- 行业弹框

    //城市弹框
    var ProvinceName, CityName;
    var CityObj = {};
    var cityType = {
        init: function (choosetype) {
            cityType.choosetype = choosetype;
            var layerHtml = "<div class='bpopup_type' id='bpopupCity'>";
            layerHtml += "<input type='hidden' id='hidReturnID' name='hidReturnID' value='' />  ";
            layerHtml += "   <div class='bpopup_type_main' id='CityType'></div>";//定义匹配的id‘CityType’
            layerHtml += "</div>";
            $("body").append(layerHtml);
            var titleHtml = "<table class='selected_type' id='city_selected'>";
            titleHtml += "   <tr>";
            titleHtml += "      <td style='width: 80px; text-align: right;'><b>已选择：&nbsp;&nbsp;</b></td>";
            titleHtml += "      <td><ul></ul></td>";
            titleHtml += "      <td style='width: 63px;'><input type='button' value='' class='selected_type_btn' id='cityOKBtn' /></td>";//确认按钮赋值d
            titleHtml += "   </tr>";
            titleHtml += "</table>";
            $("#CityType").append(titleHtml);//根据匹配的id‘CityType’赋值
            CityObj.titletype = $("#CityType .selected_type");
            var mainTable = "<table class='bpopup_type_table'>";
            mainTable += "     <tr class='last_tr'>";
            mainTable += "         <td style='padding-right:50px;'><b>选择城市</b></td>";
            mainTable += "         <td><table id='privince_table'></table></td>";
            mainTable += "       </tr>";
            mainTable += "     </table>";
            $("#CityType").append(mainTable);//根据匹配的id‘CityType’赋值
            CityObj.mainTable = $("#CityType .bpopup_type_table");
            cityType.LoadXml();
            //增加鼠标拖动事件
            var theHandle = document.getElementById("city_selected");
            theHandle.style.cursor = "move";
            var theRoot = document.getElementById("bpopupCity");
            Drag.init(theHandle, theRoot);
        },
        LoadXml: function () {
            $.ajax({
                type: "Get",
                url: "/Content/CityData.xml",
                dataType: "xml",
                async: false,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error loading XML document:' + errorThrown + ":" + textStatus);
                },
                success: function (data) {
                    ProvinceName = $(data).find('OneDimension');
                }
            });
            cityType.LoadProvince();
        },
        LoadProvince: function () {
            for (var i = 0; i < Math.ceil(ProvinceName.length / 6) ; i++) {
                var tr = document.createElement("tr");
                for (var j = 0; j < 6; j++) {
                    var td = document.createElement("td");
                    if ((3 * i + j) < ProvinceName.length) {
                        $(td).html($(ProvinceName[6 * i + j]).attr("Name"));
                        $(td).attr({ "data-id": i, "data-index": 6 * i + j, id: $(ProvinceName[3 * i + j]).attr("ID") });
                        $(td).css("width", "85px");
                        $(td).live("click", function (e) {
                            cityType.LoadCity(this, e);
                        });
                    }
                    else {
                        $(td).html("");
                    }
                    $(tr).append(td);
                }
                $("#privince_table").append(tr);
            }
        },//加载省份
        LoadCity: function (obj, e) {
            //子城市层的创建
            if ($("#citySubBpopup").size() <= 0) {
                var subCityHtml = "<div class='subBpopupType' id='citySubBpopup'>";
                subCityHtml += "  <div class='subTypeMain'>";
                subCityHtml += "    <table></table>";
                subCityHtml += "  </div>";
                subCityHtml += "</div>";
                $("body").append(subCityHtml);
            }
            else {
                $("#citySubBpopup .subTypeMain table").empty();
            }
            //end
            var _index = $(obj).attr("data-index");
            CityName = $(ProvinceName[_index]).find('TwoDimension');
            $("#citySubBpopup .subTypeMain table").html("");
            for (var i = 0; i < Math.ceil(CityName.length / 4) ; i++) {
                var tr = document.createElement("tr");
                for (var j = 0; j < 4; j++) {
                    var td = document.createElement("td");
                    if ((4 * i + j) < CityName.length) {
                        var input = document.createElement("input");
                        var span = document.createElement("span");
                        $(span).html($(CityName[4 * i + j]).attr("Name"));
                        if (cityType.choosetype == "multiple") {
                            $(input).attr({ "type": "checkbox", "name": "city_name", "data-id": $(CityName[4 * i + j]).attr("ID") });
                            $(input).click(function () {
                                $(this).attr("checked", !$(this).attr("checked"));
                            });
                            if ($("#" + $(CityName[4 * i + j]).attr("ID")).size() > 0) {
                                $(input).attr("checked", "checked");
                            }//如果已经选择则标志出来
                            $(td).click(function () {
                                var num = CityObj.titletype.find("ul").find("li").size();
                                if (num < 5) {
                                    $(this).find("input[type='checkbox']").attr("checked", !$(this).find("input[type='checkbox']").attr("checked"));
                                    cityType.ChooseCity(this);
                                }
                                else {
                                    if ($(this).find("input[type='checkbox']").attr("checked")) {
                                        $(this).find("input[type='checkbox']").attr("checked", false);
                                        cityType.ChooseCity(this);
                                    }
                                    else {
                                        alert("您最多能选择5项！");
                                    }

                                }


                            });
                        }//多选
                        else {
                            $(input).attr({ "type": "radio", "name": "city_name", "data-id": $(CityName[4 * i + j]).attr("ID") });
                            $(td).click(function () {
                                cityType.ChooseCity(this);
                            });
                        }//单选
                        $(td).append(input);
                        $(td).append(span);
                    }
                    else {
                        $(td).html("").css("padding-right", "0");
                    }
                    $(tr).append(td);
                }
                $("#citySubBpopup .subTypeMain table").append(tr);
            }
            cityType.SubLayerPositon(e);
        },//加载省份下的城市
        SubLayerPositon: function (e) {
            var ex = e.clientX;
            var ey = e.clientY;
            var bl = document.body.scrollLeft || document.documentElement.scrollLeft;
            var bt = document.body.scrollTop || document.documentElement.scrollTop;
            var bw = document.body.offsetWidth || document.documentElement.offsetWidth;
            var bh = document.documentElement.clientHeight;
            var ow = $("#citySubBpopup").width();
            var oh = $("#citySubBpopup").height();
            var _left = ex + ow > bw ? ex + bl - ow : ex + bl;
            _left = Math.max(0, bl, _left);
            var _top = ey + oh > bh ? ey + bt - oh : ey + bt;
            _top = Math.max(0, bt, _top);
            $("#citySubBpopup").css({ "left": _left, "top": _top }).show();
        },
        //工作地点选择事件
        ChooseCity: function (obj) {
            if (cityType.choosetype != "multiple") {
                CityObj.titletype.find("ul").empty();//单选时清空列表
            }
            if ($(obj).find("input[type='checkbox']").size() != 0 && !$(obj).find("input[type='checkbox']").attr("checked")) {
                var _id = $(obj).find("input[type='checkbox']").attr("data-id");
                var liobj = CityObj.titletype.find("ul").find("span#" + _id).parent("li").remove();
                return false;
            }
            $(obj).children("input").attr("checked", "checked");
            var objId = $(obj).children("input").attr("data-id");
            var objText = $(obj).children("span").text();
            var listHtml = "<li>";
            listHtml += "  <span id='" + objId + "'>" + objText + "&nbsp;</span>";
            listHtml += "  <img src='/Content/images/delete_icon.png' />";
            listHtml += "</li>";
            CityObj.titletype.find("ul").append(listHtml);
            CityObj.titletype.find("ul").children("li").click(function () { cityType.removeItem(this); });
        },
        removeItem: function (obj) {
            var _id = $(obj).find("span").attr("id");
            $(obj).remove();
            $("td#" + _id).find("input[type='checkbox']").attr("checked", false);
        }//移除已选择项目方法
    }

    //end -- 城市弹框


    //户口弹框
    var LocalProvinceName, LocalCityName;
    var LocalObj = {};
    var localType = {
        init: function (choosetype) {
            localType.choosetype = choosetype;
            var layerHtml = "<div class='bpopup_type' id='bpopupLocal'>";
            layerHtml += "<input type='hidden' id='hidReturnID' name='hidReturnID' value='' />  ";
            layerHtml += "   <div class='bpopup_type_main' id='localType'></div>";//定义匹配的id‘localType’
            layerHtml += "</div>";
            $("body").append(layerHtml);
            var titleHtml = "<table class='selected_type' id='city_selected'>";
            titleHtml += "   <tr>";
            titleHtml += "      <td style='width: 80px; text-align: right;'><b>已选择：&nbsp;&nbsp;</b></td>";
            titleHtml += "      <td><ul></ul></td>";
            titleHtml += "      <td style='width: 63px;'><input type='button' value='' class='selected_type_btn' id='cityOKBtn' /></td>";//确认按钮赋值d
            titleHtml += "   </tr>";
            titleHtml += "</table>";
            $("#localType").append(titleHtml);//根据匹配的id‘localType’赋值
            LocalObj.titletype = $("#localType .selected_type");
            var mainTable = "<table class='bpopup_type_table'>";
            mainTable += "     <tr class='last_tr'>";
            mainTable += "         <td style='padding-right:50px;'><b>选择城市</b></td>";
            mainTable += "         <td><table id='localprivince_table'></table></td>";
            mainTable += "       </tr>";
            mainTable += "     </table>";
            $("#localType").append(mainTable);//根据匹配的id‘localType’赋值
            LocalObj.mainTable = $("#localType .bpopup_type_table");
            localType.LoadXml();
            //增加鼠标拖动事件
            var theHandle = document.getElementById("city_selected");
            theHandle.style.cursor = "move";
            var theRoot = document.getElementById("bpopupLocal");
            Drag.init(theHandle, theRoot);
        },
        LoadXml: function () {
            $.ajax({
                type: "Get",
                url: "/Content/CityData.xml",
                dataType: "xml",
                async: false,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error loading XML document:' + errorThrown + ":" + textStatus);
                },
                success: function (data) {
                    LocalProvinceName = $(data).find('OneDimension');
                }
            });
            localType.LoadProvince();
        },
        LoadProvince: function () {
            for (var i = 0; i < Math.ceil(LocalProvinceName.length / 6) ; i++) {
                var tr = document.createElement("tr");
                for (var j = 0; j < 6; j++) {
                    var td = document.createElement("td");
                    if ((3 * i + j) < LocalProvinceName.length) {
                        $(td).html($(LocalProvinceName[6 * i + j]).attr("Name"));
                        $(td).attr({ "data-id": i, "data-index": 6 * i + j, id: $(LocalProvinceName[3 * i + j]).attr("ID") });
                        $(td).css("width", "85px");
                        $(td).live("click", function (e) {
                            localType.LoadCity(this, e);
                        });
                    }
                    else {
                        $(td).html("");
                    }
                    $(tr).append(td);
                }
                $("#localprivince_table").append(tr);
            }
        },//加载省份
        LoadCity: function (obj, e) {
            //子城市层的创建
            if ($("#citySubBpopupLocal").size() <= 0) {
                var subCityHtml = "<div class='subBpopupType' id='citySubBpopupLocal'>";
                subCityHtml += "  <div class='subTypeMain'>";
                subCityHtml += "    <table></table>";
                subCityHtml += "  </div>";
                subCityHtml += "</div>";
                $("body").append(subCityHtml);
            }
            else {
                $("#citySubBpopupLocal .subTypeMain table").empty();
            }
            //end
            var _index = $(obj).attr("data-index");
            LocalCityName = $(LocalProvinceName[_index]).find('TwoDimension');
            $("#citySubBpopupLocal .subTypeMain table").html("");
            for (var i = 0; i < Math.ceil(LocalCityName.length / 4) ; i++) {
                var tr = document.createElement("tr");
                for (var j = 0; j < 4; j++) {
                    var td = document.createElement("td");
                    if ((4 * i + j) < LocalCityName.length) {
                        var input = document.createElement("input");
                        var span = document.createElement("span");
                        $(span).html($(LocalCityName[4 * i + j]).attr("Name"));
                        if (localType.choosetype == "multiple") {
                            $(input).attr({ "type": "checkbox", "name": "city_name", "data-id": $(LocalCityName[4 * i + j]).attr("ID") });
                            $(input).click(function () {
                                $(this).attr("checked", !$(this).attr("checked"));
                            });
                            if ($("#" + $(LocalCityName[4 * i + j]).attr("ID")).size() > 0) {
                                $(input).attr("checked", "checked");
                            }//如果已经选择则标志出来
                            $(td).click(function () {
                                var num = LocalObj.titletype.find("ul").find("li").size();
                                if (num < 5) {
                                    $(this).find("input[type='checkbox']").attr("checked", !$(this).find("input[type='checkbox']").attr("checked"));
                                    localType.ChooseCity(this);
                                }
                                else {
                                    if ($(this).find("input[type='checkbox']").attr("checked")) {
                                        $(this).find("input[type='checkbox']").attr("checked", false);
                                        localType.ChooseCity(this);
                                    }
                                    else {
                                        alert("您最多能选择5项！");
                                    }

                                }


                            });
                        }//多选
                        else {
                            $(input).attr({ "type": "radio", "name": "city_name", "data-id": $(LocalCityName[4 * i + j]).attr("ID") });
                            $(td).click(function () {
                                localType.ChooseCity(this);
                            });
                        }//单选
                        $(td).append(input);
                        $(td).append(span);
                    }
                    else {
                        $(td).html("").css("padding-right", "0");
                    }
                    $(tr).append(td);
                }
                $("#citySubBpopupLocal .subTypeMain table").append(tr);
            }
            localType.SubLayerPositon(e);
        },//加载省份下的城市
        SubLayerPositon: function (e) {
            var ex = e.clientX;
            var ey = e.clientY;
            var bl = document.body.scrollLeft || document.documentElement.scrollLeft;
            var bt = document.body.scrollTop || document.documentElement.scrollTop;
            var bw = document.body.offsetWidth || document.documentElement.offsetWidth;
            var bh = document.documentElement.clientHeight;
            var ow = $("#citySubBpopupLocal").width();
            var oh = $("#citySubBpopupLocal").height();
            var _left = ex + ow > bw ? ex + bl - ow : ex + bl;
            _left = Math.max(0, bl, _left);
            var _top = ey + oh > bh ? ey + bt - oh : ey + bt;
            _top = Math.max(0, bt, _top);
            $("#citySubBpopupLocal").css({ "left": _left, "top": _top }).show();
        },
        //工作地点选择事件
        ChooseCity: function (obj) {
            if (localType.choosetype != "multiple") {
                LocalObj.titletype.find("ul").empty();//单选时清空列表
            }
            if ($(obj).find("input[type='checkbox']").size() != 0 && !$(obj).find("input[type='checkbox']").attr("checked")) {
                var _id = $(obj).find("input[type='checkbox']").attr("data-id");
                var liobj = LocalObj.titletype.find("ul").find("span#" + _id).parent("li").remove();
                return false;
            }
            $(obj).children("input").attr("checked", "checked");
            var objId = $(obj).children("input").attr("data-id");
            var objText = $(obj).children("span").text();
            var listHtml = "<li>";
            listHtml += "  <span id='" + objId + "'>" + objText + "&nbsp;</span>";
            listHtml += "  <img src='/Content/images/delete_icon.png' />";
            listHtml += "</li>";
            LocalObj.titletype.find("ul").append(listHtml);
            LocalObj.titletype.find("ul").children("li").click(function () { localType.removeItem(this); });
        },
        removeItem: function (obj) {
            var _id = $(obj).find("span").attr("id");
            $(obj).remove();
            $("td#" + _id).find("input[type='checkbox']").attr("checked", false);
        }//移除已选择项目方法
    }

    //end -- 户口弹框

    //职位弹框

    var onepost, twopost, threepost;
    var PostObj = {};
    var postType = {
        init: function (choosetype) {
            postType.choosetype = choosetype;
            var layerHtml = "<div class='bpopup_type' id='bpopupPost'>";
            layerHtml += "<input type='hidden' id='hidReturnID' name='hidReturnID' value='' />  ";
            layerHtml += "   <div class='bpopup_type_main' id='PostType'></div>";//定义匹配的id‘PostType’
            layerHtml += "</div>";
            $("body").append(layerHtml);
            var titleHtml = "<table class='selected_type' id='post_selected'>";
            titleHtml += "   <tr>";
            titleHtml += "      <td style='width: 80px; text-align: right;'><b>已选择：&nbsp;&nbsp;</b></td>";
            titleHtml += "      <td><ul></ul></td>";
            titleHtml += "      <td style='width: 63px;'><input type='button' value='' class='selected_type_btn' id='postOKBtn' /></td>";//确认按钮赋值d
            titleHtml += "   </tr>";
            titleHtml += "</table>";
            $("#PostType").append(titleHtml);//根据匹配的id‘PostType’赋值
            PostObj.titletype = $("#PostType .selected_type");
            var mainTable = "<table class='bpopup_type_table'></table>";
            $("#PostType").append(mainTable);//根据匹配的id‘PostType’赋值
            PostObj.mainTable = $("#PostType .bpopup_type_table");
            postType.LoadXml();
            //增加鼠标拖动事件
            var theHandle = document.getElementById("post_selected");
            theHandle.style.cursor = "move";
            var theRoot = document.getElementById("bpopupPost");
            Drag.init(theHandle, theRoot);
        },
        LoadXml: function () {
            $.ajax({
                type: "Get",
                url: "/Content/PositionData.xml",
                dataType: "xml",
                async: false,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error loading XML document:' + errorThrown + ":" + textStatus);
                },
                success: function (data) {
                    onepost = $(data).find('OneDimension');
                }
            });
            postType.LoadPost();
        },
        LoadPost: function () {
            for (var i = 0; i < onepost.length; i++) {
                twopost = $(onepost[i]).find('TwoDimension');
                var tr = document.createElement("tr");
                if (i == onepost.length - 1) {
                    $(tr).addClass("last_tr");
                }
                var _str = "<td class='bigjob'><b>" + $(onepost[i]).attr("Name") + "：</b></td><td><table class='three_post_table'></table></td>";
                $(tr).html(_str);
                PostObj.mainTable.append(tr);
                for (var num = 0; num < Math.ceil(twopost.length / 3) ; num++) {
                    var tr2 = document.createElement("tr");
                    for (var j = 0; j < 3; j++) {
                        var td = document.createElement("td");
                        if ((3 * num + j) < twopost.length) {
                            $(td).html($(twopost[3 * num + j]).attr("Name"));
                            $(td).attr({ "data-id": i, "data-subindex": 3 * num + j });
                            $(td).click(function (e) {
                                postType.LoadSubpost($(this), e);
                                PostObj.currentTd = this;
                                $(PostObj.currentTd).mouseenter(function () {
                                    if (!$("#postSubBpopup").is(":hidden") && $("#postSubBpopup").size() > 0) {
                                        $("#postSubBpopup").attr("ctd", "true");
                                    }
                                });
                                $(PostObj.currentTd).mouseleave(function () {
                                    setTimeout(function () {
                                        if ($("#postSubBpopup").attr("ctd") != "false") {
                                            $("#postSubBpopup").removeAttr("ctd");
                                            $("#postSubBpopup").hide();
                                        }
                                    }, 100);
                                });
                            });

                        }
                        else {
                            $(td).html("");
                        }
                        $(tr2).append(td);
                    }
                    $(tr).children(".bigjob").next("td").children("table").append(tr2);
                }
            }
        },
        LoadSubpost: function (obj, e) {
            //子职位层的创建
            if ($("#postSubBpopup").size() <= 0) {
                var subPostHtml = "<div class='subBpopupType' id='postSubBpopup'>";
                subPostHtml += "  <div class='subTypeMain'>";
                subPostHtml += "    <table></table>";
                subPostHtml += "  </div>";
                subPostHtml += "</div>";
                $("body").append(subPostHtml);
            }
            else {
                $(".subTypeMain table").empty();
            }
            //end
            var _oneIndex = $(obj).attr("data-id");
            var _twoIndex = $(obj).attr("data-subindex");
            var _left = $(obj).position().left;
            var _right = $("#PostType").width() - $(obj).position().left - 30;//根据匹配的id‘PostType’赋值
            var _top = $(obj).position().top + $(obj).height();
            twopost = $(onepost[_oneIndex]).find('TwoDimension');
            threepost = $(twopost[_twoIndex]).find('ThreeDimension');
            for (var i = 0; i < Math.ceil(threepost.length / 2) ; i++) {
                var tr = document.createElement("tr");
                for (var j = 0; j < 2; j++) {
                    var td = document.createElement("td");
                    if ((2 * i + j) < threepost.length) {
                        var input = document.createElement("input");
                        var span = document.createElement("span");
                        $(span).html($(threepost[2 * i + j]).attr("Name"));
                        if (postType.choosetype == "multiple") {
                            $(input).attr({ "type": "checkbox", "name": "job_type", "data-id": $(threepost[2 * i + j]).attr("ID") });
                            $(input).click(function () {
                                $(this).attr("checked", !$(this).attr("checked"));
                            });
                            if ($("#" + $(threepost[2 * i + j]).attr("ID")).size() > 0) {
                                $(input).attr("checked", "checked");
                            }//如果已经选择则标志出来
                            $(td).click(function () {
                                var num = PostObj.titletype.find("ul").find("li").size();
                                if (num < 5) {
                                    $(this).find("input[type='checkbox']").attr("checked", !$(this).find("input[type='checkbox']").attr("checked"));
                                    postType.ChoosePost(this);
                                }
                                else {
                                    if ($(this).find("input[type='checkbox']").attr("checked")) {
                                        $(this).find("input[type='checkbox']").attr("checked", false);
                                        postType.ChoosePost(this);
                                    }
                                    else {
                                        alert("您最多能选择5项！");
                                    }

                                }


                            });
                        }//多选
                        else {
                            $(input).attr({ "type": "radio", "name": "job_type", "data-id": $(threepost[2 * i + j]).attr("ID") });
                            $(td).click(function () {
                                postType.ChoosePost(this);
                            });
                        }//单选
                        $(td).append(input);
                        $(td).append(span);
                    }
                    else {
                        $(td).empty();
                    }
                    $(tr).append(td);

                }
                $("#postSubBpopup .subTypeMain table").append(tr);
                postType.SubLayerPositon(e);
            }
        },
        SubLayerPositon: function (e) {
            var ex = e.clientX;
            var ey = e.clientY;
            var bl = document.body.scrollLeft || document.documentElement.scrollLeft;
            var bt = document.body.scrollTop || document.documentElement.scrollTop;
            var bw = document.body.offsetWidth || document.documentElement.offsetWidth;
            var bh = document.documentElement.clientHeight;
            var ow = $("#postSubBpopup").width();
            var oh = $("#postSubBpopup").height();
            var _left = ex + ow > bw ? ex + bl - ow : ex + bl;
            _left = Math.max(0, bl, _left);
            var _top = ey + oh > bh ? ey + bt - oh : ey + bt;
            _top = Math.max(0, bt, _top);
            $("#postSubBpopup").css({ "left": _left, "top": _top }).show();
        },
        //职位类别选择事件
        ChoosePost: function (obj) {
            if (postType.choosetype != "multiple") {
                PostObj.titletype.find("ul").empty();//单选时清空列表
            }
            if ($(obj).find("input[type='checkbox']").size() != 0 && !$(obj).find("input[type='checkbox']").attr("checked")) {
                var _id = $(obj).find("input[type='checkbox']").attr("data-id");
                var liobj = PostObj.titletype.find("ul").find("span#" + _id).parent("li").remove();
                return false;
            }
            $(obj).children("input").attr("checked", "checked");
            var objId = $(obj).children("input").attr("data-id");
            var objText = $(obj).children("span").text();
            var listHtml = "<li>";
            listHtml += "  <span id='" + objId + "'>" + objText + "&nbsp;</span>";
            listHtml += "  <img src='/Content/images/delete_icon.png' />";
            listHtml += "</li>";
            PostObj.titletype.find("ul").append(listHtml);
            PostObj.titletype.find("ul").children("li").click(function () { postType.removeItem(this); });
        },
        removeItem: function (obj) {
            var _id = $(obj).find("span").attr("id");
            $(obj).remove();
            $("td#" + _id).find("input[type='checkbox']").attr("checked", false);
        }//移除已选择项目方法
    }

    //end -- 职位弹框

    //专业类别弹框

    var onedomain, twodomain, threedomain;
    var DomainObj = {};
    var domainType = {
        init: function () {
            var layerHtml = "<div class='bpopup_type' id='bpopupDomain'>";
            layerHtml += "<input type='hidden' id='hidReturnID' name='hidReturnID' value='' />  ";
            layerHtml += " <div class='bpopup_type_main' id='DomainType'></div>";//定义匹配的id‘DomainType’
            layerHtml += "</div>";
            $("body").append(layerHtml);
            var titleHtml = "<table class='selected_type' id='domain_selected'>";
            titleHtml += "   <tr>";
            titleHtml += "      <td style='width: 80px; text-align: right;'><b>已选择：&nbsp;&nbsp;</b></td>";
            titleHtml += "      <td><ul></ul></td>";
            titleHtml += "      <td style='width: 63px;'><input type='button' value='' class='selected_type_btn' id='domainOKBtn' /></td>";//确认按钮赋值d
            titleHtml += "   </tr>";
            titleHtml += "</table>";
            $("#DomainType").append(titleHtml);//根据匹配的id‘DomainType’赋值
            DomainObj.titletype = $("#DomainType .selected_type");
            var mainTable = "<table class='bpopup_type_table'></table>";
            $("#DomainType").append(mainTable);//根据匹配的id‘DomainType’赋值
            DomainObj.mainTable = $("#DomainType .bpopup_type_table");
            domainType.LoadXml();
            //增加鼠标拖动事件
            var theHandle = document.getElementById("domain_selected");
            theHandle.style.cursor = "move";
            var theRoot = document.getElementById("bpopupDomain");
            Drag.init(theHandle, theRoot);
        },
        LoadXml: function () {
            $.ajax({
                type: "Get",
                url: "/Content/DomainData.xml",
                dataType: "xml",
                async: false,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error loading XML document:' + errorThrown + ":" + textStatus);
                },
                success: function (data) {
                    onedomain = $(data).find('OneDimension');
                }
            });
            domainType.LoadDomain();
        },
        LoadDomain: function () {
            for (var i = 0; i < onedomain.length; i++) {
                twodomain = $(onedomain[i]).find('TwoDimension');
                var tr = document.createElement("tr");
                if (i == onedomain.length - 1) {
                    $(tr).addClass("last_tr");
                }
                var _str = "<td class='bigjob'><b>" + $(onedomain[i]).attr("Name") + "：</b></td><td><table class='three_domain_table'></table></td>";
                $(tr).html(_str);
                DomainObj.mainTable.append(tr);
                for (var num = 0; num < Math.ceil(twodomain.length / 3) ; num++) {
                    var tr2 = document.createElement("tr");
                    for (var j = 0; j < 3; j++) {
                        var td = document.createElement("td");
                        if ((3 * num + j) < twodomain.length) {
                            $(td).html($(twodomain[3 * num + j]).attr("Name"));
                            $(td).attr({ "data-id": i, "data-subindex": 3 * num + j });
                            $(td).click(function (e) {
                                domainType.LoadSubdomain($(this), e);
                                DomainObj.currentTd = this;
                                $(DomainObj.currentTd).mouseenter(function () {
                                    if (!$("#domainSubBpopupe").is(":hidden") && $("#domainSubBpopup").size() > 0) {
                                        $("#domainSubBpopup").attr("ctd", "true");
                                    }
                                });
                                $(DomainObj.currentTd).mouseleave(function () {
                                    setTimeout(function () {
                                        if ($("#domainSubBpopup").attr("ctd") != "false") {
                                            $("#domainSubBpopup").removeAttr("ctd");
                                            $("#domainSubBpopup").hide();
                                        }
                                    }, 100);
                                });
                            });

                        }
                        else {
                            $(td).html("");
                        }
                        $(tr2).append(td);
                    }
                    $(tr).children(".bigjob").next("td").children("table").append(tr2);
                }
            }
        },
        LoadSubdomain: function (obj, e) {
            //子专业层的创建
            if ($("#domainSubBpopup").size() <= 0) {
                var subDomainHtml = "<div class='subBpopupType' id='domainSubBpopup'>";
                subDomainHtml += "  <div class='subTypeMain'>";
                subDomainHtml += "    <table></table>";
                subDomainHtml += "  </div>";
                subDomainHtml += "</div>";
                $("body").append(subDomainHtml);
            }
            else {
                $("#domainSubBpopup table").empty();
            }
            //end
            var _oneIndex = $(obj).attr("data-id");
            var _twoIndex = $(obj).attr("data-subindex");
            var _left = $(obj).position().left;
            var _right = $("#DomainType").width() - $(obj).position().left - 30;//根据匹配的id‘DomainType’赋值
            var _top = $(obj).position().top + $(obj).height();
            twodomain = $(onedomain[_oneIndex]).find('TwoDimension');
            threedomain = $(twodomain[_twoIndex]).find('ThreeDimension');
            for (var i = 0; i < Math.ceil(threedomain.length / 2) ; i++) {
                var tr = document.createElement("tr");
                for (var j = 0; j < 2; j++) {
                    var td = document.createElement("td");
                    if ((2 * i + j) < threedomain.length) {
                        var input = document.createElement("input");
                        //var label = document.createElement("label");
                        var span = document.createElement("span");
                        $(input).attr({ "type": "radio", "name": "job_type", "data-id": $(threedomain[2 * i + j]).attr("ID") });
                        $(span).html($(threedomain[2 * i + j]).attr("Name"));
                        $(td).append(input);
                        $(td).append(span);
                        $(td).click(function () {
                            domainType.ChooseDomain(this);
                        });
                    }
                    else {
                        $(td).empty();
                    }
                    $(tr).append(td);

                }
                $("#domainSubBpopup .subTypeMain table").append(tr);
                domainType.SubLayerPositon(e);
            }
        },
        SubLayerPositon: function (e) {
            var ex = e.clientX;
            var ey = e.clientY;
            var bl = document.body.scrollLeft || document.documentElement.scrollLeft;
            var bt = document.body.scrollTop || document.documentElement.scrollTop;
            var bw = document.body.offsetWidth || document.documentElement.offsetWidth;
            var bh = document.documentElement.clientHeight;
            var ow = $("#domainSubBpopup").width();
            var oh = $("#domainSubBpopup").height();
            var _left = ex + ow > bw ? ex + bl - ow : ex + bl;
            _left = Math.max(0, bl, _left);
            var _top = ey + oh > bh ? ey + bt - oh : ey + bt;
            _top = Math.max(0, bt, _top);
            $("#domainSubBpopup").css({ "left": _left, "top": _top }).show();
        },
        //专业类别选择事件
        ChooseDomain: function (obj) {
            $(obj).children("input[type = 'radio']").attr("checked", "checked");
            DomainObj.titletype.find("ul").empty();//单选时清空列表
            var objId = $(obj).children("input[type = 'radio']").attr("data-id");
            var objText = $(obj).children("span").text();
            var listHtml = "<li onclick='this.remove()'>";
            listHtml += "  <span id='" + objId + "'>" + objText + "&nbsp;</span>";
            listHtml += "  <img src='/Content/images/delete_icon.png' />";
            listHtml += "</li>";
            DomainObj.titletype.find("ul").append(listHtml);
            DomainObj.titletype.find("ul").children("li").click(function () { domainType.removeItem(this); });
            //DomainObj.titletype.find("ul").children("li").click(function () { this.remove(); });
        },
        removeItem: function (obj) {
            var _id = $(obj).find("span").attr("id");
            $(obj).remove();
            $("td#" + _id).find("input[type='checkbox']").attr("checked", false);
        }//移除已选择项目方法

    }
    //end -- 专业类别弹框

    //类别弹框插件
    $.fn.TypeShow = function (number, charlength, typeName, defualtvalue) {

        var handleObj = $(this);
        charlength = charlength ? charlength : 20;
        defualtvalue = defualtvalue ? defualtvalue : "";
        var mainId = "";
        switch (typeName) {
            case "industry": { industryType.init(number); mainId = "bpopupIndustry"; IndustryObj.ddlobj = this; break; }
            case "post": { postType.init(number); mainId = "bpopupPost"; PostObj.ddlobj = this; break; }
            case "city": { cityType.init(number); mainId = "bpopupCity"; CityObj.ddlobj = this; break; }
            case "local": { localType.init(number); mainId = "bpopupLocal"; LocalObj.ddlobj = this; break; }
            case "domain": { domainType.init(number); mainId = "bpopupDomain"; DomainObj.ddlobj = this; break; }
        }
        //行业--多选值为multiple,单选为single
        $(this).click(function () {
            $(this).blur();
            if ($(".select_ullist").length > 0) {
                $(".select_ullist").fadeOut();
            }
            var scrollNum = $(window).scrollTop();
            var x = $(window).width() / 2 - $("#" + mainId).width() / 2;
            var num = ($(window).height() - $("#" + mainId).height()) / 2;
            var y = num > 0 ? num + scrollNum : scrollNum;
            $("#" + mainId).bPopup({
                modalClose: false,
                opacity: 0.44,
                closeClass: "selected_type_btn",
                follow: [false, false], //x, y
                position: [x, y], //x, y
                onOpen: setDefualtValue(),
                onClose: function () {
                    if (handleObj.val() != defualtvalue) {
                        handleObj.addClass("current");
                    }
                }
            });
            //行业类别弹框头部宽度设置
            $("#" + mainId).find(".selected_type").width($("#" + mainId + " .bpopup_type_main").width());
        });

        $("#" + mainId + " .selected_type_btn").click(function () {
            sendTypeValue(charlength, this);
        });
        //传递所选行业类别项方法
        function sendTypeValue(length, obj) {
            var listObj = $(obj).parents(".selected_type").find("ul").children("li");
            var typename = "", typeid = "";
            for (var i = 0; i < listObj.size() ; i++) {
                typename += $.trim($(listObj[i]).find("span").text());
                typeid += $(listObj[i]).find("span").attr("id");
                if (i != (listObj.size() - 1)) {
                    typename += "+";
                    typeid += ",";
                }
            }
            if ($.trim(typename) != "") {
                handleObj.val(typename).limitTypeLength({ maxLength: length });
                handleObj.siblings("input[type='hidden']").eq(0).val(typeid);
                handleObj.siblings("input[type='hidden']").eq(1).val(typename);
            }
            else {
                handleObj.val(defualtvalue);
                handleObj.removeAttr("title");
                handleObj.siblings("input[type='hidden']").val('');
                handleObj.siblings("input[type='hidden']").eq(1).val('');
            }

        }

        //start 给所有弹框设置默认初始值
        //增加-符号删除操作,处理默认值"-请选择-"
        function setDefualtValue() {

            var $chooseobj = $("#" + mainId).find(".selected_type").find("ul"),
                _id = handleObj.next("input[type='hidden']").val(),
                _name = handleObj.val();
            if (typeof (handleObj.attr("title")) != "undefined") {
                _name = handleObj.attr("title");
            }
            _name = _name.replace(/-/gm, "");
            var arrName = _name.split("+");
            var arrID;
            if ($.trim(_id) != "") {
                arrID = _id.split(",");
            }
            if ($.trim($chooseobj.html()) == "" && handleObj.val().replace(/-/gm, "") != defualtvalue) {
                //若是不存在id隐藏控件
                if (handleObj.next("input[type='hidden']").size() == 0 || $.trim(_id) == "") {
                    var $li = $("<li onclick='this.remove();'></li>");
                    var span = document.createElement("span");
                    $(span).html(_name + "&nbsp;");
                    var imgsrc = "<img src='~/Content/images/delete_icon.png' />";
                    var img = document.createElement("img");
                    img.src = "/Content/images/delete_icon.png";
                    $li.append(span, img);
                    $chooseobj.append($li);
                }
                else {
                    for (var i = 0; i < arrName.length; i++) {
                        var $li = $("<li></li>");
                        var span = document.createElement("span");
                        $(span).attr("id", arrID[i]);
                        $(span).html(arrName[i] + "&nbsp;");
                        var imgsrc = "<img src='~/Content/images/delete_icon.png' />";
                        var img = document.createElement("img");
                        img.src = "/Content/images/delete_icon.png";
                        $li.append(span, img);
                        $chooseobj.append($li);
                        $li.click(function () {
                            switch (typeName) {
                                case "industry": { industryType.removeItem(this); break; }
                                case "post": { postType.removeItem(this); break; }
                                case "city": { cityType.removeItem(this); break; }
                                case "local": { localType.removeItem(this); break; }
                                case "domain": { domainType.removeItem(this); break; }
                            }
                        });
                    }
                    //end 循环多选值
                }
                //end if
            }

            //若是行业多选情况设置默认选项
            if (typeName == "industry" && number == "multiple" && $.trim(_id) != "") {
                for (var i = 0; i < arrID.length; i++) {
                    $("td[data-value-id=" + arrID[i] + "]").children("input").attr("checked", "checked");
                }
            }
            //end
        }
        //end

    }


    //类别弹框插件
    $.fn.TypeShowByID = function (objID, number, charlength, typeName, defualtvalue) {

        var handleObj = $(this);
        charlength = charlength ? charlength : 20;
        defualtvalue = defualtvalue ? defualtvalue : "";
        var mainId = "";
        switch (typeName) {
            case "industry": { industryType.init(number); mainId = "bpopupIndustry"; IndustryObj.ddlobj = this; break; }
            case "post": { postType.init(number); mainId = "bpopupPost"; PostObj.ddlobj = this; break; }
            case "city": { cityType.init(number); mainId = "bpopupCity"; CityObj.ddlobj = this; break; }
            case "local": { localType.init(number); mainId = "bpopupLocal"; LocalObj.ddlobj = this; break; }
            case "domain": { domainType.init(number); mainId = "bpopupDomain"; DomainObj.ddlobj = this; break; }
        }
        //行业--多选值为multiple,单选为single
        $(this).click(function () {

            $(this).blur();
            if ($(".select_ullist").length > 0) {
                $(".select_ullist").fadeOut();
            }
            //handleObj = $(this);
            //临时存放当前点击的控件ID
            $("#" + mainId).find("input[name='hidReturnID']").val($(this).attr("id"));
            //alert("当前点击的控件ID：" + $(this).attr("id"));
            var scrollNum = $(window).scrollTop();
            var x = $(window).width() / 2 - $("#" + mainId).width() / 2;
            var num = ($(window).height() - $("#" + mainId).height()) / 2;
            var y = num > 0 ? num + scrollNum : scrollNum;
            $("#" + mainId).bPopup({
                modalClose: false,
                opacity: 0.44,
                closeClass: "selected_type_btn",
                follow: [false, false], //x, y
                position: [x, y], //x, y
                onOpen: setDefualtValue(),
                onClose: function () {
                    if (handleObj.val() != defualtvalue) {
                        handleObj.addClass("current");
                    }
                }
            });
            //行业类别弹框头部宽度设置
            $("#" + mainId).find(".selected_type").width($("#" + mainId + " .bpopup_type_main").width());
        });
        $("#" + mainId + " .selected_type_btn").click(function () {
            sendTypeValue(charlength, this);
        });

        //传递所选行业类别项方法
        function sendTypeValue(length, obj) {
            var handleObj = $("#" + $("#" + mainId).find("input[name='hidReturnID']").val());

            //alert("返回控件ID：" + handleObj.attr("id"));
            var listObj = $(obj).parents(".selected_type").find("ul").children("li");
            var typename = "", typeid = "";
            for (var i = 0; i < listObj.size() ; i++) {
                typename += $.trim($(listObj[i]).find("span").text());
                typeid += $(listObj[i]).find("span").attr("id");
                if (i != (listObj.size() - 1)) {
                    typename += "+";
                    typeid += ",";
                }
            }
            if ($.trim(typename) != "") {
                handleObj.val(typename).limitTypeLength({ maxLength: length });
                handleObj.siblings("input[type='hidden']").eq(0).val(typeid);
                handleObj.siblings("input[type='hidden']").eq(1).val(typename);
            }
            else {
                handleObj.val(defualtvalue);
                handleObj.removeAttr("title");
                handleObj.siblings("input[type='hidden']").val('');
                handleObj.siblings("input[type='hidden']").eq(1).val('');
            }

        }

        //start 给所有弹框设置默认初始值
        //增加-符号删除操作,处理默认值"-请选择-"
        function setDefualtValue() {

            var $chooseobj = $("#" + mainId).find(".selected_type").find("ul"),
                _id = handleObj.next("input[type='hidden']").val(),
                _name = handleObj.val();
            if (typeof (handleObj.attr("title")) != "undefined") {
                _name = handleObj.attr("title");
            }
            _name = _name.replace(/-/gm, "");
            var arrName = _name.split("+");
            var arrID;
            if ($.trim(_id) != "") {
                arrID = _id.split(",");
            }
            if ($.trim($chooseobj.html()) == "" && handleObj.val().replace(/-/gm, "") != defualtvalue) {
                //若是不存在id隐藏控件
                if (handleObj.next("input[type='hidden']").size() == 0 || $.trim(_id) == "") {
                    var $li = $("<li onclick='this.remove();'></li>");
                    var span = document.createElement("span");
                    $(span).html(_name + "&nbsp;");
                    var imgsrc = "<img src='~/Content/images/delete_icon.png' />";
                    var img = document.createElement("img");
                    img.src = "/Content/images/delete_icon.png";
                    $li.append(span, img);
                    $chooseobj.append($li);
                }
                else {
                    for (var i = 0; i < arrName.length; i++) {
                        var $li = $("<li></li>");
                        var span = document.createElement("span");
                        $(span).attr("id", arrID[i]);
                        $(span).html(arrName[i] + "&nbsp;");
                        var imgsrc = "<img src='~/Content/images/delete_icon.png' />";
                        var img = document.createElement("img");
                        img.src = "/Content/images/delete_icon.png";
                        $li.append(span, img);
                        $chooseobj.append($li);
                        $li.click(function () {
                            switch (typeName) {
                                case "industry": { industryType.removeItem(this); break; }
                                case "post": { postType.removeItem(this); break; }
                                case "city": { cityType.removeItem(this); break; }
                                case "local": { localType.removeItem(this); break; }
                                case "domain": { domainType.removeItem(this); break; }
                            }
                        });
                    }
                    //end 循环多选值
                }
                //end if
            }

            //若是行业多选情况设置默认选项
            if (typeName == "industry" && number == "multiple" && $.trim(_id) != "") {
                for (var i = 0; i < arrID.length; i++) {
                    $("td[data-value-id=" + arrID[i] + "]").children("input").attr("checked", "checked");
                }
            }
            //end
        }
        //end

    }

    //子类别的显示与隐藏
    $("#industrySubBpopup,#postSubBpopup,#citySubBpopup,#citySubBpopupLocal,#domainSubBpopup").live("mouseenter", function () {
        $(this).attr("ctd", "false");
    });
    $("#industrySubBpopup,#postSubBpopup,#citySubBpopup,#citySubBpopupLocal,#domainSubBpopup").live("mouseleave", function () {
        var subObj = $(this);
        setTimeout(function () {
            if (subObj.attr("ctd") != "true") {
                subObj.hide();
            }
        }, 100);
    });

    //获取url参数插件
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null)
            return unescape(r[2]);
        return null;
    }

    //end

    //字符限制
    $.fn.limitTypeLength = function (options) {
        //默认属性、参数
        var settings = {
            maxLength: 100 //允许的最大文本长度
        };

        //让默认参数复写plugs参数
        if (options) {
            $.extend(settings, options);
        };
        //页面初始化时执行方法
        if ($(this).attr("type") != "") {
            var _text = $.trim($(this).val());
        }
        else {
            var _text = $.trim($(this).text());
        }
        var limit_text = "";
        var _obj = $(this);
        //var textLength = getByteLength($(this).text());
        if (getByteLength(_text) > settings.maxLength) {
            $(this).attr("title", _text);
            var str = getByteVal(_text, settings.maxLength - 3);//减去省略号‘...’占有的三个字符长度
            limit_text = str;
            if ($(this).attr("type") != "") {
                $(this).val(str + "...");
            }
            else {
                $(this).text($.trim(str + "..."));
            }
            $(this).css("cursor", "pointer");
        }
        else {
            $(this).removeAttr("title");
        }
        //返回val的字节长度 
        function getByteLength(val) {
            var len = 0;
            for (var i = 0; i < val.length; i++) {
                var wordstr = val.slice(i, i + 1);
                if (wordstr.match(/[^\x00-\xff]/ig) != null) //全角 
                    len += 2;
                else
                    len += 1;
            }
            return len;
        }
        //返回val在规定字节长度max内的值 
        function getByteVal(val, max) {
            var returnValue = '';
            var byteValLen = 0;
            for (var i = 0; i < val.length; i++) {
                var wordstr = val.slice(i, i + 1);
                if (wordstr.match(/[^\x00-\xff]/ig) != null)
                    byteValLen += 2;
                else
                    byteValLen += 1;
                if (byteValLen > max)
                    break;
                returnValue += wordstr;
            }
            return returnValue;
        }

    }

    //end 字符限制

    //拖拽
    var Drag = {

        obj: null,

        init: function (o, oRoot, minX, maxX, minY, maxY, bSwapHorzRef, bSwapVertRef, fXMapper, fYMapper) {
            o.onmousedown = Drag.start;

            o.hmode = bSwapHorzRef ? false : true;
            o.vmode = bSwapVertRef ? false : true;

            o.root = oRoot && oRoot != null ? oRoot : o;

            if (o.hmode && isNaN(parseInt(o.root.style.left))) o.root.style.left = "0px";
            if (o.vmode && isNaN(parseInt(o.root.style.top))) o.root.style.top = "0px";
            if (!o.hmode && isNaN(parseInt(o.root.style.right))) o.root.style.right = "0px";
            if (!o.vmode && isNaN(parseInt(o.root.style.bottom))) o.root.style.bottom = "0px";

            o.minX = typeof minX != 'undefined' ? minX : null;
            o.minY = typeof minY != 'undefined' ? minY : null;
            o.maxX = typeof maxX != 'undefined' ? maxX : null;
            o.maxY = typeof maxY != 'undefined' ? maxY : null;

            o.xMapper = fXMapper ? fXMapper : null;
            o.yMapper = fYMapper ? fYMapper : null;

            o.root.onDragStart = new Function();
            o.root.onDragEnd = new Function();
            o.root.onDrag = new Function();
        },

        start: function (e) {
            var o = Drag.obj = this;
            e = Drag.fixE(e);
            var y = parseInt(o.vmode ? o.root.style.top : o.root.style.bottom);
            var x = parseInt(o.hmode ? o.root.style.left : o.root.style.right);
            o.root.onDragStart(x, y);

            o.lastMouseX = e.clientX;
            o.lastMouseY = e.clientY;

            if (o.hmode) {
                if (o.minX != null) o.minMouseX = e.clientX - x + o.minX;
                if (o.maxX != null) o.maxMouseX = o.minMouseX + o.maxX - o.minX;
            } else {
                if (o.minX != null) o.maxMouseX = -o.minX + e.clientX + x;
                if (o.maxX != null) o.minMouseX = -o.maxX + e.clientX + x;
            }

            if (o.vmode) {
                if (o.minY != null) o.minMouseY = e.clientY - y + o.minY;
                if (o.maxY != null) o.maxMouseY = o.minMouseY + o.maxY - o.minY;
            } else {
                if (o.minY != null) o.maxMouseY = -o.minY + e.clientY + y;
                if (o.maxY != null) o.minMouseY = -o.maxY + e.clientY + y;
            }

            document.onmousemove = Drag.drag;
            document.onmouseup = Drag.end;

            return false;
        },

        drag: function (e) {
            e = Drag.fixE(e);
            var o = Drag.obj;

            var ey = e.clientY;
            var ex = e.clientX;
            var y = parseInt(o.vmode ? o.root.style.top : o.root.style.bottom);
            var x = parseInt(o.hmode ? o.root.style.left : o.root.style.right);
            var nx, ny;

            if (o.minX != null) ex = o.hmode ? Math.max(ex, o.minMouseX) : Math.min(ex, o.maxMouseX);
            if (o.maxX != null) ex = o.hmode ? Math.min(ex, o.maxMouseX) : Math.max(ex, o.minMouseX);
            if (o.minY != null) ey = o.vmode ? Math.max(ey, o.minMouseY) : Math.min(ey, o.maxMouseY);
            if (o.maxY != null) ey = o.vmode ? Math.min(ey, o.maxMouseY) : Math.max(ey, o.minMouseY);

            nx = x + ((ex - o.lastMouseX) * (o.hmode ? 1 : -1));
            ny = y + ((ey - o.lastMouseY) * (o.vmode ? 1 : -1));

            if (o.xMapper) nx = o.xMapper(y)
            else if (o.yMapper) ny = o.yMapper(x)
            nx = nx <= 0 ? 0 : nx;
            ny = ny <= 0 ? 0 : ny;
            var maxX = document.documentElement.scrollWidth - Drag.obj.root.offsetWidth, maxY = document.documentElement.scrollHeight - Drag.obj.root.offsetHeight;
            nx = nx >= maxX ? maxX : nx;
            ny = ny >= maxY ? maxY : ny;
            Drag.obj.root.style[o.hmode ? "left" : "right"] = nx + "px";
            Drag.obj.root.style[o.vmode ? "top" : "bottom"] = ny + "px";
            Drag.obj.lastMouseX = ex;
            Drag.obj.lastMouseY = ey;

            Drag.obj.root.onDrag(nx, ny);
            return false;
        },

        end: function () {
            document.onmousemove = null;
            document.onmouseup = null;
            Drag.obj.root.onDragEnd(parseInt(Drag.obj.root.style[Drag.obj.hmode ? "left" : "right"]),
                                        parseInt(Drag.obj.root.style[Drag.obj.vmode ? "top" : "bottom"]));
            Drag.obj = null;
        },

        fixE: function (e) {
            if (typeof e == 'undefined') e = window.event;
            if (typeof e.layerX == 'undefined') e.layerX = e.offsetX;
            if (typeof e.layerY == 'undefined') e.layerY = e.offsetY;
            return e;
        }
    };

    //end拖拽


})(jQuery)

function clearselect(objid) {
    $("#" + objid + "_selected td>ul>li").remove();
    $("#" + objid + "_selected").next("div").find("input[type='checkbox']").attr("checked", false);

}