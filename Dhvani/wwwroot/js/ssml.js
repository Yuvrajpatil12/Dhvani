function SaveDictionary() {
    var msg = "";
    var isValid = true;

    if ($("#languageselect").val() == '' || $("#languageselect").val() == null) {
        msg = msg + "\n" + "Language";
        isValid = false;
    }

    if ($("#voiceselect").val() == '' || $("#voiceselect").val() == null) {
        msg = msg + "\n" + "Voice";
        isValid = false;
    }

    if ($("#ttsssml1").val() == '') {
        msg = msg + "\n" + "User Text";
        isValid = false;
    }
    else {
        if ($("#ttsssml1").val().length > 100) {
            msg = msg + "\n" + "User Text content limit is 100 characters including space.";
            isValid = false;
        }
    }

    if ($("#ttsssml2").val() == '') {
        msg = msg + "\n" + "Alternate User Text";
        isValid = false;
    }
    else {
        if ($("#ttsssml2").val().length > 100) {
            msg = msg + "\n" + "Alternate User Text content limit is 100 characters including space.";
            isValid = false;
        }
    }

    if (isValid) {
        var objToSend = {
            // Populate with properties of your SSML class
            UserAPIKey: "cmsssml",
            UserText: $("#ttsssml1").val(),
            UserAlternateText: $("#ttsssml2").val(),
            Locale: $('#languageselect option:selected').val(),
            ShortName: $('#voiceselect option:selected').val()
        };

        $("#audioPlayer1").empty();
        $("#audioPlayer2").empty();

        var bearerToken = '';
        // Usage
        getToken().then(function (token) {
            bearerToken = token;
            if (bearerToken == "1") {
                showBSAlert(__requiredCaption, "Invalid User Session.", __WARNING);
            }
            else {
                $.ajax({
                    url: '/api/SSML',
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
                            BootstrapDialog.show({
                                id: "divSaveClass",
                                title: __SUCCESS,
                                type: getDialogType(__SUCCESS),
                                message: function () {
                                    var $message = $('<div id="divSaveClassInner"></div>');
                                    $message.append(data.message);
                                    return $message;
                                },
                                closeByBackdrop: false,
                                buttons: [{
                                    label: __ok,
                                    cssClass: 'btn btn-primary',
                                    action: function (dialog) {
                                        window.location.reload();
                                    }
                                }]
                            });
                        }
                    },
                    complete: function () {
                        setTimeout(function () {
                            removeLoader("#divLoader");
                        }, 300);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        console.log(xhr.error().statusText);
                    }
                });
            }
        });
    }
    else if (msg != "") {
        showBSAlert(__requiredCaption, msg, __WARNING);
    }
    msg = "";
}

function resetAudio(textType) {
    $('#bgMusic' + textType)[0].pause();
    $('#bgMusic' + textType)[0].currentTime = 0;
    $('#stopbtn' + textType).hide();
    $('#playbtn' + textType).show();
}

