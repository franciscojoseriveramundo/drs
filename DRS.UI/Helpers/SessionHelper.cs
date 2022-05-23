using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using DRS.Entities;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DRS.UI.Helpers
{
    public class SessionHelper : ControllerBase
    {
        //Verifica si el usuario actual se encuentra en sesión o no.
        public static bool ExistUserInSession()
        {
            return System.Security.Claims.ClaimsPrincipal.Current.Identity.IsAuthenticated;
        }

        public ClaimsPrincipal AddSessionAsync(Users users)
        {
            string[] personname = users.personname.Split(' ');
            string[] personlastname = users.personlastname.Split(' ');

            var claimsPrincipal = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new Claim[] { new Claim(ClaimTypes.NameIdentifier, "AuthenticateDRS"),
                            new Claim("username", users.usersemail),
                            new Claim("name", personname[0]),
                            new Claim("lastname", personlastname[0]),
                            new Claim("userid", users.usersid.ToString()),
                            new Claim("personid", users.personid.ToString()),
                            new Claim("employeeuid", JsonSerializer.Serialize(users.cee_uuid))
                            },
                            CookieAuthenticationDefaults.AuthenticationScheme
                         )
                    );

            return claimsPrincipal;
        }
    }
}
