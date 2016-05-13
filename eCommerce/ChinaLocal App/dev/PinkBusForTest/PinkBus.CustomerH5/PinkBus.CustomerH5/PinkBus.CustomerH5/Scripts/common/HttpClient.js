define(['jquery', 'jquery-iplugin'], function ($) {
    function Client() {
        var self = this;
        var ajaxType = {
            GET: "GET",
            POST: "POST"
        };
        var basePostData = {
            Device: "Mobile",
            DeviceOS: "Andorid_IOS",
            DeviceVersion: "8.x",
            _UserId: "",
            _SubsidiaryCode: "CN",
            _ClientKey: "ProjectName",
            _UserName: "User"
        };
        if (!window.location.origin) {

            window.location.origin = window.location.protocol + "//"
              + window.location.hostname
              + (window.location.port ? ':' + window.location.port : '');
        }
        var copyObject = function (fromObj, toObj) {
            var k = null;
            for (var item in fromObj) {
                k = false;
                for (var p in toObj) {
                    if (item == p) {
                        k = true;
                        continue;
                    }
                }
                if (!k) {
                    toObj[item] = fromObj[item];
                }
            }
            return toObj;
        };
        var paramsHandler = function (params) {
            var newparam = "";
            if (params === null || params === "")
                return null;

            for (var item in params) {
                newparam += (item + "=" + params[item]) + "&";
            }
            newparam = newparam.substring(0, newparam.length - 1);
            return newparam;
        };
        var errorCount = 0;
        /**
        * Make a X-Domain request to url and callback.
        *
        * @param url {String}
        * @param method {String} HTTP verb ('GET', 'POST', 'DELETE', etc.)
        * @param data {JsonObject or null} request body 
        * @param callback {Function} to callback on completion
        * @param errback {Function} to callback on error
        */
        function corsRequest(url, method, data, callback, errback) {
            var originData = data;
            if (!data) {
                data = {};
            }
            if ($.isWeiXin()) {
            // var at = "QAAAAGJrpxapbbzwp3ATsTAQQFu-YeaY_ksaNCVH-8MRqxbupwt1KMylpdcYprxMdo_V8qlgxB1woZKCFUyaPtFZvXMEAQAAQAAAAF0acGmMdREQb0yRa9e894vet8br0ngs9_rxI-FQwc0JGfqO_ITEP4nIGRnXhDOGMseOaewolctJMPU3MGjqaUK3L2RvROympFFnrT7YY2Z6Ro9hyHZyYsP3YTNLGKso4Iqi_7UzDT4zNmiB64TaHNNghMJBs7Rq8r8HTZPv9sVpQK4svyfBjPqsw6dbtzOk9yCEMy19klmL6YmZpDp-xeB-Gsl94A36ruNYqMelzIq6Qmjt0csIxd4cHgZFev1YNYcDOtESRR8FgdZmDfqc1BobjbdyKU-KD7KQmpj8jtM47hlsxJDmuDYIwsbZES889HPP9THNgCY0TcO7SFiUzf8";
            data.access_token = localStorage.getItem(localStorage.UnionID);
            //data.access_token = at;
            console.log("AT=" + data.access_token);
            }
            copyObject(basePostData, data);
            url = ajaxType.GET === method ? (url + "?" + paramsHandler(data)) : url;
            var req;
            if (XMLHttpRequest) {
                req = new XMLHttpRequest();
                if ('withCredentials' in req) {
                    req.open(method, url, true);
                    //req.withCredentials = true;                  
                    req.onreadystatechange = function (t) {
                        if (req.readyState === 4) {
                           // alert("req.status"+req.status);
                            if (req.status >= 200 && req.status < 400) {
                                callback(JSON.parse(req.responseText));
                            }
                            else if (req.status == 401) {
                               
                                if ($.isWeiXin()) {
                                    errorCount = errorCount + 1;
                                    if (errorCount > 3) {
                                        errorCount = 0;
                                        window.location.href = VirtualDirectory + "/Shared/WeChatError";
                                    }
                                    self.GetAccessToken(function (re) {
                                        localStorage.setItem(localStorage.UnionID, re.accessToken);
                                        corsRequest(url, method, originData, callback, errback);
                                    }, null);
                                } else {
                                    errback({
                                        readyState: req.readyState,
                                        statusText: req.statusText,
                                        status: req.status
                                    });
                                }
                            } else {
                                errback({
                                    statusText: req.statusText,
                                    responseText: JSON.parse(req.responseText),
                                    status: req.status
                                });
                            }


                        }
                    };
                }
            } else if (XDomainRequest) {
                req = new XDomainRequest();
                req.open(method, url);
                req.onload = function () {
                    callback(JSON.parse(req.responseText));
                };
            }
            req.setRequestHeader("Accept", "application/json");
            if (method == ajaxType.POST) {
                req.setRequestHeader("Content-Type", "application/json");
            }
           // alert("URL:"+url);
            req.send(ajaxType.GET === method ? null : JSON.stringify(data));
        }

        self.GetAccessToken = function (reqd, callback, errback) {
            corsRequest(window.location.origin + VirtualDirectory + "/Wechat/GetAccessToken", ajaxType.POST, reqd, callback, errback);
        };
        self.GetUnionId = function (reqd, callback, errback) {
            corsRequest(window.location.origin + VirtualDirectory + "/WeChat/GetUnionId", ajaxType.GET, null, callback, errback);
        };

        self.TrackEvent = function (reqd, callback, errback) {
            corsRequest(TrackEventAPIUrl + "track/event", ajaxType.GET, reqd, callback, errback);
        };     
        //-------------
        self.CheckWechatRemind = function (reqd, callback, errback) {
            corsRequest(window.location.origin + VirtualDirectory + "/Wechat/CheckWechatRemind", ajaxType.GET, reqd, callback, errback);
        };
        self.AcceptInvitation = function (reqd, callback, errback) {         
            corsRequest(EXAPIUrl + "customers/invitation/accept/put", ajaxType.POST, reqd, callback, errback);
        };
        self.QueryInvitationBrowser = function (reqd, callback, errback) {
            corsRequest(EXAPIUrl + "customer/invitation/query/" + reqd.TicketKey, ajaxType.GET, reqd, callback, errback);
        };
        self.QueryInvitation = function (reqd, callback, errback) {         
            corsRequest(EXAPIUrl + "customer/invitation/query", ajaxType.GET, reqd, callback, errback);
        };

    }
    return Client;
});

