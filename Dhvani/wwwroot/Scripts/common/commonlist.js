/*function changeStatus(obj, id, action, type) {
    console.log(obj + " : " + id + " : " + action + " : " + type);
    if (obj) {
        
        $("#divLoader").append(getLoader());
        $.ajax({
            url: changeStatusUrl,
            data: { "ID": id, "StatusID": action, "type": type },
            type: "POST",
            cache: false,            
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    removeLoader("#divLoader");
                    if (data.isSuccess) {
                        if (data.message != "")
                            showBSAlert(data.messageCaption, data.message, __SUCCESS);
                        var onclick = "changeStatus(this,'" + id + "','0')";
                        if (action == '0') {
                            onclick = "changeStatus(this,'" + id + "','1')";
                        }
                        $(obj).attr("onclick", "");
                        $(obj).attr("data-toggle", "");
                        $(obj).removeAttr("data-original-title");
                        $(obj).attr("onclick", onclick);
                        $(obj).siblings().attr("title", "");
                        $(obj).siblings().attr("data-toggle", "tooltip");
                        //$(obj).siblings().attr("class", "btn-fa-addCustom");

                        if (action == '1') {
                            $(obj).attr("class", "btn-fa-addCustom btnCust_unlock");
                            $(obj).attr("data-original-title", __deActivate);
                            $(obj).children().attr("class", "fa fa-unlock");
                            //$(obj).next().hide();
                            
                        }
                        else {
                            if ($(obj).attr("id") == "DeActive") {
                                $(obj).prev().hide();
                            }
                            $(obj).attr("class", "btn-fa-addCustom btnCust_lock");
                            $(obj).attr("data-original-title", __activate);
                            $(obj).children().attr("class", "fa fa-lock");
                        }
                        $('[data-toggle="tooltip"]').tooltip();
                    }
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                removeLoader("#divLoader");
            }
        });        
    }
}*/

function changeStatus(obj, id, action, type) {
    console.log("action: ", action);
    BootstrapDialog.show({
        id: "divEnableDisableRecord",
        title: __WARNING,
        type: getDialogType(__WARNING),
        size: BootstrapDialog.SIZE_NORMAL,
        message: function () {
            var $message = $('<div id="divEnableDisableRecordInner" class="box box-no-border-shadow pTB10-LR05" style="margin:0px;"></div>');

            var result = '';
            result += '<div>';

            if (action == 0) {
                result += "Are you sure you want to Disable this record!";
            }
            else if (action == 2) {
                result += "Are you sure you want to Delete this record!";
            }
            else {
                result += "Are you sure you want to Enable this record!";
            }

            result += '</div>';

            $message.append(result);
            return $message;
        },
        buttons: [
            {
                label: __ok,
                cssClass: 'btn btn-primary',
                action: function (dialog) {
                    if (obj) {
                        //BootstrapDialog.confirm(__confirmMessage, function (result) {
                        //    if (result) {
                        $("#divLoader").append(getLoader());
                        $.ajax({
                            url: changeStatusUrl,
                            data: { "ID": id, "StatusID": action, "type": type },
                            type: "POST",
                            cache: false,
                            //contentType: "application/json; charset=utf-8",
                            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                            dataType: "json",
                            success: function (data) {
                                if (data != null) {
                                    removeLoader("#divLoader");
                                    if (data.isSuccess) {
                                        if (data.message != "")
                                            //showBSAlert(data.messageCaption, data.message, __SUCCESS);
                                        var onclick = "changeStatus(this,'" + id + "','0')";
                                        if (action == '0') {
                                            onclick = "changeStatus(this,'" + id + "','1')";
                                        }
                                        $(obj).attr("onclick", "");
                                        $(obj).attr("data-toggle", "");
                                        $(obj).removeAttr("data-original-title");
                                        $(obj).attr("onclick", onclick);
                                        $(obj).siblings().attr("title", "");
                                        $(obj).siblings().attr("data-toggle", "tooltip");
                                        //$(obj).siblings().attr("class", "btn-fa-addCustom");

                                        if (action == '1') {
                                            $(obj).attr("class", "btn-fa-addCustom btnCust_unlock");
                                            $(obj).attr("data-original-title", __deActivate);
                                            $(obj).children().attr("class", "fa fa-unlock");
                                            //$(obj).next().hide();
                                        }
                                        else if (action != '2') {
                                            if ($(obj).attr("id") == "DeActive") {
                                                $(obj).prev().hide();
                                            }
                                            $(obj).attr("class", "btn-fa-addCustom btnCust_lock");
                                            $(obj).attr("data-original-title", __activate);
                                            $(obj).children().attr("class", "fa fa-lock");
                                        }
                                        else if (action == '2') {
                                            //Remove row
                                        }
                                        $('[data-toggle="tooltip"]').tooltip();
                                        //removeRow("row_" + id);
                                        location.reload();
                                    }
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                removeLoader("#divLoader");
                            }
                        });
                        //    }
                        //});
                    }

                    dialog.close();
                }
            },
            {
                label: __Cancel,
                cssClass: 'btn btn-default',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }
        ],
        onshown: function (dialogRef) {
            //
        },
        closeByBackdrop: false,
        closable: false,
    });
}
function changePromoteStatus(id, PromoteStatus) {
    $("#divLoader").append(getLoader());
    $.ajax({
        url: window.location.origin + '/api/StudentDetails/PromoteStudent?ID=' + id + '&PromoteStatus=' + PromoteStatus,
        data: {},//{ "ID": id, "PromoteStatus": PromoteStatus },
        type: "POST",
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                removeLoader("#divLoader");
                if (data.isSuccess) {
                    if (data.message != "")
                        showBSAlert(data.messageCaption, data.message, __SUCCESS);
                    location.reload();
                }
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            removeLoader("#divLoader");
        }
    });
}

