using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common;
using System.Web.Mvc;
using System.Text;
using System.EnterpriseServices;
using System.Configuration;


using NPOI.HPSF;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using System.Web;
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;

namespace Models
{
    //[SupportFilter]//此处如果去掉注释，则全部继承BaseController的Controller，都将执行SupportFilter过滤
    public class BaseController : Controller
    {

        /// <summary>
        /// 获取当前登陆人的用户名
        /// </summary>
        /// <returns></returns>
        public string GetCurrentPerson()
        {

            return AccountModel.GetCurrentPerson();
        }
        /// <summary>
        /// 获取当前登陆人的用户ID
        /// </summary>
        /// <returns></returns>
        public string GetCurrentPersonID()
        {
            return AccountModel.GetCurrentPersonID();
        }
        /// <summary>
        /// 获取当前登陆人的账户信息
        /// </summary>
        /// <returns>账户信息</returns>
        public Account GetCurrentAccount()
        {
            var account = AccountModel.GetCurrentAccount();

            return account;


        }
        /// <summary>
        /// 导出数据集到excle
        /// </summary>
        /// <param name="titles">第一行显示的标题名称</param>
        /// <param name="fields">字段</param>
        /// <param name="query">数据集</param>
        /// <param name="path">excle模版的位置</param>
        /// <param name="from">显示的标题默认行数为1</param>
        /// <returns></returns>
        public string WriteExcle(string[] titles, string[] fields, dynamic[] query, string path = @"~/up/b.xls", int from = 1)
        {
            HSSFWorkbook _book = new HSSFWorkbook();
            string xlsPath = System.Web.HttpContext.Current.Server.MapPath(path);

            FileStream file = new FileStream(xlsPath, FileMode.Open, FileAccess.Read);
            IWorkbook hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheet("Sheet1");
            //string guid = Guid.NewGuid().ToString();
            string guid = Result.GetNewId();
            string saveFileName = xlsPath.Path(guid);
            //超链接样式
            //HSSFCellStyle hlink_style = hssfworkbook.CreateCellStyle();
            //HSSFFont hlink_font = workbook.CreateFont();
            //hlink_font.Underline = HSSFFont.U_SINGLE;
            //hlink_font.Color = HSSFColor.BLUE.index;
            //hlink_style.SetFont(hlink_font);

            //HSSFCellStyle hlink_style1 = new HSSFCellStyle(0, ExtendedFormatRecord);
            ICellStyle hlink_style = hssfworkbook.CreateCellStyle();
            IFont hlink_font = hssfworkbook.CreateFont();
            hlink_font.Underline = FontUnderlineType.Single;
            hlink_font.Color = IndexedColors.Blue.Index;
            hlink_style.SetFont(hlink_font);
            ///
            Dictionary<string, string> propertyName;
            PropertyInfo[] properties;
            //标题行  
            HSSFRow dataRow = sheet.CreateRow(0) as HSSFRow;
            for (int i = 0; i < titles.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(titles[i]))
                {

                    dataRow.CreateCell(i).SetCellValue(titles[i]); //列值

                }
            }
            //内容行
            for (int i = 0; i < query.Length; i++)
            {
                propertyName = new Dictionary<string, string>();
                if (query[i] == null)
                {
                    continue;
                }
                Type type = query[i].GetType();
                properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo property in properties)
                {
                    object o = property.GetValue(query[i], null);
                    if (!string.IsNullOrEmpty(property.Name) && o != null)
                    {
                        propertyName.Add(property.Name, o.ToString());
                    }
                }
                int j = 0;
                dataRow = sheet.CreateRow(i + from) as HSSFRow;
                fields.All(a =>
                {
                    if (a == "CompanyName")
                    {
                        //带有超链接的列
                        //dataRow.CreateCell(j).Hyperlink.Address = propertyName[a];
                        HSSFHyperlink link = new HSSFHyperlink(HyperlinkType.Url);
                        if (propertyName.ContainsKey("CompanyInterviewJobID"))
                        {
                            link.Address = "http://www.9191offer.com/OfflineJobCenter/Index?CompanyInterviewJobID=" + propertyName["CompanyInterviewJobID"] + "&type=1";// propertyName[a];
                        }
                        ICell cell = dataRow.CreateCell(j);
                        cell.Hyperlink = link;
                        cell.SetCellValue(DropHTML(propertyName[a]));
                        cell.CellStyle = hlink_style;
                    }
                    else if (a == "JobTitle")
                    {
                        //带有超链接的列

                        HSSFHyperlink link = new HSSFHyperlink(HyperlinkType.Url);
                        if (propertyName.ContainsKey("CompanyInterviewJobID"))
                        {
                            link.Address = "http://www.9191offer.com/OfflineJobCenter/Index?CompanyInterviewJobID=" + propertyName["CompanyInterviewJobID"] + "&type=0";// propertyName[a];
                        }
                        ICell cell = dataRow.CreateCell(j);
                        cell.Hyperlink = link;
                        cell.SetCellValue(DropHTML(propertyName[a]));
                        cell.CellStyle = hlink_style;
                    }
                    else if (propertyName.ContainsKey(a)) //列名
                    {
                        dataRow.CreateCell(j).SetCellValue(propertyName[a]);
                        //列值
                    }
                    j++;
                    return true;
                });
            }
            sheet.ForceFormulaRecalculation = true;
            using (FileStream fileWrite = new FileStream(saveFileName, FileMode.Create))
            {
                hssfworkbook.Write(fileWrite);
            }


