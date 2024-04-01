using Core.Business.BusinessFacade;
using Core.Entity;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Models;
using Core.Resources;
using Core.Utility;
using Core.Utility.Common;
using Dhvani.GoogleClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NAudio.Wave;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using VoiceViewModel.VoiceConversion;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Dhvani.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoiceController : Controller
    {
        private readonly string _module = "Dhvani.Controllers.VoiceController";
        private JsonMessage _jsonMessage = null;
        private JsonMessageDTO jsonResponseMessageDTO = null;
        private JsonMessageApi _jsonMessageApi = null;
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
        private string Mp3AzureFilePath = null;

        //private string _userTransactionUUID = null;
        private Int64 _userID = 0;

        private Int64 userTransactionID = 0;

        public VoiceController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
        }

        [Authorize]
        [HttpPost]
        [Route("generatespeechAsync")]
        public async Task<IActionResult> generatespeechAsync(Voice objVM)
        {
            var requestData = JsonConvert.SerializeObject(new { objVM });
            string userTransactionUUID = null;

            try
            {
                Log.WriteInfoLogWithoutMail(_module, "generatespeechAsync()", "INFOLOG ", requestData);
            }
            catch (Exception ex)
            {
            }

            try
            {
                // Retrieve authorization header from the request
                objVM.UserAPIKey = GetJWTUserAuthenticationHeaderValue();
                if (objVM.ShortName == "")
                {
                    List<VoiceMaster> obj = new List<VoiceMaster>();
                    VoiceMasterBusinessFacade voiceMasterBusiness = new VoiceMasterBusinessFacade();
                    obj = voiceMasterBusiness.VoiceMaster_GetRecordByAccent(objVM.Locale);
                    objVM.ShortName = obj[0].ShortName;
                    objVM.DisplayName = obj[0].DisplayName;
                }
                string strUserID = "";
                Users objEntity = new Users();
                if (objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml")
                {
                    //get the user details from the session
                    strUserID = _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) : "";

                    if (!string.IsNullOrEmpty(strUserID))
                    {
                        _userID = Convert.ToInt64(strUserID);
                    }

                    if (string.IsNullOrWhiteSpace(strUserID))
                    {
                        _jsonMessage = new JsonMessage(false, Resource.lbl_error, "Invalid User", KeyEnums.JsonMessageType.ERROR, "", "1");
                        return Json(_jsonMessage);
                    }
                }
                else
                {
                    strUserID = "0";

                    UsersBusinessFacade objBF = new UsersBusinessFacade();
                    objEntity = objBF.GetUserIdFromAPIKey(objVM.UserAPIKey);

                    if (objEntity.ID > 0)
                    {
                        _userID = objEntity.ID;
                    }

                    if (objEntity.ID > 0)
                    {
                        string strErrorCode = "";
                        string strErrorMessage = "Invalid Key";
                        VoiceMasterBusinessFacade voiceMasterBusinessFacade = new VoiceMasterBusinessFacade();
                        VoiceMaster voiceMaster = voiceMasterBusinessFacade.GetVoiceByShortName(objVM.ShortName, objEntity.ID);
                        if (voiceMaster == null)
                        {
                            strErrorCode = "1";
                            strErrorMessage = "Voice is not mapped to User.";
                        }

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
                            return Json(jsonResponseMessageDTO);
                        }
                    }
                }

                userTransactionUUID = await SaveUserDetailsAndGetUUID(objVM);

                try
                {
                    UsersController usersController = new UsersController();
                    usersController.InsertAccessMember(Convert.ToInt64(strUserID), "VoiceTTS", usersController.getAccessMember());
                }
                catch (Exception Ex)
                {
                }

                if (!(objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml" || objVM.TTSType == "pro"))
                {
                    TextToSpeech(objVM, userTransactionUUID, strUserID);

                    if (!string.IsNullOrEmpty(userTransactionUUID))
                    {
                        jsonResponseMessageDTO = new JsonMessageDTO(userTransactionUUID, "", KeyEnums.StatusKeys.notstarted.ToString(), "TTS not started.");
                        Log.WriteInfoLogWithoutMail(_module, "(UUID is Generated  = " + userTransactionUUID + ")", "INFOLOG ", userTransactionUUID);
                    }
                    else
                    {
                        jsonResponseMessageDTO = new JsonMessageDTO("", "", "error", "Transaction ID generated failed.");
                        Log.WriteInfoLogWithoutMail(_module, "(UUID is Generated  = " + userTransactionUUID + ")", "INFOLOG ", userTransactionUUID);
                    }

                    return Json(jsonResponseMessageDTO);
                }
                else
                {
                    _jsonMessage = (JsonMessage)await TextToSpeech(objVM, userTransactionUUID, strUserID);

                    //GoogleTTS googleTTS = new GoogleTTS(_hostingEnvironment, _httpContextAccessor, _cache);
                    //JsonMessage result = await googleTTS.GoogleTextToSpeech(objVM, userTransactionUUID, strUserID, userTransactionID);
                    //_jsonMessage = result;
                    if (objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml")
                    {
                    }
                    else if (objVM.TTSType == "pro")
                    {
                        JsonMessageDTOCallBack jsonResponseMessageDTO = new JsonMessageDTOCallBack(userTransactionUUID, _jsonMessage.ReturnUrl, KeyEnums.StatusKeys.completed.ToString(), "Completed", _jsonMessage.Data.ToString());
                        if (!_jsonMessage.IsSuccess)
                            jsonResponseMessageDTO = new JsonMessageDTOCallBack(userTransactionUUID, "", KeyEnums.StatusKeys.error.ToString(), _jsonMessage.Message);
                        return Json(jsonResponseMessageDTO);
                    }

                    return Json(_jsonMessage);
                }
            }
            catch (Exception ex)
            {
                if (objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml")
                {
                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, ex.Message);
                    Log.WriteLog(_module, "(generatespeechAsync= " + requestData + ")", ex.Source, ex.Message);
                    return Json(_jsonMessage);
                }
                else
                {
                    jsonResponseMessageDTO = new JsonMessageDTO("", "", "error", Resource.lbl_msg_internalServerErrorOccurred);
                    Log.WriteLog(_module, "(generatespeechAsync= " + requestData + ")", ex.Source, ex.Message);
                    return Json(jsonResponseMessageDTO);
                }
            }

            //Write Loginfo here  with UUID print
        }

        //[Authorize]
        //[HttpPost]
        //[Route("generatespeechAsync")]
        //public async Task<IActionResult> generatespeechAsync(Voice objVM)
        //{
        //    var requestData = JsonConvert.SerializeObject(new { objVM });
        //    string userTransactionUUID = null;

        //    var response = string.Empty;

        //    try
        //    {
        //        Log.WriteInfoLogWithoutMail(_module, "generatespeechAsync()", "INFOLOG ", requestData);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    try
        //    {
        //        //string jwtToken = "YOUR_JWT_TOKEN_HERE";
        //        // Retrieve authorization header from the request
        //        objVM.UserAPIKey = GetJWTUserAuthenticationHeaderValue();

        //        //// Retrieve claims from the JWT token
        //        //foreach (Claim claim in parsedToken.Claims)
        //        //{
        //        //    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        //        //}
        //        userTransactionUUID = await SaveUserDetailsAndGetUUID(objVM);

        //        string strUserID = "";
        //        Users objEntity = new Users();
        //        if (objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml")
        //        {
        //            //get the user details from the session
        //            strUserID = _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) : "";

        //            if (!string.IsNullOrEmpty(strUserID))
        //            {
        //                _userID = Convert.ToInt64(strUserID);
        //            }

        //            if (string.IsNullOrWhiteSpace(strUserID))
        //            {
        //                //jsonResponseMessageDTO = new JsonMessageDTO(false, Resource.lbl_error, "Invalid User", KeyEnums.JsonMessageType.ERROR);
        //                jsonResponseMessageDTO = new JsonMessageDTO(null, null, KeyEnums.StatusKeys.error, "Invalid User.");
        //                return Json(_jsonMessage);
        //            }
        //        }
        //        else
        //        {
        //            strUserID = "0";

        //            UsersBusinessFacade objBF = new UsersBusinessFacade();

        //            objEntity = objBF.GetUserIdFromAPIKey(objVM.UserAPIKey);

        //            if (objEntity.ID > 0)
        //            {
        //                _userID = objEntity.ID;
        //            }

        //            if (objEntity.ID > 0)
        //            {
        //                string strErrorCode = "";
        //                string strErrorMessage = "Invalid Key";
        //                if (objEntity == null)
        //                {
        //                    strErrorCode = "1";
        //                    strErrorMessage = "Invalid Key";
        //                }
        //                else if (objEntity.StatusId == 0)
        //                {
        //                    strErrorCode = "1";
        //                    strErrorMessage = "User Disabled.";
        //                }
        //                else if (objEntity.StatusId == 2)
        //                {
        //                    strErrorCode = "1";
        //                    strErrorMessage = "User Deleted.";
        //                }
        //                else
        //                {
        //                    strUserID = Convert.ToString(objEntity.ID);
        //                }
        //                if (strErrorCode == "1")
        //                {
        //                    // Create an instance of JsonResponseMessageDTO

        //                    jsonResponseMessageDTO = new JsonMessageDTO(null, null, KeyEnums.StatusKeys.error, "User Is Disabled.");
        //                    //return Json(jsonResponseMessageDTO);
        //                }
        //            }
        //        }
        //        //try
        //        //{
        //        //    UsersController usersController = new UsersController();
        //        //    usersController.InsertAccessMember(objEntity.ID, "TTS generatespeechAsync api ", usersController.getAccessMember());
        //        //}
        //        //catch (Exception Ex)
        //        //{
        //        //}

        //        if (!(objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml"))
        //        {
        //             _jsonMessage = await TextToSpeech(objVM, userTransactionUUID, strUserID);

        //            if (!string.IsNullOrEmpty(userTransactionUUID))
        //            {
        //                _jsonMessage = new JsonMessage(true, Resource.lbl_Cap_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, userTransactionUUID);
        //                Log.WriteInfoLogWithoutMail(_module, "(UUID is Generated  = " + userTransactionUUID + ")", "INFOLOG ", userTransactionUUID);
        //                //_jsonMessage = new JsonMessage(userTransactionUUID, Mp3AzureFilePath, KeyEnums.StatusKeys.success, "Text-to-speech conversion successful. MP3 URL available for playback.");
        //            }

        //            return Json(_jsonMessage);

        //        }
        //        else
        //        {
        //            //jsonResponseMessageDTO = (jsonResponseMessageDTO)

        //            JsonResult jsonMessage = await TextToSpeech(objVM, userTransactionUUID, strUserID);
        //            jsonMessage.StatusCode = 200;
        //            return Json(jsonMessage);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    //Write Loginfo here  with UUID print
        //    // Serialize the ApiResponse to JSON string
        //    //var jsonString = JsonSerializer.Serialize(response);

        //    return Json(jsonResponseMessageDTO);
        //}

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

        [HttpPost] // Don't remove this attribute
        private async Task<string> SaveUserDetailsAndGetUUID(Voice objVM)

        {
            //await Task.Delay(100);
            string UUID = "";
            try
            {
                tblTransaction transaction = ConvertNewTransaction(objVM);
                transaction.UUID = Guid.NewGuid().ToString();
                string userUUID = transaction.UUID;
                transaction.CreatedDate = DateTime.Now;
                transaction.CallBackUrl = objVM.CallBackUrl;
                transaction.SpeakingSpeed = objVM.SpeakingSpeed;
                transaction.Pitch = objVM.Pitch;
                transaction.UserId = _userID;
                tblTransactionBusinessFacade transactionBusinessFacade = new tblTransactionBusinessFacade();
                //callBackUrl save in transaction table.
                if (!string.IsNullOrEmpty(objVM.CallBackUrl))
                {
                    Int64 id = transactionBusinessFacade.Save(transaction);
                    if (id > 0)
                    {
                        userTransactionID = id;
                        return userUUID;
                    }
                }
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, ex.Message);
                Log.WriteLog(_module, "(SaveUserDetailsAndGetUUID= " + objVM + ")", ex.Source, ex.Message);
            }

            return null;
        }

        //[HttpPost] // Don't remove this attribute
        public async Task<JsonMessage> TextToSpeech(Voice objVM, string userTransactionUUID, string UserID)
        {
            var requestData = JsonConvert.SerializeObject(new { objVM });
            TimeSpan duration = TimeSpan.Zero;
            try
            {
                Log.WriteInfoLogWithoutMail(_module, "TextToSpeech()", "INFOLOG ", requestData);
            }
            catch (Exception ex)
            {
            }

            try
            {
                //await Task.Delay(60000);

                string strUserID = UserID;

                string strAudioFilesOutput = ConstantsCommon.AudioFilesOutput;
                string strAudioUrl = CoreDomain + "/" + strAudioFilesOutput + "/" + strUserID;
                if (!Directory.Exists(AudioPath + @"\" + strUserID))
                    Directory.CreateDirectory(AudioPath + @"\" + strUserID);

                AudioPath = AudioPath + @"\" + strUserID;

                string voiceText = !string.IsNullOrWhiteSpace(objVM.VoiceText) ? Convert.ToString(objVM.VoiceText.Trim()) : "";
                string voiceName = !string.IsNullOrWhiteSpace(objVM.ShortName) ? Convert.ToString(objVM.ShortName.Trim()) : "";
                string mp3name = voiceName.Split('-')[2];
                string displayName = !string.IsNullOrWhiteSpace(objVM.DisplayName) ? Convert.ToString(objVM.DisplayName.Trim()) : mp3name;
                string voiceRegion = !string.IsNullOrWhiteSpace(objVM.Locale) ? Convert.ToString(objVM.Locale.Trim()) : "";
                string speakingStyle = !string.IsNullOrWhiteSpace(objVM.SpeakingStyle) ? Convert.ToString(objVM.SpeakingStyle.Trim()) : "general";
                string speakingSpeed = !string.IsNullOrWhiteSpace(objVM.SpeakingSpeed) ? Convert.ToString(objVM.SpeakingSpeed.Trim()) : "0";
                string speakingPitch = !string.IsNullOrWhiteSpace(objVM.Pitch) ? Convert.ToString(objVM.Pitch.Trim()) : "0";

                string todayDate = StringFilter.GetTimestamp(DateTime.Now);
                string strFileName = displayName + "_" + todayDate + ".mp3";
                string strFilePath = strUserID + "/" + strFileName;
                bool IsSuccess = false;

                string strRequest = "";
                int retryCount = 1;
            retryRequest:

                voiceText = !string.IsNullOrWhiteSpace(objVM.VoiceText) ? Convert.ToString(objVM.VoiceText.Trim()) : "";
                if (objVM.UserAPIKey != "cmsssml" && objVM.TTSType != "pro")
                {
                    PronunciationBusinessFacade objBF = new PronunciationBusinessFacade();
                    List<Pronunciation> lstEntity = new List<Pronunciation>();

                    //string _cacheKey = "voicetts_" + _httpContextAccessor.HttpContext.Session.Id;
                    //if (retryCount == 1)
                    //{
                    //    lstEntity = objBF.GetPronunciationByUserID(strUserID, voiceRegion, voiceName);
                    //    _cache.Set(_cacheKey, lstEntity, absoluteExpiration: DateTime.Now.AddMinutes(5));
                    //}
                    //else
                    //{
                    //    lstEntity = _cache.Get<List<Pronunciation>>(_cacheKey);
                    //}
                    lstEntity = objBF.GetPronunciationByUserID(strUserID, voiceRegion, voiceName);

                     voiceText = voiceText.Replace("<break", " <break").Replace("/>", "/> ");
                    // String to be replaced
                    string[] words = voiceText.Split(' ');

                    string grapheme = "";
                    string alias = "";

                    bool isWordFound = false;
                    for (int i = 0; i < words.Length; i++)
                    {
                        foreach (Pronunciation objEntity in lstEntity)
                        {
                            // Get the grapheme and alias
                            grapheme = objEntity.InitialText;
                            alias = objEntity.AlternateText;

                            if (!string.IsNullOrWhiteSpace(grapheme) && !string.IsNullOrWhiteSpace(alias))
                            {
                                if (words[i] == grapheme)
                                {
                                    if (retryCount > 1)
                                    {
                                        words[i] = alias;
                                    }
                                    else
                                    {
                                        words[i] = "<phoneme alphabet=\"ipa\" ph=\"" + alias + "\">" + alias + "</phoneme>";// alias;
                                    }
                                    isWordFound = true;
                                    break;
                                }
                            }
                        }

                        if (isWordFound)
                        {
                            List<Pronunciation> lstItemsToRemove = lstEntity.FindAll(item => item.InitialText == grapheme);
                            if (lstItemsToRemove != null && lstItemsToRemove.Count > 0)
                            {
                                foreach (var itemToRemove in lstItemsToRemove)
                                {
                                    lstEntity.Remove(itemToRemove);
                                }
                            }
                            isWordFound = false;
                        }
                    }

                    // Reconstruct the string
                    voiceText = string.Join(" ", words);
                }
                else if (objVM.UserAPIKey == "cmsssml" || objVM.TTSType == "pro")
                {
                    if (retryCount == 1)
                    {
                        voiceText = "<phoneme alphabet=\"ipa\" ph=\"" + voiceText + "\">" + voiceText + "</phoneme>";
                    }
                }

                strRequest = "<speak xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml' version='1.0' xml:lang='" + voiceRegion + "'><voice name='" + voiceName + "'><mstts:express-as style='" + speakingStyle + "'><prosody rate='" + speakingSpeed + "%" + "' pitch='" + speakingPitch + "%" + "'>" + voiceText + "</prosody></mstts:express-as></voice></speak>";

                //In Process - 2 //update transaction table with status
                tblTransactionBusinessFacade objUpdateStatusIdTransationTable = new tblTransactionBusinessFacade();
                tblTransaction transactionsStatus = new tblTransaction();
                transactionsStatus.UUID = userTransactionUUID;
                transactionsStatus.ID = userTransactionID;
                transactionsStatus.TransactionStatus = "2";
                if (!string.IsNullOrEmpty(objVM.TTSType))
                {
                    string strTTSType = objVM.TTSType.ToLower();
                    if (strTTSType == "pro")
                    {
                        transactionsStatus.TransactionType = "2";
                    }
                    else
                    {
                        transactionsStatus.TransactionType = "1";
                    }
                }

                objUpdateStatusIdTransationTable.Save(transactionsStatus);

                using (var client1 = new HttpClient())
                {
                    client1.BaseAddress = new Uri(IndiaApiUrl);
                    client1.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConstantsCommon.OcpApimSubscriptionKey);
                    client1.DefaultRequestHeaders.Add("X-Microsoft-OutputFormat", ConstantsCommon.XMicrosoftOutputFormat);
                    client1.DefaultRequestHeaders.Add("User-Agent", ConstantsCommon.UserAgent);
                    client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ssml+xml"));

                    //HTTP POST
                    var request = new HttpRequestMessage(HttpMethod.Post, IndiaApiUrl);
                    // CONTENT-TYPE header
                    request.Content = new StringContent(strRequest, Encoding.UTF8, "application/ssml+xml");
                    await Task.Delay(200);
                    HttpResponseMessage r = await client1.SendAsync(request);

                    if (r.IsSuccessStatusCode)
                    {
                        System.Net.Http.HttpContent content = r.Content;
                        var contentStream = await content.ReadAsStreamAsync();
                        string fileSavePath = Path.Combine(AudioPath, strFileName);
                        using (FileStream outputFileStream = new FileStream(fileSavePath, FileMode.Create))
                        {
                            contentStream.CopyTo(outputFileStream);
                        }
                        IsSuccess = r.IsSuccessStatusCode;
                        //mp3 NAME AND Mp3 creation date and update against the UUID/JOBID IN TRASACTION TABLE

                        strAudioUrl = strAudioUrl + "/" + displayName + "_" + todayDate + ".mp3";

                        if (!string.IsNullOrEmpty(fileSavePath))
                        {
                            using (Mp3FileReader mp3Reader = new Mp3FileReader(fileSavePath))
                            {
                                // Get the total time (duration)
                                duration = mp3Reader.TotalTime;
                            }

                            tblTransaction transaction = new tblTransaction();
                            transaction.Duration = duration.TotalSeconds.ToString();
                            transaction.ID = userTransactionID;
                            transaction.MP3URL = strAudioUrl;
                            transaction.UUID = userTransactionUUID;
                            transaction.UserId = _userID;
                            transaction.TransactionStatus = "1";//Completed - 1 //update transaction table with status
                            if (!string.IsNullOrEmpty(objVM.TTSType))
                            {
                                string strTTSType = objVM.TTSType.ToLower();
                                if (strTTSType == "pro")
                                {
                                    transaction.TransactionType = "2";
                                }
                                else
                                {
                                    transaction.TransactionType = "1";
                                }
                            }
                            tblTransactionBusinessFacade transactionBusinessFacade = new tblTransactionBusinessFacade();
                            Int64 id = transactionBusinessFacade.Save(transaction);
                        }

                        if (!(objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml" || objVM.TTSType == "pro"))
                        {
                            //VoiceCallBack voiceCallBack = new VoiceCallBack();
                            //voiceCallBack.UUID = userTransactionUUID;
                            //voiceCallBack.MP3URL = strAudioUrl + "/" + displayName + "_" + todayDate + ".mp3";

                            ////Pick the URL from the transaction table and send it to the user.

                            ////client.BaseAddress = new Uri("https://localhost:7266/");
                            //var json = JsonConvert.SerializeObject(voiceCallBack);

                            var client = new HttpClient();
                            List<tblTransaction> lsttblTransaction = new List<tblTransaction>();
                            List<Status> lstStatus = new List<Status>();
                            if (objVM.UserAPIKey != null)
                            {
                                //tblTransactionBusinessFacade tblTransactionBusinessFacade = new tblTransactionBusinessFacade();
                                //lsttblTransaction = tblTransactionBusinessFacade.GetTTSStatus(userTransactionUUID, objVM.UserAPIKey);
                                //if(lsttblTransaction !=null && lsttblTransaction.Count > 0)
                                //{
                                JsonMessageDTOCallBack jsonResponseMessageDTO = new JsonMessageDTOCallBack(userTransactionUUID, strAudioUrl, KeyEnums.StatusKeys.completed.ToString(), "Completed", duration.TotalSeconds.ToString());
                                //}
                                //else
                                //{
                                //lsttblTransaction[0].UUID = userTransactionUUID;
                                //lsttblTransaction[0].MP3URL = "";
                                //lsttblTransaction[0].StatusMessage = KeyEnums.StatusKeys.notstarted.ToString();
                                //lsttblTransaction[0].TransactionStatus = "NotStarted";

                                //jsonResponseMessageDTO = new JsonMessageDTO(lsttblTransaction[0].UUID, lsttblTransaction[0].MP3URL, KeyEnums.StatusKeys.notstarted.ToString(), lsttblTransaction[0].StatusMessage);
                                //}

                                //jsonResponseMessageDTO = new JsonMessageDTO(lsttblTransaction[0].UUID, lsttblTransaction[0].MP3URL, KeyEnums.StatusKeys.notstarted.ToString(), lsttblTransaction[0].StatusMessage);

                                var json = JsonConvert.SerializeObject(jsonResponseMessageDTO);
                                var contentCallBack = new StringContent(json, Encoding.UTF8, "application/json");
                                var response = client.PostAsync(objVM.CallBackUrl, contentCallBack).Result;

                                //var responseString = response.Content.ReadAsStringAsync().Result;
                            }
                        }
                    }
                    else
                    {
                        if (retryCount == 1)
                        {
                            retryCount++;
                            goto retryRequest;
                        }
                        else
                        {
                            //update transaction table with error status and error message
                            string errorMessage = r.StatusCode + "," + r.ReasonPhrase;
                            //error status = 3
                            tblTransactionBusinessFacade transactionBusinessFacade = new tblTransactionBusinessFacade();
                            tblTransaction transactionAzureErrorMessage = new tblTransaction();
                            transactionAzureErrorMessage.UUID = userTransactionUUID;
                            transactionAzureErrorMessage.TransactionStatus = "3";
                            transactionAzureErrorMessage.StatusMessage = errorMessage;
                            transactionBusinessFacade.Save(transactionAzureErrorMessage);

                            _jsonMessage = new JsonMessage(false, Resource.lbl_error, errorMessage, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, errorMessage);
                            Log.WriteLog(_module, "(TextToSpeech= " + requestData + ")", errorMessage, "");

                            return _jsonMessage;
                        }
                    }

                    //this is the return url for the audio file.
                    //strAudioUrl = strAudioUrl + "/" + displayName + "_" + todayDate + ".mp3";

                    //Write Loginfo here  with UUID print
                    try
                    {
                        Log.WriteInfoLogWithoutMail(_module, "(AudioUrl= " + strAudioUrl + ")", "INFOLOG ", userTransactionUUID);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                if (IsSuccess)
                {
                    _jsonMessage = new JsonMessage(true, Resource.lbl_Cap_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, strAudioUrl, strFilePath, duration.TotalSeconds.ToString());
                }
                else
                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, "Something went wrong! Please try after some time.", KeyEnums.JsonMessageType.ERROR, "", "", requestData);
            }
            catch (Exception ex)
            {
                //update transaction table with error status and error message
                //string errorMessage = ex.Message;
                //error status = 3
                tblTransactionBusinessFacade transactionBusinessFacade = new tblTransactionBusinessFacade();
                tblTransaction transactionAzureErrorMessage = new tblTransaction();
                transactionAzureErrorMessage.ID = userTransactionID;
                transactionAzureErrorMessage.UUID = userTransactionUUID;
                transactionAzureErrorMessage.TransactionStatus = "3";
                transactionAzureErrorMessage.StatusMessage = ex.Message;
                transactionBusinessFacade.Save(transactionAzureErrorMessage);

                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, ex.Message);
                Log.WriteLog(_module, "(TextToSpeech= " + requestData + ")", ex.Source, ex.Message);
            }

            //return Json(_jsonMessage);
            return _jsonMessage;
        }

        //[HttpPost] // Don't remove this attribute
        //private async Task<JsonResult> TextToSpeech(Voice objVM, string userTransactionUUID, string UserID)
        //{
        //    var requestData = JsonConvert.SerializeObject(new { objVM });

        //    try
        //    {
        //        Log.WriteInfoLogWithoutMail(_module, "TextToSpeech()", "INFOLOG ", requestData);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    try
        //    {
        //        //await Task.Delay(60000);

        //        string strUserID = UserID;

        //        string strAudioFilesOutput = ConstantsCommon.AudioFilesOutput;
        //        string strAudioUrl = CoreDomain + "/" + strAudioFilesOutput + "/" + strUserID;
        //        if (!Directory.Exists(AudioPath + @"\" + strUserID))
        //            Directory.CreateDirectory(AudioPath + @"\" + strUserID);

        //        AudioPath = AudioPath + @"\" + strUserID;

        //        string voiceText = !string.IsNullOrWhiteSpace(objVM.VoiceText) ? Convert.ToString(objVM.VoiceText.Trim()) : "";
        //        string displayName = !string.IsNullOrWhiteSpace(objVM.DisplayName) ? Convert.ToString(objVM.DisplayName.Trim()) : "";
        //        string voiceRegion = !string.IsNullOrWhiteSpace(objVM.Locale) ? Convert.ToString(objVM.Locale.Trim()) : "";
        //        string speakingStyle = !string.IsNullOrWhiteSpace(objVM.SpeakingStyle) ? Convert.ToString(objVM.SpeakingStyle.Trim()) : "general";
        //        string voiceName = !string.IsNullOrWhiteSpace(objVM.ShortName) ? Convert.ToString(objVM.ShortName.Trim()) : "";
        //        string speakingSpeed = !string.IsNullOrWhiteSpace(objVM.SpeakingSpeed) ? Convert.ToString(objVM.SpeakingSpeed.Trim()) : "0";
        //        string speakingPitch = !string.IsNullOrWhiteSpace(objVM.Pitch) ? Convert.ToString(objVM.Pitch.Trim()) : "0";

        //        string todayDate = StringFilter.GetTimestamp(DateTime.Now);
        //        string strFileName = displayName + "_" + todayDate + ".mp3";
        //        string strFilePath = strUserID + "/" + strFileName;
        //        bool IsSuccess = false;

        //        string strRequest = "";
        //        if (objVM.UserAPIKey != "cmsssml")
        //        {
        //            // Path to the XML file
        //            string xmlFilePath = SSMLPath + @"\" + strUserID + @"\" + voiceRegion + "_region.xml";

        //            if (System.IO.File.Exists(xmlFilePath))
        //            {
        //                // Load XML content from file
        //                XDocument doc = XDocument.Load(xmlFilePath);

        //                // Get the namespace
        //                XNamespace ns = doc.Root.GetDefaultNamespace();

        //                // Get the grapheme and alias
        //                string grapheme = doc.Descendants(ns + "grapheme").FirstOrDefault()?.Value;
        //                string alias = doc.Descendants(ns + "alias").FirstOrDefault()?.Value;

        //                // String to be replaced
        //                string strText = voiceText;

        //                if (grapheme != null && alias != null)
        //                {
        //                    // Split the string into words
        //                    string[] words = strText.Split(' ');

        //                    // Replace grapheme with alias
        //                    for (int i = 0; i < words.Length; i++)
        //                    {
        //                        if (words[i] == grapheme)
        //                        {
        //                            words[i] = "<phoneme alphabet=\"ipa\" ph=\"" + alias + "\">" + grapheme + "</phoneme>";// alias;
        //                        }
        //                    }

        //                    // Reconstruct the string
        //                    strText = string.Join(" ", words);
        //                    voiceText = strText;
        //                }
        //            }
        //        }
        //        else if (objVM.UserAPIKey == "cmsssml")
        //        {
        //            voiceText = "<phoneme alphabet=\"ipa\" ph=\"" + voiceText + "\">" + voiceText + "</phoneme>";
        //        }

        //        strRequest = "<speak xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml' version='1.0' xml:lang='" + voiceRegion + "'><voice name='" + voiceName + "'><mstts:express-as style='" + speakingStyle + "'><prosody rate='" + speakingSpeed + "%" + "' pitch='" + speakingPitch + "%" + "'>" + voiceText + "</prosody></mstts:express-as></voice></speak>";

        //        // UPDATE TRANSATION TABLE WITH TRANSATION STATUS AND MP3 URL
        //        tblTransactionBusinessFacade objUpdateStatusIdTransationTable = new tblTransactionBusinessFacade();
        //        tblTransaction transactionsStatus = new tblTransaction();
        //        transactionsStatus.UUID = userTransactionUUID;
        //        transactionsStatus.TransactionStatus = "2";
        //        objUpdateStatusIdTransationTable.Save(transactionsStatus);

        //        //In Process - 2 //update transaction table with status
        //        using (var client1 = new HttpClient())
        //        {
        //            client1.BaseAddress = new Uri(IndiaApiUrl);
        //            client1.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConstantsCommon.OcpApimSubscriptionKey);
        //            client1.DefaultRequestHeaders.Add("X-Microsoft-OutputFormat", ConstantsCommon.XMicrosoftOutputFormat);
        //            client1.DefaultRequestHeaders.Add("User-Agent", ConstantsCommon.UserAgent);
        //            client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ssml+xml"));

        //            //HTTP POST
        //            var request = new HttpRequestMessage(HttpMethod.Post, IndiaApiUrl);
        //            // CONTENT-TYPE header
        //            request.Content = new StringContent(strRequest, Encoding.UTF8, "application/ssml+xml");
        //            await Task.Delay(200);
        //            HttpResponseMessage r = await client1.SendAsync(request);

        //            if (r.IsSuccessStatusCode)
        //            {
        //                System.Net.Http.HttpContent content = r.Content;
        //                var contentStream = await content.ReadAsStreamAsync();
        //                string fileSavePath = Path.Combine(AudioPath, strFileName);
        //                using (FileStream outputFileStream = new FileStream(fileSavePath, FileMode.Create))
        //                {
        //                    contentStream.CopyTo(outputFileStream);
        //                }
        //                IsSuccess = r.IsSuccessStatusCode;
        //                //mp3 NAME AND Mp3 creation date and update against the UUID/JOBID IN TRASACTION TABLE

        //                strAudioUrl = strAudioUrl + "/" + displayName + "_" + todayDate + ".mp3";

        //                // Assign the mp3 url file path for the class level field
        //                Mp3AzureFilePath = strAudioUrl;

        //                if (!string.IsNullOrEmpty(fileSavePath))
        //                {
        //                    tblTransaction transaction = new tblTransaction();
        //                    transaction.MP3URL = strAudioUrl;
        //                    transaction.UUID = userTransactionUUID;
        //                    transaction.UserId = _userID;
        //                    transaction.TransactionStatus = "1";
        //                    //Completed - 1 //update transaction table with status
        //                    tblTransactionBusinessFacade transactionBusinessFacade = new tblTransactionBusinessFacade();
        //                    Int64 id = transactionBusinessFacade.Save(transaction);
        //                }

        //                jsonResponseMessageDTO = new JsonMessageDTO(userTransactionUUID, Mp3AzureFilePath, KeyEnums.StatusKeys.success, "Text-to-speech conversion successful. MP3 URL available for playback.");

        //                if (!(objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml"))
        //                {
        //                    VoiceCallBack voiceCallBack = new VoiceCallBack();
        //                    voiceCallBack.UUID = userTransactionUUID;
        //                    voiceCallBack.MP3URL = strAudioUrl + "/" + displayName + "_" + todayDate + ".mp3";

        //                    //Pick the URL from the transaction table and send it to the user.
        //                    var client = new HttpClient();
        //                    client.BaseAddress = new Uri("https://localhost:7266/");
        //                    var json = JsonConvert.SerializeObject(voiceCallBack);
        //                    var contentCallBack = new StringContent(json, Encoding.UTF8, "application/json");
        //                    var response = client.PostAsync("Api/User/PostApi", contentCallBack).Result;

        //                    var responseString = response.Content.ReadAsStringAsync().Result;
        //                }
        //            }
        //            else
        //            {
        //                //update transaction table with error status and error message
        //                string errorMessage = r.StatusCode + "," + r.ReasonPhrase;
        //                //error status = 3
        //                tblTransactionBusinessFacade transactionBusinessFacade = new tblTransactionBusinessFacade();
        //                tblTransaction transactionAzureErrorMessage = new tblTransaction();
        //                transactionAzureErrorMessage.UUID = userTransactionUUID;
        //                transactionAzureErrorMessage.TransactionStatus = "3";
        //                transactionAzureErrorMessage.StatusMessage = errorMessage;
        //                transactionBusinessFacade.Save(transactionAzureErrorMessage);

        //                if (objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml")
        //                {
        //                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, errorMessage, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, errorMessage);
        //                    Log.WriteLog(_module, "(TextToSpeech= " + requestData + ")", errorMessage, "");

        //                    return Json(_jsonMessage);
        //                }
        //                else
        //                {
        //                    jsonResponseMessageDTO = new JsonMessageDTO(userTransactionUUID, "", KeyEnums.StatusKeys.error, errorMessage);
        //                    Log.WriteLog(_module, "(TextToSpeech= " + requestData + ")", errorMessage, "");

        //                    return Json(jsonResponseMessageDTO);
        //                }

        //            }

        //            //this is the return url for the audio file.
        //            //strAudioUrl = strAudioUrl + "/" + displayName + "_" + todayDate + ".mp3";

        //            //Write Loginfo here  with UUID print
        //            try
        //            {
        //                Log.WriteInfoLogWithoutMail(_module, "(AudioUrl= " + strAudioUrl + ")", "INFOLOG ", userTransactionUUID);
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }

        //        if (IsSuccess)
        //        {
        //            if (objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml")
        //            {
        //                _jsonMessage = new JsonMessage(true, Resource.lbl_Cap_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, strAudioUrl, strFilePath);
        //                return Json(_jsonMessage);
        //            }
        //            else
        //            {
        //                jsonResponseMessageDTO = new JsonMessageDTO(userTransactionUUID, strAudioUrl, KeyEnums.StatusKeys.success, "Text-to-speech conversion successful. MP3 URL available for playback.");
        //                return Json(jsonResponseMessageDTO);
        //            }
        //        }
        //        else
        //        {
        //            _jsonMessage = new JsonMessage(false, Resource.lbl_error, "Something went wrong! Please try after some time.", KeyEnums.JsonMessageType.ERROR);
        //            return Json(_jsonMessage);
        //        }
        //    }
        //    catch (Exception ex )
        //    {
        //        //update transaction table with error status and error message
        //        //string errorMessage = ex.Message;
        //        //error status = 3
        //        tblTransactionBusinessFacade transactionBusinessFacade = new tblTransactionBusinessFacade();
        //        tblTransaction transactionAzureErrorMessage = new tblTransaction();
        //        transactionAzureErrorMessage.UUID = userTransactionUUID;
        //        transactionAzureErrorMessage.TransactionStatus = "3";
        //        transactionAzureErrorMessage.StatusMessage = ex.Message;
        //        transactionBusinessFacade.Save(transactionAzureErrorMessage);

        //        if (objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml")
        //        {
        //            _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, ex.Message);
        //            Log.WriteLog(_module, "(TextToSpeech= " + requestData + ")", ex.Source, ex.Message);

        //            return Json(_jsonMessage);
        //        }
        //        else
        //        {
        //            jsonResponseMessageDTO = new JsonMessageDTO(null, null, KeyEnums.StatusKeys.internalservererror, "Internal server error. Please try again later.");
        //            Log.WriteLog(_module, "(TextToSpeech= " + requestData + ")", ex.Source, ex.Message);

        //            return Json(jsonResponseMessageDTO);
        //        }
        //    }
        //}

        [Authorize]
        [HttpPost]
        [Route("getvoiceartistlist")]
        public dynamic getvoiceartistlist()
        {
            string UserAPIKey = "";
            Int64 revsponse = 0;
            try
            {
                Log.WriteInfoLogWithoutMail(_module, "GetVoice()", "INFOLOG ", UserAPIKey.ToString());
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

                            jsonResponseMessageDTO = new JsonMessageDTO("", "", KeyEnums.StatusKeys.error.ToString(), "User Is Disabled.");
                            //return Json(jsonResponseMessageDTO);
                        }
                    }
                }
                try
                {
                    UsersController usersController = new UsersController();
                    usersController.InsertAccessMember(objEntity.ID, "GetUserVoice", usersController.getAccessMember());
                }
                catch (Exception ex)
                {
                }
                // List<VoiceMaster> lstVoiceMaster = new List<VoiceMaster>();
                List<Voices> lstVoice = new List<Voices>();
                dynamic lstVoiceMaster = null;
                if (UserAPIKey != "")
                {
                    VoiceMasterBusinessFacade voiceMasterBusinessFacade = new VoiceMasterBusinessFacade();
                    lstVoiceMaster = voiceMasterBusinessFacade.GetVoiceRecord(UserAPIKey);

                    _jsonMessageApi = new JsonMessageApi(true, lstVoiceMaster);
                }
                else
                    _jsonMessageApi = new JsonMessageApi(false, lstVoiceMaster);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "getvoiceartistlist(userID = " + Json(revsponse) + ")", ex.Source, ex.Message);
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "", string.Format("Method : AddDomain(objAPI), Source : {0}, Message {1}", ex.Source, ex.Message));
            }
            return _jsonMessageApi;
        }

        [Authorize]
        [HttpPost]
        [Route("getspeechstatus")]
        public dynamic getspeechstatus(Core.Entity.GetUserAPIKey getUserAPIKey)

        {
            string UserAPIKey = "";
            Int64 revsponse = 0;
            try
            {
                Log.WriteInfoLogWithoutMail(_module, "GetTTSStatus()", "INFOLOG ", getUserAPIKey.ToString());
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

                            jsonResponseMessageDTO = new JsonMessageDTO("", "", KeyEnums.StatusKeys.error.ToString(), "User Is Disabled.");
                            //return Json(jsonResponseMessageDTO);
                        }
                    }
                }
                try
                {
                    UsersController usersController = new UsersController();
                    usersController.InsertAccessMember(objEntity.ID, "GetTTSStatus", usersController.getAccessMember());
                }
                catch (Exception ex)
                {
                }

                List<tblTransaction> lsttblTransaction = new List<tblTransaction>();
                List<Status> lstStatus = new List<Status>();
                if (getUserAPIKey != null)
                {
                    tblTransactionBusinessFacade tblTransactionBusinessFacade = new tblTransactionBusinessFacade();
                    lsttblTransaction = tblTransactionBusinessFacade.GetTTSStatus(getUserAPIKey.UUID, UserAPIKey);
                    if (lsttblTransaction[0].TransactionStatus == "Completed")
                    {
                        jsonResponseMessageDTO = new JsonMessageDTO(lsttblTransaction[0].UUID, lsttblTransaction[0].MP3URL, KeyEnums.StatusKeys.completed.ToString(), lsttblTransaction[0].StatusMessage, lsttblTransaction[0].Duration);
                    }
                    if (lsttblTransaction[0].TransactionStatus == "Process")
                    {
                        jsonResponseMessageDTO = new JsonMessageDTO(lsttblTransaction[0].UUID, lsttblTransaction[0].MP3URL, KeyEnums.StatusKeys.inprogress.ToString(), lsttblTransaction[0].StatusMessage);
                    }
                    if (lsttblTransaction[0].TransactionStatus == "NotStarted")
                    {
                        jsonResponseMessageDTO = new JsonMessageDTO(lsttblTransaction[0].UUID, lsttblTransaction[0].MP3URL, KeyEnums.StatusKeys.notstarted.ToString(), lsttblTransaction[0].StatusMessage);
                    }
                }
                else
                    jsonResponseMessageDTO = new JsonMessageDTO("", "", KeyEnums.StatusKeys.error.ToString(), lsttblTransaction[0].StatusMessage);
                //_jsonMessageApi = new JsonMessageApi(false, null);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetTTSStatus(userID = " + Json(revsponse) + ")", ex.Source, ex.Message);
                jsonResponseMessageDTO = new JsonMessageDTO("", "", KeyEnums.StatusKeys.error.ToString(), "internal server error");
                //_jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "", string.Format("Method : AddDomain(objAPI), Source : {0}, Message {1}", ex.Source, ex.Message));
            }
            return jsonResponseMessageDTO;
        }

        public static tblTransaction ConvertNewTransaction(Voice voice)
        {
            return new tblTransaction
            {
                VoiceText = voice.VoiceText,
                DisplayName = voice.DisplayName,
                LocaleName = voice.Locale,
                ShortName = voice.ShortName,
                SpeakingStyle = voice.SpeakingStyle,
                SpeakingSpeed = voice.SpeakingSpeed,
                Pitch = voice.Pitch,
                UserAPIKey = voice.UserAPIKey,
            };
        }
    }
}