using AnalyzeId.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.ViewModel
{
    public class OCRFileViewModel
    {
        public string Id { get; set; }

        public string TransactionId { get; set; }
        public FileType FileType { get; set; }
        public string File { get; set; }
        public string UniqueId { get; set; }

    }
}
