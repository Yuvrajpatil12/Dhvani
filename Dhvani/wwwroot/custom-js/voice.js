
function SaveVoice(formName) {

    var formData = new FormData();
  
 

    //// Get the file input element
    var fileInput = document.getElementById('VoiceImage');

    $('#frmAddUser input').each(function () {
        if ($(this).attr('id') != undefined) {
            formData.append($(this).attr('id'), $(this).val());
        }
    });

    var descriptionInput = $('#Discription').val();
    formData.append('Discription', descriptionInput);

    // Check if any file is selected
    if (fileInput.files.length > 0) {
        // Append the file to the FormData object
        formData.append('VoiceImage', fileInput.files[0]);
    }



    //====================
    // Drop down value
    //====================

    // Gender dropdown value
    var selectedGender = $('#selectedGenderValue').val();
    formData.append('SelectedGender', selectedGender);

    // Age dropdown value
    var selectedAge = $('#selectedAge').val();
    formData.append('SelectedValueAge', selectedAge);

    //====================
    // Multi Select value 
    //====================

    var selectedTags = [];

    $(".fSelectSelectTags").find(".fs-dropdown .fs-option").each(function () {
        if ($(this).hasClass("selected")) {
            var selectedValue = $(this).data("value");
            if (selectedTags.indexOf(selectedValue) === -1) {
                selectedTags.push(selectedValue);
            }
        }
    });
    formData.append("SelectedTags", selectedTags);

    var selectedBestFor = [];

    $(".fSelectSelectBestFor").find(".fs-dropdown .fs-option").each(function () {
        if ($(this).hasClass("selected")) {
            var selectedValue = $(this).data("value");
            if (selectedBestFor.indexOf(selectedValue) === -1) {
                selectedBestFor.push(selectedValue);
            }
        }
    });
    formData.append("SelectedBestFor", selectedBestFor);

    $("#divLoader").append(getLoader());

    $.ajax({
        url: window.location.origin + '/VoiceMapper/SaveVoice',
        data: formData,
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

// Search drop down value list 
function getTagMasterList(voiceMasterID) {

    $('#searchTags').empty();
    $('.fs-dropdown').remove();

    // Construct the URL with the parameter
    var url = '/VoiceMapper/GetTagMasterList?voiceMasterID=' + encodeURIComponent(voiceMasterID);

    $.ajax({
        url: window.location.origin + url,
        type: "GET",
        cache: false,
        success: function (data) {


            var tagMasterValues = data.tagMasterValues;

            // Clear the dropdown before appending options
            $('#searchTags').empty();

            // Iterate through tagMasterValues and append options to the dropdown
            tagMasterValues.forEach(function (tag) {
                var option = $('<option></option>').attr('value', tag.id).text(tag.tagName);
                $('#searchTags').append(option);
            });

            // Check if data.vmTags is defined and if it has at least one element
            if (data.vmTags && data.vmTags.length > 0) {
                // Iterate over each element in data.vmTags
                data.vmTags.forEach(function (vmTag) {
                    // Find the option corresponding to the tagMasterID in vmTag
                    var option = $('#searchTags option[value="' + vmTag.tagMasterID + '"]');

                    // If the option exists, mark it as selected
                    if (option.length) {
                        option.prop('selected', true);
                    }
                });
            }

            // Initialize the fSelect plugin after appending options and setting selected options
            window.fs_test = $('.searchTags').fSelect();


        },
        complete: function () {
            setTimeout(function () {
                removeLoader("#divLoader");
                $('.searchTags').fSelect('reload');
            }, 300);
            setTimeout(function () {
                if ($(".fs-option").length > 0) {
                    $('.fSelectSelectTags').find(".fs-dropdown .fs-options").prepend('<span id="btnSelectAll" class="btn btn-sm btn-primary" style="margin: 7px; color: #000;">Select All</span> <span id="btnUnSelectAll" class="btn btn-sm btn-default" style="margin: 7px 7px 7px 0px; color: #000; float: right">Unselect All</span>');
                }
            }, 500);
            setTimeout(function () {

                $("#btnSelectAll").click(function () {
                    $(".fSelectSelectTags").find(".fs-options").find(".fs-option").addClass("selected");

                    var sList = "";
                    var sText = "";
                    $(".fSelectSelectTags").find(".fs-dropdown").find(".fs-option.selected").each(function (index, val) {
                        sList += $(this).attr("data-value") + ",";
                        sText += $(this).find(".fs-option-label").text() + ",";
                    });
                    sList = sList.substring(0, sList.length - 1);
                    sText = sText.substring(0, sText.length - 1);

                    $(".fSelectSelectTags").find(".fs-label-wrap").find(".fs-label").text(sText);

                });
                $("#btnUnSelectAll").click(function () {
                    $(".fSelectSelectTags").find(".fs-options").find(".fs-option").removeClass("selected");

                    $("#BulkUpload_ClassID_D option").removeClass("selected");
                    $(".fSelectSelectTags").find(".fs-label-wrap").find(".fs-label").text("Select");

                });
            }, 2000);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            setTimeout(function () {
            }, 300);
            console.log(xhr.error().statusText);
        }
    });

}

// Search drop down value list for bestformaster
function getBestForMasterList(voiceMasterID) {

       

    $('#searchBestFor').empty();
    $('.fs-dropdown').remove();

    // Construct the URL with the parameter
    var url = '/VoiceMapper/GetBestForMasterList?voiceMasterID=' + encodeURIComponent(voiceMasterID);

    $.ajax({
        url: window.location.origin + url,
        type: "GET",
        cache: false,
        success: function (data) {

            var bestForMasterValues = data.bestForMaster;

            // Clear the dropdown before appending options
            $('#searchBestFor').empty();

            // Iterate through tagMasterValues and append options to the dropdown
            bestForMasterValues.forEach(function (bestfor) {
                var option = $('<option></option>').attr('value', bestfor.id).text(bestfor.bestForName);
                $('#searchBestFor').append(option);
            });

            // Check if data.vmTags is defined and if it has at least one element
            if (data.vmBestFor && data.vmBestFor.length > 0) {
                // Iterate over each element in data.vmTags
                data.vmBestFor.forEach(function (vmBestForValue) {
                    // Find the option corresponding to the tagMasterID in vmTag
                    var option = $('#searchBestFor option[value="' + vmBestForValue.bestForMasterID + '"]');

                    // If the option exists, mark it as selected
                    if (option.length) {
                        option.prop('selected', true);
                    }
                });
            }
            
            
            window.fs_test = $('.searchBestFor').fSelect();



        },
        complete: function () {
            setTimeout(function () {
                removeLoader("#divLoader");
                $('.searchBestFor').fSelect('reload');
            }, 300);
            setTimeout(function () {
                if ($(".fs-option").length > 0) {
                    $('.fSelectSelectBestFor').find(".fs-dropdown").prepend('<span id="btnBestForSelectAll" class="btn btn-sm btn-primary" style="margin: 7px; color: #000;">Select All</span> <span id="btnBestForUnSelectAll" class="btn btn-sm btn-default" style="margin: 7px 7px 7px 0px; color: #000; float: right">Unselect All</span>');
                }
            }, 500);
            setTimeout(function () {

                $("#btnBestForSelectAll").click(function () {
                    //$(".fSelectSelectBestFor").find(".fs-options-BestFor").find(".fs-option").addClass("selected");
                    $(".fSelectSelectBestFor").find(".fs-option").addClass("selected");

                    var sList = "";
                    var sText = "";
                    $(".fSelectSelectBestFor").find(".fs-dropdown").find(".fs-option.selected").each(function (index, val) {
                        sList += $(this).attr("data-value") + ",";
                        sText += $(this).find(".fs-option-label").text() + ",";
                    });
                    sList = sList.substring(0, sList.length - 1);
                    sText = sText.substring(0, sText.length - 1);

                    $(".fSelectSelectBestFor").find(".fs-label-wrap").find(".fs-label").text(sText);

                });
                $("#btnBestForUnSelectAll").click(function () {
                    $(".fSelectSelectBestFor").find(".fs-option").removeClass("selected");

                    $("#BulkUpload_ClassID_D option").removeClass("selected");
                    $(".fSelectSelectBestFor").find(".fs-label-wrap").find(".fs-label").text("Select");

                });
            }, 2000);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            setTimeout(function () {
            }, 300);
            console.log(xhr.error().statusText);
        }
    });

}

// write the post request to get the user details base on userid

function showUserDetails() {
    
    $.ajax({
        url: '/VoiceMapper/GetUserDetails', //***** from controller (Inside GetSentenceList called Partial View _ListSentencePartial)
        data: JSON.stringify({ ID: 1 }),
        type: "POST",
        cache: false,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            BootstrapDialog.show({
                id: "divUserDetail",
                title: "User Details",
                size: BootstrapDialog.SIZE_WIDE,
                message: function () {
                    var $message = $('<div id="divUserDetailInnser" class="pTB10-LR05"></div>');
                    $message.append(result);
                    return $message;
                },
                closeByBackdrop: false,
                closable: true,
  
                onshown: function (dialogRef) {
                    //
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    })
}
