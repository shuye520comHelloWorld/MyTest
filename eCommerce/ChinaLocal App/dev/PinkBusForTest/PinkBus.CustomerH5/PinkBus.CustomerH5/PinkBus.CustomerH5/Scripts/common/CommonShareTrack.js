define(['jquery', 's_code', 'httpClient', 'WechatJS'], function ($, s, httpClient, WXJSDK) {
    function Client() {
        var self = this;
        self.s_code_exec = function (pageName, chanel) {
            s.pageName = pageName;
            s.channel = chanel;
            s.t();
        };

        self.s_code_sendevent = function (eventName, isSendUserRole) {
            s.events = eventName;
            if (isSendUserRole == true) {
                s.prop1 = userRole;//当前用户
            }
            s.t();
        };

        //分享到朋友圈
        self.ShareToTimeline = function (title, link, desc, imgUrl,callback) {
            var sharemodel = {
                title: title,
                link: link,
                desc: desc,
                imgUrl: imgUrl
            }
            WXJSDK.ShareToTimeline(sharemodel,callback,
               // function () {                    
                    //httpClient.GetUnionId({}, function (data) {
                    //    httpClient.TrackEvent({
                    //        EventId: "E0EC0923-13E2-407E-95B8-1BBF05FF8C0D",
                    //        ClientKey: ClientID,
                    //        UnionId: data.unionId,
                    //        Description: JSON.parse(localStorage.getItem("resourceData")).ResourceID + ":" + JSON.parse(localStorage.getItem("resourceData")).ResourceType,
                    //        ContactId: "",
                    //        LevelId: "",
                    //        SourceType: "mobile"
                    //    }, function () { });
                    //});                    
                //},
                function () {
                    //fail 
                    // alert("fail");
                });
        };

        //分享给朋友
        self.ShareToAppMsg = function (title, link, desc, imgUrl,callback) {
            // alert("Share" + JSON.stringify(JSON.parse(localStorage.getItem("resourceData"))));  
            var sharemodel = {
                title: desc,
                link: link,
                desc: title,
                imgUrl: imgUrl
            }
            WXJSDK.ShareToAppMsg(sharemodel,callback
               // function () {                    
                    //httpClient.GetUnionId({}, function (data) {
                    //    httpClient.TrackEvent({
                    //        EventId: "E0EC0923-13E2-407E-95B8-1BBF05FF8C0D",
                    //        ClientKey: ClientID,
                    //        UnionId: data.unionId,
                    //        Description: JSON.parse(localStorage.getItem("resourceData")).ResourceID + ":" + JSON.parse(localStorage.getItem("resourceData")).ResourceType,
                    //        ContactId: "",
                    //        LevelId: "",
                    //        SourceType: "mobile"
                    //    }, function () { });
                    //});
                  
                    //},
                    ,
                function () {
                    //alert("fail");
                    //card fail
                });
        };
     
        self.pageLoadH5PV = function (page, program) {
            self.s_code_exec(program + ":" + page, program);
        };
       
    }
    return Client;
})
