#pragma checksum "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Profile\_Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "78669dfbbb05a4152282736f7a341933ce8f8698"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Profile__Index), @"mvc.1.0.view", @"/Views/Profile/_Index.cshtml")]
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
#line 1 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\_ViewImports.cshtml"
using DRS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\_ViewImports.cshtml"
using DRS.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"78669dfbbb05a4152282736f7a341933ce8f8698", @"/Views/Profile/_Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c460dfe88cd1c0b162bdec9d0f09e45ccc8bc311", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Profile__Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Profile\_Index.cshtml"
 using (Html.BeginForm(null, null, FormMethod.Post, new { id = "" }))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"row\">\r\n\r\n        <div class=\"alertModalConfirm\" style=\"width: -webkit-fill-available;\">\r\n            <div");
            BeginWriteAttribute("class", " class=\"", 195, "\"", 221, 1);
#nullable restore
#line 6 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Profile\_Index.cshtml"
WriteAttributeValue("", 203, ViewBag.TypeAlert, 203, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"alertHeaderNP\" role=\"alert\">\r\n                <text>");
#nullable restore
#line 7 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Profile\_Index.cshtml"
                 Write(ViewBag.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</text>\r\n                <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">\r\n                    <span aria-hidden=\"true\">&times;</span>\r\n                </button>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n");
            WriteLiteral(@"    <div class=""row"">

        <div id=""loading"" style=""width: 100%""></div>

        <div class=""accordion"" id=""accordionProduct"" style=""width: 100%; margin-bottom: 50px;"">
            <div class=""card"">
                <div class=""card-header-modal"" id=""headingOneProduct"">
                    <h5 class=""mb-0"">
                        <button class=""btn btn-link btn-link-modal-panel"" type=""button"">
                            Información general de la cuenta
                        </button>
                        <button class=""btn btn-link btn-link-modal-panel pull-right"" type=""button"">
                            <span class=""pull-right clickable""><i class=""fa fa-chevron-up""></i></span>
                        </button>
                    </h5>
                </div>
                <div id=""collapseOneProduct"" class=""show"" aria-labelledby=""headingOneProduct"" data-parent=""#accordionProduct"">
                    <div class=""card-body card-Body-OneProduct"">
                        <div id");
            WriteLiteral(@"=""divInformation"">

                        </div>
                    </div>
                </div>
            </div>
            <div class=""card"">
                <div class=""card-header-modal"" id=""headingTwoProduct"">
                    <h5 class=""mb-0"">
                        <button class=""btn btn-link btn-link-modal-panel"" type=""button"">
                            Información de contacto
                        </button>
                        <button class=""btn btn-link btn-link-modal-panel pull-right"" type=""button"">
                            <span class=""pull-right clickable""><i class=""fa fa-chevron-up""></i></span>
                        </button>
                    </h5>
                </div>
                <div id=""collapseTwoProduct"" class=""show"" aria-labelledby=""headingTwoProduct"" data-parent=""#accordionDevolucionesProduct"">
                    <div class=""card-body card-Body-TwoProduct"">
                         <div id=""divContact"">

                        </div>");
            WriteLiteral(@"
                    </div>
                </div>
            </div>
            <div class=""card"">
                <div class=""card-header-modal"" id=""headingThreeProduct"">
                    <h5 class=""mb-0"">
                        <button class=""btn btn-link btn-link-modal-panel"" type=""button"">
                            Seguridad de la cuenta
                        </button>
                        <button class=""btn btn-link btn-link-modal-panel pull-right"" type=""button"">
                            <span class=""pull-right clickable""><i class=""fa fa-chevron-up""></i></span>
                        </button>
                    </h5>
                </div>
                <div id=""collapseThreeProduct"" class=""show"" aria-labelledby=""headingThreeProduct"" data-parent=""#accordionDevolucionesProduct"">
                    <div class=""card-body card-Body-ThreeProduct"">
                         <div id=""divSecurity"">

                        </div>
                    </div>
              ");
            WriteLiteral("  </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 80 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Profile\_Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<script>

    $(""#accordionProduct"").hide();

    var divLoading = '<div class=""card""><div class=""form-group row"">'+
                        '<div class=""col-12"">'+
                        '<div align=""center"">' +
                        '<img src=""/images/loading-2.gif"" style=""width: 15%;"" class=""center"">' +
                        '<p style=""font-family: ""Rubik"", sans-serif; font-size: 14px;"" class=""center"">Espere por favor...</p>' +
                        '</div>' +
                        '</div>' +
                        '</div></div>';

    $(""#loading"").html(divLoading);
    $(""#loading"").show();

    $(document).ready(function () {

        $(""#headingOneProduct"").click(function () {

            var $this = $(this);

            if (!$(""#collapseOneProduct"").hasClass('hide')) {
                $('.card-Body-OneProduct').slideUp();
                $(""#collapseOneProduct"").removeClass('show');
                $(""#collapseOneProduct"").addClass('hide');
                $this");
            WriteLiteral(@".find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');

            } else {
                $('.card-Body-OneProduct').slideDown();
                $(""#collapseOneProduct"").removeClass('hide');
                $(""#collapseOneProduct"").addClass('show');
                $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');

            }

        });

        $(""#headingTwoProduct"").click(function () {

            var $this = $(this);

            if (!$(""#collapseTwoProduct"").hasClass('hide')) {
                $('.card-Body-TwoProduct').slideUp();
                $(""#collapseTwoProduct"").removeClass('show');
                $(""#collapseTwoProduct"").addClass('hide');
                $this.find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');

            } else {
                $('.card-Body-TwoProduct').slideDown();
                $(""#collapseTwoProduct"").removeClass('hide');
                $(""#collapseTwoProduct"").addClass('show');");
            WriteLiteral(@"
                $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');

            }

        });

        $(""#headingThreeProduct"").click(function () {

            var $this = $(this);

            if (!$(""#collapseThreeProduct"").hasClass('hide')) {
                $('.card-Body-ThreeProduct').slideUp();
                $(""#collapseThreeProduct"").removeClass('show');
                $(""#collapseThreeProduct"").addClass('hide');
                $this.find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');

            } else {
                $('.card-Body-ThreeProduct').slideDown();
                $(""#collapseThreeProduct"").removeClass('hide');
                $(""#collapseThreeProduct"").addClass('show');
                $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');

            }

        });

        ChargeInformation();

    });

    async function ChargeInformation() {

      await InformationGeneral();
      a");
            WriteLiteral("wait ContactInformation();\r\n      await SecurityInformation();\r\n\r\n     \r\n\r\n    }\r\n\r\n    async function InformationGeneral(){\r\n        \r\n        $.get(\'");
#nullable restore
#line 176 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Profile\_Index.cshtml"
          Write(Url.Action("InformationGeneral", "Profile"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', function (content)\r\n        {\r\n            $(\"#divInformation\").html(content);\r\n        });\r\n\r\n    }\r\n\r\n    async function ContactInformation(){\r\n        \r\n        $.get(\'");
#nullable restore
#line 185 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Profile\_Index.cshtml"
          Write(Url.Action("Contact", "Profile"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', function (content)\r\n        {\r\n            $(\"#divContact\").html(content);\r\n        });\r\n\r\n    }\r\n\r\n    async function SecurityInformation(){\r\n        \r\n        $.get(\'");
#nullable restore
#line 194 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Profile\_Index.cshtml"
          Write(Url.Action("Security", "Profile"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', function (content)\r\n        {\r\n            $(\"#divSecurity\").html(content);\r\n        });\r\n        \r\n    }\r\n    \r\n\r\n</script>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591