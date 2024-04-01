var isValid = true;
var isFormValid = true;
var isOldNewDiff = true;
var isNewPassLengthGood = false;
var isPasswordConfirmPassword = true;
var isOldPasswordValid = true;
var isPasswordCheck = true;
function PostForm(obj) {
    var pageurl = '';
    var redirectURL = '';
    if (obj.name == "frmCP") {
        //pageurl = "/ChangePassword/Index";
        pageurl = "/Users/UpdateChangePassword";
    }
    else if (obj.name == "frmRP") {
        pageurl = "/Users/UpdateResetPassword";
        redirectURL = "/Users/Logout";
    }
    else
        return false;
    $('#HdnOldPassword').val($.trim($('#HdnOldPassword').val()));
    $("#PASSWORD").val($.trim($("#PASSWORD").val()));
    $("#NewPassword").val($.trim($("#NewPassword").val()));
    $("#ConfirmPassword").val($.trim($("#ConfirmPassword").val()));

    isFormValid = $(obj).valid();

    if (obj.name == "frmCP" && ($('#HdnOldPassword').val() != ($("#PASSWORD").val())))
        isOldPasswordValid = false;
    else
        isOldPasswordValid = true;

    if (obj.name == "frmCP" && ($("#PASSWORD").val() != "" && ($("#PASSWORD").val() == $("#NewPassword").val())))
        isOldNewDiff = false;
    else
        isOldNewDiff = true;

    if ($("#NewPassword").val().length < 6)
        isNewPassLengthGood = false;
    else
        isNewPassLengthGood = true;

    if ($('#NewPassword').val() != $('#ConfirmPassword').val())
        isPasswordConfirmPassword = false;
    else
        isPasswordConfirmPassword = true;
    if (!PasswordCheck($("#NewPassword").val()))
        isPasswordCheck = false;
    else
        isPasswordCheck = true;

    if (!isOldNewDiff || !isFormValid || !isNewPassLengthGood || !isPasswordConfirmPassword || !isOldPasswordValid || !isPasswordCheck) {
        isValid = false;
    }
    else {
        isValid = true;
    }

    if (isValid) {
        $("#divFPBox").append(getLoader());
        //$("#divFPBox").removeClass("box").addClass("box").append(getLoader());
        $.ajax({
            url: pageurl,
            type: "POST",
            data: $("#" + obj.name).serialize(),
            dataType: "json",
            success: function (data) {
                //removeLoader("#divFPBox");
                var newPass = $.trim($("#NewPassword").val());
                $("#HdnOldPassword").val(data);
                $("#PASSWORD").val('');
                $("#NewPassword").val('');
                $("#ConfirmPassword").val('');

                if (redirectURL == '') {
                    showBSAlert(__success, "<div class='pTB10-LR05'>" + __msg_yourPassHasBeenChanged + "</div>", __SUCCESS);
                    site.hideLogReg();
                }
                else {
                    //showBSAlert(__success, "<div class='pTB10-LR05'>" + __msg_yourPassHasBeenChanged + "</div>", __SUCCESS, false, redirectURL);
                    BootstrapDialog.show({
                        id: "divImpersonation",
                        title: __success,
                        type: getDialogType(__SUCCESS),
                        size: BootstrapDialog.SIZE_NORMAL,
                        message: function () {
                            var $message = $('<div id="divImpersonationCh" class="box box-no-border-shadow pTB10-LR05" style="margin:0px;"></div>');

                            var result = '';
                            result += __msg_yourPassHasBeenChanged;
                            result += '</div>';
                            $message.append(result);
                            return $message;
                        },
                        buttons: [
                            {
                                label: __ok,
                                cssClass: 'btn btn-primary',
                                action: function (dialog) {
                                    location.href = window.location.origin + '/Users/Login';
                                    dialog.close();
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
            },
            complete: function () {
                setTimeout(function () { //code added by Vikas
                    removeLoader("#divFPBox");
                }, 300);
            }
        });
        return false;
    }
    else {
        ValidateForm();
        return false;
    }
}
function SubmitPasswordForm(frm) {
    if (frm && frm != '')
        $("#frmRP").submit();
    else
        $("#frmCP").submit();
}

function ValidateForm() {
    var msg = "";
    if (!isOldPasswordValid)
        msg = msg + __msg_invalidOldPass + "\n";
    if (!isOldNewDiff)
        msg = msg + __msg_yourNewPassCannotBeSameAsYourPreviousPass + " \n";
    if (!isPasswordCheck)
        msg = msg + __passwordShouldContain + "\n";
    //if (!isNewPassLengthGood)
    //    msg = msg + __msg_minimumSixCharactersRequiredForPassword + " \n";
    if (!isFormValid && $(".validation-summary-errors")) {
        if ($(".validation-summary-errors").text() != "") {
            for (var i = 0; i < $(".validation-summary-errors").find("li").length; i++) {
                msg = msg + $($(".validation-summary-errors").find("li")[i]).text() + '\n';
            }
        }
    }
    if (!isPasswordConfirmPassword)
        msg = msg + __msg_passwordAndConfirmPassShouldBeSame + "\n";
    if (msg != "")
        showBSAlert(__msglogRegWarning, "<div class='pTB10-LR05'>" + msg + "</div>", __DANGER)
}

function jsAvoidSpace(event) {
    var k = event ? event.which : window.event.keyCode;
    if (k == 32) return false;
}

function PasswordCheck(password) {
    if (password.length < 7)
        return false;
    else if (password.length > 15)
        return false;
    else
        return true;
    //return /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{7,15}$/.test(value);//atleast 1 alpha, 1 digit & 1 special
}