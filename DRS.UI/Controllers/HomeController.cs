using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DRS.Entities;
using DRS.Business;
using DRS.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace DRS.UI.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDataProtector _protector;

        UserBus userBus = AppCore.Container.GetInstance<UserBus>();
        LogBus logBus = AppCore.Container.GetInstance<LogBus>();
        MenuBus menuBus = AppCore.Container.GetInstance<MenuBus>();

        public HomeController(
            ILogger<HomeController> logger, IDataProtectionProvider provider
            )
        {
            this._logger = logger;
            this._protector = provider.CreateProtector("test-purpose");
        }

        [HttpGet]
        [NoAuthenticate]
        public IActionResult Index(string mensaje = "", string alert = "", Login login = null)
        {
            HttpContext.Session.SetString("Username", "");

            if (String.IsNullOrEmpty(mensaje) && String.IsNullOrEmpty(alert))
            {
                ViewBag.Message = "Introduce tus credenciales de acceso";
                ViewBag.TypeAlert = "alert alert-info";
            }
            else
            {
                ViewBag.Message = mensaje;
                ViewBag.TypeAlert = alert;
            }

            return View();
        }

        [HttpGet]
        [Route("/Home/Login")]
        [NoAuthenticate]
        public IActionResult Login(Login login)
        {
            ViewBag.Message = HttpContext.Session.GetString("Message");
            ViewBag.TypeAlert = HttpContext.Session.GetString("TypeAlert");

            if(string.IsNullOrEmpty(ViewBag.Message) && string.IsNullOrEmpty(ViewBag.TypeAlert))
            {
                ViewBag.Message = "Sus credenciales de acceso son incorrectas.";
                ViewBag.TypeAlert = "alert alert-danger";
            }

            ViewBag.Title = "Inicio de sesión";

            return View(login);
        }

        [HttpPost]
        //[AllowAnonymousAttribute]
        [NoAuthenticate]
        public async Task<IActionResult> LoginAsync(Login login)
        {
            
            ViewBag.Message = "Sus credenciales introducidas son incorrectas.";
            ViewBag.TypeAlert = "alert alert-danger";
            ViewBag.Title = "Inicio de sesión";

            try
            {
                Users users = userBus.GetUserForLogin(login);

                if (users != null)
                {
                    //Añadimos la sesión del usuario.
                    var principal = new SessionHelper().AddSessionAsync(users);

                    AuthenticationProperties authenticationProperties = new AuthenticationProperties() { IsPersistent = true, AllowRefresh =true};

                    //Si el usuario marca que debe de recordar su contraseña esta nunca va a caducar.
                    if (login.RememberMe)
                    {
                        authenticationProperties.ExpiresUtc = DateTime.UtcNow.AddYears(200);
                    }
                    else //Si no la marca se establece que la duración de la sesión será de 1 hora.
                    {
                        authenticationProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1);
                    }

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                    ViewBag.Usuario = users;

                    ViewBag.Menus = menuBus.GetMenusByIdUser(users.usersid);

                    HttpContext.Session.SetString("UrlImageLogin", ""); 
                    ViewBag.UrlImageLogin = "";

                    HttpContext.Session.SetString("Username", users.usersemail);

                    return RedirectToAction("Index", "Devoluciones");

                }
                else
                {
                    
                    HttpContext.Session.SetString("Message", "Sus credenciales de acceso son incorrectas.");
                    HttpContext.Session.SetString("TypeAlert", "alert alert-danger");

                    ViewBag.Message = "Sus credenciales de acceso son incorrectas.";
                    ViewBag.TypeAlert = "alert alert-danger";

                    logBus.CreateIncident(new LogIncidencia()
                    {
                        Descripcion = "Las credenciales del usuario " + login.Username + " son incorrectas.",
                        FechaCreacion = DateTime.UtcNow,
                        IncidenciaId = 0,
                        UsuarioId = 0
                    });

                    ViewBag.Title = "Inicio de sesión";

                    return RedirectToAction("Login", login);

                    
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("Message", ex.Message.ToString());
                HttpContext.Session.SetString("TypeAlert", "alert alert-danger");

                ViewBag.Message = ex.Message.ToString();
                ViewBag.TypeAlert = "alert alert-danger";

                ViewBag.Title = "Inicio de sesión";

                return RedirectToAction("Login", login);

                
            }

        }

        [HttpPost]
        [NoAuthenticate]
        public async Task<JsonResult> LoginExternalAsync(string email = "")
        {
            ViewBag.Title = "Inicio de sesión";

            Response_Login response = new Response_Login();

            try
            {
                Users users = userBus.GetUserForLoginExternal(email);

                if (users != null)
                {

                    var principal = new SessionHelper().AddSessionAsync(users);

                    AuthenticationProperties authenticationProperties = new AuthenticationProperties() { IsPersistent = true, AllowRefresh = true };

                    authenticationProperties.ExpiresUtc = DateTimeOffset.MaxValue;

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                    ViewBag.Usuario = users;

                    

                    ViewBag.Menus = menuBus.GetMenusByIdUser(users.usersid);

                    HttpContext.Session.SetString("Username", users.usersemail);

                   


                    response.message = "Exitoso";
                    response.typealert = "1";

                }
                else
                {
                    response.message = "El usuario ingresado no tiene permisos de acceso a la aplicación.";
                    response.typealert = "alert alert-danger";

                    logBus.CreateIncident(new LogIncidencia()
                    {
                        Descripcion = "El usuario " + email + " no tiene permisos de acceso a la aplicación.",
                        FechaCreacion = DateTime.UtcNow,
                        IncidenciaId = 0,
                        UsuarioId = 0
                    });

                    //return RedirectToAction("Index", "Home", new { mensaje = "Sus credenciales de acceso son incorrectas.", alert = "alert alert-danger" });
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message.ToString();
                response.typealert = "alert alert-danger";

                logBus.CreateIncident(new LogIncidencia()
                {
                    Descripcion = ex.Message,
                    FechaCreacion = DateTime.UtcNow,
                    IncidenciaId = 0,
                    UsuarioId = 0
                });
            }

            return Json(response);
           
        }

        [Authenticate]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.SetString("Username", "");

            return RedirectToAction("Index", "Home", new { mensaje = "Su sesión ha sido cerrada de manera exitosa.", alert= "alert alert-success" });
        }

        [NoAuthenticate]
        public IActionResult Privacy()
        {
            //if (this.User.Identity.IsAuthenticated)
            //{
            //    return View();
            //}
            
            return RedirectToAction("Index", "Home");
        }

        [NoAuthenticate]
        [HttpGet]
        public IActionResult Recovery()
        {
            return View();
        }

        [NoAuthenticate, HttpGet]
        public IActionResult RecoveryIndex()
        {

            ViewBag.Message = "Ingrese su correo electrónico.";
            ViewBag.TypeAlert = "alert alert-info";

            ViewBag.Title = "Recuperación de contraseña";

            return PartialView("_Recovery", new Recovery());
        }
        [HttpPost]
        [NoAuthenticate]
        public IActionResult Recovery(Recovery recovery)
        {
            try
            {
                if (string.IsNullOrEmpty(recovery.Email))
                {
                    ViewBag.Message = "* Debe de ingresar un correo electrónico.";
                    ViewBag.TypeAlert = "alert alert-danger";
                }
                else
                {
                    var response = userBus.GetUserForEmail(recovery);

                    ViewBag.Message = "Revise su bandeja de entrada para poder restaurar su acceso. El enlace enviado tiene una vigencia de 15 minutos.";
                    ViewBag.TypeAlert = "alert alert-success";
                }

                recovery.Email = String.Empty;

                ViewBag.Title = "Recuperación de contraseña";

                
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.TypeAlert = "alert alert-danger";
            }

            return PartialView("_Recovery", recovery);
        }

        [HttpGet]
        [NoAuthenticate]
        public IActionResult AccountRecovery(string key = "")
        {
            ViewBag.Key = key;

            return View();
        }

        [HttpGet]
        [NoAuthenticate]
        public IActionResult AccountRecoveryIndex(string key = "")
        {

            AccountRecovery account = new AccountRecovery();

            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    ViewBag.Message = "* No tiene acceso a esta sección.";
                    ViewBag.TypeAlert = "alert alert-danger";
                    ViewBag.HideForm = "OK";
                }
                else
                {
                    var response = userBus.ComprobateVigency(key);

                    var keyInfo = userBus.getRecoveryAccess(key);

                    if(keyInfo == null)
                    {
                        ViewBag.HideForm = "OK";

                        throw new ApplicationException("* La enlace introducido no existe.");
                    }
                    else
                    {
                        if(keyInfo.Status == 0)
                        {
                            ViewBag.HideForm = "OK";

                            throw new ApplicationException("* El enlace ya no se encuentra disponible.");
                        }
                    }

                    var infoUser = userBus.GetUsers().Where(a => a.usersid == keyInfo.UserId).FirstOrDefault();

                    if(infoUser == null)
                    {
                        ViewBag.HideForm = "OK";

                        throw new ApplicationException("* El usuario relacionado al enlace proporcionado ya no existe.");
                    }

                    if(infoUser.statusId == 3)
                    {
                        ViewBag.HideForm = "OK";

                        throw new ApplicationException("* No se puede realizar la recuperación de una cuenta desactivada.");
                    }

                    if (response.Codigo == 0)
                    {
                        ViewBag.Message = response.Mensaje;
                        ViewBag.TypeAlert = "alert alert-warning";

                        return PartialView("_AccountRecovery", account);
                    }

                    account.UserId = infoUser.usersid;
                    account.Key = key;
                    account.Password = "";
                    account.ConfirmPassword = "";
                    account.Email = infoUser.usersemail;

                    ViewBag.Message = "Cree su nueva contraseña de acceso esta deberá de estar constituida por lo mínimo 8 carácteres, una mayúscula y un número. Y además de no haberla utilizado anteriormente.";
                    ViewBag.TypeAlert = "alert alert-info";
                }

                ViewBag.Title = "Cambio de contraseña";
            }
            catch(Exception ex)
            {
                account.UserId = 0;
                account.Key = key;
                account.Password = "";
                account.ConfirmPassword = "";
                account.Email = "";

                ViewBag.Message = ex.Message;
                ViewBag.TypeAlert = "alert alert-danger";
            }

            return PartialView("_AccountRecovery", account);
        }

        [HttpPost]
        [NoAuthenticate]
        public IActionResult AccountRecoveryConfirm(AccountRecovery account)
        {
            try
            {
                if (string.IsNullOrEmpty(account.Key))
                {
                    ViewBag.HideForm = "OK";
                    ViewBag.Message = "* No tiene acceso a esta sección.";
                    ViewBag.TypeAlert = "alert alert-danger";
                }
                else
                {
                    var response = userBus.ComprobateVigency(account.Key);

                    if (response.Codigo == 0)
                    {
                        ViewBag.Message = response.Mensaje;
                        ViewBag.TypeAlert = "alert alert-warning";
                    }

                    var keyInfo = userBus.getRecoveryAccess(account.Key);

                    if (keyInfo == null)
                    {
                        ViewBag.HideForm = "OK";
                        throw new ApplicationException("* El enlace no se encuentra disponible.");
                    }

                    var infoUser = userBus.GetUsers().Where(a => a.usersid == keyInfo.UserId).FirstOrDefault();

                    if (infoUser == null)
                    {
                        ViewBag.HideForm = "OK";
                        throw new ApplicationException("* El usuario relacionado al enlace proporcionado ya no existe.");
                    }

                    if (infoUser.statusId == 3)
                    {
                        ViewBag.HideForm = "OK";
                        throw new ApplicationException("* No se puede realizar la recuperación de una cuenta desactivada.");
                    }

                    var responseCh = userBus.ChangePassword(account);

                    ViewBag.TypeAlert = "alert alert-success";
                    ViewBag.Message = responseCh.Mensaje;

                    if (responseCh.Codigo == 0)
                    {
                        ViewBag.TypeAlert = "alert alert-danger";
                    }
                    
                }

                ViewBag.Title = "Cambio de contraseña";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.TypeAlert = "alert alert-danger";
            }

            return PartialView("_AccountRecovery", account);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        [HttpGet]
        [Authenticate]
        public IActionResult Menu()
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();
            Int64 personId = Convert.ToInt64(user[5].Value.ToString());
            Int64 userId = Convert.ToInt64(user[4].Value.ToString());

            return PartialView("Menu", menuBus.GetMenusByIdUser(userId));
        }

        public JsonResult SessionStatus()
        {
            bool isSession = true;

            if (!User.Identity.IsAuthenticated)
            {
                isSession = false;
            }

            return Json(new
            {
                StsUsr = isSession
            });
        }

        public ActionResult EndSession()
        {
            return RedirectToAction("Index", new { mensaje = "Su sesión ha expirado. Debe de iniciar nuevamente.", alert = "alert alert-warning" });
        }
    }
}
