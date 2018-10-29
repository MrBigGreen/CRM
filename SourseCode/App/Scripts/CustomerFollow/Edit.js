(function ($) {

    /**
 * 扩展combox验证，easyui原始只验证select text的值，不支持value验证
 */
    //$.extend($.fn.validatebox.defaults.rules, {
    //    comboxValidate: {
    //        validator: function (value, param, missingMessage) {
    //            if ($('#' + param).combobox('getValue') != '' && $('#' + param).combobox('getValue') != null) {
    //                return true;
    //            }
    //            return false;
    //        },
    //        message: "{1}"
    //    }
    //});



    $('#Province').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        url: '/Customer/GetAllProvince',
        onLoadSuccess: function () {
            if ($("#ProvinceCode").val() != "") {
                //显示原省份
                $("#Province").combobox("setValue", $("#ProvinceCode").val());
            }
        }
    });

    if ($("#CityCode").val() != "") {
        $('#City').combobox({
            valueField: 'Value', //值字段
            textField: 'Text', //显示的字段
            editable: false,
            url: '/Customer/GetCitysByProvinceName?provinceName=' + $("#ProvinceCode").val(),
            onLoadSuccess: function () {

                //显示城市
                $("#City").combobox("setValue", $("#CityCode").val());

            }
        });
    }
    if ($("#DistrictCode").val() != "") {
        $('#District').combobox({
            valueField: 'Value', //值字段
            textField: 'Text', //显示的字段
            editable: false,
            url: '/Customer/GetDistrictByCityName?cityName=' + $("#CityCode").val(),
            onLoadSuccess: function () {
                //显示原区域
                $("#District").combobox("setValue", $("#DistrictCode").val());
            }
        });
    }


    BindProvinceCity();

    $('#ReservationDate').datetimebox({
        showSeconds: false
    });

    $('#ReservationDate').datetimebox('calendar').calendar({
        validator: function (date) {
            var now = new Date();
            var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
            var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 10);
            return d1 <= date;
        }
    });

})(jQuery);


/*************************************** //绑定省份、城市、行政区信息*************************************************/
function BindProvinceCity() {

    var province = $('#Province').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        onSelect: function (newValue) {
            //改变省份
            $("#ProvinceCode").val(newValue.Value);
            $("#ProvinceName").val(newValue.Text);
            //重新加载城市
            $.get('/Customer/GetCitysByProvinceName', { provinceName: newValue.Value }, function (data) {
                city.combobox("clear").combobox('loadData', data);
                district.combobox("clear");
                $("#City").combobox("setValue", "");
                $("#City").combobox("setText", "--请选择城市--");
            }, 'json');

        }
    });

    var city = $('#City').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        onSelect: function (newValue, oldValue) {
            //改变城市
            $("#CityCode").val(newValue.Value);
            $("#CityName").val(newValue.Text);
            $.get('/Customer/GetDistrictByCityName', { cityName: newValue.Value }, function (data) {
                district.combobox("clear").combobox('loadData', data);
                $("#District").combobox("setValue", "");
                $("#District").combobox("setText", "--请选择城区--");
            }, 'json');
        }
    });

    var district = $('#District').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        onSelect: function (newValue, oldValue) {
            $("#DistrictCode").val(newValue.Value);
            $("#DistrictName").val(newValue.Text);
        }
    });
}

