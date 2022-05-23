using DRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Data
{
    public interface IPermissionDA
    {
        List<Actions> GetActionsByIdUserAndMenuId(long userId, long menuId);
        List<Actions> GetActionsForRoleUser(int userRoleId);
    }
}
