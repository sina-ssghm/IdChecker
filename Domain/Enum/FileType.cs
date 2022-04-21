using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.Enum
{
    public enum FileType:byte
    {
        [Display(Name = "ApiSignature_ID")]
        ApiSignature,
        [Display(Name = "Signature_ID")]
        Signature,
        [Display(Name = "Front_ID")]
        Front,
        [Display(Name = "Back_ID")]
        Back,
        [Display(Name = "Face_ID")]
        Face,


    }
}
