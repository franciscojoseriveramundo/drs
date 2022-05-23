using DRS.Business.Extensions;
using DRS.Business.Functions;
using DRS.Business.RestClientA;
using DRS.Data;
using DRS.Entities;
using DRS.Entities.Rest;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using SysSvcmod = System.ServiceModel.Description;

namespace DRS.Business
{
    public class DevolucionesBus
    {
        private readonly IDevolucionesDA iDevolucionesDA;
        private readonly IUserDA iUserDA;
        private readonly ILogDA iLogDA;
        private readonly IEmailDA iemailDA;
        private IConfiguration iConfiguration;
        private readonly IPermissionDA ipermissionDA;

        public DevolucionesBus(IDevolucionesDA iDevolucionesDA, IUserDA iUserDA, ILogDA iLogDA, IEmailDA iemailDA, IPermissionDA ipermissionDA)
        {
            this.iDevolucionesDA = iDevolucionesDA;
            this.iUserDA = iUserDA;
            this.iLogDA = iLogDA;
            this.iemailDA = iemailDA;
            this.ipermissionDA = ipermissionDA;
            this.iConfiguration = ConectionDB.Configuration;
        }
        public List<SelectListItem> GetMotivoEnvioSlist(List<MotivoEnvio> lst)
        {
            //Create a list of select list items - this will be returned as your select list
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el motivo de envío.", Value = "0", Selected = true });

            foreach (var item in lst)
            {
                listItem.Add(
                    new SelectListItem()
                    {
                        Text = item.motivoenvionombre,
                        Value = item.motivoenvioid.ToString(),
                        Selected = false
                    });
            }

            return listItem;
        }
        public List<MotivoEnvio> GetMotivoEnvio()
        {
            return iDevolucionesDA.GetMotivoEnvio();
        }
        public List<SelectListItem> GetEstatus()
        {
            //Create a list of select list items - this will be returned as your select list
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el estatus.", Value = "0", Selected = true });

            foreach (var item in iDevolucionesDA.GetStatus())
            {
                listItem.Add(
                    new SelectListItem()
                    {
                        Text = item.EstatusDevolucionNombre,
                        Value = item.EstatusDevolucionId.ToString(),
                        Selected = false
                    });
            }

            return listItem;
        }
        public List<SelectListItem> GetEmployeeFromSap(List<PersonEmployee> personEmployees)
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el empleado.", Value = "0", Selected = true });

            foreach (var item in personEmployees)
            {
                listItem.Add(
                    new SelectListItem()
                    {
                        Text = item.CeeUui.ToString() + " - " + item.TeeUi.ToString(),
                        Value = item.EmployeeId.ToString(),
                        Selected = false
                    });
            }

            return listItem;
        }
        public List<EmpleadosPersona> GetEmployeeByPersonId(Int64 personId = 0)
        {
            return iUserDA.GetEmployeeByPersonId(personId);
        }
        public List<SelectListItem> GetClientesAll(List<Clientes> clientes)
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el cliente.", Value = "0", Selected = true });

            foreach (var item in clientes.ToList())
            {
                listItem.Add(
                    new SelectListItem()
                    {
                        Text = item.ClaveCliente.ToString() + " - " + item.NombreCliente.ToString(),
                        Value = item.IdCliente.ToString(),
                        Selected = false
                    });
            }

