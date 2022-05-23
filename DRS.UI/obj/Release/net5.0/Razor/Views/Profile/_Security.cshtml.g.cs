#pragma checksum "C:\Desarrollo\drs\DRS.UI\Views\Profile\_Security.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "90319519c24af89a115546aeafbd919939774b76"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Profile__Security), @"mvc.1.0.view", @"/Views/Profile/_Security.cshtml")]
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
#line 1 "C:\Desarrollo\drs\DRS.UI\Views\_ViewImports.cshtml"
using DRS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Desarrollo\drs\DRS.UI\Views\_ViewImports.cshtml"
using DRS.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"90319519c24af89a115546aeafbd919939774b76", @"/Views/Profile/_Security.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c460dfe88cd1c0b162bdec9d0f09e45ccc8bc311", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Profile__Security : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DRS.Entities.SecurityProfile>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-12\">\r\n        <div class=\"alertModalSecurity\" style=\"width: -webkit-fill-available;\">\r\n            <div");
            BeginWriteAttribute("class", " class=\"", 181, "\"", 215, 1);
#nullable restore
#line 6 "C:\Desarrollo\drs\DRS.UI\Views\Profile\_Security.cshtml"
WriteAttributeValue("", 189, ViewBag.TypeAlertSecurity, 189, 26, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"alertHeaderNPSecurity\" role=\"alert\">\r\n                <text>");
#nullable restore
#line 7 "C:\Desarrollo\drs\DRS.UI\Views\Profile\_Security.cshtml"
                 Write(ViewBag.MessageSecurity);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</text>
                <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
        </div>
    </div>
</div>

<div class=""row"">
    <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
        <label class=""label-form-control"">* Contraseña actual:</label>
        ");
#nullable restore
#line 19 "C:\Desarrollo\drs\DRS.UI\Views\Profile\_Security.cshtml"
   Write(Html.PasswordFor(model => model.PasswordActual, new { @class = "form-control", placeholder = "Introduce tu contraseña actual", style = "", @maxlength="100" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"col-lg-6 col-md-6 col-sm-12 col-xs-12\">\r\n        <label class=\"label-form-control\">* Nueva contraseña:</label>\r\n        ");
#nullable restore
#line 23 "C:\Desarrollo\drs\DRS.UI\Views\Profile\_Security.cshtml"
   Write(Html.PasswordFor(model => model.Password, new { @class = "form-control", placeholder = "Introduce tu nueva contraseña", style = "", @maxlength="100" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"col-lg-6 col-md-6 col-sm-12 col-xs-12\">\r\n        <label class=\"label-form-control\">* Confirmar contraseña:</label>\r\n        ");
#nullable restore
#line 27 "C:\Desarrollo\drs\DRS.UI\Views\Profile\_Security.cshtml"
   Write(Html.PasswordFor(model => model.PasswordConfirm, new { @class = "form-control", placeholder = "Confirma tu nueva contraseña", style = "", @maxlength="100" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    ");
#nullable restore
#line 29 "C:\Desarrollo\drs\DRS.UI\Views\Profile\_Security.cshtml"
Write(Html.HiddenFor(a => a.UserId));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
</div>


<div class=""row"" style=""margin-top: 25px;"">
    <div class=""col-md-12 col-lg-12 col-sm-12 col-xs-12"">
        <div class=""pull-right"">
            <button type=""button"" class=""btn btn-success btn-sm"" id=""btnChange""><i class=""fa fa-check-circle""></i>  Cambiar contraseña</button>
        </div>
    </div>
</div>

<script>
    
    $(document).ready(function () {

      $(""#accordionProduct"").show();
      $(""#loading"").hide();

        $(""#btnChange"").click(function(){
            $(""#accordionProduct"").hide();
            $(""#loading"").show();

            var data = '");
#nullable restore
#line 52 "C:\Desarrollo\drs\DRS.UI\Views\Profile\_Security.cshtml"
                   Write(Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model) as String));

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n\r\n            var jsonParse = JSON.parse(data);\r\n\r\n            jsonParse.UserId = \'");
#nullable restore
#line 56 "C:\Desarrollo\drs\DRS.UI\Views\Profile\_Security.cshtml"
                           Write(Model.UserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n            jsonParse.PasswordActual = $(\"#PasswordActual\").val();\r\n            jsonParse.Password = $(\"#Password\").val();\r\n            jsonParse.PasswordConfirm = $(\"#PasswordConfirm\").val();\r\n\r\n            $.ajax({\r\n                url: \"");
#nullable restore
#line 62 "C:\Desarrollo\drs\DRS.UI\Views\Profile\_Security.cshtml"
                 Write(Url.Action("ChangePassword", "Profile"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", // Url
                data: {
                    profile: jsonParse
                },
                type: ""post""  // Verbo HTTP
            })
            // Se ejecuta si todo fue bien.
            .done(function (result) {
                if (result != null) {
                    $(""#accordionProduct"").show();
                    $(""#loading"").hide();
                    $(""#divSecurity"").html(result);
            }
            else {
                $(""#accordionProduct"").show();
                $(""#loading"").hide();
                var divHtlml = '<div class=""alert alert-danger"" id=""alertHeaderNP"" role=""alert""><text>Ocurrió un error al recibir la solicitud. Reintente de nuevo.</text><button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button></div>';
                $(""#alertModalSecurity"").html(divHtlml);
            }
            })
            // Se ejecuta si se produjo un error.
            .fail(function (xh");
            WriteLiteral(@"r, status, error) {
                $(""#accordionProduct"").show();
                $(""#loading"").hide();
                var divHtlml = '<div class=""alert alert-danger"" id=""alertHeaderNP"" role=""alert""><text>Ocurrió un error al recibir la solicitud. Reintente de nuevo.</text><button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button></div>';
                $(""#alertModalSecurity"").html(divHtlml);
            })


        });

    });

</script>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DRS.Entities.SecurityProfile> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591