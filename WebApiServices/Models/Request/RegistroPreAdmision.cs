using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class RegistroPreAdmision
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual List<WCO_ListarPreCarga_Result> list_PreAdmision { get; set; }
    }
}