require(['jquery', 'bootstrap', 'jquerydatetimepicker', 'json2', 'plugins/layer','laypage', 'commonAjax'],
    function ($, bootstrap, timepicker,json2, layer,laypage, Ajax) {
        var module = {
            getCustomers: function (pageNo) {
              
                $(".InviterList").html("<div style=\"text-align:center;padding-top:100px;\"><img src=\"" + Application + "/Content/images/loading.gif\" style=\"margin:0 auto; \" /></div>");
                Ajax.easyAjax("/Home/InviterList", "GET", {
                    eventKey: $("#EventKey").val(),
                    PageNo: pageNo,
                    name: $("#BCName").val(),
                    SellerId: $("#SellerId").val()
                },
                            function (d) {
                                $(".InviterList").html(d);
                                module.showPageBar(pageNo);
                                $("#InviterCountSpan").html($("#TotalRow").val());
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
            showSessions: function (InviterCustomer) {
                var strHtml = "";
                for (var i = 0; i < InviterCustomer.length; i++)
                {
                    strHtml += "<tr><td>" + (i + 1) + "</td><td>" +( InviterCustomer[i].TicketType==1?"来宾":"贵宾") + "</td><td>" + InviterCustomer[i].CustomerName + "</td><td>" + InviterCustomer[i].CustomerPhone + "</td></tr>";
                }
               
                if (InviterCustomer.length < 1)
                    strHtml = "<tr><td  colspan='4' >暂无顾客</td></tr>";
                $("#seeCustomerlist").html("");
                $("#seeCustomerlist").append(strHtml);
            },

            ready: function () {
                
                $(".searchBtn").on('click', function () {
                    module.getCustomers(1)
                })
                module.getCustomers(1)

                $("body").on("click", ".SeeInviterCustomer", function () {
                    $("#seeCustomerlist").html("<tr><td  colspan='4' >加载中...</td></tr>");
                    var eventKey= $("#EventKey").val();
                    Ajax.easyAjax("/Home/InviterCustomerList", "GET", { DirectSellerId: $(this).attr("DirectSellerId"), eventKey: eventKey },
                           function (data) {
                               module.showSessions(data.InviterCustomer);

                           }, function (XMLHttpRequest, textStatus, errorThrown) {
                               console.log(textStatus)
                           });
                })
            },
        }

        $(function () {
          
            module.ready();
        });
    })
