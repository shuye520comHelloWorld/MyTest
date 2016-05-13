
requirejs.config({
    baseUrl: miniJs == "true" ? Application + '/Scripts/PinkBusJsMini/PinkBusJs' : Application + '/Scripts/PinkBusJs',
    urlArgs: "v=" + Ver,
    shim: {
        'moment': ['jquery'],
        'bootstrap': ['jquery'],
        'jquerydatetimepicker': ['jquery'],
        'jqueryjeditable': ['jquery'],
        'layer': ['jquery'],
        'commonAjax': ['jquery'],
        'ConsultantsHandle': ['jquery'],
    },
    paths: {
        bootstrap: 'lib/bootstrap.min',
        jquerydatetimepicker: 'lib/jquery.datetimepicker',
        jqueryjeditable: 'lib/jquery.jeditable',
        jquery: 'lib/jquery.min',
        moment: 'lib/moment.min',
        laypage: 'lib/layerPager',
        commonAjax: 'plugins/commonAjax',
        layer: 'lib/layer',
        conHandle:'plugins/ConsultantsHandle',
        //knockout: 'lib/knockout-2.3.0',
        'jquery-mousewheel': 'lib/jquery.mousewheel',
        'date-functions': 'lib/date-functions',
        'json2': 'lib/json2',
        

    }
});



