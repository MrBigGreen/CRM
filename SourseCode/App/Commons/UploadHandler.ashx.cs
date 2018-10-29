using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace CRM.App.Commons
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        private string _subdir = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd");

        public void ProcessRequest(HttpContext context)
        {

            var _filedir = "/up/Contracts/";
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(_filedir)))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(_filedir));
            }
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(_filedir + _subdir)))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(_filedir + _subdir));
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
            string type = Path.GetExtension(hpf.FileName).ToLower();
            var newFileName = _subdir + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss")
                                 + rd.Next(1000, 1000000) + type;
            string filePath = System.Web.HttpContext.Current.Server.MapPath(_filedir + _subdir + "/" + newFileName);

            hpf.SaveAs(filePath);

            //try
            //{
            //    FtpClient ftpClient = new FtpClient(Constants.ImgServerInfo.ImgFtpUrl, _filedir,
            //        Constants.ImgServerInfo.ImgFtpUserName, Constants.ImgServerInfo.ImgFtpPassword);
            //    if (!ftpClient.DirectoryExist(_subdir))
            //        ftpClient.MakeDir(_subdir);
            //    ftpClient.GotoDirectory(_subdir, false);
            //    ftpClient.Upload(filePath);

            //    File.Delete(filePath);
            //}
            //catch { }

            context.Response.Write(newFileName);
        }



        #region GetPicThumbnail
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dHeight">高度</param>
        /// <param name="dWidth"></param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        bool GetPicThumbnail(Bitmap oldBitmap, string dFile, int dHeight, int dWidth, int flag)
        {
            ImageFormat tFormat = oldBitmap.RawFormat;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(oldBitmap.Width, oldBitmap.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }
            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(oldBitmap, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, oldBitmap.Width, oldBitmap.Height, GraphicsUnit.Pixel);

            g.Dispose();
            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }

            finally
            {
                oldBitmap.Dispose();
                ob.Dispose();
            }
        }
        #endregion

        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

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