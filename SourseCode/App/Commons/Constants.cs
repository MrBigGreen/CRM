using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using CRM.App.SendSmsService;


namespace CRM.App.Commons
{
    public static class Constants
    {
        public static class FtpInfo
        {
            public static readonly string FtpUrl = ConfigurationManager.AppSettings["ResumeFtpUrl"].ToString();
            public static readonly string FtpUserName = ConfigurationManager.AppSettings["FtpServerUserName"].ToString();
            public static readonly string FtpPassword = ConfigurationManager.AppSettings["FtpServerPassword"].ToString();
            public static readonly string FtpServerUrl = ConfigurationManager.AppSettings["ServerFtpUrl"].ToString();
        }

        public static class ImgServerInfo
        {
            public static readonly string ImgFtpUrl = ConfigurationManager.AppSettings["ImgFtpUrl"].ToString();
            public static readonly string ImgFtpUserName = ConfigurationManager.AppSettings["ImgFtpUserName"].ToString();
            public static readonly string ImgFtpPassword = ConfigurationManager.AppSettings["ImgFtpPassword"].ToString();
            public static readonly string ImgServerUrl = ConfigurationManager.AppSettings["ImgServerUrl"].ToString();
        }

        public static class FilePath
        {

            public const string FilePathAgentyProofSettingCode = "AgentyProofSetting";
            public const string FilePathJobHunterResumeSettingCode = "JobHunterResumeSetting";

            public static readonly string FilePath_AgentyProofSetting = ConfigurationManager.AppSettings["FilePath_AgentyProofSetting"].ToString();
            public static readonly string FilePath_JobHunterResumeSetting = ConfigurationManager.AppSettings["FilePath_JobHunterResumeSetting"].ToString();

            public const string NonPicPath = @"/Images/nonpic.jpg";
            /// <summary>
            /// 默认企业图片
            /// </summary>
            public const string NonCompanyPicPath = @"/Images/noCompanyPic.jpg";
            public static string GetSubfolderPath(string fileName)
            {
                if (!string.IsNullOrEmpty(fileName) && fileName.Length > 8)
                {
                    return fileName.Substring(0, 8).ToString() + "/";
                }
                else
                {
                    return fileName;
                }
            }

            public static string GetImgUrl(string PathSetting, string FileName)
            {
                string pathSetting = string.Empty;
                switch (PathSetting)
                {
                    case Constants.FilePath.FilePathAgentyProofSettingCode:
                        pathSetting = Constants.FilePath.FilePath_AgentyProofSetting;
                        break;
                    case Constants.FilePath.FilePathJobHunterResumeSettingCode:
                        pathSetting = Constants.FilePath.FilePath_JobHunterResumeSetting;
                        break;
                    default:
                        break;
                }
                if (string.IsNullOrWhiteSpace(FileName))
                {
                    if (PathSetting.Equals(Constants.FilePath.FilePathJobHunterResumeSettingCode))
                        return "/Images/u89.png";
                    else
                    {
                        return NonPicPath;
                    }
                }
                else
                {
                    return ImgServerInfo.ImgServerUrl + pathSetting + GetSubfolderPath(FileName) + FileName;
                }
            }
        }


        public static object DeserializeFromXML(Type type, string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        public static class Sms
        {
            static SendSmsService.SDKClientClient sc = new SDKClientClient();

            /// <summary>
            /// 发送短信
            /// </summary>
            /// <param name="Mobile">手机号</param>
            /// <param name="Msg">短信内容</param>
            /// <returns></returns>
            public static int SendMsg(string Mobile, string Msg)
            {

                int Result = sc.sendSMS(ConfigurationManager.AppSettings["SmsSn"], ConfigurationManager.AppSettings["SmsPwd"], string.Empty, Mobile.Split(new char[] { ',' }), Msg,
                     string.Empty, "GBK", 5, Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                return Result;
            }
            /// <summary>
            /// 发送短信
            /// </summary>
            /// <param name="Mobile">手机号</param>
            /// <param name="Msg">短信内容</param>
            /// <returns></returns>
            public static int SendMsgs(string[] Mobile, string Msg)
            {
                int Result = sc.sendSMS(ConfigurationManager.AppSettings["SmsSn"], ConfigurationManager.AppSettings["SmsPwd"],
                    string.Empty, Mobile, Msg,
                    string.Empty, "GBK", 5, Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                return Result;
            }
        }




    }
}