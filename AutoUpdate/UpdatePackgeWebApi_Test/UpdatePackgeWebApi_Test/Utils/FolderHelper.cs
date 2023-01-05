using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Utils
{
    public class FolderHelper
    {
        #region     基础参数

        private static List<FileInfo> _FileList = new List<FileInfo>();

        #endregion 

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
        /// 获取到当前目录下的所有文件
        /// </summary>
        /// <param name="path">指定的文件夹路径</param>
        /// <returns>返回当前目录下的所有文件信息</returns>
        internal static FileInfo[] GetAllFilesOfCurrentDir(string path)
        {
            try
            {
                string[] dir = Directory.GetDirectories(path); //文件夹列表   
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] files = fdir.GetFiles();

                return files;
            }
            catch
            {

            }
            return null;
        }

        /// <summary>
        /// 获得目录下所有文件或指定文件类型文件(包含所有子文件夹)
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="extName">扩展名可以多个 例如[.mp4] [.mp3] [.wma] 等</param>
        /// <returns>List<FileInfo></returns>
        internal static List<FileInfo> GetallFile(string path, string extName)
        {
            //检查目录是否存在
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (Directory.Exists(path))
                {
                    GetAllfilesOfDir(path, extName);
                }
                else
                {
                    Directory.CreateDirectory(path);
                }
            }
            else
            {
                //注意这里的EverydayLog.Write()是我自定义的日志文件，可以根据需要保留或删除
                //EverydayLog.Write("GetAllFileOfFolder/GetallFile()/存储视频文件的路径为空，请检查！！！");
            }
            return _FileList;
        }


        #region   私有方法
        /// <summary>
        /// 递归获取指定类型文件,包含子文件夹
        /// </summary>
        /// <param name="path">指定文件夹的路径</param>
        /// <param name="extName">文件拓展名</param>
        private static void GetAllfilesOfDir(string path, string extName)
        {
             // 文件列表
             List<FileInfo> _FileList = new List<FileInfo>();

            try
            {
                string[] dir = Directory.GetDirectories(path); //文件夹列表   
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles();

                //FileInfo[] file = Directory.GetFiles(path); //文件列表  

                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
                {
                    foreach (FileInfo f in file) //显示当前目录所有文件   
                    {
                        if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                        {
                            _FileList.Add(f);
                        }
                    }
                    foreach (string d in dir)
                    {
                        GetAllfilesOfDir(d, extName);//递归   
                    }
                }
            }
            catch (Exception ex)
            {
                //注意这里的EverydayLog.Write()是我自定义的日志文件，可以根据需要保留或删除
                string strError="/GetAllFileOfFolder()/GetallfilesOfDir()/获取指定路径：" + path + "   下的文件失败！！！，错误信息=" + ex.Message;
                throw new Exception(strError);
            }
        }

       
        #endregion 


    }//Class_end
}
