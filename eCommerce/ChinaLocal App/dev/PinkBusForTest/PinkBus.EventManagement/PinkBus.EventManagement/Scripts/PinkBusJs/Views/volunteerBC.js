require(['jquery', 'bootstrap', 'json2', 'plugins/layer', 'conHandle', 'jqueryjeditable', 'commonAjax'],
    function ($, bootstrap, json2, layer, consultant, jeditable, Ajax) {
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
                    return '<tr>' +
                            '<td class="DirectSellerID" id="' + (data.DirectSellerID || 'ID' + consultant.listObj.tmpId++) + '" data-role="DirectSellerID">' + (data.DirectSellerID || '') + '</td>' +
                            '<td class="Name" data-role="Name">' + (data.Name || ' ') + '</td>' +
                            '<td class="PhoneNumber" data-role="PhoneNumber">' + (data.PhoneNumber || ' ') + '</td>' +
                            '<td class="Level" data-role="Level">' + (data.Level || ' ') + '</td>' +
                            '<td class="ResidenceID" data-role="ResidenceID"  style="' + (data.IsDir ? "" : "color:#f00;") + '" >' + (data.ResidenceID || ' ') + '</td>' +
                            '<td class="Province" data-role="Province"  >' + (data.Province || ' ') + '</td>' +
                            '<td class="City" data-role="City"  >' + (data.City || ' ') + '</td>' +
                            '<td  data-role="CountyName" >' + (data.CountyName || ' ') + '</td>' +
                            '<td class="import-record__del"> <div class="btn btn-default">删除</div></td>' +
                            '</tr>'
                }
            },
            ready: function () {
                $("#submitList,#ticketOutSubmit").on('click', function () {
                    var res = consultant.listObj.checkSubmit();
                    if (!res.result) {
                        layer.alert(res.msg, {
                            // skin: "#f00" //样式类名
                        });
                        return false;
                    }

                    layer.myload.loading();
                    var data = {
                        Type: 1,
                        EventKey: $("#EventKey").val(),
                        //ApplyTicketTotal: $("#ApplyTicketTotal").val().length > 0 ? $("#ApplyTicketTotal").val() : 0,
                        //TicketQuantityPerSession: $("#TicketQuantityPerSession").val().length > 0 ? $("#TicketQuantityPerSession").val() : 0,
                        //EventSessions: [],
                        NormalTicketQuantityPerPerson: $("#NormalTicketQuantityPerPerson").val().length > 0 ? $("#NormalTicketQuantityPerPerson").val() : 0,
                        VIPTicketQuantityPerPerson: $("#VIPTicketQuantityPerPerson").val().length > 0 ? $("#VIPTicketQuantityPerPerson").val() : 0,
                        EventConsultants: consultant.listObj.list,
                        BtnType: this.id == 'submitList' ? 0 : 1

                    }
                    
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
                            if (r.showPop) {
                                var tdata = JSON.parse(r.data);
                                console.log(tdata);
                                $('#TicketOutTable').find('tbody tr').remove();
                                tdata.map(function (m) {
                                    var newTr = $(consultant.ticketOut.newTr(m));
                                    $('#TicketOutTable').find('tbody').append(newTr);

                                })
                                layer.closeAll('dialog')
                                $("#TicketOutConsultant").modal('show');
                            } else {
                                layer.alert(r.Msg || "保存顾问失败，请重新导入数据后重试！", {
                                    icon: 2,
                                    title: '错误信息：',
                                    closeBtn: false,
                                    btn: '我知道了'
                                })
                            }
                            
                        }

                    }, function (a,b,c) {
                        layer.alert(a+b+c, {
                            icon: 2,
                            title: '错误信息：',
                            closeBtn: false,
                            btn: '我知道了'
                        })
                    })
                })

                consultant.ready();

            },
        }


        $(function () {
            consultant.jEditableConfig = module.jEditableConfig;
            consultant.pageConf = module.pageConf;
            module.ready();
        });
    })
