require(['SuccessViewModel', "CommonShareTrack"],
    function (model, commonShareTrack) {
        var cst = new commonShareTrack();
        var currentModel = new model();
        currentModel.init(currentModel.showLoading());
        cst.pageLoadH5PV("Success", "PinkBus_h5");
    });

