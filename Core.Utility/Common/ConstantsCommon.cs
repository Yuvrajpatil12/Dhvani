using Microsoft.Extensions.Configuration;

namespace Core.Utility.Common
{
    public class ConstantsCommon
    {
        private static IConfiguration Configuration;

        public static string GetAppSetting(string appSettingKey)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            Configuration = builder.Build();

            string[] SplittedKey = appSettingKey.Split(":");

            string PureKey = SplittedKey[(SplittedKey.Length) - 1];

            bool a = Configuration.GetChildren().Any(item => item.Key == appSettingKey);
            string appSettingValue = Configuration.GetValue<string>(appSettingKey);

            return appSettingValue;
        }

        #region AppSettings

        public static readonly string OnAuthorizationController = GetAppSetting("AppSettings:OnAuthorizationController");
        public static readonly string OnAuthorizationAction = GetAppSetting("AppSettings:OnAuthorizationAction");

        public static readonly string AppName = GetAppSetting("AppSettings:AppName");
        public static readonly string RootPath = GetAppSetting("AppSettings:RootPath");
        public static readonly string AudioPath = GetAppSetting("AppSettings:AudioPath");
        public static readonly string PhyPath = GetAppSetting("AppSettings:PhyPath");
		public static readonly string SSMLPath = GetAppSetting("AppSettings:SSMLPath");
        public static readonly string SSMLPathUrl = GetAppSetting("AppSettings:SSMLPathUrl");
        public static readonly string CoreDomain = GetAppSetting("AppSettings:CoreDomain");

        public static readonly string AudioFilesOutput = GetAppSetting("AppSettings:AudioFilesOutput");

        public static readonly string IndiaApiUrl = GetAppSetting("AzureVoiceSettings:IndiaApiUrl");
        public static readonly string OcpApimSubscriptionKey = GetAppSetting("AzureVoiceSettings:OcpApimSubscriptionKey");
        public static readonly string XMicrosoftOutputFormat = GetAppSetting("AzureVoiceSettings:XMicrosoftOutputFormat");
        public static readonly string UserAgent = GetAppSetting("AzureVoiceSettings:UserAgent");
        public static readonly string VoiceRegion = GetAppSetting("AzureVoiceSettings:VoiceRegion");  

        public static readonly string shortURL = GetAppSetting("AppSettings:shortURL");
        public static readonly string Domain = GetAppSetting("AppSettings:Domain");
        public static readonly string DefaultController = GetAppSetting("AppSettings:DefaultController");
        public static readonly string DefaultView = GetAppSetting("AppSettings:DefaultView");
        public static readonly string LoginCookie = GetAppSetting("AppSettings:LoginCookie");
        public static readonly string SuperAdminDefaultController = GetAppSetting("AppSettings:SuperAdminDefaultController");
        public static readonly string SuperAdminDefaultView = GetAppSetting("AppSettings:SuperAdminDefaultView");
        public static readonly string AdminDefaultController = GetAppSetting("AppSettings:AdminDefaultController");
        public static readonly string AdminDefaultView = GetAppSetting("AppSettings:AdminDefaultView");
        public static readonly string UserDefaultController = GetAppSetting("AppSettings:UserDefaultController");
        public static readonly string UserDefaultView = GetAppSetting("AppSettings:UserDefaultView");


        #endregion AppSettings

        #region AppMailerSettings

        public static readonly string UseSmtpCredentials = GetAppSetting("AppMailerSettings:UseSmtpCredentials");
        public static readonly string UseDefaultCredentials = GetAppSetting("AppMailerSettings:UseDefaultCredentials");
        public static readonly string SmtpUsername = GetAppSetting("AppMailerSettings:SmtpUsername");
        public static readonly string SmtpPassword = GetAppSetting("AppMailerSettings:SmtpPassword");
        public static readonly string SmtpHost = GetAppSetting("AppMailerSettings:SmtpHost");
        public static readonly string SmtpPort = GetAppSetting("AppMailerSettings:SmtpPort");
        public static readonly string EnableSsl = GetAppSetting("AppMailerSettings:EnableSsl");
        public static readonly string DefaultHost = GetAppSetting("AppMailerSettings:DefaultHost");

