// send the selected dropdown value to the controller and get the data from the controller and display the second drop down value and fill those value in second drop down

function getFLanguage() {
    $.ajax({
        url: '/Pronunciation/GetLanguage',
        data: { type: 'Language' },
        type: 'GET',
        contentType: 'application/json',
        success: function (data) {
            console.log("Received data:", data);
            populateDropDown(data);
        },
        error: function () {
            // Handle error
            console.log('Error occurred while fetching dropdown values.');
        }
    });
}

function populateDropDown(data) {
    // Assuming data is an array of objects with localName property
    var select = $('#languageselect'); // Assuming the ID of your dropdown is languageselect
    select.empty(); // Clear previous options
    select.append($('<option></option>').attr('value', '').text('Select'));
    $.each(data, function (index, item) {
        // Append option to dropdown
        select.append($('<option></option>').attr('value', item.locale).text(item.localeName));
    });
}

// =====================
// First dropdown End here
// =====================

// Assuming the first dropdown ID is 'languageselect' and the second dropdown ID is 'voiceselect'
$('#languageselect').change(function () {
    var selectedLanguage = $(this).val();
    getFAccent(selectedLanguage);
    //getFVoice(selectedLanguage);
});
$('#accent').change(function () {
    var selectedLanguage = $(this).val();
    // getFAccent(selectedLanguage);
    getFVoice(selectedLanguage);
});

function getFVoice(selectedLanguage) {
    $.ajax({
        url: '/Pronunciation/GetVoice',
        type: 'GET',
        data: { language: selectedLanguage }, // Pass selected language as parameter
        contentType: 'application/json',
        success: function (data) {
            console.log("Received related values:", data);
            DropDownVoice(data); // Call function to populate related values in the second dropdown
        },
        error: function () {
            // Handle error
            console.log('Error occurred while fetching related values.');
        }
    });
}
function getFAccent(selectedLanguage) {
    $.ajax({
        url: '/Pronunciation/GetAccent',
        type: 'GET',
        data: { language: selectedLanguage }, // Pass selected language as parameter
        contentType: 'application/json',
        success: function (data) {
            console.log("Received related values:", data);
            DropDownAccent(data); // Call function to populate related values in the second dropdown
        },
        error: function () {
            // Handle error
            console.log('Error occurred while fetching related values.');
        }
    });
}
function DropDownAccent(data) {
    var select = $('#accent'); // Assuming the ID of your second dropdown is 'voiceselect'
    select.empty(); // Clear previous options

    // Check if the dropdown is empty
    if (select.children().length === 0) {
        // Append a default option with some text (replace 'Select a value' with your desired default text)
        select.append($('<option></option>').attr('value', '').text('Select Accent').prop('selected', true));
    }

    $.each(data, function (index, item) {
        var optionText = item.localeName;
        select.append($('<option></option>').attr('value', item.locale).text(optionText));
    });
}
function DropDownVoice(data) {
    var select = $('#voiceselect'); // Assuming the ID of your second dropdown is 'voiceselect'
    select.empty(); // Clear previous options

    // Check if the dropdown is empty
    if (select.children().length === 0) {
        // Append a default option with some text (replace 'Select a value' with your desired default text)
        select.append($('<option></option>').attr('value', '').text('Select Voice').prop('selected', true));
    }

    $.each(data, function (index, item) {
        var optionText = item.displayName + ' (' + item.voiceType + ')';
        select.append($('<option></option>').attr('value', item.shortName).text(optionText));
    });
}

$('#voiceselect').change(function () {
    var selectedLanguage = $(this).val();
    // getFAccent(selectedLanguage);
    getFStyle(selectedLanguage);
});

function getFStyle(selectedLanguage) {
    $.ajax({
        url: '/Pronunciation/GetStyle',
        type: 'GET',
        data: { language: selectedLanguage }, // Pass selected language as parameter
        contentType: 'application/json',
        success: function (data) {
            console.log("Received related values:", data);
            DropDownStyle(data); // Call function to populate related values in the second dropdown
        },
        error: function () {
            // Handle error
            console.log('Error occurred while fetching related values.');
        }
    });
}

