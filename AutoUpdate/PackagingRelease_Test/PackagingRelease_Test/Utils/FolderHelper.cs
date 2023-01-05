/***
*	Title："基础工具" 项目
*		主题：目录帮助类
*	Description：
*		功能：
*		    1、获取选择的目录
*		    2、创建目录
*		    3、删除目录
*	Date：2021
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Utils
{
    class FolderHelper
    {
        /// <summary>
        /// 获取选择的目录
        /// </summary>
        /// <returns>返回选择的目录</returns>
        internal static string GetSelectDirectory()
        {
            FolderBrowserDialog dir = new FolderBrowserDialog();
            dir.ShowDialog();
            return dir.SelectedPath;

        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="floderPath">目录路径</param>
        /// <returns>返回目录的创建结果（true：表示成功）</returns>
        internal static bool CreateDirectory(string floderPath)
        {
            if (string.IsNullOrEmpty(floderPath)) return false;

            if (Directory.Exists(floderPath))
            {
                return true;
            }
            else
            {
                Directory.CreateDirectory(floderPath);
               
                return true;
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="floderPath">目录路径</param>
        /// <returns>返回删除目录结果（true:表示成功）</returns>
        internal static bool DeleteDirectory(string floderPath)
        {
            if (string.IsNullOrEmpty(floderPath)) return false;

            if (Directory.Exists(floderPath))
            {
                Directory.Delete(floderPath,true);
                return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 打开目录
        /// </summary>
        /// <param name="floderPath">目录路径</param>
        /// <returns>返回打开目录结果（true:表示成功）</returns>
        internal static bool OpenDirectory(string floderPath)
        {
            if (string.IsNullOrEmpty(floderPath)) return false;

            if (Directory.Exists(floderPath))
            {
                System.Diagnostics.Process.Start("Explorer.exe", floderPath);
                return true;
            }
            return false;
          
        }

    }//Class_end

}
