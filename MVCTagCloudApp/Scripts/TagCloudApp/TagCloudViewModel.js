(function () {
    var service = tagCloudService;

    $('#TagCloud').jQCloud(service.loadingArray, {
        delay: 50,
        autoResize: true,
        fontSize: {
            from: 0.1,
            to: 0.02
        }
    });

    function updateTagCloudContents() {
        var url = $("#urlInfo").val();
        var result = service.updateTagCloud(url);
        if (result.message === 'Failure') {
            $("#urlInfo").val('Please check the url entered. Prefix may be incorrect (e.g missing http:// or www)'); 
        }
        $('#TagCloud').jQCloud('update', result.tagArray);
    }

    $("#submitUrl").click(function () {

        $('#TagCloud').jQCloud('update', service.loadingArray);
        setTimeout(updateTagCloudContents, 3000);

    });

    $('#urlInfo').live("keypress", function (e) {
        if (e.keyCode == 13) {
            $('#TagCloud').jQCloud('update', service.loadingArray);
            setTimeout(updateTagCloudContents, 3000);
        }
    });

    





})();