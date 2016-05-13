using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity
{
    public class TransactionSave<T> where T : new()
    {
        public T MainData { get; set; }
        public IDbConnection Db { get; set; }
    }
}
