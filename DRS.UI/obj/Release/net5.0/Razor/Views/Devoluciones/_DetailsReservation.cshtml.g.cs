#pragma checksum "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2e99a20a10b0e5489a0d143156a5f87094982090"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Devoluciones__DetailsReservation), @"mvc.1.0.view", @"/Views/Devoluciones/_DetailsReservation.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2e99a20a10b0e5489a0d143156a5f87094982090", @"/Views/Devoluciones/_DetailsReservation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c460dfe88cd1c0b162bdec9d0f09e45ccc8bc311", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Devoluciones__DetailsReservation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DetallesDevolucion>>
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
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-md-12"">

        <div id=""LoadingStock"" style=""display: none;"">
            <div class=""col-md-12 grid-margin"">
                <div class=""card"">
                    <div class=""card-body"">
                        <div align=""center"">
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "2e99a20a10b0e5489a0d143156a5f870949820904582", async() => {
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

        <div id=""divNewProduct"" style=""margin-top: -3%;"">
        </div>

        <div id=""divProductDetails"">

            <div class=""row"" style=""margin-top: -20px;"">
                <div class=""alertModalDP"" style=""width: -webkit-fill-available;"">
                    <div");
            BeginWriteAttribute("class", " class=\"", 981, "\"", 1017, 1);
#nullable restore
#line 26 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
WriteAttributeValue("", 989, ViewBag.TypeAlertDetailsNew, 989, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"alertHeaderNP\" role=\"alert\">\r\n                        <text>");
#nullable restore
#line 27 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                         Write(ViewBag.MessageDetailsNew);

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

            <div class=""row"">
                <div class=""col-md-12 col-lg-12 col-sm-12 col-xs-12"">
                    <div class=""pull-right"">
                        <button type=""button"" id=""NewProduct"" style=""float: right; height: 38px; margin-bottom: 20px;"" data-animation=""rubberBand"" class=""btn btn-success btn-rounded btn-sm ""><i class=""fa fa-plus-circle""></i> Añadir</button>
                    </div>
                </div>
            </div>

            <div class=""row"">
                <div class=""col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                    <div class=""table-responsive"" style=""width:100%;"">
                        <table id=""dtDetailsDevoluciones"" class=""table table-hover"">
           ");
            WriteLiteral(@"                 <thead>
                                <tr class=""header-Dt"">
                                    <th>Id</th>
                                    <th style=""display: none;"">IdDetails</th>
                                    <th>Producto</th>
                                    <th>Cantidad</th>
                                    <th>Serie</th>
                                    <th>¿es accesorio?</th>
                                    <th>¿pertenece a Servitron?</th>
                                    <th>Opciones</th>
                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 60 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr class=\"rows-Dt\">\r\n                                        <td>");
#nullable restore
#line 63 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                       Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td style=\"display: none;\">");
#nullable restore
#line 64 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                                              Write(item.IdDetallesDevolucion);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 66 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                       Write(item.Producto);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>");
#nullable restore
#line 68 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                       Write(item.Cantidad);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>\r\n");
#nullable restore
#line 70 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                             if (string.IsNullOrEmpty(item.Serie))
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <i class=\"fa fa-close center\" style=\"color: red;\" aria-hidden=\"true\"></i>\r\n");
#nullable restore
#line 73 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                            }
                                            else
                                            {
                                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                           Write(item.Serie);

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                                           
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        </td>\r\n                                        <td>\r\n");
#nullable restore
#line 80 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                             if (item.EsSoloDevolucion == true)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <i class=\"fa fa-check center\" aria-hidden=\"true\"></i>\r\n");
#nullable restore
#line 83 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                            }
                                            else
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <i class=\"fa fa-close center\" style=\"color: red;\" aria-hidden=\"true\"></i>\r\n");
#nullable restore
#line 87 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        </td>\r\n                                        <td>\r\n");
#nullable restore
#line 90 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                             if (item.ExisteProducto == false)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <i class=\"fa fa-close center\" style=\"color: red;\" aria-hidden=\"true\"></i>\r\n");
#nullable restore
#line 93 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                            }
                                            else
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <i class=\"fa fa-check center\" aria-hidden=\"true\"></i>\r\n");
#nullable restore
#line 97 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        </td>\r\n                                        <td>\r\n");
#nullable restore
#line 100 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                             if(item.StockConfirmation == null)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <button type=\"button\" id=\"Edit\" class=\"btn btn-outline-primary btn-rounded btn-sm\"");
            BeginWriteAttribute("onclick", " onclick=\"", 5515, "\"", 5548, 3);
            WriteAttributeValue("", 5525, "ModifyProduct(", 5525, 14, true);