function DropDownStyle(data) {
    var select = $('#stylelist'); // Assuming the ID of your second dropdown is 'voiceselect'
    select.empty(); // Clear previous options

    // Check if the dropdown is empty
    if (select.children().length === 0) {
        // Append a default option with some text (replace 'Select a value' with your desired default text)
        select.append($('<option></option>').attr('value', 'general').text('General').prop('selected', true));
    }

    $.each(data, function (index, item) {
        var optionText = item.styleName;
        select.append($('<option></option>').attr('value', item.styleName).text(optionText));
    });
}
//function getEditOnLoad(Value,id) {
//    var count = $("#hdnlanguageCount_").val();
//    if (count != null && count != undefined) {
//        for (var i = 1; i <= count; i++)
//        {
//            EditDropDown($('#hdnlanguage_' + (i)).val(), Value);
//        }
//    }
//}

//function EditDropDown(selectedLanguage, id) {
//    var dataa = selectedLanguage;
//    getEditAccent(id, selectedLanguage);
//    getEditVoice(id, selectedLanguage);
//}

function getEditAccent(selectedLanguage, id) {
    var Language = $("#dictionaryLanguage_" + id).val();
    $.ajax({
        url: '/Pronunciation/GetAccent',
        type: 'GET',
        data: { language: Language }, // Pass selected language as parameter
        contentType: 'application/json',
        success: function (data) {
            console.log("Received related values:", data);
            editAccentDropDown(id, data); // Call function to populate related values in the second dropdown
        },
        error: function () {
            // Handle error
            console.log('Error occurred while fetching related values.');
        }
    });
}

function getEditVoice(selectedLanguage, id) {
    var Accent = $("#accent_" + id).val();
    $.ajax({
        url: '/Pronunciation/GetVoice',
        type: 'GET',
        data: { language: Accent }, // Pass selected language as parameter
        contentType: 'application/json',
        success: function (data) {
            console.log("Received related values:", data);
            editVoiceDropDown(id, data); // Call function to populate related values in the second dropdown
        },
        error: function () {
            // Handle error
            console.log('Error occurred while fetching related values.');
        }
    });
}
function editAccentDropDown(id, data) {
    var select = $('#accent_' + id); // Assuming the ID of your second dropdown is 'voiceselect'
    select.empty(); // Clear previous options

    // Check if the dropdown is empty
    if (select.children().length === 0) {
        // Append a default option with some text (replace 'Select a value' with your desired default text)
        select.append($('<option></option>').attr('value', '').text('Select Accent').prop('selected', true));
    }

    $.each(data, function (index, item) {
        var optionText = item.localeName;
        select.append($('<option></option>').attr('value', item.locale).text(optionText));
    });
}

function editVoiceDropDown(id, data) {
    var select = $('#voice_' + id); // Assuming the ID of your second dropdown is 'voiceselect'
    select.empty(); // Clear previous options

    // Check if the dropdown is empty
    if (select.children().length === 0) {
        // Append a default option with some text (replace 'Select a value' with your desired default text)
        select.append($('<option></option>').attr('value', '').text('Select Voice').prop('selected', true));
    }

    $.each(data, function (index, item) {
        //var optionText = item.displayName;
        var optionText = item.displayName + ' (' + item.voiceType + ')';
        select.append($('<option></option>').attr('value', item.shortName).text(optionText));
    });
}

function getLanguageOnLoad() {
    var count = $("#hdnlanguageCount_").val();
    //$("#divLoader").append(getLoader());
    if (count != null && count != undefined) {
        for (var i = 1; i <= count; i++) {
            getLanguage($('#hdnlanguage_' + (i)).val(), $('#hdnlanguagevalues_' + (i)).val());
            getVoice($('#hdnlanguage_' + (i)).val(), $('#hdnVoicevalues_' + (i)).val());
            getAccent($('#hdnlanguage_' + (i)).val(), $('#hdnAccentvalues_' + (i)).val());
        }
        setTimeout(function () {
            //removeLoader("#divLoader");
        }, 2000);
    }
}

