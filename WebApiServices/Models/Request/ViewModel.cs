using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class ViewModel
    {
        public Nullable<int> Id { get; set; }
        public String Codigo { get; set; }
        public String Nombre { get; set; }
        public Nullable<int> Estado { get; set; }
    }
}