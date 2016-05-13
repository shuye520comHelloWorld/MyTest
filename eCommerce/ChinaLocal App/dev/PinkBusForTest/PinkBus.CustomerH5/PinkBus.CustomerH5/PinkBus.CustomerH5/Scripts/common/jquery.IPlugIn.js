define(['jquery'], function (jQuery) {
    (function($){
        $.fn.extend({
            checkTextLength:function(maxLength,minLength){
                var $this=$(this);
                var result=false; 
                var length = $this.val().length;                
                if (length>=minLength&&length<=maxLength) {
                    result=true;
                }
                return result;
            },
            checkPhoneNumber:function(){
                var $this=$(this);
                var result=false; 
                var txt=$this.val();                
                var re = /^[1][0-9]{10}$/;   //判断字符串是否为数字 //判断正整数 /^[1-9]+[0-9]*]*$/
                if (re.test(txt)) {
                    result= true;
                }               
                return result;
            },
            checkChinaAndText: function () {
                var $this = $(this);
                var result= false;
                var txt = $this.val();
                var re = /^[\u4e00-\u9fa5a-zA-Z]+$/;
                if (re.test(txt)) {
                    result = true;
                }
                return result;
            }
        });
        $.extend({
            formatPhone: function (pnumber) {
                var phone = "";//12345678900
                if (pnumber != "" && pnumber != undefined && pnumber.length != 0) {
                    phone = phone.concat(pnumber.substr(0, 3)).concat("-").concat(pnumber.substr(3, 4)).concat("-").concat(pnumber.substr(7, 4));
                    // console.log(phone);
                    return phone;
                }
                return "";
            },
            copyObject: function (fromObj, toObj) {
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
            },
            isWeiXin: function () {
                var ua = window.navigator.userAgent.toLowerCase();
                if (ua.match(/MicroMessenger/i) == 'micromessenger') {
                    return true;
                } else {
                    return false;
                }
            }
        });
    })(jQuery)
})