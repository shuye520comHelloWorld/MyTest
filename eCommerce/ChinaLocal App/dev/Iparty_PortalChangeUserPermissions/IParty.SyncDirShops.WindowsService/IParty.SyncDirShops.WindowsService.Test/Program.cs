using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParty.SyncDirShops.WindowsService.Test
{
    public class Program
    {
        static void Main(string[] args)
        {

            try
            {
                SyncDirShops worker = new SyncDirShops();
                var _task = worker.SyncDirShopsTask().ContinueWith
                    (t => t.Exception != null);
                if (_task.Exception != null)
                {
                    Console.WriteLine(_task.Exception.ToString());
                }
                Console.Read();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
