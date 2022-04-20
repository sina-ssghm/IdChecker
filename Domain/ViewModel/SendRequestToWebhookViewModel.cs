using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.ViewModel
{
    public class SendRequestToWebhookViewModel
    {
        public bool Succeed { get; set; }
        public string Transaction_ID { get; set; }
        public string Message { get; set; }

    }
}
