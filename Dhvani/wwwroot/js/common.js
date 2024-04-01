// JavaScript Document
var funcSelectSentenceChBxVal = [];
$(document).ready(function (e) {
    //loadView.dashboard();
    //loadView.createAcc();
    //addRemoveStudent.addFirst();
    //addRemoveTeacherEduQua.addFirst();
    //addRemoveTeacherPrevEmp.addFirst();
    //selectSentence.showContentBlock();
    //addNewTopic();
    //tblShow();
    numberOnly();
    charOnlyFnameLname();
});
var loadView = {
    dashboard: function () {
        $(".nav-sidebar .nav-link").click(function () {
            $('#dashboard').load("dashboard.html");
        });
    },
    createAcc: function () {
        $(".nav-sidebar .nav-link").click(function () {
            $('#createAccount').load("create-account.html");
        });
    }
}

// convert Formdata to object
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

function getLoader() {
    return $("#scriptLoader").html();
}
function removeLoader(selector) {
    $(selector).find(".overlay, .loading-img").remove();
}
function getLoaderSmall() {
    return $("#scriptLoaderSmall").html();
}
function removeLoaderSmall(selector) {
    $(selector).find(".overlay, .loading-img-small").remove();
}

function SubmitCreateAccount() {
    var inputMassage = "";
    var valid = true;

    var txtfullname = $("input[name='txtfullname']").val();
    var txtusername = $("input[name='txtusername']").val();
    var userTypeRole = $("select[name='userTypeRole']").val();
    var txtPassword = $("input[name='txtPassword']").val().trim();

    if (txtfullname == "") {
        inputMassage += __Fullname + "<br />";
        valid = false;
    }
    if (txtusername == "") {
        inputMassage += __Username + "<br />";
        valid = false;
    }
    if (userTypeRole == "") {
        inputMassage += __userTypeRole + "<br />";
        valid = false;
    }
    if (txtPassword == "") {
        inputMassage += __Password;
        valid = false;
    }
    if (valid) {
        alert("Action Here");
    }
    else {
        showBSAlert(__warnCaption, inputMassage, __DANGER);
    }
    return valid;
}

function showDrDetails() {
    BootstrapDialog.show({
        id: "divDrInfo",
        title: "Information",
        size: BootstrapDialog.SIZE_WIDE,
        message: function () {
            var $message = $('<div id="divDrInfoInner" class="pTB10-LR05"></div>');
            var html = '<div class="body">';
            html += '<div class="row">';
            html += '<div class="col-md-6">';
            //html += '   <label for="input-1">' + __emailAddress + '</label>:';
            html += '   <label for="input-1">Name</label>: XYZ';
            html += '</div>';
            html += '<div class="col-md-6">';
            html += '   <label for="input-1">Details</label>: XYZ';
            html += '</div>';
            html += '<div>&nbsp;</div>';
            html += '</div>';
            html += '</div>';
            $message.append(html);
            return $message;
        },
        closeByBackdrop: true,
        closable: true,
        buttons: [
            {
                label: __msglogRegClose,
                cssClass: 'btn-default',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }/*, {
				label: __msglogRegSubmit,
				cssClass: 'btn-primary',
				id: 'btnFPsubmit',
				action: function (dialog) {
					//alert("Forgor Password");
				}
			}*/
        ],
        onshown: function (dialogRef) {
            //
        }
    });
}

function SubmitDoctorDataCollection() {
    var inputMassage = "";
    var valid = true;

    var txtDoctorName = $("input[name='txtDoctorName']").val();
    var txtSBUCode = $("input[name='txtSBUCode']").val();
    var txtQualification = $("select[name='txtQualification']").val();
    var txtPracticePlace = $("input[name='txtPracticePlace']").val();
    var txtDOB = $("input[name='txtDOB']").val();
    var txtDOA = $("input[name='txtDOA']").val();

    if (txtDoctorName == "") {
        inputMassage += __drName + "<br />";
        valid = false;
    }
    if (txtSBUCode == "") {
        inputMassage += __drSUBCode + "<br />";
        valid = false;
    }
    if (txtQualification == "") {
        inputMassage += __drQualification + "<br />";
        valid = false;
    }
    if (txtPracticePlace == "") {
        inputMassage += __drPracticePlace + "<br />";
        valid = false;
    }
    if (txtDOB == "") {
        inputMassage += __drDateOfBirth + "<br />";
        valid = false;
    }
    if (txtDOA == "") {
        inputMassage += __drDOA;
        valid = false;
    }
    if (valid) {
        alert("Action Here");
    }
    else {
        showBSAlert(__warnCaption, inputMassage, __DANGER);
    }
    return valid;
}
function CallEnter(objEvent, obj) {
    if (objEvent) {
        if (objEvent.which || objEvent.keyCode) {
            if ((objEvent.which == 13) || (objEvent.keyCode == 13)) {
                $("#" + obj).click();
                return false;
            }
        }
    }
    else
        return true;
}

