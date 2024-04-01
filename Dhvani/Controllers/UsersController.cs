using Core.Business.BusinessFacade;
using Core.Entity;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Entity.ViewModel;
using Core.Models;
using Core.Resources;
using Core.Utility.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

//using Microsoft.VisualStudio.Web.CodeGeneration;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Dhvani.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class UsersController : Controller
    {
        private readonly string _module = "GyanAkaadamee.Controllers.UsersController";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IHostingEnvironment _hostingEnvironment;
        private IMemoryCache _cache;

        private ISession _session => _httpContextAccessor.HttpContext.Session;

        private Users objUserEntity = new Users();
        private UsersBusinessFacade objUserBusinessFacade = new UsersBusinessFacade();
        private ResetPassword objCP = new ResetPassword();
        private JsonMessage _jsonMessage = null;
        private Helper _helper;
        private LoginVM _loginVM = null;

        public UsersController(IHostingEnvironment environment = null, IHttpContextAccessor httpContextAccessor = null, IMemoryCache cache = null)
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
            _loginVM = _helper.GetSession();
        }

        public IActionResult Index()
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liUsers.ToString();
            return View();
        }

        #region Impersonation

        public JsonResult Impersonation(string username, string currentUser = null)
        {
            //get password, dicrypt it then call _jsonMessage = IsLoginValid(username, password, LoginMode.CMS);

            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    Users objUserEntity = new Users();
                    UsersBusinessFacade objUsersBusinessFacade = new UsersBusinessFacade();
                    bool isRemember = true;
                    LoginVM loginVM = new LoginVM();

                    string usernameImpersonations = _httpContextAccessor.HttpContext?.User.Identity.Name;

                    _jsonMessage = objUsersBusinessFacade.IsUsernameExistClick(username);

                    objUserEntity = (Users)_jsonMessage.Data;

                    _jsonMessage = IsLoginValid(username, objUserEntity.Password, LoginMode.CMS);

                    if (loginVM != null)
                    {
                        if (_jsonMessage.IsSuccess)
                        {
                            if (isRemember)
                            {
                                SetCookie("UserName", username, Convert.ToInt32(ConstantsCommon.LoginCookie));
                                //SetCookie("ReturnURL", _jsonMessage.ReturnUrl, Convert.ToInt32(GyanAkaadameeConstants.LoginCookie));
                            }
                            objUserEntity = (Users)_jsonMessage.Data;

                            if (isRemember)
                            {
                                SetCookie("UserRoleID", objUserEntity.RoleId.ToString(), Convert.ToInt32(ConstantsCommon.LoginCookie));
                            }

                            if (objUserEntity != null)
                            {
                                _helper.SetSession(objUserEntity);
                            }
                            _helper.SetUserLanguage(objUserEntity.LanguageId);
                            InsertAccessMember(objUserEntity.ID, "Login", getAccessMember());

                            string Impersonation_Users = null;

                            Impersonation_Users = _httpContextAccessor.HttpContext.Session.GetString("Impersonation_Users");

                            if (Impersonation_Users.Contains(username))
                            {
                                int index = Impersonation_Users.IndexOf(username + ",");
                                Impersonation_Users = Impersonation_Users.Substring(0, index);
                            }

                            Impersonation_Users += username + ",";
                            _httpContextAccessor.HttpContext.Session.SetString("Impersonation_Users", Impersonation_Users);

                            if (objUserEntity.RoleId == 1)
                            {
                                _jsonMessage.ReturnUrl = ConstantsCommon.Domain + ConstantsCommon.SuperAdminDefaultController + "/" + ConstantsCommon.SuperAdminDefaultView + "";
                            }
                            else if (objUserEntity.RoleId == 2)
                            {
                                _jsonMessage.ReturnUrl = ConstantsCommon.Domain + ConstantsCommon.AdminDefaultController + "/" + ConstantsCommon.AdminDefaultView + "";
                            }
                            else if (objUserEntity.RoleId == 3)
                            {
                                _jsonMessage.ReturnUrl = ConstantsCommon.Domain + ConstantsCommon.UserDefaultController + "/" + ConstantsCommon.UserDefaultView + "";
                            }
                            else
                            {
                                _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_msg_invalidEmailAddressPassword, KeyEnums.JsonMessageType.ERROR);
                            }
                        }
                        else
                        {
                            _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_msg_invalidEmailAddressPassword, KeyEnums.JsonMessageType.ERROR);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Impersonation()", ex.Source, ex.Message, ex);
            }

            return Json(_jsonMessage);
        }

        #endregion Impersonation

        #region Change Status

        public JsonResult ChangeStatus(Int64 ID, string StatusID)
        {
            try
            {
                if (ID > 0)
                {
                    UsersBusinessFacade objBF = new UsersBusinessFacade();
                    _jsonMessage = objBF.ChangeStatus(ID, StatusID);
                }
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER);
                Log.WriteLog(_module, "ChangeStatus(ID=" + ID + ",StatusID=" + StatusID + ")", ex.Source, ex.Message, ex);
            }
            return Json(_jsonMessage);
        }

        #endregion Change Status

        #region Login Related Methods

        public IActionResult Login()
        {
            _helper = new Helper(_httpContextAccessor);
            _loginVM = _helper.GetSession();

            if (objUserEntity.RoleId == 1)
            {
                _jsonMessage.ReturnUrl = ConstantsCommon.Domain + ConstantsCommon.SuperAdminDefaultController + "/" + ConstantsCommon.SuperAdminDefaultView + "";
            }
            if (objUserEntity.RoleId == 2)
            {
                _jsonMessage.ReturnUrl = ConstantsCommon.Domain + ConstantsCommon.AdminDefaultController + "/" + ConstantsCommon.AdminDefaultView + "";
            }
            if (objUserEntity.RoleId == 3)
            {
                _jsonMessage.ReturnUrl = ConstantsCommon.Domain + ConstantsCommon.UserDefaultController + "/" + ConstantsCommon.UserDefaultView + "";
            }

            return View();
        }

        [HttpPost]
        public JsonResult Login(string username, string password, string queryString, bool isRemember, bool autologin)
        {
            _helper.KillSession();

            try
            {
                SetCookie("RememberMe", isRemember.ToString().ToLower(), Convert.ToInt32(ConstantsCommon.LoginCookie));

                LoginVM loginVM = new LoginVM();

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_msg_emailPasswordRequired, KeyEnums.JsonMessageType.ERROR);
                }
                else
                {
                    password = new Encription().Encrypt(password);
                    _jsonMessage = IsLoginValid(username, password, LoginMode.CMS);

                    //

                    if (!_jsonMessage.IsSuccess)
                    {
                        password = new Encription().Decrypt(password);
                        _jsonMessage = IsLoginValid(username, password, LoginMode.CMSTEACHER);

                        if (!_jsonMessage.IsSuccess)
                        {
                            _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_msg_loginFailed, KeyEnums.JsonMessageType.ERROR);
                        }
                    }

                    if (loginVM != null)
                    {
                        if (_jsonMessage.IsSuccess)
                        {
                            if (isRemember)
                            {
                                SetCookie("UserName", username, Convert.ToInt32(ConstantsCommon.LoginCookie));
                            }
                            objUserEntity = (Users)_jsonMessage.Data;

                            if (isRemember)
                            {
                                SetCookie("UserRoleID", objUserEntity.RoleId.ToString(), Convert.ToInt32(ConstantsCommon.LoginCookie));
                            }

                            if (objUserEntity != null)
                            {
                                _helper.SetSession(objUserEntity);
                            }
                            _helper.SetUserLanguage(objUserEntity.LanguageId);
                            InsertAccessMember(objUserEntity.ID, "Login", getAccessMember());

                            string Impersonation_Users = null;
                            Impersonation_Users = _httpContextAccessor.HttpContext.Session.GetString("Impersonation_Users");

                            Impersonation_Users += username + ",";
                            _httpContextAccessor.HttpContext.Session.SetString("Impersonation_Users", Impersonation_Users);

                            if (objUserEntity.RoleId == 1 || objUserEntity.RoleId == 2)
                            {
                                _jsonMessage.ReturnUrl = ConstantsCommon.Domain + ConstantsCommon.DefaultController + "/" + ConstantsCommon.DefaultView + "";
                            }
                            else if (objUserEntity.RoleId == 3)
                            {
                                _jsonMessage.ReturnUrl = ConstantsCommon.Domain + ConstantsCommon.UserDefaultController + "/" + ConstantsCommon.UserDefaultView + "";
                            }
                        }
                        else
                        {
                            _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_msg_invalidEmailAddressPassword, KeyEnums.JsonMessageType.ERROR);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "username", string.Format("Method : Login(), Source : {0}, Message {1}", ex.Source, ex.Message));
            }
            return Json(_jsonMessage);
        }

        public ActionResult Logout()
        {
            try
            {
                string UserName = string.Empty;
                if (_loginVM.Id > 0 || _loginVM != null)
                    UserName = _loginVM.UserName;

                UserLogOut();

                return RedirectToAction("Login", "Users");
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Logout()", ex.Source, ex.Message, ex);
                return RedirectToAction("Login", "Users");
            }
        }

        private void UserLogOut()
        {
            ClearCache();//Delete the user details from cache.
            _httpContextAccessor.HttpContext.Session.Clear();

            if (_httpContextAccessor.HttpContext.Request.Cookies["LoginData"] != null)
            {
                Response.Cookies.Delete("LoginData");
            }
            if (_httpContextAccessor.HttpContext.Request.Cookies["UserLanguage"] != null)
            {
                Response.Cookies.Delete("UserLanguage");
            }
        }

        private void ClearCache()
        {
            _cache.Remove("_userId" + _session.Id + "_" + "true");
        }

        public void SetCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime > 0)
                option.Expires = DateTime.Now.AddDays(double.Parse(expireTime.ToString()));
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }

        public JsonMessage IsLoginValid(string username, string password, string LoginMode = "")
        {
            Users objUserEntity = new Users();
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    _jsonMessage = new JsonMessage(false, @Resource.lbl_msg_invalidEmailAddress, @Resource.lbl_msg_invalidEmailAddress, KeyEnums.JsonMessageType.DANGER);
                else if (string.IsNullOrWhiteSpace(password))
                    _jsonMessage = new JsonMessage(false, @Resource.lbl_msg_invalidPassowrd, @Resource.lbl_msg_invalidPassowrd, KeyEnums.JsonMessageType.DANGER);
                else
                {
                    string[] Fieldsname = new string[2];
                    string[] Values = new string[2];
                    Fieldsname[0] = username;
                    Fieldsname[1] = password;
                    Values[0] = username;
                    Values[1] = password;

                    UsersBusinessFacade objUsersBusinessFacade = new UsersBusinessFacade();
                    objUserEntity = objUsersBusinessFacade.Authenticate(username, password, LoginMode);

                    if (objUserEntity != null)
                    {
                        if (objUserEntity.StatusId == (byte)StateEnums.Statuses.Active)
                        {
                            _jsonMessage = new JsonMessage(true, @Resource.lbl_Cap_success, @Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, "strUrl", "true", objUserEntity);
                        }
                        else if (objUserEntity.StatusId == (byte)StateEnums.Statuses.Pending)
                        {
                            _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_msg_accountNotActivated, KeyEnums.JsonMessageType.FAILURE, "/User/Login");
                        }
                        else if (objUserEntity.StatusId == (byte)StateEnums.Statuses.InActive)
                        {
                            _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_accountDisabled, KeyEnums.JsonMessageType.FAILURE, "/User/Login");
                        }
                        else if (objUserEntity.StatusId == (byte)StateEnums.Statuses.Deleted)
                        {
                            _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_accountDeleted, KeyEnums.JsonMessageType.FAILURE, "/User/Login");
                        }
                        else if (objUserEntity.StatusId == (byte)StateEnums.Statuses.Active && objUserEntity.IsEmailVerified == (byte)StateEnums.Statuses.Active)
                        {
                            _jsonMessage = new JsonMessage(true, @Resource.lbl_Cap_success, @Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, "StrUrl", "true", objUserEntity);
                        }
                        else
                        {
                            _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_msg_loginFailed, KeyEnums.JsonMessageType.ERROR);
                        }
                    }
                    else
                        _jsonMessage = new JsonMessage(false, @Resource.lbl_error, @Resource.lbl_msg_invalidEmpIdAddressPassword, KeyEnums.JsonMessageType.ERROR);
                }
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, @Resource.lbl_msg_internalServerErrorOccurred, @Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.ERROR, ex.Message);
                Log.WriteLog(_module, "IsLoginValid(username=" + username + ", password=" + password + ")", ex.Source, ex.Message, ex);
            }

            return _jsonMessage;
        }

        public void InsertAccessMember(Int64 ID, string clickedby, AccessMember objAccessMember)
        {
            try
            {
                AccessMember objAM = new AccessMember();
                objAM.UserID = Convert.ToInt32(ID);
                objAM.Url = objAccessMember.Url;
                objAM.ReferrerURL = objAccessMember.ReferrerURL;

                objAM.port = objAccessMember.port;
                objAM.Host = objAccessMember.Host;
                objAM.REMOTE_HOST = objAccessMember.REMOTE_HOST;
                objAM.REMOTE_ADDR_IP = objAccessMember.REMOTE_ADDR_IP;
                objAM.Useragent = objAccessMember.Useragent;
                objAM.BrowserType = objAccessMember.BrowserType;
                objAM.BrowserVersion = objAccessMember.BrowserType;
                objAM.Platform = objAccessMember.Platform;

                objAM.ClickedBy = clickedby;
                objAM.DeviceName = objAccessMember.DeviceName;
                objAM.DeviceType = objAccessMember.DeviceType;
                objAM.OperatingSystem = objAccessMember.OperatingSystem;
                objAM.DeviceModel = objAccessMember.DeviceModel;
                objAM.Build = objAccessMember.Build;
                objAM.Version = objAccessMember.Version;
                AccessMemberBusinessFacade objAccessMemberBusinessFacade = new AccessMemberBusinessFacade();
                var AMID = objAccessMemberBusinessFacade.Save(objAM);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "InsertAccessMember(ID: " + ID + ", clickedby: " + clickedby + ")", ex.Source, ex.Message);
            }
        }

        #endregion Login Related Methods

        #region Reset Password

        [HttpGet]
        [ActionName("ResetPassword")]
        public ActionResult ResetPassword(string ui, string rd)
        {
            try
            {
                if (!ValidateInput(ui, rd))
                    return RedirectToAction("Login", "Users");
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "ResetPassword(UserID=" + objCP.UserID + ")", ex.Source, ex.Message, ex);
            }
            return View(objCP);
        }

        [HttpPost]
        [ActionName("UpdateResetPassword")]
        public ActionResult UpdateResetPassword(IFormCollection objForm)
        {
            try
            {
                objCP.UserID = Convert.ToInt32(Convert.ToString(objForm["UID"]));
                objCP.PassResetCode = Convert.ToString(objForm["Token"]);

                UpdatePassword(Convert.ToString(objForm["NewPassword"]));

                ResetPassword objRS = new ResetPassword();
                objRS.UserID = objCP.UserID;
                objRS.PassResetCode = objCP.PassResetCode;
                ResetPasswordBusinessFacade objResetPassBusinessFacade = new ResetPasswordBusinessFacade();
                objResetPassBusinessFacade.Delete(objRS);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "UpdateResetPassword(UserID=" + objCP.UserID + ")", ex.Source, ex.Message, ex);
            }

            //return Json(objCP.UserID, JsonRequestBehavior.AllowGet);
            return Json(objCP.UserID);
        }

        private void UpdatePassword(string NewPassword)
        {
            try
            {
                NewPassword = new Core.Utility.Common.Encription().Encrypt(NewPassword);
                objUserEntity = new Users();
                objUserEntity.ID = objCP.UserID;
                objUserEntity.Password = NewPassword;
                objUserBusinessFacade.UpdatePassword(objUserEntity);
                objCP.HdnOldPassword = objUserEntity.Password;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "UpdatePassword(UserID=" + objCP.UserID + ")", ex.Source, ex.Message, ex);
            }
        }

        public JsonResult IsUsernameExist(string username)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    _jsonMessage = objUserBusinessFacade.IsUserIdExist(username, 0);
                    Users obj = new Users();
                    obj = (Users)_jsonMessage.Data;
                    if (obj != null)
                    {
                        if (obj.RoleId == 5)
                        {
                            _jsonMessage = new JsonMessage(false, @Resource.lbl_error, "Username In Use", KeyEnums.JsonMessageType.ERROR, obj);
                        }
                    }
                }
                else
                    _jsonMessage = new JsonMessage(false, @Resource.lbl_error, Resource.lbl_internalServerErrorOccurred, KeyEnums.JsonMessageType.ERROR);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "IsUsernameExist(username=" + username + ")", ex.Source, ex.Message, ex);
            }
            return Json(_jsonMessage);
        }

        private bool ValidateInput(string ui, string rd)
        {
            bool isValid = true;
            try
            {
                int uid;
                isValid = Int32.TryParse(ui, out uid);

                if (isValid)
                {
                    objCP.UserID = uid;

                    objCP.PassResetCode = rd;
                    if (GetUserRecord())
                        isValid = IsTokenValid();
                    else
                        isValid = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "ValidateInput(ui=" + ui + ",rd=" + rd + ")", ex.Source, ex.Message, ex);
            }
            return isValid;
        }

        private bool IsTokenValid()
        {
            bool isValidToken = true;
            try
            {
                ResetPassword objRP = new ResetPassword();

                objRP.UserID = objCP.UserID;
                objRP.PassResetCode = objCP.PassResetCode;
                ResetPasswordBusinessFacade objResetPassBusinessFacade = new ResetPasswordBusinessFacade();
                return objResetPassBusinessFacade.GetResetPassDetails(objRP);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "IsTokenValid(Token:" + objCP.PassResetCode + ")", ex.Source, ex.Message, ex);
            }
            return isValidToken;
        }

        private bool GetUserRecord()
        {
            try
            {
                objUserEntity = new Users();
                objUserEntity.ID = objCP.UserID;

                objUserEntity = objUserBusinessFacade.GetRecords(Convert.ToInt32(objCP.UserID));
                objCP.HdnOldPassword = (objUserEntity != null ? objUserEntity.Password : "");
                objCP.HdnOldPassword = objCP.HdnOldPassword.Trim();
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetUserRecord(UserID=" + objCP.UserID + ")", ex.Source, ex.Message, ex);
            }

            return true;
        }

        public AccessMember getAccessMember()
        {
            AccessMember objAM = new AccessMember();
            try
            {
                // Retrieve role ID from session

                string roleId = _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString()) ?? "";

                objAM.Url = _httpContextAccessor.HttpContext.Request.Host + _httpContextAccessor.HttpContext.Request.Path + _httpContextAccessor.HttpContext.Request.QueryString.Value;
                objAM.ReferrerURL = Request.Headers["Referer"].ToString();

                objAM.port = Response.Headers["Alt-Svc"].ToString();
                objAM.Host = Request.Headers["Origin"].ToString();

                objAM.REMOTE_ADDR_IP = HttpContext.Connection.RemoteIpAddress.ToString();
                objAM.Useragent = Request.Headers["User-Agent"].ToString();
                objAM.BrowserVersion = Request.Headers["Sec-Ch-Ua"].ToString();
                objAM.Platform = Request.Headers["Sec-Ch-Ua-Platform"].ToString();

                if (_httpContextAccessor.HttpContext.GetServerVariable("REMOTE_HOST") != null)
                    objAM.REMOTE_HOST = _httpContextAccessor.HttpContext.GetServerVariable("REMOTE_HOST");
                else
                    objAM.REMOTE_HOST = _httpContextAccessor.HttpContext.GetServerVariable("HTTP_X_FORWARDED_FOR") ?? _httpContextAccessor.HttpContext.GetServerVariable("REMOTE_ADDR");

                objAM.REMOTE_ADDR_IP = _httpContextAccessor.HttpContext.GetServerVariable("REMOTE_ADDR") != null ? _httpContextAccessor.HttpContext.GetServerVariable("REMOTE_ADDR") : "";

                //objAM.RoleID = roleId;
                //objAM.Url = _httpContextAccessor.HttpContext.Request.Host + _httpContextAccessor.HttpContext.Request.Path + _httpContextAccessor.HttpContext.Request.QueryString.Value;
                //objAM.ReferrerURL = Request.Headers["Referer"].ToString();

                //objAM.port = Response.Headers["Alt-Svc"].ToString();
                //objAM.Host = Request.Headers["Origin"].ToString();

                //objAM.REMOTE_ADDR_IP = HttpContext.Connection.RemoteIpAddress.ToString();

                //objAM.Useragent = Request.Headers["User-Agent"].ToString();                  //User-Agent
                //                                                                             //objAM.BrowserType = websockets.WebSocketRequestedProtocols.Where(x => x.)
                //                                                                             // objAM.BrowserVersion =

                //objAM.BrowserVersion = Request.Headers["Sec-Ch-Ua"].ToString();
                //objAM.Platform = Request.Headers["Sec-Ch-Ua-Platform"].ToString();

                //if (_httpContextAccessor.HttpContext.GetServerVariable("REMOTE_HOST") != null)
                //    objAM.REMOTE_HOST = _httpContextAccessor.HttpContext.GetServerVariable("REMOTE_HOST");
                //else
                //    objAM.REMOTE_HOST = _httpContextAccessor.HttpContext.GetServerVariable("HTTP_X_FORWARDED_FOR") ?? _httpContextAccessor.HttpContext.GetServerVariable("REMOTE_ADDR");

                //objAM.REMOTE_ADDR_IP = _httpContextAccessor.HttpContext.GetServerVariable("REMOTE_ADDR") != null ? _httpContextAccessor.HttpContext.GetServerVariable("REMOTE_ADDR") : "";
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "getAccessMember()", ex.Source, ex.Message, ex);
            }
            return objAM;
        }

        #endregion Reset Password

        #region Send Mail Method

        public JsonResult ForgotPassword(string username, string loginMode = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    CommonData _CommonData = new CommonData(_httpContextAccessor);
                    _jsonMessage = objUserBusinessFacade.IsUsernameExistClick(username);
                    if (_jsonMessage.Data is not null)
                    {
                        objUserEntity = (Users)_jsonMessage.Data;
                    }

                    if (objUserEntity.ID == 0 && objUserEntity.AlternateEmail == null)
                    {
                        _jsonMessage = objUserBusinessFacade.IsUserIdExist(username, 0);

                        objUserEntity = (Users)_jsonMessage.Data;
                    }

                    if (objUserEntity.ID == 0 && objUserEntity.AlternateEmail == null)
                    {
                        _jsonMessage = objUserBusinessFacade.IsUserIdExist(username, 0);

                        objUserEntity = (Users)_jsonMessage.Data;
                    }

                    if (objUserEntity.ID > 0 && objUserEntity.UserName != "")
                    {
                        switch (objUserEntity.StatusId)
                        {
                            case (byte)StateEnums.Statuses.Active:
                            case (byte)StateEnums.Statuses.Pending:
                                string UId = objUserEntity.ID.ToString();
                                string userEmail = objUserEntity.UserName.ToString();

                                string FullName = !string.IsNullOrWhiteSpace(objUserEntity.FirstName) ? objUserEntity.LastName : "";
                                FullName = FullName.Trim();

                                string hostAddress = ConstantsCommon.Domain;

                                string token = RandomGeneratorCustom.Generate(6);
                                string Carbon_Copy = ConstantsCommon.Account_Confirmation;

                                string url = hostAddress + "Users/ResetPassword?ui=" + UId + "&rd=" + token;
                                Short_UrlBusinessFacade objShortUrlBusinessFacade = new Short_UrlBusinessFacade();

                                string shortAddress = objShortUrlBusinessFacade.MakeShortURL(url);

                                shortAddress += "Users/ResetPassword?ui=" + UId + "&rd=" + token;

                                string ResetPassLink = "<a href='" + shortAddress + "'>" + shortAddress + "</a>";

                                string tempPath = null;
                                string mailTemplate = null;
                                tempPath = _hostingEnvironment.WebRootPath + "\\files\\template\\Mail_ResetPassword.html";

                                string WEB_LINK = "<a href=\"" + ConstantsCommon.shortURL.TrimEnd('/') + "\">" + ConstantsCommon.shortURL.TrimEnd('/') + "</a>";
                                FullName = objUserEntity.FirstName + " " + objUserEntity.LastName;

                                // Path to the HTML file
                                string filePath = _hostingEnvironment.WebRootPath + "\\files\\template\\Mail_ResetPassword.html";

                                // Read the html file
                                mailTemplate = System.IO.File.ReadAllText(filePath);

                                //mailTemplate = CommonUtilities.CommonHelper.LoadHTML(tempPath);
                                mailTemplate = mailTemplate.Replace("[User]", FullName).Replace("[Password_Reset_Link]", ResetPassLink).Replace("[Website/Service]", ConstantsCommon.AppName);

                                Email email = new Email(ConstantsCommon.DefaultmailID, userEmail);
                                email.ReplyTo = ConstantsCommon.ReplyToEmail;
                                email.BCC = ConstantsCommon.Account_Confirmation;
                                email.Subject = email.GetSubject(mailTemplate);
                                email.IsBodyHTML = true;
                                email.Body = mailTemplate;

                                if (email.Send())
                                {
                                    ResetPassword resetPass = new ResetPassword();
                                    ResetPasswordBusinessFacade objResetPassBusinessFacade = new ResetPasswordBusinessFacade();
                                    resetPass.UserID = objUserEntity.ID;
                                    resetPass.PassResetCode = token;

                                    objResetPassBusinessFacade.Save(resetPass);

                                    _jsonMessage = new JsonMessage(true, Resource.lbl_Cap_information, Resource.lbl_msg_passwordChangeEmail, KeyEnums.JsonMessageType.INFO);
                                }
                                else
                                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_emailAddressFailed, KeyEnums.JsonMessageType.ERROR);

                                break;

                            case (byte)StateEnums.Statuses.InActive:
                                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_accountDisabled, KeyEnums.JsonMessageType.FAILURE);
                                break;

                            case (byte)StateEnums.Statuses.Deleted:
                                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_accountNotActivated, KeyEnums.JsonMessageType.FAILURE);
                                break;

                            default:
                                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_invalidEmailAddress, KeyEnums.JsonMessageType.ERROR);
                                break;
                        }
                    }
                    else
                        _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_accountDoesNotExit, KeyEnums.JsonMessageType.ERROR);
                }
                else
                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_eMailAddressRequired, KeyEnums.JsonMessageType.ERROR);
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, ex.Message);
                Log.WriteLog(_module, "ForgotPassword(username=" + username + ")", ex.Source, ex.Message, ex);
            }
            return Json(_jsonMessage);
        }

        #endregion Send Mail Method

        #region User Validation

        /// <summary>
        /// Add User to check the username is exist or not based on id
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public JsonResult IsUsernameExistClick(string username)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    _jsonMessage = objUserBusinessFacade.IsUsernameExistClick(username);
                    Users obj = new Users();
                    obj = (Users)_jsonMessage.Data;
                    if (obj != null)
                    {
                        if (obj.RoleId == 3)
                        {
                            _jsonMessage = new JsonMessage(false, @Resource.lbl_error, "Username In Use", KeyEnums.JsonMessageType.ERROR, obj);
                        }
                    }
                }
                else
                    _jsonMessage = new JsonMessage(false, @Resource.lbl_error, Resource.lbl_internalServerErrorOccurred, KeyEnums.JsonMessageType.ERROR);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "IsUsernameExist(username=" + username + ")", ex.Source, ex.Message, ex);
            }
            return Json(_jsonMessage);
        }

        /// <summary>
        ///  Edit User to check the username is exist or not
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public JsonResult IsUsernameExistEditClick(Int64 id, string userName)
        {
            try
            {
                if (id != 0)
                {
                    _jsonMessage = objUserBusinessFacade.IsUsernameExistEditClick(id, userName);
                    Users obj = new Users();
                    obj = (Users)_jsonMessage.Data;
                    if (obj != null)
                    {
                        if (obj.RoleId == 5)
                        {
                            _jsonMessage = new JsonMessage(false, @Resource.lbl_error, "Username In Use", KeyEnums.JsonMessageType.ERROR, obj);
                        }
                    }
                }
                else
                    _jsonMessage = new JsonMessage(false, @Resource.lbl_error, Resource.lbl_internalServerErrorOccurred, KeyEnums.JsonMessageType.ERROR);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "IsUsernameExistEditClick(id=" + id + ")", ex.Source, ex.Message, ex);
            }
            return Json(_jsonMessage);
        }

        #endregion User Validation
    }
}