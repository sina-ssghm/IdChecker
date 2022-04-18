using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.Model
{
    public class Base
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
