//const bootstrapDialog = require("../Scripts/plugins/bootstrap-dialog/bootstrap-dialog");

function GetVoice() {
    var msg = "";
    var isValid = true;
    
    if ($("#languageselect").val() == '' || $("#languageselect").val() == null) {
        msg = msg + "Language";
        isValid = false;
    }
    if ($("#accent").val() == '' || $("#accent").val() == null || $("#accent").val() == 0) {
        msg = msg + "\n" + "Accent";
        isValid = false;
    }
    if ($("#voiceselect").val() == '' || $("#voiceselect").val() == null || $("#voiceselect").val() == 0) {
        msg = msg + "\n" + "Voice";
        isValid = false;
    }
    if ($("#stylelist").val() == '' || $("#stylelist").val() == null) {
        msg = msg + "\n" + "Speaking style";
        isValid = false;
    }

    if ($("#ttsssml").val() == '') {
        msg = msg + "\n" + "Voice Content";
        isValid = false;
    }
    else {
        if ($("#ttsssml").val().length > 8000) {
            msg = msg + "Voice content limit is 8000 characters including space.";
            isValid = false;
        }
    }

    if (isValid) {
        var objToSend = {
            // Populate with properties of your Voice class
            UserAPIKey: "cms",
            CallBackUrl: "https://localhost:7266/Api/User/PostApi",
            VoiceText: $("#ttsssml").val(),//'this is test',
            DisplayName: $('#voiceselect option:selected').val().split('-')[2],
            Locale: $('#accent option:selected').val(), //'af-ZA',
            SpeakingStyle: $('#stylelist option:selected').val(),//'general',
            ShortName: $('#voiceselect option:selected').val(),//'af-ZA-AdriNeural',
            SpeakingSpeed: $('#speed').val(), //'0',
            Pitch: $('#pitch').val()  //'0'
        };

        $('#audioPlayerLink').hide();
        var bearerToken = '';
        // Usage
        getToken().then(function (token) {
            // Use the token here or pass it to another function
            bearerToken = token;

            if (bearerToken == "1") {
                showBSAlert(__requiredCaption, "Invalid User Session.", __WARNING);
            }
            else {
                $("#audioPlayer").empty();
                $("#divLoader").append(getLoader());
                $.ajax({
                    url: '/api/Voice/generatespeechAsync',
                    data: JSON.stringify(objToSend),
                    type: "POST",
                    processData: false,
                    contentType: 'application/json',
                    async: true,
                    headers: {
                        'Authorization': 'Bearer ' + bearerToken
                    },
                    success: function (data) {
                        if (data.isSuccess) {
                            $('#dvaudioPlayerLink').show();
                            $('#audioPlayerLink').show();

                            var audioPlayer = '';
                            var todayDate = Date.now();
                            var audioPath = "/AudioFiles/" + data.flag + "?d=" + todayDate;
                            $('#audioPlayerLink').attr('href', audioPath);

                            audioPlayer += '<audio id="bgMusic" src="' + audioPath + '"></audio>';
                            $("#audioPlayer").append(audioPlayer);
                            $('#bgMusic')[0].play();
                            $('#stopbtn').show();
                            $('#playbtn').hide();

                            $("#bgMusic").bind('ended', function () {
                                // done playing
                                $('#stopbtn').hide();
                                $('#playbtn').show();
                            });
                        }
                        else {
                            if (data.flag == "1")
                                showBSAlert(__requiredCaption, "Invalid User Session.", __WARNING);
                        }
                    },
                    complete: function () {
                        setTimeout(function () {
                            removeLoader("#divLoader");
                        }, 300);
                    },
                    error: function (error) {
                        //Handle error response from the server
                        console.error(error.responseText);
                    }
                });
            }
        })
            .catch(function (error) {
                // Handle error
                console.error('Error:', error.responseText);
            });
    }
    else if (msg != "") {
        showBSAlert(__requiredCaption, msg, __WARNING);
    }
    msg = "";
}

function getToken() {
    return new Promise(function (resolve, reject) {
        // Make the AJAX request
        $.ajax({
            url: '/api/Token?apiKey=cms',
            type: 'GET',
            async: false,
            success: function (data) {
                // Handle success response from the server
                console.log('Token:', data);

                // Resolve the Promise with the token
                resolve(data);
            },
            error: function (error) {
                // Handle error response from the server
                console.error(error.responseText);

                // Reject the Promise with the error
                reject(error);
            }
        });
    });
}

function resetAudio() {
    $('#bgMusic')[0].pause();
    $('#bgMusic')[0].currentTime = 0;
    $('#stopbtn').hide();
    $('#playbtn').show();
}