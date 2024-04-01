//  serialize the form data into an object.
$.fn.serializeObject = function () {
    var formData = {};
    var formArray = this.serializeArray();
    $.each(formArray, function () {
        formData[this.name] = this.value;
    });
    return formData;
};

/*const { parseJSON } = require("jquery");*/
var arrCityNames = [];
var arrCityIDs = [];
$(document).ready(function () {
    $(".parentCheckbox").each(function (index, item) {
        if ($(this).is(':checked')) {
            var thisId = $(this).attr("id");
            var parentId = $("#" + thisId).closest(".parentHolder").attr("id");
            $("#" + parentId).find(".onPrimaryShow").css("display", "inline-block");
        }
    });
    numberOnly();
});

const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});


$.validator.addMethod("emailonly",
    function (value) {
        if ($.trim(value) == '')
            return true;
        else
            return /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(value);
    }
);

UserProfile = function (data, frm, mode) {
    pageName = frm ? frm.toLowerCase() : "";
    mode = mode ? mode.toLowerCase() : "";
    var pageurl;
    pageurl = "/User/Save/";

    var self = this;
    ko.mapping.fromJS(data, {}, self);

    self.save = function () {
        SaveForm(pageurl, frm, mode);
    };
}

function SaveForm(pageurl, frm, mode) {
    SaveSchool()
}

