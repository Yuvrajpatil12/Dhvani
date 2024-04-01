using Core.Business.BusinessFacade;
using Core.Entity;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Models;
using Core.Resources;
using Core.Utility;
using Core.Utility.Common;
using Google.Cloud.TextToSpeech.V1;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Caching.Memory;
using NAudio.Wave;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Dhvani.GoogleClass
{
    public class GoogleTTS
    {
        private readonly string _module = "Dhvani.Controllers.GoogleTTS";
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private IMemoryCache _cache;
        private Helper _helper;
        private string strUserID = "";
        private Int64 id = 0;

        public GoogleTTS(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
            strUserID = _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) : "";
        }

        private JsonMessage _jsonMessage = null;

        public async Task<JsonMessage> GoogleTextToSpeech(VoiceViewModel.VoiceConversion.Voice objVM, string userTransactionUUID, string UserID, Int64 userTransactionID)
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
                string strUserID = UserID;
                int retryCount = 1;
                string strRequest = "";
            retryRequest:
                string CoreDomain = ConstantsCommon.CoreDomain;
                string AudioPath = ConstantsCommon.AudioPath;
                string strAudioFilesOutput = ConstantsCommon.AudioFilesOutput;
                string strAudioUrl = CoreDomain + "/" + strAudioFilesOutput + "/" + strUserID;
                if (!Directory.Exists(AudioPath + @"\" + strUserID))
                    Directory.CreateDirectory(AudioPath + @"\" + strUserID);

                AudioPath = AudioPath + @"\" + strUserID;

                // Set the path to your service account key JSON file
                string path = ConstantsCommon.PhyPath + "\\" + "dhvani-407810-daef11ee7253.json";

                // Set the environment variable
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

                // Instantiates a client
                TextToSpeechClient client = TextToSpeechClient.Create();
                string voiceText = objVM.VoiceText;
                string speakingSpeed = objVM.SpeakingSpeed;
                string speakingStyle = objVM.SpeakingStyle;
                string Pitch = objVM.Pitch;
                string voiceName = "hi-IN-Standard-A";
                string voiceRegion = "hi-IN";
                //string voiceName = objVM.ShortName;
                //string voiceRegion = objVM.Locale;

                // Construct the SSML 
                string text = "<speak>" + "<prosody rate=" + speakingSpeed + " pitch= " + Pitch + ">" + voiceText + "</prosody>" + "</speak>";

                // Perform the text-to-speech request
                SynthesizeSpeechResponse response = await client.SynthesizeSpeechAsync(
                    new SynthesisInput
                    {
                        Ssml = text
                    },
                    new VoiceSelectionParams
                    {
                        LanguageCode = voiceRegion,
                        SsmlGender = SsmlVoiceGender.Male,
                        Name = voiceName
                    },
                    new AudioConfig
                    {
                        AudioEncoding = AudioEncoding.Mp3
                    }
                );
                string strFilePath = "";
                if (response != null && response.AudioContent != null && response.AudioContent.Length > 0)
                {
                    string todayDate = StringFilter.GetTimestamp(DateTime.Now);
                    string strFileName = "GoogleVoice" + "_" + todayDate + ".mp3";
                    strFilePath = strUserID + "/" + strFileName;
                    string fileSavePath = Path.Combine(AudioPath, strFileName);

                    using (Stream output = File.Create(fileSavePath))
                    {
                        response.AudioContent.WriteTo(output);
                    }

                    // Check if the file was successfully saved
                    if (!string.IsNullOrEmpty(fileSavePath) && File.Exists(fileSavePath))
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
                        transaction.UserId = Convert.ToInt64(UserID);
                        transaction.TransactionStatus = "1"; 
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
                        id = transactionBusinessFacade.Save(transaction);
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
                        string errorMessage = "";
                        //error status = 3;
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
                if (!(objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml" || objVM.TTSType == "pro"))
                {
                }
                if (strAudioUrl != "")
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
                transactionAzureErrorMessage.ID = 0;
                transactionAzureErrorMessage.UUID = userTransactionUUID;
                transactionAzureErrorMessage.TransactionStatus = "3";
                transactionAzureErrorMessage.StatusMessage = ex.Message;
                transactionBusinessFacade.Save(transactionAzureErrorMessage);

                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, ex.Message);
                Log.WriteLog(_module, "(TextToSpeech= " + requestData + ")", ex.Source, ex.Message);
            }

            return _jsonMessage;
        }
    }
}