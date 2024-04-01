var a = {
    Data: [
        {
            "Name": "Microsoft Server Speech Text to Speech Voice (af-ZA, AdriNeural)",
            "DisplayName": "Adri",
            "LocalName": "Adri",
            "ShortName": "af-ZA-AdriNeural",
            "Gender": "Female",
            "Locale": "af-ZA",
            "LocaleName": "Afrikaans (South Africa)",
            "SampleRateHertz": "48000",
            "VoiceType": "Neural",
            "Status": "GA",
            "WordsPerMinute": "147"
        },
        {
            "Name": "Microsoft Server Speech Text to Speech Voice (af-ZA, WillemNeural)",
            "DisplayName": "Willem",
            "LocalName": "Willem",
            "ShortName": "af-ZA-WillemNeural",
            "Gender": "Male",
            "Locale": "af-ZA",
            "LocaleName": "Afrikaans (South Africa)",
            "SampleRateHertz": "48000",
            "VoiceType": "Neural",
            "Status": "GA",
            "WordsPerMinute": "155"
        }
    ]
};

// on change of Language
$("#languageselect").change(function (e) {
    // For the value in the Voice Dropdown
    $("#voiceselect").empty();
    let newData = a.Data.filter(data => data.Locale === e.target.value)
    newData.forEach(data => {
        var preview = "";
        if (data.Status == "Preview") {
            preview = "- Preview";
        }
        $("#voiceselect").append($('<option></option>').val(data.ShortName).html(data.DisplayName + " (" + data.VoiceType + ") " + preview));
    })
    // For the value in the SpeakingStyle Dropdown
    if (newData.length !== 0) {
        $("#voiceselect").val(newData[0].ShortName)
        $("#stylelist").empty();
        $("#stylelist").append($('<option value="general">General</option>'));
        // console.log("Voice:e", e.target.value)
        let newData1 = a.Data.filter(data => data.Locale === $("#languageselect").val() && data.DisplayName === newData[0].DisplayName)
        //console.log("newData1: ", newData1)
        newData1.forEach(data1 => {
            // console.log("stylelist", data1.StyleList)
            if (data1.StyleList == undefined) {
                $('#stylelist').prop('disabled', true);
            }
            if (data1.StyleList != undefined) {
                $('#stylelist').prop('disabled', false);
                //console.log("stylelist", data1.StyleList)
                for (var i = 0; i < data1.StyleList.length; i++) {
                    $("#stylelist").append($('<option></option>').val(data1.StyleList[i]).html(data1.StyleList[i]));
                }
            }
        })
    }
});

// on change of Voice
$("#voiceselect").change(function (e) {
    // For the value in the SpeakingStyle Dropdown
    $("#stylelist").empty();
    $("#stylelist").append($('<option value="general">General</option>'));
    // console.log("Voice:e", e.target.value)
    let newData = a.Data.filter(data => data.Locale === $("#languageselect").val() && data.ShortName === e.target.value)
    //console.log("newData: ", newData)
    newData.forEach(data => {
        // console.log("stylelist", data.StyleList)
        if (data.StyleList == undefined) {
            $('#stylelist').prop('disabled', true);
        }
        if (data.StyleList != undefined) {
            $('#stylelist').prop('disabled', false);
            //console.log("stylelist", data.StyleList)
            for (var i = 0; i < data.StyleList.length; i++) {
                $("#stylelist").append($('<option></option>').val(data.StyleList[i]).html(data.StyleList[i]));
            }
        }
    })
});

function GetTextToSpeech() {
    var fdata = new FormData();
    $('#frmTextToSpeech textarea').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('textarea#' + $(this).attr('id')).val());
        }
    });
    $('#frmTextToSpeech input').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $(this).val());
        }
    });
    $('#frmTextToSpeech select').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('select#' + $(this).attr('id') + ' option:selected').val());
        }
    });

    $("#audioPlayer").empty();
    $.ajax({
        url: '/Home/TextToSpeechData',
        data: fdata,
        type: "POST",
        processData: false,
        contentType: false,
        async: true,
        dataType: "json",
        success: function (data) {
            if (data.isSuccess) {
                var audioPlayer = '';
                var todayDate = Date.now();
                var audioPath = "/TextToSpeeech_AudioFiles/temp.mp3?d=" + todayDate;
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
        },
        complete: function () {
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.error().statusText);
        }
    });
}

