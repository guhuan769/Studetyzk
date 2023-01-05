using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;

namespace AutoUpdateClient_Test
{
    static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ClientCheck();
        }

         
        /// <summary>
        /// �ͻ��˼��
        /// </summary>
        private static void ClientCheck()
        {
            //1-���ͻ����Ƿ�����
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                return;
            }

            //��ʼ������
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AutoUpdateClientForm());


        }

       


    }//Class_end
}
