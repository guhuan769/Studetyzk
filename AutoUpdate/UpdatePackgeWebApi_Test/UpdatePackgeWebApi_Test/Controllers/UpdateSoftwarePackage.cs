using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace UpdatePackgeWebApi_Test.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UpdateSoftwarePackage:ControllerBase
    {
        #region   基础参数

        //程序更新配置文件名称
        private const string _updatePackageFileName = "UpdateInfo.xml";

        #endregion


        #region   公有方法


        //获取到程序更新包的更新文件
        [HttpGet]
        public Task<IActionResult> GetUpdateFile2()
        {
            FileInfo[] fileInfos = GetUpdateSoftwarePackageAllFiles();

            if (fileInfos == null || fileInfos.Length <= 0) return null;

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.FullName.Contains(".xml"))
                {
                    return SegmentDownloadFile(fileInfo);
                }
            }

            return null;

        }


        //获取到程序更新包的更新文件
        [HttpGet]
        public Task<IActionResult> GetUpdateFile3()
        {
            FileInfo[] fileInfos = GetUpdateSoftwarePackageAllFiles();

            if (fileInfos == null || fileInfos.Length <= 0) return null;

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.FullName.Contains(".zip"))
                {
                    return SegmentDownloadFile(fileInfo);
                }
            }

            return null;

        }

        //获取到程序更新包的更新文件
        [HttpGet]
        public Task<IActionResult> GetUpdatePackageConfig()
        {
            return  GetAppointFile(".xml");
        }

        //获取到程序更新包文件
        [HttpGet]
        public Task<IActionResult> GetUpdatePackageFile()
        {
            return GetAppointFile(".zip");
        }

        #endregion


        #region   私有方法

        //获取到指定文件
        private Task<IActionResult> GetAppointFile(string extension)
        {
            if (string.IsNullOrEmpty(extension)) return null;

            FileInfo[] fileInfos = GetUpdateSoftwarePackageAllFiles();

            if (fileInfos == null || fileInfos.Length <= 0) return null;

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.FullName.Contains(extension))
                {
                    return GetFileStreamResult(fileInfo);
                }
            }

            return null;
        }

        /// <summary>
        /// 获取到程序更新包路径
        /// </summary>
        /// <returns>返回程序更新包的路径</returns>
        private string GetUpdateSoftwarePackagePath()
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "UpdatePackage\\";
            bool result = FolderHelper.CreateDirectory(strPath);

            if (!result)
            {
                return null;
            }
            else
            {
                return strPath;
            }
        }

        /// <summary>
        /// 获取到程序更新包的所有文件
        /// </summary>
        /// <returns></returns>
        private FileInfo[] GetUpdateSoftwarePackageAllFiles()
        {
            string strPath = GetUpdateSoftwarePackagePath();

            if (string.IsNullOrEmpty(strPath)) return null;

            FileInfo[] fileInfos = FolderHelper.GetAllFilesOfCurrentDir(strPath);

            return fileInfos;
        }


        /// <summary>
        /// 获取到文件流结果
        /// </summary>
        /// <param name="fileInfo">FileInfo文件</param>
        /// <returns>返回FileStreamResult</returns>
        private async Task<IActionResult> GetFileStreamResult(FileInfo fileInfo)
        {
            if (fileInfo == null) return NotFound();
            

            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(fileInfo.FullName, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Seek(0, SeekOrigin.Begin);

            //文件名必须编码，否则会有特殊字符(如中文)无法在此下载。
            string encodeFilename = System.Net.WebUtility.UrlEncode(fileInfo.Name);
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + encodeFilename);
            return new FileStreamResult(memoryStream, "application/octet-stream");//文件流方式，指定文件流对应的ContenType。
        }



        /// <summary>
        /// 分段下载数据
        /// </summary>
        /// <param name="fileInfo">FileInfo文件</param>
        /// <returns>返回响应结果</returns>
        private async Task<IActionResult> SegmentDownloadFile(FileInfo fileInfo)
        {
            if (fileInfo == null) return NotFound();

            int index = 0;

            using (FileStream fs=new FileStream(fileInfo.FullName,FileMode.Open))
            {
                if (fs.Length <= 0)
                {
                    return Ok(new { code = -1, msg = "文件尚未处理" });
                }
                int shardSize = 1 * 1024 * 1024;//一次1M
                int count = (int)(fs.Length / shardSize);

                if ((fs.Length % shardSize) > 0)
                {
                    count += 1;
                }
                if (index > count - 1)
                {
                    return Ok(new { code = -1, msg = "无效的下标" });
                }

                fs.Seek(index * shardSize, SeekOrigin.Begin);

                if (index == count - 1)
                {
                    //最后一片 = 总长 - (每次片段大小 * 已下载片段个数)
                    shardSize = (int)(fs.Length - (shardSize * index));
                }
                byte[] datas = new byte[shardSize];
                await fs.ReadAsync(datas, 0, datas.Length);
                
                return File(datas, "application/octet-stream");
            }
            
        }


        #endregion


    }//Class_end



}