function GetTextToSpeech1() {
    var fdata = new FormData();
    $('#frmTextToSpeech textarea').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('textarea#' + $(this).attr('id')).val());
        }
    });
    $('#frmTextToSpeech input').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $(this).val());
        }
    });
    $('#frmTextToSpeech select').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('select#' + $(this).attr('id') + ' option:selected').val());
        }
    });

    $("#audioPlayer").empty();
    $.ajax({
        url: '/Home/TextToSpeechData1',
        data: fdata,
        type: "POST",
        processData: false,
        contentType: false,
        async: true,
        dataType: "json",
        success: function (data) {
            if (data.isSuccess) {
                var audioPlayer = '';
                var todayDate = Date.now();
                var audioPath = "/TextToSpeeech_AudioFiles/temp.mp3?d=" + todayDate;
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
        },
        complete: function () {
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.error().statusText);
        }
    });
}

function resetAudio() {
    $('#bgMusic')[0].pause();
    $('#bgMusic')[0].currentTime = 0;
    $('#stopbtn').hide();
    $('#playbtn').show();
}

function DictionarySpeech1() {
    var fdata = new FormData();
    $('#frmDictionary textarea').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('textarea#' + $(this).attr('id')).val());
        }
    });
    $('#frmDictionary select').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('select#' + $(this).attr('id') + ' option:selected').val());
        }
    });

    $("#audioPlayer").empty();
    $.ajax({
        url: '/Home/AddToDictionary1',
        data: fdata,
        type: "POST",
        processData: false,
        contentType: false,
        async: true,
        dataType: "json",
        success: function (data) {
            if (data.isSuccess) {
                var audioPlayer = '';
                var todayDate = Date.now();
                var audioPath = "/TextToSpeeech_AudioFiles/temp.mp3?d=" + todayDate;
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
        },
        complete: function () {
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.error().statusText);
        }
    });
}

function DictionarySpeech2() {
    var fdata = new FormData();
    $('#frmDictionary textarea').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('textarea#' + $(this).attr('id')).val());
        }
    });
    $('#frmDictionary select').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('select#' + $(this).attr('id') + ' option:selected').val());
        }
    });
    $("#audioPlayer").empty();
    $.ajax({
        url: '/Home/AddToDictionary2',
        data: fdata,
        type: "POST",
        processData: false,
        contentType: false,
        async: true,
        dataType: "json",
        success: function (data) {
            if (data.isSuccess) {
                var audioPlayer = '';
                var todayDate = Date.now();
                var audioPath = "/TextToSpeeech_AudioFiles/temp.mp3?d=" + todayDate;
                audioPlayer += '<audio id="bgMusic" src="' + audioPath + '"></audio>';
                $("#audioPlayer").append(audioPlayer);
                $('#bgMusic')[0].play();
                $('#stopbtn2').show();
                $('#playbtn2').hide();
                $("#bgMusic").bind('ended', function () {
                    // done playing
                    $('#stopbtn2').hide();
                    $('#playbtn2').show();
                });
            }
        },
        complete: function () {
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.error().statusText);
        }
    });
}

function SaveDictionary() {
    $("#divLoader").append(getLoaderSmall());
    var fdata = new FormData();

    $('#frmDictionary textarea').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('textarea#' + $(this).attr('id')).val());
        }
    });
    $('#frmDictionary select').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('select#' + $(this).attr('id') + ' option:selected').val());
        }
    });

    $("#audioPlayer").empty();
    $.ajax({
        url: '/Home/SaveDictionary',
        data: fdata,
        type: "POST",
        processData: false,
        contentType: false,
        async: true,
        dataType: "json",
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

function resetAudio2() {
    $('#bgMusic')[0].pause();
    $('#bgMusic')[0].currentTime = 0;
    $('#stopbtn2').hide();
    $('#playbtn2').show();
}

function Pronunciation() {
    $.ajax({
        url: '/Home/AddToDictionary', //***** from controller (Inside GetSentenceList called Partial View _ListSentencePartial)
        data: { "ID": 1 },
        type: "POST",
        cache: false,
        async: false,
        contentType: "application/x-www-form-urlencoded",
        success: function (result) {
            BootstrapDialog.show({
                id: "divAddToDictionary",
                title: "AddToDictionary",
                size: BootstrapDialog.SIZE_EXTRAWIDE,
                message: function () {
                    var $message = $('<div></div>');
                    $message.append(result);
                    return $message;
                },
                closable: true,
                closeByBackdrop: false,
                //onshown: function (dialogRef) {
                //    feedbackMob = [];
                //    $("#mobileNum").val("");
                //}
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}