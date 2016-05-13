var registerView = {
    contentView: function () {
        $this = this;

    },
    disableSubmit: function () {
        $(".btnSubmit").unbind("click").css("background", "#C2B8AC");
    },
    enableSubmit: function () {
        $(".btnSubmit").css("background", "#f7941d");
    },
    shareParty: function () {
        //分享给朋友
        wx.onMenuShareAppMessage({
            title: shareData.title,
            desc: shareData.desc,
            link: shareData.link,
            imgUrl: shareData.imgUrl,
            trigger: function (res) {

                // 不要尝试在trigger中使用ajax异步请求修改本次分享的内容，因为客户端分享操作是一个同步操作，这时候使用ajax的回包会还没有返回
               // alert('用户点击发送给朋友'); alert(shareData.link);
            },
            success: function (res) {
                common.trackEvent("32FFEF19-B4A6-4879-89D6-C619A2CA03E7", "friend", pageData.unionId, "", "", function () { });
                Alert('.card-form', "已分享成功,快去告诉他们吧！", 2000);
                //alert('已分享成功,快去告诉他们吧！');
            },
            cancel: function (res) {
                Alert('.card-form', "您已取消分享!", 1500);
                //alert('您已取消分享');
            },
            fail: function (res) {
                //alert(JSON.stringify(res));
            }
        });
        //alert('已注册获取“发送给朋友”状态事件');

        //分享到朋友圈
        wx.onMenuShareTimeline({
            title: shareData.title,
            link: shareData.link,
            imgUrl: shareData.imgUrl,
            trigger: function (res) {
                // 不要尝试在trigger中使用ajax异步请求修改本次分享的内容，因为客户端分享操作是一个同步操作，这时候使用ajax的回包会还没有返回
                //alert('用户点击分享到朋友圈');
            },
            success: function (res) {
                common.trackEvent("32FFEF19-B4A6-4879-89D6-C619A2CA03E7", "timeline", pageData.unionId, "", "", function () { });
                Alert('.card-form', "已分享成功！", 1000);
            },
            cancel: function (res) {
                Alert('.card-form', "您已取消分享!", 1000);
            },
            fail: function (res) {
                //alert("分享失败");
            }
        });
        //alert('您在可以在申请邀请卡之后在右上角分享并邀请给其他人哦');
    },
    setBindEvent: function () {
        $(".btnSubmit").unbind("click").click(function () {
            ///alert(333);
            //if (!form[0].checkValidity()) { $("#hideSubmit").click(); return false; }
            if (registerView.CheckData() == false) return false;
            //registerView.disableSubmit();
            common.showLoading();
            var mdata = {
                InvitationToken: common.replaceToken(),
                Name: viewModel.name(),
                PhoneNumber: viewModel.mobile(),
                ReferenceBy: viewModel.ForwordContactID(),
                UnionId:viewModel.unionId(),
                IsOnSite: false
            };
            common.saveRegister(mdata, function (data) {
                //common.hideLoading();

                if (!data.ResponseStatus) {
                    registerView.makeQRCode(data.InvitationKey + "_W");
                   
                    //alert(viewModel.openId())
                    if (viewModel.openId() == "" || !common.isWeiXin()) {
                        common.hideLoading();
                        registerView.showViews(0, 0, 1, 1);

                        //alert("抱歉,您可能领取卡券失败,请【务必截图并保存当前页面的二维码】进行活动到场签到！");
                        common.trackEvent("2EB1B5EF-AA2E-4021-BD46-0F083B844C8B", "WechatRegister", pageData.unionId, data.ContactId, data.Level, function () {});
                    }
                    else {

                       // alert("来宾邀请券生成成功，接下来请将卡券领取到微信卡包.");
                        
                        //registerView.showViews(0, 1, 0);
                        viewModel.cardCode(data.InvitationKey);
                        //viewModel.referenceKey(data.Invitation.CustomerKey);
                        shareData.link = shareData.link + "&referenceKey=" + data.CustomerKey;
                        registerView.shareParty();
                        common.trackEvent("2EB1B5EF-AA2E-4021-BD46-0F083B844C8B", "WechatRegister", pageData.unionId, data.ContactId, data.Level, function () {
                            
                            addCard(data.InvitationKey);//拼凑卡券参数信息
                        });
                    }
                    
                }
                else { Alert('.card-form', data.ResponseStatus.Message, 4000); common.hideLoading(); registerView.enableSubmit(); registerView.setBindEvent(); }
            });

        });
    },
    showViews: function (a,b,c,d) {
        if(a==0)
            $("#InfoView").addClass("hide");
        else
            $("#InfoView").removeClass("hide");

        if (b == 0)
            $("#SuccessView").addClass("hide");
        else
            $("#SuccessView").removeClass("hide");

        if (c == 0)
            $("#MyQRCodeView").addClass("hide");
        else
            $("#MyQRCodeView").removeClass("hide");

        if (d == 0)
            $("#CreateView").addClass("hide");
        else
            $("#CreateView").removeClass("hide");
        
        
    },
    CheckData: function () {
        

        if ($.trim(viewModel.name()) == "" || $.trim(viewModel.mobile()) == "") {
            Alert('.card-form', "姓名/手机号码不能为空！", 1500);
            return false;
        }

        if (!(/^[a-zA-Z\u4e00-\u9fa5]{1,10}$/.test(viewModel.name()))) {
            Alert('.card-form', "请填写正确的姓名！", 2000);
            $("#name").focus();
            return false;
        }


        var mobile = viewModel.mobile();

        if (!(/^1[3|4|5|7|8][0-9]\d{8}$/.test(mobile))) {
            Alert('.card-form', "请填写正确的手机号码！", 1500);
            $(".tel").focus();
            return false;
        }
        return true;

    },
    mappingData: function (source) {
        viewModel = ko.mapping.fromJS(source);
        viewModel.name = ko.observable('');
        viewModel.mobile = ko.observable('');
        viewModel.referenceKey = ko.observable(pageData.referenceKey);
        viewModel.Token = ko.observable(pageData.Token);
        viewModel.unionId = ko.observable(pageData.unionId);
        viewModel.openId = ko.observable(pageData.openId);
        viewModel.cardCode = ko.observable('');
        //viewModel.PartyKey = ko.observable('219ACA8D-0E00-4CF8-AF71-05C58FE0EDF1');

    },
    makeQRCode: function (value) {
        var qr = new QRCode("qrcode", {
            text: value,
            width: 160,
            height: 160,
            correctLevel: QRCode.CorrectLevel.M
        });
    },
    ready: function () {
        $(function () {
            common.showLoading();
            var mdata = { InvitationToken: common.replaceToken() }
            common.getPartyInfo(mdata, function (data) {
                common.hideLoading();
                if (data.ResponseStatus) {
                    //alert(data.ResponseStatus.Message);
                    window.location.href = "/iparty/home/error?msg=" + data.ResponseStatus.Message;
                    return;
                }

                data.meetingtime = common.partyDate(data.PartyDisplayStartDate, data.PartyDisplayEndDate);

                registerView.mappingData(data);
                registerView.setBindEvent();

                ko.applyBindings(viewModel, $("#mobile")[0]);

            });
        });

    }
};
var viewModel = new registerView.contentView();
registerView.ViewModel = viewModel;
registerView.ready();