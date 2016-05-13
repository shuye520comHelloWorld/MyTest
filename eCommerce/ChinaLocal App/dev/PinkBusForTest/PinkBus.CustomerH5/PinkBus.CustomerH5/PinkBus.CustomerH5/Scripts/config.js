if (env == true || env == "true") {
    require.config({
        baseUrl: VirtualDirectory + '/ScriptsMin'
    });
} else {
    require.config({
        baseUrl: VirtualDirectory + '/Scripts',
        //urlArgs: "v=" + new Date().getTime()
    });
}

require.config({
    waitSeconds: 2000,
    shim: {     
        's_code': {
            exports: 's'
        },
        'jweixin': {
            deps: ['jquery'],
            exports: 'wx'
        },
        'WechatJS': {
            exports: 'WXJSDK'
        },
        'main': {
            deps:['jquery']
        },
        'jquery-iplugin': {
            deps:['jquery']
        },
        'QRCode': {
            deps:['jquery']
        },
        'jquery-qrcode': {
            deps:['jquery']}
        },
    paths: {
        'jquery-qrcode':'lib/jquery.qrcode-0.12.0',
        'QRCode':'lib/QRCode',
        'jquery-iplugin':'common/jquery.IPlugIn',
        'main':'common/main',       
        'jquery': 'lib/jquery.min',
        'modal': 'lib/jquery.modal.min',
        'knockout': 'Lib/knockout-2.3.0',
        'easyXDM': 'lib/easyXDM',    
        'httpClient': 'common/HttpClient',
        'knockoutmapping': 'lib/knockout.mapping-latest',
        'jweixin': 'https://res.wx.qq.com/open/js/jweixin-1.0.0',
        'CommonShareTrack': 'Common/CommonShareTrack',
        's_code': 'lib/s_code',
        'WechatJS': 'Common/WechatJS',
        'WechatShare': 'common/WechatShare',
        'HomeViewModel': 'viewmodels/HomeViewModel',
        'CallbackViewModel': 'viewmodels/CallbackViewModel',
        'moment': 'lib/moment-with-locales',
        'SuccessViewModel': 'viewmodels/SuccessViewModel'
     
    }
});