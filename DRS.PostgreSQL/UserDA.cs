using DRS.Data;
using DRS.Entities;
using DRS.PostgreSQL.Functions;
using DRS.PostgreSQL.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRS.PostgreSQL
{
    public class UserDA : IUserDA
    {
        public int ClientExists(long employeeId, string CROOT_UUID, string TEE_ID)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return dRS.Clients.Where(a => a.Clientname == CROOT_UUID && a.Clientcode == TEE_ID).Count();
            }
        }

        public bool CreateCliente(string CROOT_UUID, string TEE_ID, int CLIFE_CYCLE_STATUS)
        {
            using (DRSContext dRS = new DRSContext())
            {
                Client client = new Client();

                client.Clientname = TEE_ID;
                client.Clientcode = CROOT_UUID;
                client.Clientstatus = CLIFE_CYCLE_STATUS;

                dRS.Add(client);

                dRS.SaveChanges();

                return true;
            }
        }

        public bool CreateProducto(string ProductCode, string ProductName, string typeTechnology)
        {
            using (DRSContext dRS = new DRSContext())
            {
                Product product = new Product();

                product.Productcode = ProductCode;
                product.Productname = ProductName;
                product.Producttechnology = typeTechnology;

                dRS.Add(product);

                dRS.SaveChanges();

                return true;
            }
        }

        public bool CreateEmployee(string CBUSINESS_PARTNER_ID, string CEE_UUID, string TEE_ID, string typeEMP)
        {

            using (DRSContext dRS = new DRSContext())
            {
                Employee employees = new Employee();

                employees.CbusinessPartnerId = CBUSINESS_PARTNER_ID;
                employees.CeeUuid = CEE_UUID;
                employees.TeeId = TEE_ID;
                employees.TypeEmployee = typeEMP;

                dRS.Add(employees);

                dRS.SaveChanges();

                return true;
            }
        }

        public bool CreateRelClientEmployee(long ClientId, long EmployeeId)
        {
            using (DRSContext dRS = new DRSContext())
            {
                Employeeclient employeeclient = new Employeeclient();

                employeeclient.Clientid = ClientId;
                employeeclient.Employeeid = EmployeeId;

                dRS.Add(employeeclient);

                dRS.SaveChanges();

                return true;
            }
        }

        public int EmployeeExist(string CBUSINESS_PARTNER_ID, string CEE_UUID, string TEE_ID)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return dRS.Employees.Where(a => a.CbusinessPartnerId == CBUSINESS_PARTNER_ID && a.CeeUuid == CEE_UUID).Count();
            }

        }

        public int ExistsRelClientEmployee(long ClientId, long EmployeeId)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return dRS.Employeeclients.Where(a => a.Clientid == ClientId && a.Employeeid == EmployeeId).Count();
            }
        }
        public Clientes GetCliente(string CROOT_UUID, string TEE_ID)
        {
            Clientes clientes = new Clientes();

            using (DRSContext dRS = new DRSContext())
            {
                var response = dRS.Clients.Where(a => a.Clientcode == CROOT_UUID).FirstOrDefault();

                if (response != null)
                {
                    clientes.ClaveCliente = response.Clientcode;
                    clientes.IdCliente = response.Clientid;
                    clientes.NombreCliente = response.Clientname;
                    clientes.ClienteEstatus = response.Clientstatus.ToString();
                }
                else
                {
                    clientes = null;
                }
            }

            return clientes;
        }
        public List<Clientes> GetClienteAll()
        {
            List<Clientes> clientes = new List<Clientes>();

            using (DRSContext dRS = new DRSContext())
            {
                clientes = (from a in dRS.Personemployees
                        join b in dRS.Employees on a.Employeeid equals b.Employeeid
                        join c in dRS.Employeeclients on b.Employeeid equals c.Employeeid
                        join d in dRS.Clients on c.Clientid equals d.Clientid
                        select new Clientes
                        {
                            IdCliente = d.Clientid,
                            NombreCliente = d.Clientname,
                            ClaveCliente = d.Clientcode,
                            PersonaId = a.Personid,
                            ClienteEstatus = d.Clientstatus.ToString()
                        }).ToList();
            }

            return clientes;
        }
        public List<ClientAddress> GetClientAddressesAll()
        {
            List<ClientAddress> clientes = new List<ClientAddress>();

            using (DRSContext dRS = new DRSContext())
            {
                clientes = (from a in dRS.Clientaddresses
                            
                            select new ClientAddress
                            {
                                Clientaddresscityname = a.Clientaddresscityname,
                                Clientaddresscountry = a.Clientaddresscountry,
                                Clientaddressdistrictname = a.Clientaddressdistrictname,
                                Clientaddressemail = a.Clientaddressemail,
                                Clientaddressid = a.Clientaddressid,
                                Clientaddressnumber = a.Clientaddressnumber,
                                Clientaddressphonenumber = a.Clientaddressphonenumber,
                                Clientaddressregioncode = a.Clientaddressregioncode,
                                Clientaddressstreetname = a.Clientaddressstreetname,
                                Clientid = a.Clientid.ToString()
                            }).ToList();
            }

            return clientes;
        }
        public List<Clientes> GetClients(long personId)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Personemployees
                        join b in dRS.Employees on a.Employeeid equals b.Employeeid
                        join c in dRS.Employeeclients on b.Employeeid equals c.Employeeid
                        join d in dRS.Clients on c.Clientid equals d.Clientid
                        where a.Personid == personId
                        select new Clientes
                        {
                            IdCliente = d.Clientid,
                            NombreCliente = d.Clientname,
                            ClaveCliente = d.Clientcode,
                            PersonaId = a.Personid,
                            ClienteEstatus = d.Clientstatus.ToString(),
                            EmployeeId = b.Employeeid
                        }).ToList();
            }
        }
        public List<Clientes> GetClientsAll()
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Clients

                        select new Clientes
                        {
                            IdCliente = a.Clientid,
                            NombreCliente = a.Clientname,
                            ClaveCliente = a.Clientcode,
                            PersonaId = 0,
                            ClienteEstatus = a.Clientid.ToString()
                        }).ToList();
            }
        }
        public List<Clientes> GetClientsByPersonId(long personId)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Personemployees
                        join b in dRS.Employees on a.Employeeid equals b.Employeeid
                        join c in dRS.Employeeclients on b.Employeeid equals c.Employeeid
                        join d in dRS.Clients on c.Clientid equals d.Clientid
                        select new Clientes
                        {
                            IdCliente = d.Clientid,
                            NombreCliente = d.Clientname,
                            ClaveCliente = d.Clientcode,
                            PersonaId = a.Personid,
                            ClienteEstatus = d.Clientstatus.ToString()
                        }).Where(a => a.PersonaId == personId).ToList();
            }
        }
        public Empleados GetEmployee(string ClientCode)
        {
            Empleados empleados = new Empleados();

            using (DRSContext dRS = new DRSContext())
            {
                var response = dRS.Employees.Where(a => a.CeeUuid == ClientCode).FirstOrDefault();

                if (response != null)
                {
                    empleados.BusinessPartnerId = response.CbusinessPartnerId;
                    empleados.CeeUUID = response.CeeUuid;
                    empleados.EmployeeId = response.Employeeid;
                    empleados.TeeID = response.TeeId;
                    empleados.TypeEmployee = response.TypeEmployee;
                }
                else
                {
                    empleados = null;
                }
            }

            return empleados;
        }

        public Productos GetProduct(string productId, string typeTechnology = "")
        {

            using (DRSContext dRS = new DRSContext())
            {
                return
                    (from a in dRS.Products
                     select new Productos
                     {
                         ProductoId = a.Productid,
                         ProductCode = a.Productcode,
                         ProductName = a.Productname,
                         ProductTechnology = a.Producttechnology

                     }).Where(a => a.ProductCode == productId && a.ProductTechnology == typeTechnology).FirstOrDefault();

            }

        }

        public Users GetUserForLogin(Entities.Login login)
        {

            //List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();

            ////Construimos la consulta a realizar.
            //StringBuilder str = new StringBuilder();
            //str.Append("select * from get_login(@EmailUser, @PassUser);");

            ////Parametros
            //npgsqlParameters.Add(new NpgsqlParameter()
            //{
            //    ParameterName = "@EmailUser",
            //    NpgsqlDbType = NpgsqlDbType.Varchar,
            //    NpgsqlValue = DBNull.Value,
            //    Value = login.Username
            //});
            //npgsqlParameters.Add(new NpgsqlParameter()
            //{
            //    ParameterName = "@PassUser",
            //    NpgsqlDbType = NpgsqlDbType.Varchar,
            //    NpgsqlValue = DBNull.Value,
            //    Value = login.Password
            //});

            using(var dRS = new DRSContext())
            {
                var response = (from a in dRS.Users
                                join b in dRS.People on a.Personid equals b.Personid
                                join c in dRS.Userskeys on a.Usersid equals c.Usersid
                                where a.Usersemail == login.Username && c.Userskeysenabled == 1 && c.Userskeyspassword == login.Password
                                select new Users
                                {
                                    cee_uuid = new List<PersonEmployee>(),
                                    clients = new List<Clientes>(),
                                    personid = b.Personid,
                                    personlastname = b.Personname,
                                    personname = b.Personname,
                                    statusId = a.Userstatusid,
                                    usersemail = a.Usersemail,
                                    usersid = a.Usersid,
                                    usersphone = a.Usersphonenumber
                                }).OrderByDescending(a => a.usersid).FirstOrDefault();

                if (response != null)
                {
                    //Obtenemos los PersonEmployeeAsignados//
                    response.cee_uuid = (from a in dRS.Personemployees
                                         join b in dRS.Employees on a.Employeeid equals b.Employeeid
                                         where a.Personid == response.personid
                                          select new PersonEmployee
                                          {
                                              CBusinessPartnerId = b.CbusinessPartnerId,
                                              CeeUui = b.CeeUuid,
                                              EmployeeId = b.Employeeid,
                                              TeeUi = b.TeeId,
                                              TypeEmployee = b.TypeEmployee
                                          }).ToList();

                    //Clientes 
                    response.clients = (from a in dRS.People
                                        join b in dRS.Personemployees on a.Personid equals b.Personid
                                        join c in dRS.Employees on b.Employeeid equals c.Employeeid
                                        join d in dRS.Employeeclients on c.Employeeid equals d.Employeeid
                                        join e in dRS.Clients on d.Clientid equals e.Clientid
                                        where a.Personid == response.personid
                                        select new Clientes
                                        {
                                            ClaveCliente = e.Clientcode,
                                            ClienteEstatus = e.Clientstatus.ToString(),
                                            IdCliente = e.Clientid,
                                            NombreCliente = e.Clientname,
                                            PersonaId = a.Personid
                                        }
                                        ).ToList();
                                       
                }

                return response;
            }

            //Realizamos la consulta
            //return new DbContextExtensions().ConvertDataTable<Users>(new DbContextExtensions().SqlQueryAsync(str.ToString(), npgsqlParameters)).FirstOrDefault();
        }

        public Users GetUserForLoginExternal(string email)
        {

            using (var dRS = new DRSContext())
            {
                var response = (from a in dRS.Users
                                join b in dRS.People on a.Personid equals b.Personid
                                where a.Usersemail == email 
                                select new Users
                                {
                                    cee_uuid = new List<PersonEmployee>(),
                                    clients = new List<Clientes>(),
                                    personid = b.Personid,
                                    personlastname = b.Personname,
                                    personname = b.Personname,
                                    statusId = a.Userstatusid,
                                    usersemail = a.Usersemail,
                                    usersid = a.Usersid,
                                    usersphone = a.Usersphonenumber
                                }).OrderByDescending(a => a.usersid).FirstOrDefault();

                if (response != null)
                {
                    //Obtenemos los PersonEmployeeAsignados//
                    response.cee_uuid = (from a in dRS.Personemployees
                                         join b in dRS.Employees on a.Employeeid equals b.Employeeid
                                         where a.Personid == response.personid
                                         select new PersonEmployee
                                         {
                                             CBusinessPartnerId = b.CbusinessPartnerId,
                                             CeeUui = b.CeeUuid,
                                             EmployeeId = b.Employeeid,
                                             TeeUi = b.TeeId,
                                             TypeEmployee = b.TypeEmployee
                                         }).ToList();

                    //Clientes 
                    response.clients = (from a in dRS.People
                                        join b in dRS.Personemployees on a.Personid equals b.Personid
                                        join c in dRS.Employees on b.Employeeid equals c.Employeeid
                                        join d in dRS.Employeeclients on c.Employeeid equals d.Employeeid
                                        join e in dRS.Clients on d.Clientid equals e.Clientid
                                        where a.Personid == response.personid
                                        select new Clientes
                                        {
                                            ClaveCliente = e.Clientcode,
                                            ClienteEstatus = e.Clientstatus.ToString(),
                                            IdCliente = e.Clientid,
                                            NombreCliente = e.Clientname,
                                            PersonaId = a.Personid
                                        }
                                        ).ToList();

                }

                return response;
            }

            //List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();

            ////Construimos la consulta a realizar.
            //StringBuilder str = new StringBuilder();
            //str.Append("select * from get_loginexternal(@EmailUser);");

            ////Parametros
            //npgsqlParameters.Add(new NpgsqlParameter()
            //{
            //    ParameterName = "@EmailUser",
            //    NpgsqlDbType = NpgsqlDbType.Varchar,
            //    NpgsqlValue = DBNull.Value,
            //    Value = email
            //});


            ////Realizamos la consulta
            //return new DbContextExtensions().ConvertDataTable<Users>(new DbContextExtensions().SqlQueryAsync(str.ToString(), npgsqlParameters)).FirstOrDefault();
        }

        public int ProductExists(string productId, string productName, string typeTechnology = "")
        {
            using (DRSContext dRS = new DRSContext())
            {
                return dRS.Products.Where(a => a.Productcode == productId && a.Producttechnology == typeTechnology).Count();
            }
        }

        public bool UpdateClient(long idCliente, string tEE_ID)
        {
            using (DRSContext dRS = new DRSContext())
            {
                var response = dRS.Clients.Where(a => a.Clientid == idCliente).FirstOrDefault();

                response.Clientname = tEE_ID;

                dRS.Update(response);

                dRS.SaveChanges();
            }

            return true;
        }

        public bool UpdateEmployee(string CBUSINESS_PARTNER_ID, string CEE_UUID, string TEE_ID, string typeEMP)
        {

            using (DRSContext dRS = new DRSContext())
            {
                var response = dRS.Employees.Where(a => a.CeeUuid == CEE_UUID).FirstOrDefault();

                response.TeeId = TEE_ID;

                dRS.Update(response);

                dRS.SaveChanges();
            }

            return true;
        }

        public bool UpdateProduct(long productoId, string productName, string productCode, string typeTechnology)
        {
            using (DRSContext dRS = new DRSContext())
            {
                var response = dRS.Products.Where(a => a.Productid == productoId).FirstOrDefault();

                response.Productcode = productCode;
                response.Productname = productName;
                response.Producttechnology = typeTechnology;

                dRS.Update(response);

                dRS.SaveChanges();
            }

            return true;
        }

        public int ExistsRelClientProduct(long idCliente, int product, string serie = "", string imsi = "", string number = "", string sim = "")
        {
            using (DRSContext dRS = new DRSContext())
            {
                return dRS.Productclients.Where(a => a.Productid == product && a.Clientid == idCliente && a.Serie == serie && a.Imsi == imsi && a.Number == number && a.Sim == sim).Count();
            }
        }

        public bool CreateRelClientProduct(long idCliente, int product, string serial = "", string imsi = "", string number = "", string sim = "")
        {
            using (DRSContext dRS = new DRSContext())
            {
                Productclient productclient = new Productclient();

                productclient.Clientid = idCliente;
                productclient.Productid = product;
                productclient.Serie = serial;
                productclient.Imsi = imsi;
                productclient.Number = number;
                productclient.Sim = sim;

                dRS.Add(productclient);

                dRS.SaveChanges();

                return true;
            }

        }

        public UserRole GetUsersForByPersonId(long personId)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.People
                        join b in dRS.Users on a.Personid equals b.Personid
                        join c in dRS.Userroles on b.Userrole equals c.Userroleid
                        where a.Personid == personId
                        select new UserRole
                        {
                            UserRoleId = c.Userroleid,
                            UserRoleName = c.Userrolename

                        }).FirstOrDefault();
            }
        }

        public List<Users> GetUsersAll()
        {
            using(var dRS = new DRSContext())
            {
                var resultado = (from a in dRS.People
                        join b in dRS.Users on a.Personid equals b.Personid
                        select new Users
                        {
                            personid = a.Personid,
                            personlastname = a.Personlastname,
                            personname = a.Personname,
                            usersid = b.Usersid,
                            usersemail = b.Usersemail,
                            usersphone = b.Usersphonenumber,
                            statusId = b.Userstatusid,
                            roleId = b.Userrole
                        }
                        ).ToList();
                return resultado;
            }

        }

        public List<Empleados> GetEmployeeFromSAP()
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Employees
                        select new Empleados
                        {
                            BusinessPartnerId = a.CbusinessPartnerId,
                            CeeUUID = a.CeeUuid,
                            EmployeeId = a.Employeeid,
                            TeeID = a.TeeId,
                            TypeEmployee = a.TypeEmployee

                        }).ToList();
            }
        }

        public List<EmpleadosPersona> GetEmployeeByPersonId(long personId)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Personemployees 
                        join b in dRS.Employees on a.Employeeid equals b.Employeeid
                        where a.Personid == personId
                        select new EmpleadosPersona
                        {
                            IdEmpleado = a.Employeeid,
                            IdPersona = a.Personid,
                            NombreEmpleado = b.TeeId
                        }).ToList();
            }
        }
        public List<Empleados> GetEmpleadosByPersonId(long personId = 0)
        {
            using(var dRS = new DRSContext())
            {
                return (from a in dRS.Employees
                        join b in dRS.Personemployees
                        on a.Employeeid equals b.Employeeid
                        where b.Personid == personId
                        select new Empleados
                        {
                            BusinessPartnerId = a.CbusinessPartnerId,
                            CeeUUID = a.CeeUuid,
                            EmployeeId = a.Employeeid,
                            TeeID = a.TeeId,
                            TypeEmployee = a.TypeEmployee
                        }).ToList();
            }
        }
        public bool UpdateClientStatus(long idCliente, int CLIFE_CYCLE_STATUS)
        {
            using (DRSContext dRS = new DRSContext())
            {
                var response = dRS.Clients.Where(a => a.Clientid == idCliente).FirstOrDefault();

                response.Clientstatus = CLIFE_CYCLE_STATUS;

                dRS.Update(response);

                dRS.SaveChanges();
            }

            return true;
        }

        public Entities.Rest.ResultsAccesories AccessoryExists(string cMATR_UUID, string cSALESOMATIONC0CA0DF3B7723DDD)
        {
            using (DRSContext dRS = new DRSContext())
            {
                return (from a in dRS.Accessories
                        where a.Accessoriescode == cMATR_UUID && a.Accessoriesunit == cSALESOMATIONC0CA0DF3B7723DDD
                        select new Entities.Rest.ResultsAccesories
                        {
                            CMATR_UUID = a.Accessoriescode,
                            CSALESOMATIONC0CA0DF3B7723DDD = a.Accessoriesunit,
                            Cs1ANsB5B1E594DE71377 = a.Accessoriesnamecomplete,
                            CSTATUSMATION08F89FA6D3D769C1 = a.Accessoriesstatus.ToString(),
                            TMATR_UUID = a.Accessoriesname
                        }).FirstOrDefault();


            }
        }

        public bool UpdateAccessory(string cMATR_UUID, string cSALESOMATIONC0CA0DF3B7723DDD, string cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377, string cs1ANsB5B1E594DE71377, string tMATR_UUID)
        {
            bool res = true;

            using (DRSContext dRS = new DRSContext())
            {
                var accessory = dRS.Accessories.Where(a => a.Accessoriescode == cMATR_UUID && a.Accessoriesunit == cSALESOMATIONC0CA0DF3B7723DDD).FirstOrDefault();

                accessory.Accessoriesstatus = Convert.ToInt32(cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377);
                accessory.Accessoriesnamecomplete = cs1ANsB5B1E594DE71377;
                accessory.Accessoriesname = tMATR_UUID;

                try
                {
                    dRS.Update(accessory);

                    dRS.SaveChanges();

                    res = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    res = false;
                }


            }

            return res;
        }

        public bool CreateAccessory(string cMATR_UUID, string cSALESOMATIONC0CA0DF3B7723DDD, string cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377, string cs1ANsB5B1E594DE71377, string tMATR_UUID)
        {
            bool res = false;

            using (DRSContext dRS = new DRSContext())
            {
                Accessory accessory = new Accessory();

                accessory.Accessoriescode = cMATR_UUID;
                accessory.Accessoriesunit = cSALESOMATIONC0CA0DF3B7723DDD;
                accessory.Accessoriesstatus = Convert.ToInt32(cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377);
                accessory.Accessoriesnamecomplete = cs1ANsB5B1E594DE71377;
                accessory.Accessoriesname = tMATR_UUID;

                try
                {
                    dRS.Add(accessory);

                    dRS.SaveChanges();

                    res = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    res = false;
                }

                return res;
            }
        }

        public bool UpdateRelClientProduct(List<ProductsClients> lstUpdtFinal)
        {
            bool resultado = true;

            List<Productclient> lst = new List<Productclient>();
            Productclient productclient = new Productclient();

            using (var dRS = new DRSContext())
            {
                var productsClient = dRS.Productclients.Where(a => a.Clientid == lstUpdtFinal.FirstOrDefault().ClientId).ToList();

                foreach (var client in productsClient)
                {
                    var productclientNewData = lstUpdtFinal.Where(a => a.Serie == client.Serie).FirstOrDefault();

                    productclient = productsClient.Where(a => a.Serie == client.Serie).FirstOrDefault();
                    productclient.Imsi = productclientNewData.Imsi;
                    productclient.Number = productclientNewData.Number;
                    productclient.Serie = productclientNewData.Serie;
                    productclient.Sim = productclientNewData.Sim;
                    productclient.Planname = productclientNewData.PlanDataName;
                    productclient.Carriername = productclientNewData.CarrierName;
                    productclient.Consoleactive = productclientNewData.ConsoleActive;
                    productclient.Planid = string.IsNullOrEmpty(productclientNewData.PlanDataId) ? 0 : Convert.ToInt32(productclientNewData.PlanDataId);
                    productclient.Carrierid = string.IsNullOrEmpty(productclientNewData.CarrierId) ? 0 : Convert.ToInt32(productclientNewData.CarrierId);

                    try
                    {
                        dRS.Update(productclient);
                    }
                    catch (Exception ex)
                    {
                        new LogDA().CreateIncident(new LogIncidencia()
                        {
                            Descripcion = "El producto " + productclientNewData.ProductCode + " perteneciente al cliente " + productclientNewData.ClientId + " NO fue actualizado. Error: " + ex.InnerException.Message + ".",
                            FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                            IncidenciaId = 0,
                            UsuarioId = 0
                        });

                        //Console.WriteLine(ex.InnerException);
                        return false;
                    }

                }

                try
                {
                    dRS.SaveChanges();
                }
                catch (Exception ex)
                {
                    new LogDA().CreateIncident(new LogIncidencia()
                    {
                        Descripcion = "Los productos perteneciente al cliente " + productsClient.FirstOrDefault().Clientid + " NO fueron actualizados. Error: " + ex.InnerException.Message + ".",
                        FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                        IncidenciaId = 0,
                        UsuarioId = 0
                    });

                    //Console.WriteLine(ex.InnerException);
                    return false;
                }


                resultado = true;
            }

            return resultado;
        }

        public List<ProductsClients> GetProductsRegisteredForClientCode(string cROOT_UUID)
        {
            using (var dRS = new DRSContext())
            {
                return (from a in dRS.Productclients
                        join b in dRS.Products on a.Productid equals b.Productid
                        join c in dRS.Clients on a.Clientid equals c.Clientid
                        where c.Clientcode == cROOT_UUID
                        select new ProductsClients
                        {
                            Imsi = a.Imsi,
                            Number = a.Number,
                            ProductCode = b.Productcode,
                            ProductName = b.Productname,
                            Serie = a.Serie,
                            Sim = a.Sim,
                            ClientId = a.Clientid,
                            ProductId = a.Productid,
                            PlanDataId = a.Planid.ToString(),
                            PlanDataName = a.Planname,
                            CarrierId = a.Carrierid.ToString(),
                            CarrierName = a.Carriername,
                            ConsoleActive = a.Consoleactive
                        }).ToList();
            }
        }

        public int CreateProducts(List<ProductsClients> newProductsForClient)
        {
            List<Productclient> prds = new List<Productclient>();

            long idNext = 0;

            using (var drs = new DRSContext())
            {
                if (drs.Productclients.Count() > 0)
                {
                    idNext = drs.Productclients.Max(a => a.Productclientid);
                }

                try
                {
                    foreach (var item in newProductsForClient)
                    {
                        try
                        {
                            drs.Add(new Productclient()
                            {
                                Clientid = item.ClientId,
                                Imsi = item.Imsi,
                                Number = item.Number,
                                Productid = item.ProductId,
                                Serie = item.Serie,
                                Sim = item.Sim,
                                Planid = string.IsNullOrEmpty(item.PlanDataId) ? 0 : Convert.ToInt32(item.PlanDataId),
                                Planname = item.PlanDataName,
                                Carrierid = string.IsNullOrEmpty(item.CarrierId) ? 0 : Convert.ToInt32(item.CarrierId),
                                Carriername = item.CarrierName,
                                Consoleactive = item.ConsoleActive
                            });
                        }
                        catch (Exception ex)
                        {
                            new LogDA().CreateIncident(new LogIncidencia()
                            {
                                Descripcion = "El producto " + item.ProductCode + " perteneciente al cliente " + item.ClientId + " NO fueron creados. Error: " + ex.InnerException.Message + ".",
                                FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                                IncidenciaId = 0,
                                UsuarioId = 0
                            });

                            //Console.WriteLine(ex.InnerException);
                            return 0;
                        }

                    }

                    drs.SaveChanges();
                }
                catch (Exception ex)
                {
                    new LogDA().CreateIncident(new LogIncidencia()
                    {
                        Descripcion = "Ocurrió un error el realizar el alta de los productos para el cliente " + newProductsForClient.FirstOrDefault().ClientId.ToString() + ". Error: " + ex.InnerException.Message,
                        FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                        IncidenciaId = 0,
                        UsuarioId = 0
                    });

                    //Console.WriteLine(ex.InnerException);
                    return 0;
                }


            }

            return 1;
        }

        public bool DeleteRelClientProduct(List<ProductsClients> lstDelFinal)
        {
            bool resultado = true;

            List<Productclient> lst = new List<Productclient>();
            Productclient productclient = new Productclient();

            using (var dRS = new DRSContext())
            {
                var productsClient = dRS.Productclients.Where(a => a.Clientid == lstDelFinal.FirstOrDefault().ClientId).ToList();

                foreach (var client in productsClient)
                {
                    var productclientDelData = lstDelFinal.Where(a => a.Serie == client.Serie).FirstOrDefault();

                    productclient = productsClient.Where(a => a.Serie == client.Serie).FirstOrDefault();

                    try
                    {
                        dRS.Remove(productclient);
                    }
                    catch (Exception ex)
                    {
                        new LogDA().CreateIncident(new LogIncidencia()
                        {
                            Descripcion = "El producto " + productclientDelData.ProductCode + " perteneciente al cliente " + productclientDelData.ClientId + " NO fue eliminado. Error: " + ex.InnerException.Message + ".",
                            FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                            IncidenciaId = 0,
                            UsuarioId = 0
                        });

                        //Console.WriteLine(ex.InnerException);
                        return false;
                    }

                }

                try
                {
                    dRS.SaveChanges();
                }
                catch (Exception ex)
                {
                    new LogDA().CreateIncident(new LogIncidencia()
                    {
                        Descripcion = "Los productos perteneciente al cliente " + productsClient.FirstOrDefault().Clientid + " NO fueron eliminados. Error: " + ex.InnerException.Message + ".",
                        FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                        IncidenciaId = 0,
                        UsuarioId = 0
                    });

                    //Console.WriteLine(ex.InnerException);
                    return false;
                }


                resultado = true;
            }

            return resultado;
        }

        public void AddAddress(List<ClientAddress> newclientsA)
        {
            using(var dRS = new DRSContext())
            {
                using (var drsTran = dRS.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var client in newclientsA)
                        {
                            //Comentarios//
                            long AddressId = 1;

                            if (dRS.Clientaddresses.Count() > 0)
                            {
                                AddressId = dRS.Clientaddresses.Max(a => a.Clientaddressid) + 1;
                            }

                            dRS.Add(new Clientaddress()
                            {
                                Clientaddresscityname = client.Clientaddresscityname,
                                Clientaddresscountry = client.Clientaddresscountry,
                                Clientaddressid = AddressId,
                                Clientaddressdistrictname = client.Clientaddressdistrictname,
                                Clientaddressemail = client.Clientaddressemail,
                                Clientaddressnumber = client.Clientaddressnumber,
                                Clientaddressphonenumber = client.Clientaddressphonenumber,
                                Clientaddressregioncode = client.Clientaddressregioncode,
                                Clientaddressstreetname = client.Clientaddressstreetname,
                                Clientid = Convert.ToInt64(client.Clientid),
                                Clientaddresspostalcode = client.Clientaddresspostalcode,
                                Clientaddresslegalname = client.Clientaddressappoderatename
                            });

                            dRS.SaveChanges();
                        }

                        
                        drsTran.Commit();
                    }
                    catch(Exception ex)
                    {
                        drsTran.Rollback();
                        throw new ApplicationException(ex.Message);
                    }
                }
            }
        }

        public void ModifyAddress(List<ClientAddress> modifyclientsA)
        {
            using (var dRS = new DRSContext())
            {
                using (var drsTran = dRS.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var client in modifyclientsA)
                        {
                            var addressE = dRS.Clientaddresses.Where(a => a.Clientid == Convert.ToInt64(client.Clientid)).FirstOrDefault();

                            addressE.Clientaddresscityname = client.Clientaddresscityname;
                            addressE.Clientaddresscountry = client.Clientaddresscountry;
                            addressE.Clientaddressdistrictname = client.Clientaddressdistrictname;
                            addressE.Clientaddressemail = client.Clientaddressemail;
                            addressE.Clientaddressnumber = client.Clientaddressnumber;
                            addressE.Clientaddressphonenumber = client.Clientaddressphonenumber;
                            addressE.Clientaddressregioncode = client.Clientaddressregioncode;
                            addressE.Clientaddressstreetname = client.Clientaddressstreetname;
                            addressE.Clientaddresspostalcode = client.Clientaddresspostalcode;
                            addressE.Clientaddresslegalname = client.Clientaddressappoderatename;

                            dRS.Update(addressE);

                            dRS.SaveChanges();
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
        }

        public Response createRecoveryAccess(RecoveryRequest recoveryRequest)
        {
            Response response = new Response();

            try
            {
                using (DRSContext dRS = new DRSContext())
                {
                    Models.Recovery recovery = new Models.Recovery();

                    long id = 1;

                    if(dRS.Recoveries.Count() > 0)
                    {
                        id = dRS.Recoveries.Max(a => a.Recoveryid) + 1;
                    }

                    recovery.Recoveryid = id;
                    recovery.Recoverykey = recoveryRequest.Key;
                    recovery.Recoverycreatedate = recoveryRequest.Created;
                    recovery.Recoverystatus = recoveryRequest.Status;
                    recovery.Usersid = recoveryRequest.UserId;

                    dRS.Add(recovery);

                    dRS.SaveChanges();

                    response.Codigo = 1;
                    response.Mensaje = "OK";
                }
            }

            catch (Exception ex)
            {
                response.Codigo = 0;
                response.Mensaje = ex.Message;
            }

            return response;


        }

        public RecoveryRequest getRecoveryAccess(string key)
        {
            using(var dRS = new DRSContext())
            {
                return (from a in dRS.Recoveries
                        where a.Recoverykey == key
                        select new RecoveryRequest
                        {
                            Created = a.Recoverycreatedate,
                            Id = a.Recoveryid,
                            Key = a.Recoverykey,
                            Status = a.Recoverystatus,
                            UserId = a.Usersid
                        }).FirstOrDefault();
            }

            
        }

        public Response ChangePassword(AccountRecovery accountRecoverycopy)
        {
            Response response = new Response();

            try
            {
                using (DRSContext dRS = new DRSContext())
                {
                    
                    
                    using (var drsTran = dRS.Database.BeginTransaction())
                    {
                        try
                        {
                            //La password anterior se inhabilita//
                            var password = dRS.Userskeys.Where(a => a.Usersid == accountRecoverycopy.UserId && a.Userskeysenabled == 1).ToList();

                            foreach(var item in password)
                            {
                                item.Userskeysenabled = 0;

                                dRS.Update(item);
                            }

                            dRS.SaveChanges();

                            //Se crea la nueva contraseña
                            long idUserkEYS = 1;

                            if(dRS.Userskeys.Count() > 0)
                            {
                                idUserkEYS = dRS.Userskeys.Max(A => A.Userskeysid) + 1;
                            }

                            Models.Userskey users = new Models.Userskey()
                            {
                                Userskeyscreatedate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                                Userskeysid = idUserkEYS,
                                Userskeyspassword = accountRecoverycopy.Password,
                                Userskeysenabled = 1,
                                Usersid = accountRecoverycopy.UserId
                            };

                            dRS.Add(users);

                            dRS.SaveChanges();

                            if (!string.IsNullOrEmpty(accountRecoverycopy.Key))
                            {
                                //Inhabilitamos la key para no volverse a usar.
                                //La password anterior se inhabilita//
                                var recoveries = dRS.Recoveries.Where(a => a.Recoverykey == accountRecoverycopy.Key).ToList();

                                foreach (var item in recoveries)
                                {
                                    item.Recoverystatus = 0;

                                    dRS.Update(item);
                                }

                                dRS.SaveChanges();
                            }

                            drsTran.Commit();
                        }
                        catch (Exception ex)
                        {
                            drsTran.Rollback();

                            return new Response()
                            {
                                Codigo = 0,
                                Mensaje = ex.Message
                            };
                        }

                    }

                    response.Codigo = 1;
                    response.Mensaje = "OK";
                }
            }

            catch (Exception ex)
            {
                response.Codigo = 0;
                response.Mensaje = ex.Message;
            }

            return response;


        }

        public bool passwordIsUsed(string password, long userId)
        {
            bool resultado = false;

            using(var dRS = new DRSContext())
            {
                if(dRS.Userskeys.Where(a => a.Usersid == userId && a.Userskeyspassword == password).Count() > 0)
                {
                    resultado = true;
                }
            }

            return resultado;
        }

        public List<UsersViewModel> GetUsersForIndex()
        {
           using(var drs = new DRSContext())
            {
                return (from a in drs.Users
                        join b in drs.People on a.Personid equals b.Personid
                        join c in drs.Userstatuses on a.Userstatusid equals c.Userstatusid
                        join d in drs.Userroles on a.Userrole equals d.Userroleid
                        select new UsersViewModel
                        {
                            Useremail = a.Usersemail,
                            UserId = a.Usersid,
                            Username = String.Concat(b.Personlastname, ' ', b.Personname),
                            Userrole = d.Userrolename,
                            Userstatus = c.Userstatusname,
                            Userstatusid = c.Userstatusid
                        }).ToList();
            }
        }

        public Response Create(Entities.Person personCreate)
        {
            Response response = new Response();

            try
            {
                using (DRSContext dRS = new DRSContext())
                {


                    using (var drsTran = dRS.Database.BeginTransaction())
                    {
                        try
                        {
                            //Creamos la persona en el sistema//
                            Models.Person person = new Models.Person();

                            //Se crea la nueva contraseña
                            long idPerson = 1;

                            if (dRS.People.Count() > 0)
                            {
                                idPerson = dRS.People.Max(A => A.Personid) + 1;
                            }

                            person.Personid = idPerson;
                            person.Personname = personCreate.Personname;
                            person.Personlastname = personCreate.Personlastname;
                            person.Personisemployee = true;
                            person.Persongender = "";

                            dRS.Add(person);

                            dRS.SaveChanges();

                            //Creamos la relación de los empleados con las personas//
                            foreach(var item in personCreate.Personemployees)
                            {
                                Personemployee personemployee = new Personemployee()
                                {
                                    Personid = idPerson,
                                    Employeeid = item.EmployeeId
                                };

                                dRS.Add(personemployee);
                            }

                            dRS.SaveChanges();

                            //Creamos el usuario con los accesos al sistema
                            Models.User user = new Models.User();

                            //Se crea la nueva contraseña
                            long idUser = 1;

                            if (dRS.Users.Count() > 0)
                            {
                                idUser = dRS.Users.Max(A => A.Personid) + 1;
                            }

                            user.Userstatusid = 1;
                            user.Usersemail = personCreate.Account.Email;
                            user.Usersphonenumber = personCreate.Account.PhoneNumber;
                            user.Personid = idPerson;
                            user.Usersid = idUser;
                            user.Userrole = personCreate.UserRoleId;

                            dRS.Add(user);
                            dRS.SaveChanges();

                            //Se crea la nueva contraseña
                            long idUsersKeys = 1;

                            if (dRS.Userskeys.Count() > 0)
                            {
                                idUsersKeys = dRS.Userskeys.Max(A => A.Userskeysid) + 1;
                            }

                            Models.Userskey userKey = new Models.Userskey()
                            {
                                Userskeyscreatedate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                                Userskeysid = idUsersKeys,
                                Userskeyspassword = personCreate.Account.Password,
                                Userskeysenabled = 1,
                                Usersid = idUser
                            };
                           
                            dRS.Add(userKey);
                            dRS.SaveChanges();

                            //Se crean los datos de perfil del RA
                            long idRAProfile = 1;

                            if (dRS.Raprofiles.Count() > 0)
                            {
                                idRAProfile = dRS.Raprofiles.Max(A => A.Raprofileid) + 1;
                            }

                            Raprofile raprofile = new Raprofile()
                            {
                                Raprofileid = idRAProfile,
                                Defaultserviceorderid = personCreate.Raprofile.Defaultserviceorderid,
                                Distributionchannelid = personCreate.Raprofile.Distributionchannelid,
                                Locationidavailable = personCreate.Raprofile.Locationidavailable,
                                Locationidquality = personCreate.Raprofile.Locationidquality,
                                Locationidrepair = personCreate.Raprofile.Locationidrepair,
                                Locationidresidence = personCreate.Raprofile.Locationidresidence,
                                Personid = idPerson,
                                Taxresidenceid = personCreate.Raprofile.Taxresidenceid,
                                Unitsalesid = personCreate.Raprofile.Unitsalesid
                            };

                            dRS.Add(raprofile);
                            dRS.SaveChanges();

                            drsTran.Commit();
                        }
                        catch (Exception ex)
                        {
                            drsTran.Rollback();

                            return new Response()
                            {
                                Codigo = 0,
                                Mensaje = ex.Message
                            };
                        }

                    }

                    response.Codigo = 1;
                    response.Mensaje = "OK";
                }
            }

            catch (Exception ex)
            {
                response.Codigo = 0;
                response.Mensaje = ex.Message;
            }

            return response;


        }

        public List<PersonEmployee> GetEmployeesAll()
        {
            using(var drs = new DRSContext())
            {
                return (from a in drs.Employees
                        select new Entities.PersonEmployee
                        {
                            CBusinessPartnerId = a.CbusinessPartnerId,
                            CeeUui = a.CeeUuid,
                            EmployeeId = a.Employeeid,
                            TeeUi = a.TeeId,
                            TypeEmployee = a.TypeEmployee
                        }).ToList();
            }
        }

        public List<UserRole> GetUserRoles()
        {
            using(var drs = new DRSContext())
            {
                return (from a in drs.Userroles
                        select new UserRole
                        {
                            UserRoleId = a.Userroleid,
                            UserRoleName = a.Userrolename
                        }).ToList();
            }
        }

        public List<UnitSales> GetUnitSales()
        {
            using (var drs = new DRSContext())
            {
                return (from a in drs.Unitsales
                        select new UnitSales
                        {
                            UnitSalesID = a.Unitsalesid,
                            UnitSalesName = a.Unitsalename
                        }).ToList();
            }
        }

        public List<Entities.Location> GetLocation()
        {
            using (var drs = new DRSContext())
            {
                return (from a in drs.Locations
                        select new Entities.Location
                        {
                            LocationId = a.Locationid,
                            LocationName = a.Locationname,
                            LocationType = a.Locationtype
                        }).ToList();
            }
        }

        public List<ChannelDistribution> GetChannel()
        {
            using (var drs = new DRSContext())
            {
                return (from a in drs.Distributionchannels
                        select new Entities.ChannelDistribution
                        {
                            ChannelDistributionId = a.Distributionchannelid,
                            ChannelDistributionName = a.Distributionchannelname
                        }).ToList();
            }
        }

        public List<DefaultServiceOrder> GetDefaultService()
        {
            using (var drs = new DRSContext())
            {
                return (from a in drs.Defaultserviceorders
                        select new Entities.DefaultServiceOrder
                        {
                            DefaultServiceOrderId = a.Defaultserviceorderid,
                            DefaultServiceOrderName = a.Defaultserviceordername
                        }).ToList();
            }
        }

        public List<TaxResidence> GetTaxResidenceTax()
        {
            using (var drs = new DRSContext())
            {
                return (from a in drs.Taxresidences
                        select new Entities.TaxResidence
                        {
                            TaxResidenceID = a.Taxresidenceid,
                            TaxResidenceName = a.Taxresidencename
                        }).ToList();
            }
        }

        public List<StatusUser> GetStatusUser()
        {
            using (var drs = new DRSContext())
            {
                return (from a in drs.Userstatuses
                        select new Entities.StatusUser
                        {
                            Id = a.Userstatusid,
                            Name = a.Userstatusname
                        }).ToList();
            }
        }

        public List<RAprofile> GetRaProfile()
        {
            using (var drs = new DRSContext())
            {
                return (from a in drs.Raprofiles
                        select new Entities.RAprofile
                        {
                            Defaultserviceorderid = a.Defaultserviceorderid,
                            Distributionchannelid = a.Distributionchannelid,
                            Locationidavailable = a.Locationidavailable,
                            Locationidquality = a.Locationidquality,
                            Locationidrepair = a.Locationidrepair,
                            Locationidresidence = a.Locationidresidence,
                            Personid = a.Personid,
                            Raprofileid = a.Raprofileid,
                            Taxresidenceid = a.Taxresidenceid,
                            Unitsalesid = a.Unitsalesid
                        }).ToList();
            }
        }

        public List<PersonEmployee> GetPersonEmployeesByPersonId(long personId)
        {
            using (var drs = new DRSContext())
            {
                return (from a in drs.Employees
                        join b in drs.Personemployees on a.Employeeid equals b.Employeeid
                        where b.Personid == personId
                        select new Entities.PersonEmployee
                        {
                           CBusinessPartnerId = a.CbusinessPartnerId,
                           CeeUui = a.CeeUuid,
                           EmployeeId = a.Employeeid,
                           TeeUi = a.TeeId,
                           TypeEmployee = a.TypeEmployee
                        }).ToList();
            }
        }

        public void Delete(Entities.Person person)
        {
            using (var drs = new DRSContext())
            {
                var response = drs.Users.Where(a => a.Usersid == person.Users.usersid && a.Userstatusid != 3).FirstOrDefault();

                if(response == null)
                {
                    throw new ApplicationException("* El usuario no se encuentra registrado en el sistema");
                }
                else
                {
                    response.Userstatusid = 3;

                    drs.Update(response);

                    drs.SaveChanges();
                }
            }
        }

        public Response Modify(Entities.Person personM)
        {
            Response response = new Response();

            try
            {
                using (DRSContext dRS = new DRSContext())
                {


                    using (var drsTran = dRS.Database.BeginTransaction())
                    {
                        try
                        {
                            //Creamos la persona en el sistema//
                            Models.Person person = dRS.People.Where(a => a.Personid == personM.Users.personid).FirstOrDefault();

                            person.Personid = personM.Users.personid;
                            person.Personname = personM.Personname;
                            person.Personlastname = personM.Personlastname;
                            person.Personisemployee = true;
                            person.Persongender = "";

                            dRS.Update(person);

                            dRS.SaveChanges();

                            //Eliminamos las relaciones de empleados para volverlas a generar
                            foreach(var item in dRS.Personemployees.Where(a => a.Personid == person.Personid).ToList())
                            {
                                dRS.Remove(item);
                            }

                            dRS.SaveChanges();

                            //Creamos la relación de los empleados con las personas//
                            foreach (var item in personM.Personemployees)
                            {
                                Personemployee personemployee = new Personemployee()
                                {
                                    Personid = person.Personid,
                                    Employeeid = item.EmployeeId
                                };

                                dRS.Add(personemployee);
                            }

                            dRS.SaveChanges();

                            //Creamos el usuario con los accesos al sistema
                            Models.User user = dRS.Users.Where(a => a.Usersid == personM.Users.usersid).FirstOrDefault();

                            user.Userstatusid = personM.Users.statusId;
                            user.Usersemail = personM.Account.Email;
                            user.Usersphonenumber = personM.Account.PhoneNumber;
                            user.Userrole = personM.UserRoleId;

                            dRS.Update(user);
                            dRS.SaveChanges();

                            Raprofile raprofile = dRS.Raprofiles.Where(a => a.Personid == personM.Users.personid).FirstOrDefault();

                            if(raprofile == null)
                            {
                                //Se crean los datos de perfil del RA
                                long idRAProfile = 1;

                                if (dRS.Raprofiles.Count() > 0)
                                {
                                    idRAProfile = dRS.Raprofiles.Max(A => A.Raprofileid) + 1;
                                }

                                raprofile.Defaultserviceorderid = personM.Raprofile.Defaultserviceorderid;
                                raprofile.Distributionchannelid = personM.Raprofile.Distributionchannelid;
                                raprofile.Locationidavailable = personM.Raprofile.Locationidavailable;
                                raprofile.Locationidquality = personM.Raprofile.Locationidquality;
                                raprofile.Locationidrepair = personM.Raprofile.Locationidrepair;
                                raprofile.Locationidresidence = personM.Raprofile.Locationidresidence;
                                raprofile.Personid = personM.Users.personid;
                                raprofile.Taxresidenceid = personM.Raprofile.Taxresidenceid;
                                raprofile.Unitsalesid = personM.Raprofile.Unitsalesid;
                                raprofile.Raprofileid = idRAProfile;

                                dRS.Add(raprofile);
                            }
                            else
                            {
                                raprofile.Defaultserviceorderid = personM.Raprofile.Defaultserviceorderid;
                                raprofile.Distributionchannelid = personM.Raprofile.Distributionchannelid;
                                raprofile.Locationidavailable = personM.Raprofile.Locationidavailable;
                                raprofile.Locationidquality = personM.Raprofile.Locationidquality;
                                raprofile.Locationidrepair = personM.Raprofile.Locationidrepair;
                                raprofile.Locationidresidence = personM.Raprofile.Locationidresidence;
                                raprofile.Personid = personM.Users.personid;
                                raprofile.Taxresidenceid = personM.Raprofile.Taxresidenceid;
                                raprofile.Unitsalesid = personM.Raprofile.Unitsalesid;

                                dRS.Update(raprofile);
                            }
                            
                            dRS.SaveChanges();

                            drsTran.Commit();
                        }
                        catch (Exception ex)
                        {
                            drsTran.Rollback();

                            return new Response()
                            {
                                Codigo = 0,
                                Mensaje = ex.Message
                            };
                        }

                    }

                    response.Codigo = 1;
                    response.Mensaje = "OK";
                }
            }

            catch (Exception ex)
            {
                response.Codigo = 0;
                response.Mensaje = ex.Message;
            }

            return response;


        }
    }
}
