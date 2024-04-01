using Core.Business.BusinessFacade;
using Core.Entity;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Models;
using Core.Resources;
using Core.Utility.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Dhvani.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PronunciationController : Controller
    {
        private readonly string _module = "Core.Controllers.App.PronunciationController";
        private JsonMessage _jsonMessage = null;
        private JsonMessageApi _jsonMessageApi = null;

        private JsonMessageDTO jsonResponseMessageDTO = null;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private IMemoryCache _cache;
        private Helper _helper;
        private string RootPath = ConstantsCommon.RootPath;
        private string AudioPath = ConstantsCommon.AudioPath;
        private string SSMLPath = ConstantsCommon.SSMLPath;
        private string SSMLPathUrl = ConstantsCommon.SSMLPathUrl;
        private string IndiaApiUrl = ConstantsCommon.IndiaApiUrl;
        private string CoreDomain = ConstantsCommon.CoreDomain;

        //private string _userTransactionUUID = null;
        private Int64 _userID = 0;

        public PronunciationController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
        }

        [Authorize]
        [HttpPost]
        [Route("setpronunciation")]
        public dynamic setpronunciation(SetPronunciation pronunciation)
        {
            string UserAPIKey = "";
            Int64 revsponse = 0;
            Int64 isSuccess = 0;
            try
            {
                Log.WriteInfoLogWithoutMail(_module, "SetPronunciation()", "INFOLOG ", pronunciation.ToString());
            }
            catch (Exception ex)
            {
            }
            try
            {
                UserAPIKey = GetJWTUserAuthenticationHeaderValue();

                string strUserID = "";
                Users objEntity = new Users();
                if (UserAPIKey == "cms" || UserAPIKey == "cmsssml")
                {
                    //get the user details from the session
                    strUserID = _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) : "";

                    if (!string.IsNullOrEmpty(strUserID))
                    {
                        _userID = Convert.ToInt64(strUserID);
                    }

                    if (string.IsNullOrWhiteSpace(strUserID))
                    {
                        //jsonResponseMessageDTO = new JsonMessageDTO(false, Resource.lbl_error, "Invalid User", KeyEnums.JsonMessageType.ERROR);
                        jsonResponseMessageDTO = new JsonMessageDTO("", "", KeyEnums.StatusKeys.error.ToString(), "Invalid User.");
                        return Json(_jsonMessage);
                    }
                }
                else
                {
                    strUserID = "0";

                    UsersBusinessFacade objBF = new UsersBusinessFacade();

                    objEntity = objBF.GetUserIdFromAPIKey(UserAPIKey);

                    if (objEntity.ID > 0)
                    {
                        _userID = objEntity.ID;
                    }

                    if (objEntity.ID > 0)
                    {
                        string strErrorCode = "";
                        string strErrorMessage = "Invalid Key";
                        if (objEntity == null)
                        {
                            strErrorCode = "1";
                            strErrorMessage = "Invalid Key";
                        }
                        else if (objEntity.StatusId == 0)
                        {
                            strErrorCode = "1";
                            strErrorMessage = "User Disabled.";
                        }
                        else if (objEntity.StatusId == 2)
                        {
                            strErrorCode = "1";
                            strErrorMessage = "User Deleted.";
                        }
                        else
                        {
                            strUserID = Convert.ToString(objEntity.ID);
                        }
                        if (strErrorCode == "1")
                        {
                            // Create an instance of JsonResponseMessageDTO
                            jsonResponseMessageDTO = new JsonMessageDTO("", "", KeyEnums.StatusKeys.error.ToString(), strErrorMessage);
                            //return Json(jsonResponseMessageDTO);
                        }
                    }
                }
                try
                {
                    UsersController usersController = new UsersController();
                    usersController.InsertAccessMember(objEntity.ID, "SetPronunciation", usersController.getAccessMember());
                }
                catch (Exception ex)
                {
                }

                List<Pronunciation> lstPronunciation = new List<Pronunciation>();
                Pronunciation obj = new Pronunciation();
                obj.UUID = pronunciation.UUID;
                obj.UserID = objEntity.ID;
                obj.InitialText = pronunciation.original_word;
                obj.AlternateText = pronunciation.revised_word;
                obj.Locale = pronunciation.ampvoicelocale;
                obj.LocaleCode = pronunciation.ampvoicelocaleID;
                obj.Accent = pronunciation.ampvoiceaccent;
                obj.AccentCode = pronunciation.ampvoiceaccentID;
                obj.VMUUID = pronunciation.ampvoiceUUID;
                obj.StatusId = pronunciation.StatusID;

                PronunciationBusinessFacade pronunciationBusinessFacade = new PronunciationBusinessFacade();
                isSuccess = pronunciationBusinessFacade.Save(obj);
                try
                {
                    if (pronunciation.UUID == "")
                    {
                        List<Pronunciation> lstpronunciation = new List<Pronunciation>();
                        if (isSuccess > 0)
                        {
                            lstpronunciation = pronunciationBusinessFacade.GetPronunciationById(isSuccess);
                        }
                        if (lstpronunciation != null && lstpronunciation.Count > 0)
                        {
                            obj = lstpronunciation[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                if (isSuccess > 0)
                    _jsonMessageApi = new JsonMessageApi(true, obj);
                // _jsonMessage = new JsonMessage(true, Resource.lbl_Cap_success, Resource.lbl_DataFetched, KeyEnums.JsonMessageType.SUCCESS, URLDetails.GetSiteRootUrl().TrimEnd('/'), "", obj);
                else
                    _jsonMessageApi = new JsonMessageApi(false, null);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "SetPronunciation(userID = " + Json(revsponse) + ")", ex.Source, ex.Message);
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "", string.Format("Method : AddDomain(objAPI), Source : {0}, Message {1}", ex.Source, ex.Message));
            }
            return _jsonMessageApi;
        }

        [Authorize]
        [HttpPost]
        [Route("getpronunciation")]
        public dynamic getpronunciation()
        {
            string UserAPIKey = "";
            Int64 revsponse = 0;
            try
            {
                Log.WriteInfoLogWithoutMail(_module, "GetPronunciation()", "INFOLOG ", UserAPIKey.ToString());
            }
            catch (Exception ex)
            {
            }
            try
            {
                UserAPIKey = GetJWTUserAuthenticationHeaderValue();

                string strUserID = "";
                Users objEntity = new Users();
                if (UserAPIKey == "cms" || UserAPIKey == "cmsssml")
                {
                    //get the user details from the session
                    strUserID = _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) : "";

                    if (!string.IsNullOrEmpty(strUserID))
                    {
                        _userID = Convert.ToInt64(strUserID);
                    }

                    if (string.IsNullOrWhiteSpace(strUserID))
                    {
                        //jsonResponseMessageDTO = new JsonMessageDTO(false, Resource.lbl_error, "Invalid User", KeyEnums.JsonMessageType.ERROR);
                        jsonResponseMessageDTO = new JsonMessageDTO("", "", KeyEnums.StatusKeys.error.ToString(), "Invalid User.");
                        return Json(_jsonMessage);
                    }
                }
                else
                {
                    strUserID = "0";

                    UsersBusinessFacade objBF = new UsersBusinessFacade();

                    objEntity = objBF.GetUserIdFromAPIKey(UserAPIKey);

                    if (objEntity.ID > 0)
                    {
                        _userID = objEntity.ID;
                    }

                    if (objEntity.ID > 0)
                    {
                        string strErrorCode = "";
                        string strErrorMessage = "Invalid Key";
                        if (objEntity == null)
                        {
                            strErrorCode = "1";
                            strErrorMessage = "Invalid Key";
                        }
                        else if (objEntity.StatusId == 0)
                        {
                            strErrorCode = "1";
                            strErrorMessage = "User Disabled.";
                        }
                        else if (objEntity.StatusId == 2)
                        {
                            strErrorCode = "1";
                            strErrorMessage = "User Deleted.";
                        }
                        else
                        {
                            strUserID = Convert.ToString(objEntity.ID);
                        }
                        if (strErrorCode == "1")
                        {
                            jsonResponseMessageDTO = new JsonMessageDTO("", "", KeyEnums.StatusKeys.error.ToString(), "User Is Disabled.");
                            //return Json(jsonResponseMessageDTO);
                        }
                    }
                }
                try
                {
                    UsersController usersController = new UsersController();
                    usersController.InsertAccessMember(objEntity.ID, "GetPronunciation", usersController.getAccessMember());
                }
                catch (Exception ex)
                {
                }

                dynamic lstPronunciation = null;
                List<GetPronunciaions> lstGetPronunciation = new List<GetPronunciaions>();
                if (UserAPIKey != null)
                {
                    PronunciationBusinessFacade pronunciationBusinessFacade = new PronunciationBusinessFacade();
                    lstPronunciation = pronunciationBusinessFacade.GetPronunciation(UserAPIKey);

                    foreach (var VoiceMaster in lstPronunciation)
                    {
                        GetPronunciaions objpronun = new GetPronunciaions();

                        objpronun.voiceUUID = VoiceMaster.UUID;
                        objpronun.original_Word = VoiceMaster.InitialText;
                        objpronun.revised_word = VoiceMaster.AlternateText;
                        objpronun.ampvoicelocale = VoiceMaster.Locale;
                        objpronun.ampvoicelocaleID = VoiceMaster.LocaleCode;
                        objpronun.ampvoiceaccent = VoiceMaster.Accent;
                        objpronun.ampvoiceaccentID = VoiceMaster.AccentCode;
                        objpronun.ampDisplayName = VoiceMaster.DisplayName;
                        objpronun.ampvoiceUUID = VoiceMaster.VMUUID;
                        objpronun.ampvoiceGender = VoiceMaster.Gender;
                        objpronun.ampvoicesampleMp3 = VoiceMaster.SampleUrl;
                        objpronun.ampvoicePhoto = VoiceMaster.VoiceImage;
                        objpronun.ampvoiceagerange = VoiceMaster.AgeBracket;

                        lstGetPronunciation.Add(objpronun);
                    }
                    _jsonMessageApi = new JsonMessageApi(true, lstGetPronunciation);
                    // _jsonMessage = new JsonMessage(true, Resource.lbl_Cap_success, Resource.lbl_DataFetched, KeyEnums.JsonMessageType.SUCCESS, URLDetails.GetSiteRootUrl().TrimEnd('/'), "", lstGetPronunciation);
                }
                else
                    _jsonMessageApi = new JsonMessageApi(false, null);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetTTSStatus(userID = " + Json(revsponse) + ")", ex.Source, ex.Message);
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "", string.Format("Method : AddDomain(objAPI), Source : {0}, Message {1}", ex.Source, ex.Message));
            }
            return _jsonMessageApi;
        }

        private string GetJWTUserAuthenticationHeaderValue()
        {
            string nameValue = null;

            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
            {
                // Authorization header value is available

                var authorizationHeaderValue = authHeaderValue.ToString();
                if (authorizationHeaderValue.StartsWith("Bearer "))
                {
                    authorizationHeaderValue = authorizationHeaderValue.Substring("Bearer ".Length);
                }
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken parsedToken = tokenHandler.ReadJwtToken(authorizationHeaderValue);

                // Print out all claims
                Console.WriteLine("Claims in the JWT token:");
                foreach (var claim in parsedToken.Claims)
                {
                    Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                }

                // Example: Retrieving a specific claim by type
                Claim nameClaim = parsedToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");

                if (nameClaim != null)
                {
                    nameValue = nameClaim.Value;
                    Console.WriteLine($"Name: {nameValue}");
                }
            }

            return nameValue;
        }
    }
}