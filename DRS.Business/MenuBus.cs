using DRS.Data;
using DRS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Business
{
    public class MenuBus
    {
        private readonly IMenuDA iMenuDA;
        public MenuBus(IMenuDA iMenuDA)
        {
            this.iMenuDA = iMenuDA;
        }

        public List<Menu> GetMenusByIdUser(long userId)
        {
            return iMenuDA.GetMenusByIdUser(userId);
        }
    }
}
