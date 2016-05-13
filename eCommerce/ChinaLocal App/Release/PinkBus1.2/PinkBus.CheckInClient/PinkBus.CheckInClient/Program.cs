using PinkBus.CheckInClient.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinkBus.CheckInClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
           // if (Global.GlobalVariable.SingleInstance) //如果配置不允许多实例
           // {
                Process pCurrent = Process.GetCurrentProcess();
                Process[] pList = Process.GetProcessesByName(pCurrent.ProcessName);
                if (pList.Length >= 2)
                {
                    MessageBox.Show("已有签到客户端在运行！");
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new MainForm());
          //  }
        }
    }

}



