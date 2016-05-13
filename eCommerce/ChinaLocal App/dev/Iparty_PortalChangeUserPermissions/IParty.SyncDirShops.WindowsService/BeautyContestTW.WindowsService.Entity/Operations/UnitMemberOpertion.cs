using BeautyContestTW.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class UnitMemberOpertion : BaseEntityOperation<UnitMember>
    {
        public UnitMemberOpertion(string dbStr) : base(dbStr) { }
    }

    public class UnitMember_HistoryOpertion : BaseEntityOperation<UnitMember_History>
    {
        public UnitMember_HistoryOpertion(string dbStr) : base(dbStr) { }
    }

    public class UnitMemberPreviousOpertion : BaseEntityOperation<UnitMemberPrevious>
    {
        public UnitMemberPreviousOpertion(string dbStr) : base(dbStr) { }
    }

    public class UnitMemberPrevious_HistoryOpertion : BaseEntityOperation<UnitMemberPrevious_History>
    {
        public UnitMemberPrevious_HistoryOpertion(string dbStr) : base(dbStr) { }
    }
}
