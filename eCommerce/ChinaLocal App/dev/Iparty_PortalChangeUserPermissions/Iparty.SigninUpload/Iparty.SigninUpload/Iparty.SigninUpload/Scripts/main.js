var alertWin = {
    errAlert: function (errMsg) {
        var win = new LCWindow();
        win.alert({
            msg:errMsg,
            text4AlertBtn: '确定'
        })
    },
    alert: function (errMsg,text) {
        var win = new LCWindow();
        win.alert({
            msg: "<div style='text-align:left;'>" + errMsg + "</div>",
            text4AlertBtn: text
        })
    },
    confirmWin: function (msg,btn1,btn2,Yfunc) {
        var win = new LCWindow();
        win.confirm({
            msg: msg,
            text4ConfirmBtn: btn1,
            text4CancelBtn: btn2
        }).on("confirm", function () {
            Yfunc();
        }).on("cancel", function () {
            //Nfunc();
        });
    },
    confirmTableWin: function (msg1,msg2,name,phone,reference, btn1, btn2, height, Yfunc) {
        var win = new LCWindow();
        win.confirm({
            msg: "<div style='text-align:left;'>"+msg1+"</div>" +
            "<table >" +
                "<tbody>" +
                    "<tr>" +
                        "<td>姓名</td>" +
                        "<td>手机号码</td>" +
                        "<td>邀约人(选填)</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>"+name+"</td>" +
                        "<td>"+phone+"</td>" +
                        "<td>" + reference + "</td>" +
                    "</tr>" +
                "</tbody>" +
            "</table>"+
            "<div style='text-align:left;'>" + msg2 + "</div>",
            height: height,
            text4ConfirmBtn: btn1,
            text4CancelBtn: btn2
        }).on("confirm", function () {
            Yfunc();
        }).on("cancel", function () {
            
        });
    },
    successWin: function (msg,btn1,Yfunc) {
        var win = new LCWindow();
        win.alert({
            msg : msg,
            text4AlertBtn : btn1
        }).on("alert", function () {
            Yfunc();
        })
    }

}









$("#btn1").live('click.confirm', function () {
    var win = new LCWindow();
    win.confirm({
        msg : "您尚有已填写的信息没有提交，点击继续后未提交的<br>信息不会自动保存，是否继续？",
        text4ConfirmBtn:'继续',
        text4CancelBtn:'返回'
    }).on("confirm", function () {
        console.log('你点击了继续')
    }).on("cancel", function() {
        console.log('你点击了返回')
    });
});


$("#btn2").live('click.confirm', function () {
    var win = new LCWindow();
    win.confirm({
        msg : "请确认活动举办时间，确认提交后将无法再修改或删除！<br> 2015年11月23日 14:00 - 16:00",
        text4ConfirmBtn:'确认提交',
        text4CancelBtn:'返回修改'
    }).on("confirm", function () {
        console.log('你点击了继续')
    }).on("cancel", function() {
        console.log('你点击了返回')
    });
});


$("#btn3").live('click.confirm', function () {
    var win = new LCWindow();
    win.confirm({
        msg : "请确认以下来宾信息，确认提交后将无法再修改或删除！<br>" +
        "<table>" +
            "<tbody>" +
                "<tr>" +
                    "<td>姓名</td>" +
                    "<td>手机号码</td>" +
                    "<td>邀约人(选填)</td>" +
                "</tr>" +
                "<tr>" +
                    "<td>老滚</td>" +
                    "<td>136 6666 6666</td>" +
                    "<td>老崔啊</td>" +
                "</tr>" +
            "</tbody>" +
        "</table>",
        height:252,
        text4ConfirmBtn:'确认提交',
        text4CancelBtn:'返回修改'
    }).on("confirm", function () {
        console.log('你点击了继续')
    }).on("cancel", function() {
        console.log('你点击了返回')
    });
});

$("#btn4").live('click.confirm', function () {
    var win = new LCWindow();
    win.confirm({
        msg : "您尚有正在填写的来宾信息没有提交，点击继续后未提交的<br>信息不会自动保存，是否继续？",
        text4ConfirmBtn:'继续',
        text4CancelBtn:'返回'
    }).on("confirm", function () {
        console.log('你点击了继续')
    }).on("cancel", function() {
        console.log('你点击了返回')
    });
});


$("#btn5").live('click.confirm', function () {
    var win = new LCWindow();
    win.confirm({
        msg : "您尚未提交活动实际的举办时间，点击继续后未提交的<br>信息不会自动保存，是否继续？",
        text4ConfirmBtn:'继续',
        text4CancelBtn:'返回'
    }).on("confirm", function () {
        console.log('你点击了继续')
    }).on("cancel", function() {
        console.log('你点击了返回')
    });
});

$("#btn6").live('click.alert', function () {
    var win = new LCWindow();
    win.alert({
        msg : "您填写的来宾信息已全部成功提交！",
        text4AlertBtn : '确定'
    }).on("alert", function () {
        console.log('你点击了确定')
    })
});