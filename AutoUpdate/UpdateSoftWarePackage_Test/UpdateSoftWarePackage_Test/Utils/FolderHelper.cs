/***
*	Title："基础工具" 项目
*		主题：目录帮助类
*	Description：
*		功能：
*		    1、获取选择的目录
*		    2、创建目录
*		    3、删除目录
*		    4、复制目录中的所有内容到指定目录
*		    5、获取当前目录下的所有文件（不含子目录下的文件）
*		    6、获取当前目录下的所有文件（含子目录下的文件）
*		    7、获取到上级目录
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
                Directory.Delete(floderPath, true);
                return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 复制目录中的所有文件到指定目录
        /// </summary>
        /// <param name="sourceDirPath">源文件夹目录</param>
        /// <param name="saveDirPath">指定文件夹目录</param>
        internal static void CopyDirectoryAllFile(string sourceFolderPath, string saveFolderPath)
        {
            try
            {
                if (!Directory.Exists(saveFolderPath))
                {
                    Directory.CreateDirectory(saveFolderPath);
                }

                string[] files = Directory.GetFiles(sourceFolderPath);
                foreach (string file in files)
                {
                    string pFilePath = saveFolderPath + "\\" + Path.GetFileName(file);
                    //if (File.Exists(pFilePath))
                    //    continue;
                    File.Copy(file, pFilePath, true);
                }

                string[] dirs = Directory.GetDirectories(sourceFolderPath);
                foreach (string dir in dirs)
                {
                    CopyDirectoryAllFile(dir, saveFolderPath + "\\" + Path.GetFileName(dir));
                }
            }
            catch (Exception )
            {

            }
        }


        /// <summary>
        /// 获取当前目录下的所有文件（不含子目录下的文件）
        /// </summary>
        /// <param name="floderPath">目录路径</param>
        /// <returns>返回当前目录下的所有文件</returns>
        internal static FileInfo[] GetAllFileNoSubDir(string floderPath)
        {
            if (string.IsNullOrEmpty(floderPath)) return null;

            DirectoryInfo fdir = new DirectoryInfo(floderPath);
            FileInfo[] fileInfos = fdir.GetFiles();

            return fileInfos;
        }


        /// <summary>
        /// 获取当前目录下的所有文件（含子目录下的文件）
        /// </summary>
        /// <param name="floderPath">指定文件夹的路径</param>
        /// <param name="fileInfoList">FileInfo列表</param>
        internal static void GetAllFileHaveSubDir(string floderPath, ref List<FileInfo> fileInfoList)
        {
            if (string.IsNullOrEmpty(floderPath) || fileInfoList == null) return;
            try
            {
                string[] dir = Directory.GetDirectories(floderPath); //文件夹列表   
                DirectoryInfo fdir = new DirectoryInfo(floderPath);
                FileInfo[] fileInfos = fdir.GetFiles();


                if (fileInfos.Length != 0 || dir.Length != 0)                  
                {
                    foreach (FileInfo f in fileInfos) 
                    {
                        fileInfoList.Add(f);
                    }
                    foreach (string d in dir)
                    {
                        GetAllFileHaveSubDir(d, ref fileInfoList);//递归   
                    }
                }
            }
            catch (Exception )
            {
             
            }
        }


        /// <summary>
        /// 获取到上级目录
        /// </summary>
        /// <param name="curFloderPath">当前目录</param>
        /// <param name="rollbackFloderNumber">回滚的目录级数</param>
        /// <returns>返回操作的上级目录</returns>
        internal static string GetPreviousDirectory(string curFloderPath, int rollbackFloderNumber)
        {
            if (string.IsNullOrEmpty(curFloderPath) || rollbackFloderNumber <= 0) return curFloderPath;

            string path = null;

            string[] paths = curFloderPath.Split("\\");

            int length = paths.Length - rollbackFloderNumber - 1;
            for (int i = 0; i < length; i++)
            {
                path += paths[i] + "\\";
            }

            return path;
        }


    }//Class_end

}
