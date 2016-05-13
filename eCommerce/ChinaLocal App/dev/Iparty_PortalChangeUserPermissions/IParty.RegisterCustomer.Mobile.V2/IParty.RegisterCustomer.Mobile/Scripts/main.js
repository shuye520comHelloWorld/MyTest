require.config({
    paths: {
        jquery: 'jquery-1.8.2.min',
        easyXDM: 'easyXDM',
        json2: 'json2',
        knockout: 'knockout-3.2.0',
        knockoutMapping: 'knockout.mapping-latest',
        QRCode: 'QRCode',
        common: 'common',
       // s_code: 's_code'
    }
});

require(['jquery', 'easyXDM', 'json2', 'knockout', 'knockoutMapping', 'QRCode', 'common'], function ($) {
    //alert($().jquery);
});