/* Add Student */
var addRemoveStudent = {
    add: function () {
        var total_element = $(".StudentBox").length;
        var lastid = $(".StudentBox:last").attr("id");
        var split_id = lastid.split("_");
        var lastIDVal = Number(split_id[1]);
        var nextindex = lastIDVal + 1;
        //if (lastIDVal <= 0)
        //	nextindex = lastIDVal - 1;
        //else if (lastIDVal > 0)
        //	nextindex = -1;

        var max = 10;
        if (total_element < max) {
            $(".StudentBox:last").after("<div class='StudentBox' id='divStudentBox_" + nextindex + "'></div>");
            var data = getStudentHTML(nextindex)
            $("#divStudentBox_" + nextindex).append(data);
        }

        if ($(".StudentBox").length == 3) {
            $(".btnAddStudent").css({ "display": "none" });
        }
        //toggleLogo($("input[name=UserName]").val());
    },
    remove: function (objRemove, flag) {
        var id = $(objRemove).attr("id");
        if (id != "removeStudentBox_1") {
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
                        $("#divStudentBox_" + deleteindex).remove();
                        addRemoveStudent.addFirst();
                    }
                });
            }
            else {
                $("#divStudentBox_" + deleteindex).remove();
                addRemoveStudent.addFirst();
                if ($(".StudentBox").length != 3) {
                    console.log($(".StudentBox").length);
                    $(".btnAddStudent").removeAttr("style");
                }
            }
        }
    },
    addFirst: function () {
        var lastid = $(".StudentBox:last").attr("id");
        if (lastid === undefined) {
            $("._jsMainStudentBox").append("<div class='StudentBox' id='divStudentBox_0'></div>");
            var data = getStudentHTML(0)
            $("#divStudentBox_0").append(data);
        }
    },
}
function getStudentHTML(nextindex) {
    var data = "";
    data += '<div class="row">';
    data += '   <input type="hidden" name="hdnName_' + nextindex + '" value="' + nextindex + '">';

    if (nextindex != 0)
        data += '<div class="col-md-12"><span class="btn-student-delrecord pull-right student-btndel" onclick="addRemoveStudent.remove(this,0)" id="removeStudentBox_' + nextindex + '"  _id="' + nextindex + '"><i class="removeStudentBox"><img src="/images/icon_close_notification.png" alt="" class="img" /></i></span></div>';
    data += '	<div class="clear"></div>';

    data += '<div class="col-md-3">';
    data += '<div class="form-group">';
    data += '<label>Relationship</label><br />';
    data += '	<select name="relationship_' + nextindex + '" id="Student_relationship_' + nextindex + '" class="form-control">';
    data += '       <option value="0">Select Relationship</option>';
    //data += '       <option value="Mother">Mother</option>';
    //data += '       <option value="Father">Father</option>';
    data += '       <option value="pickup">Pickup</option>';
    data += '       <option value="emergency">Emergency</option>';
    data += '   </select>';
    data += '</div>';
    data += '</div>';

    data += '<div class="col-md-3">';
    data += '<div class="form-group">';
    data += '   <label>Contact Name</label><br />';
    data += '   <input type="text" name="contactName_' + nextindex + '" id="Student_contactName_' + nextindex + '" class="form-control">';
    data += '</div>';
    data += '</div>';

    data += '<div class="col-md-3">';
    data += '<div class="form-group">';
    data += '   <label>Contact Number</label><br />';
    data += '   <input type="text" name="contactNumber_' + nextindex + '" id="Student_contactNumber_' + nextindex + '" class="form-control">';
    data += '</div>';
    data += '</div>';

    data += '<div class="col-md-3"  style="visibility:hidden">';
    data += '<div class="form-group">';
    data += '   <label>&nbsp;</label><br />';
    data += '   <label class="checkboxWithLabel"><input type="checkbox" name="isEmergencyContact_' + nextindex + '" id="Student_isEmergencyContact_' + nextindex + '" class="checkbox" /> Emergency Contact</label>';
    data += '</div>';
    data += '</div>';

    data += '<div class="col-md-6">';
    data += '<div class="form-group">';
    data += '   <label>Email Address</label><br />';
    data += '   <input type="text" name="emailAddress_' + nextindex + '" id="emailAddress_' + nextindex + '" onblur="IsUsernameExist(' + "'emailAddress_" + nextindex + "',''" + ')" onfocusout="emailNotSame(' + nextindex + ')" class="form-control email_box">';
    data += '</div>';
    data += '</div>';

    data += '<div class="clearfix topicSeprator"></div>';

    data += '</div>';
    return data;
}
/* End Student */

/* Start Add Teacher in Add Class */
var addTeacherInAddClass = {
    add: function () {
        var total_element = $(".TeacherAddInClassBox").length;
        var lastid = $(".TeacherAddInClassBox:last").attr("id");
        var split_id = lastid.split("_");
        var lastIDVal = Number(split_id[1]);
        var nextindex = lastIDVal + 1;
        //if (lastIDVal <= 0)
        //	nextindex = lastIDVal - 1;
        //else if (lastIDVal > 0)
        //	nextindex = -1;
        var schoolId = $("#AddClass_SchoolID").val();
        var max = 20;
        if (total_element < max) {
            $(".TeacherAddInClassBox:last").after("<div class='TeacherAddInClassBox'  id='divTeacherAddInClassBox_" + nextindex + "'></div>");
            var data = getTeacherInAddClass(nextindex)
            $("#divTeacherAddInClassBox_" + nextindex).append(data);

            FetchTeachers(schoolId, 'AddClass_TeacherID_' + nextindex);
        }

        //toggleLogo($("input[name=UserName]").val());
        //addTeacherInAddClass.updateCalendar();
    },
    remove: function (objRemove, flag) {
        var id = $(objRemove).attr("id");
        if (id != "removeTeacherAddInClassBox_1") {
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
                        $("#divTeacherAddInClassBox_" + deleteindex).remove();
                        addTeacherInAddClass.addFirst();
                    }
                });
            }
            else {
                $("#divTeacherAddInClassBox_" + deleteindex).remove();
                addTeacherInAddClass.addFirst();
            }
        }
    },
    addFirst: function () {
        var lastid = $(".TeacherAddInClassBox:last").attr("id");
        if (lastid === undefined) {
            $("._jsMainTeacherAddInClassBox").append("<div class='TeacherAddInClassBox' id='divTeacherAddInClassBox_0'></div>");
            var data = getTeacherInAddClass(0)
            $("#divTeacherAddInClassBox_0").append(data);
        }
    }
}
function getTeacherInAddClass(nextindex) {
    var data = "";
    data += '<div class="mtop05">';
    data += '   <input type="hidden" name="teacherAddInClass_' + nextindex + '" value="' + nextindex + '">';

    if (nextindex != 0)
        data += '	<div style="display: flex; flex - direction: row;">';
    data += '		<select name="OwnerID_' + nextindex + '" disabled="disabled" onchange="validateTeacherDD(\'AddClass_TeacherID_' + nextindex + '\')" id="AddClass_TeacherID_' + nextindex + '" value="@Model.TeacherID_' + nextindex + '" class="form-control" style="margin-right: 5px;">';
    data += '			<option value="0">Select Teacher</option>';
    data += '		</select>';
    data += '		<div class="clear"></div>';
    data += '		<span class="btn-teacherPrevEmp-delrecord pull-right teacherPrevEmp-btndel" onclick="addTeacherInAddClass.remove(this,0)" id = "removeTeacherAddInClassBox_' + nextindex + '"  _id = "' + nextindex + '"  style="top: 4px;"> <i class="removeTeacherAddInClassBox"><img src="/images/icon_close_notification.png" alt="" class="img" /></i></span>';
    data += '	</div > ';
    data += '	<div class="clearfix"></div>';
    data += '	</div>';
    return data;
}
/* End Add Teacher in Add Class */

