define(['laypage', 'plugins/layer', 'json2', 'commonAjax'],
    function (laypage,layer, json2, Ajax) {
        var module = {
            ready: function () {
                $('#SaveEventBtn').on('click', function () {
                    var EventObj = {
                        EventKey: $("#EventKey").val(),
                        EventTitle: $("#activityName").val(),
                        EventLocation: $("#activityCity").val(),
                        ApplyTicketStartDate: $("#activityStartDate").val(),
                        ApplyTicketEndDate: $("#activityEndDate").val(),
                        InvitationStartDate: $("#InvitationStartDate").val(),
                        InvitationEndDate: $("#InvitationEndDate").val(),
                        EventSessions: []
                    };
                    var pattern = /^[^`·!%^'"]*$/;
                    if (!pattern.test(EventObj.EventTitle)) {
                        //Alert('.card-form', "请填写正确的姓名！", 2000);
                        layer.alert("活动名称不允许输入特殊字符:`·!%^'\" ", { title: "警告：" })
                        return;
                    }

                    if (!pattern.test(EventObj.EventLocation)) {
                        //Alert('.card-form', "请填写正确的姓名！", 2000);
                        layer.alert("活动地址不允许输入特殊字符:`·!%^'\" ", { title: "警告：" })
                        return;
                    }


                    $(".activity-times__setting .activity-times_block").each(function (index, element) {
                        var Session = {
                            TimesNo: $(this).find(".TimesNo").val(),
                            DateStart: $(this).find(".dateStart").val(),
                            DateEnd: $(this).find(".dateEnd").val(),
                            VIPTotal: $(this).find(".vipTotal").val(),
                            NormalTotal: $(this).find(".normalTotal").val()
                        }
                        if (Session.DateStart.length > 0 || Session.DateEnd.length > 0 || Session.VIPTotal.length > 0 || Session.NormalTotal.length > 0)
                            EventObj.EventSessions.push(Session);
                      
                    });



                    var checkRes=module.EventSubmitCheck(EventObj);
                    if (!checkRes.result)
                    {
                        layer.alert(checkRes.msg, {title:"警告："})
                        return;
                    }
                    //return;
                    layer.myload.loading();
                    Ajax.easyAjax("/Home/SaveEvent", "POST", { EventObj: JSON.stringify(EventObj) },
                        function (jr, status) {
                            if (jr.result) {
                                module.getEvents(1);
                                $(".modal").modal("hide");
                                layer.closeAll();
                            } else {
                                layer.alert(jr.Msg, {title:'错误：'})
                                //alert(jr.Msg);
                            }
                        }, function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(2);
                        });
                }),
                $('#saveEventTitle').on('click',function(){
                    var EventObj = {
                        EventKey: $("#EventKey").val(),
                        EventTitle: $("#activityName").val(),
                        EventLocation: $("#activityCity").val()
                    };
                    var pattern = /^[^`·!%^'"]*$/;
                    if (!pattern.test(EventObj.EventTitle)) {
                        //Alert('.card-form', "请填写正确的姓名！", 2000);
                        layer.alert("活动名称不允许输入特殊字符:`·!%^'\" ", { title: "警告：" })
                        return;
                    }

                    if (!pattern.test(EventObj.EventLocation)) {
                        //Alert('.card-form', "请填写正确的姓名！", 2000);
                        layer.alert("活动地址不允许输入特殊字符:`·!%^'\" ", { title: "警告：" })
                        return;
                    }

                    if (EventObj.EventTitle.length < 1 || EventObj.EventTitle.length > 30) {
                        layer.alert("活动名称长度不能为空或者大于30个字符！", { title: "警告：" })
                        return;
                       
                    }

                    if (EventObj.EventLocation.length < 1 || EventObj.EventLocation.length > 40) {
                        layer.alert("活动地点长度不能为空或者大于40个字符！", { title: "警告：" })
                        return;
                        
                    }

                   
                    layer.myload.loading();
                    Ajax.easyAjax("/Home/SaveEventTitleAndLocation", "POST", { EventObj: JSON.stringify(EventObj) },
                        function (jr, status) {
                            if (jr.result) {
                                module.getEvents(1);
                                $(".modal").modal("hide");
                                layer.closeAll();
                            } else {
                                layer.alert(jr.Msg, { title: '错误：' })
                                //alert(jr.Msg);
                            }
                        }, function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(2);
                        });

                }),
                $('#SerchEventBtn').on('click', function () {
                    module.getEvents(1);
                }),
                //$("#activityName,#activityCity").on('blur', function () {
                //    if ($("#activityName").val().length > 0 && $("#activityCity").val().length > 0) {
                //        $("#SaveEventBtn").removeClass("disabled");
                //    } else {
                //        $("#SaveEventBtn").addClass("disabled");
                //    }

                //}),
                $("body").on("click", ".EventTitle", function () {
                    module.showEvent(this.id)
                }),
                $('body').on('click', '.icon-date', function (e) {
                    $(this).prev('.filter-form-date').focus();

                }),
                $('body').on('click','#EventTableList tbody tr td .EventTitle', function () {
                    var eventStartDate = new Date($("#EventStartDate_" + this.id).val());
                    if (eventStartDate < (new Date()) || $("#EventImport_" + this.id).val()=="true") {
                        $("#saveEventTitle").show();
                        $('#SaveEventBtn').hide()
                    } else {
                        $('#SaveEventBtn').show()
                        $("#saveEventTitle").hide();
                    }
                    
                }),
                $('.activity-btn__create button').on('click', function () {
                    $('#SaveEventBtn').show()
                    $('#saveEventTitle').hide()
                }),
                $('#activityCreate').on('show.bs.modal', function () {
                    var modalCt = this;
                    var existingInput = $(this).find('.filter-form-date');
                    existingInput.each(function (index) {
                        var inputCt = this;
                        var initVal = $(inputCt).val();
                        var prevVal = $(existingInput[index - 1]).val();
                        console.log(prevVal);
                        $(this).datetimepicker({
                            defaultSelect: false,
                            todayButton: false,
                            step: 15,
                            minTime: initVal ? false : prevVal ? (new Date(prevVal)) : '23:46',
                            defaultDate: initVal ? false : prevVal ? (new Date(prevVal)) : false,
                            defaultTime: initVal ? false : prevVal ? (new Date(prevVal)) : false,

                            onShow: function (ct) {
                                
                                var thisDate = $(inputCt).val();
                                var prevDate = jQuery(existingInput[index - 1]).val();
                                //针对邀约开始开始时间（非必填）
                                if (index == 3 && !prevDate) { prevDate = jQuery(existingInput[index - 2]).val(); }
                                $(this).data('prevdate', prevDate);
                                this.setOptions({
                                    minDate: prevDate ? (new Date(prevDate)) : false
                                });

                                if (prevDate && thisDate && thisDate.split(' ')[0] == prevDate.split(' ')[0]) {
                                   
                                    this.setOptions({
                                        minTime: prevDate ? prevDate.split(' ')[1] : false
                                    });
                                }

                            },
                            onClose: function (ct) {
                                var thisVal = $(inputCt).val();
                                var prevVal = jQuery(existingInput[index - 1]).val();
                                var thisDateVal, thisTimeVal, prevDateVal, prevTimeVal;

                                if (!thisVal) return;

                                if (!!prevVal) {
                                    thisDateVal = thisVal.split(' ')[0];
                                    thisTimeVal = thisVal.split(' ')[1];
                                    prevDateVal = prevVal.split(' ')[0];
                                    prevTimeVal = prevVal.split(' ')[1];

                                    if ((thisDateVal === prevDateVal) && (thisTimeVal < prevTimeVal)) {
                                        $(inputCt).val(thisVal = prevVal)
                                    }
                                    //月份和年份是可以下拉选择的 这也是一个可以比前边小的入口
                                    if (thisDateVal < prevDateVal) {
                                        $(inputCt).val(thisVal = prevVal)
                                    }

                                }

                                //如果当前未选择日期时间，则不去影响下一个输入框
                                if (jQuery(existingInput[index + 1]).data('xdsoft_datetimepicker')) {
                                    jQuery(existingInput[index + 1]).data('xdsoft_datetimepicker').setOptions({
                                        minDate: new Date(thisVal),
                                        minTime: new Date(thisVal),
                                        defaultDate: jQuery(existingInput[index + 1]).val() ? false : (new Date(thisVal)),
                                        defaultTime: jQuery(existingInput[index + 1]).val() ? false : (new Date(thisVal))
                                    });
                                }
                                //针对邀约开始开始时间（非必填）

                                if (index == 1 && jQuery(existingInput[index + 1]).val().length == 0 && jQuery(existingInput[index + 2]).data('xdsoft_datetimepicker')) {
                                    jQuery(existingInput[index + 2]).data('xdsoft_datetimepicker').setOptions({
                                        minDate: new Date(thisVal),
                                        minTime: new Date(thisVal),
                                        defaultDate: jQuery(existingInput[index + 1]).val() ? false : (new Date(thisVal)),
                                        defaultTime: jQuery(existingInput[index + 1]).val() ? false : (new Date(thisVal))
                                    });

                                }

                            },
                            onSelectDate: function (ct, $i) {

                                var prevDate = $(this).data('prevdate');
                                var dateObj = ct;
                                var month = dateObj.getMonth() + 1; //months from 1-12
                                var day = dateObj.getDate();
                                var year = dateObj.getFullYear();
                                newdate = year + "/" + (month < 10 ? '0' + month : month) + "/" + (day < 10 ? '0' + day : day);

                                if (newdate == (prevDate && prevDate.split(' ')[0])) {
                                    this.setOptions({
                                        minTime: prevDate.split(' ')[1]
                                    })
                                } else {
                                    this.setOptions({
                                        minTime: false
                                    })
                                }
                            }
                        });
                    })
                }).on('hide.bs.modal', function (e) {
                    var modalCt = this;
                    activeTimeDom.destory();
                    var existingInput = $(this).find('.filter-form-date');
                    existingInput.datetimepicker('destroy');
                    $(modalCt).find('form')[0].reset();
                    $(modalCt).find('form #EventKey').removeAttr("value");
                    
                    return true;
                })
            },
            EventSubmitCheck: function (obj) {
                var res = {result:true};
                 
                if (obj.EventTitle.length < 1 || obj.EventTitle.length > 30) {
                    res.msg = "活动名称长度不能为空或者大于30个字符！";
                    res.result = false;
                    return res;
                }

                if (obj.EventLocation.length < 1 || obj.EventLocation.length > 40) {
                    res.msg = "活动地点长度不能为空或者大于40个字符！";
                    res.result = false;
                    return res;
                }

                if (obj.ApplyTicketStartDate.length < 1 || obj.ApplyTicketEndDate.length < 1) {
                    res.msg = "抢票时间段必须有开始和结束时间！";
                    res.result = false;
                    return res;
                }



                if (obj.InvitationEndDate.length < 1) {
                    res.msg = "截止邀约时间必须进行填写！";
                    res.result = false;
                    return res;
                }

                

                var ApplyTicketStartDate=new Date(obj.ApplyTicketStartDate);
                var ApplyTicketEndDate = new Date(obj.ApplyTicketEndDate);
                
                var InvitationEndDate = new Date(obj.InvitationEndDate);
                if (ApplyTicketStartDate >= ApplyTicketEndDate) {
                    res.msg = "抢票活动时段 结束时间 必须大于 开始时间 ！";
                    res.result = false;
                    return res;
                }

                if (ApplyTicketEndDate >= InvitationEndDate) {
                    res.msg = "邀约截至时间 必须大于 抢票活动结束时间！";
                    res.result = false;
                    return res;
                }
                if (obj.EventSessions.length < 1) {
                    res.msg = "请至少设置一场活动时段！";
                    res.result = false;
                    return res;
                }

                if (obj.InvitationStartDate.length > 0) {
                    var InvitationStartDate = new Date(obj.InvitationStartDate);
                    if (InvitationStartDate <= ApplyTicketStartDate) {
                        res.msg = "邀约开始时间 必须大于 抢报开始时间！";
                        res.result = false;
                        return res;
                    }
                }

                $.each(obj.EventSessions, function (index,e) {
                    if (e.DateStart.length > 0 && e.DateEnd.length > 0 && e.VIPTotal.length > 0 && e.NormalTotal.length > 0) {
                        var DateStart = new Date(e.DateStart);
                        var DateEnd = new Date(e.DateEnd);
                        if (DateStart >= DateEnd) {
                            res.msg = "活动时段" + e.TimesNo + " 结束时间必须大于开始时间！";
                            res.result = false;
                            return false;
                        }

                        if (e.VIPTotal < 1 && e.NormalTotal < 1) {
                            res.msg = "活动时段" + e.TimesNo + " 贵宾券数与来宾券数不可同时为0！";
                            res.result = false;
                            return false;
                        }

                    } else if (e.DateStart.length > 0 || e.DateEnd.length > 0 || e.VIPTotal.length > 0 || e.NormalTotal.length > 0) {
                        res.msg = "请补足 活动时段" + e.TimesNo + " 的时间和分配的券数！";
                        res.result = false;
                        return false;
                    }
                });
                
                
                
                return res;

            },
            showEvent: function (id) {
                $("#EventKey").val(id);
                $("#activityName").val($("#tr_" + id + " .EventTitle").html());
                $("#activityCity").val($("#tr_" + id + " .EventLocation").html());
                $("#activityStartDate").val(module.TrimTime($("#tr_" + id + " .ApplyTicketStartDate").html()));
                $("#activityEndDate").val(module.TrimTime($("#tr_" + id + " .ApplyTicketEndDate").html()));
                $("#InvitationStartDate").val(module.TrimTime($("#tr_" + id + " .InvitationStartDate").html()));
                $("#InvitationEndDate").val(module.TrimTime($("#tr_" + id + " .InvitationEndDate").html()));
                var Sessions = JSON.parse($("#SessionVal_" + id).val());

                var activityTimesBlock = $('.activity-times_block')
                Sessions.map(function (data, index) {
                    if (index < 2) {
                        $(activityTimesBlock[index]).find('#activityDate' + (index + 1) + 'Start').val(module.TrimTime(data.SessionStartDate));
                        $(activityTimesBlock[index]).find('#activityDate' + (index + 1) + 'End').val(module.TrimTime(data.SessionEndDate));
                        $(activityTimesBlock[index]).find('#activityDate' + (index + 1) + 'Num1').val(data.VIPTicketQuantity);
                        $(activityTimesBlock[index]).find('#activityDate' + (index + 1) + 'Num2').val(data.NormalTicketQuantity);
                    } else {
                        activeTimeDom.add(data);
                        activeTimeDom.update();
                        activeTimeDom.datepickerinit()
                    }
                })

            },
            ActiveTime: function ActiveTime(config) {
                var deconfig = {
                    startNum: 3,
                    context: '.activity-times__setting',
                    blockdom: function (dataObj) {
                        var dataObj = dataObj || {};
                        return '<div class="activity-times_block">\n' +
                               '<div class="form-group">\n' +
                               '<label for="activityDate' + this.config.startNum + 'Start" class="activity-times__num" >活动时段' + this.config.startNum + '：</label>\n' +
                               '<input type="hidden" class="TimesNo"  value=' + this.config.startNum + ' />' +
                               '<input type="text" readonly="readonly" class="form-control filter-form-date dateStart" id="activityDate' + this.config.startNum + 'Start" value="' + (dataObj.SessionStartDate ? module.TrimTime(dataObj.SessionStartDate) : "") + '" placeholder="" name="activityDate' + this.config.startNum + 'Start">\n' +
                               '<span class="icon-date"></span>\n' +
                               '</div>\n' +
                               '<div class="form-group">\n' +
                               '<label for="activityDate' + this.config.startNum + 'End">&nbsp;&nbsp;到&nbsp;&nbsp;</label>\n' +
                               '<input type="text" readonly="readonly" class="form-control filter-form-date dateEnd" id="activityDate' + this.config.startNum + 'End" value="' + (dataObj.SessionStartDate ? module.TrimTime(dataObj.SessionEndDate) : "") + '"  placeholder="" name="activityDate' + this.config.startNum + 'End">\n' +
                               '<span class="icon-date"></span>\n' +
                               '</div>\n' +
                               '<div class="form-group">\n' +
                               '<label for="activityDate' + this.config.startNum + 'Num1" class="activity-times__num">贵宾券数：</label>\n' +
                               '<input type="number" class="form-control vipTotal" id="activityDate' + this.config.startNum + 'Num1" oninput="layer.handleInput(event)" placeholder="" value="' + (dataObj.VIPTicketQuantity ? dataObj.VIPTicketQuantity : '') + '" min="0" name="activityDate' + this.config.startNum + 'Num1">\n' +
                               '</div>\n' +
                               '<div class="form-group">\n' +
                               '<label for="activityDate' + this.config.startNum + 'Num2">&nbsp;来宾券数： &nbsp;</label>\n' +
                               '<input type="number" class="form-control normalTotal" id="activityDate' + this.config.startNum + 'Num2" oninput="layer.handleInput(event)" placeholder="" value="' + (dataObj.NormalTicketQuantity ? dataObj.NormalTicketQuantity : '') + '" min="0" name="activityDate' + this.config.startNum + 'Num2">\n' +
                               '</div>\n' +
                               '<div class="activity-times__new">\n' +
                               '</div>\n' +
                               '</div>\n'
                    }
                };
                this.config = $.extend({}, deconfig, config);
                this.init();
            },
            showPageBar: function (currPage) {
                var eventPageCount = $("#EventPageCount").val();
                var countFrom = $("#PageCountFrom").val();
                var countTo = $("#PageCountTo").val();
                var eventTotalRow = $("#EventTotalRow").val();
                var eventPageCount = $("#EventPageCount").val();
                var pageTxt = "显示第 " + countFrom + " 至 " + countTo + " 条，共 " + eventTotalRow + " 条";
                $(".page-indicator").html(pageTxt);
                if (eventPageCount == 1) {
                    $(".pull-right").html("<div><span style=\"background-color:#f16c74;margin-right:200px; padding: 0 12px;border-radius: 2px;line-height: 26px;color: white;display: inline-block;font-size: 12px;\">1</span></div>");
                } else {
                    laypage.dir = Application + "/content/css/laypage.css "
                    laypage({
                        cont: $(".pull-right"), //容器。值支持id名、原生dom对象，jquery对象,
                        pages: eventPageCount, //总页数
                        curr: currPage,
                        skin: '#f16c74', //皮肤
                        first: 1, //将首页显示为数字1,。若不显示，设置false即可
                        last: eventPageCount, //将尾页显示为总页数。若不显示，设置false即可
                        prev: '<', //若不显示，设置false即可
                        next: '>', //若不显示，设置false即可
                        jump: function(obj, first){
                            if (!first)
                                module.getEvents(obj.curr);
                        }
                    });
                }
               
            },
            getEvents: function (pageNo) {
                var title = $("#activitySerchName").val();
                var startDate = $("#filterStartDate").val();
                var endDate = $("#filterEndDate").val();
                $("#EventTableList").html("<div style=\"text-align:center;padding-top:100px;\"><img src=\"/PinkBusEventManagement/Content/images/loading.gif\" style=\"margin:0 auto; \" /></div>");
                Ajax.easyAjax("/Home/GetEventList", "GET", { PageNo: pageNo, EventTitle: title, EventStartDate: startDate, EventEndDate: endDate },
                            function (d) {
                                $("#EventTableList").html(d);
                                module.showPageBar(pageNo);
                            }, function (XMLHttpRequest, textStatus, errorThrown) {
                                console.log(textStatus)
                            });
            },
            TrimTime: function (time) {
                if (time.length < 10) return "";
                var newTime = time.replace("T", " ").replace(/-/g, "/").replace(/\./g,"/");
                newTime = newTime.split(':')[0] + ":" + newTime.split(':')[1];
                return newTime;
            }
        }

        module.ActiveTime.prototype = {
            constructor: module.ActiveTime,
            add: function (dataObj) {
                var newblock = this.config.blockdom.apply(this, [dataObj]);
                $(this.config.context).append(newblock);
                this.config.startNum++;
            },
            del: function () {
                var lastdom = $(this.config.context).find('.activity-times_block:last-child');

                lastdom.find('.filter-form-date').datetimepicker('destroy');
                lastdom.remove();
                this.config.startNum--;
            },
            update: function () {
                var lastdom = $(this.config.context).find('.activity-times_block:last-child').find('.activity-times__new'),
                     newdom = '<span class="icon-add" id="ActiveTimeAdd"></span>\n' +
                               '<span class="icon-minus" id="ActiveTimeMinus"></span>\n',
                     ActiveTimeAdd = $('#ActiveTimeAdd'),
                     ActiveTimeMinus = $('#ActiveTimeMinus');


                if (this.config.startNum == 3) {
                    newdom = '<span class="icon-add" id="ActiveTimeAdd"></span>\n';
                    ActiveTimeAdd.off('click.add');
                    ActiveTimeAdd.remove();
                    lastdom.append(newdom);
                    return true;
                }

                ActiveTimeAdd.off('click.add');
                ActiveTimeAdd.remove();
                ActiveTimeMinus.off('click.minus');
                ActiveTimeMinus.remove();
                lastdom.append(newdom);
            },
            destory: function () {
                while (this.config.startNum > 3) {
                    this.del();
                    this.update();
                }
            },
            datepickerinit: function () {
                var context = this;
                var lastdom = $(this.config.context).find('.activity-times_block:last-child').find('.filter-form-date');
                lastdom.each(function () {

                    var $this = $(this),
                            thisid = $this.attr('id'),
                            previd,nextid;
                    if (thisid.match(/End/)) {
                        previd = '#activityDate' + (context.config.startNum - 1) + 'Start';
                        nextid = '#activityDate' + (context.config.startNum) + 'Start';
                    } else {
                        previd = '#activityDate' + (context.config.startNum - 2) + 'End';
                        nextid = '#activityDate' + (context.config.startNum) + 'End';
                    }
                    var prevVal = jQuery(previd).val();
                    $this.datetimepicker({
                        defaultSelect: false,
                        todayButton: false,
                        step: 15,
                        minTime: prevVal ? (new Date(prevVal)) : '23:46',
                        defaultDate: prevVal ? (new Date(prevVal)) : false,
                        defaultTime: prevVal ? (new Date(prevVal)) : false,
                        onShow: function (ct) {

                            var prevDate = $(previd).val();
                            $(this).data('prevdate', prevDate);
                            this.setOptions({
                                minDate: prevDate ? (new Date(prevDate)) : false
                            });
                        },
                        onClose: function (ct) {
                            var thisVal = $this.val();
                            var prevVal = jQuery(previd).val();
                            var thisDateVal, thisTimeVal, prevDateVal, prevTimeVal;

                            if (!thisVal) return;

                            //如果当前未选日期时间 或者 没有前一个日期时间，则不需要比较
                            if (!!prevVal) {
                                thisDateVal = thisVal.split(' ')[0];
                                thisTimeVal = thisVal.split(' ')[1];
                                prevDateVal = prevVal.split(' ')[0];
                                prevTimeVal = prevVal.split(' ')[1];

                               
                                if ((thisDateVal === prevDateVal) && (thisTimeVal < prevTimeVal)) {
                                    $this.val(thisVal = prevVal)
                                }
                                //月份和年份是可以下拉选择的 这也是一个可以比前边小的入口
                                if (thisDateVal < prevDateVal) {
                                    $this.val(thisVal = prevVal)
                                }

                            }

                            //如果当前未选择日期时间，则不去影响下一个输入框
                            if (jQuery(nextid).data('xdsoft_datetimepicker')) {

                                jQuery(nextid).data('xdsoft_datetimepicker').setOptions({
                                    minDate: new Date(thisVal),
                                    minTime: new Date(thisVal),
                                    defaultDate: jQuery(nextid).val() ? false : (new Date(thisVal)),
                                    defaultTime: jQuery(nextid).val() ? false : (new Date(thisVal))
                                });
                            }

                        },
                        onSelectDate: function (ct, $i) {

                            var prevDate = $(this).data('prevdate');
                            var dateObj = ct;
                            var month = dateObj.getMonth() + 1; //months from 1-12
                            var day = dateObj.getDate();
                            var year = dateObj.getFullYear();
                            newdate = year + "/" + (month < 10 ? '0' + month : month) + "/" + (day < 10 ? '0' + day : day);

                            if (newdate == (prevDate && prevDate.split(' ')[0])) {
                                this.setOptions({
                                    minTime: prevDate.split(' ')[1]
                                })
                            } else {
                                this.setOptions({
                                    minTime: false
                                })
                            }
                        }
                    })
                })

            },
            init: function () {
                var body = $('body');

                body.on('click.add', '#ActiveTimeAdd', function (e) {
                    this.add();
                    this.update();
                    this.datepickerinit()
                }.bind(this));

                body.on('click.minus', '#ActiveTimeMinus', function (e) {
                    this.del();
                    this.update();
                }.bind(this));

            }
        }

        return module;

    })
