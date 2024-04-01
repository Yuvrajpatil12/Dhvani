using Core.Business.BusinessFacade;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Entity;
using Core.Models;
using Core.Resources;
using Core.Utility.Common;
using Core.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using VoiceViewModel.VoiceConversion;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.CognitiveServices.Speech;

namespace Dhvani.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextVoiceController : Controller
    {
        private readonly string _module = "Core.Controllers.App.VoiceController";
        private JsonMessage _jsonMessage = null;
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
        


        public TextVoiceController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> generatespeechAsync(Voice objVM)
        {
            var requestData = JsonConvert.SerializeObject(new { objVM });

            try
            {
                Log.WriteInfoLogWithoutMail(_module, "generatespeechAsync()", "INFOLOG ", requestData);
            }
            catch (Exception ex)
            {
            }
            
            try
            {
                string strUserID = "";

                if (objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml")
                {
                    //get the user details from the session
                    strUserID = _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) : "";
                    if (string.IsNullOrWhiteSpace(strUserID))
                    {
                        _jsonMessage = new JsonMessage(false, Resource.lbl_error, "Invalid User", KeyEnums.JsonMessageType.ERROR, "", "1");
                        return Json(_jsonMessage);
                    }
                }
                else
                {
                    //the request is from api call
                    //get the user details from the api key
                    strUserID = "0";

                    UsersBusinessFacade objBF = new UsersBusinessFacade();
                    Users objEntity = new Users();
                    objEntity = objBF.GetUserIdFromAPIKey(objVM.UserAPIKey);

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
                            _jsonMessage = new JsonMessage(false, Resource.lbl_error, strErrorMessage, KeyEnums.JsonMessageType.ERROR, "", "", requestData);
                            return Json(_jsonMessage);
                        }
                    }
                }

                string strAudioFilesOutput = ConstantsCommon.AudioFilesOutput;
                string strAudioUrl = CoreDomain + "/" + strAudioFilesOutput + "/" + strUserID;
                if (!Directory.Exists(AudioPath + @"\" + strUserID))
                    Directory.CreateDirectory(AudioPath + @"\" + strUserID);

                AudioPath = AudioPath + @"\" + strUserID;

                string voiceText = !string.IsNullOrWhiteSpace(objVM.VoiceText) ? Convert.ToString(objVM.VoiceText.Trim()) : "";
                string displayName = !string.IsNullOrWhiteSpace(objVM.DisplayName) ? Convert.ToString(objVM.DisplayName.Trim()) : "";
                string voiceRegion = !string.IsNullOrWhiteSpace(objVM.Locale) ? Convert.ToString(objVM.Locale.Trim()) : "";
                string speakingStyle = !string.IsNullOrWhiteSpace(objVM.SpeakingStyle) ? Convert.ToString(objVM.SpeakingStyle.Trim()) : "general";
                string voiceName = !string.IsNullOrWhiteSpace(objVM.ShortName) ? Convert.ToString(objVM.ShortName.Trim()) : "";
                string speakingSpeed = !string.IsNullOrWhiteSpace(objVM.SpeakingSpeed) ? Convert.ToString(objVM.SpeakingSpeed.Trim()) : "0";
                string speakingPitch = !string.IsNullOrWhiteSpace(objVM.Pitch) ? Convert.ToString(objVM.Pitch.Trim()) : "0";

                string todayDate = StringFilter.GetTimestamp(DateTime.Now);
                string strFileName = displayName + "_" + todayDate + ".mp3";
                string strFilePath = strUserID + "/" + strFileName;
                bool IsSuccess = false;

                string XmlDictionaryPathAzure = ConstantsCommon.BlobStorageUrl + "/" + SSMLPathUrl + "/" + strUserID + "/" + voiceRegion + "_region.xml";
                string strRequest = "";
                if (objVM.UserAPIKey == "cmsssml" || objVM.TTSType == "pro")
                    strRequest = "<speak xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml' version='1.0' xml:lang='" + voiceRegion + "'><voice name='" + voiceName + "'><mstts:express-as style='" + speakingStyle + "'><prosody rate='" + speakingSpeed + "%" + "' pitch='" + speakingPitch + "%" + "'>" + voiceText + "</prosody></mstts:express-as></voice></speak>";
                else
                    strRequest = "<speak xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml' version='1.0' xml:lang='" + voiceRegion + "'><voice name='" + voiceName + "'><mstts:express-as style='" + speakingStyle + "'><prosody rate='" + speakingSpeed + "%" + "' pitch='" + speakingPitch + "%" + "'><lexicon uri='" + XmlDictionaryPathAzure + "?d=" + todayDate + "'/>" + voiceText + "</prosody></mstts:express-as></voice></speak>";

                string speechKey = ConstantsCommon.OcpApimSubscriptionKey;
                string speechRegion = ConstantsCommon.VoiceRegion;

                var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);

                // The language of the voice that speaks.
                speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural";
                speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio24Khz48KBitRateMonoMp3);

                using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
                {
                    string text = "Enter some text that you want to speak";

                    var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                    OutputSpeechSynthesisResult(speechSynthesisResult, text);
                }

                if (IsSuccess)
                    _jsonMessage = new JsonMessage(true, Resource.lbl_Cap_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, strAudioUrl, strFilePath);
                else
                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, "Something went wrong! Please try after some time.", KeyEnums.JsonMessageType.ERROR, "", "", requestData);
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, ex.Message);
                Log.WriteLog(_module, "(generatespeechAsync= " + requestData + ")", ex.Source, ex.Message);
            }
            return Json(_jsonMessage);
        }

        static void OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
        {
            switch (speechSynthesisResult.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    Console.WriteLine($"Speech synthesized for text: [{text}]");
                    break;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    break;
                default:
                    break;
            }
        }


    }
}
