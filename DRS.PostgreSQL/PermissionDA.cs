using DRS.Data;
using DRS.Entities;
using DRS.PostgreSQL.Models;
//using DRS.PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRS.PostgreSQL
{
    public class PermissionDA : IPermissionDA
    {
        public List<Actions> GetActionsByIdUserAndMenuId(long userId, long menuId)
        {
            List<Entities.Actions> actionList = new List<Entities.Actions>();

            using (var dRS = new DRSContext())
            {
                actionList = (from a in dRS.Actions
                              join b in dRS.Actionsmenus on a.Actionsid equals b.Actionsid
                              join c in dRS.Permissions on b.Actionsid equals c.Actionsid
                              join d in dRS.Userroles on c.Userroleid equals d.Userroleid
                              where b.Menuid == menuId && d.Userroleid == userId 
                              select new Actions
                              {
                                  Actionsdescription = a.Actionsdescription,
                                  Actionsid = a.Actionsid,
                                  Actionsorderid = a.Actionsorderid,
                                  Actionsparentid = a.Actionsparentid,
                                  Actionstypeid = a.Actionsparentid
                              }
                            ).ToList();
            }

            return actionList;
        }

        public List<Actions> GetActionsForRoleUser(int userRoleId)
        {
            List<Entities.Actions> actionList = new List<Entities.Actions>();

            using (var dRS = new DRSContext())
            {
                actionList = (from a in dRS.Actions
                              join b in dRS.Actionsmenus on a.Actionsid equals b.Actionsid
                              join c in dRS.Permissions on b.Actionsid equals c.Actionsid
                              join d in dRS.Userroles on c.Userroleid equals d.Userroleid
                              where d.Userroleid == userRoleId
                              select new Actions
                              {
                                  Actionsdescription = a.Actionsdescription,
                                  Actionsid = a.Actionsid,
                                  Actionsorderid = a.Actionsorderid,
                                  Actionsparentid = a.Actionsparentid,
                                  Actionstypeid = a.Actionsparentid
                              }
                            ).ToList();
            }

            return actionList;
        }
    }
}
