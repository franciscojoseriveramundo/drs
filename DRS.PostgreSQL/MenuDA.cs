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
    public class MenuDA : IMenuDA
    {
        public List<Entities.Menu> GetMenusByIdUser(long userId)
        {
            List<Entities.Menu> menuList = new List<Entities.Menu>();

            using(var dRS = new DRSContext())
            {
                menuList = (from a in dRS.Menus
                            join b in dRS.Menuuserroles on a.Menuid equals b.Menuid
                            join c in dRS.Userroles on b.Userroleid equals c.Userroleid
                            join d in dRS.Users on c.Userroleid equals d.Userrole
                            where d.Usersid == userId
                            select new Entities.Menu
                            {
                                Menuaction = a.Menuaction,
                                Menucontroller = a.Menucontroller,
                                Menucreatedate = a.Menucreatedate,
                                Menuid = a.Menuid,
                                Menuenabled = a.Menuenabled,
                                Menuicon = a.Menuicon,
                                Menuislinked = a.Menuislinked,
                                Menuname = a.Menuname,
                                Menuorder = a.Menuorder,
                                Menuparentid = a.Menuparentid
                            }).ToList();
            }

            return menuList;
        }
    }
}
