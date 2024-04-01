//using Core.Entity.Common;
//using Core.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Caching.Memory;
//using Newtonsoft.Json;
//using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
//using System.Security.Cryptography;
//using Microsoft.AspNetCore.Authorization;
//using System.Net.Http.Headers;
//using System.Text;
//using Core.Utility.Common;
//using Core.Entity.Enums;
//using Core.Resources;
//using VoiceViewModel.VoiceConversion;
//using Core.Utility;
//using Microsoft.AspNetCore.Http;
//using Core.Business.BusinessFacade;
//using Core.Entity;

//namespace Dhvani.Controllers.Api
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VoiceController : Controller
//    {
//        private readonly string _module = "Core.Controllers.App.VoiceController";
//        private JsonMessage _jsonMessage = null;
//        private IHostingEnvironment _hostingEnvironment;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private ISession _session => _httpContextAccessor.HttpContext.Session;
//        private IMemoryCache _cache;
//        private Helper _helper;
//        private string RootPath = ConstantsCommon.RootPath;
//        private string AudioPath = ConstantsCommon.AudioPath;
//        private string IndiaApiUrl = ConstantsCommon.IndiaApiUrl;
//        private string CoreDomain = ConstantsCommon.CoreDomain;

//        public VoiceController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
//        {
//            _hostingEnvironment = environment;
//            _httpContextAccessor = httpContextAccessor;
//            _cache = cache;
//            _helper = new Helper(_httpContextAccessor);
//        }

//        [Authorize]
//        [HttpPost]
//        public async Task<IActionResult> getVoiceAsync(Voice objVM)
//        {
//            var requestData = JsonConvert.SerializeObject(new { objVM });

//            try
//            {
//                Log.WriteInfoLogWithoutMail(_module, "getVoiceAsync()", "INFOLOG ", requestData);
//            }
//            catch (Exception ex)
//            {
//            }

//            try
//            {
//                string strUserID = "";

//                if (objVM.UserAPIKey == "cms")
//                {
//                    //get the user details from the session
//                    strUserID = _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) : "";
//                    if (string.IsNullOrWhiteSpace(strUserID))
//                    {
//                        _jsonMessage = new JsonMessage(false, Resource.lbl_error, "Invalid User", KeyEnums.JsonMessageType.ERROR, "", "1");
//                        return Json(_jsonMessage);
//                    }
//                }
//                else
//                {
//                    //the request is from api call
//                    //get the user details from the api key
//                    strUserID = "0";

//                    UsersBusinessFacade objBF = new UsersBusinessFacade();
//                    Users objEntity = new Users();
//                    objEntity = objBF.GetUserIdFromAPIKey(objVM.UserAPIKey);

//                    if (objEntity.ID > 0)
//                    {
//                        string strErrorCode = "";
//                        string strErrorMessage = "Invalid Key";
//                        if (objEntity == null)
//                        {
//                            strErrorCode = "1";
//                            strErrorMessage = "Invalid Key";
//                        }
//                        else if (objEntity.StatusId == 0)
//                        {
//                            strErrorCode = "1";
//                            strErrorMessage = "User Disabled.";
//                        }
//                        else if (objEntity.StatusId == 2)
//                        {
//                            strErrorCode = "1";
//                            strErrorMessage = "User Deleted.";
//                        }
//                        else
//                        {
//                            strUserID = Convert.ToString(objEntity.ID);
//                        }
//                        if (strErrorCode == "1")
//                        {
//                            _jsonMessage = new JsonMessage(false, Resource.lbl_error, strErrorMessage, KeyEnums.JsonMessageType.ERROR, "", "", requestData);
//                            return Json(_jsonMessage);
//                        }
//                    }
//                }

//                string strAudioFilesOutput = ConstantsCommon.AudioFilesOutput;
//                string strAudioUrl = CoreDomain + "/" + strAudioFilesOutput + "/" + strUserID;
//                if (!Directory.Exists(AudioPath + @"\" + strUserID))
//                    Directory.CreateDirectory(AudioPath + @"\" + strUserID);

//                AudioPath = AudioPath + @"\" + strUserID;

