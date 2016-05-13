//commonJs
var common = {
    message: {
        unKnownMessage: "未知错误，请刷新后重试...",
        serverMessage: "服务端出现错误，请联系管理员..."
    },
    ajaxType: {
        GET: "GET",
        POST: "POST"
    },
    browserType: function () {
        var u = navigator.userAgent;
       
         if (!!u.match(/AppleWebKit.*Mobile.*/))
            return "mobile";
        else
            return "web";

    },
    requestControllerAjax: function (url, ajaxtype, data, succsscallback, errorvallback) {
        if (ajaxtype == common.ajaxType.GET) {
            data.time = new Date();
        }
        $.ajax({
            url:  url,
            type: ajaxtype,
            data: data,
            dataType: "json",
            traditional: true,
            // cache:true,
            success: function (re) {
                succsscallback(re);
            },
            error: function (re) {

                if (re != undefined && re != null) {
                    if (typeof (errorvallback) != 'undefined')
                        errorvallback(re);
                }
            }
        });
    },
    getDataFromServer: function (turl, mType, aData, apiHost, apiUrl, jsonData) {

        common.EasyAjax(apiHost, apiUrl + turl, mType, aData).request(jsonData);

    },
    sendRequest: function (turl, mType, aData, apiHost, apiUrl, callbackMethod) {
        
        common.getDataFromServer(turl, mType, aData, apiHost, apiUrl, {
            "CallBackHandler": function (data) {
                data.result = true;
                callbackMethod(data);
            },
            "ErrorCallBackHandler": function (errordata) {
                errordata.result = false;
                callbackMethod(errordata);
            }
        });
    },
    EasyAjax: function (aHost, cUrl, mType, reqd) {

        var xhr = new easyXDM.Rpc({
            remote: aHost
        }, {
            remote: {
                request: {}
            }
        });
        var _selfEasyAjax = {
            request: function (jsonData) {
                var callBack = jsonData.CallBackHandler;
                var errorCallBack = jsonData.ErrorCallBackHandler;
                xhr.request({
                    url: cUrl,
                    method: mType,
                    timeout: 60000,
                    cache: true,
                    headers: {
                        "Accept": "application/json, text/javascript, */*; q=0.01"
                    },
                    data: reqd
                }, function (response) {
                    if (response.data == "" || response.data == undefined)
                        response.data = "{}";
                    var json = easyXDM.getJSONObject().parse(response.data);
                    callBack(json);
                }, function (error) {
                    if (error.data != undefined) {
                        var json = easyXDM.getJSONObject().parse(error.data.data);
                        errorCallBack(json);
                    } else {
                        var errorjson = {};
                        errorjson.CustomError = "error";
                        errorCallBack(errorjson);
                    }
                });
            }
        };
        return _selfEasyAjax;
    },
    getLocalTime: function (unixTime, type) {
        var dateObj = new Date(unixTime * 1);

        var year = dateObj.getFullYear()
        var month = (dateObj.getMonth() * 1 + 1) > 9 ? (dateObj.getMonth() * 1 + 1) : "0" + (dateObj.getMonth() * 1 + 1);
        var date = dateObj.getDate() > 9 ? dateObj.getDate() : "0" + dateObj.getDate();
        var hour = dateObj.getHours() > 9 ? dateObj.getHours() : "0" + dateObj.getHours();
        var minute = dateObj.getMinutes() > 9 ? dateObj.getMinutes() : "0" + dateObj.getMinutes();
        var second = dateObj.getSeconds() > 9 ? dateObj.getSeconds() : "0" + dateObj.getSeconds();
        if (type == "min") {
            return year + "年" + month + "月" + date + "日" + " " + hour + ":" + minute;
        }

        return year + "." + month + "." + date;

    },
    //是否微信浏览器
    isWeiXin: function () {
        var ua = window.navigator.userAgent.toLowerCase();
        if (ua.match(/MicroMessenger/i) == 'micromessenger') {
            return true;
        } else {
            return false;
        }
    },
    replaceToken:function(){
        var token = decodeURIComponent(pageData.Token);
        token = token.replace(/\[PLUS\]/g, "+").replace(/\[LINE\]/g, "/").replace(/\[EQUAL\]/g, "=");
        return token
    },
    getPartyInfo: function (data, successCallback) {
        common.sendRequest(
            "parties/invitations/party",
            common.ajaxType.POST,
            data,
            pageData.apiHost,
            pageData.apiUrl,
            successCallback);
    },
    saveRegister:function(data,callback){
        common.sendRequest(
            "parties/invitations",
            common.ajaxType.POST,
            data,
            pageData.apiHost,
            pageData.apiUrl,
            callback);
    },
    getCardId: function (data, callback) {
        common.sendRequest(
            "query/wecards",
            common.ajaxType.POST,
            data,
            pageData.apiHost,
            pageData.apiUrl,
            callback);
    },
    cardSuccess: function (data, callback) {
        common.sendRequest(
            "wechart/cards",
            common.ajaxType.POST,
            data,
            pageData.apiHost,
            pageData.apiUrl,
            callback);
    },
    trackEvent: function (TrackEventId, Description, UinonId, ContactId, Level,callback) {
        var data = {
            EventId: TrackEventId,
            ClientKey: ClientId,
            EventName: 'Iparty',
            Description: Description,
            UinonId: UinonId,
            ContactId: ContactId,
            LevelId: Level,
            SourceType: common.browserType()
        };
        common.sendRequest("track/event", common.ajaxType.GET, data, TrackApiHost, TrackApiUrl, function () { callback()});
    },
    convertTime: function (endTimeStr) {
        
        var timeIndexStart = endTimeStr.indexOf("(");
        var endTime = endTimeStr.substr(timeIndexStart + 1, timeIndexStart + 8);
        //console.log(endTime);
        var nowtime = new Date();
        nowtime.setTime(endTime);
        //console.log(nowtime);
        return nowtime;

    },
    partyDate: function (startText,endText) {
        var start = common.convertTime(startText);
        var end = common.convertTime(endText);

        var meetingtime = start.getFullYear() + "年"
            + (start.getMonth() + 1) + "月"
            + start.getDate() + "日 "
            + start.getHours() + ":" + (start.getMinutes() > 9 ? start.getMinutes() : "0" + start.getMinutes()) + " - "
            + end.getHours() + ":" + (start.getMinutes() > 9 ? start.getMinutes() : "0" + start.getMinutes());
        return meetingtime;
    },
    showLoading: function () {
        $("body").addClass("loading-open")
        $(".loading").show();
        $(".loading-backdrop").show();
    },
    hideLoading: function () {
        $(".loading").hide();
        $(".loading-backdrop").hide();
        $("body").removeClass("loading-open")
    }
}
