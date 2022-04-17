using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.ViewModel
{
    public class GetImageBase64ForApiViewModel
    {
        public bool Succeed { get; set; }
        public Byte[] Data { get; set; }
        public string Message { get; set; }

    }
}
