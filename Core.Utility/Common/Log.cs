﻿using DataSecurity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;
using System.Web;

namespace Core.Utility.Common
{
    public static class Log
    {
        //static string Errorpath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Error\";
        private static string Errorpath;

        private static string _module = "Core.Utility.Common.Log";
        private static IConfiguration Configuration;

        static Log()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            Configuration = builder.Build();

            Errorpath = ConstantsCommon.PhyPath + @"\Error\";
            DirectoryInfo Dir = new DirectoryInfo(Path.GetDirectoryName(Errorpath));
            if (!Dir.Exists)
            {
                Dir.Create();
            }
        }

        public static void WriteLog(string fromModule, string fromMethod, string errSource, string errMessage, Exception pex = null)
        {
            var line = 0;
            try
            {
                string sDate = DateTime.Now.Date.ToString("yyyyMMMdd");
                FileStream logFile = new FileStream(Errorpath + "[" + sDate + "].log", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(logFile);

                // Write to the file using StreamWriter class
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                streamWriter.Write("LogTime : ");
                streamWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), "");
                streamWriter.WriteLine("Module Name : " + fromModule + " (" + fromMethod + ")\n");
                streamWriter.WriteLine("Error Source : " + errSource + "\n ");
                streamWriter.WriteLine("Error Message : " + errMessage + "\n ");

                if (pex != null)
                {
                    StackTrace st = new StackTrace(pex, true);
                    // Get the top stack frame
                    if (st.FrameCount > 0)
                    {
                        var frame = st.GetFrame(0);
                        streamWriter.WriteLine("Error Message GetMethod FullName : " + frame.GetMethod().Name.ToString() + "\n ");
                        streamWriter.WriteLine("Error Message GetFileName : " + Path.GetFileName(frame.GetFileName()) + "\n ");
                        streamWriter.WriteLine("Error Message GetFileLineNumber : " + frame.GetFileLineNumber() + "\n ");
                        // Get the line number from the stack frame
                        line = frame.GetFileLineNumber();
                    }
                }

                streamWriter.WriteLine(" \n ");
                streamWriter.WriteLine("======================================== \n ");
                streamWriter.Flush();
                streamWriter.Close();
                logFile.Close();
                // logMail("[" + sDate + "].log");
            }
            catch (IOException exio)
            {
                Debug.Print(exio.ToString());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
            finally
            {
            }
        }

        public static void WriteInfoLog(string fromModule, string fromMethod, string errSource, string errMessage, Exception pex = null)
        {
            var line = 0;
            try
            {
                string sDate = DateTime.Now.Date.ToString("yyyyMMMdd");
                FileStream logFile = new FileStream(Errorpath + "[" + sDate + "].Infolog", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(logFile);

                // Write to the file using StreamWriter class
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                streamWriter.Write("LogTime : ");
                streamWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), "");
                streamWriter.WriteLine("Module Name : " + fromModule + " (" + fromMethod + ")\n");
                streamWriter.WriteLine("Error Source : " + errSource + "\n ");
                streamWriter.WriteLine("Error Message : " + errMessage + "\n ");

                if (pex != null)
                {
                    StackTrace st = new StackTrace(pex, true);
                    // Get the top stack frame
                    if (st.FrameCount > 0)
                    {
                        var frame = st.GetFrame(0);
                        streamWriter.WriteLine("Error Message GetMethod FullName : " + frame.GetMethod().Name.ToString() + "\n ");
                        streamWriter.WriteLine("Error Message GetFileName : " + Path.GetFileName(frame.GetFileName()) + "\n ");
                        streamWriter.WriteLine("Error Message GetFileLineNumber : " + frame.GetFileLineNumber() + "\n ");
                        // Get the line number from the stack frame
                        line = frame.GetFileLineNumber();
                    }
                }

                streamWriter.WriteLine(" \n ");
                streamWriter.WriteLine("======================================== \n ");
                streamWriter.Flush();
                streamWriter.Close();
                logFile.Close();
                //logMail("[" + sDate + "].log");
            }
            catch (IOException exio)
            {
                Debug.Print(exio.ToString());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
            finally
            {
            }
        }

