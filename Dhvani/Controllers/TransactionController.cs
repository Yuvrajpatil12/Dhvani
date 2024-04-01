using Core.Business.BusinessFacade;
using Core.Entity;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Models;
using Core.Resources;
using Core.Utility.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Transactions;
using X.PagedList;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Dhvani.Controllers
{



    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class TransactionController : Controller
    {
        private static string _module = "Dhvani.Controllers.TransactionController";
        private Int64 _userId = 0;
        private string _role = "";
        private Int64 _companyid = 0;
        private Int64 _locationid = 0;
        private string _cacheKey = "TransactionController_";
        private JsonMessage _jsonMessage = null;
        private int page = 1, size = 10;
        private bool isValidUser = false;
        private IMemoryCache _cache;
        private Helper _helper;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private IHostingEnvironment _hostingEnvironment;
        private List<tblTransaction> _list = new List<tblTransaction>(); // common list of controller 

        public TransactionController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor = null, IMemoryCache cache = null)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _helper = new Helper(_httpContextAccessor);
            _userId = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? Convert.ToInt64(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString())) : 0;
            _role = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString()) != null ? Convert.ToString(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString())) : "";
            isValidUser = _helper.IsValidUser(_userId, _role, RoleEnums.SuperAdmin + "," + RoleEnums.Admin + "," + RoleEnums.User);
            _hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liTransaction.ToString();
            var dateRange = DateTimeHelper.GetDateRangeInIndianStandardTime();
            var startDate = dateRange.startDate;
            var toDate = dateRange.toDate;

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
            else
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_error, KeyEnums.JsonMessageType.FAILURE);
            }

            return View(_list.ToPagedList(1, 10));
        }

        #region GetList

        public List<tblTransaction> GetList(string query, string sortColumn, string sortOrder, ref int page, ref int size, string flag, string startDate = "", string toDate = "", Int64 _userId = 0, bool isLoad = true, string ListType = "", Int64 companyID = 0, Int64 locationId = 0)
        {
            List<tblTransaction> _list = new List<tblTransaction>();
            dynamic _Dlist = new List<dynamic>();
            try
            {
                _cacheKey = _cacheKey + ListType + _httpContextAccessor.HttpContext.Session.Id;

                tblTransactionBusinessFacade _userBusinessFacade = new tblTransactionBusinessFacade();
                Int64 userId = _userId;



                if (_role.ToString() == "1")
                {
                    _Dlist = _userBusinessFacade.getTransactionList(0);
                }
                else if (_role.ToString() == "2")
                {
                    _Dlist = _userBusinessFacade.getTransactionList(0);
                }
                else if (_role.ToString() == "3")
                {
                    _Dlist = _userBusinessFacade.getTransactionList(userId);
                }
           

                if (_Dlist.Count > 0) { _list = _Dlist; }

                flag = !string.IsNullOrEmpty(flag) ? flag : "";

                if (_list != null)
                {
                    if (!string.IsNullOrEmpty(query))
                    {
                        query = query.Trim().ToLower();
                        _list = _list.Where(a => a.VoiceText.ToLower().Trim().Contains(query.Trim())
                                                    || a.LoginName.ToLower().Trim().Contains(query.Trim())
                                                    || a.DisplayName.ToLower().Trim().Contains(query.Trim())
                                                    || a.LocaleName.ToLower().Trim().Contains(query.Trim())
                                                    || a.Accent.ToLower().Trim().Contains(query.Trim())
                                                    || a.ProviderName.ToLower().Trim().Contains(query.Trim())
                                                    || a.Language.ToLower().Trim().Contains(query.Trim())
                                                    || a.VoiceText.ToLower().Trim().Contains(query.Trim())
                                                    || a.TransactionStatus.ToLower().Trim().Contains(query.Trim())     
                                                    || a.CreatedDate.ToString().ToLower().Trim().Contains(query.Trim())
                                                    || a.UpdatedDate.ToString().ToLower().Trim().Contains(query.Trim())
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

                            case "loginname":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.LoginName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.LoginName).ToList();
                                break;

                            case "voicename":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.DisplayName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.DisplayName).ToList();
                                break;
                            case "language":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.LocaleName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.LocaleName).ToList();
                                break;
                            case "accent":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.Accent).ToList();
                                else
                                    _list = _list.OrderBy(p => p.Accent).ToList();
                                break;
                            case "provider":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.ProviderName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.ProviderName).ToList();
                                break;

                            case "text":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VoiceText).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VoiceText).ToList();
                                break;

                            //TransactionStatus is filed 
                            case "states":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.TransactionStatus).ToList();
                                else
                                    _list = _list.OrderBy(p => p.TransactionStatus).ToList();
                                break;

                            case "mp3":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.MP3URL).ToList();
                                else
                                    _list = _list.OrderBy(p => p.MP3URL).ToList();
                                break;                          

                            case "createddate":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.CreatedDate).ToList();
                                else
                                    _list = _list.OrderBy(p => p.CreatedDate).ToList();
                                break;

                            case "updateddate":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.UpdatedDate).ToList();
                                else
                                    _list = _list.OrderBy(p => p.UpdatedDate).ToList();
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

        #endregion GetList

        #region Search

        public ActionResult Search(string query, string sortColumn, string sortOrder, int page, int size, string flag, bool ISLOAD = false, string ListType = "", Int64 CompanyID = 0, Int64 LocationID = 0, string startDate = "", string toDate = "")
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liTransaction.ToString();
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

        #endregion Search
    }
}