function SubmitForm(formName) {
    var msg = "";
    var isValid = true;
    var validator_count = 0;


    if (formName == "frmAddUser") {
        var firstNameInput = $("#FirstName").val();
        if (firstNameInput === null || firstNameInput.trim() === "") {
            msg = msg + "First Name";
            isValid = false;
        }
        var lastNameInput = $("#LastName").val();
        if (lastNameInput === null || lastNameInput.trim() === "") {
            msg = msg + "\n" + "Last Name";
            isValid = false;
        }
        var userNameInput = $("#UserName").val();
        if (userNameInput === null || userNameInput.trim() === "") {
            msg = msg + "\n" + "Email";
            isValid = false;
        }
        else if (!emailOnly($("#UserName").val().trim())) {
            msg = msg + "\n" + "Email";
            isValid = false;
        }

    }
    if (formName == "frmSaveVoice") {
        var firstNameInput = $("#VMName").val();
        if (firstNameInput === null || firstNameInput.trim() === "") {
            msg = msg + "Name ";
            isValid = false;
        }
        var genderSelect = $("#genderSelect").val();
        if (genderSelect == 0 || genderSelect == null) {
            msg = msg + "\n" + "Gender";
            isValid = false;
        }
        var ageSelect = $("#ageSelect").val();
        if (ageSelect == 0 || ageSelect == null) {
            msg = msg + "\n" + "Age";
            isValid = false;
        }
        var displayNameInput = $("#DisplayName").val();
        if (displayNameInput === null || displayNameInput.trim() === "") {
            msg = msg + "\n" + "Display Name";
            isValid = false;
        }
        //var localeInput = $("#Locale").val();
        //if (localeInput === null || localeInput.trim() === "") {
        //    msg = msg + "\n" + "Locale";
        //    isValid = false;
        //}
        var localNameInput = $("#LocalName").val();
        if (localNameInput === null || localNameInput.trim() === "") {
            msg = msg + "\n" + "Locale Name";
            isValid = false;
        }
        //var shortNameInput = $("#ShortName").val();
        //if (shortNameInput === null || shortNameInput.trim() === "") {
        //    msg = msg + "\n" + "Short Name";
        //    isValid = false;
        //}
        var sampleURLInput = $("#SampleURL").val();
        if (sampleURLInput === null || sampleURLInput.trim() === "") {
            msg = msg + "\n" + "Sample URL";
            isValid = false;
        }
        else {
            var sampleURLInput = $("#SampleURL").val();
            if (sampleURLInput.indexOf("https://") == -1 || sampleURLInput.indexOf(".mp3") == -1) {
                msg = msg + "\n" + "Require mp3 URL like : https://xyz/audio.mp3";
                isValid = false;
            }
        }

        var voiceProviderInput = $("#VoiceProvider").val();
        if (voiceProviderInput === null || voiceProviderInput.trim() === "") {
            msg = msg + "\n" + "Voice Provider";
            isValid = false;
        }
        var characterLimitInput = $("#CharacterLimit").val();
        if (characterLimitInput === null || characterLimitInput.trim() === "") {
            msg = msg + "\n" + "Character Limit";
            isValid = false;
        }
        var languageInput = $("#Language").val();
        if (languageInput === null || languageInput.trim() === "") {
            msg = msg + "\n" + "Language";
            isValid = false;
        }
        var sampleRateHertzInput = $("#SampleRateHertz").val();
        if (sampleRateHertzInput === null || sampleRateHertzInput.trim() === "") {
            msg = msg + "\n" + "Sample Rate Hertz";
            isValid = false;
        }
        var voiceTypeInput = $("#VoiceType").val();
        if (voiceTypeInput === null || voiceTypeInput.trim() === "") {
            msg = msg + "\n" + "Voice Type";
            isValid = false;
        }
        var statusInput = $("#Status").val();
        if (statusInput === null || statusInput.trim() === "") {
            msg = msg + "\n" + "Status";
            isValid = false;
        }
        var wordsPerMinuteInput = $("#WordsPerMinute").val();
        if (wordsPerMinuteInput === null || wordsPerMinuteInput.trim() === "") {
            msg = msg + "\n" + "Words Per Minute";
            isValid = false;
        }
        var languageSelect = $("#selectLanguage").val();
        if (languageSelect == 0) {
            msg = msg + "\n" + "Language";
            isValid = false;
        }
        /*var tagsSelect = $(".fSelectSelectTags").find(".fs-label-wrap").find(".fs-label").text();
        if (tagsSelect == "Select") {
            msg = msg + "\n" + "Tag";
            isValid = false;
        }
        var tagsSelect = $(".fSelectSelectBestFor").find(".fs-label-wrap").find(".fs-label").text();
        if (tagsSelect == "Select") {
            msg = msg + "\n" + "Best For";
            isValid = false;
        }
        var voiceImageFile = $("#VoiceImage").val();
        var voiceImageSrc = $("#user_image").attr("src");
        if (voiceImageFile == "" || voiceImageSrc == "") {
            msg = msg + "\n" + "Photo";
            isValid = false;
        }*/
        var discriptionInput = $("#Discription").val();
        if (discriptionInput === null || discriptionInput.trim() === "") {
            msg = msg + "\n" + "Description";
            isValid = false;
        }

    }


    if (isValid) {
        if (formName == "frmAddUser") {
            SaveUsers(); 
        }
        if (formName == "frmSaveVoice") {
            SaveVoice(formName);
        }
    }
    else if (msg != "") {
        showBSAlert(__requiredCaption, msg, __WARNING);
    }
    msg = "";
    validator_count = 0;
}

