
using PackagingRelease_Test.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;
using Utils.Zip;

namespace PackagingRelease_Test
{
    public partial class MakeUpdatePackageForm : Form
    {
        #region   基础参数

        //需制作升级包的路径
        private string _needMakeUpdatePackagePath = null;
        //升级包文件名称
        private string _updatePackageFileName = null;
        //软件版本号
        private string _updatePackageVersion = "1.0.1";
        //更新包文件地址
        private string _updatePackageFileAddress = "http://localhost:22493/api/UpdateSoftwarePackage/GetUpdatePackageFile";
        //更新包配置地址
        private string _updatePackageConfigAddress = "http://localhost:22493/api/UpdateSoftwarePackage/GetUpdatePackageConfig";

        //升级包的配置文件名称
        private const string _updatePackageConfigFilename = "UpdateInfo.xml";

        #endregion 

        public MakeUpdatePackageForm()
        {
            InitializeComponent();

            //参数初始化
            InitPara();
        }


        #region   按钮事件
        //选择需制作升级包的路径按钮事件
        private void button_SelectNeedMakeUpdatePackeagePath_Click(object sender, EventArgs e)
        {
            textBox_UpdatePackagePath.Text = FolderHelper.GetSelectDirectory();
            textBox_UpdatePackageName.Text = Path.GetFileName(textBox_UpdatePackagePath.Text) ;

        }

        //制作按钮事件
        private void button_MakeUpdatePackage_Click(object sender, EventArgs e)
        {
            //禁用UI
            UIOPC(false);

            //显示加载动画
            //pictureBox1.Show();
            IsShowPictureBox(true);

            //开启线程压缩文件(升级包软件)
            StartThreadCompress();

        }

        //打开目录按钮事件
        private void button_OpenFloder_Click(object sender, EventArgs e)
        {

            FolderHelper.OpenDirectory(GetSaveUpdatePackagePath());
        }

    

        //关闭窗体事件
        private void MakeUpdatePackageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //清空所有资源
            Clear();
        }

        #endregion



        #region   私有方法
        /// <summary>
        /// 参数初始化
        /// </summary>
        private void InitPara()
        {
            //窗体居中
            this.StartPosition = FormStartPosition.CenterScreen;

            //添加基础值
            AddBaseValue();

            //显示程序信息
            ShowSoftwareInfo();

            //显示实时时间
            RealShowTime();

            //隐藏加载图标
            //pictureBox1.Hide();
            IsShowPictureBox(false);

            //注册压缩进度委托
            //zipHelper.compressProgress += CompressProgress;
            compressResult += CompressTaskResult;

            //注册保存配置文件的委托
            saveUpdatePackageConfig += SaveUpdatePackageConfigXMLFile;


        }

        /// <summary>
        /// 是否打开加载图
        /// </summary>
        /// <param name="isShow">是否打开（true：表示打开）</param>
        private void IsShowPictureBox(bool isShow)
        {
            textBox_MakeUpdatePackageAddress.Enabled = false;

            if (isShow)
            {
                pictureBox1.Show();

                label_MakeUpdatePackageAddress.Hide();
                textBox_MakeUpdatePackageAddress.Hide();
                button_OpenFloder.Hide();
            }
            else
            {
                pictureBox1.Hide();

                label_MakeUpdatePackageAddress.Show();
                textBox_MakeUpdatePackageAddress.Show();
                button_OpenFloder.Show();
            }
        }

        /// <summary>
        /// 添加基础值
        /// </summary>
        private void AddBaseValue()
        {
            textBox_SoftwareVersion.Text = _updatePackageVersion;
            textBox_UpdatePackageFileAddress.Text = _updatePackageFileAddress;
            textBox_UpdatePackageConfigAddress.Text = _updatePackageConfigAddress;
        }


        #region   显示程序信息
        /// <summary>
        /// 显示程序信息
        /// </summary>
        private void ShowSoftwareInfo()
        {
            string str = $"版本号:V1.0.0   作者:coffee   ";
            toolStripStatusLabel1.Text = str;
        }

        #endregion 

        #region   实时显示时间

        System.Timers.Timer timer = new System.Timers.Timer(100);

