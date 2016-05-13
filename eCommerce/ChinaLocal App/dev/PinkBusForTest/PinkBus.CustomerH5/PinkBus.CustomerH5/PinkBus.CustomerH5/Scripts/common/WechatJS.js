define(['jweixin'], function (wx) {
    var WXJSDK = {
        jsApiList: [
            'onMenuShareTimeline',
            'onMenuShareAppMessage',
            'onMenuShareQQ',
            'addCard',
            'openCard',
            'hideOptionMenu',
            'showOptionMenu',
            'closeWindow'
        ],
        ShareToTimeline: function (ToFriend, success, cancel) {

            if (typeof (ToFriend) != "object") alert("not object")
            wx.onMenuShareTimeline({
                title: ToFriend.title, // 分享标题
                link: ToFriend.link, // 分享链接
                desc: ToFriend.desc, // 分享描述
                imgUrl: ToFriend.imgUrl, // 分享图标
                success: function () {
                    if (typeof (success) != "function") {
                      //  alert("not function")
                    }
                    success()
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    if (typeof (cancel) != "function") {
                       // alert("not function")
                    }
                    cancel()
                    // 用户取消分享后执行的回调函数
                }
            });

        },
        ShareToAppMsg: function (AppMsg, success, cancel) {
            if (typeof (AppMsg) != "object") alert("not object")
            wx.onMenuShareAppMessage({
                title: AppMsg.title, // 分享标题
                desc: AppMsg.desc, // 分享描述
                link: AppMsg.link, // 分享链接
                imgUrl: AppMsg.imgUrl, // 分享图标
                type: AppMsg.type, // 分享类型,music、video或link，不填默认为link
                dataUrl: AppMsg.dataUrl, // 如果type是music或video，则要提供数据链接，默认为空
                success: function () {
                    if (typeof (success) != "function") {
                       // alert("not function")
                    }
                    success();
                },
                cancel: function () {
                    if (typeof (cancel) != "function") {
                        //alert("not function")
                    }
                    cancel()
                    // 用户取消分享后执行的回调函数
                }
            });
        },
        ShareToQQ: function (ToQQ, success, cancel) {
            wx.onMenuShareQQ({
                title: ToQQ.title, // 分享标题
                desc: ToQQ.desc, // 分享描述
                link: ToQQ.link, // 分享链接
                imgUrl: ToQQ.imgUrl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    if (typeof (success) != "function") {
                        alert("not function")
                    }
                    success()
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    if (typeof (cancel) != "function") {
                        alert("not function")
                    }
                    cancel()
                }
            });
        },
        AddWXCard: function (cardlist, success, cancel, fail) {
            wx.addCard({
                cardList: cardlist, // 需要添加的卡券列表
                success: function (res) {
                    // 添加的卡券列表信息
                    if (typeof (success) != "function") {
                        alert("not function")
                    }
                    success(res)
                },
                cancel: function (res) {
                    // 用户取消分享后执行的回调函数
                    if (typeof (cancel) != "function") {
                        alert("not function")
                    }
                    cancel(res);
                },
                fail: function (res) {
                    if (typeof (fail) != "function") {
                        alert("not function")
                    }

                    fail(res);
                }
            });
        },
        OpenWXCard: function (cardlist, fail) {
            wx.openCard({
                cardList: cardlist,
                fail: function (res) {
                    if (typeof (fail) != "function") {
                        alert("not function")
                    }
                    fail(res);
                }
            });
        },
        HideMenu: function () {
            wx.hideOptionMenu();
        },
        ShowMenu: function () {
            wx.showOptionMenu();
        },
        CloseWindow: function () {
            wx.closeWindow();
        },
        TempConfig: {
            ProjectName: "WechatTemp",
            ClientId: "",
            WechatDebug: false,
            Bind_Consultant: false,
            Card_UseCustomcode: false,

        },
    }

    return WXJSDK;
})
