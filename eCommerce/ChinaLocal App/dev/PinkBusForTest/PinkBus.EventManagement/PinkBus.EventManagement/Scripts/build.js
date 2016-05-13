({
    appDir: "./",
    baseUrl: "PinkBusJs",
    dir: "./PinkBusJsMini",
    fileExclusionRegExp: /^(r|build|main)\.js$/,
    optimizeCss: 'standard',
    removeCombined: false,
    paths: {
        bootstrap: 'lib/bootstrap.min',
        jquerydatetimepicker: 'lib/jquery.datetimepicker',
        jqueryjeditable: 'lib/jquery.jeditable',
        jquery: 'lib/jquery.min',
        moment: 'lib/moment.min',
        json2: 'lib/json2',
        commonAjax: 'plugins/commonAjax',
        layer: 'lib/layer',
        laypage: 'lib/layerPager',
        'jquery-mousewheel': 'lib/jquery.mousewheel',
        'date-functions': 'lib/date-functions',
        conHandle:'plugins/ConsultantsHandle',


    },
    modules: [
        {
            name: "Views/applyBC"
        },
       {
           name: "Views/index"
       },
       {
           name: "Views/vipBC"
       },
       {
           name: "Views/volunteerBC"
       }
      
    ]
})