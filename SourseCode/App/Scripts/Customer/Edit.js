(function ($) {

    $(function () {

        //营业执照图片
        var pic = $("#CompanyCertification").val();
        if (pic == '') {
            $("#showPic").attr("src", "/Images/noPic.jpg")
        } else {
            $("#showPic").attr("src", pic);
        }
        //行业
        $("#btnJobIndustryName").TypeShow("single", 27, "industry", $("#btnJobIndustryName").val());

        //行业
        if ($("#VocationName").val() != '') {
            $("#btnJobIndustryName").val($("#VocationName").val());
        }

        BindProvinceCity();
    });

})(jQuery);

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
            //重新加载城市
            $.get('/Customer/GetCitysByProvinceName', { provinceName: newValue.Value }, function (data) {
                city.combobox("clear").combobox('loadData', data);
                district.combobox("clear");
                $("#City").combobox("setValue", "");
                $("#City").combobox("setText", "--请选择城市--");

            }, 'json');
            $("#CityCode").val('');
            $("#CityName").val('');
            $("#DistrictCode").val('');
            $("#DistrictName").val('');

        }, onLoadSuccess: function () {
            if ($("#ProvinceCode").val() != "") {
                //显示原省份
                $("#Province").combobox("setValue", $("#ProvinceCode").val());
            }
            else {
                $("#Province").combobox("setText", "--请选择省份--");
            }
        }
    });
    //初始化城市下拉列表
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
            $("#DistrictCode").val('');
            $("#DistrictName").val('');
        },
        onLoadSuccess: function () {

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





}




/****************************************公司列表*************************************************************************/
function openDialog() {
    $('#selectCompany').dialog({
        title: '绑定客户',
        width: 760,
        height: 580,
        closed: false,
        cache: false,
        href: '/Company/Index',
        modal: true
    });
}