// Dont write save method is here 
function SaveUsers(formName, frmType) {
    
    var fdata = new FormData();

    // input value
    $('#frmAddUser input').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $(this).val());
        }
    });

    // text area value
    $('#frmAddUser textarea').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $('textarea#' + $(this).attr('id')).val());
        }
    });


    $("#divLoader").append(getLoader());
    
    $.ajax({
        url: window.location.origin + '/Admin/SaveUser',
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
                    buttons: [{                    
                        label: __ok,
                        cssClass: 'btn btn-primary',
                        action: function (dialogItself) {
                            location.href = "/Admin/Index";
                            dialogItself.close();
                        }
                    }],
                    onshown: function () {
                        console.log("IsEdit: " + IsEdit);
                        if (IsEdit == true) {
                            $(".btnContinueToAddNew").css({ "display": "none" });
                        }
                        else {
                            $(".btnContinueToAddNew").css({ "display": "inline-block" });
                        }
                    }
                });
            }

            else if (!data.isSuccess) {
                //showBSAlert(data.messageCaption, data.message, __error);
                BootstrapDialog.show({
                    id: "divSaveStudent",
                    title: __SUCCESS,
                    type: getDialogType(__SUCCESS),
                    message: function () {
                        var $message = $('<div id="divSaveStudentInner"></div>');
                        $message.append(data.message);
                        return $message;
                    },
                    closeByBackdrop: false,
                    buttons: [{
                        label: __ok,
                        cssClass: 'btn btn-primary',
                        action: function (dialog) {
                            location.reload(true);
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



if ($('.studentDetails_BtnPrimary').find('input.form-control').is(':checked')) {
    $(this).closest('div');
}

function showManStars(_id) {
    $(".onPrimaryShow").not($("#" + _id).find(".onPrimaryShow").css("display", "inline-block")).css("display", "none");
}



function showBSAlert(title, message, type, isClosable, redirectUrl, isBack) {
    BootstrapDialog.show({
        id: "divBSAlert",
        title: title,
        message: message,
        closable: isClosable == undefined ? true : isClosable,
        type: getDialogType(type),
        buttons: [
            {
                id: 'btnBSAlertOk',
                label: 'Ok',
                cssClass: 'btn-primary',
                hotkey: 13,
                action: function (dialogItself) {
                    dialogItself.close();
                    if (redirectUrl != undefined && redirectUrl != null && redirectUrl != "")
                        window.location = redirectUrl;
                    else if (isBack)
                        history.back();
                }
            }]
    });
}

function encodeImageFileAsURL(element, HiddenFieldId, counter) {
    var file = element.files[0];
    var reader = new FileReader();

    var filename = element.files[0].name
    var extension = element.files[0].type.toString().replace(/(.*)\//g, '');

    //alert("#" + HiddenFieldId + counter);

    reader.onloadend = function () {
        var base64result = reader.result.split(',')[1];

        //console.log('RESULT', reader.result);

        $("#" + HiddenFieldId + counter).val(base64result);
        $("#" + HiddenFieldId + "name_" + counter).val(filename);
        $("#" + HiddenFieldId + "extention_" + counter).val(extension);
    }
    reader.readAsDataURL(file);
}



function setInputFilter(textbox, inputFilter, errMsg) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop", "focusout"].forEach(function (event) {
        textbox.addEventListener(event, function (e) {
            if (inputFilter(this.value)) {
                // Accepted value
                if (["keydown", "mousedown", "focusout"].indexOf(e.type) >= 0) {
                    this.classList.remove("input-error");
                    this.setCustomValidity("");
                }
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                // Rejected value - restore the previous one
                this.classList.add("input-error");
                this.setCustomValidity(errMsg);
                this.reportValidity();
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            } else {
                // Rejected value - nothing to restore
                this.value = "";
            }
        });
    });
}


function imgSizeAndResolution(_id) {
    var _URL = window.URL || window.webkitURL;
    //$("#schoolStamp").change(function (e) {
    var file, img;

    if ((file = $("#" + _id).get(0).files[0])) {
        img = new Image();
        img.onload = function () {
            if (this.width > 600 || this.height > 600) {
                $($("#" + _id)).val("");               
                $("#" + _id).parent().parent().find(".imgLogo").attr("src", "");
                showBSAlert(__requiredCaption, "Image resolution should be 600 x 600 px or less.<br />Uploaded image resolution is " + this.width + " x " + this.height + " px.", __WARNING);
            }
        };
        img.onerror = function () {
            $($("#" + _id)).val("");
            //$("#" + _id).parent().parent().find(".imgLogo").attr("src", "/images/no-logo.jpg");
            $("#" + _id).parent().parent().find(".imgLogo").attr("src", "");
            showBSAlert(__requiredCaption, "Invalid file format: " + file.type, __WARNING);
        };
        img.src = _URL.createObjectURL(file);
    }
    //});
}


function removeRow(_thisRecord) {
    $("#" + _thisRecord).remove();
}

function numberOnly() {
    $('.numberonly').keypress(function (e) {
        var charCode = (e.which) ? e.which : e.keyCode;
        /*if (String.fromCharCode(charCode).match(/[^0-9]/g)) {
            return false;
        }*/
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
    });
}