        public static readonly string ResetPassword_Subject = GetAppSetting("AppMailerSettings:ResetPassword_Subject");
        public static readonly string WelcomeEmail_Subject = GetAppSetting("AppMailerSettings:WelcomeEmail_Subject");
        public static readonly string Assessment_Approval_Subject = GetAppSetting("AppMailerSettings:Assessment_Approval_Subject");
        public static readonly string Assessment_Approved_Subject = GetAppSetting("AppMailerSettings:Assessment_Approved_Subject");
        public static readonly string Assessment_Rejection_Subject = GetAppSetting("AppMailerSettings:Assessment_Rejection_Subject");
        public static readonly string Portfolio_Approval = GetAppSetting("AppMailerSettings:Portfolio_Approval");
        public static readonly string Portfolio_Approved = GetAppSetting("AppMailerSettings:Portfolio_Approved");
        public static readonly string Portfolio_Rejection = GetAppSetting("AppMailerSettings:Portfolio_Rejection");
        public static readonly string BulkStudentUpload = GetAppSetting("AppMailerSettings:BulkStudentUpload");

        #endregion AppMailerSettings

        #region AppMailer

        public static readonly string DefaultmailID = GetAppSetting("AppMailer:DefaultmailID");
        public static readonly string Account_Confirmation = GetAppSetting("AppMailer:Account_Confirmation");
        public static readonly string ReplyToEmail = GetAppSetting("AppMailer:ReplyToEmail");
        public static readonly string CareEmail = GetAppSetting("AppMailer:CareEmail");
        public static readonly string EmailForBulkUploadConsole = GetAppSetting("AppMailer:EmailForBulkUploadConsole");

        #endregion AppMailer

        #region Logging

        public static readonly string LOG_FOLDER_PATH = GetAppSetting("Logging:LOG_FOLDER_PATH");
        public static readonly string LOG_EMAIL_SENDER = GetAppSetting("Logging:LogMailer:LOG_EMAIL_SENDER");
        public static readonly string LOG_EMAIL_RECEIVER = GetAppSetting("Logging:LogMailer:LOG_EMAIL_RECEIVER");
        public static readonly string LOG_EMAIL_SUBJECT = GetAppSetting("Logging:LogMailer:LOG_EMAIL_SUBJECT");
        public static readonly string LOG_EMAIL_IS_SEND = GetAppSetting("Logging:LogMailer:LOG_EMAIL_IS_SEND");
        public static readonly string LOG_EMAIL_CC = GetAppSetting("Logging:LogMailer:LOG_EMAIL_CC");
        public static readonly string LOG_EMAIL_BCC = GetAppSetting("Logging:LogMailer:LOG_EMAIL_BCC");
        public static readonly string ErrorLogEmailSubject = GetAppSetting("Logging:LogMailer:ErrorLogEmailSubject");

        #endregion Logging

        #region AppEncryption

        public static readonly string AESUserEncrryptKey = GetAppSetting("AppEncryption:AESUserEncrryptKey");
        public static readonly string AESUserVector = GetAppSetting("AppEncryption:AESUserVector");
        public static readonly string AESUserSalt = GetAppSetting("AppEncryption:AESUserSalt");

        #endregion AppEncryption


        #region JWTSettings
        public static readonly string JWT_expires = GetAppSetting("JWTSettings:expires");
        public static readonly string JWT_issuer = GetAppSetting("JWTSettings:issuer");
        public static readonly string JWT_audience = GetAppSetting("JWTSettings:audience");
        public static readonly string JWT_secret = GetAppSetting("JWTSettings:secret");
        #endregion JWTSettings

        #region BlobSettings
        public static readonly string BlobStorageUrl = GetAppSetting("BlobSettings:BlobStorageUrl");
        public static readonly string BlobContainerName = GetAppSetting("BlobSettings:BlobContainerName");
		public static readonly string BlobStorageConnectionString = GetAppSetting("BlobSettings:BlobStorageConnectionString");
		#endregion BlobSettings

		public static readonly string CoreDomainSiteRUL = URLDetails.GetSiteRootUrl().TrimEnd('/');

        public static readonly int SQLCommandTimeOut = GetAppSetting("ConnectionStrings:SQLCommandTimeOut") != "" ? Convert.ToInt32(GetAppSetting("SQLCommandTimeOut")) : 0;

        //Paths
       
        public static readonly string UploadFetchTempVir = @"/ALLContent/Temp/";
        public static readonly string UploadFetchTempPhy = @"\ALLContent\Temp\";

        public static readonly string ContentNoLogoPathVir = "/Content/images/no-logo.jpg";

    }
}