require(['jquery', 'bootstrap', 'jquerydatetimepicker', 'json2', 'plugins/layer','laypage', 'commonAjax'],
    function ($, bootstrap, timepicker,json2, layer,laypage, Ajax) {
        var module = {
            getCustomers: function (pageNo) {
              
                $(".NewCustomerList").html("<div style=\"text-align:center;padding-top:100px;\"><img src=\"" + Application + "/Content/images/loading.gif\" style=\"margin:0 auto; \" /></div>");
                Ajax.easyAjax("/Home/NewCustomerList", "GET", { eventKey: $("#EventKey").val(), PageNo: pageNo, name: $("#customerName").val(), phone: $("#customerPhone").val() },
                            function (d) {
                                $(".NewCustomerList").html(d);
                                module.showPageBar(pageNo);
                            }, function (XMLHttpRequest, textStatus, errorThrown) {
                                console.log(textStatus)
                            });
            },
            showPageBar: function (currPage) {
               // var eventPageCount = $("#CustomerPageCount").val();
               // var countFrom = $("#PageCountFrom").val();
               // var countTo = $("#PageCountTo").val();
                var eventTotalRow = $("#TotalRow").val();
                var eventPageCount = $("#PageCount").val();
               // var pageTxt = "显示第 " + countFrom + " 至 " + countTo + " 条，共 " + eventTotalRow + " 条";
                $("#CustomerCount").html(eventTotalRow);
                if (eventPageCount == 1) {
                    $(".pull-right").html("<div><span style=\"background-color:#f16c74;margin-right:200px; padding: 0 12px;border-radius: 2px;line-height: 26px;color: white;display: inline-block;font-size: 12px;\">1</span></div>");
                } else {
                    laypage.dir = Application + "/content/css/laypage.css"
                    laypage({
                        cont: $(".pull-right"), //容器。值支持id名、原生dom对象，jquery对象,
                        pages: eventPageCount, //总页数
                        curr: currPage,
                        skin: '#f16c74', //皮肤
                        first: 1, //将首页显示为数字1,。若不显示，设置false即可
                        last: eventPageCount, //将尾页显示为总页数。若不显示，设置false即可
                        prev: '<', //若不显示，设置false即可
                        next: '>', //若不显示，设置false即可
                        jump: function (obj, first) {
                            if (!first)
                                module.getCustomers(obj.curr);
                        }
                    });
                }

            },
            ready: function () {
                $("body").on('click', ".activity-btn__import", function () {
                    var customerKey = $(this).parents("tr").attr("id").replace("tr_", "");
                     var customerInfo=JSON.parse($(this).parents("tr").find(".CustomerInfo").val())
                     console.log(customerInfo);

                     $(".A_name").html(customerInfo.CustomerName);
                     $(".A_contact").html(customerInfo.ContactInfo + "(" + customerInfo.ContactType.replace("PhoneNumber", "手机").replace("Wechat", "微信").replace("Other", "其他") + ")");
                     $(".A_age").html( customerInfo.AgeRange!=null?(customerInfo.AgeRange == 0 ? "25岁以下" : (customerInfo.AgeRange == 1) ? "25-35岁" : (customerInfo.AgeRange == 2 ? "35-45岁" : "45岁以上")):"");
                     //$(".A_age").html(customerInfo.AgeRange.HasValue ?( customerInfo.AgeRange.Value.ToString().Replace("Bellow25", "25岁以下").Replace("Between2535", "25-35岁").Replace("Between3545", "35-45岁").Replace("Above45", "大于45岁")) : "");
                     $(".A_hearMK").html(customerInfo.IsHearMaryKay!=null?(customerInfo.IsHearMaryKay==1? "是" : "否"):"");
                     $(".A_interesting").html(customerInfo.InterestingTopic!=null?(customerInfo.InterestingTopic.replace("SkinCare", "美容护肤").replace("MakeUp", "彩妆技巧").replace("DressUp", "服饰搭配").replace("FamilyTies", "家庭关系")):"");
                     $(".A_joinEvent").html(customerInfo.IsJoinEvent!=null?(customerInfo.IsJoinEvent == 0 ? "否" : "是"):"");
                     $(".A_customerType").html(customerInfo.CustomerType!=null?(customerInfo.CustomerType == 0 ? "老顾客" : (customerInfo.CustomerType == 1 ? "新顾客" : "在校学生")):"");
                     $(".A_Profession").html(customerInfo.Career);
                     $(".A_usedset").html(customerInfo.UsedProduct!=null?(customerInfo.UsedProduct==1? "是" : "否"):"");
                     $(".A_customerResponse").html(customerInfo.CustomerResponse!=null?(customerInfo.CustomerResponse == 0 ? "对产品有兴趣" : (customerInfo.CustomerResponse == 1 ? "对公司有兴趣" : (customerInfo.CustomerResponse==2 ? "一般" : "没兴趣"))):"");
                     $(".A_bestContactDate").html(customerInfo.BestContactDate!=null?(customerInfo.BestContactDate == 0 ? "工作日" : "双休日"):"");
                     $(".A_adviceContactDate").html(customerInfo.AdviceContactDate!=null?(customerInfo.AdviceContactDate == 0 ? "白天" : "晚上"):"");
                     $(".A_Address").html((customerInfo.Province?customerInfo.Province:"") + " " + (customerInfo.City?customerInfo.City.replace("县",""):"") + " " + (customerInfo.County?customerInfo.County:""));
                     //$(".A_SkinSuggest").html(customerInfo.CustomerResponse ? (customerInfo.CustomerResponse==0?"":"") : "");
                     $(".A_Dir").html(customerInfo.LastName + customerInfo.FirstName + " / " + customerInfo.DirectSellerId);
                    
                }),
                $(".searchBtn").on('click', function () {
                    module.getCustomers(1)
                })
                module.getCustomers(1)
            },
        }

        $(function () {
          
            module.ready();
        });
    })