            //一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet  
            return string.Format("../../up/{0}.xls", guid);
            //记录日志

        }

        /// <summary>
        /// 导出数据集到excle
        /// </summary>
        /// <param name="titles">第一行显示的标题名称</param>
        /// <param name="fields">字段</param>
        /// <param name="query">数据集</param>
        /// <param name="path">excle模版的位置</param>
        /// <param name="from">显示的标题默认行数为1</param>
        /// <returns></returns>
        public string WriteExcles(string[] titles, string[] fields, dynamic[] query, string path = @"~/up/CallReport.xls", int from = 1)
        {
            HSSFWorkbook _book = new HSSFWorkbook();
            string xlsPath = System.Web.HttpContext.Current.Server.MapPath(path);

            FileStream file = new FileStream(xlsPath, FileMode.Open, FileAccess.Read);
            IWorkbook hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheet("Sheet1");
            string guid = Guid.NewGuid().ToString();
            string saveFileName = xlsPath.Path(guid);

            Dictionary<string, string> propertyName;
            PropertyInfo[] properties;
            //标题行  
            HSSFRow dataRow = sheet.CreateRow(0) as HSSFRow;
            for (int i = 0; i < titles.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(titles[i]))
                {

                    dataRow.CreateCell(i).SetCellValue(titles[i]); //列值

                }
            }
            //内容行
            for (int i = 0; i < query.Length; i++)
            {
                propertyName = new Dictionary<string, string>();
                if (query[i] == null)
                {
                    continue;
                }
                Type type = query[i].GetType();
                properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo property in properties)
                {
                    object o = property.GetValue(query[i], null);
                    if (!string.IsNullOrEmpty(property.Name) && o != null)
                    {
                        propertyName.Add(property.Name, o.ToString());
                    }
                }
                int j = 0;
                dataRow = sheet.CreateRow(i + from) as HSSFRow;
                fields.All(a =>
                {

                    if (propertyName.ContainsKey(a)) //列名
                    {

                        dataRow.CreateCell(j).SetCellValue(propertyName[a]);
                        //列值
                    }
                    j++;
                    return true;
                });
            }
            sheet.ForceFormulaRecalculation = true;
            using (FileStream fileWrite = new FileStream(saveFileName, FileMode.Create))
            {
                hssfworkbook.Write(fileWrite);
            }


            //一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet  
            return string.Format("../../up/{0}.xls", guid);
            //记录日志

        }

        public string WriteExcle(DataTable data)
        {

            string xlsPath = System.Web.HttpContext.Current.Server.MapPath("~/up/b.xls");
            string guid = Result.GetNewId();
            string saveFileName = xlsPath.Path(guid);
            ExcelHelper excel = new ExcelHelper(saveFileName);
            excel.DataTableToExcel(data, "Sheet1", true);
            excel.Dispose();
            return saveFileName;
        }

        public string DropHTML(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring)) return "";
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        public string ReadExcle(string path = @"~/up/b.xls", int from = 1)
        {
            HSSFWorkbook _book = new HSSFWorkbook();
            string xlsPath = System.Web.HttpContext.Current.Server.MapPath(path);

            FileStream file = new FileStream(xlsPath, FileMode.Open, FileAccess.Read);
            IWorkbook hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheet("Sheet1");
            string guid = Guid.NewGuid().ToString();
            string saveFileName = xlsPath.Path(guid);


            StringBuilder sb2 = new StringBuilder();

            string courty = "";


            //获取sheet的首行
            var headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            //for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            //{

            //}

            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;

            for (int i = 0; i <= sheet.LastRowNum; i++)
            {

                var row = sheet.GetRow(i);

                if (row != null)
                {
                    courty = row.GetCell(0).ToString();
                    if (!string.IsNullOrWhiteSpace(courty))
                    {
                        courty = courty.Split(' ')[0];
                        sb2.Append(string.Format("'{0}',", courty));


                    }
                }




            }
            var da = sb2.ToString();

            hssfworkbook = null;
            sheet = null;




            //一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet  
            return string.Format("../../up/{0}.xls", guid);
            //记录日志

        }


        /// <summary>
        /// 客户端下载
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public void DownFile(string filePath, string fileName)
        {
            //filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["AttachmentPath"] + filePath);
            byte[] bytes = null;
            FileStream fs = null;
            try
            {
                //fs = new FileStream(filePath, FileMode.Open);
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    string path = @"mylog.txt";
                    string txtPath = System.Web.HttpContext.Current.Server.MapPath(path);//获取绝对路径
                    using (StreamWriter sw = new StreamWriter(txtPath, true, Encoding.Default))
                    {
                        sw.WriteLine((ex.Message + "|客户模板导入下载|" + DateTime.Now.ToString()).ToString());
                        sw.Close();
                    }
                    return;
                }
                catch { return; }
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                fs = null;
            }
            if (bytes == null)
            {
                return;
            }
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/octet-stream";

            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileName));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            //return new EmptyResult();
        }

        public BaseController()
        { }

    }
}
