using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Common.Email;
using System.Drawing;
using System.IO;

namespace Common
{
    public static class MailUtility
    {
        private static LinkedResource _logo = new LinkedResource(ImageToBytes(Resources.email_pic1), "image/jpg") { ContentId = "Logo" };
        private static LinkedResource _banner = new LinkedResource(ImageToBytes(Resources.email_pic1_2), "image/jpg") { ContentId = "Banner" };


        private static LinkedResource _phone = new LinkedResource(ImageToBytes(Resources.email_pic5), "image/jpg") { ContentId = "Phone" };
        private static LinkedResource _split = new LinkedResource(ImageToBytes(Resources.email_pic6), "image/jpg") { ContentId = "Split" };

        private static LinkedResource _icon1 = new LinkedResource(ImageToBytes(Resources.icon1), "image/jpg") { ContentId = "icon1" };
        private static LinkedResource _icon2 = new LinkedResource(ImageToBytes(Resources.icon2), "image/jpg") { ContentId = "icon2" };
        private static LinkedResource _icon3 = new LinkedResource(ImageToBytes(Resources.icon3), "image/jpg") { ContentId = "icon3" };
        private static LinkedResource _icon4 = new LinkedResource(ImageToBytes(Resources.icon4), "image/jpg") { ContentId = "icon4" };
        private static LinkedResource _icon5 = new LinkedResource(ImageToBytes(Resources.icon5), "image/jpg") { ContentId = "icon5" };
        private static LinkedResource _icon6 = new LinkedResource(ImageToBytes(Resources.icon6), "image/jpg") { ContentId = "icon6" };
        private static LinkedResource _icon7 = new LinkedResource(ImageToBytes(Resources.icon7), "image/jpg") { ContentId = "icon7" };
        private static LinkedResource _icon8 = new LinkedResource(ImageToBytes(Resources.icon8), "image/jpg") { ContentId = "icon8" };
        private static LinkedResource _btn = new LinkedResource(ImageToBytes(Resources.btn), "image/jpg") { ContentId = "btn" };

        #region 发送邮件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <param name="mailSubject"></param>
        /// <param name="body"></param>
        /// <param name="replyTo"></param>
        public static void SendMail(string mailAddress, string mailSubject, string body, string replyTo)
        {
            try
            {
                var caller = new AsyncSendMailDelegate(Utility.SendMailByReply);
                caller.BeginInvoke(mailAddress, mailSubject, body, null, replyTo, null, null);
            }
            catch (Exception e)
            {
                return;
            }
        }
        #endregion

        private static MemoryStream ImageToBytes(Image Image)
        {
            if (Image == null) { return null; }
            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap Bitmap = new Bitmap(Image))
                {
                    Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Position = 0;
                    data = new byte[ms.Length];
                    ms.Read(data, 0, Convert.ToInt32(ms.Length));
                    ms.Flush();
                }
            }
            return new MemoryStream(data);
        }

        delegate void AsyncFuncDelegate(string toMailAddress, string replyTo, string mailSubject, string body,
            List<LinkedResource> picResource);
        delegate void AsyncSendMailDelegate(string toMailAddress, string mailSubject, string body,
           List<LinkedResource> picResource, string replyTo);

    }
}
