using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.ViewModel.CreateElement
{
    public class Result
    {
        public string Element_No_1 { get; set; }
        public string Element_No_2 { get; set; }
        public string Element_No_3 { get; set; }
        public string Element_No_4 { get; set; }
        public string Element_No_5 { get; set; }
        public string Element_No_6 { get; set; }
        public string Element_No_7 { get; set; }
        public string Element_No_8 { get; set; }
    }

    public class Message
    {
        public string Log_Transaction_ID { get; set; }
        public int Code { get; set; }
        public bool Error { get; set; }
        public string Description { get; set; }
    }

    public class CreateElementViewModel
    {
        public Result Result { get; set; }
        public Message Message { get; set; }
    }

}
