#pragma checksum "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a55cdcba099f0845d1902c19df2812101d355ca5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users__Details), @"mvc.1.0.view", @"/Views/Users/_Details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a55cdcba099f0845d1902c19df2812101d355ca5", @"/Views/Users/_Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c460dfe88cd1c0b162bdec9d0f09e45ccc8bc311", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Users__Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DRS.Entities.Person>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""modal-header"">

    <h5 class=""modal-title alt-secondary-head"" id=""myModalLabel"">Detalles del usuario</h5>

    <button type=""button"" class=""close"" id=""btnClose"" data-dismiss=""modal"" aria-hidden=""true""><i class=""fa fa-close""></i></button>

</div>

");
#nullable restore
#line 11 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
 using (Html.BeginForm(null, null, FormMethod.Post, new { id = "NewForm" }))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"modal-body\">\r\n    <div class=\"row\">\r\n        <div class=\"col-12\">\r\n            <div class=\"alertModal\" id = \"alertModal\" style=\"width: -webkit-fill-available;\">\r\n                <div");
            BeginWriteAttribute("class", " class=\"", 573, "\"", 602, 1);
#nullable restore
#line 17 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
WriteAttributeValue("", 581, ViewBag.TypeAlertNew, 581, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"alertHeader\" role=\"alert\">\r\n                    <text>");
#nullable restore
#line 18 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                     Write(ViewBag.MessageNew);

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
        <div class=""col-12"">
            <div class=""accordion"" id=""accordionUser"" style=""width: 100%; margin-bottom: 50px;"">
                <div class=""card"">
                    <div class=""card-header-modal"" id=""headingOneUser"">
                        <h5 class=""mb-0"">
                            <button class=""btn btn-link btn-link-modal-panel"" type=""button"">
                                Datos personales
                            </button>
                            <button class=""btn btn-link btn-link-modal-panel pull-right"" type=""button"">
                                <span class=""pull-right clickable""><i class=""fa fa-chevron-up""></i></span>
                            </button>
         ");
            WriteLiteral(@"               </h5>
                    </div>
                    <div id=""collapseOneUser"" class=""show"" aria-labelledby=""headingOneUser"" data-parent=""#accordionUser"">
                        <div class=""card-body card-Body-OneUser"">
                            <div class=""row"">
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Nombre(s)</label>
                                    ");
#nullable restore
#line 46 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.TextBoxFor(model => model.Personname, new { @class = "form-control", placeholder = "Escribe el nombre(s)", style = "", @maxlength="200" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Apellido(s)</label>
                                    ");
#nullable restore
#line 50 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.TextBoxFor(model => model.Personlastname, new { @class = "form-control", placeholder = "Escribe el(los) apellido(s)", style = "", @maxlength="200" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""card"">
                    <div class=""card-header-modal"" id=""headingTwoUser"">
                        <h5 class=""mb-0"">
                            <button class=""btn btn-link btn-link-modal-panel"" type=""button"">
                                Datos de acceso
                            </button>
                            <button class=""btn btn-link btn-link-modal-panel pull-right"" type=""button"">
                                <span class=""pull-right clickable""><i class=""fa fa-chevron-up""></i></span>
                            </button>
                        </h5>
                    </div>
                    <div id=""collapseTwoUser"" class=""show"" aria-labelledby=""headingTwoUser"" data-parent=""#accordionUser"">
                        <div class=""card-body card-Body-TwoUser"">
                           ");
            WriteLiteral(" <div class=\"row\">\r\n                                <div class=\"col-lg-6 col-md-6 col-sm-12 col-xs-12\">\r\n                                    <label class=\"label-form-control\">* Correo electr??nico</label>\r\n                                    ");
#nullable restore
#line 72 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.TextBoxFor(model => model.Account.Email, new { @class = "form-control", placeholder = "Escribe un correo electr??nico", style = "", @maxlength="255" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Tel??fono de contacto</label>
                                    ");
#nullable restore
#line 76 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.TextBoxFor(model => model.Account.PhoneNumber, new { @class = "form-control", placeholder = "Escribe un tel??fono de contacto", style = "", @maxlength="13" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Rol de usuario</label>
                                    ");
#nullable restore
#line 80 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.DropDownListFor(model => model.UserRoleId, ((SelectList)ViewBag.UserRole), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Estatus de usuario</label>
                                    ");
#nullable restore
#line 84 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.DropDownListFor(model => model.Users.statusId, ((SelectList)ViewBag.Status), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12 divEmployee"">
                                    <label class=""label-form-control"">* Empleado responsable</label>
                                    <div class=""input-group"">
                                        ");
#nullable restore
#line 89 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                                   Write(Html.ListBoxFor(m => m.EmployeeId.SelectedMultiCompanyId, ViewBag.Employee as SelectList, new { id = "cbxEmployees", @class = "selectEmployee show-tick form-control input-md" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                     </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""card"">
                    <div class=""card-header-modal"" id=""headingThreeUser"">
                        <h5 class=""mb-0"">
                            <button class=""btn btn-link btn-link-modal-panel"" type=""button"">
                                Datos del representante autorizado
                            </button>
                            <button class=""btn btn-link btn-link-modal-panel pull-right"" type=""button"">
                                <span class=""pull-right clickable""><i class=""fa fa-chevron-up""></i></span>
                            </button>
                        </h5>
                    </div>
                    <div id=""collapseThreeUser"" class=""show"" aria-labelledby=""headingThreeUser"" data-parent=""#accordionUser"">
                        ");
            WriteLiteral(@"<div class=""card-body card-Body-ThreeUser"">
                            <div class=""row"">
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Unidad de venta</label>
                                    ");
#nullable restore
#line 112 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.DropDownListFor(model => model.Raprofile.Unitsalesid, ((SelectList)ViewBag.UnitSales), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Residencia fiscal</label>
                                    ");
#nullable restore
#line 116 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.DropDownListFor(model => model.Raprofile.Taxresidenceid, ((SelectList)ViewBag.TaxResidence), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Ubicaci??n recepci??n</label>
                                    ");
#nullable restore
#line 120 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.DropDownListFor(model => model.Raprofile.Locationidresidence, ((SelectList)ViewBag.Location1), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Ubicaci??n reparaci??n</label>
                                    ");
#nullable restore
#line 124 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.DropDownListFor(model => model.Raprofile.Locationidrepair, ((SelectList)ViewBag.Location2), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Ubicaci??n disponible</label>
                                    ");
#nullable restore
#line 128 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.DropDownListFor(model => model.Raprofile.Locationidavailable, ((SelectList)ViewBag.Location3), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Ubicaci??n calidad</label>
                                    ");
#nullable restore
#line 132 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.DropDownListFor(model => model.Raprofile.Locationidquality, ((SelectList)ViewBag.Location4), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Canal de distribuci??n</label>
                                    ");
#nullable restore
#line 136 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.DropDownListFor(model => model.Raprofile.Distributionchannelid, ((SelectList)ViewBag.Channel), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                                <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12"">
                                    <label class=""label-form-control"">* Servicio por defecto orden de servicio</label>
                                    ");
#nullable restore
#line 140 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
                               Write(Html.DropDownListFor(model => model.Raprofile.Defaultserviceorderid, ((SelectList)ViewBag.Default), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class=""modal-footer"">
    <div class=""row"" id=""footerBtns"">

        <div class=""col-md-12 col-lg-12 col-sm-12 col-xs-12"">
            <div class=""pull-right"">
                <button type=""button"" style=""margin-right: 5px !important;"" class=""btn btn-danger btn-sm"" id=""btnCloseM"" data-dismiss=""modal""><i class=""fa fa-close""></i>  Cerrar</button>
            </div>
        </div>

    </div>
</div>
");
#nullable restore
#line 161 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<script>
    $(document).ready(function () {

        $('.selectEmployee').select2({
            placeholder: ""Seleccione un empleado (al menos)."",
            allowClear: false,
            theme: ""material""
        });

        $(""#headingOneUser"").click(function () {

            var $this = $(this);

            if (!$(""#collapseOneUser"").hasClass('hide')) {
                $('.card-Body-OneUser').slideUp();
                $(""#collapseOneUser"").removeClass('show');
                $(""#collapseOneUser"").addClass('hide');
                $this.find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');

            } else {
                $('.card-Body-OneUser').slideDown();
                $(""#collapseOneUser"").removeClass('hide');
                $(""#collapseOneUser"").addClass('show');
                $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');

            }

        });

        $(""#headingTwoUser"").click(function () {

           ");
            WriteLiteral(@" var $this = $(this);

            if (!$(""#collapseTwoUser"").hasClass('hide')) {
                $('.card-Body-TwoUser').slideUp();
                $(""#collapseTwoUser"").removeClass('show');
                $(""#collapseTwoUser"").addClass('hide');
                $this.find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');

            } else {
                $('.card-Body-TwoUser').slideDown();
                $(""#collapseTwoUser"").removeClass('hide');
                $(""#collapseTwoUser"").addClass('show');
                $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');

            }

        });

        $(""#headingThreeUser"").click(function () {

            var $this = $(this);

            if (!$(""#collapseThreeUser"").hasClass('hide')) {
                $('.card-Body-ThreeUser').slideUp();
                $(""#collapseThreeUser"").removeClass('show');
                $(""#collapseThreeUser"").addClass('hide');
                $this.find('i').");
            WriteLiteral(@"removeClass('fa-chevron-up').addClass('fa-chevron-down');

            } else {
                $('.card-Body-ThreeUser').slideDown();
                $(""#collapseThreeUser"").removeClass('hide');
                $(""#collapseThreeUser"").addClass('show');
                $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');

            }

        });

        $.get('");
#nullable restore
#line 232 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
          Write(Url.Action("GetAllOrNotExployees", "Users"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"' + '?userRoleId=' + $(""#UserRoleId"").val(), function (content) {
          
            if(content.response == 0){
                $("".divEmployee"").hide();
            }
            else{
                $("".divEmployee"").show();
            }

        });


        $(""#btnCloseM"").click(function () {

            $(""#divLoading"").show();
            $(""#divReturns"").hide();

            $.get('");
#nullable restore
#line 249 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
              Write(Url.Action("IndexIni", "Users"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"', function (content)
            {
                $(""#replacetarget"").html(content);
            });
        });

        $(""#btnClose"").click(function () {

            $(""#divLoading"").show();
            $(""#divReturns"").hide();

            $.get('");
#nullable restore
#line 260 "C:\Users\Francisco Rivera\Desktop\drs\DRS.UI\Views\Users\_Details.cshtml"
              Write(Url.Action("IndexIni", "Users"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"', function (content)
            {
                $(""#replacetarget"").html(content);
            });
        });

        
        $(""#Personname"").attr('disabled', true);
        $(""#Personlastname"").attr('disabled', true);
        $(""#UserRoleId"").attr('disabled', true);
        $(""#cbxEmployees"").attr('disabled', true);
        $(""#Account_Email"").attr('disabled', true);
        $(""#Account_PhoneNumber"").attr('disabled', true);
        $(""#Users_statusId"").attr('disabled', true);

        $(""#Raprofile_Unitsalesid"").attr('disabled', true);
        $(""#Raprofile_Taxresidenceid"").attr('disabled', true);
        $(""#Raprofile_Locationidresidence"").attr('disabled', true);
        $(""#Raprofile_Locationidrepair"").attr('disabled', true);
        $(""#Raprofile_Locationidavailable"").attr('disabled', true);

        $(""#Raprofile_Locationidquality"").attr('disabled', true);
        $(""#Raprofile_Distributionchannelid"").attr('disabled', true);

        $(""#Raprofile_Defaultserviceorderid"").a");
            WriteLiteral("ttr(\'disabled\', true);\r\n\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DRS.Entities.Person> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
