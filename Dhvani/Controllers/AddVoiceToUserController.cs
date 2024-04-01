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
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class AddVoiceToUserController : Controller
    {
        private static string _module = "Dhvani.Controllers.AddVoiceToUserController";
        private Int64 _userId = 0;
        private Int64 _schoolId = 0;
        private string _role = "";
        private Int64 _companyid = 0;
        private Int64 _locationid = 0;
        private string _cacheKey = "AddVoiceToUserController_";
        private JsonMessage _jsonMessage = null;
        private int page = 1, size = 10;
        private bool isValidUser = false;
        private IMemoryCache _cache;
        private Helper _helper;

        private List<Users> _list = new List<Users>();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IHostingEnvironment _hostingEnvironment;

        public AddVoiceToUserController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor = null, IMemoryCache cache = null)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _helper = new Helper(_httpContextAccessor);
            _userId = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? Convert.ToInt64(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString())) : 0;
            _role = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString()) != null ? Convert.ToString(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString())) : "";
            isValidUser = _helper.IsValidUser(_userId, _role, RoleEnums.SuperAdmin);
            _hostingEnvironment = environment;
        }

        #region Save

        //public dynamic AddVoiceToUsers(IFormCollection objForm)
        //{
        //    var revsponse = JsonConvert.SerializeObject(new { objForm });
        //    try
        //    {
        //        Log.WriteInfoLogWithoutMail(_module, "AddVoiceToUsers()", "INFOLOG ", revsponse);
        //    }
        //    catch (Exception ex)
        //    { }

        //    try
        //    {
        //        ListVoiceMaster objEntity = new ListVoiceMaster();
        //        ICollection<string> iDKeys = objForm.Keys;

        //        Int64 UserID = !string.IsNullOrWhiteSpace(objForm["UserID"]) ? Convert.ToInt64(objForm["UserID"].ToString().Trim()) : 0;

        //        string[] arrIDKey = (string[])iDKeys.Where(x => x.IndexOf("hdnid_") > -1 && x.Split('_')[0] == "hdnid").ToArray();
        //        string[] arrCheckKey = (string[])iDKeys.Where(x => x.IndexOf("CheckedTopics_") > -1 && x.Split('_')[0] == "CheckedTopics").ToArray();
        //        string[] arrVoiceMasterUserMapID = (string[])iDKeys.Where(x => x.IndexOf("VoiceMasterUserMapID_") > -1 && x.Split('_')[0] == "VoiceMasterUserMapID").ToArray();
        //        string[] arrVoiceMasterID = (string[])iDKeys.Where(x => x.IndexOf("VoiceMasterID_") > -1 && x.Split('_')[0] == "VoiceMasterID").ToArray();

        //        bool isSuccess = false;

        //        #region Update Voice

        //        VoiceMasterBusinessFacade voiceMasterBusinessFacade = new VoiceMasterBusinessFacade();
        //        voiceMasterBusinessFacade.UpdateVoice(UserID);

        //        #endregion Update Voice

        //        VoiceMaster lsttVoiceMaster = new VoiceMaster();
        //        for (int i = 0; i < arrCheckKey.Length; i++)
        //        {
        //            int Check = !string.IsNullOrWhiteSpace(objForm[arrCheckKey[i]]) ? Convert.ToInt32(objForm[arrCheckKey[i]].ToString().Trim()) : 0; ;

        //            if (Check == 1)
        //            {
        //                lsttVoiceMaster.UserID = UserID;
        //                lsttVoiceMaster.VoiceMasterUserMapID = !string.IsNullOrWhiteSpace(objForm[arrVoiceMasterUserMapID[i]]) ? Convert.ToInt64(objForm[arrVoiceMasterUserMapID[i]].ToString().Trim()) : 0;
        //                lsttVoiceMaster.VoiceID = !string.IsNullOrWhiteSpace(objForm[arrVoiceMasterID[i]]) ? Convert.ToInt64(objForm[arrVoiceMasterID[i]].ToString().Trim()) : 0;
        //                lsttVoiceMaster.Type = !string.IsNullOrWhiteSpace(objForm[arrCheckKey[i]]) ? Convert.ToInt32(objForm[arrCheckKey[i]].ToString().Trim()) : 0; ;
        //                //VoiceMasterBusinessFacade voiceMasterBusinessFacade = new VoiceMasterBusinessFacade();
        //                isSuccess = voiceMasterBusinessFacade.SaveVoiceToUser(lsttVoiceMaster);
        //            }
        //        }
        //        if (isSuccess)
        //            _jsonMessage = new JsonMessage(true, Resource.lbl_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, lsttVoiceMaster);
        //        else
        //            _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_dataNotSavedSuccessfully, KeyEnums.JsonMessageType.FAILURE);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog(_module, "Index", ex.Source, ex.Message, ex);
        //    }
        //    return 0;
        //}

        public dynamic AddVoiceToUsers(IFormCollection objForm)
        {
            var revsponse = JsonConvert.SerializeObject(new { objForm });
            try
            {
                Log.WriteInfoLogWithoutMail(_module, "AddVoiceToUsers()", "INFOLOG ", revsponse);
            }
            catch (Exception ex)
            { }

            try
            {
                ListVoiceMaster objEntity = new ListVoiceMaster();
                ICollection<string> iDKeys = objForm.Keys;

                VoiceMaster voiceMaster = new VoiceMaster();

                voiceMaster.UserID = !string.IsNullOrWhiteSpace(objForm["UserID"]) ? Convert.ToInt64(objForm["UserID"].ToString().Trim()) : 0;
                voiceMaster.Type = !string.IsNullOrWhiteSpace(objForm["Check"]) ? Convert.ToInt32(objForm["Check"].ToString().Trim()) : 0;
                voiceMaster.VoiceID = !string.IsNullOrWhiteSpace(objForm["VoiceMasterID"]) ? Convert.ToInt64(objForm["VoiceMasterID"].ToString().Trim()) : 0;
                voiceMaster.VoiceMasterUserMapID = !string.IsNullOrWhiteSpace(objForm["VoiceMasterUserMapID"]) ? Convert.ToInt64(objForm["VoiceMasterUserMapID"].ToString().Trim()) : 0;

                bool isSuccess = false;
                //voiceMasterBusinessFacade.UpdateVoice(UserID);

                VoiceMasterBusinessFacade voiceMasterBusinessFacade = new VoiceMasterBusinessFacade();
                isSuccess = voiceMasterBusinessFacade.SaveVoiceToUser(voiceMaster);

                if (isSuccess)
                    _jsonMessage = new JsonMessage(true, Resource.lbl_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, "");
                else
                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_dataNotSavedSuccessfully, KeyEnums.JsonMessageType.FAILURE);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Index", ex.Source, ex.Message, ex);
            }
            return 0;
        }

        #endregion Save

        public IActionResult GetVoice(Int64 id)
        {
            List<VoiceMaster> voiceMaster = new List<VoiceMaster>();
            VoiceMasterBusinessFacade voiceMasterBusinessFacade = new VoiceMasterBusinessFacade();
            voiceMaster = voiceMasterBusinessFacade.VoiceMasterGetList(id);

            return PartialView("_VoiceListPartial", voiceMaster);
        }

        #region Voice

        public IActionResult VoiceIndex(Int64 userID)
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liAddVoiceToUser.ToString();
            var startDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(-30), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("yyyy-MM-dd");
            var toDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("yyyy-MM-dd");
            List<VoiceMasterSetting> _lstVoiceMasterSetting = new List<VoiceMasterSetting>();
            if (isValidUser)
            {
                try
                {
                    _lstVoiceMasterSetting = GetListVoice("", "", "asc", ref page, ref size, "sort", startDate, toDate, _userId, true, ViewBag.RequestList, userID);
                }
                catch (Exception ex)
                {
                    Log.WriteLog(_module, "Index", ex.Source, ex.Message, ex);
                }
            }
            return View(_lstVoiceMasterSetting.ToPagedList(1, 500));
        }

        public List<VoiceMasterSetting> GetListVoice(string query, string sortColumn, string sortOrder, ref int page, ref int size, string flag, string startDate = "", string toDate = "", Int64 _userId = 0, bool isLoad = true, string ListType = "", Int64 userID = 0, Int64 companyID = 0, Int64 locationId = 0)
        {
            List<VoiceMasterSetting> _list = new List<VoiceMasterSetting>();
            dynamic _Dlist = new List<dynamic>();
            try
            {
                _cacheKey = _cacheKey + ListType + _httpContextAccessor.HttpContext.Session.Id;

                VoiceMasterSettingBusinessFacade voiceMasterSettingBusinessFacade = new VoiceMasterSettingBusinessFacade();
                Int64 userId = _userId;

                if (isLoad || _cache.Get(_cacheKey) == null)
                {
                    Int64 UserID = _userId;
                    if (_role.ToLower() == RoleEnums.Admin.ToString().ToLower() || _role.ToLower() == RoleEnums.SuperAdmin.ToString().ToLower())
                        UserID = 0;
                    _list = voiceMasterSettingBusinessFacade.GetVoiceMasterListByUserID(userID);
                    // _list = new SchoolMasterBusinessFacade().GetList();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10));
                    // _cache.Set(_cacheKey, _Dlist, cacheEntryOptions);
                    _cache.Set(_cacheKey, _list, cacheEntryOptions);
                    ViewBag.UserID = userID;
                }
                else
                {
                    _list = (List<VoiceMasterSetting>)_cache.Get(_cacheKey);
                    flag = !string.IsNullOrEmpty(flag) ? flag : "";
                }

                if (_list.Count > 0) { _list = _list; }

                flag = !string.IsNullOrEmpty(flag) ? flag : "";

                if (_list != null)
                {
                    if (!string.IsNullOrEmpty(query))
                    {
                        query = query.Trim().ToLower();
                        _list = _list.Where(a => a.VMDisplayName.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMGender.ToLower().Trim().Contains(query.Trim())
                                                    || a.AgeBracket.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMLanguage.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMAccent.ToLower().Trim().Contains(query.Trim())
                                                    || a.VoiceProvider.ToLower().Trim().Contains(query.Trim())
                                                    ).ToList();

                        if (flag.ToLower() == "search")
                        {
                            page = 1;
                        }
                    }

                    if (flag.ToLower() == "size")
                        page = 1;

                    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortOrder))
                    {
                        if (flag.ToLower() == "sort")
                            sortOrder = sortOrder.ToLower().Trim() == "asc" ? "desc" : "asc";

                        switch (sortColumn.ToLower().Trim())
                        {
                            case "srno":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.RowNumber).ToList();
                                else
                                    _list = _list.OrderBy(p => p.RowNumber).ToList();
                                break;

                            case "gender":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMGender).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMGender).ToList();
                                break;

                            case "age":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.AgeBracket).ToList();
                                else
                                    _list = _list.OrderBy(p => p.AgeBracket).ToList();
                                break;

                            case "displayname":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMDisplayName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMDisplayName).ToList();
                                break;

                            case "language":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMLanguage).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMLanguage).ToList();
                                break;

                            case "accent":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMAccent).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMAccent).ToList();
                                break;

                            case "voiceprovider":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VoiceProvider).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VoiceProvider).ToList();
                                break;

                            default:
                                break;
                        }
                    }

                    ViewBag.SortColumn = sortColumn == null ? "" : sortColumn.ToLower();
                    ViewBag.SortOrder = sortOrder == null ? "" : sortOrder.ToLower();
                    ViewBag.Page = page;
                    ViewBag.Size = 460;
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

        public ActionResult SearchVoice(string query, string sortColumn, string sortOrder, int page, int size, string flag, bool ISLOAD = false, string ListType = "", Int64 userID = 0, Int64 CompanyID = 0, Int64 LocationID = 0, string startDate = "", string toDate = "")
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liAddVoiceToUser.ToString();
            List<VoiceMasterSetting> _lstVoiceMasterSetting = new List<VoiceMasterSetting>();
            if (isValidUser)
            {
                try
                {
                    ViewBag.RequestList = ListType;
                    if (ListType == KeyEnums.ListType.AllViews.ToString())
                    {
                        if (query != "")
                            _lstVoiceMasterSetting = GetListVoice(query, sortColumn, sortOrder, ref page, ref size, flag, startDate, toDate, _userId, false, ListType, userID, CompanyID, LocationID);
                        else
                        {
                            ViewBag.SortColumn = "";
                            ViewBag.SortOrder = "desc";
                            ViewBag.Page = 1;
                            ViewBag.Size = 500;
                        }
                    }
                    else
                        _lstVoiceMasterSetting = GetListVoice(query, sortColumn, sortOrder, ref page, ref size, flag, startDate, toDate, _userId, false, ListType, userID, CompanyID, LocationID);
                }
                catch (Exception ex)
                {
                    Log.WriteLog(_module, "Search(query=" + query + ", sortColumn=" + sortColumn + ", sortOrder=" + sortOrder + ", page=" + page + ", size=" + size + ", flag=" + flag + ", ISLOAD=" + ISLOAD + ",ListType:" + ListType + ")", ex.Source, ex.Message, ex);
                }
            }
            return PartialView("_VListPartial", _lstVoiceMasterSetting.ToPagedList(page, size));
        }

        #endregion Voice

        #region User

        public IActionResult Index()
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liAddVoiceToUser.ToString();
            var startDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(-30), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("yyyy-MM-dd");
            var toDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("yyyy-MM-dd");

            if (isValidUser)
            {
                try
                {
                    _list = GetList("", "", "asc", ref page, ref size, "sort", startDate, toDate, _userId, true, ViewBag.RequestList);
                }
                catch (Exception ex)
                {
                    Log.WriteLog(_module, "Index", ex.Source, ex.Message, ex);
                }
            }
            return View(_list.ToPagedList(1, 10));
        }

        public List<Users> GetList(string query, string sortColumn, string sortOrder, ref int page, ref int size, string flag, string startDate = "", string toDate = "", Int64 _userId = 0, bool isLoad = true, string ListType = "", Int64 companyID = 0, Int64 locationId = 0)
        {
            List<Users> _list = new List<Users>();
            dynamic _Dlist = new List<dynamic>();
            try
            {
                _cacheKey = _cacheKey + ListType + _httpContextAccessor.HttpContext.Session.Id;

                UsersBusinessFacade _userBusinessFacade = new UsersBusinessFacade();
                Int64 userId = _userId;

                if (_role.ToString() == "1")
                {
                    _Dlist = _userBusinessFacade.getUsersList(userId);
                }
                else if (_role.ToString() == "2")
                {
                    _Dlist = _userBusinessFacade.getUsersList(userId);
                }
                else if (_role.ToString() == "3")
                {
                    _Dlist = _userBusinessFacade.getUsersList(userId);
                }

                if (_Dlist.Count > 0) { _list = _Dlist; }

                flag = !string.IsNullOrEmpty(flag) ? flag : "";

                if (_list != null)
                {
                    if (!string.IsNullOrEmpty(query))
                    {
                        query = query.Trim().ToLower();
                        _list = _list.Where(a => a.UserName.ToLower().Trim().Contains(query.Trim())
                                                    || a.FirstName.ToLower().Trim().Contains(query.Trim())
                                                    || a.LastName.ToLower().Trim().Contains(query.Trim())
                                                    || a.CreatedDate.ToString().ToLower().Trim().Contains(query.Trim())
                                                    ).ToList();

                        if (flag.ToLower() == "search")
                        {
                            page = 1;
                        }
                    }

                    if (flag.ToLower() == "size")
                        page = 1;

                    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortOrder))
                    {
                        if (flag.ToLower() == "sort")
                            sortOrder = sortOrder.ToLower().Trim() == "asc" ? "desc" : "asc";

                        switch (sortColumn.ToLower().Trim())
                        {
                            case "srno":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.RowNumber).ToList();
                                else
                                    _list = _list.OrderBy(p => p.RowNumber).ToList();
                                break;

                            case "username":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.UserName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.UserName).ToList();
                                break;

                            case "firstname":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.FirstName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.FirstName).ToList();
                                break;

                            case "lastname":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.LastName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.LastName).ToList();
                                break;

                            case "createddate":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.CreatedDate).ToList();
                                else
                                    _list = _list.OrderBy(p => p.CreatedDate).ToList();
                                break;

                            default:
                                break;
                        }
                    }

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

        public ActionResult Search(string query, string sortColumn, string sortOrder, int page, int size, string flag, bool ISLOAD = false, string ListType = "", Int64 CompanyID = 0, Int64 LocationID = 0, string startDate = "", string toDate = "")
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liAddVoiceToUser.ToString();
            if (isValidUser)
            {
                try
                {
                    ViewBag.RequestList = ListType;
                    if (ListType == KeyEnums.ListType.AllViews.ToString())
                    {
                        if (query != "")
                            _list = GetList(query, sortColumn, sortOrder, ref page, ref size, flag, startDate, toDate, _userId, ISLOAD, ListType, CompanyID, LocationID);
                        else
                        {
                            ViewBag.SortColumn = "";
                            ViewBag.SortOrder = "desc";
                            ViewBag.Page = 1;
                            ViewBag.Size = 10;
                        }
                    }
                    else
                        _list = GetList(query, sortColumn, sortOrder, ref page, ref size, flag, startDate, toDate, _userId, ISLOAD, ListType, CompanyID, LocationID);
                }
                catch (Exception ex)
                {
                    Log.WriteLog(_module, "Search(query=" + query + ", sortColumn=" + sortColumn + ", sortOrder=" + sortOrder + ", page=" + page + ", size=" + size + ", flag=" + flag + ", ISLOAD=" + ISLOAD + ",ListType:" + ListType + ")", ex.Source, ex.Message, ex);
                }
            }
            return PartialView("_ListPartial", _list.ToPagedList(page, size));
        }

        #endregion User

        // return the view base on id and if id is not present then only return the view
    }
}