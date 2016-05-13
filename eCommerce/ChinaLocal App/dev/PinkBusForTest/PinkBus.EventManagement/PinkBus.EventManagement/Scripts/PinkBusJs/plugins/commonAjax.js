define(['jquery'], function () {
    var instance = {};
    instance.easyAjax = function (cUrl, mType, data, successFunc, errorFunc) {
        data.Timestamp = (new Date()).getTime();
        $.ajax({

            url: Application + cUrl,  //请求的URL
            timeout: 200000, //超时时间设置，单位毫秒
            type: mType,  //请求方式，get或post
            data: data,  //请求所传参数，json格式
            //dataType: 'html',//返回的数据格式
            success: function (data, textStatus) { //请求成功的回调函数
                //console.log(data)
                successFunc(data, textStatus);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) { //请求完成后最终执行参数
                errorFunc(XMLHttpRequest, textStatus, errorThrown)
            }
        });
    };

    return instance;
})