        public static void WriteLogWithoutMail(string fromModule, string fromMethod, string errSource, string errMessage, Exception pex = null)
        {
            try
            {
                string sDate = DateTime.Now.Date.ToString("yyyyMMMdd");
                // string Errorpath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Error\";
                FileStream logFile = new FileStream(Errorpath + "[" + sDate + "].log", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(logFile);

                // Write to the file using StreamWriter class
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                streamWriter.Write("LogTime : ");
                streamWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), "");
                streamWriter.WriteLine("Module Name : " + fromModule + " (" + fromMethod + ")\n");
                streamWriter.WriteLine("Error Source : " + errSource + "\n ");
                streamWriter.WriteLine("Error Message : " + errMessage + "\n ");
                streamWriter.WriteLine(" \n ");
                streamWriter.WriteLine("======================================== \n ");
                streamWriter.Flush();
                streamWriter.Close();
                logFile.Close();
                //logMail("[" + sDate + "]_NoMail.log");
            }
            catch (IOException exio)
            {
                Debug.Print(exio.ToString());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
            finally
            {
            }
        }

        public static void logMail(string logFile)
        {
            string Sender_Email = Configuration.GetValue<string>("AppMailer:DefaultmailID");
            string Receiver_Email = Configuration.GetValue<string>("Logging:LogMailer:LOG_EMAIL_RECEIVER");

            string logPath = Errorpath + logFile + "";
            try
            {
                Email email = new Email(Sender_Email, Receiver_Email);
                string FileUrl = ConstantsCommon.CoreDomain + @"/Bug/DownLoadFile?Id=" + HttpUtility.UrlEncode(EncryptDecrypt.EncryptNew(logFile));
                email.IsBodyHTML = true;
                email.Subject = Configuration.GetValue<string>("Logging:LogMailer:LOG_EMAIL_SUBJECT");
                email.MailPriority = Email.EmailPriority.Normal;
                string Body = "<a href=\"" + FileUrl + "\" target=\"_blank\">Click here</a> for open error file";
                email.Body = Body;

                bool status = email.Send();
                var requestObj = "";
                Log.WriteInfoLog(_module, "Email Sent = " + status + " From " + Sender_Email + " TO " + Receiver_Email + "Email template: for open error file", requestObj, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void WriteInfoLogWithoutMail(string fromModule, string fromMethod, string errSource, string errMessage)
        {
            try
            {
                Errorpath = ConstantsCommon.PhyPath + @"\Error\";

                string sDate = DateTime.Now.Date.ToString("yyyyMMMdd");
                // string Errorpath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Error\";
                FileStream logFile = new FileStream(Errorpath + "[" + sDate + "].Infolog", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(logFile);

                // Write to the file using StreamWriter class
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                streamWriter.Write("LogTime : ");
                streamWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), "");
                streamWriter.WriteLine("Module Name : " + fromModule + " (" + fromMethod + ")\n");
                streamWriter.WriteLine("Error Source : " + errSource + "\n ");
                streamWriter.WriteLine("Error Message : " + errMessage + "\n ");
                streamWriter.WriteLine(" \n ");
                streamWriter.WriteLine("======================================== \n ");
                streamWriter.Flush();
                streamWriter.Close();
                logFile.Close();
            }
            catch (IOException exio)
            {
                Debug.Print(exio.ToString());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
            finally
            {
            }
        }

        public static void WriteLogWeeklyReport(string fromModule, string fromMethod, string errSource, string errMessage, Exception pex = null)
        {
            var line = 0;
            try
            {
                string sDate = DateTime.Now.Date.ToString("yyyyMMMdd");
                FileStream logFile = new FileStream(Errorpath + "[" + sDate + "]_WeeklyReport.log", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(logFile);

                // Write to the file using StreamWriter class
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                streamWriter.Write("LogTime : ");
                streamWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), "");
                streamWriter.WriteLine("Module Name : " + fromModule + " (" + fromMethod + ")\n");
                streamWriter.WriteLine("Error Source : " + errSource + "\n ");
                streamWriter.WriteLine("Error Message : " + errMessage + "\n ");

                if (pex != null)
                {
                    StackTrace st = new StackTrace(pex, true);
                    // Get the top stack frame
                    if (st.FrameCount > 0)
                    {
                        var frame = st.GetFrame(0);
                        streamWriter.WriteLine("Error Message GetMethod FullName : " + frame.GetMethod().Name.ToString() + "\n ");
                        streamWriter.WriteLine("Error Message GetFileName : " + Path.GetFileName(frame.GetFileName()) + "\n ");
                        streamWriter.WriteLine("Error Message GetFileLineNumber : " + frame.GetFileLineNumber() + "\n ");
                        // Get the line number from the stack frame
                        line = frame.GetFileLineNumber();
                    }
                }

                streamWriter.WriteLine(" \n ");
                streamWriter.WriteLine("======================================== \n ");
                streamWriter.Flush();
                streamWriter.Close();
                logFile.Close();
                // logMail("[" + sDate + "].log");
            }
            catch (IOException exio)
            {
                Debug.Print(exio.ToString());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
            finally
            {
            }
        }
    }

    public static class DataRecordExtensions
    {
        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}