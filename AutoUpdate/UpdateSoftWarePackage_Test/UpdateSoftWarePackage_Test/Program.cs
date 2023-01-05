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
        #region   ��������
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

            //ֻ��������һ������
            mutex = new System.Threading.Mutex(true, "OnlyOneRun");
            if (mutex.WaitOne(0, false))
            {

                Application.Run(new SoftwareUpdateForm());
            }
            else
            {
                MessageBox.Show("�����Ѿ�������,��������������򣡣���", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }

        }
    }
}
