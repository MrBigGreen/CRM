using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace CRM.App.Commons
{
    public class FTPUploadHandler : IHttpHandler
    {
        private string _subdir = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd");
        private int _defaultsize = 102400;//1M
        // Get the object used to communicate with the server.
        string ftpServerIP;
        string ftpRemotePath;
        string ftpUserID;
        string ftpPassword;
        string ftpURI;

        /// <summary>
        /// 连接FTP
        /// </summary>
        /// <param name="FtpServerIP">FTP连接地址</param>
        /// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
        /// <param name="FtpUserID">用户名</param>
        /// <param name="FtpPassword">密码</param>

        public FTPUploadHandler(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword)
        {
            ftpServerIP = FtpServerIP;
            ftpRemotePath = FtpRemotePath;
            ftpUserID = FtpUserID;
            ftpPassword = FtpPassword;
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }

        /// 上传
        /// </summary>
        /// <param name="filename"></param>
        public void Upload(string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpURI + fileInf.Name;
            FtpWebRequest reqFTP;

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                //Insert_Standard_ErrorLog.Insert("FtpWeb", "Upload Error --> " + ex.Message);
            }
        }



        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Request.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            //context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            //context.Response.Charset = "utf-8";
            var _filedir = GetPath(context.Request.QueryString["settingCode"]);
            if (!Directory.Exists(_filedir))
            {
                Directory.CreateDirectory(_filedir);
            }
            if (!Directory.Exists(_filedir + _subdir))
            {
                Directory.CreateDirectory(_filedir + _subdir);
            }

            if (context.Request.Files.Count == 0)
            {
                context.Response.Write("请选择有效文件！");
                return;
            }
            HttpPostedFile hpf = context.Request.Files[0];
            if (hpf == null)
            {
                context.Response.Write("请选择有效文件！");
                return;
            }

            Random rd = new Random();
            var newFileName = _subdir + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss")
                                 + rd.Next(1000, 1000000) + Path.GetExtension(hpf.FileName);
            var filepath = _filedir + _subdir+"/";
            FTPUploadHandler ftpu = new FTPUploadHandler("192.168.1.55",filepath,"Administrator","nxp666888");
            ftpu.Upload(newFileName);
           

            //FtpWebRequest request = (System.Net.FtpWebRequest)WebRequest.Create("ftp://www.contoso.com/test.htm");
            //request.Method = WebRequestMethods.Ftp.UploadFile;

            //// This example assumes the FTP site uses anonymous logon.
            //request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

            //// Copy the contents of the file to the request stream.
            //StreamReader sourceStream = new StreamReader("testfile.txt");
            //byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            //sourceStream.Close();
            //request.ContentLength = fileContents.Length;

            //Stream requestStream = request.GetRequestStream();
            //requestStream.Write(fileContents, 0, fileContents.Length);
            //requestStream.Close();

            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            //response.Close();
        }

         public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

      

        private string GetPath(string settingCode)
        {
            switch (settingCode)
            {
                case Constants.FilePath.FilePathAgentyProofSettingCode:
                    return Constants.FilePath.FilePath_AgentyProofSetting;
                default:
                    throw new Exception("No this path");
            }
        }
    }
}