using Core.Business.BusinessFacade;
using Core.Entity;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Models;
using Core.Resources;
using Core.Utility.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech.Transcription;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using X.PagedList;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Dhvani.Controllers
{
    public class PronunciationController : Controller
    {
        private static string _module = "Dhvani.Controllers.PronunciationController";
        private Int64 _userId = 0;
        private JsonMessage _jsonMessage = null;
        private Int64 _schoolId = 0;
        private string _role = "";
        private Int64 _companyid = 0;
        private Int64 _locationid = 0;
        private string _cacheKey = "PronunciationController_";
        private int page = 1, size = 10;
        private bool isValidUser = false;
        private IMemoryCache _cache;
        private Helper _helper;
        private List<Pronunciation> _list = new List<Pronunciation>();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PronunciationController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor = null, IMemoryCache cache = null)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
            _userId = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? Convert.ToInt64(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString())) : 0;
            _role = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString()) != null ? Convert.ToString(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString())) : "";
            isValidUser = _helper.IsValidUser(_userId, _role, RoleEnums.User);
        }

        public IActionResult Index()
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liPronunciation.ToString();
            var startDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(-30), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("yyyy-MM-dd");
            var toDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("yyyy-MM-dd");

            try
            {
                _list = GetList("", "", "asc", ref page, ref size, "sort", startDate, toDate, _userId, true, ViewBag.RequestList);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Index", ex.Source, ex.Message, ex);
            }

            return View(_list.ToPagedList(1, 1000));
        }

        public JsonResult GetLanguage(string type)
        {
            List<VoiceMaster> voiceMaster = new List<VoiceMaster>();
            VoiceMasterBusinessFacade voiceMasterBusinessFacade = new VoiceMasterBusinessFacade();
            voiceMaster = voiceMasterBusinessFacade.VoiceMaster_GetLanguage(type, _userId);

            return Json(voiceMaster);
        }

        public JsonResult GetAccent(string language)
        {
            List<VoiceMaster> voiceMaster = new List<VoiceMaster>();
            VoiceMasterBusinessFacade voiceMasterBusinessFacade = new VoiceMasterBusinessFacade();
            voiceMaster = voiceMasterBusinessFacade.VoiceMaster_GetAccent(language, _userId);

            return Json(voiceMaster);
        }

        public JsonResult GetVoice(string language)
        {
            List<VoiceMaster> voiceMaster = new List<VoiceMaster>();
            VoiceMasterBusinessFacade voiceMasterBusinessFacade = new VoiceMasterBusinessFacade();
            voiceMaster = voiceMasterBusinessFacade.VoiceMaster_GetVoice(language, _userId);

            return Json(voiceMaster);
        }

        public JsonResult GetStyle(string language)
        {
            List<VoiceMaster> voiceMaster = new List<VoiceMaster>();
            VoiceMasterBusinessFacade voiceMasterBusinessFacade = new VoiceMasterBusinessFacade();
            voiceMaster = voiceMasterBusinessFacade.VoiceMaster_GetStyle(language);

            return Json(voiceMaster);
        }

        public IActionResult PronunciationSave(IFormCollection objForm)
        {
            var revsponse = JsonConvert.SerializeObject(new { objForm });
            try
            {
                Log.WriteInfoLogWithoutMail(_module, "Pronunciation()", "INFOLOG ", revsponse);
            }
            catch (Exception ex)
            { }

            try
            {
                bool isSuccess = false;
                int saveEdit = !string.IsNullOrWhiteSpace(objForm["SaveEdit"]) ? Convert.ToInt32(objForm["SaveEdit"].ToString().Trim()) : 0; ;
                Pronunciation pronunciation = new Pronunciation();
                if (saveEdit == 1)
                {
                    pronunciation.UserID = _userId;
                    pronunciation.InitialText = !string.IsNullOrWhiteSpace(objForm["yourWord"]) ? (objForm["yourWord"].ToString().Trim()) : "";
                    pronunciation.AlternateText = !string.IsNullOrWhiteSpace(objForm["alternateSpelling"]) ? (objForm["alternateSpelling"].ToString().Trim()) : "";
                    pronunciation.Locale = !string.IsNullOrWhiteSpace(objForm["languageselect_text"]) ? (objForm["languageselect_text"].ToString().Trim()) : "";
                    pronunciation.LocaleCode = !string.IsNullOrWhiteSpace(objForm["languageselect_value"]) ? (objForm["languageselect_value"].ToString().Trim()) : "";
                    pronunciation.Accent = !string.IsNullOrWhiteSpace(objForm["accent_text"]) ? (objForm["accent_text"].ToString().Trim()) : "";
                    pronunciation.AccentCode = !string.IsNullOrWhiteSpace(objForm["accent_value"]) ? (objForm["accent_value"].ToString().Trim()) : "";
                    pronunciation.ShortName = !string.IsNullOrWhiteSpace(objForm["voiceselect_value"]) ? (objForm["voiceselect_value"].ToString().Trim()) : "";
                }
                Pronunciation objEntity = new Pronunciation();
                if (saveEdit == 2)
                {
                    ICollection<string> iDKeys = objForm.Keys;
                    Int64 UserID = !string.IsNullOrWhiteSpace(objForm["UserID"]) ? Convert.ToInt64(objForm["UserID"].ToString().Trim()) : 0;

                    string[] arrIDKey = (string[])iDKeys.Where(x => x.IndexOf("hdnid_") > -1 && x.Split('_')[0] == "hdnid").ToArray();
                    string[] arrCheckIDKey = (string[])iDKeys.Where(x => x.IndexOf("type_") > -1 && x.Split('_')[0] == "type").ToArray();
                    string[] arrInitialWord = (string[])iDKeys.Where(x => x.IndexOf("yourWord_") > -1 && x.Split('_')[0] == "yourWord").ToArray();
                    string[] arrAlternateWord = (string[])iDKeys.Where(x => x.IndexOf("alternateSpelling_") > -1 && x.Split('_')[0] == "alternateSpelling").ToArray();
                    string[] arrLanguage = (string[])iDKeys.Where(x => x.IndexOf("localetext_") > -1 && x.Split('_')[0] == "localetext").ToArray();
                    string[] arrLanguageCode = (string[])iDKeys.Where(x => x.IndexOf("dictionaryLanguage_") > -1 && x.Split('_')[0] == "dictionaryLanguage").ToArray();
                    string[] arrAccent = (string[])iDKeys.Where(x => x.IndexOf("accenttext") > -1 && x.Split('_')[0] == "accenttext").ToArray();
                    string[] arrAccentCode = (string[])iDKeys.Where(x => x.IndexOf("accent_") > -1 && x.Split('_')[0] == "accent").ToArray();
                    //  string[] arrVoice = (string[])iDKeys.Where(x => x.IndexOf("textaccent_") > -1 && x.Split('_')[0] == "textaccent").ToArray();
                    string[] arrVoiceCode = (string[])iDKeys.Where(x => x.IndexOf("voice_") > -1 && x.Split('_')[0] == "voice").ToArray();

                    for (int i = 0; i < arrIDKey.Length; i++)
                    {
                        int Check = !string.IsNullOrWhiteSpace(objForm[arrCheckIDKey[i]]) ? Convert.ToInt32(objForm[arrCheckIDKey[i]].ToString().Trim()) : 0;
                        if (Check == 1)
                        {
                            pronunciation.UserID = _userId;
                            pronunciation.ID = !string.IsNullOrWhiteSpace(objForm[arrIDKey[i]]) ? Convert.ToInt64(objForm[arrIDKey[i]].ToString().Trim()) : 0;
                            pronunciation.InitialText = !string.IsNullOrWhiteSpace(objForm[arrInitialWord[i]]) ? (objForm[arrInitialWord[i]].ToString().Trim()) : "";
                            pronunciation.AlternateText = !string.IsNullOrWhiteSpace(objForm[arrAlternateWord[i]]) ? (objForm[arrAlternateWord[i]].ToString().Trim()) : "";
                            pronunciation.Locale = !string.IsNullOrWhiteSpace(objForm[arrLanguage[i]]) ? (objForm[arrLanguage[i]].ToString().Trim()) : "";
                            pronunciation.LocaleCode = !string.IsNullOrWhiteSpace(objForm[arrLanguageCode[i]]) ? (objForm[arrLanguageCode[i]].ToString().Trim()) : "";
                            pronunciation.Accent = !string.IsNullOrWhiteSpace(objForm[arrAccent[i]]) ? (objForm[arrAccent[i]].ToString().Trim()) : "";
                            pronunciation.AccentCode = !string.IsNullOrWhiteSpace(objForm[arrAccentCode[i]]) ? (objForm[arrAccentCode[i]].ToString().Trim()) : "";
                            pronunciation.ShortName = !string.IsNullOrWhiteSpace(objForm[arrVoiceCode[i]]) ? (objForm[arrVoiceCode[i]].ToString().Trim()) : "";
                            pronunciation.VMUUID = !string.IsNullOrWhiteSpace(objForm[arrVoiceCode[i]]) ? (objForm[arrVoiceCode[i]].ToString().Trim()) : "";
                        }
                    }
                }
                PronunciationBusinessFacade pronunciationBusinessFacade = new PronunciationBusinessFacade();
                isSuccess = pronunciationBusinessFacade.SaveAdmin(pronunciation);
                if (isSuccess)
                    _jsonMessage = new JsonMessage(true, Resource.lbl_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, objEntity);
                else
                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_dataNotSavedSuccessfully, KeyEnums.JsonMessageType.FAILURE);
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_dataNotSavedSuccessfully, KeyEnums.JsonMessageType.FAILURE);
            }
            return Json(_jsonMessage);
        }

        public List<Pronunciation> GetList(string query, string sortColumn, string sortOrder, ref int page, ref int size, string flag, string startDate = "", string toDate = "", Int64 _userId = 0, bool isLoad = true, string ListType = "", Int64 companyID = 0, Int64 locationId = 0)
        {
            List<Pronunciation> _list = new List<Pronunciation>();
            dynamic _Dlist = new List<dynamic>();
            try
            {
                PronunciationBusinessFacade pronunciationBusinessFacade = new PronunciationBusinessFacade();
                Int64 userId = _userId;
                _Dlist = pronunciationBusinessFacade.GetPronunciationList(userId);

                if (_Dlist.Count > 0) { _list = _Dlist; }

                flag = !string.IsNullOrEmpty(flag) ? flag : "";

                if (_list != null)
                {
                    if (flag.ToLower() == "size")
                        page = 1;

                    ViewBag.SortColumn = sortColumn == null ? "" : sortColumn.ToLower();
                    ViewBag.SortOrder = sortOrder == null ? "" : sortOrder.ToLower();
                    ViewBag.Page = page;
                    ViewBag.Size = size;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetList(query=" + query + "sortColumn=" + sortColumn + "," + "sortOrder=" + sortOrder + "," + "page=" + page + "," + "size=" + size + ", flag=" + flag + ")", ex.Source, ex.Message, ex);
            }

            if (_list == null)
                return _list;
            else
                return _list;
        }

        public JsonResult ChangeStatus(Int64 ID, string StatusID)
        {
            try
            {
                if (ID > 0)
                {
                    PronunciationBusinessFacade objBF = new PronunciationBusinessFacade();
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
    }
}