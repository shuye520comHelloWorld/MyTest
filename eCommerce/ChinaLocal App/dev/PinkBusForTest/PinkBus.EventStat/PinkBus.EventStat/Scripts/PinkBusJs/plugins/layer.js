
define(['jquery', 'layer'],
    function ($, layer) {
        layer.config({
            path: Application + '/Content/layer/' //layer.js所在的目录，可以是绝对目录，也可以是相对目录
        });

        layer.myload = {
            loading: function (msg) {
                if (!msg) msg = '保存中';
                layer.msg(msg, { icon: 16, shade: [0.3, '#000'], shadeClose: false, time: 0 })
            }
        };

        layer.handleInput = function (ev) {
            var realvalue = $(this).data('realvalue') || '';
            var titleReg = /(^[0-9]+)/;
            var matchs = ev.target.value.match(titleReg);
            if (matchs) {
                realvalue = matchs[0];
                ev.target.value = realvalue;
            } else {
                ev.target.value = '';
            }
            $(this).data('realvalue', realvalue || '')
        }

        layer.DateFormat = function (times) {
            
            var date = new Date(times);
            
            return date.getFullYear() + "/" + ((date.getMonth() + 1) < 10 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1)) + "/" + (date.getDate() < 10 ? "0" + date.getDate() : date.getDate()) + " " + (date.getHours() < 10 ? "0" + date.getHours() : date.getHours()) + ":" + (date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes());
            
        }

        return layer;
    });