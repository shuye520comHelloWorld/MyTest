
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

        return layer;
    });