function addFileUrl(name, url) {
    var fileObj = $("#fileUrl").val();
    $("#" + fileObj).val(url);
    $("#showPic").attr("src", url);
}
var $ = jQuery, $list = $('#fileList');
var ValidateIndex;
var uploader = WebUploader.create({

    // 选完文件后，是否自动上传。
    auto:true,

    // swf文件路径
    swf: '/Res/webuploader/Uploader.swf',

    // 文件接收服务端。
    server: '/FileUploader/HDpic',

    // 选择文件的按钮。可选。
    // 内部根据当前运行是创建，可能是input元素，也可能是flash.
    pick: '#filePicker',
    resize: true,
    // 只允许选择图片文件。
    accept: {
        title: '图片文件',
        extensions: 'gif,jpg,jpeg,bmp,png',
        mimeTypes: 'image/*'
    },
    fileNumLimit: 1,
    fileSizeLimit:6 * 1024 * 1024,
    fileSingleSizeLimit: 5 * 1024 * 1024
});
uploader.on('beforeFileQueued', function (file) {
    switch (file.ext.toLowerCase()) {
        case "gif": return true;  
        case "jpg": return true; 
        case "jpeg": return true; 
        case "bmp": return true; 
        case "png": return true;  
    }
    alert("请选择图片文件");
    return false;
});
uploader.on('fileQueued', function (file) {
    var $li = $(
            '<div id="' + file.id + '" class="file-item thumbnail">' +
                '<img>' +
                '<div class="info">' + file.name + '</div>' +
            '</div>'
            ),
        $img = $li.find('img');


    // $list为容器jQuery实例
    $list.append($li);
});
// 文件上传过程中创建进度条实时显示。
uploader.on('uploadProgress', function (file, percentage) {
    var $li = $('#' + file.id),
        $percent = $li.find('.progress span');

    // 避免重复创建
    if (!$percent.length) {
        $percent = $('<p class="progress"><span></span></p>')
                .appendTo($li)
                .find('span');
    }

    $percent.css('width', percentage * 100 + '%');
});

// 文件上传成功，给item添加成功class, 用样式标记上传成功。
uploader.on('uploadSuccess', function (file, responseText) {
    //$('#' + file.id).remove();
    var validate;
    for (var d in responseText) {
        validate = responseText[d];
    }
    ValidateIndex = validate.indexOf("<script");
    if (ValidateIndex > 0) {
        $.messager.alert('提示', '登录超时');
    } else {
        addFileUrl(responseText.jsonrpc, responseText.result);
    }
    uploader.reset();
});

// 文件上传失败，显示上传出错。
uploader.on('uploadError', function (file) {
    var $li = $('#' + file.id),
        $error = $li.find('div.error');

    // 避免重复创建
    if (!$error.length) {
        $error = $('<div class="error"></div>').appendTo($li);
    }

    $error.text('上传失败');
});

 
