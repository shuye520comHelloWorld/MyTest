require(['jquery', 'jquerydatetimepicker', 'bootstrap', 'json2', 'plugins/layer', 'laypage', 'commonAjax'],
    function ($, jquerydatetimepicker, bootstrap, json2, layer, laypage, Ajax) {
        var module = {
            getCustomers: function (pageNo) {

                $(".EventList").html("<div style=\"text-align:center;padding-top:100px;\"><img src=\"" + Application + "/Content/images/loading.gif\" style=\"margin:0 auto; \" /></div>");
                Ajax.easyAjax("/Home/EventList", "GET", { PageNo: pageNo, eventTitle: $("#activityName").val(), startDate: $("#filterStartDate").val(), endDate: $("#filterEndDate").val() },
                            function (d) {
                                $(".EventList").html(d);
                                module.showPageBar(pageNo);
                               // $("#TotalCount").html($("#TotalRow").val())
                            }, function (XMLHttpRequest, textStatus, errorThrown) {
                                console.log(textStatus)
                            });
            },
            showPageBar: function (currPage) {
                // var eventPageCount = $("#CustomerPageCount").val();
                 //var countFrom = $("#PageCountFrom").val();
                // var countTo = $("#PageCountTo").val();
                var eventTotalRow = $("#TotalRow").val();
                var eventPageCount = $("#PageCount").val();
                var pageTxt = "显示第 " + (eventTotalRow==0?0:((currPage - 1) * 10 + 1)) + " 至 " + (currPage * 10 > eventTotalRow ? eventTotalRow : currPage * 10) + " 条，共 " + eventTotalRow + " 条";
                $(".pageTxt").html(pageTxt);

                //$("#CustomerCount").html(eventTotalRow);
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
            showSessions:function(sessions){
                var trHtml = "";
                for (var s = 0; s < sessions.length; s++) {
                    trHtml += "<tr><td>" + layer.DateFormat(parseInt(sessions[s].SessionStartDate.replace(/\D/igm, ""))) + " ~ " + layer.DateFormat(parseInt(sessions[s].SessionEndDate.replace(/\D/igm, ""))) + "</td><td>" + sessions[s].NormalTicketUsedCount + "</td><td>" + sessions[s].VipTicketUsedCount + "</td></tr>";
                }
                if (sessions.length < 1)
                    trHtml = "<tr><td  colspan='3' >没有配置抢票时段</td></tr>";
                $("#SessionsTR").html("");
                $("#SessionsTR").append(trHtml);
            },
           
            ready: function () {
                //$(".activity-btn__import").on('click', function () {
                //    var customerKey = $(this).parents("tr").attr("id").replace("tr_", "");
                //    var customerInfo = JSON.parse($(this).parents("tr").find(".CustomerInfo").val())
                //    //console.log(customerInfo);

                //    $(".A_name").html(customerInfo.CustomerName);
                //    $(".A_contact").html(customerInfo.ContactInfo + "(" + customerInfo.ContactType.replace("PhoneNumber", "手机").replace("Wechat", "微信").replace("Other", "其他") + ")");
                //    $(".A_age").html(customerInfo.AgeRange == 0 ? "25岁以下" : (customerInfo.AgeRange == 1) ? "25-35岁" : (customerInfo.AgeRange == 2 ? "35-45岁" : "45岁以上"));
                //    $(".A_hearMK").html(customerInfo.IsHearMaryKay ? "是" : "否");
                //    $(".A_interesting").html(customerInfo.InterestingTopic.replace("SkinCare", "美容护肤").replace("MakeUp", "彩妆技巧").replace("DressUp", "服饰搭配").replace("FamilyTies", "家庭关系"));
                //    $(".A_joinEvent").html(customerInfo.IsJoinEvent == 0 ? "否" : "是");
                //    $(".A_customerType").html(customerInfo.CustomerType == 0 ? "老顾客" : (customerInfo.CustomerTyp == 1 ? "新顾客" : "在校学生"));

                //    $(".A_usedset").html(customerInfo.UsedProduct ? "否" : "是");
                //    $(".A_customerResponse").html(customerInfo.CustomerResponse == 0 ? "对产品有兴趣" : (customerInfo.CustomerResponse == 1 ? "对公司有兴趣" : (customerInfo.CustomerResponse ? "一般" : "没兴趣")));
                //    $(".A_bestContactDate").html(customerInfo.BestContactDate == 0 ? "工作日" : "双休日");
                //    $(".A_adviceContactDate").html(customerInfo.AdviceContactDate == 0 ? "白天" : "晚上");
                //    $(".A_Address").html(customerInfo.Province ? customerInfo.Province : "" + " " + customerInfo.City ? customerInfo.City.replace("县", "") : "" + " " + customerInfo.County ? customerInfo.County : "");
                //    $(".A_Dir").html(customerInfo.LastName + customerInfo.FirstName + " / " + customerInfo.DirectSellerId);

                //}),
                $(".searchBtn").on('click', function () {
                    module.getCustomers(1)
                })

                $("body").on('click', '#TicketInfo', function () {
                    $("#SessionsTR").html("<tr><td  colspan='3' >加载中...</td></tr>");
                    Ajax.easyAjax("/Home/EventSessionList", "GET", { EventKey: $(this).attr("EventKey") },
                           function (data) {
                               module.showSessions(data.sessions);

                           }, function (XMLHttpRequest, textStatus, errorThrown) {
                               console.log(textStatus)
                           });

                })
                module.getCustomers(1)
                $("body").on("click", ".icon-date", function (a) {
                    $(this).prev(".filter-form-date").focus()
                }),
                $("body").on('click', '#SeeParticulars', function () {
                    Ajax.easyAjax("/Home/EventTracking", "GET", { EventKey: $(this).attr("EventKey") },
                           function (data) {
                             
                               $(".InvitByWechat").html(data.InvitByWechat || 0);
                               $(".InvitBySMS").html(data.InvitBySMS || 0);
                               $(".SendTicketByWechat").html(data.SendTicketByWechat || 0);
                               $(".SendTicketBySMS").html(data.SendTicketBySMS || 0);
                               $(".GetTicketByBC").html(data.GetTicketByBCInput || 0);
                               $(".GetTicketByWechat").html(data.GetTicketByWechat || 0);
                               $(".GetTicketBySMS").html(data.GetTicketBySMS || 0);
                               $(".CheckinByQRcode").html(data.CheckinByQRcode || 0);
                               $(".CheckinByToken").html(data.CheckinByToken || 0);
                               $(".CheckinByInput").html(data.CheckinByLiveInput||0);
                           }, function (XMLHttpRequest, textStatus, errorThrown) {
                               console.log(textStatus)
                           });
                   
                })
            },
        }

        /*给所有的日历图标绑定点击事件*/
        $(function () {
            /*首页查询上的日历*/
            $('#filterStartDate').datetimepicker({
                defaultSelect: false,
                todayButton: false,
                step: 15,
                minTime: '23:46',
                onSelectDate: function (e) {
                    this.setOptions({
                        minTime: '00:00'
                    })
                }
            });
            $('#filterEndDate').datetimepicker({
                defaultSelect: false,
                todayButton: false,
                step: 15,
                minTime: '23:46',
                onShow: function (ct) {
                    var prevDate = jQuery('#filterStartDate').val();
                    $(this).data('prevdate', prevDate);
                    this.setOptions({
                        minDate: prevDate ? prevDate.split(' ')[0] : false,
                        startDate: prevDate ? prevDate.split(' ')[0] : false
                    });
                },
                onSelectDate: function (ct, $i) {
                    var prevDate = $(this).data('prevdate');
                    var dateObj = ct;
                    var month = dateObj.getUTCMonth() + 1; //months from 1-12
                    var day = dateObj.getUTCDate();
                    var year = dateObj.getUTCFullYear();
                    newdate = year + "/" + month + "/" + day;
                    if (newdate == (prevDate && prevDate.split(' ')[0])) {
                        this.setOptions({
                            minTime: prevDate.split(' ')[1]
                        })
                    } else {
                        this.setOptions({
                            minTime: '00:00'
                        })
                    }
                }
            });

            module.ready();
         

            
            
        })
        
    


    })
