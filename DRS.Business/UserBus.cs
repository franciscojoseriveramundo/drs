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
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DRS.Business
{
    public class UserBus
    {
        private readonly IUserDA iUserDA;
        private readonly ILogDA iLogDA;
        private readonly IEmailDA iemailDA;
        private readonly IPermissionDA ipermissionDA;

        private IConfiguration iConfiguration;
        public UserBus(IUserDA iUserDA, ILogDA iLogDA, IEmailDA iemailDA, IPermissionDA ipermissionDA)
        {
            this.iUserDA = iUserDA;
            this.iLogDA = iLogDA;
            this.iemailDA = iemailDA;
            this.ipermissionDA = ipermissionDA;
            this.iConfiguration = ConectionDB.Configuration;
        }

        public Users GetUserForLogin(Login login)
        {
            if (string.IsNullOrEmpty(login.Username) && string.IsNullOrEmpty(login.Password))
            {
                throw new ApplicationException("Complete sus credenciales de acceso.");
            }

            if (string.IsNullOrEmpty(login.Username) && !string.IsNullOrEmpty(login.Password))
            {
                throw new ApplicationException("Introduzca su correo electrónico.");
            }

            if (!string.IsNullOrEmpty(login.Username) && string.IsNullOrEmpty(login.Password))
            {
                throw new ApplicationException("Introduzca su contraseña de acceso.");
            }

            login.Password = Encriptar(login.Password);

            var getUser = iUserDA.GetUserForLogin(login);

            if (getUser == null)
            {
                throw new ApplicationException("Sus credenciales de acceso son incorrectas.");
            }

            if(getUser.statusId == 3)
            {
                throw new ApplicationException("Sus credenciales de acceso son incorrectas.");
            }

            if(getUser.statusId == 2)
            {
                throw new ApplicationException("Su cuenta de acceso ha sido bloqueada. Consulte con su administrador para que desbloqueen la misma.");
            }

            getUser.clients = GetClients(getUser.personid);

            return getUser;
        }

        public SelectList GetStatusUser()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el estatus del usuario.", Value = "0", Selected = true });

            foreach (var item in iUserDA.GetStatusUser().Where(a => a.Id != 3).ToList())
            {
                listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.Name,
                            Value = item.Id.ToString(),
                            Selected = false
                        });
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }

        public SelectList GetTaxResidenceList()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione la residencia fiscal.", Value = "0", Selected = true });

            foreach (var item in iUserDA.GetTaxResidenceTax())
            {
                listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.TaxResidenceName,
                            Value = item.TaxResidenceID.ToString(),
                            Selected = false
                        });
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }

        public SelectList GetLocationList(List<Location> lst)
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione la ubicación.", Value = "0", Selected = true });

            foreach (var item in lst)
            {
                listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.LocationName,
                            Value = item.LocationId.ToString(),
                            Selected = false
                        });
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }

        public SelectList GetChannelList()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el canal de distribución.", Value = "0", Selected = true });

            foreach (var item in iUserDA.GetChannel())
            {
                listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.ChannelDistributionName,
                            Value = item.ChannelDistributionId.ToString(),
                            Selected = false
                        });
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }

        public SelectList GetDefaultServiceList()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el servicio por defecto.", Value = "0", Selected = true });

            foreach (var item in iUserDA.GetDefaultService())
            {
                listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.DefaultServiceOrderName,
                            Value = item.DefaultServiceOrderId.ToString(),
                            Selected = false
                        });
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }

        public List<Location> GetLocation()
        {
            return iUserDA.GetLocation();
        }

        public SelectList GetEmployeesList()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            //listItem.Add(new SelectListItem() { Text = "Seleccione el empleado responsable.", Value = "0", Selected = true });

            foreach (var item in iUserDA.GetEmployeesAll())
            {
                listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.CeeUui + " - " + item.TeeUi,
                            Value = item.EmployeeId.ToString(),
                            Selected = false
                        });
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }

        public Person GetUser(long userId)
        {
            Person person = new Person()
            {
                Account = new Account(),
                Personemployees = new List<PersonEmployee>(),
                Users = new Users(),
                Raprofile = new RAprofile(),
                EmployeeId = new MultiSelectDropDownEmployee()
            };

            //Obtenemos la informacion necesaria//
            var personInformation = GetUsers().Where(a => a.usersid == userId).FirstOrDefault();

            if(personInformation != null)
            {
                person.Account.Email = personInformation.usersemail;
                person.Account.PhoneNumber = personInformation.usersphone;
                person.PersonId = personInformation.personid;
                person.Personlastname = personInformation.personlastname;
                person.Personname = personInformation.personname;
                person.Users = personInformation;
                person.UserRoleId = personInformation.roleId;

                //Obtenemos la informacion del RA PROFILE.
                var raprofile = iUserDA.GetRaProfile().Where(a => a.Personid == person.PersonId).FirstOrDefault();

                if(raprofile != null)
                {
                    person.Raprofile = raprofile;
                }

                var personEmp = iUserDA.GetPersonEmployeesByPersonId(person.PersonId);

                if(personEmp.Count > 0)
                {
                    person.Personemployees = personEmp;

                    foreach(var item in personEmp)
                    {
                        person.EmployeeId.SelectedMultiCompanyId = new List<long>();
                        person.EmployeeId.SelectedMultiCompanyId.Add(item.EmployeeId);
                    }
                }

            }

            return person;
        }

        public SelectList GetUserRolesSelectList()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione el tipo de usuario.", Value = "0", Selected = true });

            foreach (var item in GetUserRoles())
            {
                listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.UserRoleName,
                            Value = item.UserRoleId.ToString(),
                            Selected = false
                        });
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }

        public Response Modify(Person person)
        {
            //Validamos el tipo de acceso que tendra el usuario//
            var actionsByRoleId = ipermissionDA.GetActionsForRoleUser(person.UserRoleId).Where(a => a.Actionsid == 2).ToList();

            if (actionsByRoleId.Count > 0)
            {
                person.Personemployees = new List<PersonEmployee>();

                person.Personemployees = iUserDA.GetEmployeesAll();
            }
            else
            {
                var employeeAll = iUserDA.GetEmployeesAll();

                if (person.EmployeeId != null)
                {
                    foreach (var item in person.EmployeeId.SelectedMultiCompanyId)
                    {
                        person.Personemployees.Add(employeeAll.Where(a => a.EmployeeId == item).FirstOrDefault());
                    }
                }

            }

            //Validar la existencia de que el correo electrónico ya exista en el sistema

            var userGet = iUserDA.GetUsersAll().Where(a => a.usersid == person.Users.usersid && (a.statusId == 1 || a.statusId == 2)).ToList();

            if (person.Account.Email != userGet.FirstOrDefault().usersemail)
            {
                if (iUserDA.GetUsersAll().Where(a => a.usersemail == person.Account.Email && (a.statusId == 1 || a.statusId == 2)).Count() > 0)
                {
                    throw new Exception("* El correo electrónico ya se encuentra asociado a una cuenta existente.");
                }
            }

            //Validaciones de la creación de una nueva cuenta de usuario//
            ValidatePerson(person);

            var response = iUserDA.Modify(person);

            //Se realiza el envio del correo electronico//
            if (response.Codigo == 0)
            {
                throw new ApplicationException("La nueva cuenta de acceso no pudo ser creada, intente de nuevo.");
            }

            
            response.Mensaje = "El usuario ha sido modificado de manera exitosa.";

            return response;
        }

        public List<UserRole> GetUserRoles()
        {
            return iUserDA.GetUserRoles();
        }

        public SelectList GetUnitSalesList()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem() { Text = "Seleccione la unidad de ventas.", Value = "0", Selected = true });

            foreach (var item in iUserDA.GetUnitSales())
            {
                listItem.Add(
                        new SelectListItem()
                        {
                            Text = item.UnitSalesName,
                            Value = item.UnitSalesID.ToString(),
                            Selected = false
                        });
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(listItem, "Value", "Text", null);
        }

        public List<UsersViewModel> GetUsersForIndex()
        {
            return iUserDA.GetUsersForIndex();
        }

        public ClientAddress GetAddressClientById(long idCliente)
        {
            return iUserDA.GetClientAddressesAll().Where(a => a.Clientid == idCliente.ToString()).FirstOrDefault();
        }

        public Response Delete(Person person)
        {
            Response response = new Response();

            try
            {
                iUserDA.Delete(person);

                response.Codigo = 1;
                response.Mensaje = "OK";
            }
            catch(Exception ex)
            {
                response.Codigo = 0;
                response.Mensaje = ex.Message;
            }

            return response;
        }

        public List<Clientes> GetClients(Int64 personId)
        {
            return iUserDA.GetClients(personId).OrderBy(a => a.NombreCliente).ToList();
        }

        public List<Clientes> GetClientsAll()
        {
            return iUserDA.GetClientsAll().OrderBy(a => a.NombreCliente).ToList();
        }

        public List<Clientes> GetClientsByPersonId(Int64 personId)
        {
            return iUserDA.GetClientsByPersonId(personId).OrderBy(a => a.NombreCliente).ToList();
        }

        public Users GetUserForLoginExternal(string email)
        {
            var getUser = iUserDA.GetUserForLoginExternal(email);

            if (getUser == null)
            {
                throw new ApplicationException("El usuario ingresado no tiene permisos de acceso a la aplicación.");
            }

            if (getUser.statusId == 3)
            {
                throw new ApplicationException("El usuario ingresado no tiene permisos de acceso a la aplicación.");
            }

            if (getUser.statusId == 2)
            {
                throw new ApplicationException("Su cuenta de acceso ha sido bloqueada. Consulte con su administrador para que desbloqueen la misma.");
            }

            getUser.clients = GetClients(getUser.personid);

            return getUser;
        }

        string key = "mikey";

        public string Encriptar(string texto)
        {
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 =
            new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            //Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

            tdes.Clear();

            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }

        public string Desencriptar(string textoEncriptado)
        {
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes
            byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

            //se llama a las clases que tienen los algoritmos
            //de encriptación se le aplica hashing
            //algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform =
            tdes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

            tdes.Clear();
            //se regresa en forma de cadena
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        //Metodos de SAP ByDesign

        /*Obtiene el personal registrado*/
        public Personal GetPersonalBySap()
        {
            Personal personal = new Personal();

            Entities.Rest.Request request = new Entities.Rest.Request()
            {
                Authentication = new Entities.Login()
                {
                    Username = iConfiguration["AuthenticationSAPByDesign:Username"],
                    Password = iConfiguration["AuthenticationSAPByDesign:Password"]
                },
                Parameters = null,
                Resource = iConfiguration["WebServicesSAP:WsService-Employee"],
                Url = iConfiguration["WebServicesSAP:WsService-Employee"],
                Body = null

            };

            var result = Client.GET(request);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                personal = JsonConvert.DeserializeObject<Entities.Rest.Personal>(result.Content);
            }
            else
            {
                throw new ApplicationException(result.StatusDescription);
            }

            return personal;
        }

        /*Obtiene el cliente a través del numero de cliente*/
        public Clients GetClientsForPersonalBySap(string ClientCode = "")
        {
            Clients clientes = new Clients();

            Entities.Rest.Request request = new Entities.Rest.Request()
            {
                Authentication = new Entities.Login()
                {
                    Username = iConfiguration["AuthenticationSAPByDesign:Username"],
                    Password = iConfiguration["AuthenticationSAPByDesign:Password"]
                },
                Parameters = null,
                Resource = iConfiguration["WebServicesSAP:WsService-Clients"].Replace("Parameter1", ClientCode),
                Url = iConfiguration["WebServicesSAP:WsService-Clients"].Replace("Parameter1", ClientCode),
                Body = null

            };

            var result = Client.GET(request);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                clientes = JsonConvert.DeserializeObject<Entities.Rest.Clients>(result.Content);
            }
            else
            {
                throw new ApplicationException(result.StatusDescription);
            }

            return clientes;
        }

        public int CreateProduct(string ProductId, string ProductName, string typeTechnology = "")
        {
            int resultado = 0;

            //Validación de la existencia del producto en el sistema.
            var numeroProductos = iUserDA.ProductExists(ProductId, ProductName, typeTechnology);

            if (numeroProductos > 0)
            {
                resultado = 1; //Indica que ya existe el registro y en caso de que no se actualice, mando la señal que sea 1.

                //Validaremos si el registro necesita actualizarse.
                var producto = iUserDA.GetProduct(ProductId, typeTechnology);

                if (producto.ProductName != ProductName && producto.ProductTechnology != typeTechnology)
                {
                    if (!iUserDA.UpdateProduct(producto.ProductoId, ProductName, producto.ProductCode, typeTechnology))
                    {
                        resultado = 4; //Fallo la actualización
                    }
                    else
                    {
                        resultado = 5; //Actualización exitosa
                    }
                }

            }
            else
            {
                var createProducto = iUserDA.CreateProducto(ProductId, ProductName, typeTechnology);

                if (createProducto == false)
                {
                    resultado = 2; //Indica que fallo la creación del empleado
                }
                else
                {
                    resultado = 3; //El empleado esta creado de manera satisfactoria.
                }
            }

            return resultado;

            ////Validamos la relación entre el cliente y el empleado
            //var client = iUserDA.GetCliente(CROOT_UUID, TEE_ID); //1) volvemos a obtener la relacion entre cliente y empleado.
            //var product = iUserDA.GetProduct(ProductId).ProductoId;

            //if (iUserDA.ExistsRelClientProduct(client.IdCliente, (Int32)product, serial,imsi, number, sim) == 0)
            //{
            //    if (iUserDA.CreateRelClientProduct(client.IdCliente, (Int32)product, serial, imsi, number, sim) == false)
            //    {
            //        return 4; //Si hay un error en la inserción entre la relacion de cliente y empleado
            //    }

            //    resultado = 1;
            //}
            //else
            //{
            //    if(iUserDA.UpdateRelClientProduct(client.IdCliente, (Int32)product, serial, imsi, number, sim) == false)
            //    {
            //        return 6; //Si hay un error en la inserción entre la relacion de cliente y empleado
            //    }

            //    resultado = 0;
            //}
        }

        public Response GetUserForEmail(Recovery recovery)
        {
            //Validamos la existencia del usuario//
            var respuesta = iUserDA.GetUsersAll().Where(a => a.usersemail == recovery.Email && (a.statusId == 1 || a.statusId == 2)).FirstOrDefault();

            if(respuesta != null)
            {
                Guid obj = Guid.NewGuid();
                string keyReco = obj.ToString();

                RecoveryRequest recoveryRequest = new RecoveryRequest()
                {
                    Created = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    Id = 0,
                    Key = keyReco,
                    Status = 1,
                    UserId = respuesta.usersid
                };

                //Se genera la solicitud de cambio de contraseña//
                Response response = iUserDA.createRecoveryAccess(recoveryRequest);

                if(response.Codigo == 1)
                {
                    //Se creará el envio de correo eelctrónico
                    //Variable que guarda el asunto//
                    StringBuilder subject = new StringBuilder();

                    EmailLayout email = new EmailLayout();
                    List<EmailReceipts> lstreceipts = new List<EmailReceipts>();
                    string userEmail = recovery.Email;
                    files file = new files();


                    //Obtenemos el asunto del correo electrónico
                    subject.Append("Recuperación de contraseña de acceso");

                    email = iemailDA.GetEmailById(19);

                    //Generamos el body del correo electrónico
                    email.EmailBody = email.EmailBody.Replace("{@NombreDelUsuario}", String.Concat(respuesta.personname)).
                        Replace("{{@UrlSitio}}", iConfiguration["Resources:UrlSite"] + "Home/AccountRecovery?key=" + keyReco).
                        Replace("{{@UrlImageTeamvox}}", iConfiguration["Resources:UrlSite"] + iConfiguration["Resources:UrlLogoTeamVox"]).
                        Replace("{{@Anio}}", DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now).Year.ToString());

                    //Creamos el registro del Log del correo electrónico
                    EmailLog emailLog = iemailDA.CreateEmailLog(new EmailLog()
                    {
                        EmailCreateDate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                        EmailHtml = email.EmailBody,
                        EmailLayoutId = 19,
                        EmailSendDate = null,
                        EmailStatus = 1,
                        EmailSubject = subject.ToString(),
                        UserId = Convert.ToInt64(respuesta.usersid)
                    });

                    //Damos de alta los destinatarios//
                    // 1. Principal
                    bool emailDestinatary = iemailDA.CreateDestinatary(new EmailReceipts()
                    {
                        EmailAddress = recovery.Email
                    }, 1, emailLog.EmailLogId);

                    List<System.Net.Mail.Attachment> lstFilesEmail = new List<System.Net.Mail.Attachment>();

                    if (file != null)
                    {
                        if (file.fileContent != null)
                        {
                            System.Net.Mail.Attachment fileAtt = new System.Net.Mail.Attachment(new MemoryStream(file.fileContent), file.nameFile + file.extensionFile);


                            lstFilesEmail.Add(fileAtt);
                        }
                    }

                    //Enviamos el Email
                    var responseEmail = new EmailBus(iemailDA).SendEmail(
                        email,
                        lstreceipts,
                        subject.ToString(),
                        recovery.Email, lstFilesEmail); //Teniendo los reportes se adjuntaran

                    //Si no se envia se deja el estatus a cuando se creo.
                    if (responseEmail.Codigo == 0)
                    {
                        throw new ApplicationException(responseEmail.Mensaje);
                    }

                    emailLog.EmailSendDate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now);
                    emailLog.EmailStatus = 2;
                    emailLog = iemailDA.UpdateStatusEmailLog(emailLog);
                }

                

            }

            return new Response()
            {
                Codigo = 0,
                Mensaje = "OK"
            };
        }

        public Response ChangePassword(AccountRecovery account)
        {
            if(string.IsNullOrEmpty(account.Password) || string.IsNullOrEmpty(account.ConfirmPassword))
            {
                throw new ApplicationException("* Complete los campos de contraseña.");
            }

            if(account.Password != account.ConfirmPassword)
            {
                throw new ApplicationException("* Las contraseñas no coínciden.");
            }

            ValidatePassword(account.Password);

            //Copiamos objeto nuevo//
            AccountRecovery accountRecoverycopy = account;

            string contrasenaNormal = account.Password;

            //Se va a encriptar
            accountRecoverycopy.Password = Encriptar(accountRecoverycopy.Password);
            accountRecoverycopy.ConfirmPassword = Encriptar(accountRecoverycopy.ConfirmPassword);

            //Validar que la contraseña no haya sido utilizada
            bool isUsedPassword = iUserDA.passwordIsUsed(accountRecoverycopy.Password, accountRecoverycopy.UserId);

            if (isUsedPassword)
            {
                account.Password = contrasenaNormal;
                account.ConfirmPassword = contrasenaNormal;

                throw new ApplicationException("* La contraseña introducida ya ha sido utilizada. Intente con otra.");
            }

            //Se va a actaulizar la contraseña de acceso//
            Response response = iUserDA.ChangePassword(accountRecoverycopy);

            //Mensaje de felicitacion
            if(response.Codigo == 1)
            {
                response.Mensaje = "* La contraseña ha sido modificada de manera exitosa.";
            }
            else
            {
                response.Mensaje = "* Ocurrió un error al realizar el cambio de contraseña. Intente nuevamente.";
            }

            account.Password = contrasenaNormal;
            account.ConfirmPassword = contrasenaNormal;

            return response;
        }

        public void ValidatePassword(string password)
        {
            int minLenght = 8, numUpper = 1, numNumber = 1;

            var upper = new System.Text.RegularExpressions.Regex("[A-Z]");
            var number = new System.Text.RegularExpressions.Regex("[0-9]");

            if (password.Length < minLenght)
            {
                throw new ApplicationException("* La contraseña debe de tener un mínimo de 8 carácteres.");
            }

            if (upper.Matches(password).Count < numUpper)
            {
                throw new ApplicationException("* La contraseña debe de tener al menos una letra mayúscula.");
            }

            if (number.Matches(password).Count < numNumber)
            {
                throw new ApplicationException("* La contraseña debe de tener al menos un número.");
            }
        }

        public List<Users> GetUsers()
        {
            return iUserDA.GetUsersAll();
        }

        public RecoveryRequest getRecoveryAccess(string key)
        {
            return iUserDA.getRecoveryAccess(key); 
        }

        public Response ComprobateVigency(string key)
        {
            Response response = new Response();

            RecoveryRequest recovery = iUserDA.getRecoveryAccess(key);

            //Restamos las fechas//
            var resta = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now) - recovery.Created;

            response.Codigo = 1;
            response.Mensaje = "OK";

            if(recovery.Status != 1 && resta.TotalMinutes <= 15)
            {
                response.Codigo = 0;
                response.Mensaje = "* El enlace de recuperación de cuenta ya fue utilizado.";
            }

            if (resta.TotalMinutes > 15)
            {
                response.Codigo = 0;
                response.Mensaje = "* El enlace ya no se encuentra vigente.";
            }

            return response;
        }

        public int CreateAccessory(string cMATR_UUID, string cSALESOMATIONC0CA0DF3B7723DDD, string cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377, string cs1ANsB5B1E594DE71377, string tMATR_UUID)
        {
            int resultado = 0;

            //Validación de la existencia del producto en el sistema.
            var accesories = iUserDA.AccessoryExists(cMATR_UUID, cSALESOMATIONC0CA0DF3B7723DDD);

            if (accesories != null)
            {
                resultado = 1; //Indica que ya existe el registro y en caso de que no se actualice, mando la señal que sea 1.

                if (accesories.CSTATUSMATION08F89FA6D3D769C1 != cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377 || accesories.CSTATUSMATION08F89FA6D3D769C1 != cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377 || accesories.Cs1ANsB5B1E594DE71377 != cs1ANsB5B1E594DE71377 || accesories.TMATR_UUID != tMATR_UUID)
                {
                    if (iUserDA.UpdateAccessory(accesories.CMATR_UUID, accesories.CSALESOMATIONC0CA0DF3B7723DDD, cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377, cs1ANsB5B1E594DE71377, tMATR_UUID))
                    {
                        return 5; //Se actualizó correctamente
                    }
                    else
                    {
                        return 4; //Fallo la actualización
                    }
                }

            }
            else
            {
                var createAccessory = iUserDA.CreateAccessory(cMATR_UUID, cSALESOMATIONC0CA0DF3B7723DDD, cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377, cs1ANsB5B1E594DE71377, tMATR_UUID);

                if (createAccessory == false)
                {
                    return 2; //Fallo la creación del empleado
                }
                else
                {
                    resultado = 3; //El acceso se creo correctamente.
                }
            }

            return resultado;
        }

        public int CreateEmployee(string CBUSINESS_PARTNER_ID, string CEE_UUID, string TEE_ID, string Ts1ANs2AFBCFE0264F178)
        {
            int resultado = 0;

            //Validar que el empleado no exista en el sistema.
            var numeroEmpleados = iUserDA.EmployeeExist(CBUSINESS_PARTNER_ID, CEE_UUID, TEE_ID);

            if (numeroEmpleados > 0)
            {
                resultado = 1; //Indica que ya existe el registro y en caso de que no se actualice, mando la señal que sea 1.

                //Validaremos si el registro necesita actualizarse.
                var employee = iUserDA.GetEmployee(CEE_UUID);

                if (employee.TeeID != TEE_ID)
                {
                    if (iUserDA.UpdateEmployee(CBUSINESS_PARTNER_ID, CEE_UUID, TEE_ID, Ts1ANs2AFBCFE0264F178))
                    {
                        resultado = 5;
                    }
                    else
                    {
                        resultado = 4;
                    }
                }
            }
            else
            {
                var createEmployee = iUserDA.CreateEmployee(CBUSINESS_PARTNER_ID, CEE_UUID, TEE_ID, Ts1ANs2AFBCFE0264F178);

                if (createEmployee == false)
                {
                    resultado = 2; //Indica que fallo la creación del empleado
                }
                else
                {
                    return 3; //El empleado esta creado de manera satisfactoria.
                }

            }

            return resultado;
        }

        public int CreateClient(string CRSP_EMPL_UUID, string CROOT_UUID, string TEE_ID, int CLIFE_CYCLE_STATUS)
        {
            int resultado = 0;

            var employee = iUserDA.GetEmployee(CRSP_EMPL_UUID);

            if (employee == null)
            {
                resultado = 4; //No se registra el producto si no existe un empleado
            }
            else
            {
                //Existe el cliente en el sistema
                var client = iUserDA.GetCliente(CROOT_UUID, TEE_ID);

                if (client == null)
                {
                    //Insertamos el cliente en la base de datos.
                    var response = iUserDA.CreateCliente(CROOT_UUID, TEE_ID, CLIFE_CYCLE_STATUS);

                    if (response == false)
                    {
                        resultado = 3; //Si es que falla la creación del cliente
                    }
                    else
                    {
                        iLogDA.CreateTransaction(new LogTransaccion()
                        {
                            Descripcion = "El cliente " + CROOT_UUID + " - " + TEE_ID + " se creó correctamente.",
                            FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                            TransaccionId = 0,
                            UsuarioId = 0,
                            TipoOperacion = 5
                        });
                    }
                }
                else
                {
                    if (client.NombreCliente != TEE_ID)
                    {
                        if (!iUserDA.UpdateClient(client.IdCliente, TEE_ID))
                        {
                            resultado = 5;
                        }
                        else
                        {
                            iLogDA.CreateTransaction(new LogTransaccion()
                            {
                                Descripcion = "El cliente " + CROOT_UUID + " - " + TEE_ID + " se actualizó correctamente.",
                                FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                                TransaccionId = 0,
                                UsuarioId = 0,
                                TipoOperacion = 5
                            });
                        }
                    }

                    if (client.ClienteEstatus != CLIFE_CYCLE_STATUS.ToString())
                    {
                        if (!iUserDA.UpdateClientStatus(client.IdCliente, CLIFE_CYCLE_STATUS))
                        {
                            resultado = 5;
                        }
                        else
                        {
                            iLogDA.CreateTransaction(new LogTransaccion()
                            {
                                Descripcion = "El cliente " + CROOT_UUID + " - " + TEE_ID + " se actualizó correctamente.",
                                FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                                TransaccionId = 0,
                                UsuarioId = 0,
                                TipoOperacion = 5
                            });
                        }
                    }
                }

                //Validamos la relación entre el cliente y el empleado
                client = iUserDA.GetCliente(CROOT_UUID, TEE_ID); //1) volvemos a obtener la relacion entre cliente y empleado.

                if (iUserDA.ExistsRelClientEmployee(client.IdCliente, employee.EmployeeId) == 0)
                {
                    if (iUserDA.CreateRelClientEmployee(client.IdCliente, employee.EmployeeId) == false)
                    {
                        resultado = 2; //Si hay un error en la inserción entre la relacion de cliente y empleado
                    }
                    else
                    {
                        iLogDA.CreateTransaction(new LogTransaccion()
                        {
                            Descripcion = "El cliente " + CROOT_UUID + " - " + TEE_ID + " se relaciono correctamente con el empleado " + employee.TeeID + ".",
                            FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                            TransaccionId = 0,
                            UsuarioId = 0,
                            TipoOperacion = 5
                        });
                    }

                    return 1;
                }

            }

            return resultado;
        }

        public bool ClientAddressABC(ClientsAddress clientesA, Clients clientes)
        {
            bool resultado = false;

            try
            {
                //Obtenemos los clientes y direcciones dados de alta en la plataforma//
                var clientesE = iUserDA.GetClientsAll();
                var clientsA = iUserDA.GetClientAddressesAll();

                List<Entities.ClientAddress> newclientsA = new List<Entities.ClientAddress>();
                List<Entities.ClientAddress> modifyclientsA = new List<Entities.ClientAddress>();

                //Hacemos el join
                var joinCA = (from a in clientesE
                              
                              select new ClientUnionAddress
                              {
                                  ClientCode = a.ClaveCliente,
                                  ClientId = a.IdCliente
                              }).ToList();

                foreach (var cliente in clientesA.d.results)
                {
                    if(joinCA.Where(a => a.ClientCode == cliente.CBP_UUID).Count() > 0)
                    {
                        if (clientsA.Where(a => a.Clientid == (clientesE.Where(a => a.ClaveCliente == cliente.CBP_UUID).FirstOrDefault().IdCliente).ToString()).Count() == 0)
                        {
                            newclientsA.Add(new ClientAddress
                            {
                                Clientaddresscityname = cliente.CCITY_NAME,
                                Clientaddresscountry = cliente.TTAX_COUNTRY,
                                Clientaddressdistrictname = cliente.CDISTRICT_NAME,
                                Clientaddressemail = cliente.CEMAIL_URI,
                                Clientaddressid = 0,
                                Clientid = (clientesE.Where(a => a.ClaveCliente == cliente.CBP_UUID).FirstOrDefault().IdCliente).ToString(),
                                Clientaddressnumber = cliente.CHOUSE_ID,
                                Clientaddressphonenumber = cliente.CPHONE_NR,
                                Clientaddressregioncode = cliente.TREGION_CODE,
                                Clientaddressstreetname = cliente.CSTREET_NAME,
                                Clientaddressappoderatename = cliente.FALTACAMPO,
                                Clientaddresspostalcode = cliente.CSTREET_POSTAL
                            });
                        }
                        else
                        {
                            modifyclientsA.Add(new ClientAddress
                            {
                                Clientaddresscityname = cliente.CCITY_NAME,
                                Clientaddresscountry = cliente.TTAX_COUNTRY,
                                Clientaddressdistrictname = cliente.CDISTRICT_NAME,
                                Clientaddressemail = cliente.CEMAIL_URI,
                                Clientaddressid = 0,
                                Clientid = (clientesE.Where(a => a.ClaveCliente == cliente.CBP_UUID).FirstOrDefault().IdCliente).ToString(),
                                Clientaddressnumber = cliente.CHOUSE_ID,
                                Clientaddressphonenumber = cliente.CPHONE_NR,
                                Clientaddressregioncode = cliente.TREGION_CODE,
                                Clientaddressstreetname = cliente.CSTREET_NAME,
                                Clientaddressappoderatename = cliente.FALTACAMPO,
                                Clientaddresspostalcode = cliente.CSTREET_POSTAL
                            });
                        }
                    }
                    
                }

                //Agrega los nuevos registros
                iUserDA.AddAddress(newclientsA);
                //Modifica los nuevos registros
                iUserDA.ModifyAddress(modifyclientsA);

                resultado = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Direcciones, error: " + ex.Message);
                resultado =false;
            }

            return resultado;
        }

        public List<ProductsClients> GetProductsRegisteredForClientCode(string cROOT_UUID)
        {
            return iUserDA.GetProductsRegisteredForClientCode(cROOT_UUID);
        }

        public Productos GetProduct(string productCode, string productTechlogy)
        {
            Productos prd = iUserDA.GetProduct(productCode, productTechlogy);
            return prd;
        }

        public Clientes GetCliente(string cROOT_UUID, string tEE_ID = "")
        {
            return iUserDA.GetCliente(cROOT_UUID, tEE_ID);
        }

        public int CreateProducts(List<ProductsClients> newProductsForClient)
        {
            int resultado = 0;

            try
            {
                resultado = iUserDA.CreateProducts(newProductsForClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("El producto " + newProductsForClient.FirstOrDefault().ClientId + " no fueron creados. Error" + ex.Message);
                resultado = 0;
            }

            return resultado;
        }

        public int UpdateProducts(List<ProductsClients> lstFinal, List<ProductsClients> lstActualData)
        {
            int resultado = 0;

            List<ProductsClients> lstUpdtFinal = new List<ProductsClients>();

            foreach (var item in lstActualData)
            {
                var lstUpdating = lstFinal.Where(a => a.Serie == item.Serie).FirstOrDefault();
                var lstActualDataFirst = lstActualData.Where(a => a.Serie == item.Serie).FirstOrDefault();

                if (lstUpdating != null && lstActualDataFirst != null)
                {
                    if (
                        lstUpdating.ClientId != lstActualDataFirst.ClientId ||
                        lstUpdating.Imsi != lstActualDataFirst.Imsi ||
                        lstUpdating.Number != lstActualDataFirst.Number ||
                        lstUpdating.ProductCode != lstActualDataFirst.ProductCode ||
                        lstUpdating.ProductId != lstActualDataFirst.ProductId ||
                        lstUpdating.ProductName != lstActualDataFirst.ProductName ||
                        lstUpdating.Serie != lstActualDataFirst.Serie ||
                        lstUpdating.Sim != lstActualDataFirst.Sim ||
                        lstUpdating.PlanDataId != lstActualDataFirst.PlanDataId ||
                        lstUpdating.PlanDataName != lstActualDataFirst.PlanDataName ||
                        lstUpdating.CarrierId != lstActualDataFirst.CarrierId ||
                        lstUpdating.CarrierName != lstActualDataFirst.CarrierName ||
                        lstUpdating.ConsoleActive != lstActualDataFirst.ConsoleActive
                      )
                    {
                        lstUpdtFinal.Add(lstUpdating);
                    }

                }
            }

            if (lstUpdtFinal.Count() > 0)
            {
                if (iUserDA.UpdateRelClientProduct(lstUpdtFinal))
                {
                    Console.WriteLine("\t   Los productos del cliente " + lstUpdtFinal.FirstOrDefault().ClientId + " SE ACTUALIZARON correctamente.");

                    //iLogDA.CreateTransaction(new LogTransaccion()
                    //{
                    //    Descripcion = "Los productos del cliente " + lstUpdtFinal.FirstOrDefault().ClientId + " SE ACTUALIZARON correctamente.",
                    //    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    //    TransaccionId = 0,
                    //    UsuarioId = 0,
                    //    TipoOperacion = 6
                    //});

                    resultado = 1;
                }
                else
                {
                    Console.WriteLine("\t   Los productos del cliente " + lstUpdtFinal.FirstOrDefault().ClientId + " NO SE ACTUALIZARON correctamente.");

                    //iLogDA.CreateIncident(new LogIncidencia()
                    //{
                    //    Descripcion = "Los productos del cliente " + lstUpdtFinal.FirstOrDefault().ClientId + " NO SE ACTUALIZARON correctamente.",
                    //    FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                    //    IncidenciaId = 0,
                    //    UsuarioId = 0
                    //});

                    resultado = 0;
                }

            }
            else
            {
                resultado = 2;
            }

            return resultado;
        }

        public int DeleteProducts(List<ProductsClients> lstFinalDelete, List<ProductsClients> productsForClients)
        {
            int resultado = 0;

            List<ProductsClients> lstDelFinal = new List<ProductsClients>();

            foreach (var item in productsForClients)
            {
                var lstDelete = lstFinalDelete.Where(a => a.Serie == item.Serie).FirstOrDefault();
                var lstActualDataFirst = productsForClients.Where(a => a.Serie == item.Serie).FirstOrDefault();

                if (lstDelete != null && lstActualDataFirst != null)
                {
                    lstDelFinal.Add(lstDelete);
                }
            }

            if (lstDelFinal.Count() > 0)
            {
                if (iUserDA.DeleteRelClientProduct(lstDelFinal))
                {
                    Console.WriteLine("\t   Los productos del cliente " + lstDelFinal.FirstOrDefault().ClientId + " SE ACTUALIZARON correctamente.");

                    iLogDA.CreateTransaction(new LogTransaccion()
                    {
                        Descripcion = "Los productos del cliente " + lstDelFinal.FirstOrDefault().ClientId + " SE ELIMINARON correctamente.",
                        FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                        TransaccionId = 0,
                        UsuarioId = 0,
                        TipoOperacion = 6
                    });

                    resultado = 1;
                }
                else
                {
                    Console.WriteLine("\t   Los productos del cliente " + lstDelFinal.FirstOrDefault().ClientId + " NO SE ELIMINARON correctamente.");

                    iLogDA.CreateIncident(new LogIncidencia()
                    {
                        Descripcion = "Los productos del cliente " + lstDelFinal.FirstOrDefault().ClientId + " NO SE ELIMINARON correctamente.",
                        FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                        IncidenciaId = 0,
                        UsuarioId = 0
                    });

                    resultado = 0;
                }

            }
            else
            {
                resultado = 2;
            }

            return resultado;
        }
        public string GeneratePasswordRandom()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 10;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }

            return contraseniaAleatoria;
        }
        private Boolean ValidateEmail(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<Actions> GetAllOrNotExployees(int userRoleId)
        {
            return ipermissionDA.GetActionsForRoleUser(userRoleId).Where(a => a.Actionsid == 2).ToList();
        }

        public Response Create(Entities.Person personCreate, long userId)
        {
            //Validamos el tipo de acceso que tendra el usuario//
            var actionsByRoleId = ipermissionDA.GetActionsForRoleUser(personCreate.UserRoleId).Where(a => a.Actionsid == 2).ToList();

            if (actionsByRoleId.Count > 0)
            {
                personCreate.Personemployees = new List<PersonEmployee>();

                personCreate.Personemployees = iUserDA.GetEmployeesAll();
            }
            else
            {
                var employeeAll = iUserDA.GetEmployeesAll();

                if(personCreate.EmployeeId != null)
                {
                    foreach (var item in personCreate.EmployeeId.SelectedMultiCompanyId)
                    {
                        personCreate.Personemployees.Add(employeeAll.Where(a => a.EmployeeId == item).FirstOrDefault());
                    }
                }
                
            }

            //Validar la existencia de que el correo electrónico ya exista en el sistema
            if(iUserDA.GetUsersAll().Where(a => a.usersemail == personCreate.Account.Email && (a.statusId == 1 || a.statusId == 2)).Count() > 0)
            {
                throw new Exception("* El correo electrónico ya se encuentra asociado a una cuenta existente.");
            }

            //Validaciones de la creación de una nueva cuenta de usuario//
            ValidatePerson(personCreate);

            string pass = GeneratePasswordRandom();

            personCreate.Account.Password = Encriptar(pass);

            var response = iUserDA.Create(personCreate);

            //Se realiza el envio del correo electronico//
            if(response.Codigo == 0)
            {
                throw new ApplicationException("La nueva cuenta de acceso no pudo ser creada, intente de nuevo.");
            }

            //Seccion de envio de correo
            //Se creará el envio de correo eelctrónico
            //Variable que guarda el asunto//
            StringBuilder subject = new StringBuilder();

            EmailLayout email = new EmailLayout();
            List<EmailReceipts> lstreceipts = new List<EmailReceipts>();
            string userEmail = personCreate.Account.Email;
            files file = new files();


            //Obtenemos el asunto del correo electrónico
            subject.Append("Creación de cuenta de acceso");

            email = iemailDA.GetEmailById(20);

            //Generamos el body del correo electrónico
            email.EmailBody = email.EmailBody.
                Replace("{@NombreDelUsuario}", String.Concat(personCreate.Personname, ' ', personCreate.Personlastname)).
                Replace("{@Email}", personCreate.Account.Email).
                Replace("{@Password}", pass).
                Replace("{@UrlUserImg}", iConfiguration["Resources:UrlSite"] + iConfiguration["Resources:UrlUserImage"]).
                Replace("{@UrlSitio}", iConfiguration["Resources:UrlSite"]).
                Replace("{@UrlImageTeamvox}", iConfiguration["Resources:UrlSite"] + iConfiguration["Resources:UrlLogoTeamVox"]).
                Replace("{@Anio}", DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now).Year.ToString());

            //Creamos el registro del Log del correo electrónico
            EmailLog emailLog = iemailDA.CreateEmailLog(new EmailLog()
            {
                EmailCreateDate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now),
                EmailHtml = email.EmailBody,
                EmailLayoutId = 20,
                EmailSendDate = null,
                EmailStatus = 1,
                EmailSubject = subject.ToString(),
                UserId = userId
            });

            //Damos de alta los destinatarios//
            // 1. Principal
            bool emailDestinatary = iemailDA.CreateDestinatary(new EmailReceipts()
            {
                EmailAddress = personCreate.Account.Email
            }, 1, emailLog.EmailLogId);

            List<System.Net.Mail.Attachment> lstFilesEmail = new List<System.Net.Mail.Attachment>();

            if (file != null)
            {
                if (file.fileContent != null)
                {
                    System.Net.Mail.Attachment fileAtt = new System.Net.Mail.Attachment(new MemoryStream(file.fileContent), file.nameFile + file.extensionFile);


                    lstFilesEmail.Add(fileAtt);
                }
            }

            //Enviamos el Email
            var responseEmail = new EmailBus(iemailDA).SendEmail(
                email,
                lstreceipts,
                subject.ToString(),
                personCreate.Account.Email, lstFilesEmail); //Teniendo los reportes se adjuntaran

            //Si no se envia se deja el estatus a cuando se creo.
            if (responseEmail.Codigo == 0)
            {
                throw new ApplicationException(responseEmail.Mensaje);
            }

            emailLog.EmailSendDate = DateTimeHelper.getCurrentDateTimeWithTimeZone(DateTime.Now);
            emailLog.EmailStatus = 2;
            emailLog = iemailDA.UpdateStatusEmailLog(emailLog);

            response.Mensaje = "El usuario ha sido creado de manera exitosa.";

            return response;
        }

        public void ValidatePerson(Entities.Person person)
        {
            if (string.IsNullOrEmpty(person.Personname))
            {
                throw new ApplicationException("* Complete los campos obligatorios.");
            }

            if (string.IsNullOrEmpty(person.Personlastname))
            {
                throw new ApplicationException("* Complete los campos obligatorios.");
            }

            if(person.UserRoleId == 0)
            {
                throw new ApplicationException("* Complete los campos obligatorios.");
            }

            if (string.IsNullOrEmpty(person.Account.Email))
            {
                throw new ApplicationException("* Complete los campos obligatorios.");
            }
            else
            {
                if (!ValidateEmail(person.Account.Email))
                {
                    throw new ApplicationException("* Introduzca una dirección de correo electrónico válida.");
                }
            }

            if (string.IsNullOrEmpty(person.Account.PhoneNumber))
            {
                throw new ApplicationException("* Complete los campos obligatorios.");
            }
            else
            {
                if(person.Account.PhoneNumber.Length > 10)
                {
                    throw new ApplicationException("* Introduzca su número de teléfono a 10 dígitos.");
                }

                try
                {
                    Convert.ToInt64(person.Account.PhoneNumber);
                }
                catch
                {
                    throw new ApplicationException("* El número de teléfono solo debe de ser numérico.");
                }
            }

            if(person.UserRoleId > 0)
            {
                //Validamos el tipo de acceso que tendra el usuario//
                var actionsByRoleId = ipermissionDA.GetActionsForRoleUser(person.UserRoleId).Where(a => a.Actionsid == 2).ToList();

                if(actionsByRoleId.Count == 0)
                {
                    if(person.Personemployees.Count() == 0)
                    {
                        throw new ApplicationException("* Deberá de seleccionar al menos un empleado.");
                    }
                }
            }

            if(person.Raprofile.Defaultserviceorderid == 0 || person.Raprofile.Unitsalesid == 0 || person.Raprofile.Taxresidenceid == 0 ||
                person.Raprofile.Locationidresidence == 0 || person.Raprofile.Locationidrepair == 0 || person.Raprofile.Locationidavailable == 0 ||
                person.Raprofile.Locationidquality == 0 || person.Raprofile.Distributionchannelid == 0)
            {
                throw new ApplicationException("* Complete los campos obligatorios.");
            }

        }
    }
}
