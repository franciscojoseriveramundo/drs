using DRS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Data
{
    public interface IMenuDA
    {
        List<Menu> GetMenusByIdUser(long userId);
    }
}
