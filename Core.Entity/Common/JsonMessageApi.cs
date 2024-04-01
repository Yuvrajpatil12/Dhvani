//using Core.Entity.Enums;
//using System.Text.Json.Serialization;

//namespace Core.Entity.Common
//{
//    public class JsonMessageApi
//    {
//        #region DECLARTIONS

//        //private bool _isSuccess;
//        //private string _messageCaption;
//        //private string _message;
//        //private string _jsonMessageType;
//        //private string _returnUrl;
//        //private string _flag;
//        //private object _data;

//        #endregion DECLARTIONS

//        #region PROPERTIES

//        //public bool IsSuccess { get; set; }

//        public object voiceData { get; set; }

//        #endregion PROPERTIES

//        #region CONSTRUCTORS

//        //public JsonMessage()
//        //{
//        //    //NOTHING TO INITIALIZE
//        //}
//        /// <summary>
//        /// Default Constructor For JsonMessage
//        /// </summary>
//        /// <param name="isSuccess">success flag</param>
//        /// <param name="messageCaption">Popups Caption Text</param>
//        /// <param name="message">Message To Show Inside Popup</param>
//        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>

//        public JsonMessageApi(object voiceData)
//        {
//            this.voiceData = voiceData;
//        }

//        /// <summary>
//        /// Use This Constructor To Provide a ReturnUrl To Redirect Current Request.
//        /// </summary>
//        /// <param name="isSuccess">success flag</param>
//        /// <param name="messageCaption">Popups Caption Text</param>
//        /// <param name="message">Message To Show Inside Popup</param>
//        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>
//        /// <param name="returnUrl">Redirect Url Where You Want To Redirect The Request</param>

//        /// <summary>
//        /// Use This Constructor To Provide An Additional Flag To Process Certain Logic On Client Side along with ReturnUrl
//        /// </summary>
//        /// <param name="isSuccess">success flag</param>
//        /// <param name="messageCaption">Popups Caption Text</param>
//        /// <param name="message">Message To Show Inside Popup</param>
//        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>
//        /// <param name="returnUrl">Redirect Url Where You Want To Redirect The Request</param>
//        /// <param name="flag">Additional string Value For Client Side Processing</param>

//        /// <summary>
//        /// Use This Constructor If you Only Want To Provide Dynamic Object To JsonMessage
//        /// </summary>
//        /// <param name="isSuccess">success flag</param>
//        /// <param name="messageCaption">Popups Caption Text</param>
//        /// <param name="message">Message To Show Inside Popup</param>
//        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>
//        /// <param name="data">Dynamic Object</param>

//        /// <summary>
//        /// Use This Constructor To Provide More Flexibility By Providing object Itself To Client
//        /// Note : If The Object Is Too Heavy Tt May Cause A Performance Degradation !
//        /// </summary>
//        /// <param name="isSuccess">success flag</param>
//        /// <param name="messageCaption">Popups Caption Text</param>
//        /// <param name="message">Message To Show Inside Popup</param>
//        /// <param name="jsonMessageType">This Defines Header Color Of The Popup</param>
//        /// <param name="returnUrl">Redirect Url Where You Want To Redirect The Request</param>
//        /// <param name="flag">Additional string Value For Client Side Processing</param>
//        /// <param name="data">Dynamic Object</param>

//        #endregion CONSTRUCTORS
//    }
//}