#nullable restore
#line 102 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
WriteAttributeValue("", 5539, item.Id, 5539, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 5547, ")", 5547, 1, true);
            EndWriteAttribute();
            WriteLiteral(@" data-toggle=""tooltip"" data-placement=""top"" title=""Modificar el producto/accesorio"">
                                                    <i class=""fa fa-edit""></i>
                                                </button>
                                                <button type=""button"" class=""btn btn-outline-danger btn-rounded btn-sm""");
            BeginWriteAttribute("onclick", " onclick=\"", 5893, "\"", 5921, 3);
            WriteAttributeValue("", 5903, "GetValue(", 5903, 9, true);
#nullable restore
#line 105 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
WriteAttributeValue("", 5912, item.Id, 5912, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 5920, ")", 5920, 1, true);
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"popover\" id=\"showPopover\">\r\n                                                    <i class=\"fa fa-trash\"></i>\r\n                                                </button>\r\n");
#nullable restore
#line 108 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                            }
                                            else
                                            {
                                                if(item.StockConfirmation.StatusId == 0 || item.StockConfirmation.StatusId == 2 || item.StockConfirmation.StatusId == 3)
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <button type=\"button\" id=\"Edit\" class=\"btn btn-outline-primary btn-rounded btn-sm\"");
            BeginWriteAttribute("onclick", " onclick=\"", 6603, "\"", 6636, 3);
            WriteAttributeValue("", 6613, "ModifyProduct(", 6613, 14, true);
#nullable restore
#line 113 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
WriteAttributeValue("", 6627, item.Id, 6627, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 6635, ")", 6635, 1, true);
            EndWriteAttribute();
            WriteLiteral(@" data-toggle=""tooltip"" data-placement=""top"" title=""Modificar el producto/accesorio"">
                                                        <i class=""fa fa-edit""></i>
                                                    </button>
                                                    <div");
            BeginWriteAttribute("id", " id=\"", 6926, "\"", 6991, 1);
#nullable restore
#line 116 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
WriteAttributeValue("", 6931, String.Concat("divshowPopover", @item.IdDetallesDevolucion), 6931, 60, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                                        <button type=\"button\" class=\"btn btn-outline-danger btn-rounded btn-sm\"");
            BeginWriteAttribute("onclick", " onclick=\"", 7122, "\"", 7150, 3);
            WriteAttributeValue("", 7132, "GetValue(", 7132, 9, true);
#nullable restore
#line 117 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
WriteAttributeValue("", 7141, item.Id, 7141, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 7149, ")", 7149, 1, true);
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"popover\"");
            BeginWriteAttribute("id", " id=\"", 7173, "\"", 7234, 1);
#nullable restore
#line 117 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
WriteAttributeValue("", 7178, String.Concat("showPopover", item.IdDetallesDevolucion), 7178, 56, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\"fa fa-trash\"></i></button>\r\n                                                    </div>\r\n");
            WriteLiteral(@"                                                    <script>

                                                        var IdProductDel = 0;

                                                        var popover1;
                                                        
                                                        var divName = '");
#nullable restore
#line 126 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                                                  Write(String.Concat("divshowPopover", @item.IdDetallesDevolucion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n\r\n                                                        var nameBtn = \"showPopover\" + \'");
#nullable restore
#line 128 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                                                                  Write(item.IdDetallesDevolucion);

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n                                                        //$(\"#\" + divName).html(\'<button type=\"button\" class=\"btn btn-outline-danger btn-rounded btn-sm\" onclick=\"GetValue(");
#nullable restore
#line 129 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                                                                                                                                                      Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@")"" data-toggle=""popover"" id=""'+nameBtn+'""><i class=""fa fa-trash""></i></button>');

                                                        var createPopover = function (item, title) {
                       
                                                                var $pop = $(item);
        
                                                                $pop.popover({
                                                                    placement: 'right',
                                                                    title: ( title || '&nbsp;' ) + ' <a class=""close"" href=""#"">&times;</a>',
                                                                    trigger: 'click',
                                                                    container: $('body'), 
                                                                    html: true,
                                                                    content: function () {
                                                      ");
            WriteLiteral("                  return $(\'#popup-content\' + ");
#nullable restore
#line 142 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                                                                               Write(string.Concat(item.IdDetallesDevolucion));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" ).html();
                                                                    }
                                                                })
                                                                .on('shown.bs.popover', function(e) {
                                                                    //console.log('shown triggered');
                                                                    // 'aria-describedby' is the id of the current popover
                                                                    var current_popover = '#' + $(e.target).attr('aria-describedby');
                                                                    var $cur_pop = $(current_popover);
          
                                                                    $cur_pop.find('.close').click(function(){
                                                                        //console.log('close triggered');
                                                                        $p");
            WriteLiteral(@"op.popover('hide');
                                                                    });

                                                                    popover1 = $pop;
          
                                                                    $cur_pop.find('.OK').click(function(){
                                                                        //console.log('OK triggered');
                                                                        $pop.popover('hide');
                                                                        DeleteProduct();
                                                                    });
                                                                });

                                                                return $pop;
                                                            };

                                                          // create popover
                                                          createPopo");
            WriteLiteral(@"ver('#' + nameBtn, 'Eliminar el producto/accesorio');

                                                        function GetValue(Id) {
                                                            IdProductDel = Id;
                                                        }
                                                    </script>
");
            WriteLiteral("                                                    <div");
            BeginWriteAttribute("id", " id=\"", 11652, "\"", 11714, 1);
#nullable restore
#line 176 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
WriteAttributeValue("", 11657, string.Concat("popup-content",item.IdDetallesDevolucion), 11657, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""hide"">
                                                        <div class=""row"">
                                                            <div class=""col-12"">
                                                                <p>¿Confirma la eliminación de listado?</p>
                                                            </div>
                                                            <div class=""col-12"">
                                                                <a href=""#"" class=""btn btn-sm btn-danger pull-right OK"" role=""button"">Eliminar</a>
                                                            </div>
                                                        </div>
                                                    </div>
");
#nullable restore
#line 186 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                                }
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 189 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                             if(item.StockConfirmation.StatusId != 0)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <button type=\"button\" id=\"btnSee\" class=\"btn btn-outline-info btn-rounded btn-sm\"");
            BeginWriteAttribute("onclick", " onclick=\"", 12850, "\"", 12909, 3);
            WriteAttributeValue("", 12860, "CheckProductHistorial(", 12860, 22, true);
#nullable restore
#line 191 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
WriteAttributeValue("", 12882, item.IdDetallesDevolucion, 12882, 26, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 12908, ")", 12908, 1, true);
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Ver historial de confirmaciones o rechazos del producto\">\r\n                                                    <i class=\"fa fa-eye\"></i>\r\n                                                </button>\r\n");
#nullable restore
#line 194 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"

                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            \r\n                                           \r\n                                        </td>\r\n\r\n                                    </tr>\r\n");
#nullable restore
#line 201 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                            </tbody>\r\n\r\n                        </table>\r\n\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n");
            WriteLiteral(@"
            


        </div>
    </div>
</div>

<script type=""text/javascript"">

    

    //var contentDeleteProduct = '<div class=""popover-body popover-content"">¿Confirma la eliminación de listado?</div><div class=""popover-footer""><button type=""submit"" class=""btn btn-sm btn-danger"" onclick=""DeleteProduct()"">Eliminar</button></div>';

    

    $(document).ready(function () {
        $(""#DetailsReturn"").show();

        $("".hide"").hide();

        var tableDetailsDevoluciones = $('#dtDetailsDevoluciones').on('page.dt', function () { }).DataTable({
            //""dom"": ""<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'f>>"" +
            //    ""<'row'<'col-sm-12'tr>>"" +
            //    ""<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"",
            ""language"": {
                ""lengthMenu"": ""Mostrando _MENU_ registros por página"",
                ""sInfo"": ""Mostrando de _START_ a _END_  de _TOTAL_ registros"",
                ""sInfoEmpty"": """",
                ""zeroRecords""");
            WriteLiteral(@": ""No se encontraron resultados."",
                /*""info"": ""Página _PAGE_ de _PAGES_"",*/
                ""info"": ""_TOTAL_ registros dados de alta."",
                ""sSearch"": ""Buscar: "",
                ""sLengthMenu"": ""Mostrar _MENU_ resultados por página."",
                ""sinfoEmpty"": ""No se encontraron resultados."",
                ""sZeroRecords"": ""No se encontraron resultados."",
                ""sInfoFiltered"": ""(Filtrado de _MAX_ registros totales)"",
                ""paginate"": {
                    ""previous"": ""Anterior"",
                    ""next"": ""Siguiente""
                }
            },
            // Setup for responsive datatables helper.
            bAutoWidth: false,
            fixedHeader: ""true"",
            rowReorder: {
                selector: 'td:nth-child(2)'
            },
            responsive: true,
            orderable: false,
            ""searching"": false,
            ""initComplete"": function (settings, json) {
                //$('.dataTables_filte");
            WriteLiteral(@"r input').on('keypress', function (e) {
                //    Tooltip();
                //});
            },
            'columns': [
                { ""width"": ""5%"", ""targets"": 0, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""width"": ""0%"", ""targets"": 1, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""width"": ""25%"", ""targets"": 2, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""width"": ""15%"", ""targets"": 3, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""width"": ""15%"", ""targets"": 4, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""width"": ""12.5%"", ""targets"": 5, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""width"": ""12.5%"", ""targets"": 6, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""wi");
            WriteLiteral(@"dth"": ""15%"", ""targets"": 7, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' }
            ],
            'order': [0, 'asc']
        });

        $(""#divNewProduct"").hide();

        $(""#NewProduct"").click(function () {

            $(""#footerBtns"").hide();

            NewProduct();
        });

        //var contentDeleteProduct = '<div class=""arrow""></div><h3 class=""popover-header popover-title""><span class=""close pull-right"" data-dismiss=""popover-x"">&times;</span>Confirmar</h3><div class=""popover-body popover-content"">¿Confirma la eliminación de listado?</div><div class=""popover-footer""><button type=""submit"" class=""btn btn-sm btn-danger"" onclick=""DeleteProduct()"">Eliminar</button></div>';

        //$(""#confirmDeleteProduct"").html('');
        //$(""#confirmDeleteProduct"").html(contentDeleteProduct);

        //var $targ = $('.delete-product');
        //$targ.popoverButton({ target: '#confirmDeleteProduct', placement: 'top' });
        //$('#confirmDeleteProduct'");
            WriteLiteral(").popover(\'hide\');\r\n    });\r\n\r\n    function NewProduct() {\r\n        $(\"#LoadingStock\").show();\r\n        $(\"#divProductDetails\").hide();\r\n\r\n        $(\".popover\").hide();\r\n\r\n        $.get(\'");
#nullable restore
#line 310 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
          Write(Url.Action("AddProduct", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"', function (content)
        {
            $(""#divNewProduct"").html('');
            $(""#divNewProduct"").html(content);
            $(""#divNewProduct"").show();

        });
    }

    function CheckProductHistorial(Id){
        if(popover1 != null){
            popover1.popover('hide');
        }

        $("".popover"").hide();

        IdProductDel = Id;

        $.get('");
#nullable restore
#line 328 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
          Write(Url.Action("HistorialStockModify", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"' + ""?ProductId="" + Id + ""&&ReturnId="" + idReturn, function (content)
        {
            $(""#divNewProduct"").html('');
            $(""#divNewProduct"").html(content);
            $(""#divNewProduct"").show();
            $(""#divProductDetails"").hide();
            $(""#footerBtns"").hide();
        });
    }

    

    function DeleteProduct() {

        $(""#footerBtns"").hide();
        //$('#confirmDeleteProduct').popover('hide');
        $("".popover"").hide();

        $.post('");
#nullable restore
#line 346 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
           Write(Url.Action("RemoveProduct", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"' + ""?ProductId="" + IdProductDel, function (content)
        {
            $(""#divProductDetails"").html(content);
            $(""#divProductDetails"").show();
            $(""#footerBtns"").show();

        });
    }

    function ModifyProduct(Id) {

        $(""#LoadingStock"").show();
        $(""#divProductDetails"").hide();

        $("".popover"").hide();

        $.get('");
#nullable restore
#line 362 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_DetailsReservation.cshtml"
          Write(Url.Action("EditProduct", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"' + ""?ProductId="" + Id, function (content)
        {
            $(""#divNewProduct"").html('');
            $(""#divNewProduct"").html(content);
            $(""#divNewProduct"").show();
            $(""#divProductDetails"").hide();
            $(""#footerBtns"").hide();
        });

    }

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DetallesDevolucion>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
