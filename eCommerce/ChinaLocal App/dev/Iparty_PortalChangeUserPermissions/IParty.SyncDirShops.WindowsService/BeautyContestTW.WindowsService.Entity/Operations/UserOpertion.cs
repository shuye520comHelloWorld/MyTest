using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Data;
using BeautyContestTW.Entity.Entities;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public  class UserOpertion:BaseEntityOperation<User>
    {
        public UserOpertion(string str) : base(str) { }

        
    }

    
}
