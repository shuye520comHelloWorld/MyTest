var uploadView = {
    setBindEvent: function () {

        $(".btnDelRow").live("click", function () {
            if ($("#uploadTable tbody tr").length < 2) {
                $("#uploadTable tbody tr:first .td5 a:first").attr("disable", "disable");
            }
            $(this).parents("tr").remove();
            $("#tableErrMsg span").html("");
            uploadView.resetRowNum();
        });
        $("#ActualTimeBtn").click(function () {
            if ($("#ActualTimeBtn").hasClass("sending")) return;
            
            if (uploadView.checkActualTime()) {
                uploadView.disableBth(true);
                var year = $("#actualYear").val().split('-')[0];
                var month = $("#actualYear").val().split('-')[1];
                var day = $("#sel_day").val();
                
                var startHour = $("#sel_start_Hour").val();
                var startMin = $("#sel_start_Min").val();
                var endHour = $("#sel_end_Hour").val();
                var endMin = $("#sel_end_Min").val();
                var startTime = $("#actualYear").val() + day + " " + startHour + ":" + startMin;
                var endTime = $("#actualYear").val() + day + " " + endHour + ":" + endMin;
                var viewTime = year + "年" + month + "月" + day + "日 &nbsp;&nbsp;&nbsp;" + startHour + ":" + startMin + " ~ " + endHour + ":" + endMin;
                var msg = "请确认活动举办时间，确认提交后将无法再修改或删除！<br />" + viewTime;
                alertWin.confirmWin(msg, "确认提交", "返回修改", function () {
                    $.post(applicationPath + "/PaperUpload/addNewTime", { PartyKey: $("#hid_partyKey").val(), StartTime: startTime, EndTime: endTime }, function (j) {
                        uploadView.disableBth(false);
                        if (j.result) {
                            $("#hid_IsSaveActualTime").val(true);
                            var htmltext = "活动实际举办时间（选填）：" + viewTime;
                            $("#Selection").html(htmltext);
                            $("#timeErr").remove();
                        } else {

                        }
                    })
                });


            }
        })
        $("#Selection select").click(function () {
            $("#timeErr").html("");
            $("#timeErr").removeClass("timeErrEnd timeErrStart timeErrMonth");
        })
        $("#uploadTable input").live("focus", function () {
            $("#tableErrMsg span").removeClass("nameNull phoneNull recNull");
            $("#tableErrMsg span").html("");
        });
        $("#addNewRow,.addOneBtn").live("click", function () {
            if ($("#ActualTimeBtn").hasClass("sending")) return;

            if ($(this).attr("id") == "addNewRow" &&
                ($("#uploadTable tr:last input").length < 1 || $("#uploadTable tr:last .td1").html().indexOf("NAN") >-1)) {
                
                $("#uploadTable").append($("#uploadTable tbody tr:first").clone());
                uploadView.resetRowNum();
                $("#uploadTable tr:last td input").val("");
                $("#uploadTable tr:last").removeClass("hidden");
                return;
            }
            
           
            if (uploadView.checkUserNull()) {
                uploadView.disableBth(true);
                var partykey = $("#hid_partyKey").val();
                var nameTd = $("#uploadTable tr:last .td2 input").val();
                var phoneTd = $("#uploadTable tr:last .td3 input").val();
                var InviterTd = $("#uploadTable tr:last .td4 input").val();
                $.post(applicationPath + "/PaperUpload/addOneUser", { PartyKey: partykey, Name: nameTd, Phone: phoneTd, InviterId: InviterTd }, function (j) {
                    uploadView.disableBth(false);
                    if (j.result && j.status == "confirm") {
                        var msg1 = "该手机号码对应的来宾已进行线上报名，须以如下线上报名信息为准:";
                        var msg2 = "是否确认提交上述来宾信息？";
                        alertWin.confirmTableWin(msg1, msg2, j.name, phoneTd, j.sellerid, "确认", "返回", 280, function () {
                            $.post(applicationPath + "/PaperUpload/updateCustomer", { PartyKey: partykey, Phone: phoneTd }, function (jr) {
                                if (jr.result) {
                                    var nameTd = $("#uploadTable tr:last .td2 input").val(jr.name);
                                    var InviterTd = $("#uploadTable tr:last .td4 input").val(jr.sellerid);
                                    uploadView.addNewRow();
                                } else {
                                    alertWin.alert(jr.msg, "返回");
                                }
                            });


                        });
                    } else if (j.result && j.status == 'myVIP') {
                        var msg1 = "该手机号码对应的来宾是15级或20级的美容顾问或VIP来宾，须以系统显示的邀约人信息为准，如下：";
                        var msg2 = "是否确认提交上述来宾信息？";
                        alertWin.confirmTableWin(msg1, msg2, j.name, j.phone, j.inviter, "确认", "返回", 270, function () {

                            $.post(applicationPath + "/PaperUpload/addCustomer", { PartyKey: partykey, Name: j.name, Phone: phoneTd, InviterId: j.inviter }, function (jr) {
                                if (jr.result) {
                                    $("#uploadTable tr:last .td2 input").val(j.name);
                                    $("#uploadTable tr:last .td4 input").val(j.inviter);
                                    uploadView.addNewRow();
                                } else {
                                    alertWin.alert(jr.msg, "返回");
                                }
                            });

                        });
                    }
                    else if (j.result && (j.status == 'recheck')) {
                        var msg1 = "请确认以下来宾信息，确认提交后将无法再修改或删除！";
                        var msg2 = "";
                        alertWin.confirmTableWin(msg1, msg2, j.name, j.phone, j.inviter, "确认", "返回", 230, function () {
                            $.post(applicationPath + "/PaperUpload/addCustomer", { PartyKey: partykey, Name: nameTd, Phone: phoneTd, InviterId: InviterTd }, function (jr) {
                                if (jr.result) {
                                    uploadView.addNewRow();
                                } else {
                                    alertWin.alert(jr.msg, "返回");
                                }
                            });
                        });
                    } else if (j.result && j.status == "invited" ) {
                        $.post(applicationPath + "/PaperUpload/updateCustomer", { PartyKey: partykey, Phone: phoneTd }, function (jr) {
                            if (jr.result) {
                                uploadView.addNewRow();
                            } else {
                                alertWin.alert(jr.msg, "返回");
                            }

                        });
                    } else if (j.result &&  j.status == 'myVIPSame') {
                        $.post(applicationPath + "/PaperUpload/addCustomer", { PartyKey: partykey, Name: nameTd, Phone: phoneTd, InviterId: InviterTd }, function (jr) {
                            if (jr.result) {
                                uploadView.addNewRow();
                            } else {
                                alertWin.alert(jr.msg, "返回");
                            }
                        });
                    } else {
                        alertWin.alert(j.msg, "返回");
                    }
                });
            }
        });
        $("#inputComplete,#AHome").click(function () {
            if ($("#hid_IsSaveActualTime").val() == "false") {
                alertWin.confirmWin("您尚未提交活动实际的举办时间，点击继续后未提交的信息不会自动保存，是否继续？", "继续", "返回", function () {
                    location.href = applicationPath + "/PaperUpload/index";
                });
            } else if (!uploadView.checkUserSubmit()) {
                alertWin.confirmWin("您尚有正在填写的来宾信息没有提交，点击继续后未提交的信息不会自动保存，是否继续？", "继续", "返回", function () {
                    location.href = applicationPath + "/PaperUpload/index";
                });

            }
            else {
                if ($(this).attr("id") == 'inputComplete') {
                    alertWin.successWin("您填写的来宾信息已全部成功提交！","确定", function () {
                        location.href = applicationPath + "/PaperUpload/index";
                    });
                    return;
                }
                location.href = applicationPath + "/PaperUpload/index";
            }
        });

        $("#A_back,#A_ChangeParty").click(function (e) {
            var dirid = $(this).attr("value");

            if ($("#hid_IsSaveActualTime").val() == "false") {
                alertWin.confirmWin("您尚未提交活动实际的举办时间，点击继续后未提交的信息不会自动保存，是否继续？", "继续", "返回", function () {
                    location.href = applicationPath + "/PaperUpload/choseparty?dirid=" + dirid;
                });
                
            } else if (!uploadView.checkUserSubmit()) {
                alertWin.confirmWin("您尚有正在填写的来宾信息没有提交，点击继续后未提交的信息不会自动保存，是否继续？", "继续", "返回", function () {
                    location.href = applicationPath + "/PaperUpload/choseparty?dirid=" + dirid;
                });

            }
            else {
                location.href = applicationPath + "/PaperUpload/choseparty?dirid=" + dirid;
            }
        });
    },
    resetRowNum: function () {
        var tab = $("#uploadTable tbody tr");
        for (var i = 1; i <= $("#uploadTable tbody tr").length; i++) {
            tab.eq(i).find("td:first").html(i);
        }
        uploadView.inputedCount();
    },
    inputedCount: function () {
        var count = $("#uploadTable .successBtn").length;
        $("#InputCount").html(count);
    },
    addNewRow: function () {
       
            var nameTd = $("#uploadTable tr:last .td2 input").val();
            var phoneTd = $("#uploadTable tr:last .td3 input").val();
            var InviterTd = $("#uploadTable tr:last .td4 input").val();
            if (nameTd.length < 1 || phoneTd.length < 1)
                return;

            $("#uploadTable tr:last .td2").html(nameTd);
            $("#uploadTable tr:last .td3").html(phoneTd);
            $("#uploadTable tr:last .td4").html(InviterTd);
            $("#uploadTable tr:last .td5").html("已成功提交");
            $("#uploadTable tr:last .td5").addClass("successBtn");
        
        $("#uploadTable").append($("#uploadTable tbody tr:first").clone());
        uploadView.resetRowNum();
        $("#uploadTable tr:last td input").val("");
        $("#uploadTable tr:last").removeClass("hidden");


    },
    checkInput: function () {
        if (viewModel.name().length < 1) {
            alert("请输入主办人姓名！");
            $("#CName").focus();
            return false;
        }

        if (viewModel.cid().length < 1) {
            alert("请输入主办人的12位编号！");
            $("#CID").focus();
            return false;
        }

        if (!(/^\d{12}$/.test(viewModel.cid()))) {
            alert("请输入主办人的12位编号！")
            $("#CID").focus();
            return false;
        }
        return true;
    },
    checkActualTime: function () {
        var year = $("#actualYear").val().split('-')[0];
        var Month = $("#actualYear").val().split('-')[1];
        var day = $("#sel_day").val();
        var startHour = $("#sel_start_Hour").val();
        var startMin = $("#sel_start_Min").val();
        var endHour = $("#sel_end_Hour").val();
        var endMin = $("#sel_end_Min").val();
        if (day.length < 1) {
            $("#timeErr").addClass("timeErrMonth");
            $("#timeErr").html("X 请输入日期！");
            return false;
        }

        if (startHour.length < 1 || startMin.length < 1) {
            $("#timeErr").addClass("timeErrStart");
            $("#timeErr").html("X 请输入开始时间！");
            return false;
        }

        if (endHour.length < 1 || endMin.length < 1) {
            $("#timeErr").addClass("timeErrEnd");
            $("#timeErr").html("X 请输入结束时间！");
            return false;
        }

        var startDate = new Date(year, Month, day, startHour, startMin, "00");
        var endDate = new Date(year, Month, day, endHour, endMin, "00");
        if (startDate.getTime() >= endDate.getTime()) {
            $("#timeErr").addClass("timeErrStart");
            $("#timeErr").html("X 结束时间不得早于或等于开始时间！");
            return false;
        }

        return true;
    },
    checkUserNull: function () {
        var nameTd = $("#uploadTable tr:last .td2 input").val();
        var phoneTd = $("#uploadTable tr:last .td3 input").val();
        var InviterTd = $("#uploadTable tr:last .td4 input").val();
        if ($.trim(nameTd).length < 1) {
            $("#tableErrMsg span").html(" X 请输入来宾姓名！");
            $("#tableErrMsg span").addClass("nameNull");
            return false;
        }

        if ($.trim(phoneTd).length < 1) {
            $("#tableErrMsg span").html(" X 请输入来宾手机号码！");
            $("#tableErrMsg span").addClass("phoneNull");
            return false;
        }

        if (!(/^[a-zA-Z\u4e00-\u9fa5]{1,10}$/.test($.trim(nameTd)))) {
            $("#tableErrMsg span").html(" X 请输入正确的来宾姓名！");
            $("#tableErrMsg span").addClass("nameNull");
            return false;
        }


        if (!(/^1[3|4|5|7|8][0-9]\d{8}$/.test($.trim(phoneTd)))) {

            $("#tableErrMsg span").html(" X 请输入正确的来宾手机号码！");
            $("#tableErrMsg span").addClass("phoneNull");
            return false;
        }

        if ($.trim(InviterTd).length > 0) {
            if (!(/^\d{12}$/.test($.trim(InviterTd)))) {
                $("#tableErrMsg span").html(" X 请输入正确的12位邀约人编号！");
                $("#tableErrMsg span").addClass("recNull");
                return false;
            }
        }

        return true;

    },
    checkActualTimeSubmit: function () {
        if ($("#hid_IsSaveActualTime").val() == "false")
            return false;
        else
            return true;
    },
    checkUserSubmit: function () {
        var nameTd = $("#uploadTable tr:last .td2 input").val();
        var phoneTd = $("#uploadTable tr:last .td3 input").val();
        var InviterTd = $("#uploadTable tr:last .td4 input").val();

        if ($("#uploadTable tr:last .td1").html().indexOf("NAN") < 0) {
            if ($.trim(nameTd).length > 0 || $.trim(phoneTd).length > 0 || $.trim(InviterTd).length > 0) {
                return false;
            }
        }
        return true;
    },
    disableBth: function (bool) {
        if (bool) {
            //$(".btnDelRow").addClass("sending");
            $(".addOneBtn").addClass("sending");
            $("#ActualTimeBtn").addClass("sending")
        } else {
            //$(".btnDelRow").removeClass("sending");
            $(".addOneBtn").removeClass("sending");
            $("#ActualTimeBtn").removeClass("sending")
        }
    }

}

$(function () {
    uploadView.setBindEvent();
    uploadView.resetRowNum();
    
})