#pragma checksum "F:\itra\CourseProject\AppFour\AppFour\Views\Home\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "29091b714045aab09178e0e916dc59c4f11f2800"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Error), @"mvc.1.0.view", @"/Views/Home/Error.cshtml")]
namespace AspNetCore
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
#line 1 "F:\itra\CourseProject\AppFour\AppFour\Views\_ViewImports.cshtml"
using AppFour;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\itra\CourseProject\AppFour\AppFour\Views\_ViewImports.cshtml"
using AppFour.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "F:\itra\CourseProject\AppFour\AppFour\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "F:\itra\CourseProject\AppFour\AppFour\Views\_ViewImports.cshtml"
using CloudinaryDotNet;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "F:\itra\CourseProject\AppFour\AppFour\Views\_ViewImports.cshtml"
using CloudinaryDotNet.Actions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"29091b714045aab09178e0e916dc59c4f11f2800", @"/Views/Home/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"46f712dc799cf8f38e12baf8042732cc3fc1e341", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "F:\itra\CourseProject\AppFour\AppFour\Views\Home\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"text-danger\">Error.</h1>\r\n\r\n");
#nullable restore
#line 8 "F:\itra\CourseProject\AppFour\AppFour\Views\Home\Error.cshtml"
 if (Model.ShowRequestId)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        <strong>Request ID:</strong> <code>");
#nullable restore
#line 11 "F:\itra\CourseProject\AppFour\AppFour\Views\Home\Error.cshtml"
                                      Write(Model.RequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</code>\r\n    </p>\r\n");
#nullable restore
#line 13 "F:\itra\CourseProject\AppFour\AppFour\Views\Home\Error.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h3>It\'s a <b>Error</b>. Check stacktrace.</h3>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
