using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common
{
  public static class DirFile
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dir">此地路径相对站点而言</param>
        public static void CreateDir(string dir)
        {
            if (dir.Length == 0) return;
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(dir)))
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(dir));
        }
        /// <summary>
        /// 创建目录路径
        /// </summary>
        /// <param name="folderPath">物理路径</param>
        public static void CreateFolder(string folderPath)
        {
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);
        }
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dir">此地路径相对站点而言</param>
        public static void DeleteDir(string dir)
        {
            if (dir.Length == 0) return;
            if (System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(dir)))
                System.IO.Directory.Delete(System.Web.HttpContext.Current.Server.MapPath(dir), true);
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="file">格式:a/b.htm,相对根目录</param>
        /// <returns></returns>
        public static bool FileExists(string file)
        {
            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(file)))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="file">格式:a/b.htm,相对根目录</param>
        /// <returns></returns>
        public static string ReadFile(string file)
        {
            if (!FileExists(file))
                return "";
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath(file), System.Text.Encoding.UTF8);
                string str = sr.ReadToEnd();
                sr.Close();
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 保存为不带Bom的文件
        /// </summary>
        /// <param name="TxtStr"></param>
        /// <param name="tempDir">格式:a/b.htm,相对根目录</param>
        public static void SaveFile(string TxtStr, string tempDir)
        {
            SaveFile(TxtStr, tempDir, true);
        }
        /// <summary>
        /// 保存文件内容,自动创建目录
        /// </summary>
        /// <param name="TxtStr"></param>
        /// <param name="tempDir">格式:a/b.htm,相对根目录</param>
        /// <param name="noBom"></param>
        public static void SaveFile(string TxtStr, string tempDir, bool noBom)
        {
            try
            {
                CreateDir(GetFolderPath(true, tempDir));
                System.IO.StreamWriter sw;
                if (noBom)
                    sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath(tempDir), false, new System.Text.UTF8Encoding(false));
                else
                    sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath(tempDir), false, System.Text.Encoding.UTF8);

                sw.Write(TxtStr);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 复制文件
        /// 这个方法在6.0版本后改写，虽看似比前面的版本冗长，但避免了file2文件一直被占用的问题
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <param name="overwrite">如果已经存在是否覆盖？</param>
        public static void CopyFile(string file1, string file2, bool overwrite)
        {
            if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(file1)))
            {
                if (overwrite)
                    System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath(file1), System.Web.HttpContext.Current.Server.MapPath(file2), true);
                else
                {
                    if (!System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(file2)))
                        System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath(file1), System.Web.HttpContext.Current.Server.MapPath(file2));
                }
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file">此地路径相对程序路径而言</param>
        public static void DeleteFile(string file)
        {
            if (file.Length == 0) return;
            if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(file)))
                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(file));
        }
        /// <summary>
        /// 获得文件的目录路径
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>以\结尾</returns>
        public static string GetFolderPath(string filePath)
        {
            return GetFolderPath(false, filePath);
        }
        /// <summary>
        /// 获得文件的目录路径
        /// </summary>
        /// <param name="isUrl">是否是网址</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>以\或/结尾</returns>
        public static string GetFolderPath(bool isUrl, string filePath)
        {
            if (isUrl)
                return filePath.Substring(0, filePath.LastIndexOf("/") + 1);
            else
                return filePath.Substring(0, filePath.LastIndexOf("\\") + 1);
        }
        /// <summary>
        /// 获得文件的名称
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileName(string filePath)
        {
            return GetFileName(false, filePath);
        }
        /// <summary>
        /// 获得文件的名称
        /// </summary>
        /// <param name="isUrl">是否是网址</param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileName(bool isUrl, string filePath)
        {
            if (isUrl)
                return filePath.Substring(filePath.LastIndexOf("/") + 1, filePath.Length - filePath.LastIndexOf("/") - 1);
            else
                return filePath.Substring(filePath.LastIndexOf("\\") + 1, filePath.Length - filePath.LastIndexOf("\\") - 1);
        }
        /// <summary>
        /// 获得文件的后缀
        /// 不带点，小写
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileExt(string filePath)
        {
            return filePath.Substring(filePath.LastIndexOf(".") + 1, filePath.Length - filePath.LastIndexOf(".") - 1).ToLower();
        }
        /// <summary>
        /// 目录拷贝
        /// </summary>
        /// <param name="OldDir"></param>
        /// <param name="NewDir"></param>
        public static void CopyDir(string OldDir, string NewDir)
        {
            DirectoryInfo OldDirectory = new DirectoryInfo(OldDir);
            DirectoryInfo NewDirectory = new DirectoryInfo(NewDir);
            CopyDir(OldDirectory, NewDirectory);
        }
        private static void CopyDir(DirectoryInfo OldDirectory, DirectoryInfo NewDirectory)
        {
            string NewDirectoryFullName = NewDirectory.FullName + "\\" + OldDirectory.Name;

            if (!Directory.Exists(NewDirectoryFullName))
                Directory.CreateDirectory(NewDirectoryFullName);

            FileInfo[] OldFileAry = OldDirectory.GetFiles();
            foreach (FileInfo aFile in OldFileAry)
                File.Copy(aFile.FullName, NewDirectoryFullName + "\\" + aFile.Name, true);

            DirectoryInfo[] OldDirectoryAry = OldDirectory.GetDirectories();
            foreach (DirectoryInfo aOldDirectory in OldDirectoryAry)
            {
                DirectoryInfo aNewDirectory = new DirectoryInfo(NewDirectoryFullName);
                CopyDir(aOldDirectory, aNewDirectory);
            }
        }
        /// <summary>
        /// 目录删除
        /// </summary>
        /// <param name="OldDir"></param>
        public static void DelDir(string OldDir)
        {
            DirectoryInfo OldDirectory = new DirectoryInfo(OldDir);
            OldDirectory.Delete(true);
        }

        /// <summary>
        /// 目录剪切
        /// </summary>
        /// <param name="OldDirectory"></param>
        /// <param name="NewDirectory"></param>
        public static void CopyAndDelDir(string OldDirectory, string NewDirectory)
        {
            CopyDir(OldDirectory, NewDirectory);
            DelDir(OldDirectory);
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="_Request"></param>
        /// <param name="_Response"></param>
        /// <param name="_fullPath">源文件路径</param>
        /// <param name="_speed"></param>
        /// <returns></returns>
        public static bool DownloadFile(System.Web.HttpRequest _Request, System.Web.HttpResponse _Response, string _fullPath, long _speed)
        {
            string _fileName = GetFileName(false, _fullPath);
            try
            {
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;

                    double pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    int sleep = (int)Math.Floor(1000 * pack / _speed) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 206;
                        string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((fileLength - startBytes) / pack) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(int.Parse(pack.ToString())));
                            System.Threading.Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
