using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;

namespace AutoUpdateClient_Test
{
    public partial class AutoUpdateClientForm : Form
    {


        public AutoUpdateClientForm()
        {

            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            string dirPath = FolderHelper.GetSelectDirectory();
            List<FileInfo> fileInfos = new List<FileInfo>();
            FolderHelper.GetAllFileHaveSubDir(dirPath,ref fileInfos);

            if (fileInfos!=null)
            {
                foreach (var item in fileInfos)
                {
                    textBox1.Text += item.FullName+"\r\n";
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            string dirPath = FolderHelper.GetSelectDirectory();

            FileInfo[] fileInfos = FolderHelper.GetAllFileNoSubDir(dirPath);

            if (fileInfos != null)
            {
                foreach (var item in fileInfos)
                {
                    textBox1.Text += item.FullName + "\r\n";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;

            textBox1.Text += GetBasePath()+"\r\n";

            string path = GetPreviousDirectory(GetBasePath(),1);

            textBox1.Text += path;
        }




        /// <summary>
        /// 获取到本程序的基础路径
        /// </summary>
        /// <returns></returns>
        private string GetBasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        private void AutoUpdateClentForm_Load(object sender, EventArgs e)
        {
            //button1.Hide();
            //button2.Hide();

        }


        /// <summary>
        /// 获取到上级目录
        /// </summary>
        /// <param name="curFloderPath">当前目录</param>
        /// <param name="rollbackFloderNumber">回滚的目录级数目</param>
        /// <returns></returns>
        private string GetPreviousDirectory(string curFloderPath,int rollbackFloderNumber)
        {
            if (string.IsNullOrEmpty(curFloderPath)|| rollbackFloderNumber<=0) return curFloderPath;

            string path = null;

            string[] paths = curFloderPath.Split("\\");

            int length = paths.Length-rollbackFloderNumber-1;
            for (int i = 0; i <length; i++)
            {
                path += paths[i]+"\\";
            }

            return path;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //1-启动更新程序
            string updateCheckSoftware = AppDomain.CurrentDomain.BaseDirectory + "UpdateSoftware\\UpdateSoftWarePackage_Test.exe";
            //启动更新检查程序
            SoftwareOPCHelper.Start(updateCheckSoftware, "Manual");

            //2-先关闭客户端自己的线程
            SoftwareOPCHelper.Dispose(SoftwareOPCHelper.GetThisProcess());
        }

        //开启手动更新
        private void ManualUpdate()
        {
            //1-启动更新程序
            string updateCheckSoftware = AppDomain.CurrentDomain.BaseDirectory + "UpdateSoftware\\UpdateSoftWarePackage_Test.exe";
            SoftwareOPCHelper.Start(updateCheckSoftware, "Manual");

            //2-先关闭客户端自己的线程
            SoftwareOPCHelper.Dispose(SoftwareOPCHelper.GetThisProcess());
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            ManualUpdate();
        }
    }//Class_end
}
