#pragma checksum "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "759216486d54b844c9a36859d8b708298540db16"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Devoluciones__New), @"mvc.1.0.view", @"/Views/Devoluciones/_New.cshtml")]
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
#line 1 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\_ViewImports.cshtml"
using DRS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\_ViewImports.cshtml"
using DRS.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"759216486d54b844c9a36859d8b708298540db16", @"/Views/Devoluciones/_New.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c460dfe88cd1c0b162bdec9d0f09e45ccc8bc311", @"/Views/_ViewImports.cshtml")]
    public class Views_Devoluciones__New : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DRS.Entities.Devolucion>
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
            WriteLiteral("\r\n<div class=\"modal-header\">\r\n\r\n    <h5 class=\"modal-title alt-secondary-head\" id=\"myModalLabel\">Nueva devolución</h5>\r\n\r\n    <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\"><i class=\"fa fa-close\"></i></button>\r\n\r\n</div>\r\n\r\n");
#nullable restore
#line 11 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
 using (Html.BeginForm(null, null, FormMethod.Post, new { id = "" }))
{


#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"modal-body\">\r\n\r\n        <div class=\"alertModal\">\r\n            <div");
            BeginWriteAttribute("class", " class=\"", 442, "\"", 469, 1);
#nullable restore
#line 17 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
WriteAttributeValue("", 450, ViewBag.AlertModal, 450, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"alertHeader\" role=\"alert\">\r\n                <text>");
#nullable restore
#line 18 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "759216486d54b844c9a36859d8b708298540db165877", async() => {
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

                <div id=""collapseOne"" class=""show"" aria-labelledby=""headingOne"" data-parent=""#acco");
            WriteLiteral(@"rdionDevoluciones"">
                    <div class=""card-body card-Body-One"">

                        <div class=""row"" style=""margin-top: -20px;"">
                            <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""label-form-control"">* Código del cliente</label>
                                    ");
#nullable restore
#line 58 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
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
#line 65 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
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
                                    <input type=""text"" class=""form-control"" id=""txtEmpleadoResponsable""/>
                                </div>
                            </div>

                            <div class=""col-lg-4 col-md-4 col-sm-6 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""label-form-control"">* Enviado por</label>
                                    ");
#nullable restore
#line 79 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                               Write(Html.TextBoxFor(model => model.Envio, new { @class = "form-control", placeholder = "Escribe la persona que lo envia", style = "" }));

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
#line 87 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
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
#line 104 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                       Write(Html.HiddenFor(model => model.Cotizar, new { @id = "cbxCotizarH" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                            ");
#nullable restore
#line 106 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
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
#line 111 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
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
#line 118 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                               Write(Html.TextBoxFor(model => model.NumeroGuia, new { @class = "form-control", placeholder = "Escribe el número de guía", style = "" }));

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
#line 125 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                               Write(Html.TextBoxFor(model => model.NombreMensajeria, new { @class = "form-control", placeholder = "Escribe el nombre de la mensajería", style = "" }));

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
#line 132 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                               Write(Html.TextBoxFor(model => model.DestinoCC, new { @class = "form-control", placeholder = "Escribe el destino CC", style = "" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                            </div>

                            <div class=""col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""label-form-control"">Comentarios</label>
                                    ");
#nullable restore
#line 139 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                               Write(Html.TextAreaFor(model => model.Comentarios, new { @class = "form-control", placeholder = "Escribe tus comentarios (255 carácteres)", style = "resize: none;", @rows = "5", @maxlength = "255" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n\r\n");
#nullable restore
#line 144 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                         if (Model.IsBtnContinueEnabled == true)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <div class=""row"">
                                <div class=""col-md-12 col-lg-12 col-sm-12 col-xs-12"">
                                    <div class=""pull-right"">
                                        <button type=""button"" class=""btn btn-success btn-sm"" id=""btnContinue"">Continuar <i class=""fa fa-arrow-circle-right""></i></button>
                                    </div>
                                </div>
                            </div>
");
#nullable restore
#line 153 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

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
");
#nullable restore
#line 172 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                         if (Model.IsBtnContinueEnabled == false)
                        {



#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div id=\"detailsDiv\"></div>\r\n");
#nullable restore
#line 177 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"



                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n\r\n    </div>\r\n");
            WriteLiteral(@"    <div class=""modal-footer"">
        <div class=""row"" id=""footerBtns"">

            <div class=""col-md-12 col-lg-12 col-sm-12 col-xs-12"">
                <div class=""pull-right"">
                    <button type=""button"" style=""margin-right: 5px !important;"" class=""btn btn-danger btn-sm"" id=""btnCloseModify"" data-dismiss=""modal""><i class=""fa fa-close""></i>  Cerrar</button>
");
#nullable restore
#line 195 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                     if (Model.IsBtnContinueEnabled == false)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <button type=\"button\" class=\"btn btn-success btn-sm\" id=\"btnSave\"><i class=\"fa fa-check-circle\"></i>  Confirmar</button>\r\n");
#nullable restore
#line 198 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 204 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 206 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
 if (Model.IsBtnContinueEnabled == false)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<script>
        
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

    $(document).ready(function () {

        $.get('");
#nullable restore
#line 223 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
          Write(Url.Action("DetailsNew", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', function (content) {\r\n            $(\"#detailsDiv\").html(content);\r\n        });\r\n\r\n        $(\"#DetailsReturn\").show();\r\n\r\n        $(\"#btnSave\").click(function () {\r\n            $.ajax({\r\n                url: \"");
#nullable restore
#line 231 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                 Write(Url.Action("Create", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", // Url
                //data: {
                //    devolucion: jsonParse
                //},
                type: ""post""  // Verbo HTTP
            })
            // Se ejecuta si todo fue bien.
                .done(function (result) {
                    if (result != null) {
                        $(""#myModalContentXl"").html(result);
                }
                else {
                }
            })
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {
                var divHtlml = '<div class=""alert alert-danger"" id=""alertHeader"" role=""alert""><text>Ocurrió un error al recibir la solicitud. Reintente de nuevo.</text><button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button></div>';
                $(""#alertModal"").html();
            })
        });
    });

</script>
");
#nullable restore
#line 254 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        $(\"#DetailsReturn\").hide();\r\n    </script>\r\n");
#nullable restore
#line 260 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 262 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
 if (ViewBag.EnableCC == true)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        $(document).ready(function () {\r\n            $(\"#CCDiv\").show();\r\n        });\r\n    </script>\r\n");
#nullable restore
#line 269 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        $(document).ready(function () {\r\n            $(\"#CCDiv\").hide();\r\n        });\r\n    </script>\r\n");
#nullable restore
#line 277 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<script>\r\n\r\n    var cotizar = \'");
#nullable restore
#line 281 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
              Write(Model.Cotizar);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';

    $('#LoadingModal').hide();

    $(document).ready(function () {
        //$(""#headingTwo"").attr(""disabled"", ""disabled"");
        //$(""#collapseTwo"").hide();

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

        $(""#cbxCotizacion"").change(function () {

            if ($('#cbxCotizacion').prop('checked')) {
                $(""#cbxCotizarH"").val('True');

            }
            else {
                $(""#cbxCotizarH"").val('False');
");
            WriteLiteral("\n            }\r\n\r\n        });\r\n\r\n        $(\"#btnContinue\").click(function (e) {\r\n\r\n            $(\"#accordionDevoluciones\").hide();\r\n            $(\"#LoadingModal\").show();\r\n\r\n            var data = \'");
#nullable restore
#line 325 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                   Write(Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model) as String));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';

            var jsonParse = JSON.parse(data);

            jsonParse.Cliente = $(""#cbxClientesm"").val();
            jsonParse.Envio = $(""#Envio"").val();
            jsonParse.MotivoEnvio = $(""#cbxMotivoEnviom"").val();
            jsonParse.Descripcion = $(""#Descripcion"").val();
            jsonParse.Cotizar = $(""#cbxCotizarH"").val();
            jsonParse.NumeroGuia = $(""#NumeroGuia"").val();
            jsonParse.NombreMensajeria = $(""#NombreMensajeria"").val();
            jsonParse.DestinoCC = $(""#DestinoCC"").val();
            jsonParse.Comentarios = $(""#Comentarios"").val();

            //if ($(""#Contrato_ContractosSeleccionados"").val() != null) {
            //    jsonParse.Contrato.ContractosSeleccionados = $(""#Contrato_ContractosSeleccionados"").val();
            //}

            $.ajax({
                url: """);
#nullable restore
#line 344 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
                 Write(Url.Action("New", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", // Url
                data: {
                    devolucion: jsonParse
                },
                type: ""post""  // Verbo HTTP
            })
            // Se ejecuta si todo fue bien.
                .done(function (result) {
                    if (result != null) {
                        $(""#myModalContentXl"").html(result);
                }
                else {
                }
            })
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {
                var divHtlml = '<div class=""alert alert-danger"" id=""alertHeader"" role=""alert""><text>Ocurrió un error al recibir la solicitud. Reintente de nuevo.</text><button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button></div>';
                $(""#alertModal"").html();
            })
            //// Hacer algo siempre, haya sido exitosa o no.
            //.always(function () {

            //});
        });");
            WriteLiteral(@"

        $('#cbxMotivoEnviom').on('change', function () {

            if (this.value == 11) {
                $(""#divCbxCotizar"").show();
                $(""#divDescripcion"").show();

            }
            else {
                $(""#divCbxCotizar"").hide();
                $(""#divDescripcion"").hide();
                $(""#Descripcion"").val('');
                $(""#cbxCotizarH"").val('False');
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

            if (!$(""#collapseOne"").h");
            WriteLiteral(@"asClass('hide')) {
                $('.card-Body-One').slideUp();
                $(""#collapseOne"").removeClass('show');
                $(""#collapseOne"").addClass('hide');
                $this.find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');

            } else {
                $('.card-Body-One').slideDown();
                $(""#collapseOne"").removeClass('hide');
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
                $('.card-Body-Two').");
            WriteLiteral(@"slideDown();
                $(""#collapseTwo"").removeClass('hide');
                $(""#collapseTwo"").addClass('show');
                $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');

            }

        });

        $('.selectMultiple').select2({
            placeholder: ""Seleccione uno o más contratos."",
            allowClear: false,
            theme: ""material"",
            ""language"": {
                ""noResults"": function () {
                    return ""Sin resultados encontrados."";
                }
            }
        });

        $(""#btnCloseModify"").click(function () {
            $.get('");
#nullable restore
#line 450 "C:\Desarrollo\DRS\DRS\DRS.UI\Views\Devoluciones\_New.cshtml"
              Write(Url.Action("IndexIni", "Devoluciones"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', function (content)\r\n            {\r\n                $(\"#replacetarget\").html(content);\r\n            });\r\n        });\r\n\r\n    });\r\n\r\n</script>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DRS.Entities.Devolucion> Html { get; private set; }
    }
}
#pragma warning restore 1591
