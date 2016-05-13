using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinkBus.OfflineCustomer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process pCurrent = Process.GetCurrentProcess();
            Process[] pList = Process.GetProcessesByName(pCurrent.ProcessName);
            if (pList.Length >= 2)
            {
                MessageBox.Show("已有新顾客客户端在运行！");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
