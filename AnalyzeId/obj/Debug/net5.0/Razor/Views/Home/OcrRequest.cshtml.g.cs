#pragma checksum "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8a5905ba3b9caf9dfe54ffb38b6d89404343c266"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AnalyzeId.Pages.Home.Views_Home_OcrRequest), @"mvc.1.0.view", @"/Views/Home/OcrRequest.cshtml")]
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
#line 1 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\_ViewImports.cshtml"
using AnalyzeId;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\_ViewImports.cshtml"
using AnalyzeId.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\_ViewImports.cshtml"
using AnalyzeId.Shared.DTO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Html;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a5905ba3b9caf9dfe54ffb38b6d89404343c266", @"/Views/Home/OcrRequest.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"399a268aa76e52fccaf38237e928ea2d2da2e695", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_OcrRequest : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<OCRFileDTO>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GetOCRResult", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-none"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("sendData"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Result", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("resultForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CanvasResult", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("CanvasForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "/script/script.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
  
    ViewData["Title"] = "OcrRequest";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""col text-center"" id=""scanMenu"">
    <div class=""col-12 col-md-4 m-auto text-center"">
        <img class=""card-img-top"" src=""/image/header.jpeg"" alt=""Card image cap""  style=""max-width:400px"">
        <h2 class=""mb-2 text-bold-custom "">UPLOAD DRIVER LICENCE</h2>
        <img class=""card-img-top"" src=""/image/page2.jpeg"" alt=""Card image cap""  style=""max-width:400px"">
        <h2 class=""mt-3 text-bold-custom text-decoration-underline"">INSTRUCTIONS</h2>
        <div class=""p-2"">
            <h3 class=""text-left"">- Take the picture in a well-lit place</h3>
            <h3 class=""mt-2   text-left"">- Ensure there are no reflections or shadows on the licence</h3>
            <h3 class=""mt-2  text-left"">- Maximise the licence size within the guiding rectangle</h3>
            <h3 class=""mt-2  text-left"">- The background should be a light colour and monochrome</h3>
            <h3 class=""mt-2   text-left"">- Ensure your hand isn`t obscuring any part of the licence</h3>
        </div>
    </div>");
            WriteLiteral("\n    <div class=\"col-sm-4 col-8 text-center m-auto\">\r\n        <a href=\"##\"  class=\"btn mt-1 font-size-1 bg-site p-2 waves-effect camera camera-file text-bold text-light w-100\">CAPTURE ");
#nullable restore
#line 21 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
                                                                                                                              Write(Model.UrlFront==null?"FRONT":"BACK");

#line default
#line hidden
#nullable disable
            WriteLiteral(" </a>\r\n        <a href=\"##\" class=\"btn mt-1 font-size-1 bg-site p-2 waves-effect CanvasCamera camera-can text-bold text-light w-100\">CAPTURE ");
#nullable restore
#line 22 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
                                                                                                                                  Write(Model.UrlFront==null?"FRONT":"BACK");

#line default
#line hidden
#nullable disable
            WriteLiteral(" </a>\r\n        <button type=\"button\" onclick=\"submitForm(true)\"");
            BeginWriteAttribute("class", " class=\"", 1553, "\"", 1663, 9);
            WriteAttributeValue("", 1561, "btn", 1561, 3, true);
#nullable restore
#line 23 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
WriteAttributeValue(" ", 1564, Model.UrlFront==null?"d-none":"", 1565, 35, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 1600, "bg-secondary", 1601, 13, true);
            WriteAttributeValue(" ", 1613, "camera-file", 1614, 12, true);
            WriteAttributeValue(" ", 1625, "font-size-1", 1626, 12, true);
            WriteAttributeValue(" ", 1637, "p-2", 1638, 4, true);
            WriteAttributeValue(" ", 1641, "mt-2", 1642, 5, true);
            WriteAttributeValue(" ", 1646, "text-light", 1647, 11, true);
            WriteAttributeValue(" ", 1657, "w-100", 1658, 6, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n            Skip\r\n        </button>\r\n        <button type=\"button\" onclick=\"submitForm(true)\"");
            BeginWriteAttribute("class", " class=\"", 1760, "\"", 1869, 9);
            WriteAttributeValue("", 1768, "btn", 1768, 3, true);
#nullable restore
#line 26 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
WriteAttributeValue(" ", 1771, Model.UrlFront==null?"d-none":"", 1772, 35, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 1807, "bg-secondary", 1808, 13, true);
            WriteAttributeValue(" ", 1820, "camera-can", 1821, 11, true);
            WriteAttributeValue(" ", 1831, "font-size-1", 1832, 12, true);
            WriteAttributeValue(" ", 1843, "p-2", 1844, 4, true);
            WriteAttributeValue(" ", 1847, "mt-2", 1848, 5, true);
            WriteAttributeValue(" ", 1852, "text-light", 1853, 11, true);
            WriteAttributeValue(" ", 1863, "w-100", 1864, 6, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n            Skip\r\n        </button>\r\n    </div>\r\n    <div class=\"col-12 col-md-6 m-auto text-center\">\r\n        <img class=\"card-img-top mt-3\" src=\"/image/footer.jpeg\" alt=\"Card image cap\">\r\n    </div>\r\n</div>\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a5905ba3b9caf9dfe54ffb38b6d89404343c26612106", async() => {
                WriteLiteral("\r\n    <input type=\"hidden\" class=\"d-none\" id=\"UrlFront\" name=\"UrlFront\"");
                BeginWriteAttribute("value", " value=\"", 2285, "\"", 2334, 1);
#nullable restore
#line 37 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
WriteAttributeValue("", 2293, Model.UrlFront!=null?Model.UrlFront:"", 2293, 41, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n    <input type=\"hidden\" class=\"d-none\" id=\"UrlBack\" name=\"UrlBack\"");
                BeginWriteAttribute("value", " value=\"", 2405, "\"", 2452, 1);
#nullable restore
#line 38 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
WriteAttributeValue("", 2413, Model.UrlBack!=null?Model.UrlBack:"", 2413, 39, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n");
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
            WriteLiteral("\r\n\r\n\r\n");
            WriteLiteral(@"


<div class=""cameraDiv text-center d-none"">
    <div class=""col-12 col-md-6 m-auto text-center"">
        <img class=""card-img-top"" src=""/image/header.jpeg"" style=""max-width:400px"" alt=""Card image cap"">
    </div>
    <button class=""btn bg-site btn-adn block waves-effect text-light"" onclick=""$('#File').click()"">Select File</button>
    <div class=""row d-none"" id=""camraPreview"">
        <div class="" col-sm-6 col-md-4 p-0 m-auto"">
            <h3 class=""mt-2 text-bold text-black"">");
#nullable restore
#line 83 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
                                              Write(Model.UrlFront==null?"FRONT IMAGE UPLOAD": "BACK IMAGE UPLOAD");

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n            <div");
            BeginWriteAttribute("class", " class=\"", 3864, "\"", 3872, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                <img class=\"m-auto card-img-top1  camera-preview-img\"");
            BeginWriteAttribute("src", " src=\"", 3945, "\"", 3951, 0);
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 3952, "\"", 3958, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n            </div>\r\n\r\n            <h2 class=\"mt-3 text-bold text-danger\">ENSURE TEXT IS CLEAR</h2>\r\n            <h3 class=\"mt-2 text-black text-center\">If you are happy with the image, please confirm.</h3>\r\n            <div class=\"card-body\">\r\n");
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a5905ba3b9caf9dfe54ffb38b6d89404343c26616851", async() => {
                WriteLiteral("\r\n                    <input type=\"hidden\" class=\"d-none\" id=\"UrlFront\" name=\"UrlFront\"");
                BeginWriteAttribute("value", " value=\"", 4604, "\"", 4653, 1);
#nullable restore
#line 96 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
WriteAttributeValue("", 4612, Model.UrlFront!=null?Model.UrlFront:"", 4612, 41, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n                    <input type=\"file\" id=\"FileFront\" name=\"FrontFile\"");
                BeginWriteAttribute("value", " value=\"", 4727, "\"", 4778, 1);
#nullable restore
#line 97 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
WriteAttributeValue("", 4735, Model.FrontFile!=null?Model.FrontFile:"", 4735, 43, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                    <input type=\"file\" class=\"d-none\" id=\"File\" name=\"File\" accept=\"image/*\" capture=\"camera\">\r\n");
                WriteLiteral("\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                <button class=""btn bg-site btn-adn block waves-effect text-light"" onclick=""$('#resultForm').submit()"">
                    Confirm
                </button>
                <button href=""#"" class=""btn bg-site text-light waves-effect retryCamera"">
                    Retry
                </button>

            </div>
        </div>
    </div>
");
            WriteLiteral(@"    <div class=""col-12 col-md-6 m-auto text-center"">
        <img class=""card-img-top mt-3"" src=""/image/footer.jpeg"" alt=""Card image cap"">
    </div>
    <div class=""canvascontainer d-none"">
        <video id=""videoplayer"" autoplay muted playsinline class=""d-none""></video>
        <div class=""position-relative camera-video-div"">
            <canvas id=""streamCanvas""");
            BeginWriteAttribute("class", " class=\"", 5947, "\"", 5955, 0);
            EndWriteAttribute();
            WriteLiteral("></canvas>\r\n            <button onclick=\"CaptureAndProccess()\" class=\"btn btn-danger btn-lg capture-btn rounded\">\r\n                Tap to\r\n                capture\r\n            </button>\r\n        </div>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a5905ba3b9caf9dfe54ffb38b6d89404343c26621295", async() => {
                WriteLiteral("\r\n            <input type=\"hidden\" class=\"d-none\" id=\"UrlFront\" name=\"UrlFront\"");
                BeginWriteAttribute("value", " value=\"", 6375, "\"", 6424, 1);
#nullable restore
#line 131 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
WriteAttributeValue("", 6383, Model.UrlFront!=null?Model.UrlFront:"", 6383, 41, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n            <input type=\"text\" id=\"File\" name=\"File\" />\r\n            <input type=\"file\" id=\"FileFront\" name=\"FrontFile\"");
                BeginWriteAttribute("value", " value=\"", 6549, "\"", 6600, 1);
#nullable restore
#line 133 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
WriteAttributeValue("", 6557, Model.FrontFile!=null?Model.FrontFile:"", 6557, 43, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    </div>
</div>
<div class=""spin d-none text-center"" style=""display:none"">
    <div class=""spinner-border text-danger""></div>
</div>

<div class=""row d-none error-alert"">
    <div class=""col-sm-6 m-auto"">
        <div class=""card bg-danger text-white"">
            <div class=""card-body"">
                <h4 class=""card-title text-white"">Error!</h4>
                <p class=""card-text error-message"">

                </p>
            </div>
        </div>
    </div>
</div>

");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a5905ba3b9caf9dfe54ffb38b6d89404343c26625066", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_10.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
#nullable restore
#line 156 "C:\Users\bralik 36287951\Desktop\GIC (1)\GIC\GIC\AnalyzeId\Views\Home\OcrRequest.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("\r\n");
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