        /// <summary>
        /// 显示实时时间
        /// </summary>
        private void RealShowTime()
        {
            timer.Elapsed += new System.Timers.ElapsedEventHandler(ShowNowTime);
            //设置一直自动执行
            timer.AutoReset = true;
            //设置执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            timer.Start();
        }

        /// <summary>
        /// 显示当前时间
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void ShowNowTime(object o,System.Timers.ElapsedEventArgs e)
        {
           toolStripStatusLabel2.Text= DateTime.Now.ToString();
        }

        #endregion 

        #region   采用多线程压缩文件且控制组件

        /// <summary>
        /// 开启线程压缩文件
        /// </summary>
        private void StartThreadCompress()
        {
            using (var cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                ThreadPool.QueueUserWorkItem(o => CompressMethod(token));
            }
        }


        /// <summary>
        /// 压缩方法
        /// </summary>
        /// <param name="token"></param>
        private void CompressMethod(CancellationToken token)
        {
            bool result = Compress();
            compressResult(result);

            if (token.IsCancellationRequested)
            {
                return;
            }
        }

        //创建压缩结果委托
        private delegate void CompressResult(bool result);
        //申请压缩结果委托
        private CompressResult compressResult;

        //压缩结果处理方法
        private void CompressTaskResult(bool result)
        {
            //隐藏加载提示框
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new System.EventHandler(HidePictureBox), "加载中");

            }

            //保存升级包配置文件
            saveUpdatePackageConfig(result);
        }

        //多线程操作UI组件
        private void HidePictureBox(object o, System.EventArgs e)
        {
            //pictureBox1.Hide();
            IsShowPictureBox(false);
            //显示制作的更新包地址
            textBox_MakeUpdatePackageAddress.Text = GetSaveUpdatePackagePath();
            
        }

   

        #endregion

        #region   压缩文件

        //实例化压缩帮助类
        private ZipHelper zipHelper = new ZipHelper();


        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <returns>返回压缩结果（true：表示成功）</returns>
        private bool Compress()
        {
            string needMakeUpdatePackagePath = textBox_UpdatePackagePath.Text;
            string updatePackagePathAndFileName = GetUpdatePackagePathAndFileName();

            if (string.IsNullOrEmpty(needMakeUpdatePackagePath) || string.IsNullOrEmpty(updatePackagePathAndFileName)) return false;

            
            bool zipResult = zipHelper.CreatZip(needMakeUpdatePackagePath, updatePackagePathAndFileName, 
                System.IO.Compression.CompressionLevel.Optimal);

            
            return zipResult;
  
        }

        #endregion

        #region   保存升级包的配置文件

        //定义一个升级包配置文件的委托
        private delegate void  SaveUpdatePackageConfigFile(bool result);
        //声明一个委托
        private SaveUpdatePackageConfigFile saveUpdatePackageConfig;

        /// <summary>
        /// 保存升级包配置文件的委托
        /// </summary>
        /// <param name="makeUpdatePackageResult">制作压缩包的结果</param>
        private void SaveUpdatePackageConfigXMLFile(bool makeUpdatePackageResult)
        {
            if (makeUpdatePackageResult)
            {
                //获取到配置文件和名称
                string _updatePackageXMLFilePathAndName = GetSaveUpdatePackagePath() + _updatePackageConfigFilename;

                //删除原有升级包配置文件
                bool deleConfigXMLFile = FileHelper.DeleteFile(_updatePackageXMLFilePathAndName);

                if (deleConfigXMLFile)
                {
                    _needMakeUpdatePackagePath = textBox_UpdatePackagePath.Text;
                    _updatePackageFileName = GetUpdatePackageName();
                    _updatePackageVersion = textBox_SoftwareVersion.Text;
                    _updatePackageFileAddress = textBox_UpdatePackageFileAddress.Text;
                    _updatePackageConfigAddress = textBox_UpdatePackageConfigAddress.Text;


                    //保存升级包信息文件
                    string fileHash = FileHelper.GetFileMD5(GetUpdatePackagePathAndFileName());
                    bool success = SaveUpdatePackageInfo(_updatePackageXMLFilePathAndName,
                        _updatePackageVersion, _updatePackageFileAddress, _updatePackageConfigAddress, _updatePackageFileName, fileHash);

                    if (success)
                    {
                        MessageBox.Show(this,"升级包制作完成", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this,"升级包制作失败！！！","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "升级包制作失败！！！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                
                MessageBox.Show(this, "升级包制作失败！！！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          
            //启用UI
            UIOPC(true);

            Control.CheckForIllegalCrossThreadCalls = true;
        }

        #endregion

        #region   UI是否可用


        /// <summary>
        /// UI操作
        /// </summary>
        /// <param name="isEnable">是否启用(true：表示启用)</param>
        private void UIOPC(bool isEnable)
        {
            //取消线程的检查
            Control.CheckForIllegalCrossThreadCalls = false;

            textBox_UpdatePackagePath.Enabled = isEnable;
            button_SelectNeedMakeUpdatePackeagePath.Enabled = isEnable;

            textBox_UpdatePackageName.Enabled = isEnable;
            textBox_SoftwareVersion.Enabled = isEnable;
            textBox_UpdatePackageFileAddress.Enabled = isEnable;
            textBox_UpdatePackageConfigAddress.Enabled = isEnable;

            label_MakeUpdatePackageAddress.Enabled = isEnable;
            textBox_MakeUpdatePackageAddress.Enabled = false;
            button_OpenFloder.Enabled = isEnable;

            button_MakeUpdatePackage.Enabled = isEnable;

          
        }


        #endregion 

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
        private bool SaveUpdatePackageInfo(string saveFilePathAndName,string version,string updatePackageFileAddress,
            string updatePackageConfigAddress,string fileName,string fileHash)
        {
            if (string.IsNullOrEmpty(saveFilePathAndName) || string.IsNullOrEmpty(version) || 
                string.IsNullOrEmpty(updatePackageFileAddress) || string.IsNullOrEmpty(updatePackageConfigAddress)||
                string.IsNullOrEmpty(fileName)|| string.IsNullOrEmpty(fileHash)) return false;
            
            UpdatePackageInfo serverUpdateInfo = new UpdatePackageInfo();
            serverUpdateInfo.Version = version;
            serverUpdateInfo.UpdatePackageFileAddress = updatePackageFileAddress;
            serverUpdateInfo.UpdatePackageConfigAddress = updatePackageConfigAddress;
            serverUpdateInfo.FileName = fileName;
            serverUpdateInfo.FileHash = fileHash;
            bool result = XmlHelper.ObjectToXml(serverUpdateInfo, saveFilePathAndName);

            return result;
        }


        /// <summary>
        /// 获取到升级包的路径和文件名称
        /// </summary>
        /// <returns>返回升级包的路径和文件名称</returns>
        private string GetUpdatePackagePathAndFileName()
        {
            string str = GetSaveUpdatePackagePath() + GetUpdatePackageName();
            return str;
        }

        /// <summary>
        /// 获取到保存升级包的路径
        /// </summary>
        /// <returns>返回保存升级包的路径</returns>
        private string GetSaveUpdatePackagePath()
        {
            //string strDatePath = AppDomain.CurrentDomain.BaseDirectory + "UpdatePackage\\"+
            //    DateTime.Now.ToString("yyyy-MM-dd")+"\\"+DateTime.Now.ToString("HH-mm-ss");

            string strDatePath = AppDomain.CurrentDomain.BaseDirectory + "ClientUpdatePackage\\" +
               DateTime.Now.ToString("yyyy-MM-dd")+"\\"+ textBox_UpdatePackageName.Text+"\\";

            bool result = FolderHelper.CreateDirectory(strDatePath);

            if (!result) return null;

            return strDatePath;

        }

        /// <summary>
        /// 获取到升级包的名称
        /// </summary>
        /// <returns>返回到升级包的名称</returns>
        private string GetUpdatePackageName()
        {
            return textBox_UpdatePackageName.Text+".zip";
        }


        /// <summary>
        /// 清空所有资源
        /// </summary>
        private void Clear()
        {
            //取消压缩进度委托
            //zipHelper.compressProgress -= CompressProgress;
            compressResult -= CompressTaskResult;

            //取消保存配置文件的委托
            saveUpdatePackageConfig -= SaveUpdatePackageConfigXMLFile;

            //取消实时日期的任务
            timer.Dispose();
        }


        #endregion

        
    }//Class_end
}
