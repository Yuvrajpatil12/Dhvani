function SetUserId(Id) {
    $("#UserID").val(Id);
    var data = $("#UserID").val();
}

function SaveVoiceToUser(id) {
    var fdata = new FormData();
    var userid = $("#hdnUserID").val();
    var Check = $("#CheckedTopics_" + id).prop('checked') ? 1 : 0;
    var VoiceMasterID = $("#VoiceMasterID_" + id).val();
    var VoiceMasterUserMapID = $("#VoiceMasterUserMapID_" + id).val();

    fdata.append("UserID", userid);
    fdata.append("Check", Check);
    fdata.append("VoiceMasterID", VoiceMasterID);
    fdata.append("VoiceMasterUserMapID", VoiceMasterUserMapID);

    $("#divVoiceListInner").append(getLoader());

    $.ajax({
        url: window.location.origin + '/AddVoiceToUser/AddVoiceToUsers',
        data: fdata,
        type: "POST",
        processData: false,
        contentType: false,
        //cache: false,
        success: function (data) {
            //if (!data.isSuccess) {
            //    BootstrapDialog.show({
            //        id: "divAdduser",
            //        title: __SUCCESS,
            //        type: getDialogType(__SUCCESS),
            //        message: function () {
            //            var $message = $('<div id="divAdduserInner"></div>');
            //            $message.append("Data save successfully");
            //            return $message;
            //        },
            //        closeByBackdrop: false,
            //        buttons: [
            //            {
            //                label: 'Ok',
            //                cssClass: 'btn-primary',
            //                action: function (dialogItself) {
            //                    window.location.href = "/AddVoiceToUser/Index";
            //                }
            //            }
            //        ],
            //        onshown: function (dialogRef) {
            //            //
            //        }
            //    });
            //}
            //else if (data.isSuccess) {
            //    //showBSAlert(data.messageCaption, data.message, __error);
            //    BootstrapDialog.show({
            //        id: "divSaveStudent",
            //        title: __WARNING,
            //        type: getDialogType(__WARNING),
            //        message: function () {
            //            var $message = $('<div id="divSaveStudentInner"></div>');
            //            $message.append("Error");
            //            return $message;
            //        },
            //        closeByBackdrop: false,
            //        buttons: [{
            //            label: __ok,
            //            cssClass: 'btn btn-primary',
            //            action: function (dialogItself) {
            //                window.location.href = "/AddVoiceToUser/Index";
            //            }
            //        }]
            //    });
            //}
        },
        complete: function () {
            setTimeout(function () {
                removeLoader("#divVoiceListInner");
            }, 2000);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //$("#divLoginBox").removeClass("box");
            setTimeout(function () {
                //removeLoader("#divLoader");
            }, 300);
            // alert(xhr.error())
        }
    });
}

function SaveAllVoiceToUser(id) {
    var fdata = new FormData();
    var userid = $("#hdnUserID").val();
    var Check = $("#select_all_voices").prop('checked') ? 1 : 0;
    // var VoiceMasterID = $("#VoiceMasterID_" + id).val();
    // var VoiceMasterUserMapID = $("#VoiceMasterUserMapID_" + id).val();

    fdata.append("UserID", userid);
    fdata.append("Check", Check);
    // fdata.append("VoiceMasterID", VoiceMasterID);
    // fdata.append("VoiceMasterUserMapID", VoiceMasterUserMapID);

    $("#divVoiceListInner").append(getLoader());

    $.ajax({
        url: window.location.origin + '/AddVoiceToUser/AddVoiceToUsers',
        data: fdata,
        type: "POST",
        processData: false,
        contentType: false,
        //cache: false,
        success: function (data) {
            if (!data.isSuccess) {
                BootstrapDialog.show({
                    id: "divAdduser",
                    title: __SUCCESS,
                    type: getDialogType(__SUCCESS),
                    message: function () {
                        var $message = $('<div id="divAdduserInner"></div>');
                        $message.append("Data save successfully");
                        return $message;
                    },
                    closeByBackdrop: false,
                    buttons: [
                        {
                            label: 'Ok',
                            cssClass: 'btn-primary',
                            action: function (dialogItself) {
                                window.location.href = "/AddVoiceToUser/Index";
                            }
                        }
                    ],
                    onshown: function (dialogRef) {
                        //
                    }
                });
            }
            else if (data.isSuccess) {
                //showBSAlert(data.messageCaption, data.message, __error);
                BootstrapDialog.show({
                    id: "divSaveStudent",
                    title: __WARNING,
                    type: getDialogType(__WARNING),
                    message: function () {
                        var $message = $('<div id="divSaveStudentInner"></div>');
                        $message.append("Error");
                        return $message;
                    },
                    closeByBackdrop: false,
                    buttons: [{
                        label: __ok,
                        cssClass: 'btn btn-primary',
                        action: function (dialogItself) {
                            window.location.href = "/AddVoiceToUser/Index";
                        }
                    }]
                });
            }
        },
        complete: function () {
            setTimeout(function () {
                removeLoader("#divVoiceListInner");
            }, 2000);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //$("#divLoginBox").removeClass("box");
            setTimeout(function () {
                //removeLoader("#divLoader");
            }, 300);
            // alert(xhr.error())
        }
    });
}

