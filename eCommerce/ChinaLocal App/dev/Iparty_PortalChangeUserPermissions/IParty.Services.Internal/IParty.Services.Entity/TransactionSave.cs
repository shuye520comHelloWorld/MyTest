using IParty.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParty.Services.Entity
{
    public class TransactionSave<T> where T : new()
    {
        public T MainData { get; set; }
        public System.Data.IDbConnection Db { get; set; }
        public BaseRequestDto Dto { get; set; }
    }
}