//                string voiceText = !string.IsNullOrWhiteSpace(objVM.VoiceText) ? Convert.ToString(objVM.VoiceText.Trim()) : "";
//                string displayName = !string.IsNullOrWhiteSpace(objVM.DisplayName) ? Convert.ToString(objVM.DisplayName.Trim()) : "";
//                string voiceRegion = !string.IsNullOrWhiteSpace(objVM.Locale) ? Convert.ToString(objVM.Locale.Trim()) : "";
//                string speakingStyle = !string.IsNullOrWhiteSpace(objVM.SpeakingStyle) ? Convert.ToString(objVM.SpeakingStyle.Trim()) : "general";
//                string voiceName = !string.IsNullOrWhiteSpace(objVM.ShortName) ? Convert.ToString(objVM.ShortName.Trim()) : "";
//                string speakingSpeed = !string.IsNullOrWhiteSpace(objVM.SpeakingSpeed) ? Convert.ToString(objVM.SpeakingSpeed.Trim()) : "0";
//                string speakingPitch = !string.IsNullOrWhiteSpace(objVM.Pitch) ? Convert.ToString(objVM.Pitch.Trim()) : "0";

//                string todayDate = StringFilter.GetTimestamp(DateTime.Now);
//                string strFileName = displayName + "_" + todayDate + ".mp3";
//                string strFilePath = strUserID + "/" + strFileName;
//                bool IsSuccess = false;

//                string strRequest = "<speak xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml' version='1.0' xml:lang='" + voiceRegion + "'><voice name='" + voiceName + "'><mstts:express-as style='" + speakingStyle + "'><prosody rate='" + speakingSpeed + "%" + "' pitch='" + speakingPitch + "%" + "'>" + voiceText + "</prosody></mstts:express-as></voice></speak>";

//                // update values in database as status is waiting for conversion

//                using (var client1 = new HttpClient())
//                {
//                    client1.BaseAddress = new Uri(IndiaApiUrl);
//                    client1.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConstantsCommon.OcpApimSubscriptionKey);
//                    client1.DefaultRequestHeaders.Add("X-Microsoft-OutputFormat", ConstantsCommon.XMicrosoftOutputFormat);
//                    client1.DefaultRequestHeaders.Add("User-Agent", ConstantsCommon.UserAgent);
//                    client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ssml+xml"));

//                    //HTTP POST
//                    var request = new HttpRequestMessage(HttpMethod.Post, IndiaApiUrl);
//                    // CONTENT-TYPE header
//                    request.Content = new StringContent(strRequest, Encoding.UTF8, "application/ssml+xml");

//                    HttpResponseMessage r = await client1.SendAsync(request);

//                    if (r.IsSuccessStatusCode)
//                    {
//                        System.Net.Http.HttpContent content = r.Content;
//                        var contentStream = await content.ReadAsStreamAsync();
//                        string fileSavePath = Path.Combine(AudioPath, strFileName);
//                        using (FileStream outputFileStream = new FileStream(fileSavePath, FileMode.Create))
//                        {
//                            contentStream.CopyTo(outputFileStream);
//                        }
//                        IsSuccess = r.IsSuccessStatusCode;
//                    }

//                    //this is the return url for the audio file.
//                    strAudioUrl = strAudioUrl + "/" + displayName + "_" + todayDate + ".mp3";
//                }

//                if (IsSuccess)
//                    _jsonMessage = new JsonMessage(true, Resource.lbl_Cap_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, strAudioUrl, strFilePath);
//                else
//                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, "Something went wrong! Please try after some time.", KeyEnums.JsonMessageType.ERROR, "", "", requestData);
//            }
//            catch (Exception ex)
//            {
//                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, ex.Message);
//                Log.WriteLog(_module, "(getVoiceAsync= " + requestData + ")", ex.Source, ex.Message);
//            }
//            return Json(_jsonMessage);
//        }

//        /// <summary>
//        /// Created to generate the JWT KEY
//        /// </summary>
//        /// <param name="objVM"></param>
//        /// <returns></returns>
//        //[Authorize]
//        //[HttpGet]
//        //public IActionResult GetJWTKey()
//        //{
//        //    string secretKey = "";
//        //    using (var rng = new RNGCryptoServiceProvider())
//        //    {
//        //        byte[] keyBytes = new byte[50];
//        //        rng.GetBytes(keyBytes);

//        //        // Convert the byte array to a Base64-encoded string
//        //        secretKey = Convert.ToBase64String(keyBytes);
//        //    }

//        //    var keyData = new
//        //    {
//        //        Name = secretKey
//        //    };

//        //    // Return the key data as JSON
//        //    return Ok(keyData);
//        //}
//    }
//}