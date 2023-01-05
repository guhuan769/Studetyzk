/***
*	Title："基础工具" 项目
*		主题：文件下载
*	Description：
*		功能：
*		    1、HTTP方式下载文件（可获取文件的下载进度、下载结束、下载错误）
*	Date：2021
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/
using System;
using System.IO;
using System.Net;

namespace Utils
{
    public class FileDownload
    {
        #region   基础参数
        //定义一个下载状态的委托
        public delegate void delDownloadStatus(int minValue,int maxVlue, int curValue, string downPercent);
        //声明下载状态委托
        public event delDownloadStatus downloadStatus;

        //定义一个下载结束的委托
        public delegate void delDownloadEnd();
        //声明下载结束
        public event delDownloadEnd downloadEnd;

        //定义一个异常信息的委托
        public delegate void delError(string error);
        //声明异常信息委托
        public event delError error;

        #endregion



        #region   公有方法

        /// <summary>
        /// HTTP方式下载文件
        /// </summary>
        /// <param name="URL">下载文件的URL</param>
        /// <param name="filePathAndName">文件保存的路径和名称（比如：c:\software\update.exe）</param>
        public void HttpDownload(string URL, string filePathAndName)
        {
            if (string.IsNullOrEmpty(URL) || string.IsNullOrEmpty(filePathAndName)) return ;

            try
            {
                var httpWebRequest = (HttpWebRequest) WebRequest.Create(URL);
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var totalBytes = httpWebResponse.ContentLength;
                var stream = httpWebResponse.GetResponseStream();
                using (FileStream fs=new FileStream(filePathAndName, FileMode.Create))
                {
                    long totalDownloadedByte = 0;
                    var by = new byte[2048];
                    if (stream != null)
                    {
                        var osize = stream.Read(by, 0, by.Length);
                        while (osize > 0)
                        {
                            totalDownloadedByte = osize + totalDownloadedByte;
                            fs.Write(by, 0, osize);
                            osize = stream.Read(by, 0, by.Length);
                            var percent = totalDownloadedByte / (float)totalBytes * 100;
                            downloadStatus?.Invoke(0,(int)totalBytes, (int)totalDownloadedByte,
                                "正在下载更新包：" + percent.ToString("0.00") + "%");
                        }
                    }
                }
                downloadEnd?.Invoke();
            }
            catch (Exception ex)
            {
                error(ex.Message);
                //throw new Exception(ex.Message);
            }
        }

        #endregion


    }//Class_end
}