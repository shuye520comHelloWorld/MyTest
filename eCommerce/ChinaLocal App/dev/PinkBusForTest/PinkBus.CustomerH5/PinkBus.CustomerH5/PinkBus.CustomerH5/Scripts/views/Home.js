require(['HomeViewModel', "CommonShareTrack"],
    function (model, commonShareTrack) {
        var cst = new commonShareTrack();
        var currentModel = new model();
      
        currentModel.init(currentModel.showLoading());
        cst.pageLoadH5PV("Index","PinkBus_h5");
    });

