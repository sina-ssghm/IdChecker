using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.ViewModel
{
    public class BaseViewModel
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
