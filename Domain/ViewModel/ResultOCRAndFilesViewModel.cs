using AnalyzeId.Domain.Model;
using System.Collections.Generic;

namespace AnalyzeId.Domain.ViewModel
{
    public class ResultOCRAndFilesViewModel
    {
        public List<OCRForApiViewModel>  OCRs{ get; set; }
        public List<OCRFileForApiViewModel>  OCRFiles{ get; set; }
    }
}
