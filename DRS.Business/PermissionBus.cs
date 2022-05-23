using DRS.Data;
using DRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Business
{
    public class PermissionBus
    {
        private readonly IPermissionDA _ipermissionDA;
        private readonly IUserDA _iuserDA;

        public PermissionBus(IPermissionDA ipermissionDA, IUserDA iuserDA)
        {
            this._ipermissionDA = ipermissionDA;
            this._iuserDA = iuserDA;
        }

        public List<Actions> GetActionsByIdUserAndMenuId(long userId, long menuId)
        {
            return _ipermissionDA.GetActionsByIdUserAndMenuId(_iuserDA.GetUsersForByPersonId(userId).UserRoleId, menuId);
        }
    }
}
