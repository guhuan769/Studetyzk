/***
*	Title："基础工具" 项目
*		主题：目录帮助类
*	Description：
*		功能：
*		    1、创建目录
*		    2、删除目录
*	Date：2021
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Utils;
using Utils.Zip;

namespace UpdateSoftWarePackage_Test
{
    public partial class SoftwareUpdateForm : Form
    {
        #region   基础参数

        #region   1-可配置参数
        //自动更新的方式
        private const UpdateType updateType1 = UpdateType.Auto;

        //启动的客户端名称
        private const string _needStartClientName = "AutoUpdateClient_Test";

        #endregion

        #region   2-自动更新程序参数
        //任务的取消令牌
        private CancellationTokenSource _cancellationTokenSource;

        //备份前次客户端的名称
        private const string _backupPreviousClientName = "backupPreviousClient";

        //升级包文件名称
        private const string _updatePackageFileName = "测试更新包.zip";
        //软件版本号
        private const string _softwareVersion = "1.0.1";
        //更新包文件地址
        private string _updatePackageFileAddress = "http://localhost:22493/api/UpdateSoftwarePackage/GetUpdatePackageFile";
        //更新包配置地址
        private string _updatePackageConfigAddress = "http://localhost:22493/api/UpdateSoftwarePackage/GetUpdatePackageConfig";

        //升级包的配置文件名称
        private const string _updatePackageConfigFilename = "UpdateInfo.xml";

        #endregion

        #endregion


        public SoftwareUpdateForm()
        {
            InitializeComponent();

            //参数初始化
            InitPara();

            //选择更新方式
            SelectUpdateStyle(updateType1);


        }

        private void SoftwareUpdateForm_Load(object sender, EventArgs e)
        {
            
        }



        #region   私有方法

        /// <summary>
        /// 参数初始化
        /// </summary>
        private void InitPara()
        {
            //窗体居中
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// 选择更新方式
        /// </summary>
        /// <param name="updateType">更新类型</param>
        private void SelectUpdateStyle(UpdateType updateType)
        {

            switch (updateType)
            {
                case UpdateType.Auto:
                    //启动任务
                    _cancellationTokenSource = StarTask(UpdateType.Auto);
                    break;

                case UpdateType.Manual:
                    //启动任务
                    _cancellationTokenSource = StarTask(UpdateType.Manual);
                    break;

                default:
                    //启动任务
                    _cancellationTokenSource = StarTask(UpdateType.Auto);
                    break;
            }
            
        }

        //创建任务执行
        private CancellationTokenSource StarTask(UpdateType updateType)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            //注册任务取消的事件（可以执行任务取消后的操作）
            cancellationTokenSource.Token.Register(() =>
            {
                //退出本更新程序
                Application.Exit();

                //启动客户端程序
                StartClientProgram();
            });

            Task task = new Task(()=> {

                //下载服务器的升级包配置文件
                DownloadServerUpdatePackageConfigFile();

                while (!cancellationTokenSource.IsCancellationRequested)
                {

                    switch (updateType)
                    {
                        case UpdateType.Auto:
                            //版本比较
                            bool result = VerifyUpdatePackageVersion();
                            if (result)
                            {
                                UpdateClientUpdatePackageConfig();
                                DownLoadUpdatePackageFile();

                            }
                            else
                            {
                                //取消任务
                                _cancellationTokenSource.Cancel();
                            }
                            
                            break;
                        case UpdateType.Manual:
                            UpdateClientUpdatePackageConfig();
                            //无视版本直接下载更新
                            DownLoadUpdatePackageFile();

                            break;
                        default:
                            break;
                    }

                  
                }
              
            });

            task.Start();
           
            return cancellationTokenSource;

        }


        //下载服务器的升级包配置文件
        private void DownloadServerUpdatePackageConfigFile()
        {
            FileDownload fileDownload = new FileDownload();
            fileDownload.error += FileDownloadError;
            //获取到本地升级包配置文件
            UpdatePackageInfo localUpdatePackageInfo = GetUpdatePackageConfig(GetClientUpdatePackageConfig());
            //从服务器上下载升级包配置文件
            fileDownload.HttpDownload(localUpdatePackageInfo.UpdatePackageConfigAddress,GetServerUpdatePackageConfig());

        }

        //校验版本
        private bool VerifyUpdatePackageVersion()
        {
            //获取到客户端升级包配置文件
            UpdatePackageInfo clientUpdatePackageInfo = GetUpdatePackageConfig(GetClientUpdatePackageConfig());

            //获取到服务端升级包配置文件
            UpdatePackageInfo serverUpdatePackageInfo = GetUpdatePackageConfig(GetServerUpdatePackageConfig());

            //只有服务端的版本大于客户端才升级
            if (clientUpdatePackageInfo == null || serverUpdatePackageInfo == null) return false;

            bool result = VersionCompare(clientUpdatePackageInfo.Version, serverUpdatePackageInfo.Version);

            return result;
           

        }

        //版本比较
        private bool VersionCompare(string clientVersion,string serverVersion)
        {
            bool result = false;

            if (string.IsNullOrEmpty(clientVersion) || string.IsNullOrEmpty(serverVersion)) return false;

            //从左往右比较
            try
            {
                string[] clientVersionArray = clientVersion.Split('.');
                string[] serverVersionArray = serverVersion.Split('.');

                int clientlen = clientVersionArray.Length;
                int serverLen = serverVersionArray.Length;

                int totalLen = 0;

                //比较长度
                if (serverLen > clientlen)
                {
                    totalLen = clientlen;
                }
                else
                {
                    totalLen = serverLen;
                }

                //服务器版本大则更新
                for (int i = 0; i < totalLen; i++)
                {
                    int clientVersionValue = 0;
                    int.TryParse(clientVersionArray[i], out clientVersionValue);

                    int serverVersionValue = 0;
                    int.TryParse(serverVersionArray[i], out serverVersionValue);

                    if (serverVersionValue>clientVersionValue)
                    {
                        return true;
                    }
                }

                //服务器的版本长度大于客户端的版本长度也更新
                if (serverLen > clientlen) return true;
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception(ex.Message);
            }

            return result;
          
        }

        //将客户端的升级包配置文件替换为服务器的配置文件文件
        private void UpdateClientUpdatePackageConfig()
        {
            //将客户端的升级包配置文件替换为服务器的配置文件文件
            FileHelper.CopyFile(GetServerUpdatePackageConfig(), GetClientUpdatePackageConfig(), true);
        }

        //下载升级包文件
        private void DownLoadUpdatePackageFile()
        {
            //下载升级包信息
            UpdatePackageInfo localUpdatePackageInfo = GetUpdatePackageConfig(GetClientUpdatePackageConfig());

            FileDownload fileDownload = new FileDownload();
            if (GetServerUpdatePackageFile(localUpdatePackageInfo.FileName) == null) return;

            fileDownload.downloadStatus += FileDownloadStatus;
            fileDownload.downloadEnd += FileDownloadEnd;
            fileDownload.error += FileDownloadError;
            fileDownload.HttpDownload(localUpdatePackageInfo.UpdatePackageFileAddress,
                GetServerUpdatePackageFile(localUpdatePackageInfo.FileName));

        }


        /// <summary>
        /// 获取升级包配置文件
        /// </summary>
        /// <param name="updatePackageConfigPathAndName">升级包配置文件的路径和名称</param>
        /// <returns></returns>
        private UpdatePackageInfo GetUpdatePackageConfig(string updatePackageConfigPathAndName)
        {
            bool result = FileHelper.IsExistFile(updatePackageConfigPathAndName);

            UpdatePackageInfo serverUpdateInfo = new UpdatePackageInfo();
            if (result)
            {
                serverUpdateInfo = XmlHelper.XmlFileToObject<UpdatePackageInfo>(updatePackageConfigPathAndName);
            }
            return serverUpdateInfo;
        }



        /// <summary>
        /// 保存升级包信息文件
        /// </summary>
        /// <param name="saveFilePathAndName">保存的文件路径和名称</param>
        /// <param name="version">升级包的版本</param>
        /// <param name="updatePackageFileAddress">升级包文件地址</param>
        /// <param name="updatePackageConfigAddress">升级包配置地址</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileHash">文件的MD5值</param>
        /// <returns>返回结果（true:表示成功）</returns>
        private bool SaveUpdatePackageInfo(string saveFilePathAndName, string version, string updatePackageFileAddress,
            string updatePackageConfigAddress, string fileName, string fileHash)
        {
            if (string.IsNullOrEmpty(saveFilePathAndName) || string.IsNullOrEmpty(version) ||
                string.IsNullOrEmpty(updatePackageFileAddress) || string.IsNullOrEmpty(updatePackageConfigAddress) ||
                string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(fileHash)) return false;

            UpdatePackageInfo serverUpdateInfo = new UpdatePackageInfo();
            serverUpdateInfo.Version = version;
            serverUpdateInfo.UpdatePackageFileAddress = updatePackageFileAddress;
            serverUpdateInfo.UpdatePackageConfigAddress = updatePackageConfigAddress;
            serverUpdateInfo.FileName = fileName;
            serverUpdateInfo.FileHash = fileHash;
            bool result = XmlHelper.ObjectToXmlFile(serverUpdateInfo, saveFilePathAndName);

            return result;
        }

        /// <summary>
        /// 获取到客户端升级包的配置文件
        /// </summary>
        /// <returns>返回本地升级包的配置文件</returns>
        private string GetClientUpdatePackageConfig()
        {
            string clientUpdatePackageConfigPathAndName = GetClientUpdatePackagePath() + _updatePackageConfigFilename;
            bool result = FileHelper.IsExistFile(clientUpdatePackageConfigPathAndName);

            //不存在则创建一个基础的升级包配置文件
            if (!result)
            {
                SaveUpdatePackageInfo(clientUpdatePackageConfigPathAndName, _softwareVersion,
                    _updatePackageFileAddress,_updatePackageConfigAddress,_updatePackageFileName,"123456789");
                
            }
            return clientUpdatePackageConfigPathAndName;
        }


        /// <summary>
        /// 获取到备份前次客户端的路径和名称
        /// </summary>
        /// <returns></returns>
        private string GetBackupPreviousClientPathAndName()
        {
            string backupPreviousClientPathAndName = GetClientUpdatePackagePath() + _backupPreviousClientName+".zip";
            return backupPreviousClientPathAndName;
        }

        /// <summary>
        /// 获取到客户端保存升级包的路径
        /// </summary>
        /// <returns>返回保存升级包的路径</returns>
        private string GetClientUpdatePackagePath()
        {
            string strDatePath = GetBasePath() + "UpdatePackage\\Client\\";

            bool result = FolderHelper.CreateDirectory(strDatePath);

            if (!result) return null;

            return strDatePath;

        }


        /// <summary>
        /// 获取到服务器升级包的配置文件
        /// </summary>
        /// <returns>返回本地升级包的配置文件</returns>
        private string GetServerUpdatePackageConfig()
        {
            string serverUpdatePackageConfigPathAndName = GetServerUpdatePackagePath() + _updatePackageConfigFilename;
            return serverUpdatePackageConfigPathAndName;
        }

        /// <summary>
        /// 获取到服务器升级包文件
        /// </summary>
        /// <param name="updatePacakageName">升级包的名称</param>
        /// <returns>返回服务器升级包的路径和文件名称</returns>
        private string GetServerUpdatePackageFile(string updatePacakageName)
        {
            if (string.IsNullOrEmpty(updatePacakageName)) return null;
            string serverUpdatePackageFilePathAndName = GetServerUpdatePackagePath() + updatePacakageName;
            return serverUpdatePackageFilePathAndName;
        }

        /// <summary>
        /// 获取到服务器保存升级包的路径
        /// </summary>
        /// <returns>返回保存升级包的路径</returns>
        private string GetServerUpdatePackagePath()
        {
            string strDatePath = GetBasePath() + "UpdatePackage\\Server\\";

            bool result = FolderHelper.CreateDirectory(strDatePath);

            if (!result) return null;

            return strDatePath;

        }

        /// <summary>
        /// 获取到服务器保存升级包的解压路径
        /// </summary>
        /// <returns>获取到服务器保存升级包的解压路径</returns>
        private string GetServerUpdatePackageDeCompressPath(string updatePackageFileName)
        {
            if (string.IsNullOrEmpty(updatePackageFileName)) return null;

            if (updatePackageFileName.Contains(".zip"))
            {
                string[] tmp = updatePackageFileName.Split(".zip");
                updatePackageFileName = tmp[0];
            }
            string path = GetServerUpdatePackagePath() + updatePackageFileName;

            return path;

        }

        /// <summary>
        /// 获取到本程序的基础路径
        /// </summary>
        /// <returns></returns>
        private string GetBasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

       

        #region   文件下载

        //进度条委托
        private delegate void _delProcessBar(ProgressBar progressBar,int minValue,int maxValue, int value);
        //下载的百分比委托
        private delegate void _delPercent(Label label,string percent);

        //文件下载状态
        private void FileDownloadStatus(int minValue,int maxValue, int progressRate, string downPercent)
        {
            progressBar_UpdateSoftware.Invoke(new _delProcessBar(ProgressBarMethod), progressBar_UpdateSoftware,minValue,maxValue, progressRate);
            label_Tip.Invoke(new _delPercent(LableMethod), label_Tip,downPercent);
        }

        //进度条方法
        private void ProgressBarMethod(ProgressBar progressBar,int minValue,int maxValue,int curValue)
        {
            progressBar.Minimum = minValue;
            progressBar.Maximum = maxValue;
            progressBar.Value = curValue;
        }

        //标签方法
        private void LableMethod(Label label,string percent)
        {
            label.Text = percent;
        }


        //文件下载结束
        private void FileDownloadEnd()
        {
            //校验文件的Hash值
            UpdatePackageInfo serverUpdatePackageInfo = GetUpdatePackageConfig(GetServerUpdatePackageConfig());
            bool result = VerifyUpdateHash(GetServerUpdatePackageFile(serverUpdatePackageInfo.FileName),serverUpdatePackageInfo.FileHash);

            if (result)
            {
                //备份当前程序
                BackUpClientSoftware();

                //解压程序进行更新
                DeCompression();

                //取消任务
                _cancellationTokenSource.Cancel();

            }
            else
            {
                UpdateFailMethod("更新包已被篡改");
            }

           
        }

        //校验更新包文件的Hash值
        private bool VerifyUpdateHash(string updatePackageFilePathAndName,string serverUpdatePackageHash)
        {
            if (string.IsNullOrEmpty(updatePackageFilePathAndName) || string.IsNullOrEmpty(serverUpdatePackageHash)) return false;

            string downloadUpdatePackageFileHash = FileHelper.GetFileMD5(updatePackageFilePathAndName);

            if (downloadUpdatePackageFileHash.Equals(serverUpdatePackageHash))
            {
                return true;
            }
            else
            {
                //删除下载好的所有更新包信息
                FolderHelper.DeleteDirectory(GetServerUpdatePackagePath());

                return false;
            }
        }

        //备份当前的客户端程序
        private void BackUpClientSoftware()
        {
            ZipHelper zipHelper = new ZipHelper();
            if (FileHelper.IsExistFile(GetBackupPreviousClientPathAndName()))
            {
                FileHelper.DeleteFile(GetBackupPreviousClientPathAndName());
            }

            //获取到客户端程序的路径
            //string clientFloderPath = FolderHelper.GetPreviousDirectory(GetBasePath(), 1);
            string clientFloderPath = GetBasePath();
            zipHelper.CreatZip(clientFloderPath, GetBackupPreviousClientPathAndName());
            string percent = "正在备份中，请稍等...";
            FileDownloadStatus(0, 100,0, percent);

        }

        //解压更新包
        private void DeCompression()
        {
            ZipHelper zipHelper = new ZipHelper();
            UpdatePackageInfo updatePackage = GetUpdatePackageConfig(GetClientUpdatePackageConfig());

            //解压更新包
            zipHelper.unZipProgress += DeCompressStatus;
            zipHelper.UnZip(GetServerUpdatePackageFile(updatePackage.FileName), GetServerUpdatePackagePath());

            //复制解压好的更新包目文件到客户端程序目录下
            //string clientFloderPath = FolderHelper.GetPreviousDirectory(GetBasePath(),1);
            string clientFloderPath = GetBasePath();
            FolderHelper.CopyDirectoryAllFile(GetServerUpdatePackageDeCompressPath(updatePackage.FileName), clientFloderPath);

            //删除下载好的所有更新包信息
            FolderHelper.DeleteDirectory(GetServerUpdatePackagePath());
            
          
        }

        //解压状态
        private void DeCompressStatus(object sender, UnZipProgressEventArgs e)
        {
            string percent ="正在更新中，请稍等...";

            FileDownloadStatus(0,e.Count,e.Index,percent);
        }

        //文件下载错误
        private void FileDownloadError(string str)
        {
            UpdateFailMethod(str);
        }

        //更新失败后的处理方法
        private void UpdateFailMethod(string str)
        {
            //取消任务
            _cancellationTokenSource.Cancel();

            DialogResult dt = MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (dt == DialogResult.OK)
            {
                ////取消任务
                //_cancellationTokenSource.Cancel();
            }
        }

        //更新类型
        internal enum UpdateType
        {
            Auto,       //自动
            Manual,     //手动
        }

        /// <summary>
        /// 启动客户端程序
        /// </summary>
        private void StartClientProgram()
        {
            //string clientPogram = FolderHelper.GetPreviousDirectory(GetBasePath(), 1)+_needStartClientName+".exe";
            string clientPogram = GetBasePath() + _needStartClientName + ".exe";
            //启动更新检查程序
            SoftwareOPCHelper.Start(clientPogram, _needStartClientName);
        }


        #endregion



        #endregion


    }//Class_end



}
