require(['jquery', 'bootstrap', 'jquerydatetimepicker', 'json2', 'plugins/layer','laypage', 'commonAjax'],
    function ($, bootstrap, timepicker,json2, layer,laypage, Ajax) {
        var module = {
            getCustomers: function (pageNo) {
              
                $(".CheckinList").html("<div style=\"text-align:center;padding-top:100px;\"><img src=\"" + Application + "/Content/images/loading.gif\" style=\"margin:0 auto; \" /></div>");
                Ajax.easyAjax("/Home/CheckinList", "GET", {
                    eventKey: $("#EventKey").val(),
                    PageNo: pageNo,
                    name: $("#checkin_Name").val(),
                    ticketType: $("#checkin_ticketType").val(),
                    ticketStatus: $("#checkin_ticketStatus").val(),
                    checkinType: $("#checkin_checkinType").val()
                },
                            function (d) {
                                $(".CheckinList").html(d);
                                module.showCheckinSummary();
                                module.showPageBar(pageNo);
                            }, function (XMLHttpRequest, textStatus, errorThrown) {
                                console.log(textStatus)
                            });
            },
            showCheckinSummary: function () {

                var strText = "贵宾：" + $("#VIPCheckinCount").val() + "/" + $("#VIPCount").val() + "&nbsp;&nbsp;&nbsp;&nbsp;来宾：" + $("#NormalCheckinCount").val() + "/" + $("#NormalCount").val() + "&nbsp;&nbsp;&nbsp;&nbsp;志愿者：" + $("#VolunteerCheckinCount").val() + "/" + $("#VolunteerCount").val() + "";
                $(".checkinSummary").html(strText);
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
                
                $(".searchBtn").on('click', function () {
                    module.getCustomers(1)
                })
                module.getCustomers(1)


                $("body").on('click', '#CheckinMan', function () {
                    var CustomerKey = $(this).attr("CustomerKey");
                    var checkStatus = $(this).parents("tr").find(".checkStatus").html();
                    if (CustomerKey != null) {
                        $("#volunteer").addClass("hidden");
                        $("#customer").removeClass("hidden");
                        Ajax.easyAjax("/Home/CheckGuestInforma", "GET", { CustomerKey: CustomerKey },
                               function (data) {
                                  
                                   $(".checkStatuspop").html(checkStatus);
                                   $(".CustomerName").html(data.CustomerName);
                                   $(".CustomerPhone").html(data.CustomerPhone);
                                   $(".IsImportMyCustomer").html(data.CustomerType == 0 ? "老顾客" : (data.CustomerType == 1 ? "新顾客" : "VIP"));
                                   $(".age").html(data.AgeRange == 0 ? "25岁以下" : (data.AgeRange == 1) ? "25-35岁" : (data.AgeRange == 2 ? "35-45岁" : "45岁以上"));
                                   $(".Career").html(data.Career!=null?(data.Career == 0 ? "公司职员" : (data.Career == 1 ? "私营业主" : (data.Career == 2 ? "家庭主妇" : "自由职业"))):"");
                                   $(".BeautyClass").html(data.BeautyClass!=null?(data.BeautyClass?"是":"否"):"");
                                   $(".UsedProduct").html(data.UsedProduct!=null ? (data.UsedProduct ?"是" : "否"):"");
                                   $(".InterestingTopic").html(data.InterestingTopic!=null ? (data.InterestingTopic.replace("SkinCare", "美容护肤").replace("MakeUp", "彩妆技巧").replace("DressUp", "服饰搭配").replace("FamilyTies", "家庭关系")) : "");
                                   $(".UsedSet").html(data.UsedSet!=null?(data.UsedSet.replace("TimeWise", "幻时").replace("WhiteningSystemFoaming", "美白").replace("Cleanser", "经典").replace("CalmingInfluence", "舒颜").replace("Other", "其他")):"");                                   $(".InterestInCompany").html(data.InterestInCompany!=null?(data.InterestInCompany.replace("BeautyConfidence", "美丽自信").replace("CompanyCulture", "公司文化").replace("BusinessOpportunity", "事业机会").replace("Other", "其他")):"");
                               }, function (XMLHttpRequest, textStatus, errorThrown) {
                                   console.log(textStatus)
                               });
                    }
                    else {
                        $("#customer").addClass("hidden");
                        $("#volunteer").removeClass("hidden");
                        Ajax.easyAjax("/Home/CheckVolunteerInfo", "GET", { MappingKey: $(this).attr("MappingKey") },
                              function (data) {
                                  $(".VolunteerName").html(data.LastName + data.FirstName);
                                  $(".PhoneNumber").html(data.PhoneNumber);
                                  $(".Level").html(data.Level);
                                  $(".ResidenceID").html(data.ResidenceID);
                                  $(".Address").html((data.Province ? data.Province : "") + " " + (data.City ? data.City.replace("县", "") : "") + " " + (data.CountyName ? data.CountyName : ""));
                                  
                              }, function (XMLHttpRequest, textStatus, errorThrown) {
                                  console.log(textStatus)
                              });
                    }
                   
                    })
                   
                
            },
            
        }

        $(function () {
          
            module.ready();
        });
    })
