$(function () {
    $.extend($.fn.validatebox.defaults.rules, {
        comboxValidate: {
            validator: function (value, param, missingMessage) {
                if ($('#' + param).combobox('getValue') != '' && $('#' + param).combobox('getValue') != null) {
                    return true;
                }
                return false;
            },
            message: "{1}"
        }
    });
});
/*************************************** //绑定省份、城市、行政区信息*************************************************/
function BindProvinceCity() {

    var province = $('#Province').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        url: '/Customer/GetAllProvince',
        onSelect: function (newValue) {
            //改变省份
            $("#ProvinceCode").val(newValue.Value);
            $("#ProvinceName").val(newValue.Text);

            $("#CityCode").val('');
            $("#CityName").val('');
            $("#DistrictCode").val('');
            $("#DistrictName").val('');
            district.combobox("clear");


            $.get('/Customer/GetCitysByProvinceName', { provinceName: newValue.Value }, function (data) {
                city.combobox("clear").combobox('loadData', data);
                $("#City").combobox("setText", "--请选择城市--");

            }, 'json');
         

        }, onLoadSuccess: function () {
            //$("#Province").combobox("setText", "--请选择省份--");
            if ($("#ProvinceCode").val() != "") {
                //显示原省份
                $("#Province").combobox("setValue", $("#ProvinceCode").val());
            }
            else {
                $("#Province").combobox("setText", "--请选择省份--");
            }
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
                $("#District").combobox("setText", "--请选择城区--");
                $("#DistrictCode").val('');
                $("#DistrictName").val('');
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


    if ($("#ProvinceCode").val() != "") {
        $.get('/Customer/GetCitysByProvinceName', { provinceName: $("#ProvinceCode").val() }, function (data) {
            city.combobox("clear").combobox('loadData', data);
            district.combobox("clear");
            $("#City").combobox("setValue", "");
            if ($("#CityCode").val() != '') {
                //显示城市
                $("#City").combobox("setValue", $("#CityCode").val());
            }
            else {
                $("#City").combobox("setText", "--请选择城市--");
            }

        }, 'json');
    }


    //城市不为空，显示城市下的区
    if ($("#CityCode").val() != "") {
        $.get('/Customer/GetDistrictByCityName', { cityName: $("#CityCode").val() }, function (data) {
            district.combobox("clear").combobox('loadData', data);
            $("#District").combobox("setValue", "");
            if ($("#DistrictCode").val() != "") {
                //显示原区域
                $("#District").combobox("setValue", $("#DistrictCode").val());
            }
            else {
                $("#District").combobox("setText", "--请选择城区--");
            }
        }, 'json');
    }
    //初始化城区下拉列表
    var district = $('#District').combobox({
        valueField: 'Value', //值字段
        textField: 'Text', //显示的字段
        editable: false,
        onSelect: function (newValue, oldValue) {
            $("#DistrictCode").val(newValue.Value);
            $("#DistrictName").val(newValue.Text);
        },
        onLoadSuccess: function () {

        }
    });
}
