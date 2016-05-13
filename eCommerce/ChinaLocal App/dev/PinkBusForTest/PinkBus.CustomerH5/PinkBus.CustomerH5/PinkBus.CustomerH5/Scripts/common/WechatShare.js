define(['jquery', 'WechatJS', 'jweixin', 'CommonShareTrack', 'httpClient'], function ($, WXJSDK, wx, cst, httpClientModel) {
    var cst = new cst();
    $(function () {
        //注册微信jsdk
        var url = location.href.split('#')[0];
        $.getJSON(VirtualDirectory + "/wechat/GetWechatConfig", { url: url }, function (data) {
            wx.config({
                debug: location.host.indexOf('dev') > -1 ? true : false,
                appId: data.appId,
                timestamp: data.timestamp,
                nonceStr: data.noncestr,
                signature: data.signature,
                jsApiList: WXJSDK.jsApiList
            });
        });
    });
    //Test
    wx.ready(function () {
        // 1 判断当前版本是否支持指定 JS 接口，支持批量判断
        wx.checkJsApi({
            jsApiList: [
              'addCard',
              'onMenuShareTimeline',
              'onMenuShareAppMessage'
            ],
            success: function (res) {
                if (res.checkResult.onMenuShareAppMessage) {
                    var httpClient = new httpClientModel();
                    httpClient.QueryInvitation({
                        TicketKey: ticketKey
                    }, function (data) {
                        var title = "";

                        if (data.TicketType.toLowerCase() == "vip") {
                            title = "粉巴美丽到家·贵宾邀请券，点我立即领取!";
                        } else {
                            title = "粉巴美丽到家·来宾邀请券，点我立即领取!";
                        }
                        cst.ShareToTimeline(
                             title,
                             window.location.href,
                             data.EventBaseInfo.EventTitle,
                            window.location.protocol + "//"
                            + window.location.hostname
                            + (window.location.port ? ':' + window.location.port : '') + "/" + VirtualDirectory + '/Content/Images/icon-wechatshore.jpg'
                             );
                        cst.ShareToAppMsg(
                           data.EventBaseInfo.EventTitle,
                            window.location.href,
                            title,
                           window.location.protocol + "//"
                            + window.location.hostname
                            + (window.location.port ? ':' + window.location.port : '') + "/" + VirtualDirectory + '/Content/Images/icon-wechatshore.jpg'
                            );
                    }, function (re) { $.alert(re.responseText.ResponseStatus.Message); })
                }
            }
        });
    });
})
