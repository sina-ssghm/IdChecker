#pragma checksum "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c9bb0cba50bd77a38d40b1b0ade4644e121e7055"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AnalyzeId.Pages.Home.Views_Home_Result), @"mvc.1.0.view", @"/Views/Home/Result.cshtml")]
namespace AnalyzeId.Pages.Home
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\_ViewImports.cshtml"
using AnalyzeId;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\_ViewImports.cshtml"
using AnalyzeId.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\_ViewImports.cshtml"
using AnalyzeId.Shared.DTO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Html;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c9bb0cba50bd77a38d40b1b0ade4644e121e7055", @"/Views/Home/Result.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"399a268aa76e52fccaf38237e928ea2d2da2e695", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Result : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<OCRFileDTO>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GetOCRResult", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-none"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("sendData"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
  
    //var data = Model?.Data?.Result?.Data?.MRZ?.Classification;
    var shouldRequestData = Model.Succeed && !Model.IsShowResult;

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"row\">\r\n\r\n    <div class=\"card mb-3 mt-3 col-sm-4 p-0 m-auto\">\r\n\r\n        <div class=\"col-12 row\">\r\n\r\n");
#nullable restore
#line 12 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
             if (!string.IsNullOrEmpty(Model.UrlFront))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"col-6 m-auto\">\r\n                    <img class=\"card-img-top d-none\"");
            BeginWriteAttribute("src", " src=\"", 422, "\"", 487, 1);
#nullable restore
#line 14 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
WriteAttributeValue("", 428, Model.UrlFront.Remove(0,Model.UrlFront.IndexOf("Files")-1), 428, 59, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"50%\"");
            BeginWriteAttribute("alt", " alt=\"", 500, "\"", 506, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                </div>\r\n");
#nullable restore
#line 16 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 20 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
             if (!string.IsNullOrEmpty(Model.UrlBack))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"col-6 m-auto\">\r\n                    <img class=\"card-img-top  d-none\"");
            BeginWriteAttribute("src", " src=\"", 705, "\"", 768, 1);
#nullable restore
#line 22 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
WriteAttributeValue("", 711, Model.UrlBack.Remove(0,Model.UrlBack.IndexOf("Files")-1), 711, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"50%\"");
            BeginWriteAttribute("alt", " alt=\"", 781, "\"", 787, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                </div>\r\n");
#nullable restore
#line 24 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n\r\n\r\n        <div class=\"card-body\">\r\n");
#nullable restore
#line 31 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
             if (Model.Succeed == false)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"card bg-danger text-white\">\r\n                    <div class=\"card-body\">\r\n");
            WriteLiteral("                        <p class=\"card-text\">\r\n                            ");
#nullable restore
#line 37 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
                       Write(Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 41 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card-data\">\r\n");
#nullable restore
#line 44 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
                 if (shouldRequestData)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"text-center mb-2\">\r\n                        <div class=\"spinner-border text-danger text-center\"></div>\r\n                        <p>Proccessing...</p>\r\n                    </div>\r\n");
#nullable restore
#line 50 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n");
            WriteLiteral("        </div>\r\n\r\n    </div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c9bb0cba50bd77a38d40b1b0ade4644e121e705510076", async() => {
                WriteLiteral("\r\n        <input type=\"hidden\" class=\"d-none\" id=\"UrlFront\" name=\"UrlFront\"");
                BeginWriteAttribute("value", " value=\"", 2238, "\"", 2287, 1);
#nullable restore
#line 60 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
WriteAttributeValue("", 2246, Model.UrlFront!=null?Model.UrlFront:"", 2246, 41, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n        <input type=\"hidden\" class=\"d-none\" id=\"UrlBack\" name=\"UrlBack\"");
                BeginWriteAttribute("value", " value=\"", 2362, "\"", 2409, 1);
#nullable restore
#line 61 "C:\Users\Ahmad\source\repos\IdChecker\AnalyzeId\Views\Home\Result.cshtml"
WriteAttributeValue("", 2370, Model.UrlBack!=null?Model.UrlBack:"", 2370, 39, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n        $(\"#sendData\").submit()\r\n    </script>\r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<OCRFileDTO> Html { get; private set; }
    }
}
#pragma warning restore 1591