///* Start Add Teacher in Add Class */
//var addTeacherInAddClass = {
//	add: function () {
//		var total_element = $(".TeacherAddInClassBox").length;
//		var lastid = $(".TeacherAddInClassBox:last").attr("id");
//		var split_id = lastid.split("_");
//		var lastIDVal = Number(split_id[1]);
//		var nextindex = lastIDVal + 1;
//		//if (lastIDVal <= 0)
//		//	nextindex = lastIDVal - 1;
//		//else if (lastIDVal > 0)
//		//	nextindex = -1;

//		var max = 20;
//		if (total_element < max) {
//			$(".TeacherAddInClassBox:last").after("<div class='TeacherAddInClassBox'  id='divTeacherAddInClassBox_" + nextindex + "'></div>");
//			var data = getTeacherInAddClass(nextindex)
//			$("#divTeacherAddInClassBox_" + nextindex).append(data);

//			FetchTeachers('AddClass_SchoolID', 'AddClass_TeacherID_' + nextindex);
//		}

//		//toggleLogo($("input[name=UserName]").val());
//		//addTeacherInAddClass.updateCalendar();
//	},
//	remove: function (objRemove, flag) {
//		var id = $(objRemove).attr("id");
//		if (id != "removeTeacherAddInClassBox_1") {
//			var split_id = id.split("_");
//			var deleteindex = split_id[1];

//			if (flag == 1) {//To mark delete status in db for existing partners
//				BootstrapDialog.confirm(__msg_deleteFullfilmentPartner, function (result) {
//					if (result) {
//						var deletePartnerIds = $("#hdnPartnerDeleteIds").val();

//						if (deletePartnerIds != "")
//							deletePartnerIds += "," + deleteindex;
//						else
//							deletePartnerIds = deleteindex;

//						$("#hdnPartnerDeleteIds").val(deletePartnerIds);
//						$("#divTeacherAddInClassBox_" + deleteindex).remove();
//						addTeacherInAddClass.addFirst();
//					}
//				});
//			}
//			else {
//				$("#divTeacherAddInClassBox_" + deleteindex).remove();
//				addTeacherInAddClass.addFirst();
//			}
//		}
//	},
//	addFirst: function () {
//		var lastid = $(".TeacherAddInClassBox:last").attr("id");
//		if (lastid === undefined) {
//			$("._jsMainTeacherAddInClassBox").append("<div class='TeacherAddInClassBox' id='divTeacherAddInClassBox_0'></div>");
//			var data = getTeacherInAddClass(0)
//			$("#divTeacherAddInClassBox_0").append(data);
//		}
//	}
//}
//function getTeacherInAddClass(nextindex) {
//	var data = "";
//	data += '<div class="mtop05">';
//	data += '   <input type="hidden" name="teacherAddInClass_' + nextindex + '" value="' + nextindex + '">';

//	if (nextindex != 0)
//		data += '	<div style="display: flex; flex - direction: row;">';
//	data += '		<select name="OwnerID_' + nextindex + '" disabled="disabled" onchange="validateTeacherDD(\'AddClass_TeacherID_' + nextindex + '\')" id="AddClass_TeacherID_' + nextindex + '" value="@Model.TeacherID_' + nextindex + '" class="form-control" style="margin-right: 5px;">';
//	data += '			<option value="0">Select Teacher</option>';
//	data += '		</select>';
//	data += '		<div class="clear"></div>';
//	data += '		<span class="btn-teacherPrevEmp-delrecord pull-right teacherPrevEmp-btndel" onclick="addTeacherInAddClass.remove(this,0)" id = "removeTeacherAddInClassBox_' + nextindex + '"  _id = "' + nextindex + '"  style="top: 4px;"> <i class="removeTeacherAddInClassBox"><img src="/images/icon_close_notification.png" alt="" class="img" /></i></span>';
//	data += '	</div > ';
//	data += '	<div class="clearfix"></div>';
//	data += '	</div>';
//	return data;
//}
///* End Add Teacher in Add Class */

