require(['jquery', 'bootstrap', 'json2', 'plugins/layer', 'conHandle', 'jqueryjeditable', 'commonAjax'],
    function ($, bootstrap, json2, layer, consultant, jeditable,Ajax) {
        var module = {
            jEditableConfig: {
                "onblur": 'submit',  //默认是回车键提交 ，这里修改为失去焦点即提交
                "event": "dblclick",  //默认是单击可编辑 ， 这里修改为双击编辑
                "width": "90%",
                "height": "100%"
                
            },
            pageConf: {
                // 新增行 --- 导入顾问，志愿者，抢票页 略有不同。
                newTr: function (data) {
                    return '<tr title="双击单元格修改" >' +
                            '<td class="DirectSellerID" id="' + (data.DirectSellerID || 'ID' + consultant.listObj.tmpId++) + '" data-role="DirectSellerID">' + (data.DirectSellerID || '') + '</td>' +
                            '<td class="Name" data-role="Name">' + (data.Name || ' ') + '</td>' +
                            '<td class="PhoneNumber" data-role="PhoneNumber">' + (data.PhoneNumber || ' ') + '</td>' +
                            '<td class="Level" data-role="Level">' + (data.Level || ' ') + '</td>' +
                            '<td class="ResidenceID" data-role="ResidenceID"  style="' + (data.IsDir ? "" : "color:#f00;") + '" >' + (data.ResidenceID || ' ') + '</td>' +
                           // '<td class="Province" data-role="Province"  >' + (data.Province || ' ') + '</td>' +
                           // '<td class="City" data-role="City"  >' + (data.City || ' ') + '</td>' +
                           // '<td  data-role="County" >' + (data.County || ' ') + '</td>' +
                            '<td class="import-record__del"> <div class="btn btn-default">删除</div></td>' +
                            '</tr>'
                }
            },
            ready: function () {
                $("#submitList").on('click', function () {
                    var res = consultant.listObj.checkSubmit();
                    if (!res.result) {
                        layer.alert(res.msg, { icon: 0, title: '警告：', closeBtn: false });
                        return false;
                    }
                    if ($('#ApplyTicketTotal').val().length < 1) {
                        layer.confirm('可抢报总数未填写，是否确认？', {
                            btn: ['确认', '取消'] //按钮
                        }, function () {
                            $.event.trigger({
                                type: "list.submit",
                                time: new Date()
                            });
                        });
                    } else {
                        $.event.trigger({
                            type: "list.submit",
                            time: new Date()
                        });
                    }
                    
                })

                

                $(document).on('list.submit', function () {
                    var res = consultant.listObj.checkSubmit();
                    if (!res.result) {
                        layer.alert(res.msg, { icon: 0, title: '警告：', closeBtn: false });
                        return false;
                    }
                    layer.myload.loading();
                    var sessions = []
                    $.grep($(".checkSession input:checked"), function (n) { sessions.push($(n).attr("id").replace("check_", "")) });
                    var data = {
                        Type: 0,
                        EventKey: $("#EventKey").val(),
                        ApplyTicketTotal: $("#ApplyTicketTotal").val().length > 0 ? $("#ApplyTicketTotal").val() : 0,
                        TicketQuantityPerSession: $("#TicketQuantityPerSession").val().length > 0 ? $("#TicketQuantityPerSession").val() : 0,
                        EventSessionkeys: sessions,
                        EventConsultants: consultant.listObj.list
                    }
                    console.log(JSON.stringify(data))
                    Ajax.easyAjax("/home/SaveConsultant", "POST", { data: JSON.stringify(data) }, function (r) {
                        if (r.result) {
                            layer.alert("保存顾问成功！", {
                                icon: 1,
                                title: '信息：',
                                closeBtn: false,
                                btn: '确定',
                                yes: function () {
                                     window.location.reload();
                                }

                            })
                        } else {
                            layer.alert(r.Msg, {
                                icon: 2,
                                title: '错误信息：',
                                closeBtn: false,
                                btn: '我知道了'
                            })
                        }

                    }, function (a, b, c) {
                        layer.alert(a + b + c, {
                            icon: 2,
                            title: '错误信息：',
                            closeBtn: false,
                            btn: '我知道了'
                        })
                    })
                })

                if (document.querySelectorAll("input[type='number']").length > 0) {

                    Array.prototype.forEach.call(document.querySelectorAll("input[type='number']"), function (input) {
                      
                        input.addEventListener('input', layer.handleInput);

                    })

                }

                consultant.ready();
                
            },
            submit: function () {

            }
        }


        $(function () {
            consultant.jEditableConfig = module.jEditableConfig;
            consultant.pageConf = module.pageConf;
            module.ready();


        });
    })
