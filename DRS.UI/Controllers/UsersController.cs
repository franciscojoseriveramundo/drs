using DRS.Business;
using DRS.Entities;
using DRS.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DRS.UI.Controllers
{
    public class UsersController : Controller
    {
        UserBus userBus = AppCore.Container.GetInstance<UserBus>();
        LogBus logBus = AppCore.Container.GetInstance<LogBus>();
        PermissionBus permissionBus = AppCore.Container.GetInstance<PermissionBus>();
        MenuBus menuBus = AppCore.Container.GetInstance<MenuBus>();

        public IActionResult Index()
        {
            var user = this.User.Claims.First().Subject.Claims.ToList();
            ViewBag.Usuario = user[2].Value.ToString();

            ViewBag.Menus = menuBus.GetMenusByIdUser(Convert.ToInt64(user[4].Value.ToString()));


            return View();
        }

        public IActionResult IndexIni()
        {
            List<UsersViewModel> lstUsers = new List<UsersViewModel>();

            var user = this.User.Claims.First().Subject.Claims.ToList();

            List<Actions> actions = permissionBus.GetActionsByIdUserAndMenuId(Convert.ToInt64(user[5].Value.ToString()), 3);

            ViewBag.Menus = actions;

            //lstDevoluciones = devolucionesBus.GetDevoluciones(ClientId, ReasonSend, Status, Convert.ToInt64(user[4].Value.ToString()));
            lstUsers = userBus.GetUsersForIndex().Where(a => a.Userstatusid != 3).ToList();

            return PartialView("_Index", lstUsers);
        }

        public IActionResult New()
        {
            ViewBag.MessageNew = "* Complete los campos obligatorios para la creación de un nuevo usuario.";
            ViewBag.TypeAlertNew = "alert alert-info";

            ViewBag.UserRole = userBus.GetUserRolesSelectList();
            ViewBag.Employee = userBus.GetEmployeesList();
            ViewBag.UnitSales = userBus.GetUnitSalesList();

            ViewBag.TaxResidence = userBus.GetTaxResidenceList();

            var getLocation = userBus.GetLocation();

            ViewBag.Location1 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "RECEPCIÓN").ToList());
            ViewBag.Location2 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "REPARACIÓN").ToList());
            ViewBag.Location3 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "DISPONIBLE").ToList());
            ViewBag.Location4 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "CALIDAD").ToList());

            ViewBag.Channel = userBus.GetChannelList();
            ViewBag.Default = userBus.GetDefaultServiceList();

            Person person = new Person()
            {
                Account = new Account(),
                Personemployees = new List<PersonEmployee>(),
                Users = new Users(),
                Raprofile = new RAprofile(),
                EmployeeId = new MultiSelectDropDownEmployee()
            };

            return PartialView("_New", person);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult Create(Person person)
        {
            try
            {
                ViewBag.MessageNew = "* Complete los campos obligatorios para la creación de un nuevo usuario.";
                ViewBag.TypeAlertNew = "alert alert-info";

                ViewBag.UserRole = userBus.GetUserRolesSelectList();
                ViewBag.Employee = userBus.GetEmployeesList();
                ViewBag.UnitSales = userBus.GetUnitSalesList();

                ViewBag.TaxResidence = userBus.GetTaxResidenceList();

                var getLocation = userBus.GetLocation();

                ViewBag.Location1 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "RECEPCIÓN").ToList());
                ViewBag.Location2 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "REPARACIÓN").ToList());
                ViewBag.Location3 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "DISPONIBLE").ToList());
                ViewBag.Location4 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "CALIDAD").ToList());

                ViewBag.Channel = userBus.GetChannelList();
                ViewBag.Default = userBus.GetDefaultServiceList();

                person.Personemployees = new List<PersonEmployee>();

                if (person.EmployeeId == null)
                {
                    person.EmployeeId = new MultiSelectDropDownEmployee();
                    person.EmployeeId.SelectedMultiCompanyId = new List<long>();
                }

                var user = this.User.Claims.First().Subject.Claims.ToList();

                var response = userBus.Create(person, Convert.ToInt64(user[4].Value.ToString()));

                if(response.Codigo == 0)
                {
                    throw new ApplicationException(response.Mensaje);
                }

                ViewBag.MessageNew = response.Mensaje;
                ViewBag.TypeAlertNew = "alert alert-success";

                person = new Person()
                {
                    Account = new Account(),
                    Personemployees = new List<PersonEmployee>(),
                    Users = new Users(),
                    Raprofile = new RAprofile(),
                    EmployeeId = new MultiSelectDropDownEmployee()
                };
            }
            catch(Exception ex)
            {
                ViewBag.MessageNew = ex.Message;
                ViewBag.TypeAlertNew = "alert alert-danger";
            }

            return PartialView("_New", person);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult Modify(long UserId)
        {
            ViewBag.MessageNew = "* No olvide los campos obligatorios para poder realizar la edición del usuario.";
            ViewBag.TypeAlertNew = "alert alert-info";

            ViewBag.UserRole = userBus.GetUserRolesSelectList();
            ViewBag.Employee = userBus.GetEmployeesList();
            ViewBag.UnitSales = userBus.GetUnitSalesList();
            ViewBag.Status = userBus.GetStatusUser();

            ViewBag.TaxResidence = userBus.GetTaxResidenceList();

            var getLocation = userBus.GetLocation();

            ViewBag.Location1 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "RECEPCIÓN").ToList());
            ViewBag.Location2 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "REPARACIÓN").ToList());
            ViewBag.Location3 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "DISPONIBLE").ToList());
            ViewBag.Location4 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "CALIDAD").ToList());

            ViewBag.Channel = userBus.GetChannelList();
            ViewBag.Default = userBus.GetDefaultServiceList();

            var personInformation = userBus.GetUser(UserId);

            if (string.IsNullOrEmpty(personInformation.Account.PhoneNumber))
            {
                personInformation.Account.PhoneNumber = "";
            }

            ViewBag.IsPossible = "1";

            if (personInformation.Users.statusId == 3)
            {
                ViewBag.IsPossible = "0";

                ViewBag.MessageNew = "* Este usuario ya fue dado de baja del sistema por lo cual no puede ser editado.";
                ViewBag.TypeAlertNew = "alert alert-danger";
            }

            return PartialView("_Modify", personInformation);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult Modify(Person person)
        {
            ViewBag.MessageNew = "* No olvide los campos obligatorios para poder realizar la edición del usuario.";
            ViewBag.TypeAlertNew = "alert alert-info";

            ViewBag.UserRole = userBus.GetUserRolesSelectList();
            ViewBag.Employee = userBus.GetEmployeesList();
            ViewBag.UnitSales = userBus.GetUnitSalesList();
            ViewBag.Status = userBus.GetStatusUser();

            ViewBag.TaxResidence = userBus.GetTaxResidenceList();

            var getLocation = userBus.GetLocation();

            ViewBag.Location1 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "RECEPCIÓN").ToList());
            ViewBag.Location2 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "REPARACIÓN").ToList());
            ViewBag.Location3 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "DISPONIBLE").ToList());
            ViewBag.Location4 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "CALIDAD").ToList());

            ViewBag.Channel = userBus.GetChannelList();
            ViewBag.Default = userBus.GetDefaultServiceList();

            var personInformation = userBus.GetUser(person.Users.usersid);

            if (string.IsNullOrEmpty(personInformation.Account.PhoneNumber))
            {
                personInformation.Account.PhoneNumber = "";
            }

            ViewBag.IsPossible = "1";

            if (personInformation.Users.statusId == 3)
            {
                ViewBag.IsPossible = "0";

                ViewBag.MessageNew = "* Este usuario ya fue dado de baja del sistema por lo cual no puede ser editado.";
                ViewBag.TypeAlertNew = "alert alert-danger";

                return PartialView("_Modify", personInformation);
            }

            try
            {
                var response = userBus.Modify(person);

                if (response.Codigo == 0)
                {
                    throw new ApplicationException(response.Mensaje);
                }

                ViewBag.IsPossible = "1";

                ViewBag.MessageNew = "El usuario ha sido modificado correctamente.";
                ViewBag.TypeAlertNew = "alert alert-success";
            }
            catch(Exception ex)
            {
                ViewBag.IsPossible = "1";

                ViewBag.MessageNew = ex.Message;
                ViewBag.TypeAlertNew = "alert alert-danger";
            }

            return PartialView("_Modify", personInformation);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult Delete(long UserId)
        {
            ViewBag.MessageNew = "* ¿Desea dar de baja el usuario seleccionado?";
            ViewBag.TypeAlertNew = "alert alert-info";

            ViewBag.UserRole = userBus.GetUserRolesSelectList();
            ViewBag.Employee = userBus.GetEmployeesList();
            ViewBag.UnitSales = userBus.GetUnitSalesList();
            ViewBag.Status = userBus.GetStatusUser();

            ViewBag.TaxResidence = userBus.GetTaxResidenceList();

            var getLocation = userBus.GetLocation();

            ViewBag.Location1 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "RECEPCIÓN").ToList());
            ViewBag.Location2 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "REPARACIÓN").ToList());
            ViewBag.Location3 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "DISPONIBLE").ToList());
            ViewBag.Location4 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "CALIDAD").ToList());

            ViewBag.Channel = userBus.GetChannelList();
            ViewBag.Default = userBus.GetDefaultServiceList();

            var personInformation = userBus.GetUser(UserId);

            if (string.IsNullOrEmpty(personInformation.Account.PhoneNumber))
            {
                personInformation.Account.PhoneNumber = "No registrado";
            }

            ViewBag.IsPossible = "1";

            if (personInformation.Users.statusId == 3)
            {
                ViewBag.IsPossible = "0";

                ViewBag.MessageNew = "* Este usuario ya fue dado de baja del sistema, por lo cual no se puede completar el proceso.";
                ViewBag.TypeAlertNew = "alert alert-danger";
            }

            return PartialView("_Delete", personInformation);
        }

        [HttpPost]
        [Authenticate]
        public IActionResult Delete(Person person)
        {
            ViewBag.MessageNew = "* ¿Desea dar de baja el usuario seleccionado?";
            ViewBag.TypeAlertNew = "alert alert-info";

            ViewBag.UserRole = userBus.GetUserRolesSelectList();
            ViewBag.Employee = userBus.GetEmployeesList();
            ViewBag.UnitSales = userBus.GetUnitSalesList();
            ViewBag.Status = userBus.GetStatusUser();

            ViewBag.TaxResidence = userBus.GetTaxResidenceList();

            var getLocation = userBus.GetLocation();

            ViewBag.Location1 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "RECEPCIÓN").ToList());
            ViewBag.Location2 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "REPARACIÓN").ToList());
            ViewBag.Location3 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "DISPONIBLE").ToList());
            ViewBag.Location4 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "CALIDAD").ToList());

            ViewBag.Channel = userBus.GetChannelList();
            ViewBag.Default = userBus.GetDefaultServiceList();

            var personInformation = userBus.GetUser(person.Users.usersid);

            if (string.IsNullOrEmpty(personInformation.Account.PhoneNumber))
            {
                personInformation.Account.PhoneNumber = "No registrado";
            }

            ViewBag.IsPossible = "1";

            if (personInformation.Users.statusId == 3)
            {
                ViewBag.IsPossible = "0";

                ViewBag.MessageNew = "* Este usuario ya fue dado de baja del sistema, por lo cual no se puede completar el proceso.";
                ViewBag.TypeAlertNew = "alert alert-danger";
            }

            try
            {
                var response = userBus.Delete(person);

                if(response.Codigo == 0)
                {
                    throw new ApplicationException(response.Mensaje);
                }

                ViewBag.IsPossible = "0";

                ViewBag.MessageNew = "El usuario ha sido dado de baja del sistema.";
                ViewBag.TypeAlertNew = "alert alert-success";
            }
            catch (Exception ex)
            {
                ViewBag.IsPossible = "0";

                ViewBag.MessageNew = ex.Message;
                ViewBag.TypeAlertNew = "alert alert-danger";
            }

            return PartialView("_Delete", personInformation);
        }

        [HttpGet]
        [Authenticate]
        public IActionResult Details(long UserId)
        {
            ViewBag.MessageNew = "* Información del usuario seleccionado.";
            ViewBag.TypeAlertNew = "alert alert-info";

            ViewBag.UserRole = userBus.GetUserRolesSelectList();
            ViewBag.Employee = userBus.GetEmployeesList();
            ViewBag.UnitSales = userBus.GetUnitSalesList();
            ViewBag.Status = userBus.GetStatusUser();

            ViewBag.TaxResidence = userBus.GetTaxResidenceList();

            var getLocation = userBus.GetLocation();

            ViewBag.Location1 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "RECEPCIÓN").ToList());
            ViewBag.Location2 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "REPARACIÓN").ToList());
            ViewBag.Location3 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "DISPONIBLE").ToList());
            ViewBag.Location4 = userBus.GetLocationList(getLocation.Where(a => a.LocationType == "CALIDAD").ToList());

            ViewBag.Channel = userBus.GetChannelList();
            ViewBag.Default = userBus.GetDefaultServiceList();

            var personInformation = userBus.GetUser(UserId);

            if (string.IsNullOrEmpty(personInformation.Account.PhoneNumber))
            {
                personInformation.Account.PhoneNumber = "No registrado";
            }

            return PartialView("_Details", personInformation);
        }

        public IActionResult GetAllOrNotExployees(int userRoleId)
        {
            if(userRoleId == 0)
            {
                return Json(new
                {
                    response = 0
                });
            }

            if(userBus.GetAllOrNotExployees(userRoleId).Count() == 0)
            {
                return Json(new
                {
                    response = 1
                });
            }

            return Json(new
            {
                response = 0
            });
        }
    }
}