function GetVoice(textType) {
    var msg = "";
    var isValid = true;

    var strTextName = "";

    if (textType == "2") {
        strTextName = "Alternate ";
    }

    if ($("#languageselect").val() == '' || $("#languageselect").val() == null) {
        msg = msg + "\n" + "Language";
        isValid = false;
    }

    if ($("#voiceselect").val() == '' || $("#voiceselect").val() == null) {
        msg = msg + "\n" + "Voice";
        isValid = false;
    }

    if ($(".ttsssml" + textType).val() == '') {
        msg = msg + "\n" + strTextName + "User Text";
        isValid = false;
    }
    else {
        if ($("#ttsssml1").val().length > 100) {
            msg = msg + "\n" + strTextName + "User Text content limit is 100 characters including space.";
            isValid = false;
        }
    }

    if (isValid) {
        var objToSend = {
            // Populate with properties of your Voice class
            UserAPIKey: "cmsssml",
            CallBackUrl: "https://localhost:7266/Api/User/PostApi",
            VoiceText: $(".ttsssml" + textType).val(),//'this is test',
            DisplayName: "temp" + textType,
            Locale: $('#accent option:selected').val(), //'af-ZA',
            SpeakingStyle: 'general',
            ShortName: $('#voiceselect option:selected').val(),//'af-ZA-AdriNeural',
            SpeakingSpeed: '1',
            Pitch: '1'
        };

        var bearerToken = '';
        // Usage
        getToken().then(function (token) {
            // Use the token here or pass it to another function
            bearerToken = token;

            if (bearerToken == "1") {
                showBSAlert(__requiredCaption, "Invalid User Session.", __WARNING);
            }
            else {
                $("#audioPlayer" + textType).empty();
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
                            var audioPlayer = '';
                            var todayDate = Date.now();
                            var audioPath = "/AudioFiles/" + data.flag + "?d=" + todayDate;
                            audioPlayer += '<audio id="bgMusic' + textType + '" src="' + audioPath + '"></audio>';
                            $("#audioPlayer" + textType).append(audioPlayer);
                            $('#bgMusic' + textType)[0].play();
                            $('#stopbtn' + textType).show();
                            $('#playbtn' + textType).hide();

                            $("#bgMusic" + textType).bind('ended', function () {
                                // done playing
                                $('#stopbtn' + textType).hide();
                                $('#playbtn' + textType).show();
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

function GetVoicePronunciation(textType) {
    var msg = "";
    var isValid = true;

    var strTextName = "";

    if (textType == "2") {
        strTextName = "Alternate ";
    }

    if ($("#languageselect").val() == '' || $("#languageselect").val() == null) {
        msg = msg + "\n" + "Language";
        isValid = false;
    }
    if ($("#accent").val() == '' || $("#accent").val() == null) {
        msg = msg + "\n" + "Accent";
        isValid = false;
    }
    if ($(".ttsssml" + textType).val() == '') {
        msg = msg + "\n" + strTextName + "User Text";
        isValid = false;
    }
    else {
        //if ($("#ttsssml1").val().length > 100) {
        //    msg = msg + "\n" + strTextName + "User Text content limit is 100 characters including space.";
        //    isValid = false;
        //}
    }

    if (isValid) {
        var objToSend = {
            // Populate with properties of your Voice class
            UserAPIKey: "cmsssml",
            CallBackUrl: "https://localhost:7266/Api/User/PostApi",
            VoiceText: $(".ttsssml" + textType).val(),//'this is test',
            DisplayName: $('#voiceselect option:selected').val().split('-')[2],
            Locale: $('#accent option:selected').val(), //'af-ZA',
            SpeakingStyle: 'general',
            ShortName: $('#voiceselect option:selected').val(),//'af-ZA-AdriNeural',
            SpeakingSpeed: '1',
            Pitch: '1',
            TTSType: 'pro'
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
                            $('#stopbtn' + textType).show();
                            $('#playbtn' + textType).hide();

                            $("#bgMusic").bind('ended', function () {
                                // done playing
                                $('#stopbtn' + textType).hide();
                                $('#playbtn' + textType).show();
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

function GetVoiceEditPronunciation(textType, id) {
    var msg = "";
    var isValid = true;

    if (isValid) {
        var objToSend = {
            UserAPIKey: "cmsssml",
            CallBackUrl: "https://localhost:7266/Api/User/PostApi",
            VoiceText: $(".ttsssml" + textType + '_' + id).val(),
            DisplayName: $('#voice_' + id).val().split('-')[2],
            Locale: $('#accent_' + id).val(),
            SpeakingStyle: 'general',
            ShortName: $('#voice_' + id).val(),
            SpeakingSpeed: '1',
            Pitch: '1',
            TTSType: 'pro'
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
                            $('#stopbtn' + textType + '_' + id).show();
                            $('#playbtn' + textType + '_' + id).hide();

                            $("#bgMusic").bind('ended', function () {
                                // done playing
                                $('#stopbtn' + textType + '_' + id).hide();
                                $('#playbtn' + textType + '_' + id).show();
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
            url: '/api/Token?apiKey=cmsssml',
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