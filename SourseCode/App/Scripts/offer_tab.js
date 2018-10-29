(function () {
    $.fn.offertab = function () {
        $(this).children(".tab_title").children("ul").children("li").find("a").each(function () {
            $(this).click(function () {
                $(this).parent().parent().children("li").removeClass("tab_selected");
                $(this).parent().parent().children("li").find("a").addClass("btn");
                $(this).removeClass("btn").parent().addClass("tab_selected");
                $(this).parent().parent().parent().next().find(".tc_main").hide();
                $("#tc_" + $(this).attr("id")).show();
            });
        })

        $(this).children(".tab_title").children("ul").children("li").each(function () {
            if ($(this).children("span").length > 0) {
                $(this).children("a").mouseover(function () {
                    $(this).next("span").addClass("btn_sclosehover");
                    $(this).next("span").text("×");
                });
                $(this).children("span").mouseover(function () {
                    $(this).addClass("btn_sclosehover");
                    $(this).text("×");
                }).click(function () {
                    $(this).parent("li").remove();
                    $("#tc_" + $(this).prev("a").attr("id")).hide();
                });
                $(this).mouseout(function () {
                    $(this).children("span").removeClass("btn_sclosehover");
                    $(this).children("span").text("");
                });


            }
        })
    }
})(jQuery)