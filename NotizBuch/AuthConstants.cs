using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotizBuch
{
    // Sollte aus einer Datei / sicheren Umgebung geladen werden  
    public class AuthConstants
    {
        public const string Issuer = Audiance;
        public const string Audiance = "http://localhost:56943/";
        public const string Secret = "not_really_safe_bla_bla_bla_123";
    }
}
