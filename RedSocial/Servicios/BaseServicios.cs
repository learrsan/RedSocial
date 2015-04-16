using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedSocial.Servicios
{
    public class BaseServicios
    {
        public String Url { get; set; }

        public BaseServicios(String url)
        {
            Url = url;
        }
    }
}