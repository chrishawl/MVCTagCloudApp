var tagCloudService = (function () {

    var tagCloudErrorData = [{ text: "Error", weight: 10 }, { text: "Has", weight: 9 }, { text: "Occurred", weight: 8 }, { text: "Please", weight: 7 }, { text: "Try", weight: 7 },
        { text: "Another", weight: 7 }, { text: "URL", weight: 10 }, { text: "Error", weight: 9 }, { text: "Error", weight: 5 }, { text: "Error", weight: 10 }, { text: "Has", weight: 9 }, { text: "Occurred", weight: 8 }, { text: "Please", weight: 7 }, { text: "Try", weight: 7 },
        { text: "Another", weight: 7 }, { text: "URL", weight: 10 }, { text: "Error", weight: 9 }, { text: "Error", weight: 5 }];

    var loadingData = [{ text: "Loading", weight: 10 }, { text: "Please", weight: 9 }, { text: "Wait", weight: 8 }, { text: "........", weight: 7 }, { text: "Loading", weight: 3 },
        { text: "Please", weight: 7 }, { text: "Wait", weight: 10 }, { text: "Loading", weight: 9 }, { text: "Wait", weight: 5 }, { text: "Loading", weight: 10 }, { text: "Please", weight: 9 }, { text: "Wait", weight: 8 }, { text: "........", weight: 7 }, { text: "Loading", weight: 3 },
        { text: "Please", weight: 7 }, { text: "Wait", weight: 10 }, { text: "Loading", weight: 9 }, { text: "Wait", weight: 5 }];

    var getNewTagCloud = function (webUrl) {
        var tagCloudData = [];
               $.ajax({
            data: { url: webUrl},
            url: "/home/GetWordCloud/",
            async: false
        }).done(function (response) {

            if (typeof response !== typeof undefined && response !== null && response.result === 'Success' && typeof response.data !== typeof undefined && response.data !== null && response.data.length > 0) {
                tagCloudData = {
                    message: 'Success', tagArray: response.data
                };
                }
                else {                    
                tagCloudData = { message: 'Failure', tagArray: tagCloudErrorData };

                }
            })
       
        return tagCloudData;
    };


    return {
        updateTagCloud: getNewTagCloud,
        loadingArray: loadingData
    }
})();