using Core.Entity.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Common
{
    public class JsonMessageDTO
    {
        #region DECLARTIONS

        //private bool _isSuccess;
        //private string _messageCaption;
        //private string _message;
        //private string _jsonMessageType;
        //private string _returnUrl;
        //private string _flag;
        //private object _data;

        #endregion DECLARTIONS

        #region PROPERTIES

        //public bool IsSuccess { get; set; }

        //public string MessageCaption { get; set; }
        public string UUID { get; set; }

        public string MP3Path { get; set; }

        public string Status { get; set; }

        public string StatusMessage { get; set; }

        public string Duration { get; set; }


        //public string Flag { get; set; }
        //public object Data { get; set; }

        #endregion PROPERTIES

        #region CONSTRUCTORS

        //public JsonMessage()
        //{
        //    //NOTHING TO INITIALIZE
        //}
        /// <summary>
        /// Default Constructor For JsonMessage
        /// </summary>
        /// <param name="isSuccess">success flag</param>
        /// <param name="messageCaption">Popups Caption Text</param>
        /// <param name="message">Message To Show Inside Popup</param>
        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>
        [JsonConstructor]
        public JsonMessageDTO(string uuid, string mp3Url, string statusKeys, string statusMessage, string duration = "0")
        {
            this.UUID = uuid;
            this.MP3Path = mp3Url;
            this.Status = statusKeys.ToString();
            this.StatusMessage = statusMessage;
            this.Duration = duration;
        }

        /// <summary>
        /// Use This Constructor To Provide a ReturnUrl To Redirect Current Request.
        /// </summary>
        /// <param name="isSuccess">success flag</param>
        /// <param name="messageCaption">Popups Caption Text</param>
        /// <param name="message">Message To Show Inside Popup</param>
        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>
        /// <param name="returnUrl">Redirect Url Where You Want To Redirect The Request</param>
        //public JsonMessageDTO(string uuid, string mp3Url, KeyEnums.StatusKeys statusKeys)
        //    : this(uuid, mp3Url, KeyEnums.StatusKeys statusKeys)
        //{
        //    this.MP3Path = mp3Url;
        //}

        /// <summary>
        /// Use This Constructor To Provide An Additional Flag To Process Certain Logic On Client Side along with ReturnUrl
        /// </summary>
        /// <param name="isSuccess">success flag</param>
        /// <param name="messageCaption">Popups Caption Text</param>
        /// <param name="message">Message To Show Inside Popup</param>
        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>
        /// <param name="returnUrl">Redirect Url Where You Want To Redirect The Request</param>
        /// <param name="flag">Additional string Value For Client Side Processing</param>
        //public JsonMessageDTO(bool isSuccess, string messageCaption, string message, KeyEnums.JsonMessageType jsonMessageType, string returnUrl, string flag)
        //    : this(isSuccess, messageCaption, message, jsonMessageType, returnUrl)
        //{
        //    this.Flag = flag;
        //}

        /// <summary>
        /// Use This Constructor If you Only Want To Provide Dynamic Object To JsonMessage
        /// </summary>
        /// <param name="isSuccess">success flag</param>
        /// <param name="messageCaption">Popups Caption Text</param>
        /// <param name="message">Message To Show Inside Popup</param>
        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>
        /// <param name="data">Dynamic Object</param>
        //public JsonMessageDTO(bool isSuccess, string messageCaption, string message, KeyEnums.JsonMessageType jsonMessageType, object data)
        //    : this(isSuccess, messageCaption, message, jsonMessageType)
        //{
        //    this.Data = data;
        //}

        /// <summary>
        /// Use This Constructor To Provide More Flexibility By Providing object Itself To Client
        /// Note : If The Object Is Too Heavy Tt May Cause A Performance Degradation !
        /// </summary>
        /// <param name="isSuccess">success flag</param>
        /// <param name="messageCaption">Popups Caption Text</param>
        /// <param name="message">Message To Show Inside Popup</param>
        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>
        /// <param name="returnUrl">Redirect Url Where You Want To Redirect The Request</param>
        /// <param name="flag">Additional string Value For Client Side Processing</param>
        /// <param name="data">Dynamic Object</param>
        //public JsonMessageDTO(bool isSuccess, string messageCaption, string message, KeyEnums.JsonMessageType jsonMessageType, string returnUrl, string flag, object data)
        //    : this(isSuccess, messageCaption, message, jsonMessageType, returnUrl, flag)
        //{
        //    this.Data = data;
        //}

        #endregion CONSTRUCTORS
    }

    public class JsonMessageDTOCallBack
    {
        #region DECLARTIONS


        #endregion DECLARTIONS

        #region PROPERTIES

        public string uuid { get; set; }

        public string mP3Path { get; set; }

        public string status { get; set; }

        public string statusMessage { get; set; }

        public string duration { get; set; }


        #endregion PROPERTIES

        #region CONSTRUCTORS

        /// <summary>
        /// Default Constructor For JsonMessage
        /// </summary>
        /// <param name="isSuccess">success flag</param>
        /// <param name="messageCaption">Popups Caption Text</param>
        /// <param name="message">Message To Show Inside Popup</param>
        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>
        [JsonConstructor]
        public JsonMessageDTOCallBack(string uuid, string mp3Url, string statusKeys, string statusMessage, string duration = "0")
        {
            this.uuid = uuid;
            this.mP3Path = mp3Url;
            this.status = statusKeys.ToString();
            this.statusMessage = statusMessage;
            this.duration = duration;
        }

        #endregion CONSTRUCTORS
    }
}