function goSearch(flag) {
    if (flag == undefined)
        flag = 'search';

    var isLoad = false;
    if (flag == '-1')
        isLoad = true;
    searchVoice($("#hdnUserID").val(), $("#Search").val().trim(), $("#hdnSortColumn").val().trim(), $("#hdnSortOrder").val().trim(), $("#hdnPage").val().trim(), $("#hdnSize").val().trim(), flag, isLoad, listtype);
}

$(document).on("click", ".tbl1 th", function () {
    var col = $(this).attr("id");
    col = col.substring(4, col.length);

    switch (col) {
        case "activedeactive":
        case "editdeleterecords":
        case "recharge":
            break;
        default:
            searchVoice($("#hdnUserID").val(), $("#Search").val().trim(), col, $("#hdnSortOrder").val().trim(), $("#hdnPage").val().trim(), $("#hdnSize").val().trim(), 'sort', false, listtype);
            $("#hdnSortColumn").val(col);
            break;
    }

    return false;
});

function searchVoice(userid, query, sortColumn, sortOrder, page, size, flag, ISLOAD, lsttype) {
    $("#divLoader").append(getLoader());
    $.ajax({
        url: searchUrl,
        //data: JSON.stringify({ query: query, sortColumn: sortColumn, sortOrder: sortOrder, page: page, size: size, flag: flag, ISLOAD: ISLOAD, ListType: lsttype }),
        data: { "query": query, "sortColumn": sortColumn, "sortOrder": sortOrder, "page": page, "size": size, "flag": flag, "ISLOAD": ISLOAD, "ListType": lsttype, "userid": userid, },
        type: "POST",
        cache: false,
        //contentType: "application/json; charset=utf-8",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {
            removeLoader("#divLoader");
            if (result != null) {
                $("#dvCommon").empty();
                $("#dvCommon").html(result);
                if ($(window).width() > 768)
                    $("#Search").focus();
            }
            setArrow();
            //resizeListView('ddlColumns', 'tblList');
            $("#ddlColumns").val(selectValue);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            removeLoader("#divLoader");
        }
    });
}

//var VoiceIDs = [];
//function VoiceChecked(type) {
//    var data = $("#data").val();
//    if (type == 1) {
//        $('input[name="CheckedTopics"]:checked').each(function () {
//            var AddVoices = new Object();
//            AddVoices.ID = $(this).val();
//            if (!VoiceIDs.some(voice => voice.ID === AddVoices.ID)) {
//                VoiceIDs.push(AddVoices);
//            }
//        });
//    }

//    if (type == 1) {
//        $('input[name="CheckedTopics"]:not(:checked)').each(function () {
//            var RemoveVoiceID = $(this).val();
//            var indexToRemove = VoiceIDs.findIndex(voice => voice.ID === RemoveVoiceID);

//            if (indexToRemove !== -1) {
//                VoiceIDs.splice(indexToRemove, 1);
//            }
//        });
//    }

//    VoiceIDs.forEach(voice => {
//        var idToCheck = `#CheckedTopics_${voice.ID}`;
//        $(idToCheck).prop('checked', true);
//    });
//}