function getLanguage(id, selID) {
    var lan = $('#dictionaryLanguage_' + id).val();

    $("#divLoader").append(getLoader());
    $.ajax({
        url: '/Pronunciation/GetLanguage',
        data: { type: 'Language' },
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (data) {
            if (data) {
                var Language = data;
                // $('#selDiscount_' + id + ' option').remove();

                $('#dictionaryLanguage_' + id + ' option').remove();
                $('#dictionaryLanguage_' + id).append('<option selected value=0>Select Language</option>');

                for (counter in Language) {
                    if (selID == Language[counter].locale)
                        $('#dictionaryLanguage_' + id).append('<option selected value="' + Language[counter].locale + '">' + Language[counter].localeName + '</option>');
                    else
                        $('#dictionaryLanguage_' + id).append('<option value="' + Language[counter].locale + '">' + Language[counter].localeName + '</option>');
                }
            }
            else if (!data) {
            }
        },
        complete: function () {
            setTimeout(function () {
                removeLoader("#divLoader");
            }, 500);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            setTimeout(function () {
            }, 300);
            console.log(xhr.error().statusText);
        }
    });
}

function getAccent(id, selID) {
    var ladn = $('#accent_' + id).val();
    //$("#divLoader").append(getLoader());
    $.ajax({
        url: '/Pronunciation/GetLanguage',
        data: { type: 'Accent' },
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        async: false,
        success: function (data) {
            if (data) {
                var Accent = data;
                // $('#selDiscount_' + id + ' option').remove();

                $('#accent_' + id + ' option').remove();
                $('#accent_' + id).append('<option selected value=0>Select Accent</option>');

                //select.append($('<option></option>').attr('value', item.shortName).text(optionText));

                for (counter in Accent) {
                    if (selID == Accent[counter].accentCode)
                        $('#accent_' + id).append('<option selected value="' + Accent[counter].accentCode + '">' + Accent[counter].accent + '</option>');
                    else
                        $('#accent_' + id).append('<option value="' + Accent[counter].accentCode + '">' + Accent[counter].accent + '</option>');
                }
            }
            else if (!data) {
            }
        },
        complete: function () {
            setTimeout(function () {
                //removeLoader("#divLoader");
            }, 300);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            setTimeout(function () {
            }, 300);
            console.log(xhr.error().statusText);
        }
    });
}

function getVoice(id, selID) {
    var ladn = $('#voice_' + id).val();
    //$("#divLoader").append(getLoader());
    $.ajax({
        url: '/Pronunciation/GetLanguage',
        data: { type: 'Voice' },
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        async: false,
        success: function (data) {
            if (data) {
                var Voice = data;
                // $('#selDiscount_' + id + ' option').remove();

                $('#voice_' + id + ' option').remove();
                $('#voice_' + id).append('<option selected value=0>Select Voice</option>');

                //select.append($('<option></option>').attr('value', item.shortName).text(optionText));

                for (counter in Voice) {
                    var optionText = Voice[counter].displayName;
                    if (selID == Voice[counter].shortName)
                        $('#voice_' + id).append('<option selected value="' + Voice[counter].shortName + '">' + optionText + '</option>');
                    else
                        $('#voice_' + id).append('<option value="' + Voice[counter].shortName + '">' + optionText + '</option>');
                }
            }
            else if (!data) {
            }
        },
        complete: function () {
            setTimeout(function () {
                //removeLoader("#divLoader");
            }, 300);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            setTimeout(function () {
            }, 300);
            console.log(xhr.error().statusText);
        }
    });
}

