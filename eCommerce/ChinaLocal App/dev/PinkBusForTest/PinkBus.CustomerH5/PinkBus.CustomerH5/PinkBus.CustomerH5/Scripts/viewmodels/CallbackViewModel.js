define([
    'httpClient'],
    function (httpClientModel) {
        var httpClient = new httpClientModel();
        var viewModel = function () {
            var self = this;
            //alert(returnUrl+"+1");
            self.SetAccessToken = function () {
                httpClient.GetAccessToken({}, function (re) {
                    //alert("re="+JSON.stringify(re));
                    localStorage.setItem(localStorage.UnionID, re.accessToken);
                    //alert(returnUrl);
                    //var newReturnUrl = returnUrl;
                    //if (newReturnUrl.indexOf("://") > 0) {
                    //    var newReturnArrUrl = returnUrl.split("://");
                    //    newReturnArrUrl[0] = location.protocol.split(":")[0];
                        
                    //    newReturnUrl = newReturnArrUrl.join("://");
                    //}
                  
                    window.location.href = returnUrl;
                });
            }
        }
        return viewModel;

    });


