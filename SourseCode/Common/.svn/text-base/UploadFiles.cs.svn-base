using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;
using System.Globalization;

namespace Common
{
    public class UploadFiles
    {
        public string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        public string GetFileNameByTime()
        {
            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo);
            return newFileName;
        }
        public static string GetPostfixStr(string filename)
        {
            int start = filename.LastIndexOf(".");
            int length = filename.Length;
            string postfix = filename.Substring(start + 1);
            return postfix;
        }
        public string ImageSaveAs(HttpPostedFileBase postedFile, string fileName)
        {
            string ramName = fileName.Substring(0, fileName.LastIndexOf('.'));
            string fileExt = GetPostfixStr(fileName); //文件扩展名，不含“.”
            string ramFileName = GetFileNameByTime() + "." + fileExt; //随机文件名
            string dirPath = GetUpLoadPath(); //上传目录相对路径
            string serverFileName = dirPath + ramFileName;
            //物理完整路径                    
            string toFileFullPath = GetMapPath(dirPath);
            //检查有该路径是否就创建
            if (!Directory.Exists(toFileFullPath))
            {
                Directory.CreateDirectory(toFileFullPath);
            }
            try
            {
                postedFile.SaveAs(toFileFullPath + ramFileName);
            }
            catch
            {
                return "{\"jsonrpc\" : \"2.0\", \"result\" : \"上传错误\"}";
            }
            return "{\"jsonrpc\" : \"" + ramName + "\", \"result\" : \"" + serverFileName + "\"}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postedFile">充当类的基类，这些类提供对客户端已上载的单独文件的访问</param>
        /// <param name="toFileFullPath">物理完整路径</param>
        /// <param name="serverFileName">服务器虚拟路径</param>
        /// <returns></returns>
        public string FileSaveAs(HttpPostedFileBase postedFile, ref string toFileFullPath, ref string serverFileName)
        {
            string fileName = postedFile.FileName;
            string ramName = fileName.Substring(0, fileName.LastIndexOf('.'));
            string fileExt = GetPostfixStr(fileName); //文件扩展名，不含“.”
            string ramFileName = GetFileNameByTime() + "." + fileExt; //随机文件名

            string dirPath = "/up/Files/" + DateTime.Now.ToString("yyyyMMdd") + "/"; //上传目录相对路径
            serverFileName = dirPath + ramFileName;
            //物理完整路径                    
            toFileFullPath = GetMapPath(dirPath);
            //检查有该路径是否就创建
            if (!Directory.Exists(toFileFullPath))
            {
                Directory.CreateDirectory(toFileFullPath);
            }
            try
            {
                toFileFullPath = toFileFullPath + ramFileName;
                postedFile.SaveAs(toFileFullPath);
            }
            catch
            {
                return "";
            }
            return serverFileName;
        }


        /// <summary>
        /// 返回上传目录相对路径
        /// </summary>
        /// <param name="fileName">上传文件名</param>
        private string GetUpLoadPath()
        {
            string path = "/up/image/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            return path;
        }
        /// <summary>
        /// 是否为图片文件
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        private bool IsImage(string _fileExt)
        {
            ArrayList al = new ArrayList();
            al.Add("bmp");
            al.Add("jpeg");
            al.Add("jpg");
            al.Add("gif");
            al.Add("png");
            if (al.Contains(_fileExt.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
