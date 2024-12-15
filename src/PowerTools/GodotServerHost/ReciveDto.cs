using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotServerHost
{
    public class ReciveDto
    {
        public string ClientId { get; set; }
        public RecivePackageType Type { get; set; }
        public string SubType { get; set; }
        public string SourceObject { get; set; }
        public string[] Args { get; set; }
    }
}
