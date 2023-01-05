/***
*	Title："基础工具" 项目
*		主题：文件帮助类
*	Description：
*		功能：
*		    1、检查文件是否存在
*		    2、复制文件
*		    3、删除文件
*		    4、获取文件的MD5值
*	Date：2021
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    class FileHelper
    {
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePathAndName">文件的路径和名称</param>
        /// <returns>返回结果（true:表示存在）</returns>
        internal static bool IsExistFile(string filePathAndName)
        {
            bool result = File.Exists(filePathAndName);
            return result;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFileName">源文件路径和名称（比如：C:\software\test.txt）</param>
        /// <param name="destFileName">目标文件路径和名称（比如：C:\software\test.txt）</param>
        /// <param name="overwrite">是否覆盖</param>
        internal static void CopyFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (File.Exists(sourceFileName))
            {
                File.Copy(sourceFileName, destFileName, overwrite);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePathAndName">文件的路径和名称</param>
        /// <returns>返回删除结果（true:表示删除成功）</returns>
        internal static bool DeleteFile(string filePathAndName)
        {
            if (string.IsNullOrEmpty(filePathAndName)) return false;

            try
            {
                if (File.Exists(filePathAndName))
                {
                    File.Delete(filePathAndName);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="filePathAndName">文件路径和名称</param>
        /// <returns>返回文件的MD5值</returns>
        internal static string GetFileMD5(string filePathAndName)
        {
            try
            {
                using (FileStream fs=new FileStream(filePathAndName, FileMode.Open))
                {
                    var md5 = new MD5CryptoServiceProvider();
                    var retVal = md5.ComputeHash(fs);

                    var sb = new StringBuilder();
                    for (var i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }//Class_end

}
