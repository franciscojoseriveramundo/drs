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
    public class ProfileController : Controller
    {
        UserBus userBus = AppCore.Container.GetInstance<UserBus>();
        MenuBus menuBus = AppCore.Container.GetInstance<MenuBus>();
        DevolucionesBus devolucionesBus = AppCore.Container.GetInstance<DevolucionesBus>();

        public IActionResult Index()
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();
            ViewBag.Usuario = user[2].Value.ToString();

            ViewBag.Menus = menuBus.GetMenusByIdUser(Convert.ToInt64(user[4].Value.ToString()));

            return View();
        }

        [HttpGet]
        [Authenticate]
        public IActionResult IndexIni()
        {

            ViewBag.Message = "Información de tu cuenta de perfil de usuario.";
            ViewBag.TypeAlert = "alert alert-info";

            return PartialView("_Index");
        }
        [HttpGet]
        [Authenticate]
        public IActionResult GetAddress(long clientId = 0)
        {
            if(clientId == 0)
            {
                return Json(new
                {
                    address = "Seleccione un cliente para mostrar la dirección."
                });
            }

            var user = this.User.Claims.First().Subject.Claims.ToList();
            Int64 personId = Convert.ToInt64(user[5].Value.ToString());

            var clientInfo = (userBus.GetClients(personId)).Where(a => a.IdCliente == clientId).FirstOrDefault();

            if (clientInfo != null)
            {
                
                var clientAddressInfo = userBus.GetAddressClientById(clientInfo.IdCliente);

                if (clientAddressInfo != null)
                {
                    return Json(new
                    {
                        address = clientAddressInfo.Clientaddressstreetname + " " + clientAddressInfo.Clientaddressnumber + "/" + clientAddressInfo.Clientaddresspostalcode + " " + clientAddressInfo.Clientaddressdistrictname + ", " + clientAddressInfo.Clientaddresscityname + ", " + clientAddressInfo.Clientaddressregioncode + ", " + clientAddressInfo.Clientaddresscountry
                    }); 
                }
                else
                {
                    return Json(new
                    {
                        address = "Sin información encontrada de la dirección del cliente"
                    });

                }
            }
            else
            {

                return Json(new
                {
                    address = "Sin información encontrada de la dirección del cliente"
                });
            }

        }

        [HttpGet]
        [Authenticate]
        public IActionResult InformationGeneral()
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();
            Int64 personId = Convert.ToInt64(user[5].Value.ToString());
            Int64 userId = Convert.ToInt64(user[4].Value.ToString());

            InformationProfile profile = new InformationProfile();

            var userInfo = userBus.GetUsers().Where(a => a.usersid == userId).FirstOrDefault();

            if(userInfo != null)
            {
                profile.UserId = userId;
                profile.Name = userInfo.personname;
                profile.LastName = userInfo.personlastname;
                profile.ClientName = 0;
                profile.Address = "Seleccione un cliente para mostrar la dirección.";

                ViewBag.ClientesM = devolucionesBus.GetClientes(userBus.GetClients(personId));

            }
            else
            {
                profile.UserId = 0;
                profile.Name = String.Empty;
                profile.LastName = String.Empty;
                profile.ClientName = 0;
                profile.Address = String.Empty;
            }


            return PartialView("_InformationGeneral", profile);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult Contact()
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();
            Int64 personId = Convert.ToInt64(user[5].Value.ToString());
            Int64 userId = Convert.ToInt64(user[4].Value.ToString());

            ContactProfile profile = new ContactProfile();

            var userInfo = userBus.GetUsers().Where(a => a.usersid == userId).FirstOrDefault();

            if (userInfo != null)
            {
                profile.Email = userInfo.usersemail;

                var clientInfo = userBus.GetClients(personId).FirstOrDefault();

                if (clientInfo != null)
                {
                    var clientAddressInfo = userBus.GetAddressClientById(clientInfo.IdCliente);

                    if (clientAddressInfo != null)
                    {
                        if(userInfo.usersphone == null)
                        {
                            profile.Phone = "No registrado";
                        }
                        else
                        {
                            if (userInfo.usersphone.Length == 0)
                            {
                                profile.Phone = "No registrado";
                            }
                            else
                            {
                                profile.Phone = String.Format("{0:(###) ###-####}", userInfo.usersphone);
                            }
                        }
                        
                    }
                    else
                    {
                        profile.Phone = "No registrado";

                    }
                }
                else
                {
                    profile.Phone = String.Empty;
                }

            }
            else
            {
                profile.UserId = 0;
                profile.Phone = string.Empty;
                profile.Email = string.Empty;
            }


            return PartialView("_Contact", profile);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult Security()
        {
            ViewBag.TypeAlertSecurity = "alert alert-info";
            ViewBag.MessageSecurity = "Para el cambio de contraseña de acceso deberá de estar constituida por lo mínimo 8 carácteres, una mayúscula y un número. Y además de no haberla utilizado anteriormente.";

            var user = this.User.Claims.First().Subject.Claims.ToList();
            Int64 personId = Convert.ToInt64(user[5].Value.ToString());
            Int64 userId = Convert.ToInt64(user[4].Value.ToString());

            SecurityProfile profile = new SecurityProfile();
            profile.UserId = userId;

            return PartialView("_Security", profile);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult ChangePassword(SecurityProfile profile)
        {
            Int64 userId = profile.UserId;

            try
            {
                ViewBag.TypeAlertSecurity = "alert alert-success";
                ViewBag.MessageSecurity = "* La contraseña ha sido cambiada de manera exitosa.";

                var user = this.User.Claims.First().Subject.Claims.ToList();
                Int64 personId = Convert.ToInt64(user[5].Value.ToString());
                userId = Convert.ToInt64(user[4].Value.ToString());

                var userInfo = userBus.GetUsers().Where(a => a.usersid == userId).FirstOrDefault();

                if (string.IsNullOrEmpty(profile.Password) || string.IsNullOrEmpty(profile.PasswordConfirm) || string.IsNullOrEmpty(profile.PasswordActual))
                {
                    throw new ApplicationException("* Complete los campos obligatorios.");
                }

                if (profile.Password != profile.PasswordConfirm)
                {
                    throw new ApplicationException("* Las contraseñas no coínciden.");
                }

                var loginSimulate = userBus.GetUserForLogin(new Login()
                {
                    Password = profile.Password,
                    Username = userInfo.usersemail
                });

                var response = userBus.ChangePassword(new AccountRecovery()
                {
                    ConfirmPassword = profile.PasswordConfirm,
                    Email = userInfo.usersemail,
                    Key = string.Empty,
                    Password = profile.Password,
                    UserId = profile.UserId
                });

                if(response.Codigo == 0)
                {
                    throw new ApplicationException(response.Mensaje);
                }

                profile = new SecurityProfile();
                profile.UserId = userId;
                profile.Password = String.Empty;
                profile.PasswordConfirm = String.Empty;
                profile.PasswordActual = String.Empty;

                
            }
            catch(Exception ex)
            {
                profile = new SecurityProfile();
                profile.UserId = userId;
                profile.Password = String.Empty;
                profile.PasswordConfirm = String.Empty;
                profile.PasswordActual = String.Empty;

                ViewBag.TypeAlertSecurity = "alert alert-danger";
                ViewBag.MessageSecurity = ex.Message;
            }

            return PartialView("_Security", profile);
        }
    }
}