function SavePronunciation(formName) {
    var fdata = new FormData();

    var msg = "";
    var isValid = true;

    if (formName == "frmAddPronunciation") {
        var yourWordInput = $(".containerPronunciationFirst").find(".firstYourWord").val();
        if (yourWordInput === null || yourWordInput.trim() === "") {
            msg = msg + "Your Word";
            isValid = false;
        }
        var alternateSpellingInput = $(".containerPronunciationFirst").find(".firstAlternateSpelling").val();
        if (alternateSpellingInput === null || alternateSpellingInput.trim() === "") {
            msg = msg + "\n" + "Alternate Spelling";
            isValid = false;
        }
        var firstLangInput = $(".containerPronunciationFirst").find(".firstLang").val();
        if (firstLangInput == '') {
            msg = msg + "\n" + "Language";
            isValid = false;
        }
        var firstAccentInput = $(".containerPronunciationFirst").find(".firstAccent").val();
        if (firstAccentInput == '' || firstAccentInput == 0) {
            msg = msg + "\n" + "Accent";
            isValid = false;
        }
        //var firstVoiceInput = $(".containerPronunciationFirst").find(".firstVoice").val();
        //if (firstVoiceInput == '' || firstVoiceInput == 0) {
        //    msg = msg + "\n" + "Voice";
        //    isValid = false;
        //}
    }
    else {
        isValid = true;
    }

    if (isValid) {
        // input value
        if (formName == "frmAddPronunciation") {
            $('#frmAddPronunciation input').each(function () {
                if ($(this).attr('id') != undefined) {
                    fdata.append($(this).attr('id'), $(this).val());
                }
            });
            $('#frmAddPronunciation select').each(function () {
                if ($(this).attr('id') !== undefined) {
                    var selectId = $(this).attr('id');
                    var selectValue = $(this).val();
                    var selectText = $(this).find('option:selected').text(); // Get the text of the selected option
                    fdata.append(selectId + '_value', selectValue);
                    fdata.append(selectId + '_text', selectText);
                }
            });
            fdata.append('SaveEdit', 1)
        }
        if (formName == "frmEditPronunciation") {
            $('#frmEditPronunciation input').each(function () {
                if ($(this).attr('id') != undefined) {
                    fdata.append($(this).attr('id'), $(this).val());
                }
            });
            $('#frmEditPronunciation select').each(function () {
                if ($(this).attr('id') !== undefined) {
                    var selectId = $(this).attr('id');
                    var selectValue = $(this).val();
                    var selectText = $(this).find('option:selected').text(); // Get the text of the selected option
                    fdata.append('value' + selectId, selectValue);
                    fdata.append('text' + selectId, selectText);
                }
            });
            fdata.append('SaveEdit', 2)
        }

        $("#divLoader").append(getLoader());

        $.ajax({
            url: window.location.origin + '/Pronunciation/PronunciationSave',
            data: fdata,
            type: "POST",
            processData: false,
            contentType: false,
            success: function (data) {
                if (data.isSuccess) {
                    BootstrapDialog.show({
                        id: "divAdduser",
                        title: __SUCCESS,
                        type: getDialogType(__SUCCESS),
                        message: function () {
                            var $message = $('<div id="divAdduserInner"></div>');
                            $message.append(data.message);
                            return $message;
                        },
                        closeByBackdrop: false,
                        buttons: [
                            {
                                label: 'Ok',
                                cssClass: 'btn-primary',
                                action: function (dialogItself) {
                                    window.location.href = "/Pronunciation/Index";
                                }
                            }
                        ],
                        onshown: function (dialogRef) {
                            //
                        }
                    });
                }
                else if (!data.isSuccess) {
                    //showBSAlert(data.messageCaption, data.message, __error);
                    BootstrapDialog.show({
                        id: "divSaveStudent",
                        title: __WARNING,
                        type: getDialogType(__WARNING),
                        message: function () {
                            var $message = $('<div id="divSaveStudentInner"></div>');
                            $message.append(data.message);
                            return $message;
                        },
                        closeByBackdrop: false,
                        buttons: [{
                            label: __ok,
                            cssClass: 'btn btn-primary',
                            action: function (dialogItself) {
                                window.location.href = "/Pronunciation/Index";
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
                setTimeout(function () {
                }, 300);
                console.log(xhr.error().statusText);
            }
        });
    }
    else if (msg != "") {
        showBSAlert(__requiredCaption, msg, __WARNING);
    }
    msg = "";
}

function editSaveRow(id, type) {
    if (type == 'Edit') {
        $("#btnEdit_" + id).hide();
        $("#btnSave_" + id).show();
        //$("#editRow_" + id).find("input").removeClass("disabledCustom");
        //$("#editRow_" + id).find("select").removeClass("disabledCustom");
        $("#editRow_" + id).find(".alternateText").removeClass("disabledCustom");
    }
    else if (type == 'Save') {
        $("#btnEdit_" + id).show();
        $("#btnSave_" + id).hide();
        //$("#editRow_" + id).find("input").addClass("disabledCustom");
        //$("#editRow_" + id).find("select").addClass("disabledCustom");
        $("#editRow_" + id).find(".alternateText").addClass("disabledCustom");
        $("#type_" + id).val(1);
        SavePronunciation('frmEditPronunciation');
    }
    else {
        $("#editRow_" + id).remove();
    }
}