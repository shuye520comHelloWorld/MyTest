require(['jquery', 'moment', 'jquerydatetimepicker', 'bootstrap', 'ViewModels/indexModel'],
    function ( $, moment, jquerydatetimepicker, bootstrap, model) {
        /*给所有的日历图标绑定点击事件*/
        $(function () {
            /*首页查询上的日历*/
            $('#filterStartDate').datetimepicker({
                defaultSelect: false,
                todayButton: false,
                step: 15,
                minTime: '23:46',
                onSelectDate: function (e) {
                    this.setOptions({
                        minTime: false
                    })
                }
            });
            $('#filterEndDate').datetimepicker({
                defaultSelect: false,
                todayButton: false,
                step: 15,
                minTime: '23:46',
                onShow: function (ct) {
                    var prevDate = $('#filterStartDate').val();
                    $(this).data('prevdate', prevDate);
                    this.setOptions({
                        minDate: prevDate ? prevDate.split(' ')[0] : false
                    });
                },
                onSelectDate: function (ct, $i) {
                    var prevDate = $('#filterStartDate').val();
                    var dateObj = ct;
                    var month = dateObj.getUTCMonth() + 1; //months from 1-12
                    var day = dateObj.getUTCDate();
                    var year = dateObj.getUTCFullYear();
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

            model.ready();
            activeTimeDom = new model.ActiveTime();
            model.getEvents(1);

            
            
        })
        
    


    })
