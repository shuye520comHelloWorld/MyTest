using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Interface
{
    public interface IThreeDES
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}
