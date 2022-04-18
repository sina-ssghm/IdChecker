using AnalyzeId.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.Model
{
    public class OCRFile : Base
    {
        public string TransactionId { get; set; }
        public FileType  FileType{ get; set; }
        public string File { get; set; }
        public string UniqueId { get; set; }

    }
}
