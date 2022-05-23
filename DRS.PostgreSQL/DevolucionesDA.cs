using DRS.Data;
using DRS.Entities;
using DRS.PostgreSQL.Functions;
using DRS.PostgreSQL.Models;
//using DRS.PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRS.PostgreSQL
{
    public class DevolucionesDA : IDevolucionesDA
    {
   
        List<MotivoEnvio> IDevolucionesDA.GetMotivoEnvio()
        {

            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Reasonsends
                        select new MotivoEnvio
                        {
                            motivoenvioid = a.Reasonsendid,
                            motivoenvionombre = a.Reasonsendname
                        }).ToList();
            }

        }
        List<Contratos> IDevolucionesDA.GetContratos(Int64 clientId)
        {

            using(DRSContext dRS = new DRSContext())
            {
                //return (from a in dRS.Employeeclients
                //        join b in dRS.Contractclients on a.Clientid equals b.Clientid
                //        join c in dRS.Contracts on b.Contractid equals c.Contractid
                //        select new Contratos
                //        {
                //            ClaveContrato = c.Contractcode,
                //            IdContrato = c.Contractid,
                //            NombreContrato = c.Contractname,
                //            IdEmpleado = a.Employeeid
                //        }).Where(a => a.IdEmpleado == clientId).ToList();
                return new List<Contratos>();
            }
        }

        List<Clientes> IDevolucionesDA.GetClientes(long employeeId)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Employeeclients
                        join b in dRS.Clients on a.Clientid equals b.Clientid
                        where a.Employeeid == employeeId
                        select new Clientes
                        {
                            IdCliente = b.Clientid,
                            ClaveCliente = b.Clientcode,
                            NombreCliente = b.Clientname
                        }).ToList();
            }
        }

        List<Clientes> IDevolucionesDA.GetClientesAll()
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Employeeclients
                        join b in dRS.Clients on a.Clientid equals b.Clientid
                        select new Clientes
                        {
                            IdCliente = b.Clientid,
                            ClaveCliente = b.Clientcode,
                            NombreCliente = b.Clientname,
                            ClienteEstatus = Convert.ToString(b.Clientstatus),
                            PersonaId = a.Employeeid
                        }).ToList();
            }
        }

        public List<DevolucionesViewModel> GetDevoluciones()
        {
            using (DRSContext dRS = new DRSContext())
            {
                
                return (from a in dRS.Returns
                        join c in dRS.Clients on a.Clientid equals c.Clientid
                        join d in dRS.Reasonsends on a.Reasonsendid equals d.Reasonsendid
                        join g in dRS.Returnstatuses on a.Returnstatusid equals g.Returnstatusid
                        select new DevolucionesViewModel
                        {
                            Clientname = c.Clientname,
                            Returnssendedfor= a.Returnssendedfor,
                            Reasonsendid = d.Reasonsendid,
                            Returnstatusname = g.Returnstatusname,
                            Reasonsendname = d.Reasonsendname,
                            Returnsid = a.Returnsid,
                            Clientid = a.Clientid,
                            Employeeassigned = (dRS.Employees.Where(e => e.Employeeid == a.Employeeid).FirstOrDefault().TeeId)
                           
                        }).ToList();
            }
        }

        public List<Productos> GetProductsByClientId(long clientId)
        {
            using (DRSContext dRS = new DRSContext())
            {

                return (from a in dRS.Products
                        join b in dRS.Productclients on a.Productid equals b.Productid
                        select new Productos
                        {
                            ProductoId = a.Productid,
                            ProductCode = a.Productcode,
                            ProductName = a.Productname,
                            ClientId = b.Clientid,
                            CarrierId = (int)(b.Carrierid == null ? 0 : b.Carrierid),
                            PlanId = (int) (b.Planid == null ? 0 : b.Planid),
                            Serie = b.Serie
                        }).Where(a => a.ClientId == clientId).ToList();
            }
        }

        public List<EstatusDetalles> GetStatusDetails()
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Statusdetails
                        select new EstatusDetalles
                        {
                            EstatusDetallesId = a.Statusdetailsid,
                            EstatusDetallesNombre = a.Statusdetailsname
                        }).ToList();
            }
        }

        public Devolucion Create(Devolucion devolucion, string userId)
        {
            using(DRSContext dRS = new DRSContext())
            {
                //Obtenemos el usuario que esta logueado
                var userGet = dRS.Users.Where(a => a.Usersid == Convert.ToInt64(userId)).FirstOrDefault();

                //Comentarios//
                long ReturnId = 1;

                if(dRS.Returns.Count() > 0)
                {
                    ReturnId = dRS.Returns.Max(a => a.Returnsid) + 1;
                }

                using (var drsTran = dRS.Database.BeginTransaction())
                {
                    try
                    {
                        //Añadimos la devolución
                        Return returnMaster = new Return()
                        {
                            Clientid = devolucion.Cliente,
                            Returnssendedfor = devolucion.Envio,
                            Reasonsendid = devolucion.MotivoEnvio,
                            Returnsquoterepair = devolucion.Cotizar,
                            Returnsdescription = devolucion.Descripcion,
                            Returnsguidenumber = devolucion.NumeroGuia,
                            Returnsmessagername = devolucion.NombreMensajeria,
                            Returnscc = devolucion.DestinoCC,
                            Returnstatusid = devolucion.Estatus,
                            Returnscreatedate = devolucion.FechaCreacion,
                            Usersid = Convert.ToInt64(userId),
                            Employeeid = devolucion.EmpleadoResponsable
                        };

                        dRS.Add(returnMaster);

                        dRS.SaveChanges();

                        if(devolucion.Comentarios != null)
                        {
                            if (devolucion.Comentarios.Count() > 0)
                            {
                                ReturnId = 1;

                                if (dRS.Returns.Count() > 0)
                                {
                                    ReturnId = dRS.Returns.Max(a => a.Returnsid);
                                }

                                foreach (var item in devolucion.Comentarios)
                                {
                                    long valMax = 0;

                                    if (dRS.Commentsreturns.Count() == 0)
                                    {
                                        valMax = 1;
                                    }
                                    else
                                    {
                                        valMax = dRS.Commentsreturns.Max(a => a.Commentsreturnsid) + 1;
                                    }

                                    Commentsreturn commentsreturn = new Commentsreturn()
                                    {
                                        Commentsreturnscreatedate = devolucion.FechaCreacion,
                                        Commentsreturnsdescription = item.Descripcion,
                                        Returnsid = ReturnId,
                                        Usersid = Convert.ToInt64(userId),
                                        Users = userGet,
                                        Returns = returnMaster,
                                        Commentsreturnsid = valMax
                                    };

                                    dRS.Add(commentsreturn);
                                }

                                dRS.SaveChanges();
                            }

                        }

                        long contador = 1;

                        //Detalles de la devolucion//
                        foreach (var item in devolucion.Details)
                        {
                            //Comentarios//
                            long ReturnDetailsId = 1;

                            if (dRS.Returnsdetails.Count() > 0)
                            {
                                ReturnDetailsId = dRS.Returnsdetails.Max(a => a.Returnsdetailsid) + contador;
                            }

                            ReturnId = 1;

                            if (dRS.Returns.Count() > 0)
                            {
                                ReturnId = dRS.Returns.Max(a => a.Returnsid);
                            }

                            Returnsdetail returnsdetail = new Returnsdetail()
                            {
                                Returnsdetailsid = ReturnDetailsId,
                                Returnsisservitron = item.ExisteProducto,
                                Returnsisonlyaccesory = item.EsSoloDevolucion,
                                Productid = item.TipoProducto,
                                Productname = item.Producto,
                                Productquantity = Convert.ToInt32(item.Cantidad),
                                Productserialnumber = item.Serie == null ? "" : item.Serie,
                                Returnsdetailsantenna = item.Antena,
                                Returnsdetailscase = item.Carcasa,
                                Returnsdetailsdisplay = item.Display,
                                Returnsdetailsusb = item.CableUSB,
                                Returnsdetailsother = item.Otro == null ? "" : item.Otro,
                                Returnsdetailscover = item.Tapa,
                                Returnsdetailsbattery = item.Bateria,
                                Returnsdetailscharged = item.CargadorEliminador,
                                Returnsdetailsusbcable = item.CableUSB,
                                Returnsdetailsusbmagneticcable = item.CableUSBMagnetico,
                                Returnsdetailschargedbase = item.BaseCarga,
                                Returnsdetailsclip = item.Clip,
                                Returnsdetailsmanual = item.Manual,
                                Returnsdetailsbox = item.Caja,
                                Returnsdetailsextractiontool = item.HerramientaDeExtraccion,
                                Returnsid = ReturnId,
                                Returnssim = item.SIM == null ? "" : item.SIM,
                                Returnsdetailslegend = item.Leyenda == null ? "" : item.Leyenda,
                                Returnsdetailsfoldername = item.NombreCarpeta == null ? "" : item.NombreCarpeta,
                                Returnsdetailscalledidentifier = item.IdentificadorLlamadas,
                                Returnsdetailsgps = item.GPS,
                                Returnsdetailscallprivate = item.LlamadaPrivada,
                                Returnsdetailsgroups = item.Grupos == null ? "" : item.Grupos,
                                Returnsdetailssuscribesites = item.SitiosSuscribe,
                                Returnsdetailsalias = item.Alias == null ? "" : item.Alias,
                                Returnsdetailsgroup = item.Grupo == null ? "" : item.Grupo,
                                Returnsdetailssimisservitron = item.SimPropiedadServitron,
                                Returnsdetailsteamvoxliteopen = item.TeamVoxLiteAbierto,
                                Returnsdetailsteamvoxsecuremode = item.TeamVoxModoSeguro,
                                Returnsdetailsevidencelite = item.EvidenceLite,
                                Returnsdetailsevidenceforms = item.EvidenceForms,
                                Returnsdetailszaypher = item.Zaypher,
                                Returnsdetailsteamvoxdispatch = item.TeamVoxDispatch,
                                Returnsdetailsobservations = item.Observaciones == null ? "" : item.Observaciones,
                                Returnsdetailscarrierid = item.CarrierId,
                                Returnsdetailsplanid = item.PlanId,
                                Returnsdetailsdialnumber = item.Dn == null ? "":item.Dn,
                                Returnsdetailskeyboard = item.Teclado,
                                Returnsdetailsusbconector = item.ConectorUSB
                            };

                            dRS.Add(returnsdetail);

                            contador++;
                        }

                        dRS.SaveChanges();

                        drsTran.Commit();

                    }
                    catch (Exception ex)
                    {
                        drsTran.Rollback();
                        throw new ApplicationException(ex.Message);
                    }
                }

                devolucion.ClaveDevolucion = ReturnId;

                return devolucion;
            }
        }

        public Devolucion Modify(Devolucion devolucion, string userId)
        {
            using (DRSContext dRS = new DRSContext())
            {
                //Obtenemos el usuario que esta logueado
                var userGet = dRS.Users.Where(a => a.Usersid == Convert.ToInt64(userId)).FirstOrDefault();

                var getReturn = dRS.Returns.Where(a => a.Returnsid == devolucion.ClaveDevolucion).FirstOrDefault();

                using (var drsTran = dRS.Database.BeginTransaction())
                {
                    try
                    {
                        //Añadimos la devolución
                        getReturn.Clientid = devolucion.Cliente;
                        getReturn.Returnssendedfor = devolucion.Envio;
                        getReturn.Reasonsendid = devolucion.MotivoEnvio;
                        getReturn.Returnsquoterepair = devolucion.Cotizar;
                        getReturn.Returnsdescription = devolucion.Descripcion;
                        getReturn.Returnsguidenumber = devolucion.NumeroGuia;
                        getReturn.Returnsmessagername = devolucion.NombreMensajeria;
                        getReturn.Returnscc = devolucion.DestinoCC;
                        getReturn.Employeeid = devolucion.EmpleadoResponsable;
                        getReturn.Returnstatusid = devolucion.Estatus;

                        dRS.Update(getReturn);

                        dRS.SaveChanges();

                        //Detalles de la devolucion//
                        foreach (var item in dRS.Returnsdetails.Where(a => a.Returnsid == getReturn.Returnsid).ToList())
                        {

                            //Obtenemos los valores que existen y los que no//
                            var getUpdate = devolucion.Details.Where(a => a.IdDetallesDevolucion != 0 && a.IdDetallesDevolucion == item.Returnsdetailsid).Count();

                            if (getUpdate > 0) //Actualizamos los registros existentes
                            {
                                var getUpdateAction = devolucion.Details.Where(a => a.IdDetallesDevolucion != 0 && a.IdDetallesDevolucion == item.Returnsdetailsid).FirstOrDefault();

                                item.Returnsdetailsid = getUpdateAction.IdDetallesDevolucion;
                                item.Returnsisservitron = getUpdateAction.ExisteProducto;
                                item.Returnsisonlyaccesory = getUpdateAction.EsSoloDevolucion;
                                item.Productid = getUpdateAction.TipoProducto;
                                item.Productname = getUpdateAction.Producto;
                                item.Productquantity = Convert.ToInt32(getUpdateAction.Cantidad);
                                item.Productserialnumber = getUpdateAction.Serie == null ? "" : getUpdateAction.Serie;
                                item.Returnsdetailsantenna = getUpdateAction.Antena;
                                item.Returnsdetailscase = getUpdateAction.Carcasa;
                                item.Returnsdetailsdisplay = getUpdateAction.Display;
                                item.Returnsdetailsusb = getUpdateAction.CableUSB;
                                item.Returnsdetailsother = getUpdateAction.Otro == null ? "" : getUpdateAction.Otro;
                                item.Returnsdetailscover = getUpdateAction.Tapa;
                                item.Returnsdetailsbattery = getUpdateAction.Bateria;
                                item.Returnsdetailscharged = getUpdateAction.CargadorEliminador;
                                item.Returnsdetailsusbcable = getUpdateAction.CableUSB;
                                item.Returnsdetailsusbmagneticcable = getUpdateAction.CableUSBMagnetico;
                                item.Returnsdetailschargedbase = getUpdateAction.BaseCarga;
                                item.Returnsdetailsclip = getUpdateAction.Clip;
                                item.Returnsdetailsmanual = getUpdateAction.Manual;
                                item.Returnsdetailsbox = getUpdateAction.Caja;
                                item.Returnsdetailsextractiontool = getUpdateAction.HerramientaDeExtraccion;
                                item.Returnsid = devolucion.ClaveDevolucion;
                                item.Returnssim = getUpdateAction.SIM == null ? "" : getUpdateAction.SIM;
                                item.Returnsdetailslegend = getUpdateAction.Leyenda == null ? "" : getUpdateAction.Leyenda;
                                item.Returnsdetailsfoldername = getUpdateAction.NombreCarpeta == null ? "" : getUpdateAction.NombreCarpeta;
                                item.Returnsdetailscalledidentifier = getUpdateAction.IdentificadorLlamadas;
                                item.Returnsdetailsgps = getUpdateAction.GPS;
                                item.Returnsdetailscallprivate = getUpdateAction.LlamadaPrivada;
                                item.Returnsdetailsgroups = getUpdateAction.Grupos == null ? "" : getUpdateAction.Grupos;
                                item.Returnsdetailssuscribesites = getUpdateAction.SitiosSuscribe;
                                item.Returnsdetailsalias = getUpdateAction.Alias == null ? "" : getUpdateAction.Alias;
                                item.Returnsdetailsgroup = getUpdateAction.Grupo == null ? "" : getUpdateAction.Grupo;
                                item.Returnsdetailssimisservitron = getUpdateAction.SimPropiedadServitron;
                                item.Returnsdetailsteamvoxliteopen = getUpdateAction.TeamVoxLiteAbierto;
                                item.Returnsdetailsteamvoxsecuremode = getUpdateAction.TeamVoxModoSeguro;
                                item.Returnsdetailsevidencelite = getUpdateAction.EvidenceLite;
                                item.Returnsdetailsevidenceforms = getUpdateAction.EvidenceForms;
                                item.Returnsdetailszaypher = getUpdateAction.Zaypher;
                                item.Returnsdetailsteamvoxdispatch = getUpdateAction.TeamVoxDispatch;
                                item.Returnsdetailsobservations = getUpdateAction.Observaciones == null ? "" : getUpdateAction.Observaciones;
                                item.Returnsdetailscarrierid = getUpdateAction.CarrierId;
                                item.Returnsdetailsplanid = getUpdateAction.PlanId;
                                item.Returnsdetailsdialnumber = getUpdateAction.Dn == null ? "" : getUpdateAction.Dn;
                                item.Returnsdetailskeyboard = getUpdateAction.Teclado;
                                item.Returnsdetailsusbconector = getUpdateAction.ConectorUSB;

                                dRS.Update(item);
                            }
                            else //Eliminamos los no existentes
                            {
                                //Se valida si existe un registro dentro de los productos con acciones confirmadas//
                                var itemConfirm = dRS.Stockconfirmations.Where(a => a.Returnsdetailsid == item.Returnsdetailsid).FirstOrDefault();

                                if(itemConfirm != null)
                                {
                                    foreach(var itemSD in dRS.Stockconfirmationdetails.Where(a => a.Stockconfirmationid == itemConfirm.Stockconfirmationid).ToList())
                                    {
                                        dRS.Remove(itemSD);
                                    }

                                    

                                    dRS.Remove(itemConfirm);
                                }

                                dRS.Remove(item);
                            }

                        }

                        dRS.SaveChanges();

                        long contador = 1;

                        //Se añaden los nuevos registros.
                        foreach (var item in devolucion.Details.Where(a => a.IdDetallesDevolucion == 0).ToList())
                        {
                            long ReturnDetailsId = 1;

                            if (dRS.Returns.Count() > 0)
                            {
                                ReturnDetailsId = dRS.Returnsdetails.Max(a => a.Returnsdetailsid) + contador;
                            }

                            Returnsdetail returnsdetail = new Returnsdetail()
                            {
                                Returnsdetailsid = ReturnDetailsId,
                                Returnsisservitron = item.ExisteProducto,
                                Returnsisonlyaccesory = item.EsSoloDevolucion,
                                Productid = item.TipoProducto,
                                Productname = item.Producto,
                                Productquantity = Convert.ToInt32(item.Cantidad),
                                Productserialnumber = item.Serie == null ? "" : item.Serie,
                                Returnsdetailsantenna = item.Antena,
                                Returnsdetailscase = item.Carcasa,
                                Returnsdetailsdisplay = item.Display,
                                Returnsdetailsusb = item.CableUSB,
                                Returnsdetailsother = item.Otro == null ? "" : item.Otro,
                                Returnsdetailscover = item.Tapa,
                                Returnsdetailsbattery = item.Bateria,
                                Returnsdetailscharged = item.CargadorEliminador,
                                Returnsdetailsusbcable = item.CableUSB,
                                Returnsdetailsusbmagneticcable = item.CableUSBMagnetico,
                                Returnsdetailschargedbase = item.BaseCarga,
                                Returnsdetailsclip = item.Clip,
                                Returnsdetailsmanual = item.Manual,
                                Returnsdetailsbox = item.Caja,
                                Returnsdetailsextractiontool = item.HerramientaDeExtraccion,
                                Returnsid = devolucion.ClaveDevolucion,
                                Returnssim = item.SIM == null ? "" : item.SIM,
                                Returnsdetailslegend = item.Leyenda == null ? "" : item.Leyenda,
                                Returnsdetailsfoldername = item.NombreCarpeta == null ? "" : item.NombreCarpeta,
                                Returnsdetailscalledidentifier = item.IdentificadorLlamadas,
                                Returnsdetailsgps = item.GPS,
                                Returnsdetailscallprivate = item.LlamadaPrivada,
                                Returnsdetailsgroups = item.Grupos == null ? "" : item.Grupos,
                                Returnsdetailssuscribesites = item.SitiosSuscribe,
                                Returnsdetailsalias = item.Alias == null ? "" : item.Alias,
                                Returnsdetailsgroup = item.Grupo == null ? "" : item.Grupo,
                                Returnsdetailssimisservitron = item.SimPropiedadServitron,
                                Returnsdetailsteamvoxliteopen = item.TeamVoxLiteAbierto,
                                Returnsdetailsteamvoxsecuremode = item.TeamVoxModoSeguro,
                                Returnsdetailsevidencelite = item.EvidenceLite,
                                Returnsdetailsevidenceforms = item.EvidenceForms,
                                Returnsdetailszaypher = item.Zaypher,
                                Returnsdetailsteamvoxdispatch = item.TeamVoxDispatch,
                                Returnsdetailsobservations = item.Observaciones == null ? "" : item.Observaciones,
                                Returnsdetailscarrierid = item.CarrierId,
                                Returnsdetailsplanid = item.PlanId,
                                Returnsdetailsdialnumber = item.Dn == null ? "" : item.Dn,
                                Returnsdetailskeyboard = item.Teclado,
                                Returnsdetailsusbconector = item.ConectorUSB
                            };

                            dRS.Add(returnsdetail);

                            contador++;
                        }


                        dRS.SaveChanges();

                        drsTran.Commit();

                    }
                    catch (Exception ex)
                    {
                        drsTran.Rollback();
                        throw new ApplicationException(ex.Message);
                    }
                }

                return devolucion;
            }
        }

        public bool CreateDetails(DetallesDevolucion item, long claveDevolucion)
        {
            throw new NotImplementedException();
        }

        public bool isSerialCompatibleWithClientId(long tipoProducto, long clientId, string Serial)
        {
            using(DRSContext dRS = new DRSContext())
            {
                if(dRS.Productclients.Where(a => a.Clientid == clientId && a.Productid == tipoProducto && a.Serie == Serial).Count() == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public List<ProductosSerial> GetProductsByClientIdAndProductId(long productId, long clientId)
        {
            using (DRSContext dRS = new DRSContext())
            {

                var numbers =  (from a in dRS.Products
                        join b in dRS.Productclients on a.Productid equals b.Productid
                        select new ProductosSerial
                        {
                            ProductId = a.Productid,
                            Serial = b.Productclientid.ToString(),
                            SerialDescription = b.Serie,
                            ClientId = b.Clientid,
                            SIM = b.Sim,
                            Dn = b.Number
                        }).Where(a => a.ClientId == clientId && a.ProductId == productId).ToList();
                return numbers;
            }
        }

        public List<EstatusDevolucion> GetStatus()
        {
            using (DRSContext dRS = new DRSContext())
            {

                return (from a in dRS.Returnstatuses
                        select new EstatusDevolucion
                        {
                            EstatusDevolucionId = a.Returnstatusid,
                            EstatusDevolucionNombre = a.Returnstatusname
                        }).ToList();
            }
        }

        public List<Accesorios> GetAccessories(string ce_ui)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Accessories
                        where a.Accessoriesunit == ce_ui
                        select new Accesorios
                        {
                            IdAccesorio = a.Accessoriesid,
                            NombreAccesorio = a.Accessoriescode + " - " + a.Accessoriesname
                        }).ToList();
            }
        }

        public List<Accesorios> GetAccessoriesAll()
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Accessories
                        select new Accesorios
                        {
                            IdAccesorio = a.Accessoriesid,
                            NombreAccesorio = a.Accessoriescode + " - " + a.Accessoriesname
                        }).ToList();
            }
        }

        public List<Productos> GetProducts()
        {
            using(var drs = new DRSContext())
            {
                return (from a in drs.Products
                        select new Productos
                        {
                            ProductoId = a.Productid,
                            ProductCode = a.Productcode,
                            ProductName = a.Productname,
                            ProductTechnology = a.Producttechnology
                        }).ToList();
            }
        }

        public List<Entities.Carrier> GetCarriers()
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Carriers
                        select new Entities.Carrier
                        {
                            CarrierId = a.Carrierid,
                            CarrierName = a.Carriername
                        }).ToList();
            }
        }

        public List<PlanData> GetPlanDatas()
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Plans
                        select new Entities.PlanData
                        {
                            PlanDataId = a.Planid,
                            PlanDataName = a.Planname
                        }).ToList();
            }
        }

        public Devolucion GetReturnById(long returnId)
        {
            Devolucion devolucion = new Devolucion();

            using (DRSContext dRS = new DRSContext())
            {
                var dev = dRS.Returns.Where(a => a.Returnsid == returnId).FirstOrDefault();

                devolucion = (from a in dRS.Returns
                              where a.Returnsid == returnId
                            select new Entities.Devolucion
                            {
                                Cliente = a.Clientid,
                                Envio = a.Returnssendedfor,
                                MotivoEnvio = a.Reasonsendid,
                                Cotizar = a.Returnsquoterepair,
                                Descripcion = a.Returnsdescription,
                                NumeroGuia = a.Returnsguidenumber,
                                NombreMensajeria = a.Returnsmessagername,
                                DestinoCC = a.Returnscc,
                                Estatus = a.Returnstatusid,
                                FechaCreacion = a.Returnscreatedate,
                                EmpleadoResponsable = a.Employeeid,
                                ClaveDevolucion = a.Returnsid,
                                UserId = a.Usersid
                            }).FirstOrDefault();

                if(devolucion != null)
                {
                    devolucion.Comentarios = (from b in dRS.Commentsreturns
                                              join bb in dRS.Users on b.Usersid equals bb.Usersid
                                              join bbb in dRS.People on bb.Personid equals bbb.Personid
                                              where b.Returnsid == returnId
                                              select new Entities.Comentarios
                                              {
                                                  ComentariosId = b.Commentsreturnsid,
                                                  Descripcion = b.Commentsreturnsdescription,
                                                  FechaCreación = b.Commentsreturnscreatedate,
                                                  UsuarioId = b.Usersid,
                                                  PersonName = bbb.Personname + " " + bbb.Personlastname 
                                              }
                                              ).ToList();

                    devolucion.Details = (from c in dRS.Returnsdetails
                                          where c.Returnsid == returnId
                                          select new Entities.DetallesDevolucion
                                          {
                                              ExisteProducto = c.Returnsisservitron ,
                                              EsSoloDevolucion = c.Returnsisonlyaccesory ,
                                              TipoProducto = Convert.ToInt64(c.Productid),
                                              Producto = c.Productname ,
                                              Cantidad = c.Productquantity.ToString(),
                                              Serie = c.Productserialnumber,
                                              Antena = c.Returnsdetailsantenna,
                                              Carcasa = c.Returnsdetailscase,
                                              Display = c.Returnsdetailsdisplay ,
                                              CableUSB = c.Returnsdetailsusb ,
                                              Otro = c.Returnsdetailsother ,
                                              Tapa = c.Returnsdetailscover ,
                                              Bateria = c.Returnsdetailsbattery ,
                                              CargadorEliminador = c.Returnsdetailscharged ,
                                              CableUSBMagnetico = c.Returnsdetailsusbmagneticcable ,
                                              BaseCarga = c.Returnsdetailschargedbase ,
                                              Clip = c.Returnsdetailsclip ,
                                              Manual = c.Returnsdetailsmanual ,
                                              Caja = c.Returnsdetailsbox ,
                                              HerramientaDeExtraccion = c.Returnsdetailsextractiontool ,
                                              SIM = c.Returnssim ,
                                              Leyenda = c.Returnsdetailslegend ,
                                              NombreCarpeta = c.Returnsdetailsfoldername,
                                              IdentificadorLlamadas = c.Returnsdetailscalledidentifier ,
                                              GPS = c.Returnsdetailsgps ,
                                              LlamadaPrivada = c.Returnsdetailscallprivate ,
                                              Grupos = c.Returnsdetailsgroups ,
                                              SitiosSuscribe = c.Returnsdetailssuscribesites ,
                                              Alias = c.Returnsdetailsalias ,
                                              Grupo = c.Returnsdetailsgroup ,
                                              SimPropiedadServitron = c.Returnsdetailssimisservitron ,
                                              TeamVoxLiteAbierto = c.Returnsdetailsteamvoxliteopen ,
                                              TeamVoxModoSeguro = c.Returnsdetailsteamvoxsecuremode ,
                                              EvidenceLite = c.Returnsdetailsevidencelite ,
                                              EvidenceForms = c.Returnsdetailsevidenceforms ,
                                              Zaypher = c.Returnsdetailszaypher ,
                                              TeamVoxDispatch = c.Returnsdetailsteamvoxdispatch ,
                                              Observaciones = c.Returnsdetailsobservations ,
                                              CarrierId = Convert.ToInt32(c.Returnsdetailscarrierid) ,
                                              PlanId = Convert.ToInt32(c.Returnsdetailsplanid) ,
                                              Dn = c.Returnsdetailsdialnumber,
                                              IdDetallesDevolucion = c.Returnsdetailsid,
                                              Teclado = c.Returnsdetailskeyboard,
                                              ConectorUSB = c.Returnsdetailsusbconector
                                          }).ToList();
                }
            }

            return devolucion;
        }

        public Response Cancel(Devolucion devolucion, long userId)
        {
            Response response = new Response();

            using (DRSContext dRS = new DRSContext())
            {
                var dev = dRS.Returns.Where(a => a.Returnsid == devolucion.ClaveDevolucion).FirstOrDefault();

                if (dev != null)
                {
                    dev.Returnstatusid = 2;

                    try
                    {
                        dRS.Update(dev);

                        dRS.SaveChanges();

                        response.Codigo = 1;
                        response.Mensaje = "OK";
                    }
                    catch(Exception ex)
                    {
                        response.Codigo = 0;
                        response.Mensaje = ex.Message;
                    }
                }
                else
                {
                    response.Codigo = 0;
                    response.Mensaje = "La devolución seleccionada ya no se encuentra existente en la plataforma.";
                }
            }

            return response;
        }

        public bool AddComment(Comentarios comentarios, long returnId)
        {
            bool response = false;

            try
            {
                using (DRSContext dRS = new DRSContext())
                {
                    long valMax = 0;

                    if (dRS.Commentsreturns.Count() == 0)
                    {
                        valMax = 1;
                    }
                    else
                    {
                        valMax = dRS.Commentsreturns.Max(a => a.Commentsreturnsid) + 1;
                    }

                    Commentsreturn commentsreturn = new Commentsreturn();
                    commentsreturn.Commentsreturnscreatedate = comentarios.FechaCreación;
                    commentsreturn.Commentsreturnsdescription = comentarios.Descripcion;
                    commentsreturn.Returnsid = returnId;
                    commentsreturn.Usersid = comentarios.UsuarioId;
                    commentsreturn.Commentsreturnsid = valMax;

                    dRS.Add(commentsreturn);

                    dRS.SaveChanges();

                    response = true;
                }
            }
            catch
            {
                response = false;
            }

            return response;
        }

        public StockConfirmation GetStockConfirmation(long idDetallesDevolucion)
        {
            using(var dRS = new DRSContext())
            {
                var responseMaster = (from a in dRS.Stockconfirmations
                                      where a.Returnsdetailsid == idDetallesDevolucion
                                      select new StockConfirmation
                                      {
                                          ReturnDetailsId = a.Returnsdetailsid,
                                          StatusId = a.Stockconfirmationstatusid,
                                          StockDetails = new List<StockConfirmationDetails>(),
                                          StockId = a.Stockconfirmationid
                                      }).FirstOrDefault();

                if(responseMaster == null)
                {
                    responseMaster = null;
                }
                else
                {
                    responseMaster.StockDetails = (from a in dRS.Stockconfirmationdetails
                                                   where a.Stockconfirmationid == responseMaster.StockId
                                                   select new StockConfirmationDetails
                                                   {
                                                       StockConfirmationComments = a.Stockconfirmationcomments,
                                                       StockConfirmationDetailsCreateDate = a.Stockconfirmationcreatedate,
                                                       StockConfirmationDetailsId = a.Stockconfirmationdetailsid,
                                                       StockConfirmationId = a.Stockconfirmationid,
                                                       StockConfirmationQuantity = a.Stockconfirmationquantity.ToString(),
                                                       UserId = a.Usersid
                                                   }).ToList();
                }

                return responseMaster;
            }
        }

        public void modifyStatusReturn(Devolucion returnExistent)
        {
            using (DRSContext dRS = new DRSContext())
            {
                var getReturn = dRS.Returns.Where(a => a.Returnsid == returnExistent.ClaveDevolucion).FirstOrDefault();

                using (var drsTran = dRS.Database.BeginTransaction())
                {
                    try
                    {
                        //Añadimos la devolución
                        getReturn.Returnstatusid = returnExistent.Estatus;

                        dRS.Update(getReturn);

                        dRS.SaveChanges();

                        drsTran.Commit();

                    }
                    catch (Exception ex)
                    {
                        drsTran.Rollback();
                        throw new ApplicationException(ex.Message);
                    }
                }
            }
        }

        public void CreateConfirmStock(StockConfirmationDetails details, long productId, bool existConfirm, int confirmExists)
        {
            try
            {
                using (var dRS = new DRSContext())
                {
                    Stockconfirmation stockconfirmation = new Stockconfirmation();

                    if (existConfirm)
                    {
                        stockconfirmation = dRS.Stockconfirmations.Where(a => a.Returnsdetailsid == productId).FirstOrDefault();

                        if(confirmExists == 1)
                        {
                            stockconfirmation.Stockconfirmationstatusid = 1;
                        }
                        else
                        {
                            if(stockconfirmation.Stockconfirmationstatusid == 2)
                            {
                                stockconfirmation.Stockconfirmationstatusid = 3;
                            }
                            else
                            {
                                stockconfirmation.Stockconfirmationstatusid = 2;
                            }
                        }

                        dRS.Update(stockconfirmation);

                        dRS.SaveChanges();
                    }
                    else
                    {
                        if (confirmExists == 1)
                        {
                            stockconfirmation.Stockconfirmationstatusid = 1;
                        }
                        else
                        {
                            stockconfirmation.Stockconfirmationstatusid = 2;
                        }

                        stockconfirmation.Returnsdetailsid = productId;

                        long maxValue = 1;

                        if(dRS.Stockconfirmations.Count() > 0)
                        {
                            maxValue = dRS.Stockconfirmations.Max(a => a.Stockconfirmationid) + 1;
                        }

                        stockconfirmation.Stockconfirmationid = maxValue;

                        dRS.Add(stockconfirmation);

                        dRS.SaveChanges();

                    }

                    //Insertamos los detalles//
                    long maxValueDetails = 1;

                    if (dRS.Stockconfirmationdetails.Count() > 0)
                    {
                        maxValueDetails = dRS.Stockconfirmationdetails.Max(a => a.Stockconfirmationdetailsid) + 1;
                    }

                    Stockconfirmationdetail stockconfirmationdetail = new Stockconfirmationdetail()
                    {
                        Stockconfirmationcreatedate = details.StockConfirmationDetailsCreateDate,
                        Stockconfirmationcomments = details.StockConfirmationComments == null ? "" : details.StockConfirmationComments,
                        Stockconfirmationid = stockconfirmation.Stockconfirmationid,
                        Stockconfirmationdetailsid = maxValueDetails,
                        Stockconfirmationquantity = Convert.ToInt32(details.StockConfirmationQuantity),
                        Usersid = details.UserId
                    };

                    dRS.Add(stockconfirmationdetail);

                    dRS.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public EstatusDetalles getStatusReturnConfirmation(int statusId)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Stockconfirmationstatuses
                        where a.Stockconfirmationstatusid == statusId
                        select new EstatusDetalles
                        {
                            EstatusDetallesId = a.Stockconfirmationstatusid,
                            EstatusDetallesNombre = a.Stockconfirmationstatusname
                        }).FirstOrDefault();
            }
        }

        public bool updateStatusConfirmation(List<DetallesDevolucion> dev)
        {
            try
            {
                foreach (var item in dev)
                {
                    using(var drs = new DRSContext())
                    {
                        var response = drs.Stockconfirmations.Where(a => a.Stockconfirmationid == item.StockConfirmation.StockId).FirstOrDefault(); 

                        if(response != null)
                        {
                            response.Stockconfirmationstatusid = 5;

                            drs.Update(response);
                            drs.SaveChanges();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            

            return true;
        }

        public long createConfirmStock(List<StockConfirmDetails> stockConfirmDetails, long returnId = 0)
        {
            long stockConfirmId = 0;

            using (var drs = new DRSContext())
            {
                using (var drsTran = drs.Database.BeginTransaction())
                {
                    try
                    {
                        stockConfirmId = 1;

                        Stockconfirm stockconfirm = new Stockconfirm()
                        {
                            Stockconfirmcreatedate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                            Stockconfirmsapcode = String.Empty,
                            Stockconfirmsapuuid = String.Empty,
                            Returnid = returnId,
                            //Stockconfirmid = stockConfirmId,
                            Stockconfirmstatusid = 2
                        };

                        drs.Add(stockconfirm);
                        drs.SaveChanges();

                        //long stockConfirmDetailsId = 1;

                        foreach (var item in stockConfirmDetails)
                        {
                            //if (drs.Stockconfirmdetails.Count() > 0)
                            //{
                            //    stockConfirmDetailsId = drs.Stockconfirmdetails.Max(a => a.Stockconfirmdetailsid) + 1;
                            //}

                            if (drs.Stockconfirms.Count() > 0)
                            {
                                stockConfirmId = drs.Stockconfirms.Max(a => a.Stockconfirmid);
                            }

                            Stockconfirmdetail stockconfirmdetail = new Stockconfirmdetail()
                            {
                                Productid = Convert.ToInt32(item.ProductId),
                                Returndetailsid = item.ReturnDetailsId,
                                Stockconfirmationid = item.StockConfirmationid,
                                //Stockconfirmdetailsid = stockConfirmDetailsId,
                                Stockconfirmid = stockConfirmId,
                                Stockconfirmorderid = item.StockConfirmOrderId,
                                Stockconfirmationdetailsid = item.StockConfirmationDetailsId
                            };

                            drs.Add(stockconfirmdetail);
                            drs.SaveChanges();
                        }

                        drsTran.Commit();

                    }
                    catch (Exception ex)
                    {
                        drsTran.Rollback();
                        throw new ApplicationException(ex.Message);
                    }
                }

            }

            return stockConfirmId;
        }

        public StockConfirm GetStockConfirm(long stockConfirmId)
        {
            StockConfirm stockConfirm = new StockConfirm();

            using(var drs = new DRSContext())
            {
                stockConfirm = (from a in drs.Stockconfirms
                                where a.Stockconfirmid == stockConfirmId
                                select new Entities.StockConfirm
                                {
                                    ReturnId = a.Returnid,
                                    StatusConfirmStatusId = a.Stockconfirmstatusid,
                                    StockConfirmCreateDate = a.Stockconfirmcreatedate,
                                    StockConfirmId = a.Stockconfirmid,
                                    StockConfirmSapCode = a.Stockconfirmsapcode,
                                    StockConfirmSaUuid = a.Stockconfirmsapuuid
                                }).FirstOrDefault();

                if(stockConfirm != null)
                {
                    stockConfirm.Details = (from a in drs.Stockconfirmdetails
                                            where a.Stockconfirmid == stockConfirmId
                                            select new Entities.StockConfirmDetails
                                            {
                                                ProductId = a.Productid,
                                                ReturnDetailsId = a.Returndetailsid,
                                                StockConfirmationid = a.Stockconfirmationid,
                                                StockConfirmDetailsId = a.Stockconfirmdetailsid,
                                                StockConfirmId = a.Stockconfirmid,
                                                StockConfirmOrderId = a.Stockconfirmorderid,
                                                StockConfirmationDetailsId = a.Stockconfirmationdetailsid
                                            }).ToList();
                }
            }

            return stockConfirm;
        }

        public List<StockConfirm> GetAllStockConfirm()
        {
            List<StockConfirm> stockConfirm = new List<StockConfirm>();

            using (var drs = new DRSContext())
            {
                stockConfirm = (from a in drs.Stockconfirms
                                select new Entities.StockConfirm
                                {
                                    ReturnId = a.Returnid,
                                    StatusConfirmStatusId = a.Stockconfirmstatusid,
                                    StockConfirmCreateDate = a.Stockconfirmcreatedate,
                                    StockConfirmId = a.Stockconfirmid,
                                    StockConfirmSapCode = a.Stockconfirmsapcode,
                                    StockConfirmSaUuid = a.Stockconfirmsapuuid
                                }).ToList();

                if (stockConfirm.Count() > 0)
                {
                    for(int i = 0; i<stockConfirm.Count(); i++)
                    {
                        stockConfirm[i].Details = (from a in drs.Stockconfirmdetails
                                                where a.Stockconfirmid == stockConfirm[i].StockConfirmId
                                                   select new Entities.StockConfirmDetails
                                                {
                                                    ProductId = a.Productid,
                                                    ReturnDetailsId = a.Returndetailsid,
                                                    StockConfirmationid = a.Stockconfirmationid,
                                                    StockConfirmDetailsId = a.Stockconfirmdetailsid,
                                                    StockConfirmId = a.Stockconfirmid,
                                                    StockConfirmOrderId = a.Stockconfirmorderid
                                                }).ToList();
                    }
                }
            }

            return stockConfirm;
        }

        public void UpdateConfirmStock(StockConfirm stockConfirm)
        {
            using(var drs = new DRSContext())
            {
                var getbyId = drs.Stockconfirms.Where(a => a.Stockconfirmid == stockConfirm.StockConfirmId).FirstOrDefault();

                if(getbyId != null)
                {
                    if(getbyId.Stockconfirmstatusid != stockConfirm.StatusConfirmStatusId)
                    {
                        getbyId.Stockconfirmstatusid = stockConfirm.StatusConfirmStatusId;
                    }

                    if(getbyId.Stockconfirmsapcode != stockConfirm.StockConfirmSapCode)
                    {
                        getbyId.Stockconfirmsapcode = stockConfirm.StockConfirmSapCode;
                    }

                    if (getbyId.Stockconfirmsapuuid != stockConfirm.StockConfirmSaUuid)
                    {
                        getbyId.Stockconfirmsapuuid = stockConfirm.StockConfirmSaUuid;
                    }

                    drs.Update(getbyId);

                    drs.SaveChanges();
                }
            }
        }

        public void DeleteConfirmStock(long stockConfirmId)
        {
            using (var drs = new DRSContext())
            {
                var getbyId = drs.Stockconfirms.Where(a => a.Stockconfirmid == stockConfirmId).FirstOrDefault();

                if (getbyId != null)
                {
                    var getByDetailsId = drs.Stockconfirmdetails.Where(a => a.Stockconfirmid == stockConfirmId).ToList();

                    foreach(var item in getByDetailsId)
                    {
                        drs.Remove(item);
                    }

                    drs.SaveChanges();

                    drs.Remove(getbyId);

                    drs.SaveChanges();
                }
            }
        }

        public bool ExistsProductInAnotherReturn(long tipoProducto, string serie, long idReturn)
        {
            bool isExists = false;

            using(var drs = new DRSContext())
            {
                var getReturnDetails = drs.Returnsdetails.Where(a => a.Productid == tipoProducto && a.Productserialnumber == serie && a.Returnsid != idReturn).ToList();

                foreach(var item in getReturnDetails)
                {
                    var getReturnId = drs.Returns.Where(a => a.Returnsid == item.Returnsid).FirstOrDefault();

                    if(getReturnId != null)
                    {
                        if(getReturnId.Returnstatusid == 2)
                        {
                            isExists = false;
                        }
                        else
                        {
                            isExists = true;
                        }
                    }
                    else
                    {
                        isExists = false;
                    }
                }
            }

            return isExists;
        }

        public List<StockConfirm> GetStockConfirmByReturnId(long returnId)
        {
            List<StockConfirm> stockConfirm = new List<StockConfirm>();

            int contador = 0;

            using (var drs = new DRSContext())
            {
                stockConfirm = (from a in drs.Stockconfirms
                                where a.Returnid == returnId
                                select new Entities.StockConfirm
                                {
                                    ReturnId = a.Returnid,
                                    StatusConfirmStatusId = a.Stockconfirmstatusid,
                                    StockConfirmCreateDate = a.Stockconfirmcreatedate,
                                    StockConfirmId = a.Stockconfirmid,
                                    StockConfirmSapCode = a.Stockconfirmsapcode,
                                    StockConfirmSaUuid = a.Stockconfirmsapuuid
                                }).ToList();

                foreach(var item in stockConfirm)
                {
                    item.Details = (from a in drs.Stockconfirmdetails
                                    where a.Stockconfirmid == item.StockConfirmId
                                    select new Entities.StockConfirmDetails
                                    {
                                        ProductId = a.Productid,
                                        ReturnDetailsId = a.Returndetailsid,
                                        StockConfirmationid = a.Stockconfirmationid,
                                        StockConfirmDetailsId = a.Stockconfirmdetailsid,
                                        StockConfirmId = a.Stockconfirmid,
                                        StockConfirmOrderId = a.Stockconfirmorderid
                                    }).ToList();

                    stockConfirm[contador].Details = item.Details;

                    contador++;
                }
            }

            return stockConfirm;
        }

        public void UpdateConfirmation(List<StockConfirmDetails> details)
        {
            using (var drs = new DRSContext())
            {
                foreach(var item in details)
                {
                    var response = drs.Stockconfirmations.Where(a => a.Stockconfirmationid == item.StockConfirmationid).FirstOrDefault();

                    if(response != null)
                    {
                        response.Stockconfirmationstatusid = 4;

                        drs.Update(response);

                        drs.SaveChanges();

                        foreach (var itemDeleteDetail in drs.Stockconfirmationdetails.Where(a => a.Stockconfirmationid == item.StockConfirmationid && a.Stockconfirmationdetailsid == item.StockConfirmationDetailsId && a.Stockconfirmationcomments != "-1").OrderByDescending(a => a.Stockconfirmationid).Take(1).ToList())
                        {
                            var itemNew = itemDeleteDetail;

                            itemDeleteDetail.Stockconfirmationcomments = "-1";
                            //itemDeleteDetail.Stockconfirmationcreatedate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now);

                            drs.Update(itemDeleteDetail);

                            drs.SaveChanges();

                            itemNew.Stockconfirmationdetailsid = 1;

                            if (drs.Stockconfirmationdetails.Count() > 0)
                            {
                                itemNew.Stockconfirmationdetailsid = drs.Stockconfirmationdetails.Max(a => a.Stockconfirmationdetailsid) + 1;
                            }

                            itemNew.Stockconfirmationquantity = 0;
                            itemNew.Stockconfirmationcomments = "La confirmación ha sido anulada.";
                            itemNew.Stockconfirmationcreatedate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now);

                            drs.Add(itemNew);

                            drs.SaveChanges();
                        }
                    }

                }

                
            }
        }

        public List<StockConfirm> GetConfirmsSap()
        {
            List<StockConfirm> stockConfirm = new List<StockConfirm>();

            int contador = 0;

            using (var drs = new DRSContext())
            {
                stockConfirm = (from a in drs.Stockconfirms
                                select new Entities.StockConfirm
                                {
                                    ReturnId = a.Returnid,
                                    StatusConfirmStatusId = a.Stockconfirmstatusid,
                                    StockConfirmCreateDate = a.Stockconfirmcreatedate,
                                    StockConfirmId = a.Stockconfirmid,
                                    StockConfirmSapCode = a.Stockconfirmsapcode,
                                    StockConfirmSaUuid = a.Stockconfirmsapuuid
                                }).ToList();

                foreach (var item in stockConfirm)
                {
                    item.Details = (from a in drs.Stockconfirmdetails
                                    where a.Stockconfirmid == item.StockConfirmId
                                    select new Entities.StockConfirmDetails
                                    {
                                        ProductId = a.Productid,
                                        ReturnDetailsId = a.Returndetailsid,
                                        StockConfirmationid = a.Stockconfirmationid,
                                        StockConfirmDetailsId = a.Stockconfirmdetailsid,
                                        StockConfirmId = a.Stockconfirmid,
                                        StockConfirmOrderId = a.Stockconfirmorderid
                                    }).ToList();

                    stockConfirm[contador].Details = item.Details;

                    contador++;
                }
            }

            return stockConfirm;
        }
    }
}