/* Add Teacher Edu Qua */
var addRemoveTeacherEduQua = {
    add: function () {
        var total_element = $(".TeacherEduQuaBox").length;
        var lastid = $(".TeacherEduQuaBox:last").attr("id");
        var split_id = lastid.split("_");
        var lastIDVal = Number(split_id[1]);
        var nextindex = lastIDVal + 1;
        //if (lastIDVal <= 0)
        //	nextindex = lastIDVal - 1;
        //else if (lastIDVal > 0)
        //	nextindex = -1;

        var max = 10;
        if (total_element < max) {
            $(".TeacherEduQuaBox:last").after("<div class='TeacherEduQuaBox' id='divTeacherEduQuaBox_" + nextindex + "'></div>");
            var data = getTeacherEduQuaHTML(nextindex)
            $("#divTeacherEduQuaBox_" + nextindex).append(data);
        }

        //toggleLogo($("input[name=UserName]").val());
        addRemoveTeacherEduQua.updateCalendar();
    },
    remove: function (objRemove, flag) {
        var id = $(objRemove).attr("id");
        if (id != "removeTeacherEduQuaBox_1") {
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
                        $("#divTeacherEduQuaBox_" + deleteindex).remove();
                        addRemoveTeacherEduQua.addFirst();
                    }
                });
            }
            else {
                $("#divTeacherEduQuaBox_" + deleteindex).remove();
                addRemoveTeacherEduQua.addFirst();
            }
        }
    },
    addFirst: function () {
        var lastid = $(".TeacherEduQuaBox:last").attr("id");
        if (lastid === undefined) {
            $("._jsMainStudentBox").append("<div class='TeacherEduQuaBox' id='divTeacherEduQuaBox_0'></div>");
            var data = getTeacherEduQuaHTML(0)
            $("#divTeacherEduQuaBox_0").append(data);
        }
    },
    updateCalendar: function () {
        $('#DivQualificationStartDate, #DivQualificationEndDate').datepicker({
            format: "yyyy-mm-dd",
            autoclose: true,
            orientation: "bottom",
            todayHighlight: true,
            //startDate: '0m',
            endDate: '+0d',
            maxDate: 0
        }).on('changeDate', function (ev) {
            $(this).datepicker('hide');
        });
    }
}
function getTeacherEduQuaHTML(nextindex) {
    var data = "";
    data += '<div class="row">';
    data += '   <input type="hidden" name="hdnName_' + nextindex + '" value="' + nextindex + '">';

    if (nextindex != 0)
        data += '<div class="col-md-12"><span class="btn-teacherEduQua-delrecord pull-right teacherEduQua-btndel" onclick="addRemoveTeacherEduQua.remove(this,0)" id="removeTeacherEduQuaBox_' + nextindex + '"  _id="' + nextindex + '"><i class="removeTeacherEduQuaBox"><img src="/images/icon_close_notification.png" alt="" class="img" /></i></span></div>';
    data += '	<div class="clear"></div>';
    //data += '		<div class="col-md-3">';
    //data += '			<div class="form-group">';
    //data += '				<label>Qualification Type</label><br />';
    //data += '				<input type="text" name="QualificationType_' + nextindex + '" id="Teacher_QualificationType_' + nextindex + '" class="form-control">';
    //data += '			</div>';
    //data += '		</div>';
    data += '		<input type="hidden" id="QualificationDoc_id_' + nextindex + '" name="QualificationDoc_id_' + nextindex + '" value="0" />';
    data += '		<input type="hidden" id="QualificationDoc_base64_' + nextindex + '" name="QualificationDoc_base64_' + nextindex + '" />';
    data += '       <input type="hidden" id="QualificationDoc_base64_name_' + nextindex + '" name="QualificationDoc_base64_name_' + nextindex + '"/>';
    data += '		<input type="hidden" id="QualificationDoc_base64_extention_' + nextindex + '" name="QualificationDoc_base64_extention_' + nextindex + '"/>';

    data += '       <div class="col-md-6">';
    data += '			<div class="form-group">';
    data += '				<label>Qualification Name</label><br />';
    data += '               <input type="text" name="QualificationName_' + nextindex + '" id="QualificationName_' + nextindex + '" class="form-control">';
    data += '			</div>';
    data += '		</div>';
    data += '		<div class="col-md-6">';
    data += '			<div class="form-group">';
    data += '				<label>Upload Document</label><br />';
    data += '               <input type="file" onchange="encodeImageFileAsURL(this,\'QualificationDoc_base64_\',' + nextindex + ')" name="QualificationDoc_base64_' + nextindex + '" id="QualificationDoc_' + nextindex + '" class="form-control" />';
    data += '			</div>';
    data += '		</div>';
    //data += '       <div class="col-md-3">';
    //data += '			<div class="form-group">';
    //data += '				<label>Qualification Start Date</label><br />';
    //data += '				<div class="input-group date" data-provide="datepicker" id="DivQualificationStartDate">';
    //data += '					<input type="text" class="form-control card_inputs" id="Teacher_QualificationStartDate_' + nextindex + '" name="QualificationStartDate_' + nextindex + '" onchange="" onblur="" placeholder="DD-MM-YYYY" value="" />';
    //data += '					<div class="input-group-addon input-group-addon-cust"> <span class="input-group-text input-group-text-cust"> <i class="far fa-calendar-alt"></i> </span> </div>';
    //data += '				</div>';
    //data += '			</div>';
    //data += '		</div>';
    //data += '       <div class="col-md-3">';
    //data += '			<div class="form-group">';
    //data += '				<label>Qualification End Date</label><br />';
    //data += '               <div class="input-group date" data-provide="datepicker" id="DivQualificationEndDate">';
    //data += '					<input type="text" class="form-control card_inputs" id="QualificationEndDate_' + nextindex + '" name="Teacher_QualificationEndDate_' + nextindex + '" onchange="" onblur="" placeholder="DD-MM-YYYY" value="" />';
    //data += '                   <div class="input-group-addon input-group-addon-cust"> <span class="input-group-text input-group-text-cust"> <i class="far fa-calendar-alt"></i> </span> </div>';
    //data += '				</div>';
    //data += '			</div>';
    //data += '		</div>';
    //data += '		<div class="col-md-3">';
    //data += '			<div class="form-group">';
    //data += '				<label>Result</label><br />';
    //data += '               <input type="text" name="Result_' + nextindex + '" id="Teacher_Result_' + nextindex + '" class="form-control">';
    //data += '			</div>';
    //data += '		</div>';
    //data += '		<div class="col-md-6">';
    //data += '			<div class="form-group">';
    //data += '				<label>University Name</label><br />';
    //data += '				<input type="text" name="UnivercityName_' + nextindex + '" id="Teacher_UnivercityName_' + nextindex + '" class="form-control">';
    //data += '			</div>';
    //data += '		</div>';
    //data += '		<div class="col-md-3">';
    //data += '			<div class="form-group">';
    //data += '				<label>University Pincode</label><br />';
    //data += '               <input type="text" name="UnivercityPincode_' + nextindex + '" id="Teacher_UnivercityPincode_' + nextindex + '" class="form-control">';
    //data += '			</div>';
    //data += '		</div>';

    data += '		<div class="clearfix topicSeprator"></div>';

    data += '</div>';
    return data;
}
/* End Teacher Edu Qua */
/* Add Teacher Prev Emp */
var addRemoveTeacherPrevEmp = {
    add: function () {
        var total_element = $(".TeacherPrevEmpBox").length;
        var lastid = $(".TeacherPrevEmpBox:last").attr("id");
        var split_id = lastid.split("_");
        var lastIDVal = Number(split_id[1]);
        var nextindex = lastIDVal + 1;
        if (lastIDVal <= 0)
            nextindex = lastIDVal - 1;
        else if (lastIDVal > 0)
            nextindex = -1;

        var max = 10;
        if (total_element < max) {
            $(".TeacherPrevEmpBox:last").after("<div class='TeacherPrevEmpBox' id='divTeacherPrevEmpBox_" + nextindex + "'></div>");
            var data = getTeacherPrevEmpHTML(nextindex)
            $("#divTeacherPrevEmpBox_" + nextindex).append(data);
        }

        //toggleLogo($("input[name=UserName]").val());
        addRemoveTeacherPrevEmp.updateCalendar();
    },
    remove: function (objRemove, flag) {
        var id = $(objRemove).attr("id");
        if (id != "removeTeacherPrevEmpBox_1") {
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
                        $("#divTeacherPrevEmpBox_" + deleteindex).remove();
                        addRemoveTeacherPrevEmp.addFirst();
                    }
                });
            }
            else {
                $("#divTeacherPrevEmpBox_" + deleteindex).remove();
                addRemoveTeacherPrevEmp.addFirst();
            }
        }
    },
    addFirst: function () {
        var lastid = $(".TeacherPrevEmpBox:last").attr("id");
        if (lastid === undefined) {
            $("._jsMainStudentBox").append("<div class='TeacherPrevEmpBox' id='divTeacherPrevEmpBox_0'></div>");
            var data = getTeacherPrevEmpHTML(0)
            $("#divTeacherPrevEmpBox_0").append(data);
        }
    },
    updateCalendar: function () {
        $('#DivEmploymentStartDate, #DivEmploymentEndtDate').datepicker({
            format: "yyyy-mm-dd",
            autoclose: true,
            orientation: "bottom",
            todayHighlight: true,
            //startDate: '0m',
            endDate: '+0d',
            maxDate: 0
        }).on('changeDate', function (ev) {
            $(this).datepicker('hide');
        });
    }
}
function getTeacherPrevEmpHTML(nextindex) {
    var data = "";
    data += '<div class="row">';
    data += '   <input type="hidden" name="hdnName_' + nextindex + '" value="' + nextindex + '">';

    if (nextindex != 0)
        data += '<div class="col-md-12"><span class="btn-teacherPrevEmp-delrecord pull-right teacherPrevEmp-btndel" onclick="addRemoveTeacherPrevEmp.remove(this,0)" id="removeTeacherPrevEmpBox_' + nextindex + '"  _id="' + nextindex + '"><i class="removeTeacherPrevEmpBox"><img src="/images/icon_close_notification.png" alt="" class="img" /></i></span></div>';
    data += '	<div class="clear"></div>';
    data += '	<div class="col-md-3">';
    data += '		<div class="form-group">';
    data += '			<label>Designation</label><br />';
    data += '           <input type="text" name="Designation_' + nextindex + '" id="Teacher_Designation_' + nextindex + '" class="form-control">';
    data += '		</div>';
    data += '	</div>';
    data += '	<div class="col-md-3">';
    data += '		<div class="form-group">';
    data += '			<label>Employer</label><br />';
    data += '           <input type="text" name="Employer_' + nextindex + '" id="Teacher_Employer_' + nextindex + '" class="form-control">';
    data += '		</div>';
    data += '	</div>';
    data += '   <div class="col-md-3">';
    data += '		<div class="form-group">';
    data += '			<label>Employment Start Date</label><br />';
    data += '			<div class="input-group date" data-provide="datepicker" id="DivEmploymentStartDate">';
    data += '				<input type="text" class="form-control card_inputs" id="Teacher_EmploymentStartDate_' + nextindex + '" name="EmploymentStartDate_' + nextindex + '" onchange="" onblur="" placeholder="DD-MM-YYYY" value="" />';
    data += '               <div class="input-group-addon input-group-addon-cust"> <span class="input-group-text input-group-text-cust"> <i class="far fa-calendar-alt"></i> </span> </div>';
    data += '			</div>';
    data += '		</div>';
    data += '	</div>';
    data += '	<div class="col-md-3">';
    data += '		<div class="form-group">';
    data += '			<label>Employment End Date</label><br />';
    data += '			<div class="input-group date" data-provide="datepicker" id="DivEmploymentEndtDate">';
    data += '				<input type="text" class="form-control card_inputs" id="Teacher_EmploymentEndDate_' + nextindex + '" name="EmploymentEndDate_' + nextindex + '" onchange="" onblur="" placeholder="DD-MM-YYYY" value="" />';
    data += '				<div class="input-group-addon input-group-addon-cust"> <span class="input-group-text input-group-text-cust"> <i class="far fa-calendar-alt"></i> </span> </div>';
    data += '			</div>';
    data += '		</div>';
    data += '	</div>';
    data += '	<div class="col-md-3">';
    data += '		<div class="form-group">';
    data += '			<label class="checkboxWithLabel"><input type="checkbox" name="IsCurrentEmployer_' + nextindex + '" id="Teacher_IsCurrentEmployer_' + nextindex + '" class="checkbox" /> Current Employer</label>';
    data += '       </div>';
    data += '	</div>';
    data += '	<div class="clearfix topicSeprator"></div>';

    data += '</div>';
    return data;
}
/* End Teacher Prev Emp */
/* Add Assessment */
var addRemoveAssessment = {
    add: function () {
        var total_element = $(".AddAssessmentBox").length;
        var lastid = $(".AddAssessmentBox:last").attr("id");
        var split_id = lastid.split("_");
        var lastIDVal = Number(split_id[1]);
        var nextindex = lastIDVal + 1;
        //if (lastIDVal <= 0)
        //	nextindex = lastIDVal - 1;
        //else if (lastIDVal > 0)
        //	nextindex = -1;

        var max = 10;
        if (total_element < max) {
            $(".AddAssessmentBox:last").after("<div class='AddAssessmentBox' id='divAddAssessmentBox_" + nextindex + "'></div>");
            var data = getAssessmentHTML(nextindex)
            $("#divAddAssessmentBox_" + nextindex).append(data);
        }

        //toggleLogo($("input[name=UserName]").val());
        addRemoveAssessment.updateCalendar();
    },
    remove: function (objRemove, flag) {
        var id = $(objRemove).attr("id");
        if (id != "removeAddAssessmentBox_1") {
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
                        $("#divAddAssessmentBox_" + deleteindex).remove();
                        addRemoveAssessment.addFirst();
                    }
                });
            }
            else {
                $("#divAddAssessmentBox_" + deleteindex).remove();
                addRemoveAssessment.addFirst();
            }
        }
    },
    addFirst: function () {
        var lastid = $(".AddAssessmentBox:last").attr("id");
        if (lastid === undefined) {
            $("._jsMainAddAssessmentBox").append("<div class='AddAssessmentBox' id='divAddAssessmentBox_0'></div>");
            var data = getAssessmentHTML(0)
            $("#divAddAssessmentBox_0").append(data);
        }
    },
    updateCalendar: function () {
        $('#DivQualificationStartDate, #DivQualificationEndDate').datepicker({
            format: "yyyy-mm-dd",
            autoclose: true,
            orientation: "bottom",
            todayHighlight: true,
            //startDate: '0m',
            endDate: '+0d',
            maxDate: 0
        }).on('changeDate', function (ev) {
            $(this).datepicker('hide');
        });
    }
}
function getAssessmentHTML(nextindex) {
    var data = "";
    data += '<div class="">';
    data += '   <input type="hidden" name="hdnName_' + nextindex + '" value="' + nextindex + '">';

    if (nextindex != 0)
        data += '<div><span class="btn-teacherEduQua-delrecord pull-right assessment-btndel" onclick="addRemoveAssessment.remove(this,0)" id="removeAddAssessmentBox_' + nextindex + '"  _id="' + nextindex + '"><i class="removeAddAssessmentBox"><img src="/images/icon_close_notification.png" alt="" class="img" /></i></span></div>';
    data += '	<div class="clear"></div>';
    data += '	<div class="row mtop05">';
    data += '		<div class="col-md-8">';
    data += '			<div class="form-group">';
    data += '				<span>Topic ' + nextindex + '</span>';
    data += '			</div>';
    data += '			<div class="clearfix"></div>';
    data += '		</div>';
    data += '		<div class="col-md-4">';
    data += '			<div class="form-group">';
    data += '				<select class="form-control dropdownDownArrow" name="selectStatus_' + nextindex + '" id="selectStatus_' + nextindex + '">';
    data += '					<option>-- Select --</option>';
    data += '					<option>Approaching</option>';
    data += '					<option>Emerging</option>';
    data += '					<option>Performing</option>';
    data += '					<option>Excelling</option>';
    data += '					<option>To complete</option>';
    data += '				</select>';
    data += '			</div>';
    data += '			<div class="clearfix"></div>';
    data += '		</div>';
    data += '		<div class="clearfix"></div>';
    data += '	</div>';
    data += '	<div class="clearfix topicSeprator"></div>';
    data += '</div>';
    return data;
}
/* End Assessment */

