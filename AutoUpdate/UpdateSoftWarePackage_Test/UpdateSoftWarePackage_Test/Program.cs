using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateSoftWarePackage_Test
{
    static class Program
    {
        #region   基础参数
        private static System.Threading.Mutex mutex;

        #endregion 

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //只允许运行一个程序
            mutex = new System.Threading.Mutex(true, "OnlyOneRun");
            if (mutex.WaitOne(0, false))
            {

                Application.Run(new SoftwareUpdateForm());
            }
            else
            {
                MessageBox.Show("程序已经在运行,请勿启动多个程序！！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }

        }
    }
}
