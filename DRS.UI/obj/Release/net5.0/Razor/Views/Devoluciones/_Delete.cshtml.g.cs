#pragma checksum "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "15d79f1aa20ceee5279227ad8559072d18732562"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Devoluciones__Delete), @"mvc.1.0.view", @"/Views/Devoluciones/_Delete.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"15d79f1aa20ceee5279227ad8559072d18732562", @"/Views/Devoluciones/_Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c460dfe88cd1c0b162bdec9d0f09e45ccc8bc311", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Devoluciones__Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DRS.Entities.Devolucion>
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
<div class=""modal-header"">

    <h5 class=""modal-title alt-secondary-head"" id=""myModalLabel"">Cancelar devolución</h5>

    <button type=""button"" class=""close"" id=""btnClose"" data-dismiss=""modal"" aria-hidden=""true""><i class=""fa fa-close""></i></button>

</div>

");
#nullable restore
#line 11 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
 using (Html.BeginForm(null, null, FormMethod.Post, new { id = "" }))
{


#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"modal-body\">\r\n\r\n    <div class=\"alertModal\">\r\n        <div");
            BeginWriteAttribute("class", " class=\"", 447, "\"", 474, 1);
#nullable restore
#line 17 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
WriteAttributeValue("", 455, ViewBag.AlertModal, 455, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"alertHeader\" role=\"alert\">\r\n            <text>");
#nullable restore
#line 18 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
             Write(ViewBag.MessageModal);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</text>
            <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                <span aria-hidden=""true"">&times;</span>
            </button>
        </div>
    </div>

    <div id=""LoadingModal"">
        <div class=""col-md-12 grid-margin"">
            <div class=""card"">
                <div class=""card-body"">
                    <div align=""center"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "15d79f1aa20ceee5279227ad8559072d187325625861", async() => {
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

    <div class=""accordion"" id=""accordionDevoluciones"">
        <div class=""card"">
            <div class=""card-header-modal"" id=""headingOne"">
                <h5 class=""mb-0"">
                    <button class=""btn btn-link btn-link-modal-panel"" type=""button"">
                        Información general de la devolución
                    </button>
                    <button class=""btn btn-link btn-link-modal-panel pull-right"" type=""button"">
                        <span class=""pull-right clickable""><i class=""fa fa-chevron-up""></i></span>
                    </button>
                </h5>
            </div>

            <div id=""collapseOne"" class=""show"" aria-labelledby=""headingOne"" data-parent=""#accordionDevoluciones"">
                <div class=""card-body card-Body-One"">
");
            WriteLiteral(@"
                    <div class=""row"" style=""margin-top: -20px;"">
                        <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"">
                            <div class=""form-group"">
                                <label class=""label-form-control"">* Código del cliente</label>
                                ");
#nullable restore
#line 58 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                           Write(Html.DropDownListFor(model => model.Cliente, (List<SelectListItem>)ViewBag.ClientesM, htmlAttributes: new { @id = "cbxClientesm", @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </div>
                        </div>

                        <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"" id=""divCbxEmpleadoResponsable"">
                            <div class=""form-group"">
                                <label class=""label-form-control"">* Empleado responsable</label>
                                ");
#nullable restore
#line 65 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                           Write(Html.DropDownListFor(model => model.EmpleadoResponsable, (List<SelectListItem>)ViewBag.EmpleadosM, htmlAttributes: new { @id = "cbxEmpleadosm", @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </div>
                        </div>

                        <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"" id=""divCbxEmpleadoResponsable"">
                            <div class=""form-group"">
                                <label class=""label-form-control"">* Empleado responsable</label>
                                <input type=""text"" class=""form-control"" id=""txtEmpleadoResponsable"" />
                            </div>
                        </div>

                        <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"">
                            <div class=""form-group"">
                                <label class=""label-form-control"">* Enviado por</label>
                                ");
#nullable restore
#line 79 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                           Write(Html.TextBoxFor(model => model.Envio, new { @class = "form-control", placeholder = "Escribe la persona que lo envia", style = "", @maxlength = "100" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                            </div>
                        </div>

                        <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"">
                            <div class=""form-group"">
                                <label class=""label-form-control"">* Motivo del envío</label>
                                ");
#nullable restore
#line 87 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                           Write(Html.DropDownListFor(model => model.MotivoEnvio, (List<SelectListItem>)ViewBag.MotivoEnvioM, htmlAttributes: new { @id = "cbxMotivoEnviom", @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                            </div>
                        </div>

                        <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"" style=""margin-bottom: 5px;"" id=""divCbxCotizar"">
                            <div class=""form-group"">
                                <label class=""label-form-control"">* Cotizar reparación</label>
                                <div class=""input-group"">
                                    <label class=""content-input"" style=""position: relative;"">
                                        <input id=""cbxCotizacion"" type=""checkbox"" />
                                        <i></i>
                                    </label>
                                </div>
                            </div>
                        </div>

                        ");
#nullable restore
#line 104 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                   Write(Html.HiddenFor(model => model.Cotizar, new { @id = "cbxCotizarH" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                        ");
#nullable restore
#line 106 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                   Write(Html.HiddenFor(model => model.IsBtnContinueEnabled, new { @id = "btnContinueValue" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                        <div class=""col-lg-12 col-md-12 col-sm-12 col-xs-12"" id=""divDescripcion"">
                            <div class=""form-group"">
                                <label class=""label-form-control"">* Descripción de la falla</label>
                                ");
#nullable restore
#line 111 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                           Write(Html.TextAreaFor(model => model.Descripcion, new { @class = "form-control", placeholder = "Escribe tus descripción de la falla (255 carácteres)", style = "resize: none;", @rows = "5", @maxlength = "255" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </div>
                        </div>

                        <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"">
                            <div class=""form-group"">
                                <label class=""label-form-control"">* Número de guía</label>
                                ");
#nullable restore
#line 118 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                           Write(Html.TextBoxFor(model => model.NumeroGuia, new { @class = "form-control", placeholder = "Escribe el número de guía", style = "", @maxlength = "100" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </div>
                        </div>

                        <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"">
                            <div class=""form-group"">
                                <label class=""label-form-control"">* Nombre de mensajería</label>
                                ");
#nullable restore
#line 125 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                           Write(Html.TextBoxFor(model => model.NombreMensajeria, new { @class = "form-control", placeholder = "Escribe el nombre de la mensajería", style = "", @maxlength = "150" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </div>
                        </div>

                        <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"" id=""CCDiv"">
                            <div class=""form-group"">
                                <label class=""label-form-control"">* Destino CC</label>
                                ");
#nullable restore
#line 132 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                           Write(Html.TextBoxFor(model => model.DestinoCC, new { @class = "form-control", placeholder = "Escribe el destino CC", style = "", @maxlength = "150" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n\r\n");
            WriteLiteral(@"                    </div>
                </div>
            </div>
        </div>
        <div class=""card"" id=""DetailsReturn"">
            <div class=""card-header-modal"" id=""headingTwo"">
                <h5 class=""mb-0"">
                    <button class=""btn btn-link btn-link-modal-panel"" type=""button"">
                        Detalles
                    </button>
                    <button class=""btn btn-link btn-link-modal-panel pull-right"" type=""button"">
                        <span class=""pull-right clickable""><i class=""fa fa-chevron-up""></i></span>
                    </button>
                </h5>
            </div>
            <div id=""collapseTwo"" class=""show"" aria-labelledby=""headingTwo"" data-parent=""#accordionDevoluciones"">
                <div class=""card-body card-Body-Two"">
                    <div class=""card-body"">

                        <div class=""row"">
                            <div class=""col-md-12"">

                                <div id=""divProductDetai");
            WriteLiteral(@"ls"">

                                </div>

                                <div id=""tblDetails"">

                                    <div class=""row"" style=""margin-top: -20px;"">
                                        <div class=""alertModalDP"" style=""width: -webkit-fill-available;"">
                                            <div");
            BeginWriteAttribute("class", " class=\"", 9444, "\"", 9480, 1);
#nullable restore
#line 172 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
WriteAttributeValue("", 9452, ViewBag.TypeAlertDetailsNew, 9452, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"alertHeaderNP\" role=\"alert\">\r\n                                                <text>");
#nullable restore
#line 173 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
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
                                        <div class=""col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                                            <div class=""table-responsive"" style=""width:100%;"">
                                                <table id=""dtDetailsDevoluciones"" class=""table table-hover"">
                                                    <thead>
                                                        <tr class=""header-Dt"">
                                                            <th>Id</th>
                     ");
            WriteLiteral(@"                                       <th>Producto</th>
                                                            <th>Cantidad</th>
                                                            <th>Serie</th>
                                                            <th>¿es accesorio?</th>
                                                            <th>¿pertenece a Servitron?</th>
                                                           
                                                        </tr>
                                                    </thead>
                                                    <tbody>
");
#nullable restore
#line 197 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                         foreach (var item in Model.Details)
                                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                        <tr class=\"rows-Dt\">\r\n                                                            <td>");
#nullable restore
#line 200 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                           Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                            <td>\r\n                                                                ");
#nullable restore
#line 202 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                           Write(item.Producto);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                            </td>\r\n                                                            <td>");
#nullable restore
#line 204 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                           Write(item.Cantidad);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                            <td>\r\n");
#nullable restore
#line 206 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                                 if (string.IsNullOrEmpty(item.Serie))
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <i class=\"fa fa-close center\" style=\"color: red;\" aria-hidden=\"true\"></i>\r\n");
#nullable restore
#line 209 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                                }
                                                                else
                                                                {
                                                                   

#line default
#line hidden
#nullable disable
#nullable restore
#line 212 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                              Write(item.Serie);

#line default
#line hidden
#nullable disable
#nullable restore
#line 212 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                                              
                                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                            </td>\r\n                                                            <td>\r\n");
#nullable restore
#line 216 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                                 if (item.EsSoloDevolucion == true)
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                   <i class=\"fa fa-check center\" aria-hidden=\"true\"></i>\r\n");
#nullable restore
#line 219 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                                }
                                                                else
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                   <i class=\"fa fa-close center\" style=\"color: red;\" aria-hidden=\"true\"></i>\r\n");
#nullable restore
#line 223 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                            </td>\r\n                                                            <td>\r\n");
#nullable restore
#line 226 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                                 if (item.ExisteProducto == false)
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                   <i class=\"fa fa-close center\" style=\"color: red;\" aria-hidden=\"true\"></i>\r\n");
#nullable restore
#line 229 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                                }
                                                                else
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                   <i class=\"fa fa-check center\" aria-hidden=\"true\"></i>\r\n");
#nullable restore
#line 233 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                            </td>\r\n                                                            \r\n                                                        </tr>\r\n");
#nullable restore
#line 237 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                                                    </tbody>

                                                </table>

                                            </div>
                                        </div>

                                    </div>


                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</div>
