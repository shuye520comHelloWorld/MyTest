using BeautyContestTW.Entity.Entities;
using BeautyContestTW.WindowsService.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class DirectorRelationshipsOperations : BaseEntityOperation<DirectorRelationships>
    {
        public DirectorRelationshipsOperations(string dbStr) : base(dbStr) { }        
    }
}