function removeRow(_thisRecord) {
    $("#"+_thisRecord).remove();
}

function search(query, sortColumn, sortOrder, page, size, flag, ISLOAD, lsttype) {
    $("#divLoader").append(getLoader());
    $.ajax({
        url: searchUrl,
        //data: JSON.stringify({ query: query, sortColumn: sortColumn, sortOrder: sortOrder, page: page, size: size, flag: flag, ISLOAD: ISLOAD, ListType: lsttype }),
        data: { "query": query, "sortColumn": sortColumn, "sortOrder": sortOrder, "page": page, "size": size, "flag": flag, "ISLOAD": ISLOAD, "ListType": lsttype },
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

var addRemovePronunciationItem = {
    add: function () {
        var total_element = $(".PronunciationBox").length;
        var lastid = $(".PronunciationBox:last").attr("id");
        var split_id = lastid.split("_");
        var lastIDVal = Number(split_id[1]);
        var nextindex = lastIDVal + 1;
        if (lastIDVal <= 0)
            nextindex = lastIDVal - 1;
        else if (lastIDVal > 0)
            nextindex = -1;

        var max = 10;
        if (total_element < max) {
            $(".PronunciationBox:last").after("<div class='PronunciationBox' id='divPronunciationBox_" + nextindex + "' style='margin-top: 0px;'></div>");
            var data = getPronunciationHTML(nextindex)
            $("#divPronunciationBox_" + nextindex).append(data);
        }
    },
    remove: function (objRemove, flag) {
        var id = $(objRemove).attr("id");
        if (id != "removePronunciationBox_1") {
            var split_id = id.split("_");
            var deleteindex = split_id[1];

            if (flag == 1) {//To mark delete status in db for existing partners
                BootstrapDialog.confirm(__msg_deleteFullfilmentPartner, function (result) {
                    if (result) {
                        var deletePartnerIds = $("#hdnPartnerDeleteIds").val();

                        if (deletePartnerIds != "")
                            deletePartnerIds += "," + deleteindex;
                        else
                            deletePartnerIds = deleteindex;

                        $("#hdnPartnerDeleteIds").val(deletePartnerIds);
                        $("#divPronunciationBox_" + deleteindex).remove();
                        addRemoveFinanceItem.addFirst();
                    }
                });
            }
            else {
                $("#divPronunciationBox_" + deleteindex).remove();
                addRemoveFinanceItem.addFirst();
                if ($(".PronunciationBox").length != 3) {
                    console.log($(".PronunciationBox").length);
                    $(".btnAddStudent").removeAttr("style");
                }
            }
        }
    },
    addFirst: function () {
        var lastid = $(".PronunciationBox:last").attr("id");
        if (lastid === undefined) {
            $("._jsMainPronunciationBox").append("<div class='PronunciationBox' id='divPronunciationBox_0'></div>");
            var data = getPronunciationHTML(0)
            $("#divPronunciationBox_0").append(data);
        }
    },

    editRow: function () {

    }
}

function getPronunciationHTML(nextindex) {
    var yourWord = $("#yourWord_0").val();
    var alternateSpelling = $("#alternateSpelling_0").val();
    var dictionaryLanguage = $("#dictionaryLanguage_0").val();
    var voice = $("#voice_0").val();
    var accent = $("#accent_0").val();

    var data = "";
    data += '<div>';
    data += '   <input type="hidden" name="hdnPronunciationBox_' + nextindex + '" value="' + nextindex + '" id="hdnPronunciationBox_' + nextindex + '" />';

    data += '<table id="tbl_' + nextindex + '" class="table table-bordered table-hover dataTable dtr-inline" role="grid" aria-describedby="example2_info">';
    data += '<tbody>';
    data += '<tr id="tblRow_' + nextindex + '">';
    data += '   <td class="wordbreak" style="width: 20%;">';    
    data += '       <div class="form-control form-control-custom">';
    data += '           <input type="text" id="yourWord_' + nextindex + '" name="yourWord_' + nextindex + '" value="' + yourWord +'" style="cursor: not-allowed;" readonly />';
    data += '           <span class="btnReplay"><i class="fas fa-play"></i></span>';
    data += '       </div>';
    data += '   </td>';
    data += '   <td class="wordbreak" style="width: 20%;">';    
    data += '       <div class="form-control form-control-custom">';
    data += '           <input type="text" id="alternateSpelling_' + nextindex + '" name="alternateSpelling_' + nextindex + '" value="' + alternateSpelling +'" style="cursor: not-allowed;" readonly />';
    data += '           <span class="btnReplay"><i class="fas fa-play"></i></span>';
    data += '       </div>';
    data += '   </td>';
    data += '   <td class="wordbreak" style="width: 15%;">';    
    data += '       <select class="form-control" id="dictionaryLanguage_' + nextindex + '" name="dictionaryLanguage_' + nextindex + '" value="' + dictionaryLanguage +'" style="cursor: not-allowed;" readonly>';
    data += '           <option value="0">Select</option>';
    data += '       </select>';
    data += '   </td>';
    data += '   <td class="wordbreak" style="width: 15%;">';    
    data += '       <select class="form-control" id="voice_' + nextindex + '" name="voice_' + nextindex + '" value="' + voice +'" style="cursor: not-allowed;" readonly>';
    data += '           <option value="0">Select</option>';
    data += '       </select>';
    data += '   </td>';
    data += '   <td class="wordbreak" style="width: 15%;">';    
    data += '       <select class="form-control" id="accent_' + nextindex + '" name="accent_' + nextindex + '" value="' + accent +'" style="cursor: not-allowed;" readonly>';
    data += '           <option value="0">Select</option>';
    data += '       </select>';
    data += '   </td>';
    data += '   <td class="wordbreak">';
    data += '       <div style="margin-top: 5px;">';
    data += '           <span class="btn-fa-addCustom btnCust_edit" id="btnEdit_' + nextindex + '" onclick="addRemovePronunciationItem.editRow"><i class="fa fa-edit"></i></span>';
    data += '           <span class="btn-fa-addCustom btnCust_trash" onclick="addRemovePronunciationItem.remove(this,0)" id="removePronunciationBox_' + nextindex + '" _id="' + nextindex + '"><i class="fa fa-trash"></i></span>';
    data += '       </div>';
    data += '   </td>';
    data += '</tr>';
    data += '</tbody>';
    data += '</table>';
    data += '</div>';
    return data;
}