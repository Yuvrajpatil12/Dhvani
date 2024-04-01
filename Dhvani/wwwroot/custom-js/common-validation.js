// user edit check user name exist or not
function IsUsernameExistEditPopUp(input_id, editusername) {
    var username = $("#" + input_id).val();
    var isCheck = true;
    if (editusername == '')
        isCheck = true;
    else if (username == editusername)
        isCheck = false;

    if (isCheck && username && username != undefined && username != null) {
        $.ajax({
            url: window.location.origin + '/Users/IsUsernameExistEditClick?username=' + username.toString(),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.isSuccess) {
                    //$('#' + input_id).prop("disabled", true);
                }
                else if (!data.isSuccess) {
                    $('#' + input_id).prop("disabled", false);
                    //showModal(input_id);
                    BootstrapDialog.show({
                        id: "divSaveStudent",
                        title: __WARNING,
                        type: getDialogType(__WARNING),
                        message: function () {
                            var $message = $('<div id="divSaveStudentInner"></div>');
                            $message.append(data.message);
                            return $message;
                            //showModal($("#" + input_id).val().toString);
                        },
                        closeByBackdrop: false,
                        closable: false,
                        buttons: [{
                            label: __ok,
                            cssClass: 'btn btn-primary',
                            action: function (dialogItself) {
                                //location.reload(true);
                                dialogItself.close();
                                $('#' + input_id).val("");
                                var id = input_id.split("_")[1];
                                $('#alternateEmailAddress_' + id).val("");
                            }
                        }],
                        onshown: function () {
                            if (isEmailNotSame == true) {
                                $('#divBSAlert .close').click();
                            }
                        }
                    });
                }
            },
            complete: function () {
                setTimeout(function () {
                    //
                }, 300);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                setTimeout(function () {
                }, 300);
                console.log(xhr.error().statusText);
            }
        });
    }
    else {
    }
}

function IsUsernameExistPopUp(input_id, editusername) {
    var username = $("#" + input_id).val();
    var isCheck = true;
    if (editusername == '')
        isCheck = true;
    else if (username == editusername)
        isCheck = false;

    if (isCheck && username && username != undefined && username != null) {
        $.ajax({
            url: window.location.origin + '/Users/IsUsernameExistClick?username=' + username.toString(),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.isSuccess) {
                    //$('#' + input_id).prop("disabled", true);
                }
                else if (!data.isSuccess) {
                    $('#' + input_id).prop("disabled", false);
                    //showModal(input_id);
                    BootstrapDialog.show({
                        id: "divSaveStudent",
                        title: __WARNING,
                        type: getDialogType(__WARNING),
                        message: function () {
                            var $message = $('<div id="divSaveStudentInner"></div>');
                            $message.append(data.message);
                            return $message;
                            //showModal($("#" + input_id).val().toString);
                        },
                        closeByBackdrop: false,
                        closable: false,
                        buttons: [{
                            label: __ok,
                            cssClass: 'btn btn-primary',
                            action: function (dialogItself) {
                                //location.reload(true);
                                dialogItself.close();
                                $('#' + input_id).val("");
                                var id = input_id.split("_")[1];
                                $('#alternateEmailAddress_' + id).val("");
                            }
                        }],
                        onshown: function () {
                            if (isEmailNotSame == true) {
                                $('#divBSAlert .close').click();
                            }
                        }
                    });
                }
            },
            complete: function () {
                setTimeout(function () {
                    //
                }, 300);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                setTimeout(function () {
                }, 300);
                console.log(xhr.error().statusText);
            }
        });
    }
    else {
    }
}