");
            WriteLiteral(@"<div class=""modal-footer"">
    <div class=""row"" id=""footerBtns"">

        <div class=""col-md-12 col-lg-12 col-sm-12 col-xs-12"">
            <div class=""pull-right"">
                <button type=""button"" style=""margin-right: 5px !important;"" class=""btn btn-danger btn-sm"" id=""btnCloseModify"" data-dismiss=""modal""><i class=""fa fa-close""></i>  Cerrar</button>
");
#nullable restore
#line 268 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                 if (Model.IsBtnContinueEnabled == true)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <button type=\"button\" class=\"btn btn-success btn-sm cancel-reservation\" data-placement=\"top\" id=\"btnSave\"><i class=\"fa fa-check-circle\"></i>  Confirmar</button>\r\n");
#nullable restore
#line 271 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n\r\n");
#nullable restore
#line 275 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
         if(Model.IsBtnContinueEnabled == true){

#line default
#line hidden
#nullable disable
            WriteLiteral("            <!-- PopoverX content -->\r\n            <div id=\"confirmCancelReservation\">\r\n                \r\n            </div>\r\n");
#nullable restore
#line 280 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
#nullable restore
#line 284 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 286 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
 if (ViewBag.EnableCC == true)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<script>\r\n    $(document).ready(function () {\r\n        $(\"#CCDiv\").show();\r\n    });\r\n</script>\r\n");
#nullable restore
#line 293 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        $(document).ready(function () {\r\n            $(\"#CCDiv\").hide();\r\n        });\r\n    </script>\r\n");
#nullable restore
#line 301 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<script>\r\n\r\n    var cotizar = \'");
#nullable restore
#line 305 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
              Write(Model.Cotizar);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';

    $('#LoadingModal').hide();

    var idReservation = 0;

    $(""#cbxClientesm"").attr(""disabled"", true);
    $(""#Envio"").attr(""disabled"", true);
    $(""#cbxMotivoEnviom"").attr(""disabled"", true);
    $(""#Descripcion"").attr(""disabled"", true);
    $(""#cbxCotizarH"").attr(""disabled"", true);
    $(""#NumeroGuia"").attr(""disabled"", true);
    $(""#NombreMensajeria"").attr(""disabled"", true);
    $(""#DestinoCC"").attr(""disabled"", true);
    $(""#Comentarios"").attr(""disabled"", true);
    $(""#cbxCotizacion"").attr(""disabled"", true);

    var contentConfirmCancelReservation = '<div id=""cancelReservation"" class=""popover popover-x popover-default""><div class=""arrow""></div><h3 class=""popover-header popover-title""><span class=""close pull-right"" data-dismiss=""popover-x"">&times;</span>Confirmar</h3><div class=""popover-body popover-content"">¿Confirma la cancelación de la devolución?</div><div class=""popover-footer""><button type=""submit"" class=""btn btn-sm btn-danger"" onclick=""CancelReturn()"">Confirmar</button></d");
            WriteLiteral(@"iv></div>';


    $(document).ready(function () {

        $(""#divCbxEmpleadoResponsable"").hide();
        $(""#txtEmpleadoResponsable"").val($(""#cbxEmpleadosm option:selected"").text());
        $(""#txtEmpleadoResponsable"").attr(""disabled"", true);

        if ($(""#cbxEmpleadosm"").val() == ""0"") {
            $(""#txtEmpleadoResponsable"").val('Empleado no encontrado');
        }


        if (cotizar == 'True') {
            $(""#cbxCotizarH"").val('True');
            $(""#cbxCotizacion"").attr(""checked"", true);
            //$(""#User"").css(""display"", ""block"");
        }
        else {
            $(""#cbxCotizarH"").val('False');
        }

        $('#cbxMotivoEnviom').on('change', function () {

            if (this.value == 11) {
                $(""#divCbxCotizar"").show();
                $(""#divDescripcion"").show();

            }
            else {
                $(""#divCbxCotizar"").hide();
                $(""#divDescripcion"").hide();
                $(""#Descripcion"").val('');
 ");
            WriteLiteral(@"               $(""#cbxCotizarH"").val('False');
                $(""#cbxCotizacion"").prop('checked', false);
            }

        });

        if ($(""#cbxMotivoEnviom"").val() == 11) {
            $(""#divCbxCotizar"").show();
            $(""#divDescripcion"").show();
        }
        else {
            $(""#divCbxCotizar"").hide();
            $(""#divDescripcion"").hide();
            $(""#Descripcion"").val('');
            $(""#cbxCotizarH"").val('False');
            $(""#cbxCotizacion"").prop(""checked"", false);
        }

        $(""#headingOne"").click(function () {

            var $this = $(this);

            if (!$(""#collapseOne"").hasClass('hide')) {
                $('.card-Body-One').slideUp();
                $(""#collapseOne"").removeClass('show');
                $(""#collapseOne"").addClass('hide');
                $this.find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');

            } else {
                $('.card-Body-One').slideDown();
                $(""#col");
            WriteLiteral(@"lapseOne"").removeClass('hide');
                $(""#collapseOne"").addClass('show');
                $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');

            }

        });

        $(""#headingTwo"").click(function () {

            var $this = $(this);

            if (!$(""#collapseTwo"").hasClass('hide')) {
                $('.card-Body-Two').slideUp();
                $(""#collapseTwo"").removeClass('show');
                $(""#collapseTwo"").addClass('hide');
                $this.find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');

            } else {
                $('.card-Body-Two').slideDown();
                $(""#collapseTwo"").removeClass('hide');
                $(""#collapseTwo"").addClass('show');
                $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');

            }

        });

        $(""#btnCloseModify"").click(function () {

            $(""#divLoadingReturns"").show();
            $(""#repl");
            WriteLiteral("acetarget\").hide();\r\n\r\n            $.get(\'");
#nullable restore
#line 419 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
              Write(Url.Action("IndexIni", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"', function (content)
            {
                $(""#replacetarget"").html(content);
            });
        });

        $(""#btnClose"").click(function () {

            $(""#divLoadingReturns"").show();
            $(""#replacetarget"").hide();

            $.get('");
#nullable restore
#line 430 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
              Write(Url.Action("IndexIni", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', function (content)\r\n            {\r\n                $(\"#replacetarget\").html(content);\r\n            });\r\n        });\r\n\r\n        \r\n\r\n        $.get(\'");
#nullable restore
#line 438 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
          Write(Url.Action("DetailsNew", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"', function (content) {
            $(""#detailsDiv"").html(content);
        });

        $(""#DetailsReturn"").show();

        var tableDetailsDevoluciones = $('#dtDetailsDevoluciones').on('page.dt', function () { }).DataTable({
            //""dom"": ""<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'f>>"" +
            //    ""<'row'<'col-sm-12'tr>>"" +
            //    ""<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"",
            ""language"": {
                ""lengthMenu"": ""Mostrando _MENU_ registros por página"",
                ""sInfo"": ""Mostrando de _START_ a _END_  de _TOTAL_ registros"",
                ""sInfoEmpty"": """",
                ""zeroRecords"": ""No se encontraron resultados."",
                /*""info"": ""Página _PAGE_ de _PAGES_"",*/
                ""info"": ""_TOTAL_ registros dados de alta."",
                ""sSearch"": ""Buscar: "",
                ""sLengthMenu"": ""Mostrar _MENU_ resultados por página."",
                ""sinfoEmpty"": ""No se encontraron resultados."",
             ");
            WriteLiteral(@"   ""sZeroRecords"": ""No se encontraron resultados."",
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
                //$('.dataTables_filter input').on('keypress', function (e) {
                //    Tooltip();
                //});
            },
            'columns': [
                { ""width"": ""15%"", ""targets"": 0, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""width"": ""30%"", ""targets"": 1, 'orderable': false, 'searchable':");
            WriteLiteral(@" false, 'className': 'dt-body-center' },
                { ""width"": ""15%"", ""targets"": 2, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""width"": ""15%"", ""targets"": 3, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""width"": ""12.5%"", ""targets"": 4, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
                { ""width"": ""12.5%"", ""targets"": 5, 'orderable': false, 'searchable': false, 'className': 'dt-body-center' },
            ],
            'order': [0, 'asc']
        });

        $(""#confirmCancelReservation"").html(contentConfirmCancelReservation);

        var $targ = $('.cancel-reservation');
        $targ.popoverButton({ target: '#cancelReservation', placement: 'top' });

    });

    function CancelReturn(){

        idReservation = '");
#nullable restore
#line 499 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                    Write(Model.ClaveDevolucion);

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n\r\n        $(\"#accordionDevoluciones\").hide();\r\n\r\n        $(\"#LoadingModal\").show();\r\n\r\n        $(\"#btnSave\").hide();\r\n\r\n        $(\"#cancelReservation\").html(\'\');\r\n\r\n\r\n        $.post(\'");
#nullable restore
#line 510 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
           Write(Url.Action("CancelReturn", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\' + \"?ReturnId=\" + ");
#nullable restore
#line 510 "C:\Desarrollo\drs\DRS.UI\Views\Devoluciones\_Delete.cshtml"
                                                                         Write(Model.ClaveDevolucion);

#line default
#line hidden
#nullable disable
            WriteLiteral(", function (content)\r\n        {\r\n            $(\"#myModalContentXl\").html(content);\r\n        });\r\n    }\r\n\r\n</script>");
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