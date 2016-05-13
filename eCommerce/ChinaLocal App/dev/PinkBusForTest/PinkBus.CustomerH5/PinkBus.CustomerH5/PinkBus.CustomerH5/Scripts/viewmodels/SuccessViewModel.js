define(['jquery', 'httpClient',
    'moment', 'jquery-qrcode', 
    'jquery-iplugin', 'main'], function ($, httpClientModel, moment, qrcode) {
        function Client() {           
            var httpClient = new httpClientModel();        
            var self = this;
            self.init = function (loadingobj) {
                if ($.isWeiXin()) {
                    httpClient.QueryInvitation({
                        TicketKey: ticketKey
                    }, function (data) {
                        self.login(data, loadingobj);
                    });
                    httpClient.CheckWechatRemind({}, function (data) {
                        if (!data.isCheck) {
                            $('.success-modal').show();
                            $('.success-modal-bg').show();
                        }
                    });
                } else {
                    httpClient.QueryInvitationBrowser({
                        TicketKey: ticketKey
                    }, function (data) {
                        self.login(data, loadingobj);
                    })
                }
                $('#smClose').on('click', function (e) {
                    $('.success-modal').hide();
                    $('.success-modal-bg').hide();
                });
            };
            self.login = function (data, loadingobj) {
                if (data.TicketType.toLowerCase() == "vip") {
                    $("body").removeClass().addClass("cardtype1");
                    $(".card-customerType").text("贵宾：");
                    $("#card-type").text("贵宾邀请券");
                    document.title = "贵宾邀请券";
                } else {
                    $("body").removeClass().addClass("cardtype2");
                    $(".card-customerType").text("来宾：");                  
                    document.title = "来宾邀请券";
                    $("#card-type").text("来宾邀请券");
                }
                $('#card-eventTitle').text(data.EventBaseInfo.EventTitle);
                $('#card-location').text(data.EventBaseInfo.Location);
                $('#card-dateTime').text(
                        (moment(data.EventBaseInfo.EventStartDate).format("YYYY/MM/DD") == moment(data.EventBaseInfo.EventEndDate).format("YYYY/MM/DD")) ?
                        (moment(data.EventBaseInfo.EventStartDate).format("YYYY.MM.DD HH:mm") + " - " + moment(data.EventBaseInfo.EventEndDate).format("HH:mm")) :
                        (moment(data.EventBaseInfo.EventStartDate).format("YYYY.MM.DD HH:mm") + " - " + moment(data.EventBaseInfo.EventEndDate).format("YYYY.MM.DD HH:mm"))

                    );
                $('#card-consultantName').text( data.LastName+data.FirstName);               
                $('#card-consultantPhone').text($.formatPhone(data.ConsultantPhone));
                $('#card-tel').attr("href", "tel:" + data.ConsultantPhone);
                $('#card-sms').attr("href", "sms:" + data.ConsultantPhone);

                if ($.isWeiXin()) {
                    $('#card-customer').text(data.CustomerName);
                    $('section:not(#card-success-wechat)').remove();
                    $('#card-success-wechat').show();
                } else {
                    $('#card-customer-2').text(data.CustomerName);
                    $('section:not(#card-success-brower)').remove();
                    $('#card-success-brower').show();
                }
                //---------------------------------
                qrico_image = new Image();
                qrico_image.src = VirtualDirectory + '/Content/Images/qrcodeico250.jpg';
                qrico_image.onload = function () {
                    self.updateQrCode(QRCodeUrl+ticketKey);
                }
                self.hideLoading(loadingobj);
                //---------------------------------

             
            };
            self.updateQrCode=function(text) {               
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
            self.showLoading = function () {
                return new Loading();
            };
            self.hideLoading = function (loadingobj) {
                $.proxy(loadingobj.destory, loadingobj)();
            };
        }
        return Client;
    })