function postDetails(obj, id) {
    $.ajax({
        url: '/Posts/GetPostDetails',
        data: { ID: id },
        type: "POST",
        cache: false,
        async: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result) {
            BootstrapDialog.show({
                id: "divPostDetails",
                title: "ClassInsta Post",
                size: BootstrapDialog.SIZE_EXTRAWIDE,
                message: function () {
                    var $message = $('<div id="divPostDetailsInner" class="pTB10-LR05"></div>');
                    $message.append(result);
                    return $message;
                },
                closeByBackdrop: false,
                closable: true,
                buttons: [
                    {
                        label: __ok,
                        cssClass: 'btn-primary',
                        action: function (dialogItself) {
                            dialogItself.close();
                        }
                    }
                ],
                onshown: function (dialogRef) {
                    selectSentence.addSentence();
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
        },
        complete: function () {
            //
        }
    })
}

// Add Post
//https://www.geeksforgeeks.org/how-to-dynamically-add-remove-table-rows-using-jquery/
// Denotes total number of rows
function addNewPost() {
    // Denotes total number of rows
    var rowIdx = 0;
    // jQuery button click event to add a row
    $('#addBtn').on('click', function () {
        var inputMassage = "";
        var valid = true;

        if ($("#selClass").val().trim() == "-- Select --") {
            inputMassage = inputMassage + "Class" + "\n";
            valid = false;
        }

        if ($("#selTerm").val().trim() == "-- Select --") {
            inputMassage = inputMassage + "Term" + "\n";
            valid = false;
        }

        if ($("#inputNewTopic").val().trim() == "") {
            inputMassage = inputMassage + "Topic";
            valid = false;
        }

        if (valid == false) {
            showBSAlert(__requiredCaption, inputMassage, __DANGER);
        }
        else {
            var newTopicName = $("#inputNewTopic").val();
            if (newTopicName != "" && $("#selClass").val() != "-- Select --" && $("#selTerm").val() != "-- Select --") {
                // Adding a row inside the tbody.
                $('#tbody').append(`<tr id="row_${++rowIdx}">
				 <td class="wordbreak row-index">
					<div id="div_${rowIdx}" class="divTopicEditable" contenteditable="true">${newTopicName} ${rowIdx}</div>
				 </td>
				 <td>
					<a id="" data-toggle="tooltip" title="" onclick="" class="btn-fa-addCustom btnCust_unlock"><i class="fa fa-unlock"></i></a>
					<a id="" data-toggle="tooltip" title="" onclick="" class="btn-fa-addCustom btnCust_lock"><i class="fa fa-lock"></i></a>
					<a class="btn-fa-addCustom remove" type="button"><i class="fa fa-trash"></i></a>
				 </td>
			</tr>`);
            }
            else {
                //showBSAlert(__warnCaption, "Please add topic name.", __DANGER);
            }
            tblShow();
        }
        return valid;
    });

    // jQuery button click event to remove a row.
    $('#tbody').on('click', '.remove', function () {
        var child = $(this).closest('tr').nextAll();
        child.each(function () {
            var id = $(this).attr('id');
            var idx = $(this).children('.row-index').children('div');
            var dig = parseInt(id.substring(4));
            //console.log("id : " + id + " : idx " + idx + " : dig " + dig);
            idx.html(`Row ${dig - 1}`);

            // Modifying row id.
            $(this).attr('id', `row_${dig - 1}`);
            $(this).find('.divTopicEditable').attr('id', `div_${dig - 1}`);
            //console.log(`Row ${dig - 1}` + " :" + `div_${dig - 1}`);
        });
        $(this).closest('tr').remove();
        rowIdx--;
        tblShow();
    });
}
function tblShow() {
    var rowCount = $("#tblTopics").find("#tbody tr").length;
    if (rowCount > 0) {
        $(".tblTopicsContainer").slideDown("slow");
    }
    else {
        $(".tblTopicsContainer").slideUp("fast");
    }
}
// End Add Post
var isEmailNotSame = false;
var emailArr = [];
function emailNotSame(_id) {
    if (!emailArr.includes($("#emailAddress_" + _id).val())) {
        emailArr[_id] = $("#emailAddress_" + _id).val();
    }
    else {
        var currentVal = $("#emailAddress_" + _id).val();
        if (currentVal == "") {
            emailArr.splice(_id, 0);
        }
        else {
            if (emailArr[_id] == $("#emailAddress_" + _id).val()) {
                //
            }
            else {
                $("#emailAddress_" + _id).val("");
                $("#alternateEmailAddress_" + _id).val("");
                isEmailNotSame = true;
                showBSAlert(__warnCaption, 'Email address entered cannot be same.', __WARNING);
            }
        }
    }
}
function numberOnly() {
    $('.numberonly').keypress(function (e) {
        var charCode = (e.which) ? e.which : e.keyCode;
        if (String.fromCharCode(charCode).match(/[^0-9]/g)) {
            return false;
        }
    });
}

function charOnlyFnameLname() {
    $('.charOnlyFNameLName').keypress(function (e) {
        var charCode = (e.which) ? e.which : e.keyCode;
        if (String.fromCharCode(charCode).match(/[^A-Za-z- ]/g)) {
            return false;
        }
    });
}

/* Start Add Meal */
var addRemoveMeal = {
    add: function () {
        var total_element = $(".MealBox").length;
        var lastid = $(".MealBox:last").attr("id");
        var split_id = lastid.split("_");
        var lastIDVal = Number(split_id[1]);
        var nextindex = lastIDVal + 1;
        if (lastIDVal <= 0)
            nextindex = lastIDVal - 1;
        else if (lastIDVal > 0)
            nextindex = -1;

        var max = 10;
        if (total_element < max) {
            $(".MealBox:last").after("<div class='MealBox' id='divMealBox_" + nextindex + "'></div>");
            var data = getMealHTML(nextindex)
            $("#divMealBox_" + nextindex).append(data);
        }

        //if ($(".MealBox").length == 3) {
        //	$(".btnAddStudent").css({ "display": "none" });
        //}
        //toggleLogo($("input[name=UserName]").val());
    },
    remove: function (objRemove, flag) {
        var id = $(objRemove).attr("id");
        if (id != "removeMealBox_1") {
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
                        $("#divMealBox_" + deleteindex).remove();
                        addRemoveMeal.addFirst();
                    }
                });
            }
            else {
                $("#divMealBox_" + deleteindex).remove();
                addRemoveMeal.addFirst();
                if ($(".MealBox").length != 3) {
                    console.log($(".MealBox").length);
                    $(".btnAddStudent").removeAttr("style");
                }
            }
        }
    },
    addFirst: function () {
        var lastid = $(".MealBox:last").attr("id");
        if (lastid === undefined) {
            $("._jsMainMealBox").append("<div class='MealBox' id='divMealBox_0'></div>");
            var data = getMealHTML(0)
            $("#divMealBox_0").append(data);
        }
    },
}
function getMealHTML(nextindex) {
    var data = "";
    data += '<div class="">';
    data += '   <input type="hidden" name="hdnMealBox_' + nextindex + '" value="' + nextindex + '">';

    if (nextindex != 0)
        data += '    <div class="col-md-4">';
    data += '		<div class="form-group">';
    data += '			<span class="meal-btndel pull-right student-btndel" onclick="addRemoveMeal.remove(this,0)" id="removeMealBox_' + nextindex + '" _id="' + nextindex + '"><i class="removeMealBox"><img src="/images/icon_close_notification.png" alt="" class="img" /></i></span>';
    data += '			<label>Food Name</label> <span class="starField">*</span><br />';
    data += '			<input type="text" name="foodName_' + nextindex + '" id="foodName_' + nextindex + '" value="" class="form-control foodNameValidate" placeholder="Food Name">';
    data += '		</div>';
    data += '    </div>';

    data += '<div class="clearfix"></div><div class="col-md-12"></div>';

    data += '</div>';
    return data;
}

