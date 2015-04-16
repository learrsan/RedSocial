using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace RedSocial.Utilidades
{
    public class Serializador
    {
        public static String Serializar<T>(T objeto)
        {
            var serializer = new JavaScriptSerializer();
            var res = serializer.Serialize(objeto);
            return res;
        }

        public static T Deserializar<T>(String texto)
        {
            var serializer = new JavaScriptSerializer();
            var res = serializer.Deserialize<T>(texto);
            return res;
        }
    }
}