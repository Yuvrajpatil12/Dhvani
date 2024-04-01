using Core.Business.BusinessFacade;
using Core.Entity;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Models;
using Core.Resources;
using Core.Utility.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using X.PagedList;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;


namespace Dhvani.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class VoiceMapperController : Controller
    {
        private static string _module = "Dhvani.Controllers.VoiceController";
        private Int64 _userId = 0;
        private string _role = "";
        private Int64 _companyid = 0;
        private Int64 _locationid = 0;
        private string _cacheKey = "VoiceController_";
        private JsonMessage _jsonMessage = null;
        private int page = 1, size = 10;
        private bool isValidUser = false;
        private IMemoryCache _cache;
        private Helper _helper;

        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IHostingEnvironment _hostingEnvironment;
        private List<VoiceMasterSetting> _list = new List<VoiceMasterSetting>(); // common list of controller 


        public VoiceMapperController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor = null, 
            IMemoryCache cache = null, IWebHostEnvironment env = null)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _helper = new Helper(_httpContextAccessor);
            _userId = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? Convert.ToInt64(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString())) : 0;
            _role = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString()) != null ? Convert.ToString(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString())) : "";
            isValidUser = _helper.IsValidUser(_userId, _role, RoleEnums.SuperAdmin + "," + RoleEnums.Admin );
            _hostingEnvironment = environment;
            _env = env;
        }

        public IActionResult Index()
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liVoice.ToString();
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

        public List<VoiceMasterSetting> GetList(string query, string sortColumn, string sortOrder, ref int page, ref int size, string flag, string startDate = "", string toDate = "", Int64 _userId = 0, bool isLoad = true, string ListType = "", Int64 companyID = 0, Int64 locationId = 0)
        {
            List<VoiceMasterSetting> _list = new List<VoiceMasterSetting>();
            dynamic _Dlist = new List<dynamic>();
            try
            {
                _cacheKey = _cacheKey + ListType + _httpContextAccessor.HttpContext.Session.Id;

                VoiceMasterSettingBusinessFacade voiceMasterBusinessFacade = new VoiceMasterSettingBusinessFacade();
                Int64 userId = _userId;



                if (_role.ToString() == "1")
                {
                    _Dlist = voiceMasterBusinessFacade.GetVoiceMasterList();
                }
                else if (_role.ToString() == "2")
                {
                    _Dlist = voiceMasterBusinessFacade.GetVoiceMasterList();
                }


                if (_Dlist.Count > 0) { _list = _Dlist; }

                flag = !string.IsNullOrEmpty(flag) ? flag : "";

                if (_list != null)
                {
                    if (!string.IsNullOrEmpty(query))
                    {
                        query = query.Trim().ToLower();
                        _list = _list.Where(a => a.VMName.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMGender.ToLower().Trim().Contains(query.Trim())
                                                    || a.AgeBracket.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMDisplayName.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMLanguage.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMAccent.ToLower().Trim().Contains(query.Trim())
                                                    || a.Description.ToLower().Trim().Contains(query.Trim())
                                                    || a.VoiceProvider.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMVoiceType.ToLower().Trim().Contains(query.Trim())
                                                    || a.TagName.ToLower().Trim().Contains(query.Trim())
                                                    || a.BestForName.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMLocaleName.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMShortName.ToLower().Trim().Contains(query.Trim())
                                                    || a.SampleUrl.ToLower().Trim().Contains(query.Trim())
                                                    || a.CharLimit.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMSampleRateHertz.ToLower().Trim().Contains(query.Trim())
                                                    || a.VMWordsPerMinute.ToLower().Trim().Contains(query.Trim())
                                                    || a.StyleName.ToLower().Trim().Contains(query.Trim())
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

                            case "name":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMName).ToList();
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
                            case "discription":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.Description).ToList();
                                else
                                    _list = _list.OrderBy(p => p.Description).ToList();
                                break;
                            case "voiceprovider":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VoiceProvider).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VoiceProvider).ToList();
                                break;

                            case "voicetype":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMVoiceType).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMVoiceType).ToList();
                                break;

                            case "tag":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.TagName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.TagName).ToList();
                                break;

                            case "bestfor":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.BestForName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.BestForName).ToList();
                                break;

                            case "locale":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMLocale).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMLocale).ToList();
                                break;

                            case "localename":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMLocaleName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMLocaleName).ToList();
                                break;
                            case "shortname":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMShortName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMShortName).ToList();
                                break;
                            case "sampleurl":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.SampleUrl).ToList();
                                else
                                    _list = _list.OrderBy(p => p.SampleUrl).ToList();
                                break;
                            case "characterlimit":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.CharLimit).ToList();
                                else
                                    _list = _list.OrderBy(p => p.CharLimit).ToList();
                                break;
                            case "sampleratehertz":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMSampleRateHertz).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMSampleRateHertz).ToList();
                                break;
                            case "status":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMStatus).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMStatus).ToList();
                                break;

                            case "wordsperminute":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.VMWordsPerMinute).ToList();
                                else
                                    _list = _list.OrderBy(p => p.VMWordsPerMinute).ToList();

                                break;

                            case "speakstyle":
                                if (sortOrder.ToLower() == "desc")
                                    _list = _list.OrderByDescending(p => p.StyleName).ToList();
                                else
                                    _list = _list.OrderBy(p => p.StyleName).ToList();
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

        #endregion GetList

        #region Search

        public ActionResult Search(string query, string sortColumn, string sortOrder, int page, int size, string flag, bool ISLOAD = false, string ListType = "", Int64 CompanyID = 0, Int64 LocationID = 0, string startDate = "", string toDate = "")
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liVoice.ToString();
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

        #region Add and Edit Operation

        // add and edit operation for razor page cshtml 
        public ActionResult AddEdit(string id)
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liVoice.ToString();
            
            List<VoiceMasterSetting> _voiceMasterSetting = new List<VoiceMasterSetting>();
            VoiceMasterSetting objmodel = new VoiceMasterSetting();

            // Get the tag master all values from database 
            //TagMasterBusinessFacade tagMasterBusinessFacade = new TagMasterBusinessFacade();
            //objmodel._listTagMasters = tagMasterBusinessFacade.GetAllTagMasterValues();

            // Create the list for VMTags convert into Int64 
            List<VMTags> listVMTags = new List<VMTags>();

            try
            {
                if (isValidUser)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        VoiceMasterSettingBusinessFacade voiceMasterBusinessFacade = new VoiceMasterSettingBusinessFacade();
                        _voiceMasterSetting = voiceMasterBusinessFacade.GetVoiceMasterById(id);

                        if (_voiceMasterSetting.Count > 0)
                        {
                            objmodel = _voiceMasterSetting.FirstOrDefault();

                            // Set the path for the image
                            if (!string.IsNullOrEmpty(objmodel.VoiceImage))
                            {
                                objmodel.VoiceImage = ConstantsCommon.Domain + "/files/images/" + objmodel.VoiceImage;
                            }

                        }

                        // Get the data base on voice master table uuid 
                        VMTagsBusinessFacade vMTagsBusinessFacade = new VMTagsBusinessFacade();
                        listVMTags = vMTagsBusinessFacade.VmTagMasterGetRecordById(id);

                        // Assign the value to the TagMasterIDs for the view model
                        if(listVMTags.Count > 0)
                        {
                            objmodel.TagMasterIDs = listVMTags.Select(temp => temp.TagMasterID).ToList();
                        }

                        // Get the data base on VMBestFor table uuid
                        VMBestForBusinessFacade vMBestForBusinessFacade = new VMBestForBusinessFacade();
                        //objmodel._listBestForMaster = vMBestForBusinessFacade.VMBestForGetRecordById();

                        // Get the data base on VMStyleList table uuid
                        List<VMStyleList> _listVMStyleList = new List<VMStyleList>();
                     
                        VMStyleListBusinessFacade vMStyleListBusinessFacade = new VMStyleListBusinessFacade();
                        objmodel._StyleListValues = vMStyleListBusinessFacade.GetVMStyleListRecordById(id);                         
                        




                    }
                }
                else
                {
                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_error, KeyEnums.JsonMessageType.FAILURE);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "AddEdit(id=" + id + ")", ex.Source, ex.Message, ex);
            }
            return View(objmodel);
        }

        #endregion

        #region Save 

        public async Task<IActionResult> SaveVoice(IFormCollection objForm)
        {
            var revsponse = JsonConvert.SerializeObject(new { objForm });
            try
            {
                Log.WriteInfoLogWithoutMail(_module, "SaveUser()", "INFOLOG ", revsponse);
            }
            catch (Exception ex)
            { }

            try
            {
                VoiceMasterSetting objEntityVoiceMaster = new VoiceMasterSetting();

                // Check if files are uploaded
                if (objForm.Files.Count > 0)
                {
                    try
                    {
                        var imageFile = objForm.Files.FirstOrDefault();
                        // Check if the file is null or empty
                        if (imageFile == null || imageFile.Length == 0)
                            throw new ArgumentException("Image file is null or empty.");

                        // Generate a unique filename using timestamp
                        var fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}.webp";

                        // Construct the file path
                        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "files\\images", fileName);

                        // Create the directory if it doesn't exist
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

                        // Save the image to disk
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        // Return the relative path of the saved image
                        objEntityVoiceMaster.VoiceImage = fileName;
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception
                        throw new ApplicationException("Failed to save image.", ex);
                    }


                }





                objEntityVoiceMaster.ID = !string.IsNullOrWhiteSpace(objForm["ID"]) ? Convert.ToInt64(objForm["ID"].ToString().Trim()) : 0;
                objEntityVoiceMaster.UUID = !string.IsNullOrWhiteSpace(objForm["UUID"]) ? Convert.ToString(objForm["UUID"].ToString().Trim()) : "";
                objEntityVoiceMaster.Description = !string.IsNullOrWhiteSpace(objForm["Discription"]) ? Convert.ToString(objForm["Discription"].ToString().Trim()) : "";
               

                objEntityVoiceMaster.VoiceProvider = !string.IsNullOrWhiteSpace(objForm["VoiceProvider"]) ? Convert.ToString(objForm["VoiceProvider"].ToString().Trim()) : "";
                objEntityVoiceMaster.VMName = !string.IsNullOrWhiteSpace(objForm["VMName"]) ? Convert.ToString(objForm["VMName"].ToString().Trim()) : "";
                objEntityVoiceMaster.VMDisplayName = !string.IsNullOrWhiteSpace(objForm["DisplayName"]) ? Convert.ToString(objForm["DisplayName"].ToString().Trim()) : "";
                objEntityVoiceMaster.VMLocale = !string.IsNullOrWhiteSpace(objForm["Locale"]) ? Convert.ToString(objForm["Locale"].ToString().Trim()) : "";
                objEntityVoiceMaster.VMLocaleName = !string.IsNullOrWhiteSpace(objForm["LocalName"]) ? Convert.ToString(objForm["LocalName"].ToString().Trim()) : "";
                objEntityVoiceMaster.VMShortName = !string.IsNullOrWhiteSpace(objForm["ShortName"]) ? Convert.ToString(objForm["ShortName"].ToString().Trim()) : "";
                objEntityVoiceMaster.SampleUrl = !string.IsNullOrWhiteSpace(objForm["SampleURL"]) ? Convert.ToString(objForm["SampleURL"].ToString().Trim()) : "";
                objEntityVoiceMaster.VoiceProvider = !string.IsNullOrWhiteSpace(objForm["VoiceProvider"]) ? Convert.ToString(objForm["VoiceProvider"].ToString().Trim()) : "";
                objEntityVoiceMaster.CharLimit = !string.IsNullOrWhiteSpace(objForm["CharacterLimit"]) ? Convert.ToString(objForm["CharacterLimit"].ToString().Trim()) : "";
                objEntityVoiceMaster.VMSampleRateHertz = !string.IsNullOrWhiteSpace(objForm["SampleRateHertz"]) ? Convert.ToString(objForm["SampleRateHertz"].ToString().Trim()) : "";
                objEntityVoiceMaster.VMVoiceType = !string.IsNullOrWhiteSpace(objForm["VoiceType"]) ? Convert.ToString(objForm["VoiceType"].ToString().Trim()) : "";
                objEntityVoiceMaster.VMStatus = !string.IsNullOrWhiteSpace(objForm["Status"]) ? Convert.ToString(objForm["Status"].ToString().Trim()) : "";
                objEntityVoiceMaster.VMWordsPerMinute = !string.IsNullOrWhiteSpace(objForm["WordsPerMinute"]) ? Convert.ToString(objForm["WordsPerMinute"].ToString().Trim()) : "";
                objEntityVoiceMaster.VMStyleList = !string.IsNullOrWhiteSpace(objForm["SpeakStyle"]) ? Convert.ToString(objForm["SpeakStyle"].ToString().Trim()) : "";
                objEntityVoiceMaster.VoiceMasterID = !string.IsNullOrWhiteSpace(objForm["VoiceMasterID"]) ? Convert.ToInt64(objForm["VoiceMasterID"].ToString().Trim()) : 0;

                objEntityVoiceMaster.VMLanguage = !string.IsNullOrWhiteSpace(objForm["Language"]) ? Convert.ToString(objForm["Language"].ToString().Trim()) : "";

                //var gender = objForm["gender"];
                
                objEntityVoiceMaster.AgeBracket = !string.IsNullOrWhiteSpace(objForm["SelectedValueAge"]) ? Convert.ToString(objForm["SelectedValueAge"].ToString().Trim()) : "";

                objEntityVoiceMaster.VMGender = !string.IsNullOrWhiteSpace(objForm["SelectedGender"]) ? Convert.ToString(objForm["SelectedGender"].ToString().Trim()) : "";

                

                string selTags = objForm["SelectedTags"];            
                if (!string.IsNullOrEmpty(selTags))
                {

                    VMTagsBusinessFacade vMTagsUpdateRecordsBusinessFacade = new VMTagsBusinessFacade();
                    VMTags objVMTagsUpdateRecords = new VMTags();
                    objVMTagsUpdateRecords.VoiceMasterID = objEntityVoiceMaster.VoiceMasterID;
                    vMTagsUpdateRecordsBusinessFacade.UpdateRecords(objVMTagsUpdateRecords);

                    VMTagsBusinessFacade vMTagsBusinessFacade = new VMTagsBusinessFacade();
                    VMTags objVMTags = new VMTags();

                    string[] values = selTags.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        
                        objVMTags.TagMasterID = Convert.ToInt64(values[i]);
                        objVMTags.VoiceMasterID = objEntityVoiceMaster.VoiceMasterID;

                        vMTagsBusinessFacade.Save(objVMTags);

                    }

                }                

                // Best for value store in database 
                string selBestFor = objForm["SelectedBestFor"];                
                if (!string.IsNullOrEmpty(selBestFor))
                {
                    VMBestForBusinessFacade objVMBestForUpdateRecordsBusinessFacade = new VMBestForBusinessFacade();
                    VMBestFor objVMBestForUpdateRecords = new VMBestFor();

                    objVMBestForUpdateRecords.VoiceMasterID = objEntityVoiceMaster.VoiceMasterID;
                    objVMBestForUpdateRecordsBusinessFacade.Update(objVMBestForUpdateRecords);

                    string[] BestForvalues = selBestFor.Split(',');
                    for (int i = 0; i < BestForvalues.Length; i++)
                    {
                        VMBestForBusinessFacade objVMBestForBusinessFacade = new VMBestForBusinessFacade();
                        VMBestFor objVMBestFor = new VMBestFor();

                        objVMBestFor.BestForMasterID = Convert.ToInt64(BestForvalues[i]);
                        objVMBestFor.VoiceMasterID = objEntityVoiceMaster.VoiceMasterID;

                        objVMBestForBusinessFacade.Save(objVMBestFor);

                    }
                }
                
                

                string OperationMode = null;
                if (string.IsNullOrEmpty(objEntityVoiceMaster.UUID))
                {
                    OperationMode = "I";
                }
                else
                {
                    OperationMode = "U";
                }
                                

                if (OperationMode == "I")
                {
                    Int64 resultId = 0;

                    //write the line for convert guid id in UUID object 
                    objEntityVoiceMaster.UUID = Convert.ToString(Guid.NewGuid());
                    VoiceMasterSettingBusinessFacade voiceMasterSettingBusinessFacade = new VoiceMasterSettingBusinessFacade();
                    
                    //ImageConversionService imageConversionService = new ImageConversionService(_hostingEnvironment);
                    voiceMasterSettingBusinessFacade.Save(objEntityVoiceMaster);

                    //imageConversionService.ConvertAndSaveImageAsync(objEntityVoiceMaster.VoiceImage, objForm.Files[0].FileName);
                    //objEntityVoiceMaster.UUID = Convert.ToString(Guid.NewGuid());
                    _jsonMessage = new JsonMessage(true, Resource.lbl_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS);

                    //if (resultId > 0)
                    //{
                    //    _jsonMessage = new JsonMessage(true, Resource.lbl_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS);                      

                    //}
                    //else
                    //{
                    //    _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_error, KeyEnums.JsonMessageType.FAILURE);
                    //}
                }
                else
                {

                    VoiceMasterSettingBusinessFacade voiceMasterSettingBusinessFacade = new VoiceMasterSettingBusinessFacade();
                  

                    Int64 updateResultId = 0;
                    //updateResultId = voiceMasterSettingBusinessFacade.Save(objEntityVoiceMaster);
                    voiceMasterSettingBusinessFacade.Save(objEntityVoiceMaster);
                    _jsonMessage = new JsonMessage(true, Resource.lbl_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS);

                    //if (updateResultId > 0)
                    //{
                    //    _jsonMessage = new JsonMessage(true, Resource.lbl_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS);
                    //}
                    //else
                    //{
                    //    _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_error, KeyEnums.JsonMessageType.FAILURE);
                    //}
                }

                return Json(_jsonMessage);


            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        public IActionResult GetUserDetails([FromQuery] string voiceMasterID)
        {
            //if (string.IsNullOrEmpty(voiceMasterID))
            //{
            //    return BadRequest("Invalid voiceMasterID");
            //}


            return PartialView("_userDetailsPartial");
        }
        //public IActionResult GetUserDetailsByUserId([FromQuery] string voiceMasterID)
        //{
        //    if (string.IsNullOrEmpty(voiceMasterID))
        //    {
        //        return BadRequest("Invalid voiceMasterID");
        //    }

        //    BestForMasterBusinessFacade objBestForMasterBusinessFacade = new BestForMasterBusinessFacade();
        //    var result = objBestForMasterBusinessFacade.GetBestForMasterList();



        //    return Json(result);
        //}


        /// <summary>
        /// Get all master tag id from the database
        /// </summary>
        /// <param name="voiceMasterID">Get the data from VMTags table base on voiceMasterID parameter</param>
        /// <returns></returns>.

        public IActionResult GetTagMasterList([FromQuery] string voiceMasterID)
        {
            if (string.IsNullOrEmpty(voiceMasterID))
            {
                return BadRequest("Invalid voiceMasterID");
            }

            TagMasterBusinessFacade objTagMasterBusinessFacade = new TagMasterBusinessFacade();
            var result = objTagMasterBusinessFacade.GetAllTagMasterValues();

            VMTagsBusinessFacade objVMTagsBusinessFacade = new VMTagsBusinessFacade();
            var resultVMTags = objVMTagsBusinessFacade.VMTagsGetSelectedTagsVoiceMasterById(voiceMasterID);

            var responseData = new
            {
                TagMasterValues = result,
                VmTags = resultVMTags
            };

            return Json(responseData);
        }

        public IActionResult GetBestForMasterList([FromQuery] string voiceMasterID)
        {
            if (string.IsNullOrEmpty(voiceMasterID))
            {
                return BadRequest("Invalid voiceMasterID");
            }

            BestForMasterBusinessFacade objBestForMasterBusinessFacade = new BestForMasterBusinessFacade();
            var result = objBestForMasterBusinessFacade.GetBestForMasterList();

            VMBestForBusinessFacade objVMBestForBusinessFacade = new VMBestForBusinessFacade();
            var resultVMBestFor = objVMBestForBusinessFacade.VMBestForGetSelectedBestForVoiceMasterById(voiceMasterID);

            var responseData = new
            {
                BestForMaster = result,
                VMBestFor = resultVMBestFor
            };

            return Json(responseData);
        }

    




    }
}
