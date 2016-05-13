define(['jquery', 'json2', 'plugins/layer', 'commonAjax'],
    function ($, json2, layer, Ajax) {
        var Consultants = {
            jEditableConfig: {
                "onblur": 'submit',  //默认是回车键提交 ，这里修改为失去焦点即提交
                "event": "dblclick",  //默认是单击可编辑 ， 这里修改为双击编辑
                "width": "90%",
                "height": "100%",
                "type": "number"
            },
            listObj: {
                list: [],
                DirectSellerIDs: [],
                tmpId: 0,
                timesMax: 3000,  //设置最大循环数量，防止死循环
                find: function (id) {
                    console.log(this.DirectSellerIDs);
                    return this.DirectSellerIDs.indexOf(id)
                },
                del: function (id) {
                    var listIndex = this.find(id);
                    if (listIndex < 0) return;
                    this.DirectSellerIDs.splice(listIndex, 1);
                    this.list.splice(listIndex, 1);
                    this.change();
                    return true;
                },
                push: function (data) {
                    this.DirectSellerIDs.push(data.DirectSellerID);
                    this.list.push(data);
                    this.change();
                },
                update: function (id, prop, value) {
                    console.log('------')
                    console.log(id);
                    console.log(prop);
                    console.log(value);
                    var context = this;
                    var listIndex = this.find(id);
                    if (prop == 'DirectSellerID') {

                        if (!value) {
                            console.log('no value');
                            return false;
                        }

                        if (this.find(value) > -1) return false;
                        this.DirectSellerIDs[listIndex] = value;
                    }
                    console.log(listIndex);
                    console.log(this.list[listIndex])
                    if (prop == 'Name' && this.list[listIndex][prop] != value)
                        this.list[listIndex]['IsUpdateName'] = true;

                    this.list[listIndex][prop] = value;
                    console.log(this.list)
                    return true;
                },
                clear: function () {
                    this.list = [];
                    this.DirectSellerIDs = [];
                    $('#editable tbody tr').remove();
                    this.change();
                },
                checkSubmit: function () {
                    var b = {};
                    if ($("#UserType").val() == "NormalBC") {
                        if ($(".checkSession input:checked").length < 1) return b = { result: false, msg: "还没有选择抢报活动时段！" }
                        if ($('#TicketQuantityPerSession').val() < 1) return b = { result: false, msg: "每人来宾券不能为 0！" }
                    } else if ($("#UserType").val() == "VolunteerBC") {
                        if ($("#VIPTicketQuantityPerPerson").val() < 1 && $("#NormalTicketQuantityPerPerson").val() < 1) return b = { result: false, msg: "志愿者来宾券和贵宾券不能同时为 0！" }
                    } 

                    if (this.list.length < 1) return b = { result: false, msg: "还没有导入顾问名单！" }

                    $.each(this.list, function (index, e) {
                        b = Consultants.InfoCheck(e, b);
                        if (!b.result) {
                            return false;
                        }
                    })
                    return b;
                },
                change: function () {
                    $(".counsultantCount span").html(this.list.length);
                }
            },
            pageConf: {
                // 新增行 --- 导入顾问，志愿者，抢票页 略有不同。
                newTr: function (data) {
                    return '<tr>' +
                            '<td class="DirectSellerID" id="' + (data.DirectSellerID || 'ID' + Consultants.listObj.tmpId++) + '" data-role="DirectSellerID">' + (data.DirectSellerID || '') + '</td>' +
                            '<td  data-role="Name">' + (data.Name || ' ') + '</td>' +
                            '<td data-role="PhoneNumber">' + (data.PhoneNumber || ' ') + '</td>' +
                            '<td class="Level" data-role="Level">' + (data.Level || ' ') + '</td>' +
                            '<td data-role="ResidenceID"  style="' + (data.IsDir ? "" : "color:#f00;") + '" >' + (data.ResidenceID || ' ') + '</td>' +
                            ' <td class="import-record__del"> <div class="btn btn-default">删除</div></td>' +
                            '</tr>'
                }
            },
            ticketOut: {
                newTr: function (data) {
                    return '<tr>' +
                            '<td >' + (data.DirectSellerID || '') + '</td>' +
                            '<td >' + (data.Name || ' ') + '</td>' +

                            '<td >' + (data.VIPRealCount) + ' / ' + (data.VIPSettingCount) + '</td>' +
                            '<td >' + (data.NormalRealCount) + ' / ' + (data.NormalSettingCount) + '</td>' +
                            '</tr>'
                }
            },
            ready: function () {
                $('#addNewRow').on('click', function () {
                    var n = false;
                    $.each(Consultants.listObj.DirectSellerIDs, function (i, e) {
                        console.log(e)
                        if (e.indexOf('ID') > -1) { n = true; return false; }
                    });
                    if (n) {
                        layer.alert('请填写正确的编号或完善已新增的空白记录!');
                        return;
                    }
                    var data = { IsDir: true, IsUpdateName: true, DirectSellerID: '',ContactID:'', Name: '', PhoneNumber: '', Level: '', ResidenceID: '', NormalTicketQuantity: 0, VIPTicketQuantity: 0, Province: '', City: '', County: '' }
                    var newTr = $(Consultants.pageConf.newTr(data));
                    Consultants.listObj.push({ DirectSellerID: 'ID' + (Consultants.listObj.tmpId - 1), IsDir: true, IsUpdateName: true, ContactID: '', Name: '', PhoneNumber: '', Level: '', ResidenceID: '', NormalTicketQuantity: 0, VIPTicketQuantity: 0, Province: '', City: '', County: '' });
                    newTr.find('td:not(".import-record__del")').editable(Consultants.jEditableCallback, Consultants.jEditableConfig);
                    $('#editable').find('tbody').append(newTr);

                }),
                $('#getInfoBtn').on('click', function () {
                    var listTextarea = $('#listTextarea');
                    var listTextareaValue = listTextarea.val();
                    var desired = listTextareaValue.replace(/[^\w\s]/gi, ''); //去除所有特殊字符
                    var listTextareaArray = desired.split("\n");
                    listTextareaArray = listTextareaArray.map(function (v) { return v.trim(); });
                    listTextareaArray = $.grep(listTextareaArray, function (n) { return n.trim() }); //去除空字符串 如最后一行是空的可能
                    $('#importModal').modal('hide');
                    $.event.trigger({
                        type: "modal.list",
                        list: listTextareaArray,
                        time: new Date()
                    });
                }),
                $('.btc-close').on('click', function () {
                //    layer.confirm('您确定要关闭本页面吗？', {
                //        title: '页面关闭提示：',
                //        btn: ['确定', '取消'] //按钮
                //    }, function () {
                        window.close();
                //    }, function () {

                //    });
                }),
                $(document).on('modal.list', function (e) {
                    console.time("33");
                    layer.myload.loading('加载中');
                    //layer.msg('加载中', { icon: 16, shade: [0.3, '#000'], shadeClose: false, time: 0 });
                    $.ajax({
                        url: Application + '/home/GetBCInfo',
                        type: 'post',
                        traditional: true,
                        data: { Ids: e.list, EventKey: $("#EventKey").val() },
                        dataType: 'json'
                    }).done(function (json) {

                        $.event.trigger({
                            type: "list.ajax.down",
                            message: json.data,
                            time: new Date()
                        });
                    }).fail(function () {
                        layer.alert("获取顾问信息失败！");
                    });
                }),
                $(document).on('list.ajax.down', function (e) {
                    Consultants.listObj.clear();

                    var i = 0;
                    var d = JSON.parse(e.message);
                    var times = 0;
                    function t() {
                        console.log(i);
                        console.log(d.length);

                        times++;
                        if (i >= d.length || times >= Consultants.listObj.timesMax) {

                            clearInterval(interval);
                            layer.closeAll('dialog');
                            console.log(Consultants.listObj.list.length);
                            console.timeEnd("33");

                            return;
                        }

                        var data = d[i];
                        console.log(data)

                        data.Name = data.LastName + data.FirstName;
                        //验证现存
                        if (Consultants.listObj.DirectSellerIDs.indexOf(data.DirectSellerID) > 0) {
                            i++; return;
                        }
                        Consultants.listObj.push(data);
                        var newTr = $(Consultants.pageConf.newTr(data));

                        if (data.IsDir) {
                            var c = ".DirectSellerID,.Level,.import-record__del";
                            if (data.Name && data.Name.length > 0) c += "," + ".Name";
                            //if (data.PhoneNumber && data.PhoneNumber.length > 0) c += "," + ".PhoneNumber";
                            //if (data.ResidenceID && data.ResidenceID.length > 0) c += "," + ".ResidenceID";
                            if (data.Province && data.Province.length > 0) c += "," + ".Province";
                            if (data.City && data.City.length > 0) c += "," + ".City";
                            newTr.find('td:not("' + c + '")').editable(Consultants.jEditableCallback, Consultants.jEditableConfig);
                        }
                        else
                            newTr.find('tr:not("td")').editable(Consultants.jEditableCallback, Consultants.jEditableConfig);

                        $('#editable tbody').append(newTr);
                        $(".layui-layer-dialog .layui-layer-content").contents().last()[0].textContent = "正在处理：" + i + "/" + d.length;

                        i++;

                    }
                    var interval = setInterval(t, 1);

                }),

                $('body').on('click.del', '.import-record__del .btn', function (e) {
                    var thisTr = $(this).closest('tr');
                    var id = thisTr.find('td:first-child').attr('id');
                    Consultants.listObj.del(id);
                    thisTr.remove();

                })
            },
            jEditableCallback: function (value, settings, elem) {
                var $this = $(this);
                var valueTrim = value.trim();
                var selfRole = $this.data('role');
                var DirectSellerIDTd = $this.closest('tr').find('td:first-child');
                var DirectSellerID = DirectSellerIDTd.attr('id');
                var ifexist = -1;
                var updateStatus = Consultants.listObj.update(DirectSellerID, selfRole, valueTrim);

                if (!valueTrim) return false;
                if (selfRole == 'DirectSellerID') {
                    if (!updateStatus) {
                        if (DirectSellerID != valueTrim) {
                            layer.alert('无法修改，此顾问编号已在表格中存在');
                            //$this.html("");
                        }
                        return DirectSellerID;
                    }

                    Consultants.CheckSellerId(valueTrim, $this)
                    $this.attr('id', valueTrim);
                }
                return (valueTrim);
            },
            CheckSellerId: function (val, obj) {
                Ajax.easyAjax("/home/CheckSellerId", "POST", { SellerId: val, EventKey: $("#EventKey").val() }, function (r) {
                    if (r.result) {
                        Consultants.listObj.update(val, "ContactID", r.ContactID);
                        Consultants.listObj.update(val, "MappingKey", r.MappingKey);
                        return true;
                    }
                    else {
                        layer.alert(r.msg, {
                            // skin: "#f00" //样式类名
                        });
                        obj.html("");
                        return false;
                    }
                });
            },
            InfoCheck: function (e, r) {
                console.log(e)
                if (!e.IsDir) {
                    r.result = false;
                    r.msg = "请删除编号错误或无效顾问的数据！";
                    return r;
                }

                if (e.DirectSellerID.length != 12) {
                    r.result = false;
                    r.msg = (e.DirectSellerID.length!=12?"最后一行顾问":e.DirectSellerID) + " 的编号不合法！";
                    return r;
                }

                if (e.Name.length > 20 || e.Name.length < 2) {
                    r.result = false;
                    r.msg = "编号 " + e.DirectSellerID + " 的顾问姓名不合法！";
                    return r;
                }
                if (!(/^1[1-9]\d{9}$/.test(e.PhoneNumber))) {
                    r.result = false;
                    r.msg = "编号 " + e.DirectSellerID + " 的顾问手机号不合法！";
                    return r;
                }
                if (!(/(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/.test(e.ResidenceID)) && e.ResidenceID) {
                    r.result = false;
                    r.msg = "编号 " + e.DirectSellerID + " 的顾问身份证号不合法！";
                    return r;
                }

                if (isNaN(e.Level)) {  //Number(e.Level) == 0 ||
                    r.result = false;
                    r.msg = "编号 " + e.DirectSellerID + " 的顾问职级不合法！";
                    return r;
                }

                if ($("#UserType").val() == "VIPBC") {
                    if (e.NormalTicketQuantity.length < 1) {
                        e.NormalTicketQuantity = 0;
                    }
                    if (e.VIPTicketQuantity.length < 1) {
                        e.VIPTicketQuantity = 0;
                    }
                }

                return r = { result: true };
            }

        };

        return Consultants;
    });