#pragma checksum "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "51b66a05a4665a79eca18aaa9d638872a2001f5f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Devoluciones_Index), @"mvc.1.0.view", @"/Views/Devoluciones/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"51b66a05a4665a79eca18aaa9d638872a2001f5f", @"/Views/Devoluciones/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c460dfe88cd1c0b162bdec9d0f09e45ccc8bc311", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Devoluciones_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DRS.Entities.Devolucion>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/loading-2.gif"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height: 15%; width: 15%; text-align: center"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("center"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\Index.cshtml"
  
    ViewData["Title"] = "Devoluciones";
    Layout = "~/Views/Shared/_LayoutPage.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!-- Page-Title -->\r\n<div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n        <div class=\"page-title-box\">\r\n            <h4 class=\"page-title\">");
#nullable restore
#line 12 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\Index.cshtml"
                              Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
        </div>
    </div>
</div>

<div class=""row"">
    <div class=""col-md-12 col-lg-12 col-xl-12"">
        <div class=""card m-b-30"">
            <div class=""card-title"">
                <h6 class=""card-title-text"">Filtros</h6>
            </div>
            <div class=""card-body"">
                <div class=""row"" style=""margin-top: -25px;"">
                    <div class=""col-lg-4 col-md-6 col-xs-12 col-sm-12"">
                        <div class=""form-group"">
                            <label class=""label-form-control"">Código de cliente</label>
                            ");
#nullable restore
#line 28 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\Index.cshtml"
                       Write(Html.DropDownListFor(model => model.Cliente, (List<SelectListItem>)ViewBag.Clientes, htmlAttributes: new { @id = "cbxClientes", @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n");
            WriteLiteral("                    <div class=\"col-lg-4 col-md-6 col-xs-12 col-sm-12\">\r\n                        <div class=\"form-group\">\r\n                            <label class=\"label-form-control\">Motivo de envío</label>\r\n                            ");
#nullable restore
#line 42 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\Index.cshtml"
                       Write(Html.DropDownListFor(model => model.MotivoEnvio, (List<SelectListItem>)ViewBag.MotivoEnvio, htmlAttributes: new { @id = "cbxMotivoEnvio", @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </div>
                    </div>
                    <div class=""col-lg-4 col-md-6 col-xs-12 col-sm-12"">
                        <div class=""form-group"">
                            <label class=""label-form-control"">Estatus</label>
                            ");
#nullable restore
#line 48 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\Index.cshtml"
                       Write(Html.DropDownListFor(model => model.Estatus, (List<SelectListItem>)ViewBag.EstatusDevolucion, htmlAttributes: new { @id = "cbxEstatusDevolucion", @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n");
            WriteLiteral(@"                </div>
                <div class=""row"">
                    <div class=""col-md-12 col-lg-12 col-sm-12 col-xs-12"">
                        <div class=""pull-right"">
                            <button type=""button"" class=""btn btn-info btn-sm"" id=""btnSearch""><i class=""fa fa-search""></i>  Buscar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div id=""divLoadingReturns"" style=""display: block;"">

    <div class=""row"">
        <div style=""width: 100%"">
            <div class=""col-md-12 col-lg-12 col-xl-12"">
                <div class=""card"">
                    <div class=""card-body"">
                        <div align=""center"">
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "51b66a05a4665a79eca18aaa9d638872a2001f5f8123", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                            <p style=""font-family: 'Rubik', sans-serif; font-size: 14px;"" class=""center"">Cargando...</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    
</div>

<div id=""divReturnsExists"" style=""width: 100%;"">
    <div id=""replacetarget"">

    </div>
</div>


");
            WriteLiteral(@"<!-- modal Principal placeholder-->
<div id='myModalXl' class='modal fade'>
    <div class=""modal-dialog modal-xl"">
        <div class=""modal-content"">
            <div id='myModalContentXl'></div>
        </div>
    </div>
</div>

<div id='myModalLg' class='modal fade'>
    <div class=""modal-dialog modal-lg"">
        <div class=""modal-content"">
            <div id='myModalContentLg'></div>
        </div>
    </div>
</div>

<div id='myModalMd' class='modal fade'>
    <div class=""modal-dialog modal-md"">
        <div class=""modal-content"">
            <div id='myModalContentMd'></div>
        </div>
    </div>
</div>

<div id='myModalSm' class='modal fade'>
    <div class=""modal-dialog modal-sm"">
        <div class=""modal-content"">
            <div id='myModalContentSm'></div>
        </div>
    </div>
</div>

<script>

    $(document).ready(function () {

        $(function () {
            LimpiarModales();
            $.ajaxSetup({ cache: false });
            $(""a[dat");
            WriteLiteral(@"a-modal]"").on(""click"", function (e) {

                // hide dropdown if any (this is used wehen invoking modal from link in bootstrap dropdown )
                //$(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');
                $('#myModalContentLg').load(this.href, function () {
                    $('#myModalLg').modal({
                        backdrop: 'static',
                        keyboard: true
                    }, 'show');
                    bindFormLg(this);
                });
                return false;
            });
        });

        function LimpiarModales() {
            $(""#myModalContentMd"").html('');
            $(""#myModalContentLg"").html('');
            $(""#myModalContentSm"").html('');
            $(""#myModalContentXl"").html('');
        }

        $(""#divLoadingReturns"").show();
        $(""#replacetarget"").hide();

        $.get('");
#nullable restore
#line 166 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\Index.cshtml"
          Write(Url.Action("IndexIni", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', function (content)\r\n        {\r\n            $(\"#replacetarget\").html(content);\r\n        });\r\n\r\n        $(\"#btnSearch\").click(function () {\r\n\r\n            $(\"#divLoadingReturns\").show();\r\n            $(\"#replacetarget\").hide();\r\n\r\n            $.get(\'");
#nullable restore
#line 176 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\Index.cshtml"
              Write(Url.Action("IndexIni", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"' + ""?ClientId="" + $(""#cbxClientes"").val() + ""&ReasonSend="" + $(""#cbxMotivoEnvio"").val() + ""&Status="" + $(""#cbxEstatusDevolucion"").val(), function (content)
            {
                $(""#replacetarget"").html(content);
            });
            
        });

        $('#cbxClientes').select2({
            width: '29% !important',
            allowClear: false,
            height: '100%'
        });

    });

</script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DRS.Entities.Devolucion> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591