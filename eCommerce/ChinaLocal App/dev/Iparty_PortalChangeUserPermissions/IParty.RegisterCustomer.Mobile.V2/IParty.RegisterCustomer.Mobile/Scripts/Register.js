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
    setBindEvent: function () {
        $(".btnSubmit").unbind("click").click(function () {
            //if (!form[0].checkValidity()) { $("#hideSubmit").click(); return false; }
            if (registerView.CheckData() == false) return false;
            registerView.disableSubmit();
            common.showLoading();
            //console.log(ko.toJS(viewModel));
            var mdata = {
                InvitationToken: common.replaceToken(),
                Name: viewModel.name(),
                PhoneNumber: viewModel.mobile(),
                ReferenceBy: viewModel.ForwordContactID(),
                UnionId:"",
                IsOnSite: false
            };
            common.saveRegister(mdata, function (data) {
                //console.log(data);
                common.hideLoading();
                if (!data.ResponseStatus) {
                    common.trackEvent("2EB1B5EF-AA2E-4021-BD46-0F083B844C8B", "Register", "", data.ContactId, data.Level, function () { });
                    if (pageData.debug) Alert('.card-form', "来宾邀请券生成成功", 1500);
                    $(".header").addClass("headerSuccess")
                    registerView.makeQRCode(data.InvitationKey + "_MB");
                    viewModel.showQRCode(true);
                }
                else { Alert('.card-form', data.ResponseStatus.Message, 4000); common.hideLoading(); registerView.enableSubmit(); registerView.setBindEvent(); }
            });
        });
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
        viewModel.showQRCode = ko.observable(false);
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
                   // Alert('.card-form', data.ResponseStatus.Message, 3000);;
                    window.location.href = "/iparty/home/error?msg=" + data.ResponseStatus.Message; return;
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