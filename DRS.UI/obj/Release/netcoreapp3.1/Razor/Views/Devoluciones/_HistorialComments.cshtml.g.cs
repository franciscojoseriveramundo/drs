#pragma checksum "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a98d44185077e8c1e91fdf2d94b391b026f59a4e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Devoluciones__HistorialComments), @"mvc.1.0.view", @"/Views/Devoluciones/_HistorialComments.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a98d44185077e8c1e91fdf2d94b391b026f59a4e", @"/Views/Devoluciones/_HistorialComments.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c460dfe88cd1c0b162bdec9d0f09e45ccc8bc311", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Devoluciones__HistorialComments : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Comentarios>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<style>
    .HistorialDiv {
        margin: 2rem auto;
        border: 1px solid #aaa;
        height: 300px;
        width: 90%;
        max-width: 400px;
        background: #f1f2f3;
        overflow: auto;
        box-sizing: border-box;
        padding: 0 1rem;
    }

        /* Estilos para motores Webkit y blink (Chrome, Safari, Opera... )*/

        .HistorialDiv::-webkit-scrollbar {
            -webkit-appearance: none;
        }

            .HistorialDiv::-webkit-scrollbar:vertical {
                width: 10px;
            }

    #tblHistorial .sorting_disabled {
        display: none;
    }
</style>



");
#nullable restore
#line 33 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
 if (Model.Count() == 0)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
     if (!string.IsNullOrEmpty(ViewBag.MessageComments))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("         <div class=\"row\">\r\n            <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\">\r\n\r\n\r\n                <div class=\"alertModal\">\r\n                    <div");
            BeginWriteAttribute("class", " class=\"", 946, "\"", 983, 1);
#nullable restore
#line 42 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
WriteAttributeValue("", 954, ViewBag.MessageAlertComments, 954, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"alertHeader\" role=\"alert\">\r\n                        <text>");
#nullable restore
#line 43 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                         Write(ViewBag.MessageComments);

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
");
#nullable restore
#line 54 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <ul class=""list-unstyled mb-0 pr-3"" id=""boxscroll2"" tabindex=""1"" style=""height: 425px !IMPORTANT;"">
        <li class=""p-3"">
            <div class=""media-body"">
                <p class=""media-heading mb-0"">Sin comentarios previos.</p>
            </div>
        </li>

        <style>
            #crdHst {
                height: 365px !IMPORTANT;
            }

            #crdHst2 {
                height: 365px !IMPORTANT;
            }
        </style>
    </ul>
");
#nullable restore
#line 74 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <style>\r\n        #tblHistorial_wrapper {\r\n        overflow-x: hidden;\r\n        }\r\n    </style>\r\n");
#nullable restore
#line 83 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
     if (!string.IsNullOrEmpty(ViewBag.MessageComments))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("         <div class=\"row\">\r\n            <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\">\r\n\r\n\r\n                <div class=\"alertModal\">\r\n                    <div");
            BeginWriteAttribute("class", " class=\"", 2228, "\"", 2265, 1);
#nullable restore
#line 90 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
WriteAttributeValue("", 2236, ViewBag.MessageAlertComments, 2236, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"alertHeader\" role=\"alert\">\r\n                        <text>");
#nullable restore
#line 91 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                         Write(ViewBag.MessageComments);

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
");
#nullable restore
#line 102 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"table-responsive\">\r\n        <table class=\"table table-hover tblHistorialDiv\" id=\"tblHistorial\">\r\n            <thead>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 112 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            <div class=\"row\">\r\n                                <div class=\"col-md-12\">\r\n");
#nullable restore
#line 118 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                                     if (item.PersonName == "Sin asignar")
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <p class=\"media-heading mb-0\" style=\"font-weight: bold; color: #000\">Sin asignar</p>\r\n");
#nullable restore
#line 121 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <p class=\"media-heading mb-0\" style=\"font-weight: bold; color: #000\">");
#nullable restore
#line 124 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                                                                                                        Write(item.PersonName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 125 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </div>\r\n                            </div>\r\n                            <div class=\"row\">\r\n                                <div class=\"col-md-9\">\r\n                                    <small class=\"text-muted\">");
#nullable restore
#line 130 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                                                         Write(item.Descripcion);

#line default
#line hidden
#nullable disable
            WriteLiteral("</small>\r\n                                </div>\r\n                                <div class=\"col-md-3\">\r\n                                    <small class=\"pull-right text-muted\">");
#nullable restore
#line 133 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                                                                    Write(item.FechaCreaci??n.ToLocalTime().ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>");
#nullable restore
#line 133 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                                                                                                                                Write(string.Concat(item.FechaCreaci??n.ToLocalTime().ToString("HH:mm:ss"), ""));

#line default
#line hidden
#nullable disable
            WriteLiteral("</small>\r\n                                </div>\r\n                            </div>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 138 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n\r\n    </div>\r\n");
#nullable restore
#line 143 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n\r\n<script type=\"text/javascript\">\r\n    $(document).ready(function () {\r\n\r\n        $(\"#LoadingH\").hide();\r\n\r\n\r\n    });\r\n</script>\r\n\r\n");
#nullable restore
#line 159 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
 if (Model.Count() == 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        $(\"#boxscroll2\").css(\"height\", \"60px\");\r\n        $(\".media-body\").css(\"font-weight\", \"500\");\r\n        $(\".media-body\").css(\"text-align\", \"center\");\r\n    </script>\r\n");
#nullable restore
#line 166 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <script>

        $(document).ready(function () {

            var tableHst = $('#tblHistorial').DataTable({
                ""dom"": ""<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'f>>"" +
                    ""<'row'<'col-sm-12'tr>>"" +
                    ""<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"",
                ""language"": {
                    ""lengthMenu"": ""Mostrando _MENU_ registros por p??gina"",
                    ""sInfo"": ""Mostrando de _START_ a _END_  de _TOTAL_ registros"",
                    ""sInfoEmpty"": """",
                    ""zeroRecords"": ""Sin resultados."",
                    ""info"": ""_PAGE_ de _PAGES_"",
                    ""bLengthChange"": false,
                    ""sSearch"": ""Buscar: "",
                    ""sLengthMenu"": ""Mostrar _MENU_ resultados por p??gina."",
                    ""sinfoEmpty"": ""Sin resultados."",
                    ""sZeroRecords"": ""Sin resultados."",
                    ""sInfoFiltered"": ""(Filtrado de _MAX_ registros totales)"",
        ");
            WriteLiteral(@"            ""paginate"": {
                        ""previous"": ""Anterior"",
                        ""next"": ""Siguiente""
                    }
                },
                ""initComplete"": function (settings, json) {
                    $('.dataTables_filter input').on('keypress', function (e) {
                        Tooltip();
                        $("".sorting_disabled"").css(""display"", ""none"");
                    });
                },
                // Setup for responsive datatables helper.
                bAutoWidth: false,
                fixedHeader: false,
                orderable: false,
                responsive: true,
                searching: false,
                ""info"": false,
                ""ordering"": false,
                ""lengthChange"": false,
                ""pageLength"": 5,
                'columns': [
                    { ""width"": ""100%"", ""targets"": 0, 'orderable': false, 'searchable': true},
                ],

            });
        });

      ");
            WriteLiteral("  \r\n    </script>\r\n");
#nullable restore
#line 219 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Devoluciones\_HistorialComments.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Comentarios>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
