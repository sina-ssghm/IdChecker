using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.ViewModel.UploadFile
{
  
    public class ElementData
    {
        public string Application_ID { get; set; }
        public string Element_Key { get; set; }
        public string Element_Title { get; set; }
        public string Element_Col { get; set; }
        public string Element_Priority { get; set; }
        public string Element_Faicon { get; set; }
        public string Element_Group_Key { get; set; }
        public string Related_Element_Key { get; set; }
    }

    public class Process
    {
        public string Element_Key { get; set; }
        public string File_Name { get; set; }
        public string File_Type { get; set; }
        public string ID_Document_Code { get; set; }
        public string ID_Document_Text { get; set; }
        public string OCR_Code { get; set; }
        public string OCR_Text { get; set; }
        public string OCR_Egine_Code { get; set; }
        public string OCR_Egine_Text { get; set; }
        public string Validation_Code { get; set; }
        public string Validation_Text { get; set; }
        public string CaptureType_Code { get; set; }
        public string CaptureType_Text { get; set; }
    }

    public class Result
    {
        public ElementData Element_Data { get; set; }
        public Process Process { get; set; }
    }

    public class Message
    {
        public string Log_Transaction_ID { get; set; }
        public int Code { get; set; }
        public bool Error { get; set; }
        public string Description { get; set; }
    }

    public class UploadFileViewModel
    {
        public Result Result { get; set; }
        public Message Message { get; set; }
    }
}