/*function SubmitForm(formName) {
    var msg = "";
    var isValid = true;
    var validator_count = 0;

    if (formName == "frmAddUser") {
        if ($("#FirstName").val().trim() == "") {
            msg = msg + "First Name";
            isValid = false;
        }
        if ($("#LastName").val().trim() == "") {
            msg = msg + "\n" + "Last Name";
            isValid = false;
        }
        if ($("#UserName").val().trim() == "") {
            msg = msg + "\n" + "Email Address";
            isValid = false;
        }
        else if (!emailOnly($("#UserName").val().trim())) {
            msg = msg + "\n" + "Email Address";
            isValid = false;
        }
    }

    if (isValid) {
        if (formName == "frmAddUser") {
            //
        }
    }
    else if (msg != "") {
        showBSAlert(__requiredCaption, msg, __WARNING);
    }
    msg = "";
    validator_count = 0;
}
$.validator.addMethod("emailonly",

    function (value) {
        if ($.trim(value) == '')
            return true;
        else
            return /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(value);
    }
);*/

function getVoiceList(id) {
    $("#userID").val(id);
    $("#divLoader").append(getLoader());
    $.ajax({
        url: '/AddVoiceToUser/GetVoice',
        data: { "ID": id },
        type: "POST",
        datatype: "json",
        async: false,
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result) {
            BootstrapDialog.show({
                id: "divVoiceList",
                title: "Voice List",
                size: BootstrapDialog.SIZE_WIDE,
                message: function () {
                    var $message = $('<div id="divVoiceListInner" class="pTB10-LR05 box"></div>');
                    $message.append(result);
                    return $message;
                },
                closeByBackdrop: false,
                closable: true,
                //buttons: [
                //    {
                //        label: 'Ok',
                //        cssClass: 'btn-primary',
                //        action: function (dialogItself) {
                //            dialogItself.close();
                //        }
                //    }
                //],
                onshown: function (dialogRef) {
                    //
                    removeLoader("#divLoader");
                }
            });
        },
        complete: function () {
            setTimeout(function () {
                //selectAllVoiceToUser()
                removeLoader("#divLoader");
            }, 300);
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    })
}

