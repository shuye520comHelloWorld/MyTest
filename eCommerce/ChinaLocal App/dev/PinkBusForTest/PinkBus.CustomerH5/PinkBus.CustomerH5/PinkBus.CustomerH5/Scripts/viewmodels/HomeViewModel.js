define(['jquery',
   'httpClient',
    'moment',
    'jquery-qrcode',   
    'main',
    'jquery-iplugin',
    'WechatShare'
],
    function ($, httpClientModel, moment, qrcode) {

      
        function Client() {
            var self = this;          
            var httpClient = new httpClientModel();
            self.showLoading = function () {
                return new Loading();
            };
            self.hideLoading = function (loadingobj) {
                $.proxy(loadingobj.destory, loadingobj)();
            };
            self.init = function (loadingobj) {
               
                if ($.isWeiXin()) {
                    httpClient.QueryInvitation({
                        TicketKey: ticketKey
                    }, function (data) {
                       
                        self.login(data, loadingobj);
                    }, function (re) { $.alert(re.responseText.ResponseStatus.Message); })
                } else {
                    httpClient.QueryInvitationBrowser({
                        TicketKey: ticketKey
                    }, function (data) {
                        self.login(data, loadingobj);
                    }, function (re) { $.alert(re.responseText.ResponseStatus.Message); })
                }
            };
            self.createQRCode = function (text) {
                qrico_image = new Image();
                qrico_image.src = VirtualDirectory + '/Content/Images/qrcodeico250.jpg';
                qrico_image.onload = function () {
                    self.updateQrCode(QRCodeUrl + ticketKey);
                }
            };
            self.updateQrCode = function (text) {
                var options = {
                    render: "canvas",
                    ecLevel: "H",
                    minVersion: 5,

                    fill: "#333333",
                    background: "#ffffff",
                    // fill: $('#img-buffer')[0],

                    text: text,
                    size: 160,
                    radius: 0,
                    quiet: 0,

                    mode: 4,

                    mSize: 0.3,
                    mPosX: 0.5,
                    mPosY: 0.5,

                    // label: $('#label').val(),
                    //fontname: $('#font').val(),
                    //fontcolor: $('#fontcolor').val(),

                    image: qrico_image
                };
                $('#container').empty().qrcode(options);
            };
            self.login = function (data, loadingobj) {
                moment.locale('zh-cn');
                // alert(JSON.stringify(data));
                if (data.TicketType.toLowerCase() == "vip") {
                    $("body").removeClass().addClass("cardtype1");
                    $("#card-type").text("贵宾邀请券");
                    document.title = "贵宾邀请券";
                } else {
                    $("body").removeClass().addClass("cardtype2");
                    document.title = "来宾邀请券";
                    $("#card-type").text("来宾邀请券");
                }
                $('#card-eventTitle').text(data.EventBaseInfo.EventTitle);
                $('#card-location').text(data.EventBaseInfo.Location);
                $('#card-dateTime').text(
                    (moment(data.EventBaseInfo.EventStartDate).format("YYYY/MM/DD") == moment(data.EventBaseInfo.EventEndDate).format("YYYY/MM/DD"))?
                    (moment(data.EventBaseInfo.EventStartDate).format("YYYY.MM.DD HH:mm") + " - " + moment(data.EventBaseInfo.EventEndDate).format("HH:mm")) :
                    (moment(data.EventBaseInfo.EventStartDate).format("YYYY.MM.DD HH:mm") + " - " + moment(data.EventBaseInfo.EventEndDate).format("YYYY.MM.DD HH:mm"))
                
                    );
                $('#card-consultantName').text(data.LastName+data.FirstName);
              
                $('#card-consultantPhone').text($.formatPhone(data.ConsultantPhone));
                $('#card-tel').attr("href", "tel:" + data.ConsultantPhone);
                $('#card-sms').attr("href", "sms:" + data.ConsultantPhone);
                $('#ticketKey').val(data.TicketKey);
                if (data.TicketStatus == "Inviting" &&//unused and unexpired
                    $.isWeiXin() && // "is wechat open"
                    data.WechatUserType == "Inviter" &&// "is card consultant"
                    !data.IsInvitationEnd
                    ) {
                    console.log($('section:not(#card-invalid)'));
                    $('section:not(#card-invalid)').remove();
                    $('#invitedendtime').text(moment(data.EventBaseInfo.InvitationEndDate).format("ll"));
                    $('#card-invalid').show();
                }
                else if (data.TicketStatus == "Invited" &&
                $.isWeiXin() &&
                (data.WechatUserType == "Inviter")) {
                    $('section:not(#card-successbyconsu)').remove();
                    $('.card-customerName').text(data.CustomerName);
                    //$('.card-userhead').attr("src", data.HeadImgUrl);
                    self.createQRCode();
                    $('#card-successbyconsu').show();

                } else if ($.isWeiXin() &&
                    data.TicketStatus == "Invited" &&
                    data.WechatUserType == "Customer") {
                    $('section:not(#card-successbyuser)').remove();
                    $('.user-name').text("来宾：" + data.CustomerName);
                    self.createQRCode();
                    $('#card-successbyuser').show();
                }
                else if (data.IsInvitationEnd ||
                    data.TicketStatus == "Expired" ||
                    (!$.isWeiXin() && data.TicketStatus == "Invited") ||
                    ($.isWeiXin() && data.TicketStatus == "Invited" && data.WechatUserType == "Other")) {
                    $('section:not(#card-expired)').remove();
                    $('#card-expired').show();
                }
                else {
                    $('section:not(.info-form)').remove();
                    var selectDate = {
                        'multi': ['topic', 'series', 'content'], //可以多选指示器,
                        'course': [{ text: "是", value: 1 }, { text: "否", value: 0 }],
                        'product': [{ text: "是", value: 1 }, { text: "否", value: 0 }],
                        'identity': [{ text: "老顾客", value: "Old" }, { text: "新顾客", value: "New" }, { text: "VIP", value: "VIP" }],
                        'age': [{ text: "25岁以下", value: "Blow25" }, { text: "25-35岁", value: "Between25And35" }, { text: "35-45岁", value: "Between35And45" }, { text: "45岁以上", value: "Above45" }],
                        'profession': [{ text: "公司职员", value: "Clerk" }, { text: "私营业主", value: "PrivateOwner" }, { text: "家庭主妇", value: "Housewife" }, { text: "自由职业", value: "Freelancers" }],
                        'topic': [{ text: "美容护肤", value: "SkinCare" }, { text: "彩妆技巧", value: "MakeUp" }, { text: "服饰搭配", value: "DressUp" }, { text: "家庭关系", value: "FamilyTies" }],
                        'series': [{ text: "幻时/幻时佳", value: "TimeWise" }, { text: "美白", value: "WhiteningSystemFoaming" }, { text: "经典", value: "Cleanser" }, { text: "舒颜", value: "CalmingInfluence" }, { text: "其他系列", value: "Other" }],
                        'content': [{ text: "美丽自信", value: "BeautyConfidence" }, { text: "公司文化", value: "CompanyCulture" }, { text: "事业机会", value: "BusinessOpportunity" }, { text: "其他", value: "Other" }]
                    };
                    $('.info-select').infoSelect(selectDate);
                    $('.info-form').show();
                    //
                    $('#name').focus(function () {
                        $('#error_name').text("");
                    });
                    $('#tel').focus(function () {
                        $('#error_phone').text("");
                    });
                    $('#identity').on('click', function () {
                        $('#error_identity').text("");

                    });
                    $('#age').on('click', function () {
                        $('#error_age').text("");

                    });
                    $('#submitform').click(function () {
                        var $this = $(this);
                        if ($this.attr("class").indexOf("disable") < 0) {
                            var err = false;
                            if ($('#name').val().length == 0) {
                                $('#error_name').text("请输入顾客姓名");
                                err = true;
                            } else if (!$('#name').checkChinaAndText()) {
                                $('#error_name').text("请输入正确的顾客姓名");
                                err = true;
                            }
                            if ($('#tel').val().length == 0) {
                                $('#error_phone').text("请输入顾客的手机号码");
                                 err = true;
                              
                            } else if (!$('#tel').checkPhoneNumber()) {
                                $('#error_phone').text("手机号码格式不正确");
                                err = true;
                            }
                            if ($('#hid-identity').val().length == 0) {
                                $('#error_identity').text("请选择来宾身份");
                                err = true;
                              
                            }
                            if ($('#hid-age').val().length == 0) {
                                $('#error_age').text("请选择年龄");
                                err = true;
                            }
                            if (err) {                               
                                return;
                            }
                            var reqd = {
                                TicketKey: $('#ticketKey').val(),
                                CustomerName: $('#name').val(),
                                CustomerPhone: $('#tel').val(),
                                CustomerType: $('#hid-identity').val(),//.substring(1, 2),
                                AgeRange: $('#hid-age').val(),//.substring(1, 2),
                                Career: $('#hid-profession').val(),//? $('#hid-profession').val().substring(1, 2):"",
                                InterestingTopic: $('#hid-topic').val(),
                                BeautyClass: $('#hid-course').val(),// ?  $('#hid-course').val().substring(1, 2):"",
                                UsedProduct: $('#hid-product').val(),//?  $('#hid-product').val().substring(1, 2):"",
                                UsedSet: $('#hid-series').val(),
                                InterestInCompany: $('#hid-content').val(),
                                UnionID:unionid,
                                HeadImgUrl: headImgUrl,
                                Source: $.isWeiXin() ? "Wechat" : "MobileBrowser"
                            };
                           
                            httpClient.AcceptInvitation(
                               reqd
                            , function (data) {
                                if (data.Result) {
                                    window.location.href = VirtualDirectory + "/home/success?TicketKey=" + data.TicketKey;
                                } else {
                                    $.alert(data.ErrorMessage);
                                }
                            }, function (re) {
                               // alert(JSON.stringify(re));
                                $.alert(re.responseText.ResponseStatus.Message);
                            })
                        }
                    });
                   
                }
                self.hideLoading(loadingobj);
               
            };
        }
        return Client;


    });


