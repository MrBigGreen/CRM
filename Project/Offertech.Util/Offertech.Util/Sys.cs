using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace Offertech.Util
{
    /// <summary>
    /// 系统操作
    /// </summary>
    public class Sys
    {
        #region Line(换行符)

        /// <summary>
        /// 换行符
        /// </summary>
        public static string Line
        {
            get
            {
                return Environment.NewLine;
            }
        }

        #endregion

        #region Guid

        /// <summary>
        /// Guid
        /// </summary>
        public static Guid Guid
        {
            get { return Guid.NewGuid(); }
        }


        #endregion
        #region 唯一ID
        /// <summary>
        /// 唯一ID
        /// </summary>
        /// <returns></returns>
        public static string GetOID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }


        /// <summary>
        /// 唯一ID
        /// </summary>
        /// <returns></returns>
        public static long GetNumberOID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 利用时间
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrderNumber()
        {
            Random R = new Random();
            string strDateTimeNumber = DateTime.Now.ToString("yyyyMMddHHmmssms");
            string strRandomResult = R.Next(1, 1000).ToString();
            return strDateTimeNumber + strRandomResult;
        }

        /// <summary>
        /// 利用时间+GUID
        /// </summary>
        /// <returns></returns>
        public static string GetNewID()
        {
            Random R = new Random();
            string strDateTimeNumber = DateTime.Now.ToString("yyMMddHHmmssms");
            string strRandomResult = GetNumberOID().ToString().PadRight(6, '0');
            return strDateTimeNumber + strRandomResult;
        }
        #endregion

        #region GetType(获取类型)

        /// <summary>
        /// 获取类型,对可空类型进行处理
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetType<T>()
        {
            return Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        }

        #endregion

        #region GetPhysicalPath(获取物理路径)

        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        public static string GetPhysicalPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return string.Empty;
            if (HttpContext.Current == null)
            {
                if (relativePath.StartsWith("~"))
                    relativePath = relativePath.Remove(0, 2);
                return Path.GetFullPath(relativePath);
            }
            if (relativePath.StartsWith("~"))
                return HttpContext.Current.Server.MapPath(relativePath);
            if (relativePath.StartsWith("/") || relativePath.StartsWith("\\"))
                return HttpContext.Current.Server.MapPath("~" + relativePath);
            return HttpContext.Current.Server.MapPath("~/" + relativePath);
        }

        #endregion

        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\") || strPath.StartsWith("~"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 0)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion
        #region StartProcess(启动进程)

        /// <summary>
        /// 启动进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        public static void StartProcess(string processName)
        {
            Process.Start(processName);
        }

        #endregion
    }
}
