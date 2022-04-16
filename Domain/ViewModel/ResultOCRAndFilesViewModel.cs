using AnalyzeId.Domain.Model;
using System.Collections.Generic;

namespace AnalyzeId.Domain.ViewModel
{
    public class ResultOCRAndFilesViewModel
    {
        public List<OCR>  OCRs{ get; set; }
        public List<OCRFileViewModel>  OCRFiles{ get; set; }
    }
}