            return listItem;
        }
        public List<SelectListItem> GetClientes(List<Clientes> clientes)
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el cliente.", Value = "0", Selected = true });

            foreach (var item in clientes.Where(a => a.ClienteEstatus == "2").OrderBy(a => a.NombreCliente).ToList())
            {
                listItem.Add(
                    new SelectListItem()
                    {
                        Text = item.ClaveCliente.ToString() + " - " + item.NombreCliente.ToString(),
                        Value = item.IdCliente.ToString(),
                        Selected = false
                    });
            }

            return listItem;
        }
        public List<Devoluciones> GetDevoluciones(Int64 clientId = 0, int motivoEnvio = 0, int Status = 0, Int64 personId = 0)
        {
            List<Devoluciones> devoluciones = new List<Devoluciones>();

            //Obtenemos las devoluciones que esten en confirmacion sap.
            foreach (var item in iDevolucionesDA.GetConfirmsSap().Where(a => a.StatusConfirmStatusId == 1))
            {
                //Validamos que la devolucion este en el stockconfirmation//
                List<StockConfirm> getStockConfirm = iDevolucionesDA.GetStockConfirmByReturnId(item.ReturnId);

                if (getStockConfirm.Where(a => a.StatusConfirmStatusId == 1).ToList().Count() > 0)
                {
                    GetStockConfirm(getStockConfirm.Where(a => a.StatusConfirmStatusId == 1).ToList(), item.ReturnId);
                }
            }

            List<DevolucionesViewModel> devolucionesTotal = iDevolucionesDA.GetDevoluciones();

            //Obtenemos los clientes a los que tiene acceso el usuario
            var clientes = iUserDA.GetClients(personId);

            //Validamos el tipo de usuario//
            UserRole user = iUserDA.GetUsersForByPersonId(personId);

            List<Actions> actions = ipermissionDA.GetActionsByIdUserAndMenuId(user.UserRoleId, 1);

            if (actions.Where(a => a.Actionsid == 2).Count() > 0)
            {
                //Interamos todas las devoluciones
                foreach (var item in devolucionesTotal)
                {
                    //Si el cliente se encuentra dentro de la coleccion lo mostrará en pantalla
                    //Aplicamos los fitros de busqueda.
                    if (clientId == 0 && motivoEnvio == 0)
                    {
                        devoluciones.Add(
                            new Devoluciones()
                            {
                                Cliente = item.Clientname,
                                EmpleadoResponsable = item.Employeeassigned,
                                EnviadoPor = item.Returnssendedfor,
                                Estatus = item.Returnstatusname,
                                MotivoEnvio = item.Reasonsendname,
                                NoInterno = item.Returnsid.ToString(),
                                MotivoEnvioId = item.Reasonsendid,
                                ClienteId = item.Clientid
                            });
                    } else if (clientId != 0 && motivoEnvio == 0)
                    {
                        if (item.Clientid == clientId)
                        {
                            devoluciones.Add(
                                new Devoluciones()
                                {
                                    Cliente = item.Clientname,
                                    EmpleadoResponsable = item.Employeeassigned,
                                    EnviadoPor = item.Returnssendedfor,
                                    Estatus = item.Returnstatusname,
                                    MotivoEnvio = item.Reasonsendname,
                                    NoInterno = item.Returnsid.ToString(),
                                    MotivoEnvioId = item.Reasonsendid,
                                    ClienteId = item.Clientid
                                });
                        }
                    } else if (clientId == 0 && motivoEnvio != 0)
                    {
                        if (item.Reasonsendid == motivoEnvio)
                        {
                            devoluciones.Add(
                                new Devoluciones()
                                {
                                    Cliente = item.Clientname,
                                    EmpleadoResponsable = item.Employeeassigned,
                                    EnviadoPor = item.Returnssendedfor,
                                    Estatus = item.Returnstatusname,
                                    MotivoEnvio = item.Reasonsendname,
                                    NoInterno = item.Returnsid.ToString(),
                                    MotivoEnvioId = item.Reasonsendid,
                                    ClienteId = item.Clientid
                                });
                        }
                    } else
                    {
                        if (item.Reasonsendid == motivoEnvio && item.Clientid == clientId)
                        {
                            devoluciones.Add(
                                new Devoluciones()
                                {
                                    Cliente = item.Clientname,
                                    EmpleadoResponsable = item.Employeeassigned,
                                    EnviadoPor = item.Returnssendedfor,
                                    Estatus = item.Returnstatusname,
                                    MotivoEnvio = item.Reasonsendname,
                                    NoInterno = item.Returnsid.ToString(),
                                    MotivoEnvioId = item.Reasonsendid,
                                    ClienteId = item.Clientid
                                });
                        }
                    }
                }
            } else
            {
                //Interamos todas las devoluciones
                foreach (var item in devolucionesTotal)
                {
                    //Iteramos los clientes a los que tiene acceso.
                    foreach (var itemClient in clientes)
                    {
                        //Si el cliente se encuentra dentro de la coleccion lo mostrará en pantalla
                        if (itemClient.IdCliente == item.Clientid)
                        {
                            //Aplicamos los fitros de busqueda.
                            if (clientId == 0 && motivoEnvio == 0 && devoluciones.Where(a => a.NoInterno == item.Returnsid.ToString()).Count() == 0)
                            {
                                devoluciones.Add(
                                    new Devoluciones()
                                    {
                                        Cliente = item.Clientname,
                                        EmpleadoResponsable = item.Employeeassigned,
                                        EnviadoPor = item.Returnssendedfor,
                                        Estatus = item.Returnstatusname,
                                        MotivoEnvio = item.Reasonsendname,
                                        NoInterno = item.Returnsid.ToString(),
                                        MotivoEnvioId = item.Reasonsendid,
                                        ClienteId = item.Clientid
                                    });
                            } 
                            else if (clientId != 0 && motivoEnvio == 0 && devoluciones.Where(a => a.NoInterno == item.Returnsid.ToString()).Count() == 0)
                            {
                                if (item.Clientid == clientId)
                                {
                                    devoluciones.Add(
                                        new Devoluciones()
                                        {
                                            Cliente = item.Clientname,
                                            EmpleadoResponsable = item.Employeeassigned,
                                            EnviadoPor = item.Returnssendedfor,
                                            Estatus = item.Returnstatusname,
                                            MotivoEnvio = item.Reasonsendname,
                                            NoInterno = item.Returnsid.ToString(),
                                            MotivoEnvioId = item.Reasonsendid,
                                            ClienteId = item.Clientid
                                        });
                                }
                            } 
                            else if (clientId == 0 && motivoEnvio != 0 && devoluciones.Where(a => a.NoInterno == item.Returnsid.ToString()).Count() == 0)
                            {
                                if (item.Reasonsendid == motivoEnvio)
                                {
                                    devoluciones.Add(
                                        new Devoluciones()
                                        {
                                            Cliente = item.Clientname,
                                            EmpleadoResponsable = item.Employeeassigned,
                                            EnviadoPor = item.Returnssendedfor,
                                            Estatus = item.Returnstatusname,
                                            MotivoEnvio = item.Reasonsendname,
                                            NoInterno = item.Returnsid.ToString(),
                                            MotivoEnvioId = item.Reasonsendid,
                                            ClienteId = item.Clientid
                                        });
                                }
                            } 
                            else
                            {
                                if (item.Reasonsendid == motivoEnvio && item.Clientid == clientId && devoluciones.Where(a => a.NoInterno == item.Returnsid.ToString()).Count() == 0)
                                {
                                    devoluciones.Add(
                                        new Devoluciones()
                                        {
                                            Cliente = item.Clientname,
                                            EmpleadoResponsable = item.Employeeassigned,
                                            EnviadoPor = item.Returnssendedfor,
                                            Estatus = item.Returnstatusname,
                                            MotivoEnvio = item.Reasonsendname,
                                            NoInterno = item.Returnsid.ToString(),
                                            MotivoEnvioId = item.Reasonsendid,
                                            ClienteId = item.Clientid
                                        });
                                }
                            }
                        }
                    }
                }
            }

            EstatusDevolucion estatusDev = iDevolucionesDA.GetStatus()
                .Where(a => a.EstatusDevolucionId == Status)
                .FirstOrDefault();

            if (Status != 0)
            {
                return devoluciones.Where(a => a.Estatus == estatusDev.EstatusDevolucionNombre).ToList();
            }

            return devoluciones;
        }
        public bool NewProduct(DetallesDevolucion detalleDevolucion, Int64 clientId, List<DetallesDevolucion> lst, long returnId)
        {
            bool resultado = true;

            if (detalleDevolucion.ExisteProducto == true && detalleDevolucion.EsSoloDevolucion == true)
            {
                if (detalleDevolucion.TipoProducto == 0)
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
            } else if (detalleDevolucion.ExisteProducto == true && detalleDevolucion.EsSoloDevolucion == false)
            {
                if (detalleDevolucion.TipoProducto == 0)
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }

                if (detalleDevolucion.Serie == "0" || string.IsNullOrEmpty(detalleDevolucion.Serie))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                } else
                {
                    bool isSerialCompatibleWithClientId = iDevolucionesDA.isSerialCompatibleWithClientId(
                        detalleDevolucion.TipoProducto,
                        clientId,
                        detalleDevolucion.Serie);

                    if (!isSerialCompatibleWithClientId)
                    {
                        throw new ApplicationException("* El número de serie no se encuentra registrado.");
                    } 
                    else
                    {
                        if (lst.Where(
                            a => a.TipoProducto == detalleDevolucion.TipoProducto && a.Serie == detalleDevolucion.Serie)
                            .Any())
                        {
                            throw new ApplicationException("* El número de serie ya esta registrado en la devolución.");
                        }
                        else
                        {
                            //Validación que verifica que el numero de serie no este en otra devolución distinta a esta.
                            bool isExists = iDevolucionesDA.ExistsProductInAnotherReturn(detalleDevolucion.TipoProducto, detalleDevolucion.Serie, returnId);

                            if(isExists == true)
                            {
                                throw new ApplicationException("* El número de serie ya se encuentra registrado en otra devolución.");
                            }
                        }
                    }
                }

                if (iDevolucionesDA.GetProducts().Where(a => a.ProductoId == detalleDevolucion.TipoProducto).FirstOrDefault().ProductTechnology.ToUpper() == "TEAMVOX")
                {
                    if (string.IsNullOrEmpty(detalleDevolucion.SIM))
                    {
                        throw new ApplicationException("* Complete todos los campos obligatorios.");
                    }

                    if (string.IsNullOrEmpty(detalleDevolucion.Dn))
                    {
                        throw new ApplicationException("* Complete todos los campos obligatorios.");
                    }

                    //Validamos los formatos de TeamVox y Legacy//
                    if (detalleDevolucion.MotivoEnvio == 12)
                    {
                        if (string.IsNullOrEmpty(detalleDevolucion.Alias))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.Grupo))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.CarrierId.ToString()) || detalleDevolucion.CarrierId == 0)
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.PlanId.ToString()) || detalleDevolucion.PlanId == 0)
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }


                        if (string.IsNullOrEmpty(detalleDevolucion.Observaciones))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }
                    }
                }
                else
                {
                    //Validamos los formatos de TeamVox y Legacy//
                    if (detalleDevolucion.MotivoEnvio == 12)
                    {
                        if (string.IsNullOrEmpty(detalleDevolucion.Leyenda))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.NombreCarpeta))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.Grupos))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.Observaciones))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }
                    }
                }
            } else if (detalleDevolucion.ExisteProducto == false && detalleDevolucion.EsSoloDevolucion == true)
            {
                if (string.IsNullOrEmpty(detalleDevolucion.Producto))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
            } else
            {
                if (string.IsNullOrEmpty(detalleDevolucion.Producto))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }

                if (detalleDevolucion.Serie == "0" || string.IsNullOrEmpty(detalleDevolucion.Serie))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
            }

            if (string.IsNullOrEmpty(detalleDevolucion.Cantidad))
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            } else
            {
                if (new IsNumberOrNot().Validate(detalleDevolucion.Cantidad))
                {
                    throw new ApplicationException("* La cantidad debe de ser un número entero.");
                } else
                {
                    if (Convert.ToInt32(detalleDevolucion.Cantidad) == 0)
                    {
                        throw new ApplicationException("* La cantidad debe de ser un valor mayor a cero.");
                    }
                }
            }

            if (detalleDevolucion.Antena == 0 ||
                detalleDevolucion.Carcasa == 0 ||
                detalleDevolucion.Display == 0 ||
                detalleDevolucion.Teclado == 0 ||
                detalleDevolucion.ConectorUSB == 0 ||
                detalleDevolucion.Tapa == 0 ||
                detalleDevolucion.Bateria == 0 ||
                detalleDevolucion.CargadorEliminador == 0 ||
                detalleDevolucion.CableUSB == 0 ||
                detalleDevolucion.CableUSBMagnetico == 0 ||
                detalleDevolucion.BaseCarga == 0 ||
                detalleDevolucion.Clip == 0 ||
                detalleDevolucion.Manual == 0 ||
                detalleDevolucion.Caja == 0 ||
                detalleDevolucion.HerramientaDeExtraccion == 0)
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            resultado = true;

            return resultado;
        }
        public bool ModifyProduct(DetallesDevolucion detalleDevolucion, Int64 clientId, List<DetallesDevolucion> lst, long returnId)
        {
            bool resultado = true;

            if (detalleDevolucion.ExisteProducto == true && detalleDevolucion.EsSoloDevolucion == true)
            {
                if (detalleDevolucion.TipoProducto == 0)
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
            }
            else if (detalleDevolucion.ExisteProducto == true && detalleDevolucion.EsSoloDevolucion == false)
            {
                if (detalleDevolucion.TipoProducto == 0)
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }

                if (detalleDevolucion.Serie == "0" || string.IsNullOrEmpty(detalleDevolucion.Serie))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
                else
                {
                    bool isSerialCompatibleWithClientId = iDevolucionesDA.isSerialCompatibleWithClientId(
                        detalleDevolucion.TipoProducto,
                        clientId,
                        detalleDevolucion.Serie);

                    if (!isSerialCompatibleWithClientId)
                    {
                        throw new ApplicationException("* El número de serie no se encuentra registrado.");
                    }
                    else
                    {
                        if (lst.Where(
                            a => a.TipoProducto == detalleDevolucion.TipoProducto &&
                                a.Serie == detalleDevolucion.Serie &&
                                a.Id != detalleDevolucion.Id)
                            .Any())
                        {
                            throw new ApplicationException("* El número de serie ya esta registrado en la devolución.");
                        }
                        else
                        {
                            //Validación que verifica que el numero de serie no este en otra devolución distinta a esta.
                            bool isExists = iDevolucionesDA.ExistsProductInAnotherReturn(detalleDevolucion.TipoProducto, detalleDevolucion.Serie, returnId);

                            if (isExists == true)
                            {
                                throw new ApplicationException("* El número de serie ya se encuentra registrado en otra devolución.");
                            }
                        }
                    }
                }

                if (iDevolucionesDA.GetProducts().Where(a => a.ProductoId == detalleDevolucion.TipoProducto).FirstOrDefault().ProductTechnology.ToUpper() == "TEAMVOX")
                {
                    if (string.IsNullOrEmpty(detalleDevolucion.SIM))
                    {
                        throw new ApplicationException("* Complete todos los campos obligatorios.");
                    }

                    if (string.IsNullOrEmpty(detalleDevolucion.Dn))
                    {
                        throw new ApplicationException("* Complete todos los campos obligatorios.");
                    }

                    //Validamos los formatos de TeamVox y Legacy//
                    if (detalleDevolucion.MotivoEnvio == 12)
                    {
                        if (string.IsNullOrEmpty(detalleDevolucion.Alias))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.Grupo))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if(string.IsNullOrEmpty(detalleDevolucion.CarrierId.ToString()) || detalleDevolucion.CarrierId == 0)
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.PlanId.ToString()) || detalleDevolucion.PlanId == 0)
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.Observaciones))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }
                    }
                }
                else
                {
                    //Validamos los formatos de TeamVox y Legacy//
                    if (detalleDevolucion.MotivoEnvio == 12)
                    {
                        if (string.IsNullOrEmpty(detalleDevolucion.Leyenda))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.NombreCarpeta))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.Grupos))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }

                        if (string.IsNullOrEmpty(detalleDevolucion.Observaciones))
                        {
                            throw new ApplicationException("* Complete todos los campos obligatorios.");
                        }
                    }
                }
            } else if (detalleDevolucion.ExisteProducto == false && detalleDevolucion.EsSoloDevolucion == true)
            {
                if (string.IsNullOrEmpty(detalleDevolucion.Producto))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
            } else
            {
                if (string.IsNullOrEmpty(detalleDevolucion.Producto))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }

                if (detalleDevolucion.Serie == "0" || string.IsNullOrEmpty(detalleDevolucion.Serie))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
            }

            if (string.IsNullOrEmpty(detalleDevolucion.Cantidad))
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            } else
            {
                if (new IsNumberOrNot().Validate(detalleDevolucion.Cantidad))
                {
                    throw new ApplicationException("* La cantidad debe de ser un número entero.");
                } else
                {
                    if (Convert.ToInt32(detalleDevolucion.Cantidad) == 0)
                    {
                        throw new ApplicationException("* La cantidad debe de ser un valor mayor a cero.");
                    }
                }
            }

            if (detalleDevolucion.Antena == 0 ||
                detalleDevolucion.Carcasa == 0 ||
                detalleDevolucion.Display == 0 ||
                detalleDevolucion.Teclado == 0 ||
                detalleDevolucion.ConectorUSB == 0 ||
                detalleDevolucion.Tapa == 0 ||
                detalleDevolucion.Bateria == 0 ||
                detalleDevolucion.CargadorEliminador == 0 ||
                detalleDevolucion.CableUSB == 0 ||
                detalleDevolucion.CableUSBMagnetico == 0 ||
                detalleDevolucion.BaseCarga == 0 ||
                detalleDevolucion.Clip == 0 ||
                detalleDevolucion.Manual == 0 ||
                detalleDevolucion.Caja == 0 ||
                detalleDevolucion.HerramientaDeExtraccion == 0)
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            resultado = true;

            return resultado;
        }
        public List<Productos> GetProducts()
        {
            return iDevolucionesDA.GetProducts();
        }
        public SelectList GetSeriesForProductId(long ProductId = 0, long ClientId = 0)
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el número de serie.", Value = "0", Selected = true });

            if (ProductId != 0)
            {
                foreach (var item in iDevolucionesDA.GetProductsByClientIdAndProductId(ProductId, ClientId))
                {
                    listItem.Add(new SelectListItem() { Text = item.SerialDescription, Value = item.SerialDescription });
                }
            }

            return new SelectList(listItem, "Value", "Text", null);
        }
        public List<ProductosSerial> GetProductsByClientIdAndProductId(long productId, long clientId)
        { 
            return iDevolucionesDA.GetProductsByClientIdAndProductId(productId, clientId);
        }
        public SelectList GetStatusDetails()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el estatus.", Value = "0", Selected = false });

            foreach (var item in iDevolucionesDA.GetStatusDetails())
            {
                if (item.EstatusDetallesId == 1)
                {
                    listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.EstatusDetallesNombre.ToString(),
                            Value = item.EstatusDetallesId.ToString(),
                            Selected = true
                        });
                } else
                {
                    listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.EstatusDetallesNombre.ToString(),
                            Value = item.EstatusDetallesId.ToString(),
                            Selected = false
                        });
                }
            }

            return new SelectList(listItem, "Value", "Text", null);
        }
        public List<Carrier> GetCarriers()
        {
            return iDevolucionesDA.GetCarriers();
        }
        public List<PlanData> GetPlanDatas()
        {
            return iDevolucionesDA.GetPlanDatas();
        }
        public SelectList GetCarriersForSelectList()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el carrier.", Value = "0", Selected = true });

            foreach (var item in GetCarriers())
            {
                if (listItem.Where(a => a.Value == item.CarrierId.ToString()).Count() == 0)
                {
                    listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.CarrierName,
                            Value = item.CarrierId.ToString(),
                            Selected = false
                        });
                }
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }
        public SelectList GetPlanDataForSelectList()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el plan.", Value = "0", Selected = true });

            foreach (var item in GetPlanDatas())
            {
                if (listItem.Where(a => a.Value == item.PlanDataId.ToString()).Count() == 0)
                {
                    listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.PlanDataName,
                            Value = item.PlanDataId.ToString(),
                            Selected = false
                        });
                }
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }
        public List<Productos> GetProductos(long clientId = 0)
        {
            return iDevolucionesDA.GetProductsByClientId(clientId);
        }
        public SelectList GetProductsByClientId(long clientId = 0)
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el producto.", Value = "0", Selected = true });

            foreach (var item in iDevolucionesDA.GetProductsByClientId(clientId))
            {
                if (listItem.Where(a => a.Value == item.ProductoId.ToString()).Count() == 0)
                {
                    listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.ProductCode + " - " + item.ProductName,
                            Value = item.ProductoId.ToString(),
                            Selected = false
                        });
                }
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }
        public SelectList GetAccessories(string ce_ui = "")
        {
            string search = string.Empty;
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el accesorio.", Value = "0", Selected = true });

            if (ce_ui == "CC")
            {
                search = "SVTCC";
            } else
            {
                search = "SVTCO";
            }

            foreach (var item in iDevolucionesDA.GetAccessories(search))
            {
                if (listItem.Where(a => a.Value == item.IdAccesorio.ToString()).Count() == 0)
                {
                    listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.NombreAccesorio,
                            Value = item.IdAccesorio.ToString(),
                            Selected = false
                        });
                }
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }
        public SelectList GetContratos(Int64 ClientId = 0)
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            foreach (var item in iDevolucionesDA.GetContratos(ClientId))
            {
                listItem.Add(
                    new SelectListItem()
                    {
                        Text = item.NombreContrato,
                        Value = item.IdContrato.ToString(),
                        Selected = false
                    });
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }
        public Devolucion Create(Devolucion devolucion, string UserId = "", bool enableCC = false, string userEmail = "")
        {
            if (devolucion.Details.Count == 0)
            {
                throw new ApplicationException(
                    "* Necesita al menos un nuevo producto/accesorio para la devolución para continuar.");
            }

            //Asignamos la fecha de creación//
            devolucion.FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now);
            devolucion.Estatus = 1;

            //Comentarios//
            if (!string.IsNullOrEmpty(devolucion.Comentario))
            {
                devolucion.Comentarios.Add(new Comentarios()
                {
                    ComentariosId = devolucion.Comentarios.Count() > 0 ? devolucion.Comentarios.Max(a => a.ComentariosId) + 1 : 1,
                    Descripcion = devolucion.Comentario,
                    FechaCreación = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    UsuarioId = Convert.ToInt64(UserId)
                });
            }

            //Se añadira el encabezado de la devolucion//
            var createDevolution = iDevolucionesDA.Create(devolucion, UserId);

            if (createDevolution == null)
            {
                throw new ApplicationException("Ocurrió un error al momento de crear la devolución. Si el problema persiste consulte a Soporte Técnico.");
            }

            //Variable donde guarda las copias//
            var lstemailReceipts = new List<EmailReceipts>();


            //Obtenemos los productos registrados para el cliente
            var productsForClient = iDevolucionesDA.GetProducts();

            //Obtenemos la cantidad del arreglo con los id de serie(campo clave)//
            string[] supplierIDs = new string[devolucion.Details.Count];

            for (int i = 0; i < devolucion.Details.Count; i++)
            {
                supplierIDs[i] = devolucion.Details[i].TipoProducto.ToString();
            }

            //Para insertar nuevos registros solo contemplará las series no existentes.
            var productsExisting =
                    (from b in productsForClient
                     where (supplierIDs.Contains(b.ProductoId.ToString()))
                     select b).ToList();

            //Email
            EmailLayout email = new EmailLayout();

            //Layout a utilizar - variable
            int LayoutUse = 0;


            //Validamos que el destinatario sea CC o No
            if (enableCC) // Validamos si es CC
            {
                //Validamos los tipos de devolucion.
                if (devolucion.MotivoEnvio == 12) // Enviado a reprogramamcion
                {
                    //Validamos si tenemos tecnologia TeamVox en los productos//
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() == "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(2);
                        lstemailReceipts = iemailDA.GetReceipts(2);
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr24@gmail.com"
                        });
                        LayoutUse = 2;

                        SendEmailCreate(devolucion, email, "TeamVox,", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 1));
                    }
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() != "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(3);
                        lstemailReceipts = iemailDA.GetReceipts(3);
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr_24@hotmail.com"
                        });
                        LayoutUse = 3;

                        SendEmailCreate(devolucion, email, "Legacy,", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 2));
                    }

                }
                else //Todas las demás
                {
                    email = iemailDA.GetEmailById(1);
                    lstemailReceipts = iemailDA.GetReceipts(1);
                    LayoutUse = 1;

                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() == "TEAMVOX").Count() > 0)
                    {
                        SendEmailCreate(devolucion, email, "", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 1));
                    }
                    else
                    {
                        SendEmailCreate(devolucion, email, "", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 2));
                    }
                }
            }
            else
            {
                //Validamos los tipos de devolucion.
                if (devolucion.MotivoEnvio == 12) // Enviado a reprogramamcion
                {
                    //Validamos si tenemos tecnologia TeamVox en los productos//
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() == "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(5);
                        lstemailReceipts = iemailDA.GetReceipts(5);
                        LayoutUse = 5;
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr24@gmail.com"
                        });

                        SendEmailCreate(devolucion, email, "TeamVox,", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 1));
                    }
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() != "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(6);
                        lstemailReceipts = iemailDA.GetReceipts(6);
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr_24@hotmail.com"
                        });
                        LayoutUse = 6;

                        SendEmailCreate(devolucion, email, "Legacy,", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 2));
                    }

                }
                else //Todas las demás
                {
                    email = iemailDA.GetEmailById(4);
                    lstemailReceipts = iemailDA.GetReceipts(4);
                    LayoutUse = 4;

                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() == "TEAMVOX").Count() > 0)
                    {
                        SendEmailCreate(devolucion, email, "", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 1));
                    }
                    else
                    {
                        SendEmailCreate(devolucion, email, "", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 2));
                    }
                }
            }

            return createDevolution;
        }
        public Devolucion Modify(Devolucion devolucion, string UserId = "", bool enableCC = false, string userEmail = "", string ui_ci = "")
        {
            if (devolucion.Details.Count == 0)
            {
                throw new ApplicationException(
                    "* Necesita al menos un nuevo producto/accesorio para la devolución para continuar.");
            }

            var devolucionAfter = iDevolucionesDA.GetReturnById(devolucion.ClaveDevolucion);

            if(devolucion.Cliente != devolucionAfter.Cliente)
            {
                throw new ApplicationException(
                    "* El campo cliente no puede ser editado.");
            }

            if(devolucion.EmpleadoResponsable != devolucionAfter.EmpleadoResponsable)
            {
                throw new ApplicationException(
                    "* El campo empleado responsable no puede ser editado.");
            }

            if(devolucion.MotivoEnvio != devolucionAfter.MotivoEnvio)
            {
                throw new ApplicationException(
                    "* El campo motivo de envío no puede ser editado.");
            }

            if (devolucion.Cliente == 0)
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            if (string.IsNullOrEmpty(devolucion.Envio) || string.IsNullOrWhiteSpace(devolucion.Envio))
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            if (devolucion.MotivoEnvio == 0)
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            if (devolucion.MotivoEnvio == 11)
            {
                if (string.IsNullOrEmpty(devolucion.Descripcion) || string.IsNullOrWhiteSpace(devolucion.Descripcion))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
            }

            if (string.IsNullOrEmpty(devolucion.NumeroGuia) || string.IsNullOrWhiteSpace(devolucion.NumeroGuia))
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            if (string.IsNullOrEmpty(devolucion.NombreMensajeria) ||
                string.IsNullOrWhiteSpace(devolucion.NombreMensajeria))
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            if (ui_ci == "CC")
            {
                if (string.IsNullOrEmpty(devolucion.DestinoCC) || string.IsNullOrWhiteSpace(devolucion.DestinoCC))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
            }

            if (devolucionAfter.Estatus == 2)
            {
                throw new ApplicationException("La operación no puede ser terminada, debido a que la devolución ya se encuentra como cancelada.");
            }

            //Asignamos la fecha de creación//
            devolucion.FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now);

            if(devolucion.Estatus == 4)
            {
                devolucion.Estatus = 3; //Si esta rechazado por almacen se cambia el estatus en Proceso.
            }

            //Se añadira el encabezado de la devolucion//
            var modifyDevolution = iDevolucionesDA.Modify(devolucion, UserId);

            if (modifyDevolution == null)
            {
                throw new ApplicationException("Ocurrió un error al momento de modificar la devolución. Si el problema persiste consulte a Soporte Técnico.");
            }

            //Obtener comentarios//
            var getReturn = iDevolucionesDA.GetReturnById(devolucion.ClaveDevolucion);

            ////Comentarios//
            if (getReturn.Comentarios.Count() > 0)
            {
                devolucion.Comentarios = new List<Comentarios>();

                foreach (var comments in getReturn.Comentarios)
                {
                    devolucion.Comentarios.Add(new Comentarios()
                    {
                        ComentariosId = comments.ComentariosId,
                        Descripcion = comments.Descripcion,
                        FechaCreación = comments.FechaCreación,
                        UsuarioId = comments.UsuarioId
                    });
                }

            }

            //Variable donde guarda las copias//
            var lstemailReceipts = new List<EmailReceipts>();


            //Obtenemos los productos registrados para el cliente
            var productsForClient = iDevolucionesDA.GetProducts();

            //Obtenemos la cantidad del arreglo con los id de serie(campo clave)//
            string[] supplierIDs = new string[devolucion.Details.Count];

            for (int i = 0; i < devolucion.Details.Count; i++)
            {
                supplierIDs[i] = devolucion.Details[i].TipoProducto.ToString();
            }

            //Para insertar nuevos registros solo contemplará las series no existentes.
            var productsExisting =
                    (from b in productsForClient
                     where (supplierIDs.Contains(b.ProductoId.ToString()))
                     select b).ToList();

            //Email
            EmailLayout email = new EmailLayout();

            //Layout a utilizar - variable
            int LayoutUse = 0;

            //Validamos que el destinatario sea CC o No
            if (enableCC) // Validamos si es CC
            {
                //Validamos los tipos de devolucion.
                if (devolucion.MotivoEnvio == 12) // Enviado a reprogramamcion
                {
                    //Validamos si tenemos tecnologia TeamVox en los productos//
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() == "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(14);
                        lstemailReceipts = iemailDA.GetReceipts(2);
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr24@gmail.com"
                        });
                        LayoutUse = 14;
                        
                        SendEmailCreate(devolucion, email, "TeamVox,", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 1));
                    }
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() != "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(15);
                        lstemailReceipts = iemailDA.GetReceipts(3);
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr_24@hotmail.com"
                        });
                        LayoutUse = 15;
                        
                        SendEmailCreate(devolucion, email, "Legacy,", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 2));
                    }

                }
                else //Todas las demás
                {
                    email = iemailDA.GetEmailById(13);
                    lstemailReceipts = iemailDA.GetReceipts(1);
                    LayoutUse = 13;

                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() == "TEAMVOX").Count() > 0)
                    {
                        SendEmailCreate(devolucion, email, "", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 1));
                    }
                    else
                    {
                        SendEmailCreate(devolucion, email, "", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 2));
                    }

                        
                }
            }
            else
            {
                //Validamos los tipos de devolucion.
                if (devolucion.MotivoEnvio == 12) // Enviado a reprogramamcion
                {
                    //Validamos si tenemos tecnologia TeamVox en los productos//
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() == "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(17);
                        lstemailReceipts = iemailDA.GetReceipts(5);
                        LayoutUse = 17;
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr24@gmail.com"
                        });
                        
                        SendEmailCreate(devolucion, email, "TeamVox,", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 1));
                    }
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() != "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(18);
                        lstemailReceipts = iemailDA.GetReceipts(6);
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr_24@hotmail.com"
                        });
                        LayoutUse = 18;

                        SendEmailCreate(devolucion, email, "Legacy,", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 2));
                    }

                }
                else //Todas las demás
                {
                    email = iemailDA.GetEmailById(16);
                    lstemailReceipts = iemailDA.GetReceipts(4);
                    LayoutUse = 16;

                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() == "TEAMVOX").Count() > 0)
                    {
                        SendEmailCreate(devolucion, email, "", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 1));
                    }
                    else
                    {
                        SendEmailCreate(devolucion, email, "", lstemailReceipts, LayoutUse, Convert.ToInt64(UserId), userEmail, createFormat(devolucion, 2));
                    }
                }
            }


            return modifyDevolution;
        }
        public void SendEmailCreate(Devolucion devolucion, EmailLayout email, string Technology, List<EmailReceipts> lstreceipts, int LayoutUse, long UserId, string userEmail, files file)
        {
            //Variable que guarda el asunto//
            StringBuilder subject = new StringBuilder();

            var employeePrincipalS = iUserDA.GetEmployeeFromSAP().Where(a => a.EmployeeId == devolucion.EmpleadoResponsable).FirstOrDefault();

            //Datos necesarios para el correo//
            string reasonSendName = iDevolucionesDA.GetMotivoEnvio().Where(a => a.motivoenvioid == devolucion.MotivoEnvio).FirstOrDefault().motivoenvionombre;
            string employeePrincipal = employeePrincipalS.CeeUUID + " - " + employeePrincipalS.TeeID;
            string nameClient = iDevolucionesDA.GetClientesAll().Where(a => a.IdCliente == devolucion.Cliente).FirstOrDefault().NombreCliente;

            //Obtenemos el asunto del correo electrónico
            subject.Append("#" + devolucion.ClaveDevolucion.ToString() + "," + reasonSendName + "," + nameClient + "," + Technology + ",");
            subject.Append(employeePrincipal);

            StringBuilder strComments = new StringBuilder();

            if(devolucion.Comentarios != null)
            {
                if (devolucion.Comentarios.Count() > 0)
                {
                    int comentarios = 1;

                    foreach (var item in devolucion.Comentarios)
                    {
                        if (comentarios == devolucion.Comentarios.Count())
                        {
                            strComments.Append(item.Descripcion);
                        }
                        else
                        {
                            strComments.Append("<br/>" + item.Descripcion);
                        }

                        comentarios++;
                    }
                }

            }

            //Generamos el body del correo electrónico
            email.EmailBody = email.EmailBody
                .Replace("{{@UrlImageTeamvox}}", iConfiguration["Resources:UrlSite"] + iConfiguration["Resources:UrlLogoTeamVox"])
                .Replace("{{@NombreCliente}}", nameClient)
                .Replace("{{@Clave}}", devolucion.ClaveDevolucion.ToString())
                .Replace("{{@EmpleadoResponsable}}", employeePrincipal)
                .Replace("{{@EnviadoPor}}", devolucion.Envio)
                .Replace("{{@MotivoEnvio}}", reasonSendName)
                .Replace("{{@Guia}}", devolucion.NumeroGuia)
                .Replace("{{@Mensajeria}}", devolucion.NombreMensajeria)
                .Replace("{{@DestinoCC}}", string.IsNullOrEmpty(devolucion.DestinoCC) ? "N/A" : devolucion.DestinoCC)
                .Replace("{{@FechaCreacion}}", devolucion.FechaCreacion.Day.ToString() + "/" + devolucion.FechaCreacion.Month.ToString() + "/" + devolucion.FechaCreacion.Year.ToString() + " " + devolucion.FechaCreacion.ToString("HH:mm:ss"))
                .Replace("{{@UrlSitio}}", iConfiguration["Resources:UrlSite"])
                .Replace("{{@Anio}}", DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now).Year.ToString())
                .Replace("{{@Comentarios}}", devolucion.Comentarios == null ? "Sin comentarios" : devolucion.Comentarios.Count() == 0 ? "Sin comentarios" : strComments.ToString())
                ;
                
            //Creamos el registro del Log del correo electrónico
            EmailLog emailLog = iemailDA.CreateEmailLog(new EmailLog()
            {
                EmailCreateDate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                EmailHtml = email.EmailBody,
                EmailLayoutId = LayoutUse,
                EmailSendDate = null,
                EmailStatus = 1,
                EmailSubject = subject.ToString(),
                UserId = Convert.ToInt64(UserId)
            });

            //Damos de alta los destinatarios//
            // 1. Principal
            bool emailDestinatary = iemailDA.CreateDestinatary(new EmailReceipts()
            {
                EmailAddress = userEmail
            }, 1, emailLog.EmailLogId);

            //2. Secundarios
            foreach (var item in lstreceipts)
            {
                emailDestinatary = iemailDA.CreateDestinatary(new EmailReceipts()
                {
                    EmailAddress = item.EmailAddress
                }, 2, emailLog.EmailLogId);
            }

            List<System.Net.Mail.Attachment> lstFilesEmail = new List<System.Net.Mail.Attachment>();

            if(file != null)
            {
                if (file.fileContent != null)
                {
                    System.Net.Mail.Attachment fileAtt = new System.Net.Mail.Attachment(new MemoryStream(file.fileContent), file.nameFile + file.extensionFile);


                    lstFilesEmail.Add(fileAtt);
                }
            }

            Response responseEmail = new Response();

            try
            {
                //Enviamos el Email
                responseEmail = new EmailBus(iemailDA).SendEmail(
                    email,
                    lstreceipts,
                    subject + " - Envios de prueba (Ignorar) - Francisco Rivera",
                    userEmail, lstFilesEmail); //Teniendo los reportes se adjuntaran
            }
            catch(Exception ex)
            {
                responseEmail.Codigo = 0;
                responseEmail.Mensaje = ex.Message;
            }

            //Si no se envia se deja el estatus a cuando se creo.
            if (responseEmail.Codigo == 0)
            {
                throw new ApplicationException(responseEmail.Mensaje);
            }

            emailLog.EmailSendDate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now);
            emailLog.EmailStatus = 2;
            emailLog = iemailDA.UpdateStatusEmailLog(emailLog);

        }
        public Response New(Devolucion devolucion, string ui_ci)
        {
            Response response = new Response();

            if(devolucion.Cliente == 0)
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            if(string.IsNullOrEmpty(devolucion.Envio) || string.IsNullOrWhiteSpace(devolucion.Envio))
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            if(devolucion.MotivoEnvio == 0)
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            if(devolucion.MotivoEnvio == 11)
            {
                if(string.IsNullOrEmpty(devolucion.Descripcion) || string.IsNullOrWhiteSpace(devolucion.Descripcion))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
            }

            if(string.IsNullOrEmpty(devolucion.NumeroGuia) || string.IsNullOrWhiteSpace(devolucion.NumeroGuia))
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            if(string.IsNullOrEmpty(devolucion.NombreMensajeria) ||
                string.IsNullOrWhiteSpace(devolucion.NombreMensajeria))
            {
                throw new ApplicationException("* Complete todos los campos obligatorios.");
            }

            if(ui_ci == "CC")
            {
                if(string.IsNullOrEmpty(devolucion.DestinoCC) || string.IsNullOrWhiteSpace(devolucion.DestinoCC))
                {
                    throw new ApplicationException("* Complete todos los campos obligatorios.");
                }
            }

            //if (string.IsNullOrWhiteSpace(devolucion.Comentarios))
            //{
            //    throw new ApplicationException("* Complete todos los campos obligatorios.");
            //}

            //ServiceReference1.ServiceOrderBundleMaintainConfirmation serviceOrderBundleMaintainConfirmation = new ServiceReference1.ServiceOrderBundleMaintainConfirmation();

            //CreateNewOrder();
            //Devolcuon();

            response.Codigo = 1;
            response.Mensaje = "Introduzca los detalles de envio de la devolución";

            return response;
        }
        public Response Cancel(Devolucion devolucion, long userId, bool enableCC = false, string userEmail = "")
        {
            //Validaciones de la reservación
            if (iDevolucionesDA.GetReturnById(devolucion.ClaveDevolucion).Estatus == 2)
            {
                throw new ApplicationException("La operación no puede ser terminada, debido a que la devolución ya se encuentra como cancelada.");
            }

            //Cancelando la devolucion//
            var response = iDevolucionesDA.Cancel(devolucion, userId);

            if(response.Codigo == 0)
            {
                return response;
            }

            //Se envia correo de cancelación//

            //Variable donde guarda las copias//
            var lstemailReceipts = new List<EmailReceipts>();


            //Obtenemos los productos registrados para el cliente
            var productsForClient = iDevolucionesDA.GetProducts();

            //Obtenemos la cantidad del arreglo con los id de serie(campo clave)//
            string[] supplierIDs = new string[devolucion.Details.Count];

            for (int i = 0; i < devolucion.Details.Count; i++)
            {
                supplierIDs[i] = devolucion.Details[i].TipoProducto.ToString();
            }

            //Para insertar nuevos registros solo contemplará las series no existentes.
            var productsExisting =
                    (from b in productsForClient
                     where (supplierIDs.Contains(b.ProductoId.ToString()))
                     select b).ToList();

            //Email
            EmailLayout email = new EmailLayout();

            //Layout a utilizar - variable
            int LayoutUse = 0;

            //Validamos que el destinatario sea CC o No
            if (enableCC) // Validamos si es CC
            {
                //Validamos los tipos de devolucion.
                if (devolucion.MotivoEnvio == 12) // Enviado a reprogramamcion
                {
                    //Validamos si tenemos tecnologia TeamVox en los productos//
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() == "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(8);
                        lstemailReceipts = iemailDA.GetReceipts(2);
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr24@gmail.com"
                        });
                        LayoutUse = 8;
                        SendEmailCreate(devolucion, email, "TeamVox,", lstemailReceipts, LayoutUse, Convert.ToInt64(userId), userEmail, null);
                    }
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() != "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(9);
                        lstemailReceipts = iemailDA.GetReceipts(3);
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr_24@hotmail.com"
                        });
                        LayoutUse = 9;
                        SendEmailCreate(devolucion, email, "Legacy,", lstemailReceipts, LayoutUse, Convert.ToInt64(userId), userEmail, null);
                    }

                }
                else //Todas las demás
                {
                    email = iemailDA.GetEmailById(7);
                    lstemailReceipts = iemailDA.GetReceipts(1);
                    LayoutUse = 7;
                    SendEmailCreate(devolucion, email, "", lstemailReceipts, LayoutUse, Convert.ToInt64(userId), userEmail, null);
                }
            }
            else
            {
                //Validamos los tipos de devolucion.
                if (devolucion.MotivoEnvio == 12) // Enviado a reprogramamcion
                {
                    //Validamos si tenemos tecnologia TeamVox en los productos//
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() == "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(11);
                        lstemailReceipts = iemailDA.GetReceipts(5);
                        LayoutUse = 11;
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr24@gmail.com"
                        });
                        SendEmailCreate(devolucion, email, "TeamVox,", lstemailReceipts, LayoutUse, Convert.ToInt64(userId), userEmail, null);
                    }
                    if (productsExisting.Where(a => a.ProductTechnology.ToUpper() != "TEAMVOX").Count() > 0)
                    {
                        email = iemailDA.GetEmailById(12);
                        lstemailReceipts = iemailDA.GetReceipts(6);
                        lstemailReceipts.Add(new EmailReceipts()
                        {
                            EmailAddress = "vmtr_24@hotmail.com"
                        });
                        LayoutUse = 12;
                        SendEmailCreate(devolucion, email, "Legacy,", lstemailReceipts, LayoutUse, Convert.ToInt64(userId), userEmail, null);
                    }

                }
                else //Todas las demás
                {
                    email = iemailDA.GetEmailById(10);
                    lstemailReceipts = iemailDA.GetReceipts(4);
                    LayoutUse = 10;
                    SendEmailCreate(devolucion, email, "", lstemailReceipts, LayoutUse, Convert.ToInt64(userId), userEmail, null);
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////

            return new Response()
            {
                Codigo = 1,
                Mensaje = "La devolución ha sido cancelada de manera exitosa."
            };

        }
        public Devolucion GetReturnById(long returnId)
        {
            var dev = iDevolucionesDA.GetReturnById(returnId);

            List<DetallesDevolucion> detDev = new List<DetallesDevolucion>();

            long contador = 1;

            foreach (var item in dev.Details)
            {
                item.Id = contador;

                var response = iDevolucionesDA.GetStockConfirmation(item.IdDetallesDevolucion);

                if(response == null)
                {
                    item.DetailsReservationStock = new DevolucionesDetailsStock();
                    item.StockConfirmation = new StockConfirmation();
                    item.StockConfirmation.StockDetails = new List<StockConfirmationDetails>();

                    item.DetailsReservationStock = new DevolucionesDetailsStock();

                    //var lastModified = response.StockDetails.OrderByDescending(a => a.StockConfirmationDetailsId).FirstOrDefault();

                    item.DetailsReservationStock.lastquantityreported = "";
                    item.DetailsReservationStock.lastcommentreported = "";

                    item.DetailsReservationStock.quantity = item.Cantidad;
                    item.DetailsReservationStock.nameproduct = item.Producto;
                    item.DetailsReservationStock.statusid = 6;

                    item.DetailsReservationStock.status = iDevolucionesDA.getStatusReturnConfirmation(item.DetailsReservationStock.statusid).EstatusDetallesNombre;

                    item.DetailsReservationStock.lastquantityreported = "0";
                    item.DetailsReservationStock.lastcommentreported = "";
                }
                else
                {
                    item.StockConfirmation = response;
                    item.DetailsReservationStock = new DevolucionesDetailsStock();

                    var lastModified = response.StockDetails.OrderByDescending(a => a.StockConfirmationDetailsId).FirstOrDefault();

                    item.DetailsReservationStock.lastquantityreported = "0";
                    item.DetailsReservationStock.lastcommentreported = "";

                    item.DetailsReservationStock.quantity = item.Cantidad;
                    item.DetailsReservationStock.nameproduct = item.Producto;
                    item.DetailsReservationStock.statusid = 6;
                    item.DetailsReservationStock.status = iDevolucionesDA.getStatusReturnConfirmation(item.DetailsReservationStock.statusid).EstatusDetallesNombre;


                    if (lastModified != null)
                    {
                        item.DetailsReservationStock.statusid = item.StockConfirmation.StatusId;
                        item.DetailsReservationStock.status = iDevolucionesDA.getStatusReturnConfirmation(item.StockConfirmation.StatusId).EstatusDetallesNombre;

                        var stockInfo = dev.Details.Where(a => a.IdDetallesDevolucion == item.IdDetallesDevolucion && (a.StockConfirmation.StatusId == 1 || a.StockConfirmation.StatusId == 2 || a.StockConfirmation.StatusId == 3 || a.StockConfirmation.StatusId == 5)).ToList();

                        string quantity = "0";

                        if(stockInfo.Count > 0)
                        {
                            foreach(var itemStock in stockInfo)
                            {
                                if(itemStock.StockConfirmation.StatusId == 1 || itemStock.StockConfirmation.StatusId == 2 || itemStock.StockConfirmation.StatusId == 3  || itemStock.StockConfirmation.StatusId == 5)
                                {
                                    foreach(var itemQuantity in itemStock.StockConfirmation.StockDetails.ToList())
                                    {
                                        if(itemQuantity.StockConfirmationComments != "-1")
                                        quantity = (Convert.ToInt64(quantity) + Convert.ToInt64(itemQuantity.StockConfirmationQuantity)).ToString();
                                    }
                                }
                            }
                        }

                        item.DetailsReservationStock.lastquantityreported = quantity;
                        item.DetailsReservationStock.lastcommentreported = lastModified.StockConfirmationComments;
                        
                    }
                    
                }

                item.QuantityRestante = Convert.ToInt64(item.Cantidad) - Convert.ToInt64(item.DetailsReservationStock.lastquantityreported);

                detDev.Add(item);

                contador++;
            }

            //Contar los registros sin completar

            var contar = dev.Details.ToList();

            int contarSinConfirmar = 0, contarConfirmar = 0, contarRechazados = 0;

            dev.ConfirmaRechazada = -1;

            foreach (var item in contar)
            {
                if (item.StockConfirmation.StatusId == 0)
                {
                    contarSinConfirmar++;
                }

                if (item.StockConfirmation.StatusId == 2 || item.StockConfirmation.StatusId == 3)
                {
                    contarRechazados++;
                }

                if (item.StockConfirmation.StatusId == 1)
                {
                    contarConfirmar++;
                }
            }

            if((contarSinConfirmar > 0 && contarConfirmar > 0 && contarRechazados > 0 ) && dev.ConfirmaRechazada == -1) //1 1 1 REV
            {
                dev.ConfirmaRechazada = 1;
            }
            else if ((contarSinConfirmar > 0 && contarConfirmar > 0 && contarRechazados == 0) && dev.ConfirmaRechazada == -1) //1 1 0 REV
            {
                dev.ConfirmaRechazada = 2;
            }
            else if ((contarSinConfirmar > 0 && contarConfirmar == 0 && contarRechazados > 0) && dev.ConfirmaRechazada == -1) //1 0 1
            {
                dev.ConfirmaRechazada = 3;
            }
            else if ((contarSinConfirmar > 0 && contarConfirmar == 0 && contarRechazados == 0) && dev.ConfirmaRechazada == -1) //1 0 0
            {
                dev.ConfirmaRechazada = 4;
            }
            else if ((contarSinConfirmar == 0 && contarConfirmar > 0 && contarRechazados > 0) && dev.ConfirmaRechazada == -1) //0 1 1 REV
            {
                dev.ConfirmaRechazada = 5;
            }
            else if ((contarSinConfirmar == 0 && contarConfirmar > 0 && contarRechazados == 0) && dev.ConfirmaRechazada == -1) //0 1 0 REV
            {
                dev.ConfirmaRechazada = 6;
            }
            else if ((contarSinConfirmar == 0 && contarConfirmar == 0 && contarRechazados > 0) && dev.ConfirmaRechazada == -1) //0 0 1
            {
                dev.ConfirmaRechazada = 7;
            }
            else //0 0 0 -- Nunca sucedera porque es cuando no existen registros y en la devolucion siempre existen
            {

            }

            if (dev.Estatus == 4 || dev.Estatus == 5) //Si la devolucion esta confirmada (totalmente) o rechazada
            {
                dev.ConfirmaRechazada = -1;
            }

            dev.Details = detDev;

            return dev;

        }

        private void GetStockConfirm(List<StockConfirm> stockConfirms, long returnId)
        {
            bool isTrue = false;

            //Realizamos la invocación del servicio Rest//
            MercancyStatus mercancyStatus = new MercancyStatus();

            MercancyStatusRequest statusMercancy = new MercancyStatusRequest();
            statusMercancy.Id = new List<string>();

            foreach(var item in stockConfirms)
            {
                statusMercancy.Id.Add(item.StockConfirmSapCode.ToString());
            }
            
            var request = RestClientA.Client.POST(new Request()
            {
                Body = statusMercancy,
                Url = iConfiguration["APISap:URLSAPAPI"],
                Resource = iConfiguration["APISap:StatusMercancy"]
            });

            if (request.StatusCode == System.Net.HttpStatusCode.OK)
            {
                mercancyStatus = JsonConvert.DeserializeObject<MercancyStatus>(request.Content);

                isTrue = true;
            }
            else
            {
                mercancyStatus = JsonConvert.DeserializeObject<MercancyStatus>(request.Content);

                isTrue = false;
            }

            //Si el resultado es OK deberemos de validar la situacion.
            if(isTrue == true)
            {
                foreach(var item in mercancyStatus.MercancyStatusDetails)
                {
                    if(item.MercancyStatusId == 4)
                    {
                        var getConfirm = stockConfirms.Where(a => Convert.ToInt64(a.StockConfirmSapCode) == Convert.ToInt64(item.DocumentId)).FirstOrDefault();

                        if(getConfirm != null)
                        {
                            getConfirm.StatusConfirmStatusId = 4;

                            iDevolucionesDA.UpdateConfirmStock(getConfirm);

                            iDevolucionesDA.UpdateConfirmation(getConfirm.Details);

                            var dev = iDevolucionesDA.GetReturnById(returnId);

                            dev.Estatus = 3;

                            iDevolucionesDA.modifyStatusReturn(dev);

                        }
                    }
                }
            }
        }

        public void CreateNewOrder()
        {
            //String endpointurl = "http://hostname:port/XISOAPAdapter/MessageServlet?senderParty=&senderService=BC_SENDER_SYSTEM0&receiverParty=&receiverService=&interface=SI_SEND_ORG_DATA&interfaceNamespace=http://example.com/HR/OrganisationData";
            //BasicHttpBinding binding = new BasicHttpBinding();
            ////If you need HTTP with Basic Auth for internal network or dev environments. Otherwise remove these two lines:
            //binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            var binding = SOAPServices.SOAPService.GetBindingForEndpoint(new TimeSpan(10000000000000));
            var endpoint = SOAPServices.SOAPService
                .GetEndpointAddress(
                    "https://my342508.sapbydesign.com/sap/bc/srt/scs/sap/manageserviceorderin4?sap-vhost=my342508.sapbydesign.com");

            //// Create the client
            GestionOrden.ManageServiceOrderInClient sapService = new GestionOrden.ManageServiceOrderInClient(
                binding,
                endpoint);

            SysSvcmod.ClientCredentials clientCredentials = new SysSvcmod.ClientCredentials();
            clientCredentials.UserName.UserName = "_ORDENDESERV";
            clientCredentials.UserName.Password = "Welcome01hfghfthgfhfghfgh";

            sapService.ChannelFactory.Endpoint.EndpointBehaviors.RemoveAt(0);
            sapService.ChannelFactory.Endpoint.EndpointBehaviors.Add(clientCredentials);

            var parameter = new GestionOrden.ServiceOrderBundleMaintainRequest();

            var serviceOrder = parameter.ServiceOrder;

            List<GestionOrden.ServiceOrderMaintainRequest> lstServiceOrder = new List<GestionOrden.ServiceOrderMaintainRequest>(
                );

            var serviceOrderMaintainRequest = new GestionOrden.ServiceOrderMaintainRequest();

            serviceOrderMaintainRequest.actionCode = GestionOrden.ActionCode.Item04;

            serviceOrderMaintainRequest.BuyerName = new GestionOrden.EXTENDED_Name()
            {
                languageCode = "ES",
                Value = "2Real People Solutions S.A. de C.V."
            };

            serviceOrderMaintainRequest.BuyerID = new GestionOrden.BusinessTransactionDocumentID()
            {
                Value = "PRUEBAS WS SERVICIOS"
            };

            serviceOrderMaintainRequest.ShipToParty = new GestionOrden.ServiceOrderMaintainRequestPartyParty()
            {
                PartyID = new GestionOrden.PartyID() { Value = "C0001889" }
            };

            serviceOrderMaintainRequest.SalesUnitParty = new GestionOrden.ServiceOrderMaintainRequestPartyParty()
            {
                PartyID = new GestionOrden.PartyID() { Value = "SVTCO" }
            };

            serviceOrderMaintainRequest.BuyerParty = new GestionOrden.ServiceOrderMaintainRequestPartyParty()
            {
                PartyID = new GestionOrden.PartyID() { Value = "C0001889" }
            };

            serviceOrderMaintainRequest.PayerParty = new GestionOrden.ServiceOrderMaintainRequestPartyParty()
            {
                PartyID = new GestionOrden.PartyID() { Value = "C0001889" }
            };

            serviceOrderMaintainRequest.RequestedFulfillmentPeriodPeriodTerms = new GestionOrden.ServiceOrderMaintainRequestRequestedFulfillmentPeriodPeriodTerms(
                )
            {
                StartDateTime = new GestionOrden.DateTime() { Value = DateTime.UtcNow }
            };

            serviceOrderMaintainRequest.SalesAndServiceBusinessArea = new GestionOrden.ServiceOrderMaintainRequestSalesAndServiceBusinessArea(
                )
            {
                DistributionChannelCode = new GestionOrden.DistributionChannelCode() { Value = "01" }
            };

            var producto = new GestionOrden.ServiceOrderMaintainRequestServiceReferenceObject()
            {
                ReferenceProductKey =
                    new GestionOrden.ServiceOrderMaintainRequestReferenceProductKey()
                    {
                        ProductID = new GestionOrden.ProductID() { Value = "TVXONE" }
                    },
                RegisteredProductKey =
                    new GestionOrden.ServiceOrderMaintainRequestRegisteredProductKey()
                    {
                        ProductID = new GestionOrden.ProductID() { Value = "357509100005619" }
                    }
            };

            List<GestionOrden.ServiceOrderMaintainRequestServiceReferenceObject> lstProducts = new List<GestionOrden.ServiceOrderMaintainRequestServiceReferenceObject>(
                );

            serviceOrderMaintainRequest.ServiceReferenceObject = lstProducts.ToArray();

            lstServiceOrder.Add(serviceOrderMaintainRequest);

            serviceOrder = lstServiceOrder.ToArray();

            parameter.ServiceOrder = serviceOrder;

            //var response = new GestionOrden.MaintainBundleRequest(parameter);
            var response = new GestionOrden.ServiceOrderBundleMaintainRequest()
            {
                ServiceOrder = parameter.ServiceOrder
            };


            //var request = new GestionOrden.

            //GestionOrden.ManageServiceOrderIn iManage;
            //iManage.MaintainBundleAsync(valor);

            //EndpointAddress endpoint = new EndpointAddress(endpointurl);

            //GestionOrden.ManageServiceOrderIn manageServiceOrderIn;
            //var request = new GestionOrden.MaintainBundleRequest();

            var resultado = sapService.MaintainBundleAsync(response);

            string vari = XmlExtension.Serialize(response);

            Console.WriteLine("hOLIII");

            //var wsclient = new GestionOrden.ManageServiceOrderInClient(binding, endpoint);
            ////wsclient.ClientCredential.UserName = "username";
            ////wsclient.ClientCredential.Password = "password";

            ////Here you can use client
            //var request = new GestionOrden.ManageServiceOrder();
            //request.field = "value"
            //var response = await wsclient.SI_SEND_ORG_DATAAsync(request);

            //// Create the client
            //GestionOrden.ManageServiceOrderInClient sapService = new GestionOrden.ManageServiceOrderInClient(binding, endpoint);
            //sapService.ClientCredentials = new NetworkCredential("Account", "Password");

            //// Prepare the parameters
            //sapService.param = new SapServiceReference._parameters();
            //param.id = 1;
            //param.action = "R";

            //// Call SAP service
            //SapServiceReference._ws_Response response = sapService._someFunction(param);
            //string result = response.EvDescriptionText.Tdline;
        }
        public Response AddComments(Comentarios comentarios, long returnId)
        {
            Response response = new Response();

            try
            {
                //Validamos el estatus de la devolución//
                if(iDevolucionesDA.GetReturnById(returnId).Estatus == 2) //Cancelada
                {
                    throw new ApplicationException("No se pueden añadir comentarios a devoluciones canceladas.");
                }

                if (string.IsNullOrEmpty(comentarios.Descripcion))
                {
                    throw new ApplicationException("El campo de comentario debe de ser llenado.");
                }

                bool addComment = iDevolucionesDA.AddComment(comentarios, returnId);

                if (addComment)
                {
                    response.Codigo = 1;
                    response.Mensaje = "El comentario ha sido añadido correctamente.";
                }
                else
                {
                    response.Codigo = 0;
                    response.Mensaje = "Ocurrió un error al añadir el comentario, consulte a Soporte Técnico.";
                }
            }
            catch(Exception ex)
            {
                response.Codigo = 0;
                response.Mensaje = ex.Message;
            }

            return response;
        }
        public void Devolcuon()
        {
            //Create binding
            //Note, this is not secure but it's not up to us to decide. This should only ever be run within
            //the VPN or Intranet where IPSec is active. If SAP is ever directly from outside the network,
            //credentials and messages will not be private.
            //var binding = new BasicHttpBinding();
            //binding.MaxReceivedMessageSize = int.MaxValue;
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            //binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;

            //Assign address
            //var address = new EndpointAddress("http://my342508.sapbydesign.com/sap/bc/srt/scs/sap/manageserviceorderin4?sap-vhost=my342508.sapbydesign.com");

            //Create service client
            //var client = new SAP_RFC_READ_TABLE.RFC_READ_TABLEPortTypeClient(binding, address);
            var client = new Orden.ManageServiceOrderInClient(
                SOAPServices.SOAPService.GetBindingForEndpoint(new TimeSpan(int.MaxValue)),
                SOAPServices.SOAPService
                    .GetEndpointAddress(
                        "https://my342508.sapbydesign.com/sap/bc/srt/scs/sap/manageserviceorderin4?sap-vhost=my342508.sapbydesign.com"));

            //Assign credentials
            client.ClientCredentials.UserName.UserName = "_ORDENDESERV";
            client.ClientCredentials.UserName.Password = "Welcome01";

            var parameter = new Orden.ServiceOrderBundleMaintainRequest();

            var serviceOrder = parameter.ServiceOrder;

            List<Orden.ServiceOrderMaintainRequest> lstServiceOrder = new List<Orden.ServiceOrderMaintainRequest>();

            var serviceOrderMaintainRequest = new Orden.ServiceOrderMaintainRequest();

            serviceOrderMaintainRequest.actionCode = Orden.ActionCode.Item04;

            serviceOrderMaintainRequest.BuyerName = new Orden.EXTENDED_Name()
            {
                languageCode = "ES",
                Value = "2Real People Solutions S.A. de C.V."
            };

            serviceOrderMaintainRequest.BuyerID = new Orden.BusinessTransactionDocumentID()
            {
                Value = "PRUEBAS WS SERVICIOS"
            };

            serviceOrderMaintainRequest.ShipToParty = new Orden.ServiceOrderMaintainRequestPartyParty()
            {
                PartyID = new Orden.PartyID() { Value = "C0001889" }
            };

            serviceOrderMaintainRequest.SalesUnitParty = new Orden.ServiceOrderMaintainRequestPartyParty()
            {
                PartyID = new Orden.PartyID() { Value = "SVTCO" }
            };

            serviceOrderMaintainRequest.BuyerParty = new Orden.ServiceOrderMaintainRequestPartyParty()
            {
                PartyID = new Orden.PartyID() { Value = "C0001889" }
            };

            serviceOrderMaintainRequest.PayerParty = new Orden.ServiceOrderMaintainRequestPartyParty()
            {
                PartyID = new Orden.PartyID() { Value = "C0001889" }
            };

            serviceOrderMaintainRequest.RequestedFulfillmentPeriodPeriodTerms = new Orden.ServiceOrderMaintainRequestRequestedFulfillmentPeriodPeriodTerms(
                )
            {
                StartDateTime = new Orden.DateTime() { Value = DateTime.UtcNow }
            };

            serviceOrderMaintainRequest.SalesAndServiceBusinessArea = new Orden.ServiceOrderMaintainRequestSalesAndServiceBusinessArea(
                )
            {
                DistributionChannelCode = new Orden.DistributionChannelCode() { Value = "01" }
            };

            var producto = new Orden.ServiceOrderMaintainRequestServiceReferenceObject()
            {
                ReferenceProductKey =
                    new Orden.ServiceOrderMaintainRequestReferenceProductKey()
                    {
                        ProductID = new Orden.ProductID() { Value = "TVXONE" }
                    },
                RegisteredProductKey =
                    new Orden.ServiceOrderMaintainRequestRegisteredProductKey()
                    {
                        ProductID = new Orden.ProductID() { Value = "357509100005619" }
                    }
            };

            List<Orden.ServiceOrderMaintainRequestServiceReferenceObject> lstProducts = new List<Orden.ServiceOrderMaintainRequestServiceReferenceObject>(
                );

            serviceOrderMaintainRequest.ServiceReferenceObject = lstProducts.ToArray();

            lstServiceOrder.Add(serviceOrderMaintainRequest);

            serviceOrder = lstServiceOrder.ToArray();

            parameter.ServiceOrder = serviceOrder;

            //var response = new GestionOrden.MaintainBundleRequest(parameter);
            var response = new Orden.MaintainBundleRequest()
            {
                ServiceOrderBundleMaintainRequest_sync =
                    new Orden.ServiceOrderBundleMaintainRequest()
                    {
                        ServiceOrder = serviceOrder,
                        BasicMessageHeader = new Orden.BusinessDocumentBasicMessageHeader()
                    }
                //BasicMessageHeader = new GestionOrden.BusinessDocumentBasicMessageHeader()
            };

            try
            {
                var resultado = client.MaintainBundle(response);

                Console.WriteLine(resultado.ToString());
            } catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            } finally
            {
            }
        }
        public files createFormat(Devolucion dev, int format)
        {
            files result = new files();
            //var files = result.Content.Headers;

            var request = RestClientA.Client.POST(new Request()
            {
                Body = createBodyFormat(dev, format),
                Url = iConfiguration["API:URLReportsAPI"],
                Resource = iConfiguration["API:GetReturnById"]
            });

            if(request.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<files>(request.Content);
            }
            else
            {
                throw new ApplicationException("El formato de impresión no pudo ser obtenido de manera exitosa.");
            }

            return result;
        }
        public returns createBodyFormat(Devolucion dev, int format = 0)
        {
            returns ret = new returns();

            ret.returnsid = dev.ClaveDevolucion;
            ret.typedocument = format;
            ret.reasonsend = dev.MotivoEnvio.ToString();

            Devolucion devs = iDevolucionesDA.GetReturnById(dev.ClaveDevolucion);

            //Información del RA//
            Users users = iUserDA.GetUsersAll().Where(a => a.usersid == devs.UserId).FirstOrDefault();

            ret.legalappoderate = users.personname + " " + users.personlastname;
            ret.ra = iUserDA.GetEmpleadosByPersonId(users.personid).FirstOrDefault().TeeID;
            ret.email = users.usersemail;
            ret.numbertelephone = users.usersphone;

            //Cliente
            Clientes clientes = new Clientes();
            clientes = iUserDA.GetClientsAll().Where(a => a.IdCliente == dev.Cliente).FirstOrDefault();

            //Informacion del cliente
            ClientAddress address = new ClientAddress();
            address = iUserDA.GetClientAddressesAll().Where(a => a.Clientid == dev.Cliente.ToString()).FirstOrDefault();

            //Llenamos la informacion del cliente//
            ret.numbertelephoneclient = address.Clientaddressphonenumber;
            ret.clientcode = clientes.ClaveCliente + "-" + clientes.NombreCliente;
            ret.appoderategalclient = dev.Envio;
            ret.emailclient = address.Clientaddressemail;
            ret.address = address.Clientaddressstreetname + " " + address.Clientaddressnumber + "/" + address.Clientaddresspostalcode + " " + address.Clientaddressdistrictname + ", " + address.Clientaddresscityname + ", " + address.Clientaddressregioncode + ", " + address.Clientaddresscountry;
            ret.numbertelephoneclient = address.Clientaddressphonenumber;

            //LLenamos los detalles//
            List<returnsdetails> retlist = new List<returnsdetails>();

            foreach (var item in dev.Details)
            {
                var searchbrandname = iDevolucionesDA.GetProducts().Where(a => a.ProductoId == item.TipoProducto).FirstOrDefault();
                string brandname = searchbrandname == null ? "": searchbrandname.ProductTechnology;

                var searchcarrier = iDevolucionesDA.GetCarriers().Where(a => a.CarrierId == item.CarrierId).FirstOrDefault();
                string carrier = searchcarrier == null ? "" : searchcarrier.CarrierName;

                var searchdataplan = iDevolucionesDA.GetPlanDatas().Where(a => a.PlanDataId == item.PlanId).FirstOrDefault();
                string dataplan = searchdataplan == null ? "" : searchdataplan.PlanDataName;

                retlist.Add(new returnsdetails()
                {
                    callingidenfier = item.IdentificadorLlamadas == true ? "Sí" : "No",
                    gps = item.GPS,
                    groups = item.Grupos,
                    legend = item.Leyenda,
                    name = item.Producto,
                    namefolder = item.NombreCarpeta,
                    observations = item.Observaciones,
                    privatecall = item.LlamadaPrivada,
                    serie = item.Serie,
                    suscribesites = item.SitiosSuscribe,
                    alias = item.Alias,
                    brandname = brandname,
                    carrier = carrier,
                    dataplan = dataplan,
                    dn = item.Dn,
                    evidenceforms = item.EvidenceForms,
                    evidencelite = item.EvidenceLite,
                    group = item.Grupo,
                    isservitron = item.ExisteProducto,
                    sim = item.SIM,
                    simisservitron = item.SimPropiedadServitron,
                    teamvoxdispatch = item.TeamVoxDispatch,
                    teamvoxlite = item.TeamVoxLiteAbierto,
                    teamvoxsecuremode = item.TeamVoxModoSeguro,
                    zaypher = item.Zaypher
                });
            }

            ret.returnsdetails = retlist;

            return ret;
        }

        public Response ConfirmProductAccesory(StockConfirmationDetails details, long productId, int isConfirm, long returnId = 0)
        {
            Response response = new Response();

            //Validar la existencia del ReturnDetailsId en la tabla.
            var returnExistent = GetReturnById(returnId);

            if(returnExistent == null)
            {
                throw new ApplicationException("La devolución asociada con el producto ya no existe dentro del sistema, por lo cual recomendamos cerrar este proceso.");
            }

            //Validamos el status de la devolucion//
            if(returnExistent.Estatus == 2) //No continuar si esta cancelado
            {
                throw new ApplicationException("La devolución se encuentra cancelada. Le recomendamos cerrar este proceso de confirmación de mercancía.");
            }

            if (returnExistent.Estatus == 4) //No continuar si esta rechazado por almacen
            {
                throw new ApplicationException("La devolución se encuentra rechazada por almacén. Le recomendamos cerrar este proceso de confirmación de mercancía y re-aperturar la devolución.");
            }

            if (returnExistent.Details.Where(a => a.IdDetallesDevolucion == productId).Count() == 0)
            {
                throw new ApplicationException("El producto a confirmar ya no se encuentra registrado en la devolución, le recomendamos cerrar esta ventana de confirmación.");

            }

            var detailProduct = returnExistent.Details.Where(a => a.IdDetallesDevolucion == productId).FirstOrDefault();

            if (detailProduct.ExisteProducto == false)
            {
                if(isConfirm == 0)
                {
                    if (string.IsNullOrEmpty(details.StockConfirmationQuantity) || string.IsNullOrEmpty(details.StockConfirmationComments))
                    {
                        throw new ApplicationException("Complete los campos obligatorios.");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(details.StockConfirmationQuantity))
                    {
                        throw new ApplicationException("Complete los campos obligatorios.");
                    }

                    if (Convert.ToInt32(details.StockConfirmationQuantity) < 0)
                    {
                        throw new ApplicationException("Introduzca la cantidad a devolver con un valor mayor a cero.");
                    }
                }
            }


            if (Convert.ToInt64(returnExistent.Details.Where(a => a.IdDetallesDevolucion == productId).FirstOrDefault().Cantidad) < Convert.ToInt64(details.StockConfirmationQuantity))
            {
                throw new ApplicationException("No puede confirmar una cantidad mayor a la reportada.");
            }

            var stockInfo = returnExistent.Details.Where(a => a.IdDetallesDevolucion == productId && (a.StockConfirmation.StatusId == 1 || a.StockConfirmation.StatusId == 2 || a.StockConfirmation.StatusId == 3 || a.StockConfirmation.StatusId == 5)).ToList();

            string quantity = "0";

            if (stockInfo.Count > 0)
            {
                foreach (var itemStock in stockInfo)
                {
                    if(itemStock.QuantityRestante > 0)
                    {
                        if (itemStock.StockConfirmation.StatusId == 1 || itemStock.StockConfirmation.StatusId == 2 || itemStock.StockConfirmation.StatusId == 3 || itemStock.StockConfirmation.StatusId == 5)
                        {
                            foreach (var itemQuantity in itemStock.StockConfirmation.StockDetails.ToList())
                            {
                                quantity = (Convert.ToInt64(quantity) + Convert.ToInt64(itemQuantity.StockConfirmationQuantity)).ToString();
                            }
                        }
                    }
                    
                }
            }

            long sumas = 0;

            if(details.StockConfirmationId != 0)
            {
                sumas = (Convert.ToInt64(quantity)) + Convert.ToInt64(details.StockConfirmationQuantity);
            }
            
            long reportado = (Convert.ToInt64(returnExistent.Details.Where(a => a.IdDetallesDevolucion == productId).FirstOrDefault().Cantidad));
            long restante = (Convert.ToInt64(returnExistent.Details.Where(a => a.IdDetallesDevolucion == productId).FirstOrDefault().QuantityRestante));
            long faltante = (reportado - sumas);

            if(faltante < 0)
            {
                throw new ApplicationException("No puede confirmar una cantidad mayor a la faltante.");
            }

            //Se realiza la actualización del estatus de la devolucion en proceso de almacen.
            returnExistent.Estatus = 3; //En proceso

            iDevolucionesDA.modifyStatusReturn(returnExistent);

            //Se valida la existencia del returnIdDetails//
            bool existConfirm = false;

            if(returnExistent.Details.Where(a => a.IdDetallesDevolucion == productId).FirstOrDefault().StockConfirmation.StatusId > 0)
            {
                existConfirm = true;
            }

            details.StockConfirmationDetailsCreateDate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now);

            if(faltante >= 0)
            {
                iDevolucionesDA.CreateConfirmStock(details, productId, existConfirm, isConfirm);
            }

            response.Codigo = 1;

            response.Mensaje = "El producto/accesorio ha sido rechazado correctamente.";

            if (isConfirm == 1)
            {
                response.Mensaje = "La confirmación del producto/accesorio ha sido realizada correctamente.";
            }
            
            return response;
        }

        public Response modifyStatusReturn(long returnId = 0, int estatus = 0, string ce_uui = "")
        {

            Response response = new Response();

            try
            {
                //Validar la existencia del ReturnDetailsId en la tabla.
                var returnExistent = GetReturnById(returnId);

                //Se realiza la actualización del estatus de la devolucion en proceso de almacen.

                
                //Validar el estatus de sus detalles para validar si aceptar parcialmente o aceptar totalmente la devolucion.

                if (estatus == 1)
                {
                    //Funcion de arbol de decision para saber si será aceptada parcialmente o totalmente//
                    if(confirmPartialOrAll(returnExistent, ce_uui) == 1)
                    {
                        returnExistent.Estatus = 5; //Aceptado por almacen
                    }
                    else
                    {
                        returnExistent.Estatus = 6; //Parcialmente aceptado
                    }
                    
                }
                else
                {
                    returnExistent.Estatus = 4; //Rechazado por almacen
                }

                iDevolucionesDA.modifyStatusReturn(returnExistent);

                response.Codigo = 1;

                if (returnExistent.Estatus == 4)
                {
                    response.Mensaje = "* La devolución ha sido rechazada correctamente.";
                }
                else
                {
                    response.Mensaje = "* La devolución ha sido confirmada correctamente.";
                }
            }
            catch(Exception ex)
            {
                return new Response(){
                    Codigo = 0,
                    Mensaje = ex.Message
                };
            }

            return response;
        }

        public int confirmPartialOrAll(Devolucion devolucion, string ce_uui = "")
        {
            int confirmadas = 0;

            var detallesDevolucion = devolucion.Details;

            var contadorConfirmadas = 0;

            foreach(var item in detallesDevolucion.OrderBy(a => a.TipoProducto).ToArray())
            {
                if(item.StockConfirmation.StatusId == 1)
                {
                    contadorConfirmadas++;
                }
            }

            List<StockConfirmDetails> stockConfirmDetails = new List<StockConfirmDetails>();

            //Obtengos los productos seriados//
            var productsSerials = detallesDevolucion.Where(a => a.EsSoloDevolucion == false && a.ExisteProducto == true && a.StockConfirmation.StatusId == 1).ToList();
            var productsGrouping = productsSerials.GroupBy(a => a.TipoProducto).ToList();

            int orderId = 1;

            //Traer todos los stock confirm que existen.
            var stockConfirm = iDevolucionesDA.GetAllStockConfirm().Where(a => a.ReturnId == devolucion.ClaveDevolucion || (a.StatusConfirmStatusId == 1 && a.StatusConfirmStatusId ==2)).ToList();

            for (int i = 0; i< productsGrouping.Count; i++)
            {
                var productSerial = productsSerials.Where(a => a.TipoProducto == Convert.ToInt64(productsGrouping[i].Key));

                foreach(var productSerialItem in productSerial)
                {
                    foreach(var item in productSerialItem.StockConfirmation.StockDetails.Where(a => a.StockConfirmationComments != "-1").ToList())
                    {
                        bool isExistConfirmationId = false;

                        foreach (var itemstockConfirm in stockConfirm)
                        {
                            foreach(var itemDetails in itemstockConfirm.Details)
                            {
                                if (isExistConfirmationId == false)
                                {
                                    if (itemDetails.StockConfirmationDetailsId == item.StockConfirmationDetailsId)
                                    {
                                        isExistConfirmationId = true;
                                    }
                                }
                            }
                        }

                        if (isExistConfirmationId == false)
                        {
                            stockConfirmDetails.Add(new StockConfirmDetails()
                            {
                                ProductId = productSerialItem.TipoProducto,
                                ReturnDetailsId = productSerialItem.IdDetallesDevolucion,
                                StockConfirmationid = productSerialItem.StockConfirmation.StockId,
                                StockConfirmDetailsId = 0,
                                StockConfirmId = 0,
                                StockConfirmOrderId = orderId,
                                StockConfirmationDetailsId = item.StockConfirmationDetailsId,
                                StockQuantity = Convert.ToInt32(item.StockConfirmationQuantity)
                            });
                        }
                    }
                }

                orderId++;
            }

            //Añado los productos no seriados
            var productsNotSerials = detallesDevolucion.Where(a => a.EsSoloDevolucion == true && a.StockConfirmation.StatusId == 1).ToList();

            foreach(var product in productsNotSerials)
            {
                foreach (var item in product.StockConfirmation.StockDetails.Where(a => a.StockConfirmationComments != "-1").ToList())
                {
                    bool isExistConfirmationId = false;

                    foreach (var itemstockConfirm in stockConfirm)
                    {
                        foreach (var itemDetails in itemstockConfirm.Details)
                        {
                            if(isExistConfirmationId == false)
                            {
                                if(itemDetails.StockConfirmationDetailsId == item.StockConfirmationDetailsId)
                                {
                                    isExistConfirmationId = true;
                                }
                            }
                        }
                    }

                    if(isExistConfirmationId == false)
                    {
                        stockConfirmDetails.Add(new StockConfirmDetails()
                        {
                            ProductId = product.TipoProducto,
                            ReturnDetailsId = product.IdDetallesDevolucion,
                            StockConfirmationid = product.StockConfirmation.StockId,
                            StockConfirmDetailsId = 0,
                            StockConfirmId = 0,
                            StockConfirmOrderId = orderId,
                            StockConfirmationDetailsId = item.StockConfirmationDetailsId,
                            StockQuantity = Convert.ToInt32(item.StockConfirmationQuantity)
                        });
                    }
                }

                orderId++;
            }

            if (contadorConfirmadas > 0)
            {
                //Se da de alta el folio en la base de datos para poder realizar al final la alta a SAP.
                long stockconfirmId = iDevolucionesDA.createConfirmStock(stockConfirmDetails, devolucion.ClaveDevolucion);

                //Se realiza la ejecución en SAP.
                EntrancyMercancy(devolucion, stockconfirmId, ce_uui);

                //Se actualizan los estatus de los confirmados//
                iDevolucionesDA.updateStatusConfirmation(detallesDevolucion.Where(a => a.StockConfirmation.StatusId == 1).ToList());
            }

            if (contadorConfirmadas == detallesDevolucion.Count())
            {
                confirmadas = 1;
            }

            return confirmadas;
        }

        public void EntrancyMercancy(Devolucion devolucion, long stockConfirmId, string ce_uui = "")
        {
            StockConfirm stockConfirm = iDevolucionesDA.GetStockConfirm(stockConfirmId);

            if (stockConfirm != null)
            {
                //Armamos el cuerpo de la solicitud//
                EntrancyMercancyRequest entrancyMercancyRequest = new EntrancyMercancyRequest();
                entrancyMercancyRequest.ExternalID = "Z002";
                entrancyMercancyRequest.SiteID = "SVTDEV";
                entrancyMercancyRequest.TransactionDateTime = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now);
                entrancyMercancyRequest.IDDevPortal = "DEV-" + devolucion.ClaveDevolucion.ToString();
                entrancyMercancyRequest.InventoryMovementDirectionCode = "2";

                if (ce_uui == "CC")
                {
                    entrancyMercancyRequest.CostCenterID = "SVTCC";
                }
                else
                {
                    entrancyMercancyRequest.CostCenterID = "SVTCO";
                }

                //Obtenemos los valores que son seriados//
                List<StockConfirmDetails> stockConfirmDetails = new List<StockConfirmDetails>();

                //Obtengos los productos seriados//
                var productsSerials = devolucion.Details.Where(a => a.EsSoloDevolucion == false && a.ExisteProducto == true && a.StockConfirmation.StatusId == 1).ToList();
                var productsGrouping = productsSerials.GroupBy(a => a.TipoProducto).ToList();

                int orderId = 1;

                for (int i = 0; i < productsGrouping.Count; i++)
                {
                    var productSerial = productsSerials.Where(a => a.TipoProducto == Convert.ToInt64(productsGrouping[i].Key));

                    foreach (var productSerialItem in productSerial)
                    {
                        stockConfirmDetails.Add(new StockConfirmDetails()
                        {
                            ProductId = productSerialItem.TipoProducto,
                            ReturnDetailsId = productSerialItem.IdDetallesDevolucion,
                            StockConfirmationid = productSerialItem.StockConfirmation.StockId,
                            StockConfirmDetailsId = 0,
                            StockConfirmId = 0,
                            StockConfirmOrderId = orderId,
                            StockQuantity = 1
                            
                        });
                    }

                    orderId++;
                }

                //Añado los productos no seriados
                var productsNotSerials = devolucion.Details.Where(a => a.EsSoloDevolucion == true && a.StockConfirmation.StatusId == 1).ToList();
                var productsGroupingNotSerials = productsNotSerials.GroupBy(a => a.StockConfirmation).ToList();

                foreach (var product in productsNotSerials)
                {
                    long productId = product.TipoProducto;
                    long returnDetailsId = product.IdDetallesDevolucion;
                    int stockConfirmOrderId = orderId;
                    long StockConfirmationid = product.StockConfirmation.StockId;


                    foreach (var productItem in product.StockConfirmation.StockDetails.Where(a => a.StockConfirmationComments != "-1"))
                    {
                        stockConfirmDetails.Add(new StockConfirmDetails()
                        {
                            ProductId = productId,
                            ReturnDetailsId = returnDetailsId,
                            StockConfirmationid = StockConfirmationid,
                            StockConfirmDetailsId = 0,
                            StockConfirmId = 0,
                            StockConfirmOrderId = stockConfirmOrderId,
                            StockConfirmationDetailsId = productItem.StockConfirmationDetailsId,
                            StockQuantity = Convert.ToInt32(productItem.StockConfirmationQuantity)
                        });
                    }

                    orderId++;
                }

                List<EntrancyMercancyRequestDetails> lstDetails = new List<EntrancyMercancyRequestDetails>();

                if(stockConfirmDetails.Count() > 0)
                {
                    //Armamos los detalles de la orden de confirmacion de SAP.
                    int obtainOrderMax = stockConfirmDetails.Max(a => a.StockConfirmOrderId);
                    //bool isExists = false;
                    for(int i = 1; i<=obtainOrderMax; i++)
                    {
                        var item = new EntrancyMercancyRequestDetails();

                        //Validamos si es seriado o no es seriado el orden//
                        var orderSearch = stockConfirmDetails.Where(a => a.StockConfirmOrderId == i).ToList();

                        long sumaQuantity = 0, contadorStocks = 1;

                        foreach (var itemValues in orderSearch.ToList())
                        {
                            if (item.ExternalItemID != itemValues.StockConfirmOrderId.ToString())
                            {
                                contadorStocks = 0;
                                sumaQuantity = 0;
                                sumaQuantity += itemValues.StockQuantity;
                                item = new EntrancyMercancyRequestDetails();
                                //isExists = true;
                                item.ExternalItemID = itemValues.StockConfirmOrderId.ToString();
                                item.InventoryItemChangeQuantity = sumaQuantity;
                                item.LogisticsAreaID = "REC";
                                item.OwnerPartyInternalID = "SVT";
                                item.InventoryRestrictedUseIndicator = false;
                                item.PosDevPortal = itemValues.StockConfirmOrderId.ToString();

                                //Codigo del producto
                                if (devolucion.Details.Where(a => a.IdDetallesDevolucion == itemValues.ReturnDetailsId && a.EsSoloDevolucion == false && a.ExisteProducto == true && a.StockConfirmation.StatusId == 1).Any())
                                {
                                    var productId = iDevolucionesDA.GetProducts().Where(a => a.ProductoId == itemValues.ProductId).FirstOrDefault();

                                    item.MaterialInternalID = productId.ProductCode;
                                }
                                else //Codigo del accesorio//
                                {
                                    var accesoryId = iDevolucionesDA.GetAccessoriesAll().Where(a => a.IdAccesorio == itemValues.ProductId).FirstOrDefault();

                                    if(accesoryId == null)
                                    {
                                        item.MaterialInternalID = "0";
                                    }
                                    else
                                    {
                                        item.MaterialInternalID = accesoryId.IdAccesorio.ToString();
                                    }
                                }

                                contadorStocks++;
                            }
                            else
                            {
                                sumaQuantity += itemValues.StockQuantity;
                                item.InventoryItemChangeQuantity = sumaQuantity;
                                //isExists = false;

                                contadorStocks++;
                            }

                            if(orderSearch.Count() == contadorStocks)
                            {
                                if (item.InventoryItemChangeSerialNumber == null)
                                {
                                    item.InventoryItemChangeSerialNumber = new List<string>();
                                }

                                //Validamos si el producto es seriado o no//
                                if (devolucion.Details.Where(a => a.IdDetallesDevolucion == itemValues.ReturnDetailsId && a.EsSoloDevolucion == false && a.ExisteProducto == true && a.StockConfirmation.StatusId == 1).Any())
                                {
                                    string serial = devolucion.Details.Where(a => a.IdDetallesDevolucion == itemValues.ReturnDetailsId && a.EsSoloDevolucion == false && a.ExisteProducto == true && a.StockConfirmation.StatusId == 1).FirstOrDefault().Serie;

                                    item.InventoryItemChangeSerialNumber.Add(serial);
                                }
                                else
                                {
                                    item.InventoryItemChangeSerialNumber = new List<string>();
                                }

                                lstDetails.Add(item);
                            }

                        }
                    }

                    
                }

                entrancyMercancyRequest.Details = lstDetails;

                //Realizamos la invocación del servicio Rest//

                EntrancyMercancy entrancyMercancy = new EntrancyMercancy();

                var request = RestClientA.Client.POST(new Request()
                {
                    Body = entrancyMercancyRequest,
                    Url = iConfiguration["APISap:URLSAPAPI"],
                    Resource = iConfiguration["APISap:EntrancyMercancy"]
                });

                if (request.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    entrancyMercancy = JsonConvert.DeserializeObject<EntrancyMercancy>(request.Content);
                }
                else
                {
                    entrancyMercancy = JsonConvert.DeserializeObject<EntrancyMercancy>(request.Content);
                }

                //Fin REST

                if(entrancyMercancy.Code == 1)
                {
                    //Si el valor esta actualizado este se actualizará.
                    stockConfirm.StockConfirmSapCode = entrancyMercancy.gACID;
                    stockConfirm.StockConfirmSaUuid = entrancyMercancy.gACUUID;
                    stockConfirm.StatusConfirmStatusId = 1;

                    iDevolucionesDA.UpdateConfirmStock(stockConfirm);
                }
                else
                {
                    //Si algo sale mal en la ejecución esta acción de confirmación se hace revert al movimiento.
                    iDevolucionesDA.DeleteConfirmStock(stockConfirm.StockConfirmId);

                    throw new ApplicationException("La confirmación de la devolución no pudo ser realizada por el siguiente error: " + entrancyMercancy.Log);
                }
            }
        }

        public HistorialStockConfirmation GetHistorialStock(long returnId, long productId)
        {
            HistorialStockConfirmation historialStockConfirmation = new HistorialStockConfirmation();

            var dev = GetReturnById(returnId);

            List<DetallesDevolucion> detDev = new List<DetallesDevolucion>();

            foreach (var item in dev.Details.Where(a => a.IdDetallesDevolucion == productId).ToList())
            {

                var response = iDevolucionesDA.GetStockConfirmation(item.IdDetallesDevolucion);

                if (response == null)
                {
                    historialStockConfirmation = new HistorialStockConfirmation();
                    historialStockConfirmation.StockDetails = new List<HistorialStockConfirmationDetails>();
                }
                else
                {
                    historialStockConfirmation.ReturnDetailsId = response.ReturnDetailsId;
                    historialStockConfirmation.StatusId = iDevolucionesDA.getStatusReturnConfirmation(response.StatusId).EstatusDetallesNombre;
                    historialStockConfirmation.StockId = response.StockId;

                    historialStockConfirmation.StockDetails = new List<HistorialStockConfirmationDetails>();

                    var historial = response.StockDetails.OrderByDescending(a => a.StockConfirmationDetailsId).ToList();

                    foreach (var itemS in historial)
                    {
                        var nameUser = iUserDA.GetUsersAll().Where(a => a.usersid == itemS.UserId).FirstOrDefault();

                        historialStockConfirmation.StockDetails.Add(new HistorialStockConfirmationDetails()
                        {
                            StockConfirmationComments = itemS.StockConfirmationComments == "-1" ? "": itemS.StockConfirmationComments,
                            StockConfirmationDetailsCreateDate = itemS.StockConfirmationDetailsCreateDate,
                            StockConfirmationDetailsId = itemS.StockConfirmationDetailsId,
                            StockConfirmationId = itemS.StockConfirmationId,
                            StockConfirmationQuantity = itemS.StockConfirmationQuantity,
                            UserId = nameUser == null ? "Sin usuario" : nameUser.personname + " " + nameUser.personlastname
                        });
                    }
                }
            }

            return historialStockConfirmation;
        }
    }
}
