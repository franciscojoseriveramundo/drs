using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Entities.Rest
{
    public class Request
    {
        public string Url { get; set; }
        public string Resource { get; set; }
        public object Parameters { get; set; }
        public object Body { get; set; }
        public Login Authentication { get; set; }
        //public Token Token { get; set; }
        public Int64 UsrLgn { get; set; }
    }
}