function selectAllVoiceToUser() {    
    $('#select_all_voices').on('click', function () {
        console.log("Select All");
        $(".btnCopyToMyTopic").removeClass("disabledCustom");
        if (this.checked) {
            /*$('.checkbox_voice').each(function () {
                this.checked = true;
            });*/

            BootstrapDialog.show({
                id: "divSelectAllVoice",
                title: __WARNING,
                type: getDialogType(__WARNING),
                size: BootstrapDialog.SIZE_NORMAL,
                message: function () {
                    var $message = $('<div id="divSelectAllVoiceInner"></div>');
                    var result = '';
                    result += '<div>Are you sure you want to select all records!</div>';
                    $message.append(result);
                    return $message;
                },
                buttons: [
                    {
                        label: __ok,
                        cssClass: 'btn btn-primary',
                        action: function (dialog) {
                            $("#divLoader").append(getLoader());

                            $("#select_all_voices").prop("checked", true);
                            $('.checkbox_voice').each(function () {
                                this.checked = true;
                            });

                            removeLoader("#divLoader");
                            dialog.close();
                        }
                    },
                    {
                        label: __Cancel,
                        cssClass: 'btn btn-default',
                        action: function (dialogItself) {
                            $("#select_all_voices").prop("checked", false);
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

        } else {
            /*$('.checkbox_voice').each(function () {
                this.checked = false;
            });*/

            BootstrapDialog.show({
                id: "divSelectAllVoice",
                title: __WARNING,
                type: getDialogType(__WARNING),
                size: BootstrapDialog.SIZE_NORMAL,
                message: function () {
                    var $message = $('<div id="divSelectAllVoiceInner"></div>');
                    var result = '';
                    result += '<div>Are you sure you want to deselect all records!</div>';
                    $message.append(result);
                    return $message;
                },
                buttons: [
                    {
                        label: __ok,
                        cssClass: 'btn btn-primary',
                        action: function (dialog) {
                            $("#divLoader").append(getLoader());

                            $("#select_all_voices").prop("checked", false);
                            $('.checkbox_voice').each(function () {
                                this.checked = false;
                            });                            

                            removeLoader("#divLoader");
                            dialog.close();
                        }
                    },
                    {
                        label: __Cancel,
                        cssClass: 'btn btn-default',
                        action: function (dialogItself) {                            
                            $("#select_all_voices").prop("checked", true);
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
    });

    $('.checkbox_voice').on('click', function () {
        if ($('.checkbox_voice:checked').length == $('.checkbox_voice').length) {
            $('#select_all_voices').prop('checked', true);
            console.log("Select IF");
        } else {
            $('#select_all_voices').prop('checked', false);
            console.log("Select Else");
        }
    });

    var checkbox_voice = $(".checkbox_voice");
    checkbox_voice.click(function () {
        /*if ($(this).is(":checked")) {
            $(".btnCopyToMyTopic").removeClass("btnDisabled");
        } else {
            $(".btnCopyToMyTopic").addClass("btnDisabled");
        }*/
        $('.btnCopyToMyTopic').prop('disabled', !$('.checkbox_voice:checked').length);
    });
}

function unSelectAll() {
    $('.checkbox_voice').each(function () {
        this.checked = false;
    });
    $("#select_all_voices").prop("checked", false);
}
function AddVoiceToUser() {
    var userid = $("#userID").val();

    var fdata = new FormData();
    $('input[name="CheckedTopics"]:checked').each(function () {
        if ($(this).attr('id') !== undefined) {
            if ($(this).is(':checked')) {
                $(this).val(1);
            }
        }
    });
    $('#frmVoice input').each(function () {
        if ($(this).attr('id') != undefined) {
            fdata.append($(this).attr('id'), $(this).val());
        }
    });
    var userid = $("#userID").val();
    fdata.append("UserID", userid);

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

function Checkbox() {
    $('input[name="CheckedTopics"]').each(function () {
        if ($(this).is(':checked')) {
            $(this).val(1);
        } else {
            $(this).val(0);
        }
    });
}

$.validator.addMethod("emailonly",
    function (value) {
        if ($.trim(value) == '')
            return true;
        else
            return /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(value);
    }
);

function imgSizeAndResolution(_id) {
    var _URL = window.URL || window.webkitURL;

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
            $("#" + _id).parent().parent().find(".imgLogo").attr("src", "");
            showBSAlert(__requiredCaption, "Invalid file format: " + file.type, __WARNING);
        };
        img.src = _URL.createObjectURL(file);
    }
}