using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity
{
    public class BaseAlteredEntity
    {
        public string AlteredBy { get; set; }
        public string AlteredType { get; set; }
        public DateTime AlteredWhen { get; set; }
    }
}
