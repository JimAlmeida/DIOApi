using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIOApi.Data_Transfer_Objects
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
