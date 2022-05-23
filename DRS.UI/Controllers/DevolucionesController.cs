using DRS.Business;
using DRS.Business.Functions;
using DRS.Entities;
using DRS.UI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DRS.UI.Controllers
{
    public class DevolucionesController : Controller
    {
        DevolucionesBus devolucionesBus = AppCore.Container.GetInstance<DevolucionesBus>();
        UserBus userBus = AppCore.Container.GetInstance<UserBus>();
        LogBus logBus = AppCore.Container.GetInstance<LogBus>();
        PermissionBus permissionBus = AppCore.Container.GetInstance<PermissionBus>();
        MenuBus menuBus = AppCore.Container.GetInstance<MenuBus>();

        [Authenticate]
        public IActionResult Index()
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();
            ViewBag.Usuario = user[2].Value.ToString();

            ViewBag.Menus = menuBus.GetMenusByIdUser(Convert.ToInt64(user[4].Value.ToString()));

            Int64 personId = Convert.ToInt64(user[5].Value.ToString());

            ViewBag.Clientes = devolucionesBus.GetClientes(userBus.GetClients(personId));
            ViewBag.MotivoEnvio = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio());
            ViewBag.EstatusDevolucion = devolucionesBus.GetEstatus();

            return View(new Devolucion());
        }

        [HttpGet]
        [Authenticate]
        public IActionResult IndexIni(long ClientId = 0, int ReasonSend = 0, int Status = 0)
        {
            List<Devoluciones> lstDevoluciones = new List<Devoluciones>();

            var user = this.User.Claims.First().Subject.Claims.ToList();

            List<Actions> actions = permissionBus.GetActionsByIdUserAndMenuId(Convert.ToInt64(user[5].Value.ToString()), 1);

            ViewBag.Menus = actions;

            lstDevoluciones = devolucionesBus.GetDevoluciones(ClientId, ReasonSend, Status, Convert.ToInt64(user[4].Value.ToString()));

            return PartialView("_Index", lstDevoluciones);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult GetEmployeeSelection(int ClientId)
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();
            Int64 personId = Convert.ToInt64(user[5].Value.ToString());
            string userEmp = user[6].Value.ToString();

            var clients = userBus.GetClients(personId).Where(a => a.IdCliente == ClientId).FirstOrDefault();

            List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).Where(a => a.EmployeeId == clients.EmployeeId).ToList();

            if(person.Count() == 0)
            {
                return Json(new PersonEmployee(){
                    CBusinessPartnerId = "0",
                    CeeUui = "0",
                    EmployeeId = 0,
                    TeeUi = "0",
                    TypeEmployee="0",
                });
            }

            return Json(person.FirstOrDefault());
        }

        [HttpGet]
        [Authenticate]
        public IActionResult New()
        {
            Devolucion devolucion = new Devolucion()
            {
                Details = new List<DetallesDevolucion>()
            };

            try
            {
                ViewBag.AlertModal = "alert alert-info";
                ViewBag.MessageModal = "Complete todos los campos obligatorios para crear una devolución";

                var user = this.User.Claims.First().Subject.Claims.ToList();
                Int64 personId = Convert.ToInt64(user[5].Value.ToString());

                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                string ce_ui = person.FirstOrDefault().CeeUui;

                if (ce_ui == "CC")
                {
                    ViewBag.EnableCC = true;
                }
                else
                {
                    ViewBag.EnableCC = false;
                }

                ViewBag.ClientesM = devolucionesBus.GetClientes(userBus.GetClients(personId));
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio());
                ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(person);

                //var empleadoResponsable = devolucionesBus.GetEmployeeByPersonId(personId).Where(a => a.IdEmpleado == person.FirstOrDefault().EmployeeId).FirstOrDefault();

                devolucion.EmpleadoResponsable = 0;

                HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                HttpContext.Session.SetString("MotivoEnvioId", devolucion.MotivoEnvio.ToString());

                devolucion.IsBtnContinueEnabled = true;

            }
            catch (Exception ex)
            {
                ViewBag.AlertModal = "alert alert-danger";
                ViewBag.MessageModal = ex.Message;
            }

            return PartialView("_New", devolucion);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult New(Devolucion devolucion)
        {
            try
            {
                var user = this.User.Claims.First().Subject.Claims.ToList();
                Int64 personId = Convert.ToInt64(user[5].Value.ToString());


                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                var ce_ui_obj = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault();

                ViewBag.ClientesM = devolucionesBus.GetClientes(userBus.GetClients(personId));
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio());
                ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(person);

                string ce_ui = string.Empty;

                if (ce_ui_obj == null)
                {
                    throw new Exception("* Seleccione el cliente para poder continuar.");
                }
                else
                {
                    ce_ui = ce_ui_obj.CeeUui;

                    if (ce_ui_obj.CeeUui == "CC")
                    {
                        ViewBag.EnableCC = true;
                    }
                    else
                    {
                        ViewBag.EnableCC = false;
                    }
                }

                
                var empleadoResponsable = devolucionesBus.GetEmployeeByPersonId(personId).Where(a => a.IdEmpleado == devolucion.EmpleadoResponsable).FirstOrDefault();

                if (empleadoResponsable != null)
                {
                    devolucion.EmpleadoResponsable = empleadoResponsable.IdEmpleado;
                }
                else
                {
                    devolucion.EmpleadoResponsable = 0;
                }

                ViewBag.Devoluciones = devolucion;

                var respuesta = devolucionesBus.New(devolucion, ce_ui);

                if (respuesta.Codigo == 0)
                {
                    throw new ApplicationException(respuesta.Mensaje);
                }

                HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                HttpContext.Session.SetString("MotivoEnvioId", devolucion.MotivoEnvio.ToString());
                HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(new List<DetallesDevolucion>()));

                devolucion.IsBtnContinueEnabled = false;

                HttpContext.Session.SetString("Devolucion", JsonConvert.SerializeObject(devolucion));

                logBus.CreateTransaction(new LogTransaccion()
                {
                    Descripcion = "Se crea el cabecero de la devolución correctamente.",
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    TransaccionId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                    TipoOperacion = 7
                });

                ViewBag.AlertModal = "alert alert-info";
                ViewBag.MessageModal = respuesta.Mensaje;
            }
            catch (Exception ex)
            {
                ViewBag.AlertModal = "alert alert-danger";
                ViewBag.MessageModal = ex.Message;

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = 0
                });

                devolucion.IsBtnContinueEnabled = true;
            }

            return PartialView("_New", devolucion);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult DetailsNew()
        {
            ViewBag.MessageDetailsNew = "Productos/accesorios de la devolución.";
            ViewBag.TypeAlertDetailsNew = "alert alert-info";

            List<DetallesDevolucion> lst = new List<DetallesDevolucion>();

            string valor = "";

            try
            {
                valor = HttpContext.Session.GetString("DetalleDevolucion").ToString();
            }
            catch
            {
                valor = "";
            }

            if (string.IsNullOrEmpty(valor))
            {
                HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(lst));

                lst = new List<DetallesDevolucion>();
            }
            else
            {

                try
                {
                    string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");

                    if (JSON.Length > 0)
                    {
                        var total = JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").ToArray();
                        lst = total.ToList();
                    }
                    else
                    {
                        lst = new List<DetallesDevolucion>();
                    }

                    HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(lst));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //lst = JsonConvert.DeserializeObject<List<DetallesDevolucion>>(JsonConvert.SerializeObject(HttpContext.Session.GetString("DetalleDevolucion").ToString()));
            }

            return PartialView("_DetailsNew", lst);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult NewProduct()
        {
            ViewBag.MessageNewProduct = "* Complete todos los campos para dar de alta un nuevo producto/accesorio";
            ViewBag.AlertNewProduct = "alert alert-info";

            DetallesDevolucion detallesDevolucion = new DetallesDevolucion()
            {
                MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()),
                Antena = 1,
                BaseCarga = 1,
                Bateria = 1,
                CableUSB = 1,
                CableUSBMagnetico = 1,
                Caja = 1,
                Carcasa = 1,
                CargadorEliminador = 1,
                Clip = 1,
                ConectorUSB = 1,
                Display = 1,
                HerramientaDeExtraccion = 1,
                Manual = 1,
                Tapa = 1,
                Teclado = 1,
                Alias = string.Empty,
                TeamVoxLiteAbierto = false,
                EvidenceForms = false,
                EvidenceLite = false,
                GPS = false,
                Grupo = string.Empty,
                Grupos = string.Empty,
                IdentificadorLlamadas = false,
                Leyenda = string.Empty,
                LlamadaPrivada = false,
                NombreCarpeta = string.Empty,
                Observaciones = string.Empty,
                SimPropiedadServitron = false,
                SitiosSuscribe = string.Empty,
                TeamVoxDispatch = false,
                TeamVoxModoSeguro = false,
                Zaypher = false,
                Producto = "0",
                Serie = "0",
                SIM = "",
                Otro = string.Empty,
                TipoProducto = 0,
                CarrierId = 0,
                PlanId = 0,
                Dn = string.Empty
            };

            if (Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 11 || Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 12)
            {
                detallesDevolucion.ExisteProducto = true;
                detallesDevolucion.Cantidad = "1";

            }
            else if (Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 10)
            {
                detallesDevolucion.ExisteProducto = false;
                detallesDevolucion.Cantidad = "1";
            }
            else
            {
                detallesDevolucion.ExisteProducto = true;
                detallesDevolucion.Cantidad = "1";
            }

            string devolucionJSON = HttpContext.Session.GetString("Devolucion");

            ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
            ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
            ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
            ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();
            ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

            return PartialView("_NewProduct", detallesDevolucion);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult NewProduct(DetallesDevolucion detalleDevolucion)
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();

            try
            {
                ViewBag.MessageNewProduct = "* Complete todos los campos para dar de alta un nuevo producto/accesorio";
                ViewBag.AlertNewProduct = "alert alert-info";

                detalleDevolucion.MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString());

                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                string devolucionJSON = HttpContext.Session.GetString("Devolucion");

                Devolucion devolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                ViewBag.StatusPza = devolucionesBus.GetStatusDetails();

                string valor = JsonConvert.SerializeObject(HttpContext.Session.GetString("DetalleDevolucion")).ToString();

                List<DetallesDevolucion> lst = new List<DetallesDevolucion>();

                if (valor.Replace("\"[]\"", "").Length > 0)
                {
                    string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");
                    var total = JsonConvert.DeserializeObject<List<DetallesDevolucion>>(JSON).ToArray();
                    lst = total.ToList();
                }

                var getProducts = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

                if (detalleDevolucion.ExisteProducto == true)
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                    else
                    {

                        ViewBag.ProductosM = getProducts;
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detalleDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                }
                else
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                    }
                    else
                    {
                        ViewBag.ProductosM = getProducts;
                    }

                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                }

                ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();

                if (devolucionesBus.NewProduct(detalleDevolucion, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()), lst, devolucion.ClaveDevolucion) == false)
                {
                    ViewBag.MessageNewProduct = "No se creó un nuevo producto/accesorio en el listado.";
                    ViewBag.AlertNewProduct = "alert alert-danger";
                }
                else
                {

                    if (lst.Count() == 0)
                    {
                        detalleDevolucion.Id = 1;
                    }
                    else
                    {
                        detalleDevolucion.Id = lst.Max(a => a.Id) + 1;
                    }

                    detalleDevolucion.Cantidad = Convert.ToInt64(detalleDevolucion.Cantidad).ToString();

                    if (detalleDevolucion.ExisteProducto == true)
                    {
                        if (detalleDevolucion.EsSoloDevolucion == true)
                        {
                            detalleDevolucion.Producto = devolucionesBus.GetAccessories(ce_ui).Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                        }
                        else
                        {
                            detalleDevolucion.Producto = getProducts.Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                        }
                    }
                    else
                    {
                        if (detalleDevolucion.EsSoloDevolucion == true)
                        {
                            detalleDevolucion.Producto = devolucionesBus.GetAccessories(ce_ui).Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                        }
                        else
                        {
                            detalleDevolucion.Producto = getProducts.Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                        }
                    }

                    //Obtenemos los datos del plan y carrier//
                    var producto = devolucionesBus.GetProductos(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString())).Where(a => a.Serie == detalleDevolucion.Serie).ToList();

                    //if(producto.Count > 0)
                    //{
                    //    if(detalleDevolucion.MotivoEnvio == 12)
                    //    {
                    //        detalleDevolucion.CarrierId = producto.Where(a => a.Serie == detalleDevolucion.Serie).FirstOrDefault().CarrierId;
                    //        detalleDevolucion.PlanId = producto.Where(a => a.Serie == detalleDevolucion.Serie).FirstOrDefault().PlanId;
                    //    }
                    //    else
                    //    {
                    //        detalleDevolucion.CarrierId = 0;
                    //        detalleDevolucion.PlanId = 0;
                    //    }
                    //}
                    //else
                    //{
                    //    detalleDevolucion.CarrierId = 0;
                    //    detalleDevolucion.PlanId = 0;
                    //}

                    lst.Add(detalleDevolucion);

                    //Devolucion devolucion = JsonConvert.DeserializeObject();
                    devolucionJSON = HttpContext.Session.GetString("Devolucion");

                    Devolucion deserializeDevolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);
                    deserializeDevolucion.Details = lst.ToList();

                    HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(lst.OrderBy(a => a.Id)));
                    HttpContext.Session.SetString("Devolucion", JsonConvert.SerializeObject(deserializeDevolucion));

                    ViewBag.MessageNewProduct = "Se ha añadido un producto/acceso a la lista de manera correcta.";
                    ViewBag.AlertNewProduct = "alert alert-success";

                    logBus.CreateTransaction(new LogTransaccion()
                    {
                        Descripcion = "Se añadió el producto/accesorio " + detalleDevolucion.Producto + " en la lista de detalles.",
                        FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                        TransaccionId = 0,
                        UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                        TipoOperacion = 8
                    });

                    DetallesDevolucion detallesDevolucion = new DetallesDevolucion()
                    {
                        MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()),
                        Antena = 1,
                        BaseCarga = 1,
                        Bateria = 1,
                        CableUSB = 1,
                        CableUSBMagnetico = 1,
                        Caja = 1,
                        Carcasa = 1,
                        CargadorEliminador = 1,
                        Clip = 1,
                        ConectorUSB = 1,
                        Display = 1,
                        HerramientaDeExtraccion = 1,
                        Manual = 1,
                        Tapa = 1,
                        Teclado = 1,
                        Alias = string.Empty,
                        TeamVoxLiteAbierto = false,
                        EvidenceForms = false,
                        EvidenceLite = false,
                        GPS = false,
                        Grupo = string.Empty,
                        Grupos = string.Empty,
                        IdentificadorLlamadas = false,
                        Leyenda = string.Empty,
                        LlamadaPrivada = false,
                        NombreCarpeta = string.Empty,
                        Observaciones = string.Empty,
                        SimPropiedadServitron = false,
                        SitiosSuscribe = string.Empty,
                        TeamVoxDispatch = false,
                        TeamVoxModoSeguro = false,
                        Zaypher = false,
                        Producto = "0",
                        Serie = "0",
                        SIM = "",
                        Otro= string.Empty,
                        TipoProducto = 0,
                        CarrierId = 0,
                        PlanId = 0,
                        Dn = string.Empty
                    };

                    if (Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 11 || Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 12)
                    {
                        detallesDevolucion.ExisteProducto = true;
                        detallesDevolucion.Cantidad = "1";

                    }
                    else if (Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 10)
                    {
                        detallesDevolucion.ExisteProducto = false;
                        detallesDevolucion.Cantidad = "1";
                    }
                    else
                    {
                        detallesDevolucion.ExisteProducto = true;
                        detallesDevolucion.Cantidad = "1";
                    }

                    ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
                    ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                    ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();
                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

                    return PartialView("_NewProduct", detallesDevolucion);

                }

                return PartialView("_NewProduct", detalleDevolucion);
            }
            catch (Exception ex)
            {
                ViewBag.MessageNewProduct = ex.Message;
                ViewBag.AlertNewProduct = "alert alert-danger";

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });

                return PartialView("_NewProduct", detalleDevolucion);
            }
        }

        [HttpGet]
        [Authenticate]
        public IActionResult ModifyProduct(int ProductId = 0)
        {
            DetallesDevolucion detallesDevolucion = new DetallesDevolucion();

            var user = this.User.Claims.First().Subject.Claims.ToList();

            string valor = "";

            try
            {
                valor = HttpContext.Session.GetString("DetalleDevolucion").ToString();
            }
            catch
            {
                valor = "";
            }

            string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");

            try
            {
                detallesDevolucion = JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").Where(a => a.Id == ProductId).FirstOrDefault();
                detallesDevolucion.MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString());


                ViewBag.MessageNewProduct = "* Complete todos los campos para modificar un producto/accesorio";
                ViewBag.AlertNewProduct = "alert alert-info";

                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                string devolucionJSON = HttpContext.Session.GetString("Devolucion");

                Devolucion devolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                if (detallesDevolucion.ExisteProducto == true)
                {
                    if (detallesDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                    else
                    {

                        ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detallesDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                }
                else
                {
                    if (detallesDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                    }
                    else
                    {
                        ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }

                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                }
                
                ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
                ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detallesDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();

            }
            catch (Exception ex)
            {
                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-info";
            }

            return PartialView("_ModifyProduct", detallesDevolucion);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult ModifyProduct(DetallesDevolucion detalleDevolucion)
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();

            try
            {
                ViewBag.MessageNewProduct = "* Complete todos los campos para modificar un producto/accesorio";
                ViewBag.AlertNewProduct = "alert alert-info";

                detalleDevolucion.MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString());

                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                string devolucionJSON = HttpContext.Session.GetString("Devolucion");

                Devolucion devolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                //List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                //string devolucionJSON = HttpContext.Session.GetString("Devolucion");

                //Devolucion devolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);

                //string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
                ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();

                string valor = JsonConvert.SerializeObject(HttpContext.Session.GetString("DetalleDevolucion")).ToString();

                List<DetallesDevolucion> lst = new List<DetallesDevolucion>();

                if (valor.Replace("\"[]\"", "").Length > 0)
                {
                    string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");
                    var total = JsonConvert.DeserializeObject<List<DetallesDevolucion>>(JSON).ToArray();
                    lst = total.ToList();
                }

                var getProducts = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

                detalleDevolucion.Cantidad = Convert.ToInt64(detalleDevolucion.Cantidad).ToString();

                if (detalleDevolucion.ExisteProducto == true)
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        detalleDevolucion.Producto = devolucionesBus.GetAccessories(ce_ui).Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                    }
                    else
                    {
                        detalleDevolucion.Producto = getProducts.Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                    }
                }
                else
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        detalleDevolucion.Producto = devolucionesBus.GetAccessories(ce_ui).Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                    }
                    else
                    {
                        detalleDevolucion.Producto = getProducts.Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                    }
                }

                if (detalleDevolucion.ExisteProducto == true)
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                    else
                    {

                        ViewBag.ProductosM = getProducts;
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detalleDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                }
                else
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                    }
                    else
                    {
                        ViewBag.ProductosM = getProducts;
                    }

                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                }

                if (devolucionesBus.ModifyProduct(detalleDevolucion, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()), lst, devolucion.ClaveDevolucion) == false)
                {
                    ViewBag.MessageNewProduct = "No se modificó el producto " + detalleDevolucion.Producto + " seleccionado.";
                    ViewBag.AlertNewProduct = "alert alert-danger";
                }
                else
                {
                    //Obtenemos los datos del plan y carrier//
                    //var producto = devolucionesBus.GetProductos(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString())).Where(a => a.Serie == detalleDevolucion.Serie).ToList();

                    //if (producto.Count > 0)
                    //{
                    //    if (detalleDevolucion.MotivoEnvio == 12)
                    //    {
                    //        detalleDevolucion.CarrierId = producto.Where(a => a.Serie == detalleDevolucion.Serie).FirstOrDefault().CarrierId;
                    //        detalleDevolucion.PlanId = producto.Where(a => a.Serie == detalleDevolucion.Serie).FirstOrDefault().PlanId;
                    //    }
                    //    else
                    //    {
                    //        detalleDevolucion.CarrierId = 0;
                    //        detalleDevolucion.PlanId = 0;
                    //    }
                    //}
                    //else
                    //{
                    //    detalleDevolucion.CarrierId = 0;
                    //    detalleDevolucion.PlanId = 0;
                    //}

                    lst.Where(w => w.Id == detalleDevolucion.Id).Select(s => new
                    {
                        s.Id,
                        V1 = s.Antena = detalleDevolucion.Antena,
                        V2 = s.BaseCarga = detalleDevolucion.BaseCarga,
                        V3 = s.Bateria = detalleDevolucion.Bateria,
                        V4 = s.CableUSB = detalleDevolucion.CableUSB,
                        V5 = s.CableUSBMagnetico = detalleDevolucion.CableUSBMagnetico,
                        V6 = s.Caja = detalleDevolucion.Caja,
                        V7 = s.Cantidad = detalleDevolucion.Cantidad,
                        V8 = s.Carcasa = detalleDevolucion.Carcasa,
                        V9 = s.CargadorEliminador = detalleDevolucion.CargadorEliminador,
                        V10 = s.Clip = detalleDevolucion.Clip,
                        V11 = s.ConectorUSB = detalleDevolucion.ConectorUSB,
                        V12 = s.Devolucion = detalleDevolucion.Devolucion,
                        V13 = s.Display = detalleDevolucion.Display,
                        V14 = s.EsSoloDevolucion = detalleDevolucion.EsSoloDevolucion,
                        V15 = s.ExisteProducto = detalleDevolucion.ExisteProducto,
                        V16 = s.HerramientaDeExtraccion = detalleDevolucion.HerramientaDeExtraccion,
                        V17 = s.Manual = detalleDevolucion.Manual,
                        V18 = s.MotivoEnvio = detalleDevolucion.MotivoEnvio,
                        V19 = s.Otro = detalleDevolucion.Otro,
                        V20 = s.Producto = detalleDevolucion.Producto,
                        V21 = s.Serie = detalleDevolucion.Serie,
                        V22 = s.SIM = detalleDevolucion.SIM,
                        V23 = s.Tapa = detalleDevolucion.Tapa,
                        V24 = s.Teclado = detalleDevolucion.Teclado,
                        V25 = s.TipoProducto = detalleDevolucion.TipoProducto,
                        V26 = s.Alias = detalleDevolucion.Alias,
                        V27 = s.EvidenceForms = detalleDevolucion.EvidenceForms,
                        V28 = s.EvidenceLite = detalleDevolucion.EvidenceLite,
                        V29 = s.GPS = detalleDevolucion.GPS,
                        V30 = s.Grupo = detalleDevolucion.Grupo,
                        V31 = s.Grupos = detalleDevolucion.Grupos,
                        V32 = s.IdentificadorLlamadas = detalleDevolucion.IdentificadorLlamadas,
                        V33 = s.Leyenda = detalleDevolucion.Leyenda,
                        V34 = s.LlamadaPrivada = detalleDevolucion.LlamadaPrivada,
                        V35 = s.NombreCarpeta = detalleDevolucion.NombreCarpeta,
                        V36 = s.Observaciones = detalleDevolucion.Observaciones,
                        V37 = s.SimPropiedadServitron = detalleDevolucion.SimPropiedadServitron,
                        V38 = s.SitiosSuscribe = detalleDevolucion.SitiosSuscribe,
                        V39 = s.TeamVoxDispatch = detalleDevolucion.TeamVoxDispatch,
                        V40 = s.TeamVoxLiteAbierto = detalleDevolucion.TeamVoxLiteAbierto,
                        V41 = s.TeamVoxModoSeguro = detalleDevolucion.TeamVoxModoSeguro,
                        V42 = s.Zaypher = detalleDevolucion.Zaypher,
                        V43 = s.PlanId = detalleDevolucion.PlanId,
                        V44 = s.CarrierId = detalleDevolucion.CarrierId,
                        V45 = s.Dn = detalleDevolucion.Dn
                    }).ToList();

                    //Devolucion devolucion = JsonConvert.DeserializeObject();
                    devolucionJSON = HttpContext.Session.GetString("Devolucion");

                    Devolucion deserializeDevolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);
                    deserializeDevolucion.Details = lst.ToList();

                    HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(lst.OrderBy(a => a.Id)));
                    HttpContext.Session.SetString("Devolucion", JsonConvert.SerializeObject(deserializeDevolucion));

                    ViewBag.MessageNewProduct = "El producto ha sido modificado correctamente.";
                    ViewBag.AlertNewProduct = "alert alert-success";

                    logBus.CreateTransaction(new LogTransaccion()
                    {
                        Descripcion = "Se modificó el producto/accesorio " + detalleDevolucion.Producto + " en la lista de detalles.",
                        FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                        TransaccionId = 0,
                        UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                        TipoOperacion = 9
                    });


                    ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
                    ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                    ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();
                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detalleDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

                    return PartialView("_ModifyProduct", detalleDevolucion);


                }

                return PartialView("_ModifyProduct", detalleDevolucion);
            }
            catch (Exception ex)
            {
                ViewBag.MessageNewProduct = ex.Message;
                ViewBag.AlertNewProduct = "alert alert-danger";

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });

                return PartialView("_ModifyProduct", detalleDevolucion);
            }
        }

        [HttpPost]
        [Authenticate]
        public IActionResult DeleteProduct(int ProductId = 0)
        {
            List<DetallesDevolucion> lst = new List<DetallesDevolucion>();

            var user = this.User.Claims.First().Subject.Claims.ToList();

            int contador = 1;

            string valor = "", nombreProducto = "";

            try
            {
                valor = HttpContext.Session.GetString("DetalleDevolucion").ToString();
            }
            catch
            {
                valor = "";
            }

            string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");

            try
            {
                if (JSON.Length > 0)
                {
                    var total = JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").ToArray();

                    foreach (var item in total)
                    {
                        if (item.Id != ProductId)
                        {
                            lst.Add(new DetallesDevolucion()
                            {
                                Antena = item.Antena,
                                BaseCarga = item.BaseCarga,
                                Bateria = item.Bateria,
                                CableUSB = item.CableUSB,
                                CableUSBMagnetico = item.CableUSBMagnetico,
                                Caja = item.Caja,
                                Cantidad = item.Cantidad,
                                Carcasa = item.Carcasa,
                                CargadorEliminador = item.CargadorEliminador,
                                Clip = item.Clip,
                                ConectorUSB = item.ConectorUSB,
                                Devolucion = item.Devolucion,
                                Display = item.Display,
                                EsSoloDevolucion = item.EsSoloDevolucion,
                                ExisteProducto = item.ExisteProducto,
                                HerramientaDeExtraccion = item.HerramientaDeExtraccion,
                                Id = contador,
                                Manual = item.Manual,
                                MotivoEnvio = item.MotivoEnvio,
                                Otro = item.Otro,
                                Producto = item.Producto,
                                Serie = item.Serie,
                                Tapa = item.Tapa,
                                Teclado = item.Teclado,
                                TipoProducto = item.TipoProducto,
                                SIM = item.SIM,
                                CarrierId = item.CarrierId,
                                PlanId = item.PlanId,
                                Dn = item.Dn,
                                Alias = item.Alias,
                                TeamVoxLiteAbierto = item.TeamVoxLiteAbierto,
                                EvidenceForms = item.EvidenceForms,
                                EvidenceLite = item.EvidenceLite,
                                GPS = item.GPS,
                                Grupo = item.Grupo,
                                Grupos = item.Grupos,
                                IdentificadorLlamadas = item.IdentificadorLlamadas,
                                Leyenda = item.Leyenda,
                                LlamadaPrivada = item.LlamadaPrivada,
                                NombreCarpeta = item.NombreCarpeta,
                                Observaciones = item.Observaciones,
                                SimPropiedadServitron = item.SimPropiedadServitron,
                                SitiosSuscribe = item.SitiosSuscribe,
                                TeamVoxDispatch = item.TeamVoxDispatch,
                                TeamVoxModoSeguro = item.TeamVoxModoSeguro,
                                Zaypher = item.Zaypher
                            });

                            contador++;
                        }
                        else
                        {
                            nombreProducto = item.Producto;
                        }
                    }
                }
                else
                {
                    lst = new List<DetallesDevolucion>();
                }

                HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(lst));

                ViewBag.MessageDetailsNew = "El producto ha sido eliminado del listado correctamente";
                ViewBag.TypeAlertDetailsNew = "alert alert-success";

                logBus.CreateTransaction(new LogTransaccion()
                {
                    Descripcion = "Se eliminó el producto/accesorio " + nombreProducto + " de la lista de detalles.",
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    TransaccionId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                    TipoOperacion = 10
                });

                return PartialView("_DetailsNew", lst);
            }
            catch (Exception ex)
            {
                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-info";

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });

                HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").ToArray()));

                return PartialView("_DetailsNew", JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").ToArray());
            }

        }

        [HttpGet]
        [Authenticate]
        public IActionResult DetailsProduct(int ProductId = 0)
        {
            DetallesDevolucion detallesDevolucion = new DetallesDevolucion();

            var user = this.User.Claims.First().Subject.Claims.ToList();

            string valor = "";

            try
            {
                valor = HttpContext.Session.GetString("DetalleDevolucion").ToString();
            }
            catch
            {
                valor = "";
            }

            string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");

            try
            {
                detallesDevolucion = JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").Where(a => a.Id == ProductId).FirstOrDefault();
                detallesDevolucion.MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString());

                if (Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 11 || Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 12)
                {
                    detallesDevolucion.ExisteProducto = true;
                    detallesDevolucion.Cantidad = "1";

                }
                else
                {
                    detallesDevolucion.ExisteProducto = true;
                    detallesDevolucion.Cantidad = string.Empty;
                }

                string devolucionJSON = HttpContext.Session.GetString("Devolucion");

                ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();
                ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
                ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detallesDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

                ViewBag.MessageNewProduct = "* Detalles del producto/accesorio " + detallesDevolucion.Producto + ".";
                ViewBag.AlertNewProduct = "alert alert-info";
            }
            catch (Exception ex)
            {
                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-info";
            }

            return PartialView("_DetailsProduct", detallesDevolucion);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult Create()
        {
            Devolucion devolucion = new Devolucion();

            var user = this.User.Claims.First().Subject.Claims.ToList();

            try
            {
                string devolucionJSON = HttpContext.Session.GetString("Devolucion");

                Int64 personId = Convert.ToInt64(user[5].Value.ToString());
                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                devolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                if (ce_ui == "CC")
                {
                    ViewBag.EnableCC = true;
                }
                else
                {
                    ViewBag.EnableCC = false;
                }

                if (devolucion.Details == null)
                {
                    devolucion.Details = new List<DetallesDevolucion>();
                }

                bool isCC = false;

                if (ce_ui == "CC")
                {
                    ViewBag.EnableCC = true;
                    isCC = true;
                }
                else
                {
                    ViewBag.EnableCC = false;
                    isCC = false;
                }

                ViewBag.ClientesM = devolucionesBus.GetClientes(userBus.GetClients(personId));
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio());
                ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(person);
                ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, devolucion.Cliente);

                ViewBag.Devoluciones = devolucion;

                devolucionesBus.Create(devolucion, user[4].Value.ToString(), isCC, user[1].Value.ToString());

                HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                HttpContext.Session.SetString("MotivoEnvioId", "0");


                HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(new List<DetallesDevolucion>()));
                HttpContext.Session.SetString("Devolucion", JsonConvert.SerializeObject(new Devolucion()));

                var empleadoResponsable = devolucionesBus.GetEmployeeByPersonId(personId);

                ViewBag.AlertModal = "alert alert-success";
                ViewBag.MessageModal = "La devolución ha sido creada exitosamente.";

                logBus.CreateTransaction(new LogTransaccion()
                {
                    Descripcion = "Se creo la devolución " + devolucion.ClaveDevolucion + " correctamente.",
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    TransaccionId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                    TipoOperacion = 7
                });

                if (empleadoResponsable != null)
                {
                    return PartialView("_New", new Devolucion()
                    {
                        EmpleadoResponsable = 0,
                        IsBtnContinueEnabled = true
                    });
                }
                else
                {
                    return PartialView("_New", new Devolucion()
                    {
                        EmpleadoResponsable = 0,
                        IsBtnContinueEnabled = true
                    });
                }

            }
            catch (Exception ex)
            {
                ViewBag.AlertModal = "alert alert-danger";
                ViewBag.MessageModal = ex.Message;

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });
            }

            return PartialView("_New", devolucion);
        }

        [Authenticate]
        public JsonResult AutoCompleteSerialProduct(int TipoProducto = 0)
        {
            List<SelectName> selectName = new List<SelectName>();

            string devolucionJSON = HttpContext.Session.GetString("Devolucion");

            var user = this.User.Claims.First().Subject.Claims.ToList();
            Int64 personId = Convert.ToInt64(user[5].Value.ToString());
            string ce_ui = user[6].Value.ToString();

            Devolucion devolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);

            if (devolucion.Details == null)
            {
                devolucion.Details = new List<DetallesDevolucion>();
            }

            return Json(devolucionesBus.GetSeriesForProductId(TipoProducto, devolucion.Cliente));
        }

        [Authenticate]
        public JsonResult GetSIMForProductAndSerial(int TipoProducto = 0, string Serial = "")
        {
            string devolucionJSON = HttpContext.Session.GetString("Devolucion");

            Devolucion devolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);

            if (devolucion.Details == null)
            {
                devolucion.Details = new List<DetallesDevolucion>();
            }

            var number = devolucionesBus.GetProductsByClientIdAndProductId(TipoProducto, devolucion.Cliente).Where(a => a.SerialDescription == Serial).FirstOrDefault();

            return Json(number);
        }

        [Authenticate]
        public JsonResult isTeamVox(int TipoProducto = 0)
        {
            int IsTeamVox = 0;

            var search = devolucionesBus.GetProducts().Where(a => a.ProductoId == TipoProducto).ToList();

            if (search.Count > 0)
            {
                IsTeamVox = search.FirstOrDefault().ProductTechnology.ToUpper() == "TEAMVOX" ? 1 : 0;
            }

            return Json(IsTeamVox);
        }

        [Authenticate]
        public JsonResult GetProduct(bool IsOnlyAccessory = false)
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();
            //Int64 personId = Convert.ToInt64(user[5].Value.ToString());

            string userEmp = user[6].Value.ToString();

            List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

            //Obtenemos los datos de la devolucion
            string devolucionJSON = HttpContext.Session.GetString("Devolucion");

            Devolucion deserializeDevolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);

            string ce_ui = person.Where(a => a.EmployeeId == deserializeDevolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

            if (IsOnlyAccessory == true)
            {
                return Json(devolucionesBus.GetAccessories(ce_ui));
            }

            return Json(devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString())));
        }

        [HttpGet]
        [Authenticate]
        public ActionResult Delete(long ReturnId = 0)
        {
            Devolucion devolucion = new Devolucion()
            {
                Details = new List<DetallesDevolucion>()
            };

            try
            {
                ViewBag.AlertModal = "alert alert-info";
                ViewBag.MessageModal = "¿Esta seguro de cancelar la devolución?";

                ViewBag.MessageDetailsNew = "Productos/accesorios de la devolución.";
                ViewBag.TypeAlertDetailsNew = "alert alert-info";

                var user = this.User.Claims.First().Subject.Claims.ToList();
                Int64 personId = Convert.ToInt64(user[5].Value.ToString());

                string userEmp = user[6].Value.ToString();

                devolucion = devolucionesBus.GetReturnById(ReturnId);

                if (devolucion == null)
                {
                    throw new ApplicationException("La devolución no se encuentra disponible para poder cancelarse.");
                }
                else
                {
                    ViewBag.ClientesM = devolucionesBus.GetClientesAll(userBus.GetClientsAll().Where(a => a.IdCliente == devolucion.Cliente).ToList());
                    ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio());


                    List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                    string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                    if (ce_ui == "CC")
                    {
                        ViewBag.EnableCC = true;
                    }
                    else
                    {
                        ViewBag.EnableCC = false;
                    }

                    ViewBag.ClientesM = devolucionesBus.GetClientes(userBus.GetClients(personId));
                    ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio());
                    ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(person);

                    var empleadoResponsable = devolucionesBus.GetEmployeeByPersonId(personId).Where(a => a.IdEmpleado == devolucion.EmpleadoResponsable).FirstOrDefault();

                    if (empleadoResponsable != null)
                    {
                        devolucion.EmpleadoResponsable = empleadoResponsable.IdEmpleado;
                    }
                    else
                    {
                        devolucion.EmpleadoResponsable = 0;
                    }

                    HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                    HttpContext.Session.SetString("MotivoEnvioId", devolucion.MotivoEnvio.ToString());

                    devolucion.IsBtnContinueEnabled = true;

                    if (devolucion.Estatus == 2)
                    {
                        devolucion.IsBtnContinueEnabled = false;

                        ViewBag.AlertModal = "alert alert-warning";
                        ViewBag.MessageModal = "La devolución seleccionada se encuentra cancelada.";
                    }

                }

            }
            catch (Exception ex)
            {
                ViewBag.AlertModal = "alert alert-danger";
                ViewBag.MessageModal = ex.Message;
            }

            return PartialView("_Delete", devolucion);
        }

        [HttpPost]
        [Authenticate]
        public ActionResult CancelReturn(long ReturnId = 0)
        {
            Int64 userId = 0;

            Devolucion devolucion = new Devolucion()
            {
                Details = new List<DetallesDevolucion>()
            };

            try
            {
                var user = this.User.Claims.First().Subject.Claims.ToList();
                Int64 personId = Convert.ToInt64(user[5].Value.ToString());
                userId = Convert.ToInt64(user[4].Value.ToString());

                ViewBag.MessageDetailsNew = "Productos/accesorios de la devolución.";
                ViewBag.TypeAlertDetailsNew = "alert alert-info";

                devolucion = devolucionesBus.GetReturnById(ReturnId);

                if (devolucion == null)
                {
                    throw new ApplicationException("La devolución no se encuentra disponible para poder cancelarse.");
                }

                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                bool isCC = false;

                if (ce_ui == "CC")
                {
                    ViewBag.EnableCC = true;
                    isCC = true;
                }
                else
                {
                    ViewBag.EnableCC = false;
                    isCC = false;
                }

                ViewBag.ClientesM = devolucionesBus.GetClientes(userBus.GetClients(personId));
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio());
                ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(person);

                var empleadoResponsable = devolucionesBus.GetEmployeeByPersonId(personId).Where(a => a.IdEmpleado == devolucion.EmpleadoResponsable).FirstOrDefault();

                if (empleadoResponsable != null)
                {
                    devolucion.EmpleadoResponsable = empleadoResponsable.IdEmpleado;
                }
                else
                {
                    devolucion.EmpleadoResponsable = 0;
                }

                HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                HttpContext.Session.SetString("MotivoEnvioId", devolucion.MotivoEnvio.ToString());

                var response = devolucionesBus.Cancel(devolucion, Convert.ToInt64(user[4].Value.ToString()), isCC, user[1].Value.ToString());

                ViewBag.AlertModal = "alert alert-success";
                ViewBag.MessageModal = response.Mensaje;

                if(response.Codigo == 0)
                {
                    ViewBag.AlertModal = "alert alert-danger";
                }

                devolucion.IsBtnContinueEnabled = false;

                logBus.CreateTransaction(new LogTransaccion()
                {
                    Descripcion = "Se canceló la devolución " + devolucion.ClaveDevolucion + " correctamente.",
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    TransaccionId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                    TipoOperacion = 15
                });

                return PartialView("_Delete", devolucion);
            }
            catch (Exception ex)
            {
                ViewBag.AlertModal = "alert alert-danger";
                ViewBag.MessageModal = ex.Message;

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message + "Clave de la devolución " + devolucion.ClaveDevolucion.ToString() + ".",
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = userId
                });
            }

            return PartialView("_Delete", devolucion);
        }

        [HttpPost]
        [Authenticate]
        public ActionResult AddComments(long ReturnId = 0, string comment = "")
        {
            List<Comentarios> comentarios = new List<Comentarios>();
            var user = this.User.Claims.First().Subject.Claims.ToList();

            try
            {
                Comentarios comments = new Comentarios();
                comments.ComentariosId = 0;
                comments.FechaCreación = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now);
                comments.UsuarioId = Convert.ToInt64(user[4].Value.ToString());
                comments.Descripcion = comment;

                var response = devolucionesBus.AddComments(comments, ReturnId);

                ViewBag.MessageComments = response.Mensaje;
                ViewBag.MessageAlertComments = "alert alert-success";

                if(response.Codigo == 0)
                {
                    ViewBag.MessageAlertComments = "alert alert-danger";
                    throw new ApplicationException(response.Mensaje);
                }
                
                comentarios = devolucionesBus.GetReturnById(ReturnId).Comentarios.OrderByDescending(a => a.FechaCreación).ToList();

                logBus.CreateTransaction(new LogTransaccion()
                {
                    Descripcion = "Se añadió un nuevo comentario para la devolución " + ReturnId.ToString() + " correctamente.",
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    TransaccionId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                    TipoOperacion = 16
                });
            }
            catch (Exception ex)
            {
                comentarios = devolucionesBus.GetReturnById(ReturnId).Comentarios;

                ViewBag.MessageComments = ex.Message;
                ViewBag.MessageAlertComments = "alert alert-danger";

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message + "Clave de la devolución " + ReturnId.ToString() + ".",
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });
            }

            return PartialView("_HistorialComments", comentarios);
        }

        [HttpGet]
        [Authenticate]
        public ActionResult HistorialComments(long ReturnId = 0)
        {
            List<Comentarios> comentarios = new List<Comentarios>();

            comentarios = devolucionesBus.GetReturnById(ReturnId).Comentarios.OrderByDescending(a => a.FechaCreación).ToList();

            ViewBag.MessageComments = "";
            ViewBag.MessageAlertComments = "";

            return PartialView("_HistorialComments", comentarios);
        }

        [HttpGet]
        [Authenticate]
        public ActionResult Modify(long ReturnId = 0)
        {
            Devolucion devolucion = new Devolucion()
            {
                Details = new List<DetallesDevolucion>()
            };

            try
            {
                ViewBag.AlertModal = "alert alert-info";
                ViewBag.MessageModal = "Información de la devolución a modificar, no olvide los campos obligatorios.";

                var user = this.User.Claims.First().Subject.Claims.ToList();
                Int64 personId = Convert.ToInt64(user[5].Value.ToString());

                

                ViewBag.ClientesM = devolucionesBus.GetClientesAll(userBus.GetClientsAll().ToList());
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio().ToList());
                ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(new List<PersonEmployee>());

                HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                HttpContext.Session.SetString("MotivoEnvioId", devolucion.MotivoEnvio.ToString());

                devolucion = devolucionesBus.GetReturnById(ReturnId);

                if (devolucion == null)
                {
                    throw new ApplicationException("La devolución no se encuentra disponible para poder cancelarse.");
                }
                else
                {
                    string userEmp = user[6].Value.ToString();

                    List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                    string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                    if (ce_ui == "CC")
                    {
                        ViewBag.EnableCC = true;
                    }
                    else
                    {
                        ViewBag.EnableCC = false;
                    }

                    ViewBag.ClientesM = devolucionesBus.GetClientesAll(userBus.GetClientsAll().Where(a => a.IdCliente == devolucion.Cliente).ToList());
                    ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio().Where(a => a.motivoenvioid == devolucion.MotivoEnvio).ToList());
                    ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(person);

                    HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(devolucion.Details));
                    HttpContext.Session.SetString("Devolucion", JsonConvert.SerializeObject(devolucion));
                }
                

                devolucion.IsBtnContinueEnabled = true;

            }
            catch (Exception ex)
            {
                ViewBag.AlertModal = "alert alert-danger";
                ViewBag.MessageModal = ex.Message;
            }

            return PartialView("_Modify", devolucion);
        }

        [HttpPost]
        [Authenticate]
        public ActionResult Modify(Devolucion devolucion)
        {

            var user = this.User.Claims.First().Subject.Claims.ToList();

            try
            {
                Int64 personId = Convert.ToInt64(user[5].Value.ToString());
                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                string valor = JsonConvert.SerializeObject(HttpContext.Session.GetString("DetalleDevolucion")).ToString();

                List<DetallesDevolucion> lst = new List<DetallesDevolucion>();

                if (valor.Replace("\"[]\"", "").Length > 0)
                {
                    string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");
                    var total = JsonConvert.DeserializeObject<List<DetallesDevolucion>>(JSON).ToArray();
                    lst = total.ToList();
                }

                devolucion.Details = lst;

                if (devolucion.Details == null)
                {
                    devolucion.Details = new List<DetallesDevolucion>();
                }

                bool isCC = false;

                if (ce_ui == "CC")
                {
                    ViewBag.EnableCC = true;
                    isCC = true;
                }
                else
                {
                    ViewBag.EnableCC = false;
                    isCC = false;
                }

                ViewBag.ClientesM = devolucionesBus.GetClientesAll(userBus.GetClientsAll().ToList());
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio().ToList());
                ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(person);

                ViewBag.Devoluciones = devolucion;

                devolucionesBus.Modify(devolucion, user[4].Value.ToString(), isCC, user[1].Value.ToString(), ce_ui);

                HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                HttpContext.Session.SetString("MotivoEnvioId", devolucion.MotivoEnvio.ToString());


                HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(devolucion.Details));
                HttpContext.Session.SetString("Devolucion", JsonConvert.SerializeObject(devolucion));

                var empleadoResponsable = devolucionesBus.GetEmployeeByPersonId(devolucion.EmpleadoResponsable);

                ViewBag.AlertModal = "alert alert-success";
                ViewBag.MessageModal = "La devolución ha sido modificada exitosamente.";

                logBus.CreateTransaction(new LogTransaccion()
                {
                    Descripcion = "Se modificó la devolución " + devolucion.ClaveDevolucion + " correctamente.",
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    TransaccionId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                    TipoOperacion = 11
                });

                devolucion.IsBtnContinueEnabled = true;

            }
            catch (Exception ex)
            {
                ViewBag.AlertModal = "alert alert-danger";
                ViewBag.MessageModal = ex.Message;

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });
            }

            return PartialView("_Modify", devolucion);
        }
    

        [HttpGet]
        [Authenticate]
        public ActionResult DetailsReservation(long ReturnId)
        {
            try
            {
                Devolucion devolucion = devolucionesBus.GetReturnById(ReturnId);

                string valor = JsonConvert.SerializeObject(HttpContext.Session.GetString("DetalleDevolucion")).ToString();

                List<DetallesDevolucion> lst = new List<DetallesDevolucion>();

                if (valor.Replace("\"[]\"", "").Length > 0)
                {
                    string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");
                    var total = JsonConvert.DeserializeObject<List<DetallesDevolucion>>(JSON).ToArray();
                    lst = total.ToList();
                }

                if(devolucion.Details.Count != lst.Count)
                {
                    devolucion.Details = lst;
                }

                ViewBag.MessageDetailsNew = "Productos/accesorios de la devolución.";
                ViewBag.TypeAlertDetailsNew = "alert alert-info";

                HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                HttpContext.Session.SetString("MotivoEnvioId", devolucion.MotivoEnvio.ToString());

                ViewBag.ConfirmRechazado = devolucion.ConfirmaRechazada;

                return PartialView("_DetailsReservation", devolucion.Details);
            }
            catch(Exception ex)
            {

                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-danger";

                return PartialView("_DetailsReservation", new List<DetallesDevolucion>());
            }
            
        }

        [HttpGet]
        [Authenticate]
        public ActionResult DetailsReservationForBtnClose()
        {
            try
            {
                ViewBag.MessageDetailsNew = "Productos/accesorios de la devolución.";
                ViewBag.TypeAlertDetailsNew = "alert alert-info";

                string valor = JsonConvert.SerializeObject(HttpContext.Session.GetString("DetalleDevolucion")).ToString();

                List<DetallesDevolucion> lst = new List<DetallesDevolucion>();

                if (valor.Replace("\"[]\"", "").Length > 0)
                {
                    string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");
                    var total = JsonConvert.DeserializeObject<List<DetallesDevolucion>>(JSON).ToArray();
                    lst = total.ToList();
                }

                return PartialView("_DetailsReservation", lst);
            }
            catch (Exception ex)
            {

                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-danger";

                return PartialView("_DetailsReservation", new List<DetallesDevolucion>());
            }
        }

        [HttpGet]
        [Authenticate]
        public IActionResult AddProduct()
        {
            ViewBag.MessageNewProduct = "* Complete todos los campos para dar de alta un nuevo producto/accesorio";
            ViewBag.AlertNewProduct = "alert alert-info";

            DetallesDevolucion detallesDevolucion = new DetallesDevolucion()
            {
                MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()),
                Antena = 1,
                BaseCarga = 1,
                Bateria = 1,
                CableUSB = 1,
                CableUSBMagnetico = 1,
                Caja = 1,
                Carcasa = 1,
                CargadorEliminador = 1,
                Clip = 1,
                ConectorUSB = 1,
                Display = 1,
                HerramientaDeExtraccion = 1,
                Manual = 1,
                Tapa = 1,
                Teclado = 1,
                Alias = string.Empty,
                TeamVoxLiteAbierto = false,
                EvidenceForms = false,
                EvidenceLite = false,
                GPS = false,
                Grupo = string.Empty,
                Grupos = string.Empty,
                IdentificadorLlamadas = false,
                Leyenda = string.Empty,
                LlamadaPrivada = false,
                NombreCarpeta = string.Empty,
                Observaciones = string.Empty,
                SimPropiedadServitron = false,
                SitiosSuscribe = string.Empty,
                TeamVoxDispatch = false,
                TeamVoxModoSeguro = false,
                Zaypher = false,
                Producto = "0",
                Serie = "0",
                SIM = "",
                Otro = string.Empty,
                TipoProducto = 0,
                CarrierId = 0,
                PlanId = 0,
                Dn = string.Empty
            };

            int idEnvioo = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString());

            if (Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 11 || Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 12)
            {
                detallesDevolucion.ExisteProducto = true;
                detallesDevolucion.Cantidad = "1";

            }
            else if (Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 10)
            {
                detallesDevolucion.ExisteProducto = false;
                detallesDevolucion.Cantidad = "1";
            }
            else
            {
                detallesDevolucion.ExisteProducto = true;
                detallesDevolucion.Cantidad = "1";
            }

            string devolucionJSON = HttpContext.Session.GetString("Devolucion");

            ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
            ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
            ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
            ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();
            ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

            return PartialView("_AddProduct", detallesDevolucion);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult AddProduct(DetallesDevolucion detalleDevolucion)
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();

            try
            {
                ViewBag.MessageNewProduct = "* Complete todos los campos para dar de alta un nuevo producto/accesorio";
                ViewBag.AlertNewProduct = "alert alert-info";

                detalleDevolucion.MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString());

                string ce_ui = user[6].Value.ToString();

                ViewBag.StatusPza = devolucionesBus.GetStatusDetails();

                string valor = JsonConvert.SerializeObject(HttpContext.Session.GetString("DetalleDevolucion")).ToString();

                List<DetallesDevolucion> lst = new List<DetallesDevolucion>();

                if (valor.Replace("\"[]\"", "").Length > 0)
                {
                    string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");
                    var total = JsonConvert.DeserializeObject<List<DetallesDevolucion>>(JSON).ToArray();
                    lst = total.ToList();
                }

                var getProducts = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

                if (detalleDevolucion.ExisteProducto == true)
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                    else
                    {

                        ViewBag.ProductosM = getProducts;
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detalleDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                }
                else
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                    }
                    else
                    {
                        ViewBag.ProductosM = getProducts;
                    }

                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                }

                ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();

                string devolucionJSON1 = HttpContext.Session.GetString("Devolucion");

                if (devolucionesBus.NewProduct(detalleDevolucion, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()), lst, JsonConvert.DeserializeObject<Devolucion>(devolucionJSON1).ClaveDevolucion) == false)
                {
                    ViewBag.MessageNewProduct = "No se creó un nuevo producto/accesorio en el listado.";
                    ViewBag.AlertNewProduct = "alert alert-danger";
                }
                else
                {

                    if (lst.Count() == 0)
                    {
                        detalleDevolucion.Id = 1;
                    }
                    else
                    {
                        detalleDevolucion.Id = lst.Max(a => a.Id) + 1;
                    }

                    detalleDevolucion.Cantidad = Convert.ToInt64(detalleDevolucion.Cantidad).ToString();

                    if (detalleDevolucion.ExisteProducto == true)
                    {
                        if (detalleDevolucion.EsSoloDevolucion == true)
                        {
                            detalleDevolucion.Producto = devolucionesBus.GetAccessories(ce_ui).Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                        }
                        else
                        {
                            detalleDevolucion.Producto = getProducts.Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                        }
                    }
                    else
                    {
                        if (detalleDevolucion.EsSoloDevolucion == true)
                        {
                            detalleDevolucion.Producto = devolucionesBus.GetAccessories(ce_ui).Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                        }
                        else
                        {
                            detalleDevolucion.Producto = getProducts.Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                        }
                    }

                    //Obtenemos los datos del plan y carrier//
                    var producto = devolucionesBus.GetProductos(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString())).Where(a => a.Serie == detalleDevolucion.Serie).ToList();

                    //if(producto.Count > 0)
                    //{
                    //    if(detalleDevolucion.MotivoEnvio == 12)
                    //    {
                    //        detalleDevolucion.CarrierId = producto.Where(a => a.Serie == detalleDevolucion.Serie).FirstOrDefault().CarrierId;
                    //        detalleDevolucion.PlanId = producto.Where(a => a.Serie == detalleDevolucion.Serie).FirstOrDefault().PlanId;
                    //    }
                    //    else
                    //    {
                    //        detalleDevolucion.CarrierId = 0;
                    //        detalleDevolucion.PlanId = 0;
                    //    }
                    //}
                    //else
                    //{
                    //    detalleDevolucion.CarrierId = 0;
                    //    detalleDevolucion.PlanId = 0;
                    //}

                    if(detalleDevolucion.StockConfirmation == null)
                    {
                        detalleDevolucion.StockConfirmation = new StockConfirmation();
                        detalleDevolucion.StockConfirmation.StockDetails = new List<StockConfirmationDetails>();
                    }
                    

                    lst.Add(detalleDevolucion);

                    //Devolucion devolucion = JsonConvert.DeserializeObject();
                    string devolucionJSON = HttpContext.Session.GetString("Devolucion");

                    Devolucion deserializeDevolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);
                    deserializeDevolucion.Details = lst.ToList();

                    HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(lst.OrderBy(a => a.Id)));
                    HttpContext.Session.SetString("Devolucion", JsonConvert.SerializeObject(deserializeDevolucion));

                    ViewBag.MessageNewProduct = "Se ha añadido un producto/acceso a la lista de manera correcta.";
                    ViewBag.AlertNewProduct = "alert alert-success";

                    logBus.CreateTransaction(new LogTransaccion()
                    {
                        Descripcion = "Se añadió el producto/accesorio " + detalleDevolucion.Producto + " en la lista de detalles.",
                        FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                        TransaccionId = 0,
                        UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                        TipoOperacion = 12
                    });

                    DetallesDevolucion detallesDevolucion = new DetallesDevolucion()
                    {
                        MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()),
                        Antena = 1,
                        BaseCarga = 1,
                        Bateria = 1,
                        CableUSB = 1,
                        CableUSBMagnetico = 1,
                        Caja = 1,
                        Carcasa = 1,
                        CargadorEliminador = 1,
                        Clip = 1,
                        ConectorUSB = 1,
                        Display = 1,
                        HerramientaDeExtraccion = 1,
                        Manual = 1,
                        Tapa = 1,
                        Teclado = 1,
                        Alias = string.Empty,
                        TeamVoxLiteAbierto = false,
                        EvidenceForms = false,
                        EvidenceLite = false,
                        GPS = false,
                        Grupo = string.Empty,
                        Grupos = string.Empty,
                        IdentificadorLlamadas = false,
                        Leyenda = string.Empty,
                        LlamadaPrivada = false,
                        NombreCarpeta = string.Empty,
                        Observaciones = string.Empty,
                        SimPropiedadServitron = false,
                        SitiosSuscribe = string.Empty,
                        TeamVoxDispatch = false,
                        TeamVoxModoSeguro = false,
                        Zaypher = false,
                        Producto = "0",
                        Serie = "0",
                        SIM = "",
                        Otro = string.Empty,
                        TipoProducto = 0,
                        CarrierId = 0,
                        PlanId = 0,
                        Dn = string.Empty
                    };

                    if (Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 11 || Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 12)
                    {
                        detallesDevolucion.ExisteProducto = true;
                        detallesDevolucion.Cantidad = "1";

                    }
                    else if (Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString()) == 10)
                    {
                        detallesDevolucion.ExisteProducto = false;
                        detallesDevolucion.Cantidad = "1";
                    }
                    else
                    {
                        detallesDevolucion.ExisteProducto = true;
                        detallesDevolucion.Cantidad = "1";
                    }

                    ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
                    ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                    ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();
                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

                    return PartialView("_AddProduct", detallesDevolucion);

                }

                return PartialView("_AddProduct", detalleDevolucion);
            }
            catch (Exception ex)
            {
                ViewBag.MessageNewProduct = ex.Message;
                ViewBag.AlertNewProduct = "alert alert-danger";

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });

                return PartialView("_AddProduct", detalleDevolucion);
            }
        }

        [HttpGet]
        [Authenticate]
        public IActionResult EditProduct(int ProductId = 0)
        {
            DetallesDevolucion detallesDevolucion = new DetallesDevolucion();

            var user = this.User.Claims.First().Subject.Claims.ToList();

            string valor = "";

            try
            {
                valor = HttpContext.Session.GetString("DetalleDevolucion").ToString();
            }
            catch
            {
                valor = "";
            }

            string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");

            try
            {
                detallesDevolucion = JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").Where(a => a.Id == ProductId).FirstOrDefault();

                //Devolucion devolucion = JsonConvert.DeserializeObject();
                string devolucionJSON = HttpContext.Session.GetString("Devolucion");

                Devolucion deserializeDevolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);
                HttpContext.Session.SetString("MotivoEnvioId", deserializeDevolucion.MotivoEnvio.ToString());
                HttpContext.Session.SetString("ClientId", deserializeDevolucion.Cliente.ToString());

                //Motivo de Envio
                detallesDevolucion.MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString());

                ViewBag.MessageNewProduct = "* Complete todos los campos para modificar un producto/accesorio";
                ViewBag.AlertNewProduct = "alert alert-info";

                string ce_ui = user[6].Value.ToString();

                if (detallesDevolucion.ExisteProducto == true)
                {
                    if (detallesDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                    else
                    {

                        ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detallesDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                }
                else
                {
                    if (detallesDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                    }
                    else
                    {
                        ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }

                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                }

                ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
                ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detallesDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();

                


            }
            catch (Exception ex)
            {
                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-info";
            }

            return PartialView("_EditProduct", detallesDevolucion);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult EditProduct(DetallesDevolucion detalleDevolucion)
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();

            try
            {
                ViewBag.MessageNewProduct = "* Complete todos los campos para modificar un producto/accesorio";
                ViewBag.AlertNewProduct = "alert alert-info";

                detalleDevolucion.MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString());

                string ce_ui = user[6].Value.ToString();

                ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
                ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();

                string valor = JsonConvert.SerializeObject(HttpContext.Session.GetString("DetalleDevolucion")).ToString();

                List<DetallesDevolucion> lst = new List<DetallesDevolucion>();

                if (valor.Replace("\"[]\"", "").Length > 0)
                {
                    string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");
                    var total = JsonConvert.DeserializeObject<List<DetallesDevolucion>>(JSON).ToArray();
                    lst = total.ToList();
                }

                var getProducts = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

                detalleDevolucion.Cantidad = Convert.ToInt64(detalleDevolucion.Cantidad).ToString();

                if (detalleDevolucion.ExisteProducto == true)
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        detalleDevolucion.Producto = devolucionesBus.GetAccessories(ce_ui).Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                    }
                    else
                    {
                        detalleDevolucion.Producto = getProducts.Where(a => a.Value == detalleDevolucion.TipoProducto.ToString()).FirstOrDefault().Text;
                    }
                }

                if (detalleDevolucion.ExisteProducto == true)
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                    else
                    {

                        ViewBag.ProductosM = getProducts;
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detalleDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                }
                else
                {
                    if (detalleDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                    }
                    else
                    {
                        ViewBag.ProductosM = getProducts;
                    }

                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                }

                string devolucionJSON1 = HttpContext.Session.GetString("Devolucion");

                if (devolucionesBus.ModifyProduct(detalleDevolucion, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()), lst, JsonConvert.DeserializeObject<Devolucion>(devolucionJSON1).ClaveDevolucion) == false)
                {
                    ViewBag.MessageNewProduct = "No se modificó el producto " + detalleDevolucion.Producto + " seleccionado.";
                    ViewBag.AlertNewProduct = "alert alert-danger";
                }
                else
                {
                    //Obtenemos los datos del plan y carrier//
                    //var producto = devolucionesBus.GetProductos(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString())).Where(a => a.Serie == detalleDevolucion.Serie).ToList();

                    //if (producto.Count > 0)
                    //{
                    //    if (detalleDevolucion.MotivoEnvio == 12)
                    //    {
                    //        detalleDevolucion.CarrierId = producto.Where(a => a.Serie == detalleDevolucion.Serie).FirstOrDefault().CarrierId;
                    //        detalleDevolucion.PlanId = producto.Where(a => a.Serie == detalleDevolucion.Serie).FirstOrDefault().PlanId;
                    //    }
                    //    else
                    //    {
                    //        detalleDevolucion.CarrierId = 0;
                    //        detalleDevolucion.PlanId = 0;
                    //    }
                    //}
                    //else
                    //{
                    //    detalleDevolucion.CarrierId = 0;
                    //    detalleDevolucion.PlanId = 0;
                    //}

                    lst.Where(w => w.Id == detalleDevolucion.Id).Select(s => new
                    {
                        s.Id,
                        V1 = s.Antena = detalleDevolucion.Antena,
                        V2 = s.BaseCarga = detalleDevolucion.BaseCarga,
                        V3 = s.Bateria = detalleDevolucion.Bateria,
                        V4 = s.CableUSB = detalleDevolucion.CableUSB,
                        V5 = s.CableUSBMagnetico = detalleDevolucion.CableUSBMagnetico,
                        V6 = s.Caja = detalleDevolucion.Caja,
                        V7 = s.Cantidad = detalleDevolucion.Cantidad,
                        V8 = s.Carcasa = detalleDevolucion.Carcasa,
                        V9 = s.CargadorEliminador = detalleDevolucion.CargadorEliminador,
                        V10 = s.Clip = detalleDevolucion.Clip,
                        V11 = s.ConectorUSB = detalleDevolucion.ConectorUSB,
                        V12 = s.Devolucion = detalleDevolucion.Devolucion,
                        V13 = s.Display = detalleDevolucion.Display,
                        V14 = s.EsSoloDevolucion = detalleDevolucion.EsSoloDevolucion,
                        V15 = s.ExisteProducto = detalleDevolucion.ExisteProducto,
                        V16 = s.HerramientaDeExtraccion = detalleDevolucion.HerramientaDeExtraccion,
                        V17 = s.Manual = detalleDevolucion.Manual,
                        V18 = s.MotivoEnvio = detalleDevolucion.MotivoEnvio,
                        V19 = s.Otro = detalleDevolucion.Otro,
                        V20 = s.Producto = detalleDevolucion.Producto,
                        V21 = s.Serie = detalleDevolucion.Serie,
                        V22 = s.SIM = detalleDevolucion.SIM,
                        V23 = s.Tapa = detalleDevolucion.Tapa,
                        V24 = s.Teclado = detalleDevolucion.Teclado,
                        V25 = s.TipoProducto = detalleDevolucion.TipoProducto,
                        V26 = s.Alias = detalleDevolucion.Alias,
                        V27 = s.EvidenceForms = detalleDevolucion.EvidenceForms,
                        V28 = s.EvidenceLite = detalleDevolucion.EvidenceLite,
                        V29 = s.GPS = detalleDevolucion.GPS,
                        V30 = s.Grupo = detalleDevolucion.Grupo,
                        V31 = s.Grupos = detalleDevolucion.Grupos,
                        V32 = s.IdentificadorLlamadas = detalleDevolucion.IdentificadorLlamadas,
                        V33 = s.Leyenda = detalleDevolucion.Leyenda,
                        V34 = s.LlamadaPrivada = detalleDevolucion.LlamadaPrivada,
                        V35 = s.NombreCarpeta = detalleDevolucion.NombreCarpeta,
                        V36 = s.Observaciones = detalleDevolucion.Observaciones,
                        V37 = s.SimPropiedadServitron = detalleDevolucion.SimPropiedadServitron,
                        V38 = s.SitiosSuscribe = detalleDevolucion.SitiosSuscribe,
                        V39 = s.TeamVoxDispatch = detalleDevolucion.TeamVoxDispatch,
                        V40 = s.TeamVoxLiteAbierto = detalleDevolucion.TeamVoxLiteAbierto,
                        V41 = s.TeamVoxModoSeguro = detalleDevolucion.TeamVoxModoSeguro,
                        V42 = s.Zaypher = detalleDevolucion.Zaypher,
                        V43 = s.PlanId = detalleDevolucion.PlanId,
                        V44 = s.CarrierId = detalleDevolucion.CarrierId,
                        V45 = s.Dn = detalleDevolucion.Dn,
                        V46 = s.IdDetallesDevolucion = detalleDevolucion.IdDetallesDevolucion
                    }).ToList();

                    //Devolucion devolucion = JsonConvert.DeserializeObject();
                    string devolucionJSON = HttpContext.Session.GetString("Devolucion");

                    Devolucion deserializeDevolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);
                    deserializeDevolucion.Details = lst.ToList();

                    HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(lst.OrderBy(a => a.Id)));
                    HttpContext.Session.SetString("Devolucion", JsonConvert.SerializeObject(deserializeDevolucion));

                    ViewBag.MessageNewProduct = "El producto ha sido modificado correctamente.";
                    ViewBag.AlertNewProduct = "alert alert-success";

                    logBus.CreateTransaction(new LogTransaccion()
                    {
                        Descripcion = "Se modificó el producto/accesorio " + detalleDevolucion.Producto + " en la lista de detalles.",
                        FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                        TransaccionId = 0,
                        UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                        TipoOperacion = 13
                    });


                    ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
                    ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                    ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();
                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detalleDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));

                    return PartialView("_EditProduct", detalleDevolucion);


                }

                return PartialView("_EditProduct", detalleDevolucion);
            }
            catch (Exception ex)
            {
                ViewBag.MessageNewProduct = ex.Message;
                ViewBag.AlertNewProduct = "alert alert-danger";

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });

                return PartialView("_EditProduct", detalleDevolucion);
            }
        }

        [HttpPost]
        [Authenticate]
        public IActionResult RemoveProduct(int ProductId = 0)
        {
            List<DetallesDevolucion> lst = new List<DetallesDevolucion>();

            var user = this.User.Claims.First().Subject.Claims.ToList();

            int contador = 1;

            string valor = "", nombreProducto = "";

            try
            {
                valor = HttpContext.Session.GetString("DetalleDevolucion").ToString();
            }
            catch
            {
                valor = "";
            }

            string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");

            try
            {
                if (JSON.Length > 0)
                {
                    var total = JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").ToArray();

                    if(total.Count() -1  == 0)
                    {
                        throw new ApplicationException("¡El listado no puede quedar vacío!");
                    }
                    else
                    {
                        foreach (var item in total)
                        {
                            if (item.Id != ProductId)
                            {


                                var detail = new DetallesDevolucion()
                                {
                                    Antena = item.Antena,
                                    BaseCarga = item.BaseCarga,
                                    Bateria = item.Bateria,
                                    CableUSB = item.CableUSB,
                                    CableUSBMagnetico = item.CableUSBMagnetico,
                                    Caja = item.Caja,
                                    Cantidad = item.Cantidad,
                                    Carcasa = item.Carcasa,
                                    CargadorEliminador = item.CargadorEliminador,
                                    Clip = item.Clip,
                                    ConectorUSB = item.ConectorUSB,
                                    Devolucion = item.Devolucion,
                                    Display = item.Display,
                                    EsSoloDevolucion = item.EsSoloDevolucion,
                                    ExisteProducto = item.ExisteProducto,
                                    HerramientaDeExtraccion = item.HerramientaDeExtraccion,
                                    Id = contador,
                                    Manual = item.Manual,
                                    MotivoEnvio = item.MotivoEnvio,
                                    Otro = item.Otro,
                                    Producto = item.Producto,
                                    Serie = item.Serie,
                                    Tapa = item.Tapa,
                                    Teclado = item.Teclado,
                                    TipoProducto = item.TipoProducto,
                                    SIM = item.SIM,
                                    CarrierId = item.CarrierId,
                                    PlanId = item.PlanId,
                                    Dn = item.Dn,
                                    Alias = item.Alias,
                                    TeamVoxLiteAbierto = item.TeamVoxLiteAbierto,
                                    EvidenceForms = item.EvidenceForms,
                                    EvidenceLite = item.EvidenceLite,
                                    GPS = item.GPS,
                                    Grupo = item.Grupo,
                                    Grupos = item.Grupos,
                                    IdentificadorLlamadas = item.IdentificadorLlamadas,
                                    Leyenda = item.Leyenda,
                                    LlamadaPrivada = item.LlamadaPrivada,
                                    NombreCarpeta = item.NombreCarpeta,
                                    Observaciones = item.Observaciones,
                                    SimPropiedadServitron = item.SimPropiedadServitron,
                                    SitiosSuscribe = item.SitiosSuscribe,
                                    TeamVoxDispatch = item.TeamVoxDispatch,
                                    TeamVoxModoSeguro = item.TeamVoxModoSeguro,
                                    Zaypher = item.Zaypher,
                                    IdDetallesDevolucion = item.IdDetallesDevolucion
                                };

                                if (item.StockConfirmation == null)
                                {
                                    detail.StockConfirmation = new StockConfirmation();
                                    detail.StockConfirmation.StockDetails = new List<StockConfirmationDetails>();
                                }
                                else
                                {
                                    detail.StockConfirmation = new StockConfirmation();
                                    detail.StockConfirmation = item.StockConfirmation;
                                }

                                lst.Add(detail);

                                contador++;
                            }
                            else
                            {
                                nombreProducto = item.Producto;
                            }
                        }
                    }
                    
                }
                else
                {
                    lst = new List<DetallesDevolucion>();
                }

                HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(lst));

                ViewBag.MessageDetailsNew = "El producto ha sido eliminado del listado correctamente.";
                ViewBag.TypeAlertDetailsNew = "alert alert-success";

                logBus.CreateTransaction(new LogTransaccion()
                {
                    Descripcion = "Se eliminó el producto/accesorio " + nombreProducto + " de la lista de detalles.",
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    TransaccionId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString()),
                    TipoOperacion = 14
                });

                return PartialView("_DetailsReservation", lst);
            }
            catch (Exception ex)
            {
                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-danger";

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });

                HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").ToArray()));

                return PartialView("_DetailsReservation", JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").ToArray());
            }

        }

        [HttpGet]
        [Authenticate]
        public ActionResult Details(long ReturnId = 0)
        {
            Devolucion devolucion = new Devolucion()
            {
                Details = new List<DetallesDevolucion>()
            };

            try
            {
                ViewBag.AlertModal = "alert alert-info";
                ViewBag.MessageModal = "Información acerca de la devolución";

                var user = this.User.Claims.First().Subject.Claims.ToList();
                Int64 personId = Convert.ToInt64(user[5].Value.ToString());

                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                devolucion = devolucionesBus.GetReturnById(ReturnId);

                if (devolucion == null)
                {
                    throw new ApplicationException("La devolución no se encuentra disponible para poder cancelarse.");
                }

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                if (ce_ui == "CC")
                {
                    ViewBag.EnableCC = true;
                }
                else
                {
                    ViewBag.EnableCC = false;
                }

                ViewBag.ClientesM = devolucionesBus.GetClientesAll(userBus.GetClientsAll());
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio());
                ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(person);

                var empleadoResponsable = devolucionesBus.GetEmployeeByPersonId(personId).Where(a =>a.IdEmpleado == devolucion.EmpleadoResponsable).FirstOrDefault();

                if (empleadoResponsable != null)
                {
                    devolucion.EmpleadoResponsable = empleadoResponsable.IdEmpleado;
                }
                else
                {
                    devolucion.EmpleadoResponsable = 0;
                }

                HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                HttpContext.Session.SetString("MotivoEnvioId", devolucion.MotivoEnvio.ToString());

                ViewBag.ClientesM = devolucionesBus.GetClientesAll(userBus.GetClientsAll().Where(a => a.IdCliente == devolucion.Cliente).ToList());
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio().Where(a => a.motivoenvioid == devolucion.MotivoEnvio).ToList());
                

                devolucion.IsBtnContinueEnabled = true;

                ViewBag.MessageDetailsNew = "Productos/accesorios de la devolución.";
                ViewBag.TypeAlertDetailsNew = "alert alert-info";
            }
            catch (Exception ex)
            {
                ViewBag.AlertModal = "alert alert-danger";
                ViewBag.MessageModal = ex.Message;
            }

            return PartialView("_Details", devolucion);
        }

        [HttpGet]
        [Authenticate]
        public ActionResult Stock(long ReturnId = 0)
        {
            Devolucion devolucion = new Devolucion()
            {
                Details = new List<DetallesDevolucion>()
            };

            try
            {
                ViewBag.AlertModal = "alert alert-info";
                ViewBag.MessageModal = "Información de la devolución a modificar, no olvide los campos obligatorios.";

                var user = this.User.Claims.First().Subject.Claims.ToList();
                Int64 personId = Convert.ToInt64(user[5].Value.ToString());

                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                devolucion = devolucionesBus.GetReturnById(ReturnId);

                if (devolucion == null)
                {
                    throw new ApplicationException("La devolución no se encuentra disponible para poder cancelarse.");
                }

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                if (ce_ui == "CC")
                {
                    ViewBag.EnableCC = true;
                }
                else
                {
                    ViewBag.EnableCC = false;
                }

                ViewBag.ClientesM = devolucionesBus.GetClientesAll(userBus.GetClientsAll().ToList());
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio().ToList());
                ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(person);

                HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                HttpContext.Session.SetString("MotivoEnvioId", devolucion.MotivoEnvio.ToString());

                

                ViewBag.ClientesM = devolucionesBus.GetClientesAll(userBus.GetClientsAll().Where(a => a.IdCliente == devolucion.Cliente).ToList());
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio().Where(a => a.motivoenvioid == devolucion.MotivoEnvio).ToList());

                HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(devolucion.Details));
                HttpContext.Session.SetString("Devolucion", JsonConvert.SerializeObject(devolucion));

                devolucion.IsBtnContinueEnabled = true;

            }
            catch (Exception ex)
            {
                ViewBag.AlertModal = "alert alert-danger";
                ViewBag.MessageModal = ex.Message;
            }

            return PartialView("_Stock", devolucion);

        }

        [HttpGet]
        [Authenticate]
        public ActionResult ServiceOrder(long ReturnId = 0)
        {
            Devolucion devolucion = new Devolucion()
            {
                Details = new List<DetallesDevolucion>()
            };

            try
            {
                ViewBag.AlertModal = "alert alert-info";
                ViewBag.MessageModal = "Información de la devolución a modificar, no olvide los campos obligatorios.";

                var user = this.User.Claims.First().Subject.Claims.ToList();
                Int64 personId = Convert.ToInt64(user[5].Value.ToString());

                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                devolucion = devolucionesBus.GetReturnById(ReturnId);

                if (devolucion == null)
                {
                    throw new ApplicationException("La devolución no se encuentra disponible para poder cancelarse.");
                }

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                if (ce_ui == "CC")
                {
                    ViewBag.EnableCC = true;
                }
                else
                {
                    ViewBag.EnableCC = false;
                }

                ViewBag.ClientesM = devolucionesBus.GetClientesAll(userBus.GetClientsAll().ToList());
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio().ToList());
                ViewBag.EmpleadosM = devolucionesBus.GetEmployeeFromSap(person);

                HttpContext.Session.SetString("ClientId", devolucion.Cliente.ToString());
                HttpContext.Session.SetString("MotivoEnvioId", devolucion.MotivoEnvio.ToString());



                ViewBag.ClientesM = devolucionesBus.GetClientesAll(userBus.GetClientsAll().Where(a => a.IdCliente == devolucion.Cliente).ToList());
                ViewBag.MotivoEnvioM = devolucionesBus.GetMotivoEnvioSlist(devolucionesBus.GetMotivoEnvio().Where(a => a.motivoenvioid == devolucion.MotivoEnvio).ToList());

                HttpContext.Session.SetString("DetalleDevolucion", JsonConvert.SerializeObject(devolucion.Details));
                HttpContext.Session.SetString("Devolucion", JsonConvert.SerializeObject(devolucion));

                devolucion.IsBtnContinueEnabled = true;

            }
            catch (Exception ex)
            {
                ViewBag.AlertModal = "alert alert-danger";
                ViewBag.MessageModal = ex.Message;
            }

            return PartialView("_ServiceOrder", devolucion);

        }

        [HttpGet]
        [Authenticate]
        public ActionResult DetailsReservationStock(long ReturnId)
        {
            try
            {
                Devolucion devolucion = devolucionesBus.GetReturnById(ReturnId);

                ViewBag.MessageDetailsNew = "Productos/accesorios de la devolución.";
                ViewBag.TypeAlertDetailsNew = "alert alert-info";

                ViewBag.ConfirmRechazado = devolucion.ConfirmaRechazada;

                return PartialView("_DetailsReservationStock", devolucion.Details);
            }
            catch (Exception ex)
            {

                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-danger";

                return PartialView("_DetailsReservationStock", new List<DetallesDevolucion>());
            }

        }

        [HttpGet]
        [Authenticate]
        public ActionResult DetailsReservationServiceOrder(long ReturnId)
        {
            try
            {
                Devolucion devolucion = devolucionesBus.GetReturnById(ReturnId);

                ViewBag.MessageDetailsNew = "Productos/accesorios de la devolución.";
                ViewBag.TypeAlertDetailsNew = "alert alert-info";

                ViewBag.ConfirmRechazado = devolucion.ConfirmaRechazada;

                return PartialView("_DetailsReservationServiceOrder", devolucion.Details.Where(a => a.StockConfirmation.StatusId == 5).ToList());
            }
            catch (Exception ex)
            {

                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-danger";

                return PartialView("_DetailsReservationServiceOrder", new List<DetallesDevolucion>());
            }

        }

        [HttpGet]
        [Authenticate]
        public IActionResult StockConfirm(int ProductId = 0)
        {
            DetallesDevolucion detallesDevolucion = new DetallesDevolucion();

            var user = this.User.Claims.First().Subject.Claims.ToList();

            string valor = "";

            try
            {
                valor = HttpContext.Session.GetString("DetalleDevolucion").ToString();
            }
            catch
            {
                valor = "";
            }

            string JSON = valor.Substring(1, valor.Length - 2).Replace(@"\", "");

            try
            {
                detallesDevolucion = JsonConvert.DeserializeObject<List<DetallesDevolucion>>("[" + JSON + "]").Where(a => a.IdDetallesDevolucion == ProductId).FirstOrDefault();

                //Devolucion devolucion = JsonConvert.DeserializeObject();
                string devolucionJSON = HttpContext.Session.GetString("Devolucion");

                Devolucion deserializeDevolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);
                HttpContext.Session.SetString("MotivoEnvioId", deserializeDevolucion.MotivoEnvio.ToString());
                HttpContext.Session.SetString("ClientId", deserializeDevolucion.Cliente.ToString());

                //Motivo de Envio
                detallesDevolucion.MotivoEnvio = Convert.ToInt32(HttpContext.Session.GetString("MotivoEnvioId").ToString());

                ViewBag.MessageNewProduct = "* Complete los campos para realizar la confirmación del producto/accesorio.";
                ViewBag.AlertNewProduct = "alert alert-info";
                HttpContext.Session.SetString("IsExists", detallesDevolucion.ExisteProducto.ToString());
                HttpContext.Session.SetString("IsAccesory", detallesDevolucion.EsSoloDevolucion.ToString());
                

                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                Devolucion devolucion = JsonConvert.DeserializeObject<Devolucion>(devolucionJSON);

                if (devolucion == null)
                {
                    throw new ApplicationException("La devolución no se encuentra disponible para poder cancelarse.");
                }

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                if (detallesDevolucion.ExisteProducto == true)
                {
                    if (detallesDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                    else
                    {

                        ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                        ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detallesDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }
                }
                else
                {
                    if (detallesDevolucion.EsSoloDevolucion == true)
                    {
                        ViewBag.ProductosM = devolucionesBus.GetAccessories(ce_ui);
                    }
                    else
                    {
                        ViewBag.ProductosM = devolucionesBus.GetProductsByClientId(Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                    }

                    ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(0, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                }

                ViewBag.StatusPza = devolucionesBus.GetStatusDetails();
                ViewBag.SerieM = devolucionesBus.GetSeriesForProductId(detallesDevolucion.TipoProducto, Convert.ToInt64(HttpContext.Session.GetString("ClientId").ToString()));
                ViewBag.CarrierM = devolucionesBus.GetCarriersForSelectList();
                ViewBag.PlanDataM = devolucionesBus.GetPlanDataForSelectList();
            }
            catch (Exception ex)
            {
                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-info";

                detallesDevolucion = new DetallesDevolucion();
            }

            return PartialView("_StockConfirm", detallesDevolucion);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult Confirmation(long ProductId = 0, long ReturnId = 0)
        {
            StockConfirmationDetails details = new StockConfirmationDetails();

            try
            {
                ViewBag.MessageConfirm = "Realice la confirmación de la recepción del producto/accesorio.";
                ViewBag.TypeAlertConfirm = "alert alert-info";

                var user = this.User.Claims.First().Subject.Claims.ToList();

                details.UserId = Convert.ToInt64(user[4].Value.ToString());
                ViewBag.IsExists = HttpContext.Session.GetString("IsExists").ToString();
                ViewBag.IsAccesory = HttpContext.Session.GetString("IsAccesory").ToString();

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }
            catch(Exception ex)
            {
                ViewBag.MessageConfirm = ex.Message;
                ViewBag.TypeAlertConfirm = "alert alert-danger";

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }
            

            return PartialView("_Confirmation", details);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult HistorialStock(long ProductId = 0, long ReturnId = 0)
        {
            HistorialStockConfirmation details = new HistorialStockConfirmation();

            try
            {
                ViewBag.MessageConfirm = "Historial de confirmación de mercancía.";
                ViewBag.TypeAlertConfirm = "alert alert-info";

                details = devolucionesBus.GetHistorialStock(ReturnId, ProductId);

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.MessageConfirm = ex.Message;
                ViewBag.TypeAlertConfirm = "alert alert-danger";

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }


            return PartialView("_HistorialStock", details);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult HistorialStockDetails(long ProductId = 0, long ReturnId = 0)
        {
            HistorialStockConfirmation details = new HistorialStockConfirmation();

            try
            {
                ViewBag.MessageConfirm = "Historial de confirmación de mercancía.";
                ViewBag.TypeAlertConfirm = "alert alert-info";

                details = devolucionesBus.GetHistorialStock(ReturnId, ProductId);

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.MessageConfirm = ex.Message;
                ViewBag.TypeAlertConfirm = "alert alert-danger";

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }


            return PartialView("_HistorialStockDetails", details);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult HistorialStockServiceOrder(long ProductId = 0, long ReturnId = 0)
        {
            HistorialStockConfirmation details = new HistorialStockConfirmation();

            try
            {
                ViewBag.MessageConfirm = "Historial de confirmación de mercancía.";
                ViewBag.TypeAlertConfirm = "alert alert-info";

                details = devolucionesBus.GetHistorialStock(ReturnId, ProductId);

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.MessageConfirm = ex.Message;
                ViewBag.TypeAlertConfirm = "alert alert-danger";

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }


            return PartialView("_HistorialStockServiceOrder", details);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult HistorialStockModify(long ProductId = 0, long ReturnId = 0)
        {
            HistorialStockConfirmation details = new HistorialStockConfirmation();

            try
            {
                ViewBag.MessageConfirm = "Historial de confirmación de mercancía.";
                ViewBag.TypeAlertConfirm = "alert alert-info";

                details = devolucionesBus.GetHistorialStock(ReturnId, ProductId);

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.MessageConfirm = ex.Message;
                ViewBag.TypeAlertConfirm = "alert alert-danger";

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }


            return PartialView("_HistorialStockModify", details);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult ConfirmProductAccesory(StockConfirmationDetails details, long ProductId = 0, int isConfirm = 0, long returnId = 0)
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();

            try
            {
                details.UserId = Convert.ToInt64(user[4].Value.ToString());

                ProductId = Convert.ToInt64(HttpContext.Session.GetString("ConfirmationStockProductId"));

                var response = devolucionesBus.ConfirmProductAccesory(details, ProductId, isConfirm, returnId);

                ViewBag.MessageConfirm = response.Mensaje;
                ViewBag.TypeAlertConfirm = "alert alert-success";
                ViewBag.IsExists = HttpContext.Session.GetString("IsExists").ToString();
                ViewBag.IsAccesory = HttpContext.Session.GetString("IsAccesory").ToString();

                details.StockConfirmationQuantity = String.Empty;

                if (response.Codigo == 0)
                {
                    throw new ApplicationException(response.Mensaje);
                }
            }
            catch(Exception ex)
            {
                ViewBag.MessageConfirm = ex.Message;
                ViewBag.TypeAlertConfirm = "alert alert-danger";

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });

                HttpContext.Session.SetString("ConfirmationStockProductId", ProductId.ToString());
            }

            return PartialView("_Confirmation", details);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult ConfirmRejected(long returnId = 0, int actionConfirmRejected = 0)
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();

            Devolucion devolucion = new Devolucion();

            try
            {
                devolucion = devolucionesBus.GetReturnById(returnId);

                ViewBag.ConfirmRechazado = devolucion.ConfirmaRechazada;

                string userEmp = user[6].Value.ToString();

                List<PersonEmployee> person = JsonConvert.DeserializeObject<List<PersonEmployee>>("" + userEmp + "").OrderBy(a => a.EmployeeId).ToList();

                if (devolucion == null)
                {
                    throw new ApplicationException("La devolución no se encuentra disponible para poder cancelarse.");
                }

                string ce_ui = person.Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault().CeeUui;

                var response = devolucionesBus.modifyStatusReturn(returnId, actionConfirmRejected, ce_ui);

                ViewBag.TypeAlertDetailsNew = "alert alert-success";
                ViewBag.MessageDetailsNew = response.Mensaje;

                if (response.Codigo == 0)
                {
                    throw new ApplicationException(response.Mensaje);
                }

                devolucion = devolucionesBus.GetReturnById(returnId);

                ViewBag.ConfirmRechazado = devolucion.ConfirmaRechazada;

                return PartialView("_DetailsReservationStock", devolucion.Details);
            }
            catch (Exception ex)
            {
                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });

                ViewBag.ConfirmRechazado = devolucion.ConfirmaRechazada;
                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-danger";

                return PartialView("_DetailsReservationStock", devolucion.Details);
            }
        }

        [HttpPost]
        [Authenticate]
        public IActionResult ConfirmRapid(long ProductId = 0, long returnId = 0)
        {
            Devolucion devolucion = new Devolucion();
            var user = this.User.Claims.First().Subject.Claims.ToList();

            try
            {

                devolucion = devolucionesBus.GetReturnById(returnId);

                ViewBag.ConfirmRechazado = devolucion.ConfirmaRechazada;

                StockConfirmationDetails details = new StockConfirmationDetails();

                details.UserId = Convert.ToInt64(user[4].Value.ToString());
                details.StockConfirmationQuantity = (Convert.ToInt64(devolucion.Details.Where(a => a.IdDetallesDevolucion == ProductId).FirstOrDefault().QuantityRestante)).ToString();
                details.StockConfirmationId = 0;
                details.StockConfirmationComments = "";
                details.StockConfirmationDetailsId = 0;

                var response = devolucionesBus.ConfirmProductAccesory(details, ProductId, 1, returnId);

                devolucion = devolucionesBus.GetReturnById(returnId);

                ViewBag.MessageDetailsNew = response.Mensaje;
                ViewBag.TypeAlertDetailsNew = "alert alert-success";

                ViewBag.ConfirmRechazado = devolucion.ConfirmaRechazada;

                if (response.Codigo == 0)
                {
                    throw new ApplicationException(response.Mensaje);
                }

                return PartialView("_DetailsReservationStock", devolucion.Details);
            }
            catch (Exception ex)
            {
                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });

                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-danger";

                return PartialView("_DetailsReservationStock", devolucion.Details);
            }
        }

        [HttpPost]
        [Authenticate]
        public IActionResult ConfirmAll(long returnId = 0)
        {
            Devolucion devolucion = new Devolucion();

            var user = this.User.Claims.First().Subject.Claims.ToList();

            try
            {
                devolucion = devolucionesBus.GetReturnById(returnId);

                ViewBag.ConfirmRechazado = devolucion.ConfirmaRechazada;

                Response response = new Response();

                foreach(var item in devolucion.Details)
                {
                    StockConfirmationDetails details = new StockConfirmationDetails();

                    details.UserId = Convert.ToInt64(user[4].Value.ToString());
                    details.StockConfirmationQuantity = devolucion.Details.Where(a => a.IdDetallesDevolucion == item.IdDetallesDevolucion).FirstOrDefault().Cantidad;
                    details.StockConfirmationId = 0;
                    details.StockConfirmationComments = "";
                    details.StockConfirmationDetailsId = 0;

                    response = devolucionesBus.ConfirmProductAccesory(details, item.IdDetallesDevolucion, 1, returnId);

                    if (response.Codigo == 0)
                    {
                        throw new ApplicationException(response.Mensaje);
                    }
                }

                devolucion = devolucionesBus.GetReturnById(returnId);

                ViewBag.ConfirmRechazado = devolucion.ConfirmaRechazada;

                ViewBag.MessageDetailsNew = "Los productos/accesorios han sido confirmados correctamente";
                ViewBag.TypeAlertDetailsNew = "alert alert-success";

                return PartialView("_DetailsReservationStock", devolucion.Details);
            }
            catch (Exception ex)
            {
                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    IncidenciaId = 0,
                    UsuarioId = Convert.ToInt64(user[4].Value.ToString())
                });

                ViewBag.MessageDetailsNew = ex.Message;
                ViewBag.TypeAlertDetailsNew = "alert alert-danger";

                return PartialView("_DetailsReservationStock", devolucion.Details);
            }
        }

    }
}
